using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class TransicoesCena : MonoBehaviour
{
    public GameObject telaTransicao;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(tempoTransicaoDaTela());
        AudioListener.volume = 1;
    }

    IEnumerator tempoTransicaoDaTela(){
        telaTransicao.GetComponent<Animator>().SetInteger("transition", 1);

        yield return new WaitForSeconds(1.5f);

        telaTransicao.SetActive(false);
    }
}
