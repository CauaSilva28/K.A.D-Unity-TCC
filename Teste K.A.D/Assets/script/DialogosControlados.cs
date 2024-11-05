using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogosControlados : MonoBehaviour
{
    [Header("Diálogo")]
    public string[] dialogo; 

    [Header("Interface de Diálogo")]
    public GameObject telaDialogo;
    public GameObject elementosDialogo;
    public Text dialogoTexto;
    public Text nomePersonagem;
    public Image imagemPersonagem; 
    public Text textoPassarTeclas;

   [Header("Controle de Diálogo")]
    public bool iniciarDialogo = false;
    public bool fimDoDialogo;
    public bool aparecerFalas; 
    private bool apareceu = false;

    public float segundosletras;
    public float segundosPassarFala;

    public string[] nomePersonagemStr;
    public Sprite[] imgPersonagens;

    public AudioSource[] audiosInicioDialogo;
    public Animator[] animacoesPerso;

    public GameObject audioTexto;

    [Header("Elementos adicionais")]
    public AparecerTeclasTexto scriptAparecerTeclas;
    public TelaSelecionarPerso selecaoPerso;

    public bool tirarAnimacoes;

    [Header("Elementos personagens jogaveis")]
    public Sprite[] imgPersoJogaveis;
    public AudioSource[] audioPersoJogaveis;
    public string[] nomePersoJogaveis;
    public Animator[] animacoesPersoJogaveis;

    void Start(){ 

    }

    void Update()
    {
        adicionandoElementosPersosJogaveis();

        if (aparecerFalas)
        {
            if(Input.GetKeyDown(KeyCode.F)){
                apareceu = true;
                scriptAparecerTeclas.aparecer = false;
                scriptAparecerTeclas.texto = "";
                telaDialogo.SetActive(true);
                telaDialogo.GetComponent<Animator>().SetInteger("transicao", 1);
                textoPassarTeclas.enabled = true;
                
                if (!iniciarDialogo)
                {
                    StartCoroutine(ExibirDialogo());
                    iniciarDialogo = true;       
                }
            }
        }
    }

    IEnumerator ExibirDialogo(){
        for(int i = 0 ; i < dialogo.Length ; i++){
            nomePersonagem.text = nomePersonagemStr[i];
            imagemPersonagem.sprite = imgPersonagens[i];

            yield return new WaitForSeconds(0.5f);

            elementosDialogo.GetComponent<Animator>().SetInteger("transicao", 1);

            yield return new WaitForSeconds(1f);

            audiosInicioDialogo[i].Play();

            if(!tirarAnimacoes){
                animacoesPerso[i].SetBool("falando", true);
            }

            dialogoTexto.text = ""; 
            foreach (char letter in dialogo[i])
            {
                audioTexto.SetActive(true);
                dialogoTexto.text += letter;

                if(Input.GetKeyDown(KeyCode.Space)){
                    dialogoTexto.text = dialogo[i];
                    audioTexto.SetActive(false);
                }
                else{
                    yield return new WaitForSeconds(segundosletras);
                }
            }

            yield return dialogoTexto.text == dialogo[i];

            audioTexto.SetActive(false);

            if(!tirarAnimacoes){
                animacoesPerso[i].SetBool("falando", false);
            }

            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space)); // Espera até que o jogador pressione a tecla Espaço

            elementosDialogo.GetComponent<Animator>().SetInteger("transicao", 2);

            yield return new WaitForSeconds(1f);

            dialogoTexto.text = ""; 

            yield return new WaitForSeconds(0.5f);
        }

        telaDialogo.GetComponent<Animator>().SetInteger("transicao", 2);
        aparecerFalas = false;
        fimDoDialogo = true;

        yield return new WaitForSeconds(1f);

        textoPassarTeclas.enabled = false;
        telaDialogo.SetActive(false);
        Destroy(gameObject);
    }

    private void adicionandoElementosPersosJogaveis(){
        for(int i = 0 ; i < dialogo.Length ; i++){
            
            if(nomePersonagemStr[i] == "Personagem"){
                nomePersonagemStr[i] = nomePersoJogaveis[selecaoPerso.numPerso];
            }

            if(imgPersonagens[i] == null){
                imgPersonagens[i] = imgPersoJogaveis[selecaoPerso.numPerso];
            }

            if(audiosInicioDialogo[i] == null){
                audiosInicioDialogo[i] = audioPersoJogaveis[selecaoPerso.numPerso];
            }

            if(!tirarAnimacoes){
                if(animacoesPerso[i] == null){
                    animacoesPerso[i] = animacoesPersoJogaveis[selecaoPerso.numPerso];
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(!apareceu){
                aparecerFalas = true; 
                scriptAparecerTeclas.aparecer = true;
                scriptAparecerTeclas.texto = "Aperte \"F\" para conversar com a garota";
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(!apareceu){
                aparecerFalas = false; 
                scriptAparecerTeclas.aparecer = false;
                scriptAparecerTeclas.texto = "";
            }
        }
    }
}
