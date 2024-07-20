using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColetarItens : MonoBehaviour
{
    private int itensColetados = 0;

    private GameObject itemParaColetar;

    public GameObject barraConserto;
    public GameObject barraVidaKombi;
    private bool podeColetar = false;

    public GameObject spawnarInimigos;

    public AparecerTextos textosObjetivo;

    public AparecerTeclasTexto scriptAparecerTeclas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        textosObjetivo.texto = "Procure os itens necessários para o conserto: " + itensColetados + "/6";

        if(itensColetados >= 6){
            barraVidaKombi.SetActive(true);
            barraConserto.SetActive(true);

            barraConserto.GetComponent<Slider>().value += 0.005f * Time.deltaTime;

            textosObjetivo.texto = "Proteja Seu Zé dos inimigos";

            spawnarInimigos.SetActive(true);
        }

        if (podeColetar && Input.GetKey(KeyCode.F))
        {
            itensColetados++;
            Destroy(itemParaColetar);
            podeColetar = false;
            scriptAparecerTeclas.aparecer = false;
        }
    }

    private void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Itens")){
            scriptAparecerTeclas.aparecer = true;
            scriptAparecerTeclas.texto = "Aperte \"F\" para coletar o item";
            podeColetar = true;
            itemParaColetar = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other){
        if(other.gameObject.CompareTag("Itens")){
            scriptAparecerTeclas.aparecer = false;
            podeColetar = false;
            itemParaColetar = null;
        }
    }
}
