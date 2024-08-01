using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FuncoesBotoes : MonoBehaviour
{
    public AudioSource somSelectMenu;
    public GameObject telaTransicao;

    public void ReiniciarFase(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void IniciarJogo(){
        somSelectMenu.Play();
        StartCoroutine(inicioJogo());
    }

    IEnumerator inicioJogo(){
        telaTransicao.SetActive(true);
        telaTransicao.GetComponent<Animator>().SetInteger("transition", 2);

        yield return new WaitForSeconds(5f);

        SceneManager.LoadScene("Fase1");
    }
}
