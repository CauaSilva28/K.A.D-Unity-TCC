using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class AreaKombi : MonoBehaviour
{
    public GameObject SeuZe;

    public ColetarItens coletarItens;

    private AudioSource audioKombi;
    public AudioClip somDanoKombi;

    public AparecerTeclasTexto scriptAparecerTeclas;

    private bool iniciarConserto = false;

    private Coroutine coroutine;

    private bool iniciouConserto = false;

    public GameObject itens;

    public Transform posicaoVelho;
    // Start is called before the first frame update
    void Start()
    {
        audioKombi = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 velhoConsertando = posicaoVelho.position;
        if (iniciarConserto)
        {
            scriptAparecerTeclas.aparecer = false;
            itens.SetActive(true);
            coletarItens.enabled = true;
            SeuZe.GetComponent<MovimentoVelho>().enabled = false;
            SeuZe.GetComponent<NavMeshAgent>().speed = 0;
            SeuZe.GetComponent<Animator>().SetInteger("transition", 3);
            SeuZe.GetComponent<Transform>().transform.position = velhoConsertando;
            iniciarConserto = false;
            iniciouConserto = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(!iniciouConserto){
                scriptAparecerTeclas.aparecer = true;
                scriptAparecerTeclas.texto = "Aperte \"E\" para iniciar o conserto";
            }
        }

        if (other.gameObject.CompareTag("inimigo"))
        {
            audioKombi.PlayOneShot(somDanoKombi);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.E))
            {
                if(!iniciouConserto){
                    iniciarConserto = true;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(!iniciouConserto){
                scriptAparecerTeclas.aparecer = false;
            }
        }
    }
}
