using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColetarItens : MonoBehaviour
{
    private int itensColetados = 0;
    public Text mensagemColetados;
    public Text textoCimaConserto;

    private GameObject itemParaColetar;

    public Slider barraConserto;
    public GameObject barraConsertoObj;
    public GameObject barraVidaKombi;
    private bool podeColetar = false; // Variável de controle

    public GameObject spawnarInimigos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mensagemColetados.enabled = true;
        mensagemColetados.text = "Procure os itens necessários para o conserto: " + itensColetados + "/6";

        if(itensColetados >= 6){
            mensagemColetados.enabled = false;
            barraVidaKombi.SetActive(true);
            barraConsertoObj.SetActive(true);
            barraConserto.value += 0.00002f;
            textoCimaConserto.enabled = true;
            textoCimaConserto.text = "PROTEJA SEU ZÉ DOS INIMIGOS";
            spawnarInimigos.SetActive(true);
        }

        if (podeColetar && Input.GetKey(KeyCode.F))
        {
            itensColetados++;
            Destroy(itemParaColetar);
            podeColetar = false;
            textoCimaConserto.enabled = false;
            textoCimaConserto.text = "";
        }
    }

    private void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Itens")){
            textoCimaConserto.enabled = true;
            textoCimaConserto.text = "Aperte \"F\" para coletar o item";
            podeColetar = true;
            itemParaColetar = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other){
        if(other.gameObject.CompareTag("Itens")){
            textoCimaConserto.enabled = false;
            textoCimaConserto.text = "";
            podeColetar = false;
            itemParaColetar = null;
        }
    }
}
