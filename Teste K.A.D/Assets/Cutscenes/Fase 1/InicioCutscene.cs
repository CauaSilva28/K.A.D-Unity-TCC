using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InicioCutscene : MonoBehaviour
{
    public GameObject[] player;
    public GameObject ObjDesativado;
    public AudioSource musicaFundo;

    public TelaSelecionarPerso telaSelecionarPerso;

    private bool ativou = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(ObjDesativado.activeSelf){
            if(!ativou){
                ativou = true;
                player[telaSelecionarPerso.numPerso].GetComponent<Movimento>().enabled = true;
                player[telaSelecionarPerso.numPerso].GetComponent<AnimacoesPerso>().enabled = true;
                musicaFundo.Play();
            }
        }
    }
}
