using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atacar : MonoBehaviour
{
    private bool atacando = false;
    private GameObject inimigo;
    public float anguloDeVisao = 45f;

    public AudioClip somSoco;
    private AudioSource sonsPerso;

    void Start(){
        sonsPerso = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (atacando)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (EstaOlhandoParaInimigo(inimigo))
                {
                    StartCoroutine(tempoAtaque());
                }
            }
        }
    }

    IEnumerator tempoAtaque(){
        sonsPerso.PlayOneShot(somSoco);
        Dino scriptDino = inimigo.GetComponent<Dino>(); // Obtém o script do inimigo que está sendo atacado
        scriptDino.vidaDino--;
        atacando = false;
        yield return new WaitForSeconds(1f);
        atacando = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("inimigo"))
        {
            atacando = true;
            inimigo = collision.gameObject;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("inimigo"))
        {
            atacando = false;
            inimigo = null;
        }
    }

    bool EstaOlhandoParaInimigo(GameObject inimigo)
    {
        if (inimigo == null)
            return false;

        Vector3 direcaoParaInimigo = (inimigo.transform.position - transform.position).normalized;
        float angulo = Vector3.Angle(transform.forward, direcaoParaInimigo);

        return angulo < anguloDeVisao;
    }
}
