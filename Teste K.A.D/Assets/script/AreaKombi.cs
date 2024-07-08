using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class AreaKombi : MonoBehaviour
{
    public Text textoCima;
    public Text textoCimaConcerto;
    public Transform posicaoVelho;
    public NavMeshAgent nav;
    public Animator anim;
    public MovimentoVelho movimentoVelho;

    public ColetarItens coletarItens;

    private AudioSource audioKombi;
    public AudioClip somDanoKombi;

    private bool iniciarConserto = false;
    // Start is called before the first frame update
    void Start()
    {
        audioKombi = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 velhoConsertando = new Vector3(110.9f, 36f, 97.9f);
        if (iniciarConserto)
        {
            textoCimaConcerto.enabled = true;
            coletarItens.enabled = true;
            textoCima.text = "";
            textoCima.enabled = false;
            movimentoVelho.enabled = false;
            nav.speed = 0;
            anim.SetInteger("transition", 3);
            posicaoVelho.transform.position = velhoConsertando;
        }
        else{
            textoCimaConcerto.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            textoCima.text = "Aperte \"E\" para iniciar o conserto";
            textoCima.enabled = true;
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
                iniciarConserto = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            textoCima.text = "Leve o Seu Zé para consertar a Kombi";
        }
    }
}
