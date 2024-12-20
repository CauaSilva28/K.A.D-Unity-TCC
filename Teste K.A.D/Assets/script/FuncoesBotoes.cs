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
    public GameObject telaCreditos;

    private string textoFase;

    public void ReiniciarFase(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void IniciarHistoria(){
        textoFase = "Historia";
        somSelectMenu.Play();
        StartCoroutine(inicioJogo());
    }
    public void IniciarJogo(){
        textoFase = "Fase1";
        somSelectMenu.Play();
        StartCoroutine(inicioJogo());
    }
    public void IniciarFase2(){
        textoFase = "Fase2";
        somSelectMenu.Play();
        StartCoroutine(inicioJogo());
    }
    public void IniciarFase3(){
        textoFase = "Fase3";
        somSelectMenu.Play();
        StartCoroutine(inicioJogo());
    }
    public void IniciarFase4(){
        textoFase = "Fase4";
        somSelectMenu.Play();
        StartCoroutine(inicioJogo());
    }
    public void IniciarFase5(){
        textoFase = "Fase5";
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

    public void AbrirCreditos(){
        somSelectMenu.Play();
        telaCreditos.SetActive(true);
        telaMenu.SetActive(false);
    }

    public void botaoVoltar()
    {
        telaMenu.SetActive(true);
        telaControles.SetActive(false);
        telaFases.SetActive(false);
        telaCreditos.SetActive(false);
    }

    public void SairDoJogo()
    {
        Application.Quit();
    }

    public void IrCreditosFim(){
        SceneManager.LoadScene("CreditosFim");
    }

    public void voltarParaMenu(){
        SceneManager.LoadScene("Menu");
    }

    IEnumerator inicioJogo(){
        telaTransicao.SetActive(true);
        telaTransicao.GetComponent<Animator>().SetInteger("transition", 2);

        yield return new WaitForSeconds(5f);

        SceneManager.LoadScene(textoFase);
    }
}
