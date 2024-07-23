using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FimTelaTransicao : MonoBehaviour
{
    public GameObject telaTransicao;
    public GameObject cameraKombi;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(fimFase());
    }

    IEnumerator fimFase(){
        telaTransicao.SetActive(true);
        telaTransicao.GetComponent<Animator>().SetInteger("transition", 2);

        yield return new WaitForSeconds(2f);

        cameraKombi.GetComponent<AudioListener>().enabled = false;

        yield return new WaitForSeconds(6f);

        SceneManager.LoadScene("Fase2");
    }
}
