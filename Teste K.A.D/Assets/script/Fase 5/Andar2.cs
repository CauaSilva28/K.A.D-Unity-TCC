using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Andar2 : MonoBehaviour
{
    public Slider[] barraVidaMiniBoss;
    public GameObject[] barreiras;

    public GameObject areaMensagemFim;

    public GameObject spawnarInimigo;

    private bool aconteceu;

    // Update is called once per frame
    void Update()
    {
        if(barraVidaMiniBoss[0].value <= 0 && barraVidaMiniBoss[1].value <= 0){
            areaMensagemFim.SetActive(true);
            barreiras[1].SetActive(false);
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Player")){
            if(!aconteceu){
                barreiras[0].SetActive(true);
                spawnarInimigo.GetComponent<SpawnarInimigos>().DestruirTodosInimigos();
                aconteceu = true;
            }
        }
    }
}
