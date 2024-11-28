using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PassarHistoria : MonoBehaviour
{
    public GameObject textoHistoria;
    public GameObject telaTransicao;

    public Text frases;
    public Image imgFase;

    public string[] textosFrases;
    public Sprite[] spritImgHistoria;

    private bool passou = false;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (!passou)
        {
            StartCoroutine(Passar());
            passou = true;
        } 
    }

    IEnumerator Passar()
    {
        yield return new WaitForSeconds(2f);
        
        for (int i = 0; i < textosFrases.Length; i++)
        {
            frases.text = textosFrases[i];
            imgFase.sprite = spritImgHistoria[i];

            telaTransicao.SetActive(true);
            telaTransicao.GetComponent<Animator>().SetInteger("transition", 1);

            yield return new WaitForSeconds(1f);

            textoHistoria.GetComponent<Animator>().SetInteger("transition", 1);

            yield return new WaitForSeconds(2f);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.RightArrow));

            textoHistoria.GetComponent<Animator>().SetInteger("transition", 2);
            telaTransicao.GetComponent<Animator>().SetInteger("transition", 2);

            yield return new WaitForSeconds(2f);
        }

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene("Fase1");
    }
}
