using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Dino : MonoBehaviour
{
    public float distanciaMinima;
    public float anguloDeVisao = 45f; // Ajuste este valor conforme necessário
    public float velocidadeRotacao; // Velocidade de rotação
    public float recuoDistancia; // Distância do recuo
    public float recuoTempo; // Tempo do recuo

    public float velocidadeDino = 60f;

    private Animator anim;

    public Transform personagem;
    public Transform notPersonagem;

    private NavMeshAgent navMeshAgent;

    public Transform rotacionarDino;

    private AudioSource SomDino;

    public AudioClip Rosnando;

    public Slider vidaPerso;

    public int vidaDino;

    private bool sofrendoAcao = false;
    private bool atacando = false;
    private bool tirouVidaPerso = false;

    private Collider colisorDino;

    private bool recuando = false;
    private bool foraColisao = true;
    private bool persoAtacado = false;
    private bool dropou = false;

    public Movimento movePerso;

    public bool perseguindoPlayer;

    public GameObject curaPrefab;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        navMeshAgent.stoppingDistance = distanciaMinima;

        colisorDino = GetComponent<Collider>();

        SomDino = GetComponent<AudioSource>();

        // Personagem está andando de costas, esse código faz ele andar corretamente
        rotacionarDino.transform.Rotate(-180f, 180f, 0f);

        // Inicia som
        StartCoroutine(TocarSomACada10Segundos());
    }

    // Som de rosnado a cada 10 segundos
    IEnumerator TocarSomACada10Segundos()
    {
        while (true)
        {
            SomDino.PlayOneShot(Rosnando);
            yield return new WaitForSeconds(10f);
        }
    }

    // Update é chamado uma vez por frame
    void Update()
    {
        if(Vector3.Distance(transform.position, personagem.transform.position) <= distanciaMinima){
            if(!persoAtacado){
                movePerso.sendoAtacado = true;
                persoAtacado = true;
            }
            foraColisao = false;
            sofrendoAcao = true;
            atacando = true;
            perseguindoPlayer = true;
        }
        else{
            movePerso.sendoAtacado = false;
            sofrendoAcao = false;
            atacando = false;
            foraColisao = true;
        }

        if (vidaDino > 0)
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
                    if (navMeshAgent.speed >= velocidadeDino-10)
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
                    StartCoroutine(TirarVidaPerso());
                }

                RotacionarParaPersonagem(); // Adiciona a rotação para o personagem no Update
            }
        }
        else
        {
            StartCoroutine(EliminarDino());
        }
    }

    IEnumerator TirarVidaPerso()
    {
        anim.SetInteger("transition", 2);
        yield return new WaitForSeconds(1f);
        if (!tirouVidaPerso)
        {
            if(!recuando && !foraColisao){
                vidaPerso.value -= 0.1f;
            }
            tirouVidaPerso = true;
            yield return new WaitForSeconds(2f);
            tirouVidaPerso = false;
        }
        yield return null; // Aguarda o próximo frame antes de continuar o loop
    }

    IEnumerator EliminarDino()
    {
        anim.SetInteger("transition", 3);
        colisorDino.enabled = false;
        SomDino.enabled = false;
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
            if (numeroAleatorio > 7)
            {
                GameObject curaInstance = Instantiate(curaPrefab, transform.position, Quaternion.identity);
                curaInstance.tag = "Cura";
            }
            dropou = true;
        }
    }

    public void Recuar()
    {
        StartCoroutine(RecuarCoroutine());
    }

    IEnumerator RecuarCoroutine()
    {
        recuando = true;
        anim.SetInteger("transition", 4);

        Vector3 direcaoRecuo = (transform.position - personagem.position).normalized;
        Vector3 posicaoFinalRecuo = transform.position + direcaoRecuo * recuoDistancia;

        float tempoInicio = Time.time;
        while (Time.time < tempoInicio + recuoTempo)
        {
            transform.position = Vector3.Lerp(transform.position, posicaoFinalRecuo, (Time.time - tempoInicio) / recuoTempo);
            yield return null;
        }

        recuando = false;

        navMeshAgent.speed = 0;

        yield return new WaitForSeconds(2f);

        navMeshAgent.speed = velocidadeDino;
    }
}