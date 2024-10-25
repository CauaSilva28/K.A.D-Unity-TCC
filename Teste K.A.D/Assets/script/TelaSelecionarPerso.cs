using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TelaSelecionarPerso : MonoBehaviour
{
    public GameObject[] PersonagensSelecao;
    public Sprite[] PersoImg;
    public Sprite[] FerramentaPersoImg;
    public string[] PersoNome;

    public Text[] localNomePerso;
    public Image localImgPerso;
    public Image localImgFerramenta;

    public GameObject naoJogo;
    public GameObject jogo;

    public AudioSource somSelect;
    public GameObject telaTransicao;

    public int numPerso;

    void Start(){
        Cursor.lockState = CursorLockMode.None;
    }
    
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

    public void SelecionarMirela()
    {
        numPerso = 2;
        somSelect.Play();
        StartCoroutine(SelecionarPersonagem());
    }

    public void SelecionarEnzo()
    {
        numPerso = 3;
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
        localImgFerramenta.sprite = FerramentaPersoImg[numPerso];
        
        jogo.SetActive(true);
        naoJogo.SetActive(false);
    }
}
