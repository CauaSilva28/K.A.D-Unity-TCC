using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AreasTexto : MonoBehaviour
{
    public AparecerTextos textoObjetivo;
    public string texto;

    public bool temporario;
    // Start is called before the first frame update
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            textoObjetivo.aparecerTexto = true;
            textoObjetivo.texto = texto;

            if(temporario){
                Invoke("sumirObjetivo", 10);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(!temporario){
                Destroy(gameObject);
            }
        }
    }

    void sumirObjetivo(){
        textoObjetivo.aparecerTexto = false;
        textoObjetivo.texto = "";
        Destroy(gameObject);
    }
}
