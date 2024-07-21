using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public class MortePerso : MonoBehaviour
{
    public PostProcessVolume postProcessVolume;
    private ChromaticAberration chromaticAberration;
    private ColorGrading colorGrading;
    
    public Slider vidaPerso;

    public GameObject gritoMorte;
    public GameObject somGameOver;

    private AnimacoesPerso animPerso;

    public GameObject telaTransicao;
    public GameObject telaGameOver;

    public GameObject spawnInimigos;

    public GameObject camera;

    public GameObject SomLevanoDano;
    // Start is called before the first frame update
    void Start()
    {
        animPerso = GetComponent<AnimacoesPerso>();

        if (postProcessVolume.profile.TryGetSettings(out chromaticAberration) && postProcessVolume.profile.TryGetSettings(out colorGrading))
        {
            chromaticAberration.intensity.value = 0.1f;
            colorGrading.mixerRedOutRedIn.value = 100f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(vidaPerso.value <= 0.3f){
            chromaticAberration.intensity.value = 1f;
            colorGrading.mixerRedOutRedIn.value = 200f;
        }
        else{
            chromaticAberration.intensity.value = 0.1f;
            colorGrading.mixerRedOutRedIn.value = 100f;
        }

        if(vidaPerso.value <= 0){
            gritoMorte.SetActive(true);
            animPerso.morrendo = true;
            animPerso.anim.SetInteger("transition", 7);
            SomLevanoDano.SetActive(false);
            StartCoroutine(Morrendo());
        }
    }

    IEnumerator Morrendo(){
        GetComponent<Movimento>().enabled = true;
        GetComponent<Movimento>().morrendo = true;
        GetComponent<Atacar>().enabled = true;
        GetComponent<AnimacoesPerso>().enabled = true;
        GetComponent<CurarVida>().enabled = true;

        yield return new WaitForSeconds(4f);

        telaTransicao.SetActive(true);
        telaTransicao.GetComponent<Animator>().SetInteger("transition", 2);

        yield return new WaitForSeconds(5f);

        spawnInimigos.GetComponent<SpawnarInimigos>().enabled = false;
        somGameOver.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        telaGameOver.SetActive(true);
        telaGameOver.GetComponent<Animator>().SetBool("surgir", true);

        yield return new WaitForSeconds(5f);

        camera.GetComponent<AudioListener>().enabled = false;
    }
}