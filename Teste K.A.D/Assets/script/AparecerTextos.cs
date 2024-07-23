using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AparecerTextos : MonoBehaviour
{
    public GameObject elementoObjetivos;
    public Text textoObjetivo;

    public string texto;

    public bool aparecerTexto;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(aparecerTexto){
            StopAllCoroutines();
            elementoObjetivos.SetActive(true);
            elementoObjetivos.GetComponent<Animator>().SetInteger("transition", 1);
            textoObjetivo.text = texto;
        }
        else{
            StartCoroutine(SairTextoObjetivo());
        }
    }

    IEnumerator SairTextoObjetivo(){
        elementoObjetivos.GetComponent<Animator>().SetInteger("transition", 2);

        yield return new WaitForSeconds(1f);

        elementoObjetivos.GetComponent<Animator>().SetInteger("transition", 0);
        elementoObjetivos.SetActive(false);
        textoObjetivo.text = "";
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            aparecerTexto = true;
        }
    }
}
