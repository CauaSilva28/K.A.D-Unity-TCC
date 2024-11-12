using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AreaPortao : MonoBehaviour
{
    public GameObject spawnInimigos;
    public Slider vidaObjeto;

    public GameObject telaTransicao;
    public GameObject portao;

    public GameObject[] player;

    public GameObject somDestruindo;
    public ParticleSystem particulaFumaca;

    public TelaSelecionarPerso selecaoPerso;
    // Update is called once per frame
    void Update()
    {
        if(vidaObjeto.value <= 0){
            somDestruindo.SetActive(true);
            particulaFumaca.Play();
            StartCoroutine(fimDeJogo());
        }
    }

    IEnumerator fimDeJogo(){
        spawnInimigos.GetComponent<SpawnarInimigos>().DesabilitarScriptsInimigos();
        player[selecaoPerso.numPerso].GetComponent<Movimento>().perdendo = true;
        PlayerPrefs.SetInt("Fase4Completa", 1);

        yield return new WaitForSeconds(1f);

        Destroy(portao);

        yield return new WaitForSeconds(1f);

        telaTransicao.SetActive(true);
        telaTransicao.GetComponent<Animator>().SetInteger("transition", 2);

        yield return new WaitForSeconds(8f);

        SceneManager.LoadScene("Fase5");
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Player")){
            spawnInimigos.SetActive(true);
        }
    }
}
