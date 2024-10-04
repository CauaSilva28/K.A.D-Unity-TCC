using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Playables;

public class AreaKombi : MonoBehaviour
{
    [Header("Objetos")]
    public GameObject SeuZe;
    public GameObject vidaKombi;
    public GameObject player;
    public GameObject velho;
    public GameObject telaTransicao;
    public GameObject somGameOver;
    public GameObject telaGameOver;
    public GameObject cameraKombi;
    public GameObject cameraPlayer;
    public GameObject fogoKombi;
    public GameObject explosaoKombi;
    public GameObject somExplosao;
    public GameObject objetosCanvas;
    public GameObject cutsceneFim;
    public GameObject areaAparecerObjetivo;
    public GameObject itens;
    public GameObject areaFalaSeuZe;
    public GameObject areaFalaSeuZeFim;
    public Transform posicaoVelho;

    [Header("Scripts")]
    public AparecerTeclasTexto scriptAparecerTeclas;
    public AparecerTextos textoObjetivo;
    public SpawnarInimigos spawnInimigo;
    public PausarJogo pauseJogo;

    [Header("Booleans")]
    private bool iniciouConserto = false;
    private bool finalizouConserto = false;
    private bool iniciarCutsceneFim = false;
    private bool iniciarConserto = false;
    private bool apareceuFalaFim = false;

    [Header("Canvas")]
    public Slider barraVidaKombi;
    public Slider barraConserto;
    
    [Header("Audios")]
    private AudioSource audioKombi;
    public AudioClip somDanoKombi;
    
    // Start is called before the first frame update
    void Start()
    {
        audioKombi = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (iniciarConserto)
        {
            ConsertoKombi();
        }

        if(barraVidaKombi.value <= 0){
            spawnInimigo.DesabilitarScriptsInimigos();
            StartCoroutine(KombiDestruida());
        }

        if(barraConserto.value >= 1){
            if(!apareceuFalaFim){
                areaFalaSeuZeFim.SetActive(true);
                apareceuFalaFim = true;
            }
            textoObjetivo.aparecerTexto = true;
            textoObjetivo.texto = "Corra para a Kombi!";

            finalizouConserto = true;
        }

        if(iniciarCutsceneFim){
            FimDeJogo();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(!iniciouConserto){
                scriptAparecerTeclas.aparecer = true;
                scriptAparecerTeclas.texto = "Aperte \"F\" para iniciar o conserto";
            }

            if(finalizouConserto){
                scriptAparecerTeclas.aparecer = true;
                scriptAparecerTeclas.texto = "Aperte \"F\" para entrar na Kombi";
            }
        }

        if (other.gameObject.CompareTag("inimigo"))
        {
            audioKombi.PlayOneShot(somDanoKombi);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.F))
            {
                if(!iniciouConserto){
                    iniciarConserto = true;
                }

                if(finalizouConserto){
                    iniciarCutsceneFim = true;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(!iniciouConserto || finalizouConserto){
                scriptAparecerTeclas.aparecer = false;
            }
        }
    }

    IEnumerator KombiDestruida(){
        pauseJogo.perdendo = true;
        cameraKombi.SetActive(true);
        cameraPlayer.SetActive(false);
        player.GetComponent<ColetarItens>().perdendo = true;
        velho.SetActive(false);
        player.GetComponent<Movimento>().perdendo = true;
        player.GetComponent<Atacar>().enabled = false;
        player.GetComponent<AnimacoesPerso>().enabled = false;
        player.GetComponent<CurarVida>().enabled = false;
        player.GetComponent<Rigidbody>().isKinematic = true;
        player.GetComponent<Animator>().SetInteger("transition", 0);

        GetComponent<Collider>().enabled = true;

        yield return new WaitForSeconds(3f);

        fogoKombi.SetActive(true);
        somExplosao.SetActive(true);
        explosaoKombi.SetActive(true);

        yield return new WaitForSeconds(5f);

        telaTransicao.SetActive(true);
        telaTransicao.GetComponent<Animator>().SetInteger("transition", 2);

        yield return new WaitForSeconds(5f);

        somGameOver.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        telaGameOver.SetActive(true);
        telaGameOver.GetComponent<Animator>().SetBool("surgir", true);

        yield return new WaitForSeconds(1f);

        AudioListener.volume = 0;
    }

    private void FimDeJogo(){
        player.GetComponent<Movimento>().perdendo = true;
        pauseJogo.perdendo = true;
        spawnInimigo.DesabilitarScriptsInimigos();
        objetosCanvas.SetActive(false);
        vidaKombi.SetActive(false);
        player.SetActive(false);
        velho.SetActive(false);
        gameObject.SetActive(false);
        cutsceneFim.SetActive(true);
        cutsceneFim.GetComponent<PlayableDirector>().Play();

        PlayerPrefs.SetInt("Fase1Completa", 1); //Comando responsavel por realizar o salvamento da fase 1 (sendo 1 o valor que mostra q ela foi completa)
    }

    private void ConsertoKombi(){
        Vector3 velhoConsertando = posicaoVelho.position;

        areaFalaSeuZe.SetActive(true);
        areaAparecerObjetivo.SetActive(false);
        scriptAparecerTeclas.aparecer = false;
        itens.SetActive(true);
        player.GetComponent<ColetarItens>().enabled = true;
        SeuZe.GetComponent<MovimentoVelho>().enabled = false;
        SeuZe.GetComponent<NavMeshAgent>().speed = 0;
        SeuZe.GetComponent<Animator>().SetInteger("transition", 2);
        SeuZe.GetComponent<Transform>().transform.position = velhoConsertando;
        SeuZe.GetComponent<Transform>().transform.rotation = posicaoVelho.rotation;
        iniciarConserto = false;
        iniciouConserto = true;
    }
}
