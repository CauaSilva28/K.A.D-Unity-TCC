using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossSeguePlayer : MonoBehaviour
{
    [Header("Atributos")]
    public int vidaBoss;
    public float danoBoss;
    public float chargeForce;
    public float rotationSpeed;
    public float stopDuration;

    private float calculoVida;

    [Header("Referências")]
    public GameObject[] player;
    public GameObject particulaPrefab;
    public GameObject particulaInimigoPrefab;
    public AudioSource somColisao;
    public AudioSource somColisaoInimigo;
    public AudioSource gritoBossAtaque;
    public Slider vidaPerso;
    public TelaSelecionarPerso selecaoPerso;
    public Slider vidaSlideBoss;

    private Vector3 chargeDirection;
    public bool isCharging = false;
    private bool isWaiting = false;
    private Vector3 targetPosition;

    private Animator animBoss;

    [Header("Referências vitória")]
    public AudioSource gritoBoss;
    public GameObject spawnInimigo;
    public GameObject telaTransicao;
    public GameObject cutsceneFim;
    public GameObject personagens;
    private bool aconteceuVitoria;
    private bool ficouMaisForte;

    void Start(){
        animBoss = GetComponent<Animator>();
        calculoVida = 1f / vidaBoss;
    }

    private void Update()
    {
        vidaSlideBoss.value = vidaBoss * calculoVida;

        Vitoria();

        if(vidaBoss < 5 && !ficouMaisForte){
            stopDuration /= 2;
            chargeForce += 20;
            ficouMaisForte = true;
        }

        if (isCharging && vidaPerso.value > 0 && vidaBoss > 0)
        {
            // Move o Boss diretamente na direção do alvo
            transform.position += chargeDirection * chargeForce * Time.deltaTime;

            // Rotaciona suavemente para a direção do movimento
            Quaternion targetRotation = Quaternion.LookRotation(chargeDirection);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );

            // Verifica se chegou ao destino
            if (Vector3.Distance(transform.position, targetPosition) <= 1f)
            {
                PararImpulso();
            }
        }
        else{
            animBoss.SetBool("correu", false);
        }
    }

    public void IniciarImpulso()
    {
        if (player[selecaoPerso.numPerso].GetComponent<Transform>() != null && !isWaiting && vidaPerso.value > 0 && vidaBoss > 0)
        {
            gritoBossAtaque.Play();
            animBoss.SetBool("correu", true);
            // Define o alvo como a posição atual do jogador
            targetPosition = player[selecaoPerso.numPerso].GetComponent<Transform>().position;

            // Define a direção da investida
            chargeDirection = (targetPosition - transform.position).normalized;

            // Inicia a investida
            isCharging = true;
        }
    }

    private void PararImpulso()
    {
        animBoss.SetBool("correu", false);
        isCharging = false;
        StartCoroutine(WaitBeforeNextCharge());
    }

    private IEnumerator WaitBeforeNextCharge()
    {
        isWaiting = true;

        // Aguarda o tempo de pausa
        yield return new WaitForSeconds(stopDuration);

        // Permite nova investida
        isWaiting = false;

        // Reinicia o ciclo de investida
        IniciarImpulso();
    }

    private void PararMoverLado()
    {
        StartCoroutine(recuarLado());

        // Pausa antes de tentar novamente
        PararImpulso();
    }

    IEnumerator recuarLado(){
        Vector3 direcaoRecuo = (transform.position - transform.right).normalized;
        Vector3 posicaoInicial = transform.position;
        Vector3 posicaoFinalRecuo = posicaoInicial + direcaoRecuo * 40f;

        float tempoInicio = Time.time;
        while (Time.time < tempoInicio + 1f)
        {
            Vector3 novaPosicao = Vector3.Lerp(posicaoInicial, posicaoFinalRecuo, (Time.time - tempoInicio) / 1f);
            novaPosicao.y = posicaoInicial.y;
            transform.position = novaPosicao;
            yield return null;
        }
    }

    private void Vitoria(){
        if(vidaBoss <= 0 && !aconteceuVitoria){
            StartCoroutine(vitoria());
            aconteceuVitoria = true;
        }
    }

    IEnumerator vitoria(){
        gritoBoss.Play();
        animBoss.SetBool("morreu", true);
        animBoss.SetBool("correu", false);
        GetComponent<Collider>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;

        yield return new WaitForSeconds(4f);

        player[selecaoPerso.numPerso].GetComponent<Movimento>().perdendo = true;
        player[selecaoPerso.numPerso].GetComponent<AnimacoesPerso>().enabled = false;
        player[selecaoPerso.numPerso].GetComponent<Atacar>().enabled = false;
        player[selecaoPerso.numPerso].GetComponent<Rigidbody>().isKinematic = true;
        player[selecaoPerso.numPerso].GetComponent<Animator>().SetInteger("transition", 0);
        spawnInimigo.GetComponent<SpawnarInimigos>().DestruirTodosInimigos();
        telaTransicao.SetActive(true);
        telaTransicao.GetComponent<Animator>().SetInteger("transition", 2);

        yield return new WaitForSeconds(1f);

        Destroy(spawnInimigo);

        yield return new WaitForSeconds(2f);

        gameObject.SetActive(false);
        personagens.SetActive(false);
        cutsceneFim.SetActive(true);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (isCharging)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                // Aplica dano ao jogador
                vidaPerso.value -= danoBoss;

                // Para a investida ao colidir
                PararImpulso();
            }
            else if (other.gameObject.CompareTag("parede"))
            {
                // Instancia a partícula ao chegar no destino
                Instantiate(particulaPrefab, transform.position, Quaternion.identity);

                // Reproduz som de colisão
                somColisao.Play();

                // Para a investida ao colidir com obstáculos
                PararImpulso();
            }
            else if(other.gameObject.CompareTag("pilar")){
                vidaBoss -= 1;
                other.gameObject.GetComponent<VidaPilar>().vidaPilar--;

                // Instancia a partícula ao chegar no destino
                Instantiate(particulaPrefab, transform.position, Quaternion.identity);

                // Reproduz som de colisão
                somColisao.Play();

                // Para a investida ao colidir com obstáculos
                PararMoverLado();
            }

            if(other.gameObject.CompareTag("inimigo")){
                Destroy(other.gameObject);

                Instantiate(particulaInimigoPrefab, transform.position, Quaternion.identity);

                somColisaoInimigo.Play();
            }
        }
    }
}
