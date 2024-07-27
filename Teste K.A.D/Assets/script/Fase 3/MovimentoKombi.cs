using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoKombi : MonoBehaviour
{
    private float velocidade = 100f;
    private float velodesvio = 1f;

    private float veloGiroRoda = 100f;

    public GameObject[] rodas;

    private bool dandoRe = false;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
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

        // Ajusta a velocidade de desvio baseado na velocidade do carro
        if (velocidade == 0)
        {
            velodesvio = 0;
        }
        else
        {
            velodesvio = 0.2f;
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
            velocidade = 5f;
        }
    }

    void OnCollisionExit(Collision collider){
        if(!collider.gameObject.CompareTag("estrada")){
            velocidade = 100f;
        }
    }
}
