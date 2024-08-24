using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelaSelecionarPerso : MonoBehaviour
{
    public GameObject[] PersonagensSelecao;
    public GameObject telaSelecao;
    public GameObject jogo;

    public AudioSource somSelect;
    public GameObject telaTransicao;
    
    public void SelecionarJeronimo()
    {
        somSelect.Play();
        StartCoroutine(SelecionarPersonagem());
    }

    IEnumerator SelecionarPersonagem(){
        telaTransicao.SetActive(true);
        telaTransicao.GetComponent<Animator>().SetInteger("transition", 2);

        yield return new WaitForSeconds(4f);

        PersonagensSelecao[0].SetActive(true);
        jogo.SetActive(true);
        telaSelecao.SetActive(false);

        telaTransicao.GetComponent<Animator>().SetInteger("transition", 1);

        yield return new WaitForSeconds(3f);

        telaTransicao.SetActive(false);
    }
}
