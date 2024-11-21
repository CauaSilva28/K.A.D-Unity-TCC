using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndarFinal : MonoBehaviour
{
    public GameObject barreira;
    public GameObject telaTransicao;

    public GameObject personagens;

    public GameObject[] playerCutscene;

    public GameObject cutsceneBoss;

    public TelaSelecionarPerso selecaoPerso;

    private bool aconteceu;

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Player")){
            if(!aconteceu){
                StartCoroutine(iniciarCutsceneBoss());
                aconteceu = true;
            }
        }
    }

    IEnumerator iniciarCutsceneBoss(){
        barreira.SetActive(true);
        telaTransicao.SetActive(true);
        telaTransicao.GetComponent<Animator>().SetInteger("transition", 2);

        yield return new WaitForSeconds(5f);

        personagens.SetActive(false);
        playerCutscene[selecaoPerso.numPerso].SetActive(true);
        cutsceneBoss.SetActive(true);
    }
}
