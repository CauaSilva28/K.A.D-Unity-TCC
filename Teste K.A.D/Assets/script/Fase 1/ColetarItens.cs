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
    public GameObject areaFalaColetaItens;
    private bool podeColetar = false;
    private bool aconteceuEnumerator = false;

    public GameObject spawnarInimigos;

    public AparecerTextos textosObjetivo;

    public AparecerTeclasTexto scriptAparecerTeclas;

    public AudioSource somColetouItem;

    public bool perdendo = false;

    private bool ativouFala = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(itensColetados >= 6){
            if(!ativouFala){
                areaFalaColetaItens.SetActive(true);
                ativouFala = true;
            }
            barraVidaKombi.SetActive(true);
            barraConserto.SetActive(true);

            if(!perdendo){
                barraConserto.GetComponent<Slider>().value += 0.005f * Time.deltaTime;
            }

            if(!aconteceuEnumerator){
                StartCoroutine(textoInimigos());
            }

            spawnarInimigos.SetActive(true);
        }
        else{
            textosObjetivo.texto = "Procure os itens necessários para o conserto: " + itensColetados + "/6";
        }

        if (podeColetar && Input.GetKey(KeyCode.F))
        {
            somColetouItem.Play();
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

    IEnumerator textoInimigos(){
        textosObjetivo.texto = "Proteja Seu Zé dos inimigos";

        yield return new WaitForSeconds(10f);

        textosObjetivo.aparecerTexto = false;
        aconteceuEnumerator = true;
    }
}
