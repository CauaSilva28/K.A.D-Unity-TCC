using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossSeguePlayer : MonoBehaviour
{
    [Header("Atributos")]
    public int vidaBoss;
    public float danoBoss;
    public float chargeForce = 10f;
    public float rotationSpeed = 5f;
    public float stopDuration = 5f;

    private float calculoVida;

    [Header("Referências")]
    public Transform[] player;
    public GameObject particulaPrefab;
    public AudioSource somColisao;
    public Slider vidaPerso;
    public TelaSelecionarPerso selecaoPerso;
    public Slider vidaSlideBoss;

    private Vector3 chargeDirection;
    public bool isCharging = false;
    private bool isWaiting = false;
    private Vector3 targetPosition;

    void Start(){
        calculoVida = 1f / vidaBoss;
    }

    private void Update()
    {
        vidaSlideBoss.value = vidaBoss * calculoVida;

        if (isCharging && vidaPerso.value > 0)
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
    }

    public void IniciarImpulso()
    {
        if (player[selecaoPerso.numPerso] != null && !isWaiting)
        {
            // Define o alvo como a posição atual do jogador
            targetPosition = player[selecaoPerso.numPerso].position;

            // Define a direção da investida
            chargeDirection = (targetPosition - transform.position).normalized;

            // Inicia a investida
            isCharging = true;
        }
    }

    private void PararImpulso()
    {
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
        // Gera um pequeno movimento lateral aleatório
        Vector3 lateralMove = transform.right * 30f;
        transform.position += lateralMove;

        // Pausa antes de tentar novamente
        PararImpulso();
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

                // Instancia a partícula ao chegar no destino
                Instantiate(particulaPrefab, transform.position, Quaternion.identity);

                // Reproduz som de colisão
                somColisao.Play();

                // Para a investida ao colidir com obstáculos
                PararMoverLado();
            }
        }
    }
}
