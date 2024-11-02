using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public class PerderVidaKombi : MonoBehaviour
{
    public PostProcessVolume postProcessVolume;
    private ChromaticAberration chromaticAberration;
    private ColorGrading colorGrading;

    public GameObject telaTransicao;
    public GameObject telaGameOver;
    public GameObject somGameOver;

    public GameObject explosaoKombi;
    public GameObject somExplosaoKombi;
    public GameObject fogoKombi;
    public GameObject somVeiculo;

    public GameObject camera;

    public GameObject inimigo;

    public Slider vidaKombi;
    private bool explodiu = false;

    public PausarJogo pauseJogo;

    void Start(){
        if (postProcessVolume.profile.TryGetSettings(out chromaticAberration) && postProcessVolume.profile.TryGetSettings(out colorGrading))
        {
            chromaticAberration.intensity.value = 0.1f;
            colorGrading.mixerRedOutRedIn.value = 100f;
        }
    }

    void Update(){
        if(vidaKombi.value <= 0.2f){
            chromaticAberration.intensity.value = 1f;
            colorGrading.mixerRedOutRedIn.value = 200f;
        }
        else{
            chromaticAberration.intensity.value = 0.1f;
            colorGrading.mixerRedOutRedIn.value = 100f;
        }

        if(vidaKombi.value <= 0){
            StartCoroutine(Perdendo());
        }
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("explosivo")){
            other.gameObject.GetComponent<ParticleSystem>().Play();
            StartCoroutine(explodir(other.gameObject));
        }
    }

    void OnCollisionEnter(Collision collider){
        if(collider.gameObject.CompareTag("obstaculos") || collider.gameObject.CompareTag("frenteVeiculoInimigo")){
            vidaKombi.value -= 0.1f;
        }
    }

    IEnumerator explodir(GameObject explosivo){
        if(!explodiu){
            vidaKombi.value -= 0.4f;
            explodiu = true;
        }

        yield return new WaitForSeconds(1f);

        explodiu = false;
        Destroy(explosivo);
    }

    IEnumerator Perdendo(){
        somVeiculo.SetActive(false);
        GetComponent<MovimentoKombi>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        gameObject.tag = "Untagged";
        GetComponent<MovimentoKombi>().turbo.SetActive(false);
        pauseJogo.perdendo = true;

        yield return new WaitForSeconds(2f);

        explosaoKombi.SetActive(true);
        somExplosaoKombi.SetActive(true);

        yield return new WaitForSeconds(1f);

        fogoKombi.SetActive(true);

        yield return new WaitForSeconds(3f);

        telaTransicao.SetActive(true);
        telaTransicao.GetComponent<Animator>().SetInteger("transition", 2);
        inimigo.GetComponent<VeiculoInimigo>().navMeshAgent.speed = 0;

        yield return new WaitForSeconds(5f);

        somGameOver.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        telaGameOver.SetActive(true);
        telaGameOver.GetComponent<Animator>().SetBool("surgir", true);

        yield return new WaitForSeconds(1f);

        AudioListener.volume = 0;
    }
}
