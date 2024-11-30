using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovimentoKombi : MonoBehaviour
{
    public float valorVelocidade;
    public float valorVeloTurbo;
    public float velodesvio;

    private float velocidade;

    public Slider staminaKombi;

    public float veloGiroRoda = 100f;
    private float tempoDescanco = 0f;

    public GameObject[] rodas;
    public GameObject turbo;

    public bool dandoRe = false;
    private bool fimNitro = false;
    private bool colidiu = false;

    public PausarJogo pauseJogo;

    void Update()
    {
        if(!pauseJogo.pausado){
            if(!colidiu && !dandoRe){
                utilizarTurbo();
            }
            else{
                turbo.SetActive(false);
            }

            RecuperarStamina();
            MovimentacaoKombi();

            // Rotaciona o carro baseado nas entradas horizontais
            if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(0f, velodesvio, 0f);
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(0f, -velodesvio, 0f);
            }
        }
    }

    void OnCollisionStay(Collision collider){
        if(!collider.gameObject.CompareTag("estrada")){
            colidiu = true;
            velocidade = 5f;
        }
    }

    void OnCollisionExit(Collision collider){
        if(!collider.gameObject.CompareTag("estrada")){
            colidiu = false;
            velocidade = valorVelocidade;
        }
    }

    void utilizarTurbo(){
        if(staminaKombi.value <= 0){
            fimNitro = true;
            turbo.SetActive(false);
        }

        if(Input.GetKey(KeyCode.LeftShift) && !fimNitro){
            turbo.SetActive(true);
            velocidade = valorVeloTurbo;
            staminaKombi.value -= 0.15f * Time.deltaTime;
        }
        else{
            turbo.SetActive(false);
            velocidade = valorVelocidade;
        }
    }

    private void MovimentacaoKombi(){
        float currentVeloGiroRoda = veloGiroRoda * Time.deltaTime+2;

        for (var i = 0; i < rodas.Length; i++)
        {
            rodas[i].transform.Rotate(0f, 0f, -currentVeloGiroRoda);
        }

        Vector3 movement = transform.right * velocidade * Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.S)){
            dandoRe = true;
        }
        else if(Input.GetKeyDown(KeyCode.W)){
            dandoRe = false;
        }

        if(dandoRe){
            transform.position -= movement;
        }
        else{
            transform.position += movement;
        }
    }

    private void RecuperarStamina(){
        if(fimNitro){
            tempoDescanco += Time.deltaTime * 1;
            if(tempoDescanco > 2){
                staminaKombi.value += 0.09f * Time.deltaTime;
                if(staminaKombi.value >= 1){
                    fimNitro = false;
                }
            }
        }
        else{
            tempoDescanco = 0f;
        }
    }
}
