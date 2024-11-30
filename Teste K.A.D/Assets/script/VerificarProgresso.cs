using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerificarProgresso : MonoBehaviour
{
    public GameObject[] fasesTrancada;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("Fase1Completa", 0) == 1)
        {   
            fasesTrancada[0].SetActive(false);
        }

        if (PlayerPrefs.GetInt("Fase2Completa", 0) == 1)
        {
            fasesTrancada[1].SetActive(false);
        }

        if (PlayerPrefs.GetInt("Fase3Completa", 0) == 1)
        {
            fasesTrancada[2].SetActive(false);
        }

        if (PlayerPrefs.GetInt("Fase4Completa", 0) == 1)
        {
            fasesTrancada[3].SetActive(false);
        }
    }
}
