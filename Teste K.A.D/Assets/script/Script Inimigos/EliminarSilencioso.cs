using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EliminarSilencioso : MonoBehaviour
{
    public GameObject[] personagem;
    public TelaSelecionarPerso persoSelecionado;

    public GameObject inimigo;
    public AparecerTeclasTexto scriptAparecerTeclas;

    public AudioSource somAbate;

    private bool podeEliminar = false;
    private bool eliminou = false;
    
    // Update is called once per frame
    void Update()
    {
        if(podeEliminar && Input.GetKeyDown(KeyCode.F)){
            personagem[persoSelecionado.numPerso].GetComponent<Animator>().SetBool("EliminouSilencio", true);
            StartCoroutine(inimigoEliminado());
        }
    }

    IEnumerator inimigoEliminado(){
        eliminou = true;
        podeEliminar = false;
        scriptAparecerTeclas.aparecer = false;
        scriptAparecerTeclas.texto = "";
        inimigo.GetComponent<InimigosMovimentoAuto>().enabled = false;
        inimigo.GetComponent<VisaoInimigos>().enabled = false;  

        yield return new WaitForSeconds(0.5f);

        personagem[persoSelecionado.numPerso].GetComponent<Animator>().SetBool("EliminouSilencio", false);
        inimigo.GetComponent<Animator>().SetBool("taMorrendo", true);
        inimigo.GetComponent<Animator>().SetInteger("transition", 2);
        somAbate.Play();

        yield return new WaitForSeconds(3.5f);

        Destroy(inimigo);
    }

    void OnTriggerStay(Collider other){
        if(other.gameObject.CompareTag("Player") && !eliminou){
            scriptAparecerTeclas.aparecer = true;
            scriptAparecerTeclas.texto = "Aperte \"F\" para eliminar o inimigo";
            podeEliminar = true;
        }
    }

    void OnTriggerExit(Collider other){
        if(other.gameObject.CompareTag("Player") && !eliminou){
            scriptAparecerTeclas.aparecer = false;
            scriptAparecerTeclas.texto = "";
            podeEliminar = false;
        }
    }
}
