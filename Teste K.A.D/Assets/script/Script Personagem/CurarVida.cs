using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurarVida : MonoBehaviour
{
    public float valorCura;

    public Slider vidaPerso;

    private GameObject inimigo;

    public AparecerTeclasTexto scriptAparecerTeclas;

    private bool podeCurar = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (podeCurar && Input.GetKey(KeyCode.F))
        {
            vidaPerso.value += valorCura;
            scriptAparecerTeclas.aparecer = false;
            Destroy(inimigo);
            podeCurar = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cura"))
        {
            scriptAparecerTeclas.aparecer = true;
            scriptAparecerTeclas.texto = "Aperte \"F\" para utilizar a cura";
            podeCurar = true;
            inimigo = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Cura"))
        {
            scriptAparecerTeclas.aparecer = false;
            podeCurar = false;
        }
    }
}
