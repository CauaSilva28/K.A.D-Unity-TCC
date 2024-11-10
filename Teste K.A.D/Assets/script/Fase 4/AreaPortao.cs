using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AreaPortao : MonoBehaviour
{
    public GameObject spawnInimigos;
    public Slider vidaObjeto;

    // Update is called once per frame
    void Update()
    {
        if(vidaObjeto.value <= 0){
            fimDeJogo();
        }
    }

    void fimDeJogo(){

    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Player")){
            spawnInimigos.SetActive(true);
        }
    }
}
