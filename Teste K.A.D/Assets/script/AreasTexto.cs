using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AreasTexto : MonoBehaviour
{
    public AparecerTextos textoObjetivo;
    public string texto;
    // Start is called before the first frame update
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            textoObjetivo.aparecerTexto = true;
            textoObjetivo.texto = texto;
        }
    }
}
