using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Inimigos : MonoBehaviour
{
    [Header("Valores float")]
    public float distanciaMinima;
    public float anguloDeVisao = 45f; // Ajuste este valor conforme necessário
    public float velocidadeRotacao; // Velocidade de rotação
    public float recuoDistancia; // Distância do recuo
    public float recuoTempo; // Tempo do recuo
    public float vidaInimigo;
    public float dano;
    public float velocidadeInimigo = 60f;
    public float tempoRosnar;

    [Header("Booleans")]
    private bool recuando = false;
    private bool foraColisao = true;
    private bool persoAtacado = false;
    private bool dropou = false;
    public bool perseguindoPlayer;
    public bool parado;
    public bool andandoDeCostas;
    private bool sofrendoAcao = false;
    private bool atacando = false;
    private bool tirouVidaPerso = false;

    [Header("Objetos")]
    public GameObject[] ferramentaCena;
    public GameObject[] ferramentaObj;
    public GameObject[] curaPrefab;
    public GameObject[] personagem;
    public GameObject retopoInimigo;

    [Header("Elementos objetos")]
    public Transform notPersonagem;
    public Transform rotacionarInimigo;

    public Animator anim;

    public NavMeshAgent navMeshAgent;

    public Slider vidaPerso;

    private Collider colisorInimigo;

    public Material[] materiais;

    public TelaSelecionarPerso persoSelecionado;

    [Header("Audios")]
    private AudioSource SomInimigo;
    public AudioSource[] SomDanoPlayer;
    public AudioClip Rosnando;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        navMeshAgent.stoppingDistance = distanciaMinima;

        colisorInimigo = GetComponent<Collider>();

        SomInimigo = GetComponent<AudioSource>();

        if(andandoDeCostas){
            // Personagem está andando de costas, esse código faz ele andar corretamente
            rotacionarInimigo.transform.Rotate(-180f, 180f, 0f);
        }

        // Inicia som
        StartCoroutine(TocarSomDepoisDeUmTempo());

        CorSorteada();
    }

    IEnumerator TocarSomDepoisDeUmTempo()
    {
        while (true)
        {
            SomInimigo.PlayOneShot(Rosnando);
            yield return new WaitForSeconds(tempoRosnar);
        }
    }

    // Update é chamado uma vez por frame
    void Update()
    {
        if(!personagem[persoSelecionado.numPerso].GetComponent<Movimento>().perdendo){
            if (vidaInimigo > 0)
            {
                DistanciaInimigoPerso();
                
                if (!recuando)
                {
                    if(parado){
                        navMeshAgent.speed = 0;
                    }
                    else{
                        navMeshAgent.speed = velocidadeInimigo;
                        RotacionarParaPersonagem(); // Adiciona a rotação para o personagem no Update
                    }

                    MovimentoInimigo();

                    if (atacando && EstaOlhandoParaPersonagem())
                    {
                        if(navMeshAgent.speed > 0){
                            anim.SetBool("taAtacando", true);
                        }
                    }
                    else{
                        anim.SetBool("taAtacando", false);
                    }
                }
            }
            else
            {
                anim.SetBool("taAtacando", false);
                StartCoroutine(EliminarInimigo());
            }
        }
        else{
            anim.SetBool("taRecuando", false);
            anim.SetBool("taAtacando", false);
            anim.SetInteger("transition", 0);
        }
    }

    void AtacarPlayer(){
        if(vidaInimigo > 0 && !recuando && !foraColisao && EstaOlhandoParaPersonagem()){
            vidaPerso.value -= dano;
            SomDanoPlayer[persoSelecionado.numPerso].Play();
        }
    }

    private void DistanciaInimigoPerso(){
        if(Vector3.Distance(transform.position, personagem[persoSelecionado.numPerso].GetComponent<Transform>().transform.position) <= distanciaMinima){
            if(!persoAtacado){
                persoAtacado = true;
            }
            foraColisao = false;
            sofrendoAcao = true;
            atacando = true;
            perseguindoPlayer = true;
            parado = false;
        }
        else{
            sofrendoAcao = false;
            atacando = false;
            foraColisao = true;
        }
    }

    private void MovimentoInimigo(){
        if(!perseguindoPlayer){
            navMeshAgent.SetDestination(notPersonagem.position);
        }
        else{
            navMeshAgent.SetDestination(personagem[persoSelecionado.numPerso].GetComponent<Transform>().position);
        }

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
        if (personagem[persoSelecionado.numPerso].GetComponent<Transform>() == null)
            return false;

        Vector3 direcaoParaPersonagem = (personagem[persoSelecionado.numPerso].GetComponent<Transform>().position - transform.position).normalized;
        float angulo = Vector3.Angle(transform.forward, direcaoParaPersonagem);

        return angulo < anguloDeVisao;
    }

    void RotacionarParaPersonagem()
    {
        if(perseguindoPlayer){
            if (personagem[persoSelecionado.numPerso].GetComponent<Transform>() == null)
                return;

            Vector3 direcaoParaPersonagem = (personagem[persoSelecionado.numPerso].GetComponent<Transform>().position - transform.position).normalized;
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

            if(ferramentaCena[persoSelecionado.numPerso].activeSelf){
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
                    GameObject ferramentaInstance = Instantiate(ferramentaObj[persoSelecionado.numPerso], transform.position, Quaternion.identity);
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

        Vector3 direcaoRecuo = (transform.position - personagem[persoSelecionado.numPerso].GetComponent<Transform>().position).normalized;
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
