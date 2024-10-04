using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudiosPassos : MonoBehaviour
{
    public GameObject somConcreto;
    public GameObject somGrama;

    private bool andandoGrama = false;
    private bool andandoConcreto = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<Movimento>().moveDirection.x != 0 && GetComponent<Movimento>().moveDirection.z != 0 && !GetComponent<Movimento>().pauseJogo.pausado && !GetComponent<Movimento>().isDashing && !GetComponent<Movimento>().perdendo){
            if(GetComponent<Movimento>().velocidade == GetComponent<Movimento>().velocidadeAndando){
                somConcreto.GetComponent<AudioSource>().pitch = 0.7f;
                somGrama.GetComponent<AudioSource>().pitch = 0.8f;
            }
            else{
                somConcreto.GetComponent<AudioSource>().pitch = 1.05f;
                somGrama.GetComponent<AudioSource>().pitch = 1.1f;
            }

            if(andandoConcreto){
                somConcreto.SetActive(true);
                somGrama.SetActive(false);
            }
            else if(andandoGrama){
                somGrama.SetActive(true);
                somConcreto.SetActive(false);
            }
        }
        else{
            CancelarAudios();
        }
    }

    void OnTriggerStay(Collider other){
        if(other.gameObject.CompareTag("concreto")){
            andandoConcreto = true;
            andandoGrama = false;
        }
        else if(other.gameObject.CompareTag("grama")){
            andandoGrama = true;
            andandoConcreto = false;
        }
    }

    void OnTriggerExit(Collider other){
        if(other.gameObject.CompareTag("concreto")){
            andandoConcreto = false;
        }

        if(other.gameObject.CompareTag("grama")){
            andandoGrama = false;
        }
    }

    void CancelarAudios(){
        somGrama.SetActive(false);
        somConcreto.SetActive(false);
    }
}
