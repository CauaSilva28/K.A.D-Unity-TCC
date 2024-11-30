using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class FimFase2 : MonoBehaviour
{
    public AparecerTeclasTexto scriptAparecerTeclas;
    public PausarJogo pauseJogo;
    public GameObject personagens;
    public GameObject cutsceneInicio;

    public GameObject cutsceneFim;

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
                scriptAparecerTeclas.aparecer = false;
                scriptAparecerTeclas.texto = "";
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
        pauseJogo.perdendo = true;
        cutsceneFim.SetActive(true);
        cutsceneFim.GetComponent<PlayableDirector>().Play();
        personagens.SetActive(false);
        cutsceneInicio.SetActive(false);

        PlayerPrefs.SetInt("Fase2Completa", 1);
    }

    public void IrFase3(){
        SceneManager.LoadScene("Fase3");
    }
}
