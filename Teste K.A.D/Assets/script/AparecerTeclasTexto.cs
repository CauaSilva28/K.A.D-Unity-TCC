using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AparecerTeclasTexto : MonoBehaviour
{
    public GameObject elementoTeclas;
    public Text textoTeclas;
    public string texto;

    public bool aparecer;

    // Update is called once per frame
    void Update()
    {
        if(aparecer){
            StopAllCoroutines();
            elementoTeclas.SetActive(true);
            elementoTeclas.GetComponent<Animator>().SetInteger("transition", 1);
            textoTeclas.text = texto;
        }
        else{
            StartCoroutine(SairTextoTeclas());
        }
    }

    IEnumerator SairTextoTeclas(){
        elementoTeclas.GetComponent<Animator>().SetInteger("transition", 2);

        yield return new WaitForSeconds(1f);

        elementoTeclas.GetComponent<Animator>().SetInteger("transition", 0);
        elementoTeclas.SetActive(false);
        textoTeclas.text = "";
    }
}
