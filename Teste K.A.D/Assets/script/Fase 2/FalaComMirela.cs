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

    public DialogosControlados dialogoMirela;

    private bool conversar = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (conversar)
        {
            if (inimigosMercado.Count == 0)
            {
                scriptAparecerTeclas.aparecer = true;
                scriptAparecerTeclas.texto = "Aperte \"F\" para falar com a garota";
                if (Input.GetKeyDown(KeyCode.F))
                {
                    cameraConversa.SetActive(true);
                    PlayerConversa[selecaoPerso.numPerso].SetActive(true);
                    Player[selecaoPerso.numPerso].SetActive(false);
                    scriptAparecerTeclas.aparecer = false;
                    scriptAparecerTeclas.texto = "";
                    dialogoMirela.iniciarDialogo = true;
                }
            }
            else
            {
                scriptAparecerTeclas.aparecer = true;
                scriptAparecerTeclas.texto = "Derrote todos os inimigos!";
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player")){
            conversar = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player")){
            conversar = false;
            scriptAparecerTeclas.aparecer = false;
            scriptAparecerTeclas.texto = "";
        }
    }
}
