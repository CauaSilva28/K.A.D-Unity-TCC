using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransicoesCena : MonoBehaviour
{
    public GameObject telaTransicao;

    private bool aconteceu = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!aconteceu){
            StartCoroutine(tempoTransicaoDaTela());
            aconteceu = true;
        }
    }

    IEnumerator tempoTransicaoDaTela(){
        telaTransicao.GetComponent<Animator>().SetInteger("transition", 1);

        yield return new WaitForSeconds(3f);

        telaTransicao.SetActive(false);
    }
}
