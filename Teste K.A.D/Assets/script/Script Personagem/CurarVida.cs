using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurarVida : MonoBehaviour
{
    public float valorCura;

    public Slider vidaPerso;

    private GameObject item;

    public GameObject ferramenta;
    public Slider vidaFerramenta;

    public AparecerTeclasTexto scriptAparecerTeclas;

    private bool podeCurar = false;
    private bool podeColetar = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (podeCurar && Input.GetKey(KeyCode.F))
        {
            vidaPerso.value += valorCura;
            scriptAparecerTeclas.aparecer = false;
            Destroy(item);
            podeCurar = false;
        }

        if(!ferramenta.activeSelf){
            if (podeColetar && Input.GetKey(KeyCode.F))
            {
                ferramenta.SetActive(true);
                vidaFerramenta.value = 1;
                scriptAparecerTeclas.aparecer = false;
                Destroy(item);
                podeColetar = false;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cura"))
        {
            scriptAparecerTeclas.aparecer = true;
            scriptAparecerTeclas.texto = "Aperte \"F\" para utilizar a cura";
            podeCurar = true;
            item = other.gameObject;
        }

        if (other.gameObject.CompareTag("Ferramenta"))
        {
            scriptAparecerTeclas.aparecer = true;
            scriptAparecerTeclas.texto = "Aperte \"F\" para coletar a ferramenta";
            podeColetar = true;
            item = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Cura"))
        {
            scriptAparecerTeclas.aparecer = false;
            podeCurar = false;
        }

        if (other.gameObject.CompareTag("Ferramenta"))
        {
            scriptAparecerTeclas.aparecer = false;
            podeColetar = false;
        }
    }
}
