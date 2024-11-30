using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movimento : MonoBehaviour
{
    [Header("Valores float")]  
    public float velocidadeCorrida;
    public float velocidadeAndando;
    public float valorDesgaste;
    public float valorDesgasteDash;
    public float valorRecuperacao;

    public float velocidade;
    private float tempoDescanco;
    private float rotationSpeed = 2f;
    private float veloDash = 80f; // A velocidade do dash deve ser maior que a velocidade normal
    private float tempoDash = 0.6f; // Duração do dash em segundos
    private float gravidade = 200f;
    private float velocidadeVertical = 0f;

    [Header("Elementos canvas")]
    public Slider staminaPerso;  

    [Header("Scripts")]
    private AnimacoesPerso animPerso;
    public PausarJogo pauseJogo;


    [Header("Objetos")]
    public Vector3 moveDirection = Vector3.zero;
    private CharacterController controller; 
    public ParticleSystem DashEfeito;
    public AudioSource somDash;

    [Header("Booleans")]
    public bool cansou;
    private bool correndo;
    public bool isDashing = false;
    private bool podeDash = true;
    public bool perdendo;
    public bool pausado;
    public bool pulando = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animPerso = GetComponent<AnimacoesPerso>();
    }

    void Update()
    {
        if(!perdendo && !pauseJogo.pausado){
            if (isDashing)
                return; // Se o personagem estiver dashing, sai do método Update.
            
            Movimentacao();
            Gravidade();
            Rotacao();

            // Dash-------------------------------
            if (Input.GetKeyDown(KeyCode.Q) && !isDashing && Input.GetKey(KeyCode.W) || 
            Input.GetKeyDown(KeyCode.Q) && !isDashing && Input.GetKey(KeyCode.A) || 
            Input.GetKeyDown(KeyCode.Q) && !isDashing && Input.GetKey(KeyCode.S) || 
            Input.GetKeyDown(KeyCode.Q) && !isDashing && Input.GetKey(KeyCode.D))
            {
                if(!cansou){
                    tempoDescanco = 0f;
                    StartCoroutine(TempoDash());
                }
            }
        }
    }

    IEnumerator Dash()
    {
        animPerso.anim.SetBool("Dashing", true);
        somDash.Play();
        DashEfeito.Play();
        isDashing = true;
        float tempoIniciar = Time.time;

        while (Time.time < tempoIniciar + tempoDash)
        {
            Vector3 dashDirection = moveDirection.normalized * veloDash;
            dashDirection.y = 0; // Opcional: pode-se desejar manter a direção de dash apenas no plano horizontal
            controller.Move(dashDirection * Time.deltaTime);
            staminaPerso.value -= valorDesgasteDash * Time.deltaTime;
            yield return null;
        }

        isDashing = false;
        animPerso.anim.SetBool("Dashing", false);
    }

    IEnumerator TempoDash()
    {
        if (podeDash){
            podeDash = false;
            yield return StartCoroutine(Dash());
            yield return new WaitForSeconds(0.4f);
            podeDash = true;
        }
    }

    private void RecuperarStamina(){
        if(staminaPerso.value < 1){
            tempoDescanco += Time.deltaTime * 1;
            if(tempoDescanco > 2){
                staminaPerso.value += valorRecuperacao * Time.deltaTime;
            }
        }
    }

    private void Movimentacao(){
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.LeftShift))
        {
            if(!cansou){
                tempoDescanco = 0f;
                staminaPerso.value -= valorDesgaste * Time.deltaTime;
                velocidade = velocidadeCorrida;
                correndo = true;
            }
            else{
                velocidade = velocidadeAndando;
            }
        }
        else
        {
            velocidade = velocidadeAndando;
            correndo = false;
        }

        //Stamina-------------------------------------
        if(staminaPerso.value <= 0){
            cansou = true;
        }
        else{
            cansou = false;
        }

        if(!correndo && !isDashing){
            RecuperarStamina();
        }

        // Movimentacao-------------------------------
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * velocidade;
        moveDirection = transform.TransformDirection(moveDirection);
    }

    private void Gravidade(){
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
    }

    private void Rotacao(){
        float mouseXInput = Input.GetAxis("Mouse X");
        transform.Rotate(0f, mouseXInput * rotationSpeed, 0f);
    }
}
