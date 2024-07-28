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

    private float veloGiroRoda = 100f;
    private float tempoDescanco = 0f;

    public GameObject[] rodas;
    public GameObject turbo;

    public bool dandoRe = false;
    private bool fimNitro = false;
    private bool colidiu = false;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if(!colidiu && !dandoRe){
            utilizarTurbo();
        }
        else{
            turbo.SetActive(false);
        }

        RecuperarStamina();

        // Movimento automatico
        float currentVeloGiroRoda = veloGiroRoda * Time.deltaTime+2;

        for (var i = 0; i < rodas.Length; i++)
        {
            rodas[i].transform.Rotate(currentVeloGiroRoda, 0f, 0f);
        }

        Vector3 movement = transform.forward * velocidade * Time.deltaTime;

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

    private void RecuperarStamina(){
        if(fimNitro){
            tempoDescanco += Time.deltaTime * 1;
            if(tempoDescanco > 2){
                staminaKombi.value += 0.08f * Time.deltaTime;
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
