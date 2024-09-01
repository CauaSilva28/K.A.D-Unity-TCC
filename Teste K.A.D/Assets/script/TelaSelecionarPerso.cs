using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TelaSelecionarPerso : MonoBehaviour
{
    public GameObject[] PersonagensSelecao;
    public Sprite[] PersoImg;
    public string[] PersoNome;

    public Text[] localNomePerso;
    public Image localImgPerso;

    public GameObject telaSelecao;
    public GameObject jogo;

    public AudioSource somSelect;
    public GameObject telaTransicao;

    private int numPerso;
    
    public void SelecionarJeronimo()
    {
        numPerso = 0;
        somSelect.Play();
        StartCoroutine(SelecionarPersonagem());
    }

    public void SelecionarSeuZe()
    {
        numPerso = 1;
        somSelect.Play();
        StartCoroutine(SelecionarPersonagem());
    }

    IEnumerator SelecionarPersonagem(){
        telaTransicao.SetActive(true);
        telaTransicao.GetComponent<Animator>().SetInteger("transition", 2);

        yield return new WaitForSeconds(4f);

        for(int i = 0;i<localNomePerso.Length;i++){
            localNomePerso[i].text = PersoNome[numPerso];
        }

        PersonagensSelecao[numPerso].SetActive(true);
        localImgPerso.sprite = PersoImg[numPerso];
        
        jogo.SetActive(true);
        telaSelecao.SetActive(false);

        telaTransicao.GetComponent<Animator>().SetInteger("transition", 1);

        yield return new WaitForSeconds(3f);

        telaTransicao.SetActive(false);
    }
}
