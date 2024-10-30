using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalaComMirela : MonoBehaviour
{
    public List<GameObject> inimigosMercado;

    public AparecerTeclasTexto scriptAparecerTeclas;

    public GameObject cameraConversa;
    public GameObject areasFinal;

    public GameObject Mirela;
    public GameObject[] Player;
    public GameObject[] PlayerConversa;
    public GameObject MirelaConversa;

    public TelaSelecionarPerso selecaoPerso;

    public GameObject dialogoMirela;

    private bool conversar = false;

    private bool saiuArea = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(dialogoMirela.GetComponent<DialogosControlados>().aparecerFalas){
            if (inimigosMercado.Count == 0)
            {
                MirelaConversa.GetComponent<Animator>().SetBool("atacando", false);
                dialogoMirela.SetActive(true);

                if(dialogoMirela.GetComponent<DialogosControlados>().iniciarDialogo){
                    cameraConversa.SetActive(true);
                    PlayerConversa[selecaoPerso.numPerso].SetActive(true);
                    Player[selecaoPerso.numPerso].SetActive(false);
                    scriptAparecerTeclas.aparecer = false;
                    scriptAparecerTeclas.texto = "";
                }
            }
            else
            {
                if(!saiuArea){
                    scriptAparecerTeclas.aparecer = true;
                    scriptAparecerTeclas.texto = "Derrote todos os inimigos!";
                    dialogoMirela.SetActive(false);
                }
            }
        }

        if(dialogoMirela.GetComponent<DialogosControlados>().fimDoDialogo){
            fimDialogoMirela();     
        }
    }

    void fimDialogoMirela(){
        cameraConversa.SetActive(false);
        PlayerConversa[selecaoPerso.numPerso].SetActive(false);
        Player[selecaoPerso.numPerso].SetActive(true);
        MirelaConversa.SetActive(false);
        Mirela.SetActive(true);
        areasFinal.SetActive(true);
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Player")){
            saiuArea = false;
        }
    }

    void OnTriggerExit(Collider other){
        if(other.gameObject.CompareTag("Player")){
            scriptAparecerTeclas.aparecer = false;
            scriptAparecerTeclas.texto = "";
            saiuArea = true;
        }
    }
}
