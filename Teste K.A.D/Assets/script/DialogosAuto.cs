using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogosAuto : MonoBehaviour
{
    [Header("Diálogo")]
    public string[] dialogo; 

    [Header("Interface de Diálogo")]
    public GameObject telaDialogo;
    public GameObject elementosDialogo;
    public Text dialogoTexto;
    public Text nomePersonagem;
    public Image imagemPersonagem; 

   [Header("Controle de Diálogo")]
    private bool iniciarDialogo = false;
    public bool aparecerFalas; 

    public float segundosletras;

    public string[] nomePersonagemStr;
    public Sprite[] imgPersonagens;

    void Start(){ 

    }

    void Update()
    {
        if (aparecerFalas)
        {
            telaDialogo.SetActive(true);
            telaDialogo.GetComponent<Animator>().SetInteger("transicao", 1);
            
            if (!iniciarDialogo)
            {
                StartCoroutine(ExibirDialogo());
                iniciarDialogo = true;       
            }
        }
    }

    IEnumerator ExibirDialogo(){
        for(int i = 0 ; i < dialogo.Length ; i++){
            nomePersonagem.text = nomePersonagemStr[i];
            imagemPersonagem.sprite = imgPersonagens[i];

            yield return new WaitForSeconds(1f);

            elementosDialogo.GetComponent<Animator>().SetInteger("transicao", 1);

            yield return new WaitForSeconds(1f);

            dialogoTexto.text = ""; 
            foreach (char letter in dialogo[i])
            {
                dialogoTexto.text += letter; 
                yield return new WaitForSeconds(segundosletras);
            }

            yield return dialogoTexto.text == dialogo[i];

            yield return new WaitForSeconds(2f);

            elementosDialogo.GetComponent<Animator>().SetInteger("transicao", 2);

            yield return new WaitForSeconds(0.6f);

            dialogoTexto.text = ""; 

            yield return new WaitForSeconds(0.5f);
        }

        telaDialogo.GetComponent<Animator>().SetInteger("transicao", 2);
        aparecerFalas = false;

        yield return new WaitForSeconds(1f);

        telaDialogo.SetActive(false);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            aparecerFalas = true; 
        }
    }
}
