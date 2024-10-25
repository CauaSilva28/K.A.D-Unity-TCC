using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalaComMirela : MonoBehaviour
{
    public List<GameObject> inimigosMercado;

    public AparecerTeclasTexto scriptAparecerTeclas;

    public GameObject cameraConversa;

    public GameObject Mirela;
    public GameObject[] Player;
    public GameObject[] PlayerConversa;

    public TelaSelecionarPerso selecaoPerso;

    public GameObject dialogoMirela;

    private bool conversar = false;

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
                scriptAparecerTeclas.aparecer = true;
                scriptAparecerTeclas.texto = "Derrote todos os inimigos!";
                dialogoMirela.SetActive(false);
            }
        }
        else{
            scriptAparecerTeclas.aparecer = false;
            scriptAparecerTeclas.texto = "";
        }
    }
}
