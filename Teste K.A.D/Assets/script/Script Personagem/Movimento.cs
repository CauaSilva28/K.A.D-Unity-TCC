using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimento : MonoBehaviour
{
    private float velocidade = 30f;
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
    private float rotationSpeed = 2f;
    private float veloDash = 80f; // A velocidade do dash deve ser maior que a velocidade normal
    private float tempoDash = 0.6f; // Duração do dash em segundos
    private float gravidade = 9.8f;
    private float velocidadeVertical = 0f;
    public bool isDashing = false;

    private ParticleSystem DashEfeito;

    public AudioSource somDash;

    private bool podeDash = true;

    private AnimacoesPerso animPerso;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        controller = GetComponent<CharacterController>();
        DashEfeito = GetComponent<ParticleSystem>();
        animPerso = GetComponent<AnimacoesPerso>();
    }

    void Update()
    {
        if (isDashing)
            return; // Se o personagem estiver dashing, sai do método Update.
        
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.LeftShift))
        {
            velocidade = 60f;
        }
        else
        {
            velocidade = 30f;
        }

        // Movimentacao-------------------------------
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * velocidade;
        moveDirection = transform.TransformDirection(moveDirection);

        // Gravidade-----------------------------
        if (controller.isGrounded)
        {
            velocidadeVertical = 0f; // Reseta a velocidade vertical se estiver no chao
        }
        else
        {
            velocidadeVertical -= gravidade * Time.deltaTime; // Aplica a gravidade
        }

        moveDirection.y = velocidadeVertical;

        controller.Move(moveDirection * Time.deltaTime);
        // FIM Movimentacao-------------------------------

        // Rotacaoo-------------------------------
        float mouseXInput = Input.GetAxis("Mouse X");
        transform.Rotate(0f, mouseXInput * rotationSpeed, 0f);
        // FIM Rotacaoo-------------------------------

        // Dash-------------------------------
        if (Input.GetKeyDown(KeyCode.Q) && !isDashing && Input.GetKey(KeyCode.W) || 
        Input.GetKeyDown(KeyCode.Q) && !isDashing && Input.GetKey(KeyCode.A) || 
        Input.GetKeyDown(KeyCode.Q) && !isDashing && Input.GetKey(KeyCode.S) || 
        Input.GetKeyDown(KeyCode.Q) && !isDashing && Input.GetKey(KeyCode.D))
        {
            StartCoroutine(TempoDash());
        }
    }

    IEnumerator Dash()
    {
        animPerso.emAcao = true;
        animPerso.anim.SetInteger("transition", 5);
        somDash.Play();
        DashEfeito.Play();
        isDashing = true;
        float tempoIniciar = Time.time;

        while (Time.time < tempoIniciar + tempoDash)
        {
            Vector3 dashDirection = moveDirection.normalized * veloDash;
            dashDirection.y = 0; // Opcional: pode-se desejar manter a direção de dash apenas no plano horizontal
            controller.Move(dashDirection * Time.deltaTime);
            yield return null;
        }

        isDashing = false;

        yield return new WaitForSeconds(0.3f);

        animPerso.emAcao = false;
    }

    IEnumerator TempoDash()
    {
        if (podeDash){
            podeDash = false;
            yield return StartCoroutine(Dash());
            yield return new WaitForSeconds(0.4f); // Espera adicional de 1.5 segundos após o dash para completar os 2 segundos
            podeDash = true;
        }
    }
}
