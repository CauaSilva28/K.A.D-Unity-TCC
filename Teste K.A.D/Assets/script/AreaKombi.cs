using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Playables;

public class AreaKombi : MonoBehaviour
{
    public GameObject SeuZe;

    public ColetarItens coletarItens;

    private AudioSource audioKombi;
    public AudioClip somDanoKombi;

    public AparecerTeclasTexto scriptAparecerTeclas;

    private bool iniciarConserto = false;

    private Coroutine coroutine;

    private bool iniciouConserto = false;
    private bool finalizouConserto = false;
    private bool iniciarCutsceneFim = false;

    public GameObject itens;

    public Transform posicaoVelho;

    public GameObject areaAparecerObjetivo;

    public Slider barraVidaKombi;
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

    public Slider barraConserto;
    public GameObject cutsceneFim;
    public AparecerTextos textoObjetivo;
    public GameObject objetosCanvas;
    public SpawnarInimigos spawnInimigo;

    public ColetarItens scriptColeta;
    // Start is called before the first frame update
    void Start()
    {
        audioKombi = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 velhoConsertando = posicaoVelho.position;
        if (iniciarConserto)
        {
            areaAparecerObjetivo.SetActive(false);
            scriptAparecerTeclas.aparecer = false;
            itens.SetActive(true);
            coletarItens.enabled = true;
            SeuZe.GetComponent<MovimentoVelho>().enabled = false;
            SeuZe.GetComponent<NavMeshAgent>().speed = 0;
            SeuZe.GetComponent<Animator>().SetInteger("transition", 3);
            SeuZe.GetComponent<Transform>().transform.position = velhoConsertando;
            iniciarConserto = false;
            iniciouConserto = true;
        }

        if(barraVidaKombi.value <= 0){
            spawnInimigo.DesabilitarScriptsInimigos();
            StartCoroutine(KombiDestruida());
        }

        if(barraConserto.value >= 1){
            textoObjetivo.aparecerTexto = true;
            textoObjetivo.texto = "Corra para a Kombi!";
            finalizouConserto = true;
        }

        if(iniciarCutsceneFim){
            spawnInimigo.DesabilitarScriptsInimigos();
            objetosCanvas.SetActive(false);
            vidaKombi.SetActive(false);
            player.SetActive(false);
            velho.SetActive(false);
            gameObject.SetActive(false);
            cutsceneFim.SetActive(true);
            cutsceneFim.GetComponent<PlayableDirector>().Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(!iniciouConserto){
                scriptAparecerTeclas.aparecer = true;
                scriptAparecerTeclas.texto = "Aperte \"E\" para iniciar o conserto";
            }

            if(finalizouConserto){
                scriptAparecerTeclas.aparecer = true;
                scriptAparecerTeclas.texto = "Aperte \"E\" para entrar na Kombi";
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
            if (Input.GetKey(KeyCode.E))
            {
                if(!iniciouConserto){
                    iniciarConserto = true;
                }
            }

            if (Input.GetKey(KeyCode.E))
            {
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
        cameraKombi.SetActive(true);
        cameraPlayer.SetActive(false);
        scriptColeta.perdendo = true;
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

        yield return new WaitForSeconds(2f);

        cameraKombi.GetComponent<AudioListener>().enabled = false;
    }
}
