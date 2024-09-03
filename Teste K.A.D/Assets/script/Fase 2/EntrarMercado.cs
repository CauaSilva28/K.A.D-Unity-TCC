using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntrarMercado : MonoBehaviour
{
    public GameObject[] player;
    public Transform posicaoMercadoEntrada;
    public TelaSelecionarPerso selecaoPerso;

    public AparecerTeclasTexto scriptAparecerTeclas;

    private bool entrarMercado = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (entrarMercado)
        {
            Vector3 posicaoPlayerMercado = posicaoMercadoEntrada.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            scriptAparecerTeclas.aparecer = true;
            scriptAparecerTeclas.texto = "Aperte \"F\" para entrar no mercado";
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.F))
            {
                entrarMercado = true;
            }
        }
    }
}
