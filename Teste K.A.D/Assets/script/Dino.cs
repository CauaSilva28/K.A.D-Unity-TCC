using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Dino : MonoBehaviour
{
    public float distanciaMinima;
    public float anguloDeVisao = 45f; // Ajuste este valor conforme necessário
    public float velocidadeRotacao = 2f; // Velocidade de rotação

    private Animator anim;
    public Transform personagem;
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
    private Animator dinoMorteAnim;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        navMeshAgent.stoppingDistance = distanciaMinima;

        colisorDino = GetComponent<Collider>();
        dinoMorteAnim = GetComponent<Animator>();

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
        if(vidaDino > 0){
            navMeshAgent.SetDestination(personagem.position);

            float distancia = Vector3.Distance(transform.position, personagem.position);

            if (!sofrendoAcao)
            {
                if (navMeshAgent.speed >= 50)
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
        else{
            StartCoroutine(EliminarDino());
        }
    }

    IEnumerator TirarVidaPerso()
    {
        if (!tirouVidaPerso)
        {
            vidaPerso.value -= 0.1f;
            tirouVidaPerso = true;
            yield return new WaitForSeconds(2f);
            tirouVidaPerso = false;
        }
        yield return null; // Aguarda o próximo frame antes de continuar o loop
    }

    IEnumerator EliminarDino(){
        dinoMorteAnim.SetInteger("transition", 3);
        colisorDino.enabled = false;
        SomDino.enabled = false;

        yield return new WaitForSeconds(5f);

        Destroy(gameObject);
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            anim.SetInteger("transition", 2);
            sofrendoAcao = true;
            atacando = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            sofrendoAcao = false;
            atacando = false;
        }
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
        if (personagem == null)
            return;

        Vector3 direcaoParaPersonagem = (personagem.position - transform.position).normalized;
        Quaternion rotacaoParaPersonagem = Quaternion.LookRotation(direcaoParaPersonagem);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotacaoParaPersonagem, velocidadeRotacao * Time.deltaTime);
    }
}