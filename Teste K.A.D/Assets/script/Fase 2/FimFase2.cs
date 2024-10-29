using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class FimFase2 : MonoBehaviour
{
    public AparecerTeclasTexto scriptAparecerTeclas;
    public GameObject personagens;
    public GameObject cutsceneInicio;

    public GameObject cutsceneFim;

    public 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
          
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            scriptAparecerTeclas.aparecer = true;
            scriptAparecerTeclas.texto = "Aperte \"F\" para sair com a Kombi";
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player")){
            if (Input.GetKey(KeyCode.F))
            {
                FimDeJogo();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            scriptAparecerTeclas.aparecer = false;
            scriptAparecerTeclas.texto = "";
        }
    }

    void FimDeJogo()
    {
        cutsceneFim.GetComponent<PlayableDirector>().Play();
        personagens.SetActive(false);
        cutsceneInicio.SetActive(false);

        PlayerPrefs.SetInt("Fase2Completa", 1);
    }
}
