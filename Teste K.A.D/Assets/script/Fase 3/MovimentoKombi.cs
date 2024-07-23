using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoKombi : MonoBehaviour
{
    private float velocidade = 100f;
    private float velodesvio = 0.2f;

    private float veloGiroRoda = 100f;

    public GameObject[] rodas;

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
        transform.position += movement;


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
}
