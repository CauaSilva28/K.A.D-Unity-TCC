using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurarVida : MonoBehaviour
{
    public float valorCura;
    public Text teclaColeta;
    public Slider vidaPerso;

    private GameObject inimigo;

    private bool podeCurar = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (podeCurar && Input.GetKey(KeyCode.F))
        {
            vidaPerso.value += valorCura;
            teclaColeta.enabled = false;
            teclaColeta.text = "";
            Destroy(inimigo);
            podeCurar = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cura"))
        {
            teclaColeta.enabled = true;
            teclaColeta.text = "Aperte \"F\" para utilizar a cura";
            podeCurar = true;
            inimigo = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Cura"))
        {
            teclaColeta.enabled = false;
            teclaColeta.text = "";
            podeCurar = false;
        }
    }
}
