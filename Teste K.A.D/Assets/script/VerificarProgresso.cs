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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}