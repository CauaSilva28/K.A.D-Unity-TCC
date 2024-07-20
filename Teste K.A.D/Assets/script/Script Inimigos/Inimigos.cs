using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Inimigos : MonoBehaviour
{
    public float distanciaMinima;
    public float anguloDeVisao = 45f; // Ajuste este valor conforme necessário
    public float velocidadeRotacao; // Velocidade de rotação
    public float recuoDistancia; // Distância do recuo
    public float recuoTempo; // Tempo do recuo

    public float dano;

    public float velocidadeInimigo = 60f;

    private Animator anim;

    public Transform personagem;
    public Transform notPersonagem;

    private NavMeshAgent navMeshAgent;

    public Transform rotacionarInimigo;

    private AudioSource SomInimigo;

    public AudioClip Rosnando;

    public Slider vidaPerso;

    public float vidaInimigo;

    private bool sofrendoAcao = false;
    private bool atacando = false;
    private bool tirouVidaPerso = false;

    private Collider colisorInimigo;

    private bool recuando = false;
    private bool foraColisao = true;
    private bool persoAtacado = false;
    private bool dropou = false;

    public bool perseguindoPlayer;

    public GameObject[] curaPrefab;

    public Material[] materiais;
    public GameObject retopoInimigo;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        navMeshAgent.stoppingDistance = distanciaMinima;

        colisorInimigo = GetComponent<Collider>();

        SomInimigo = GetComponent<AudioSource>();

        // Personagem está andando de costas, esse código faz ele andar corretamente
        rotacionarInimigo.transform.Rotate(-180f, 180f, 0f);

        // Inicia som
        StartCoroutine(TocarSomDepoisDeUmTempo());

        CorSorteada();
    }

    IEnumerator TocarSomDepoisDeUmTempo()
    {
        while (true)
        {
            SomInimigo.PlayOneShot(Rosnando);
            yield return new WaitForSeconds(5f);
        }
    }

    // Update é chamado uma vez por frame
    void Update()
    {
        if(Vector3.Distance(transform.position, personagem.transform.position) <= distanciaMinima){
            if(!persoAtacado){
                persoAtacado = true;
            }
            foraColisao = false;
            sofrendoAcao = true;
            atacando = true;
            perseguindoPlayer = true;
        }
        else{
            sofrendoAcao = false;
            atacando = false;
            foraColisao = true;
        }

        if (vidaInimigo > 0)
        {
            if (!recuando)
            {
                if(!perseguindoPlayer){
                    navMeshAgent.SetDestination(notPersonagem.position);
                }
                else{
                    navMeshAgent.SetDestination(personagem.position);
                }

                float distancia = Vector3.Distance(transform.position, personagem.position);

                if (!sofrendoAcao)
                {
                    if (navMeshAgent.speed >= velocidadeInimigo-10)
                    {
                        anim.SetInteger("transition", 1);
                    }
                    else if (navMeshAgent.speed <= 0)
                    {
                        anim.SetInteger("transition", 0);
                    }
                }

                if (atacando && EstaOlhandoParaPersonagem())
                {
                    if(navMeshAgent.speed > 0){
                        StartCoroutine(TirarVidaPerso());
                    }
                }

                RotacionarParaPersonagem(); // Adiciona a rotação para o personagem no Update
            }
        }
        else
        {
            StartCoroutine(EliminarInimigo());
        }
    }

    IEnumerator TirarVidaPerso()
    {
        anim.SetInteger("transition", 2);
        yield return new WaitForSeconds(1.3f);
        if (!tirouVidaPerso)
        {
            if(!recuando && !foraColisao){
                vidaPerso.value -= dano;
            }
            tirouVidaPerso = true;
            yield return new WaitForSeconds(2f);
            tirouVidaPerso = false;
        }
        yield return null; // Aguarda o próximo frame antes de continuar o loop
    }

    IEnumerator EliminarInimigo()
    {
        anim.SetInteger("transition", 3);
        anim.SetBool("taMorrendo", true);
        colisorInimigo.enabled = false;
        SomInimigo.enabled = false;
        navMeshAgent.enabled = false;

        yield return new WaitForSeconds(1f);

        DropItem();

        yield return new WaitForSeconds(4f);

        Destroy(gameObject);
    }

    bool EstaOlhandoParaPersonagem()
    {
        if (personagem == null)
            return false;

        Vector3 direcaoParaPersonagem = (personagem.position - transform.position).normalized;
        float angulo = Vector3.Angle(transform.forward, direcaoParaPersonagem);

        return angulo < anguloDeVisao;
    }

    void RotacionarParaPersonagem()
    {
        if(perseguindoPlayer){
            if (personagem == null)
                return;

            Vector3 direcaoParaPersonagem = (personagem.position - transform.position).normalized;
            Quaternion rotacaoParaPersonagem = Quaternion.LookRotation(direcaoParaPersonagem);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacaoParaPersonagem, velocidadeRotacao * Time.deltaTime);
        }
    }

    public void DropItem()
    {
        if (!dropou)
        {
            int numeroAleatorio = Random.Range(1, 11);
            int curaAleatoria = Random.Range(0, curaPrefab.Length);

            if (numeroAleatorio > 7)
            {
                GameObject curaInstance = Instantiate(curaPrefab[curaAleatoria], transform.position, Quaternion.identity);
                curaInstance.tag = "Cura";
            }
            dropou = true;
        }
    }

    public void Recuar()
    {
        StartCoroutine(RecuarCoroutine());
    }

    public void CorSorteada()
    {
        int materialSorteado = Random.Range(0, materiais.Length);
        retopoInimigo.GetComponent<Renderer>().material = materiais[materialSorteado];
    }

    IEnumerator RecuarCoroutine()
    {
        recuando = true;
        anim.SetInteger("transition", 4);
        anim.SetBool("taRecuando", true);

        Vector3 direcaoRecuo = (transform.position - personagem.position).normalized;
        Vector3 posicaoInicial = transform.position;
        Vector3 posicaoFinalRecuo = posicaoInicial + direcaoRecuo * recuoDistancia;

        float tempoInicio = Time.time;
        while (Time.time < tempoInicio + recuoTempo)
        {
            Vector3 novaPosicao = Vector3.Lerp(posicaoInicial, posicaoFinalRecuo, (Time.time - tempoInicio) / recuoTempo);
            novaPosicao.y = posicaoInicial.y;
            transform.position = novaPosicao;
            yield return null;
        }

        anim.SetInteger("transition", 0);
        anim.SetBool("taRecuando", false);

        navMeshAgent.speed = 0;

        yield return new WaitForSeconds(1f);

        recuando = false;
        navMeshAgent.speed = velocidadeInimigo;
    }
}
