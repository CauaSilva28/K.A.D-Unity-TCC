using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaFrenteBase : MonoBehaviour
{
    public GameObject portaoBase;
    public GameObject fumacaPortao;
    public AudioSource somPortaoAbrindo;

    void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Player")){
            fumacaPortao.SetActive(true);
            portaoBase.GetComponent<Animator>().SetInteger("transicao", 1);
            somPortaoAbrindo.Play();
            Destroy(gameObject);
        }
    }
}
