using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndarFinal : MonoBehaviour
{
    public GameObject barreira;
    public GameObject telaTransicao;

    public GameObject[] player;
    public GameObject[] cameraPlayer;
    public Transform posicaoPlayer;

    public GameObject cutsceneBoss;

    public TelaSelecionarPerso selecaoPerso;

    private bool aconteceu;


    void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Player")){
            if(!aconteceu){
                StartCoroutine(iniciarCutsceneBoss());
                aconteceu = true;
            }
        }
    }

    IEnumerator iniciarCutsceneBoss(){
        barreira.SetActive(true);
        telaTransicao.SetActive(true);
        telaTransicao.GetComponent<Animator>().SetInteger("transition", 2);

        yield return new WaitForSeconds(5f);

        player[selecaoPerso.numPerso].GetComponent<Rigidbody>().isKinematic = true;
        player[selecaoPerso.numPerso].GetComponent<Transform>().transform.position = posicaoPlayer.position;
        player[selecaoPerso.numPerso].GetComponent<Transform>().transform.rotation = posicaoPlayer.rotation;
        player[selecaoPerso.numPerso].GetComponent<Movimento>().perdendo = true;
        player[selecaoPerso.numPerso].GetComponent<AnimacoesPerso>().morrendo = true;
        player[selecaoPerso.numPerso].GetComponent<Animator>().SetInteger("transition", 0);
        cameraPlayer[selecaoPerso.numPerso].SetActive(false);
        cutsceneBoss.SetActive(true);
    }

    public void RetornarPlayer(){
        player[selecaoPerso.numPerso].GetComponent<Rigidbody>().isKinematic = false;
        player[selecaoPerso.numPerso].GetComponent<Movimento>().perdendo = false;
        player[selecaoPerso.numPerso].GetComponent<AnimacoesPerso>().morrendo = false;
        cameraPlayer[selecaoPerso.numPerso].SetActive(true);
    }
}
