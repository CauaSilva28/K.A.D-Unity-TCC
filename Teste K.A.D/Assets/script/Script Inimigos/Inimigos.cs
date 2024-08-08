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
    public float vidaInimigo;

    public float dano;

    public float velocidadeInimigo = 60f;

    public Animator anim;

    public Transform personagem;
    public Transform notPersonagem;

    public NavMeshAgent navMeshAgent;

    public Transform rotacionarInimigo;

    private AudioSource SomInimigo;

    public AudioSource SomDanoPlayer;

    public AudioClip Rosnando;

    public Slider vidaPerso;

    private bool sofrendoAcao = false;
    private bool atacando = false;
    private bool tirouVidaPerso = false;

    private Collider colisorInimigo;

    private bool recuando = false;
    private bool foraColisao = true;
    private bool persoAtacado = false;
    private bool dropou = false;

    public bool perseguindoPlayer;

    public GameObject ferramentaCena;
    public GameObject ferramentaObj;

    public GameObject[] curaPrefab;

    public Material[] materiais;
    public GameObject retopoInimigo;

    public Movimento movePerso;

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
            yield return new WaitForSeconds(10f);
        }
    }

    // Update é chamado uma vez por frame
    void Update()
    {
        if(!movePerso.perdendo){
            if (vidaInimigo > 0)
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
                            anim.SetBool("taAtacando", true);
                        }
                    }
                    else{
                        anim.SetBool("taAtacando", false);
                    }

                    RotacionarParaPersonagem(); // Adiciona a rotação para o personagem no Update
                }
            }
            else
            {
                StartCoroutine(EliminarInimigo());
            }
        }
    }

    void AtacarPlayer(){
        if(!recuando && !foraColisao && EstaOlhandoParaPersonagem()){
            vidaPerso.value -= dano;
            SomDanoPlayer.Play();
        }
    }

    IEnumerator EliminarInimigo()
    {
        anim.SetInteger("transition", 2);
        anim.SetBool("taMorrendo", true);
        SomInimigo.enabled = false;
        navMeshAgent.enabled = false;

        yield return new WaitForSeconds(1f);

        DropItem();
        colisorInimigo.enabled = false;

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

            if(ferramentaCena.activeSelf){
                if (numeroAleatorio > 7)
                {
                    GameObject curaInstance = Instantiate(curaPrefab[curaAleatoria], transform.position, Quaternion.identity);
                    curaInstance.tag = "Cura";
                }
            }
            else{
                if (numeroAleatorio > 5 && numeroAleatorio < 9)
                {
                    GameObject curaInstance = Instantiate(curaPrefab[curaAleatoria], transform.position, Quaternion.identity);
                    curaInstance.tag = "Cura";
                }
                else if(numeroAleatorio >= 9){
                    GameObject ferramentaInstance = Instantiate(ferramentaObj, transform.position, Quaternion.identity);
                    ferramentaInstance.tag = "Ferramenta";
                }
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
        anim.SetBool("taRecuando", true);
        anim.SetBool("taAtacando", false);
        anim.SetInteger("transition", 0);

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

        anim.SetBool("taRecuando", false);

        navMeshAgent.speed = 0;

        yield return new WaitForSeconds(1f);

        recuando = false;
        navMeshAgent.speed = velocidadeInimigo;
    }
}
