using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FuncoesBotoes : MonoBehaviour
{
    public AudioSource somSelectMenu;
    public GameObject telaTransicao;

    public GameObject telaControles;
    public GameObject telaMenu;
    public GameObject telaFases;

    public void ReiniciarFase(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void IniciarJogo(){
        somSelectMenu.Play();
        StartCoroutine(inicioJogo());
    }

    public void AbrirTelaControles()
    {
        somSelectMenu.Play();
        telaControles.SetActive(true);
        telaMenu.SetActive(false);
    }

    public void AbrirTelaFases()
    {
        somSelectMenu.Play();
        telaFases.SetActive(true);
        telaMenu.SetActive(false);
    }

    public void botaoVoltar()
    {
        telaMenu.SetActive(true);
        telaControles.SetActive(false);
        telaFases.SetActive(false);
    }

    public void SairDoJogo()
    {
        Application.Quit();
    }

    IEnumerator inicioJogo(){
        telaTransicao.SetActive(true);
        telaTransicao.GetComponent<Animator>().SetInteger("transition", 2);

        yield return new WaitForSeconds(5f);

        SceneManager.LoadScene("Fase1");
    }
}
