using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraVidaInimigos : MonoBehaviour
{
    public Slider barraVidaInimigo;
    private float calculo;

    void Start(){
        calculo = 1f / GetComponent<Inimigos>().vidaInimigo;
    }
    // Update is called once per frame
    void Update()
    {
        barraVidaInimigo.value = GetComponent<Inimigos>().vidaInimigo * calculo;
    }
}
