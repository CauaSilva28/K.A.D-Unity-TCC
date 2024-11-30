using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InicioAndar1 : MonoBehaviour
{
    public GameObject portao;
    public GameObject[] barreiras;
    public GameObject spawnarInimigo;
    public AudioSource somPortaoFechando;

    public GameObject areaMensagemFim;
    public GameObject inimigosForaBase;

    private bool aconteceu;

    void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Player")){
            if(!aconteceu){
                StartCoroutine(decorrerAndar());
                aconteceu = true;
            }
        }
    }

    IEnumerator decorrerAndar(){
        inimigosForaBase.SetActive(false);
        barreiras[0].SetActive(true);
        spawnarInimigo.SetActive(true);
        portao.GetComponent<Animator>().SetInteger("transicao", 2);
        somPortaoFechando.Stop();

        yield return new WaitForSeconds(0.2f);

        somPortaoFechando.Play();

        yield return new WaitForSeconds(86f);

        barreiras[1].SetActive(false);
        areaMensagemFim.SetActive(true);
        spawnarInimigo.GetComponent<SpawnarInimigos>().perdendo = true;
        spawnarInimigo.SetActive(false);

        yield return new WaitForSeconds(2f);

        Destroy(gameObject);
    }
}
