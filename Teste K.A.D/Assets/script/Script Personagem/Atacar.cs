using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atacar : MonoBehaviour
{
    public float danoPlayer;
    public bool atacando = false;
    public float anguloDeVisao;

    public AudioSource sonsPerso;
    public AnimacoesPerso animPerso;
    public Movimento movePerso;

    private List<GameObject> inimigos = new List<GameObject>();

    void Start()
    {
        
    }

    void Update()
    {
        if (atacando)
        {
            GameObject inimigoAlvo = ProcurarInimigoAlvo();
            if (inimigoAlvo != null)
            {
                Inimigos scriptInimigo = inimigoAlvo.GetComponent<Inimigos>();

                if (movePerso.isDashing && Input.GetKey(KeyCode.W))
                {
                    scriptInimigo.Recuar();
                }
            }
        }

        LimparInimigosMortos();
    }

    // Um evento foi criado na animação do ataque (simbolo de bandeira), e e nele foi adicionado esse método
    void Ataque1(){
        if(atacando){
            GameObject inimigoAlvo = ProcurarInimigoAlvo();
            if (inimigoAlvo != null)
            {
                Inimigos scriptInimigo = inimigoAlvo.GetComponent<Inimigos>();

                sonsPerso.Play();
                scriptInimigo.vidaInimigo -= danoPlayer;
            }
        }
    }

    void Ataque2(){
        if(atacando){
            GameObject inimigoAlvo = ProcurarInimigoAlvo();
            if (inimigoAlvo != null)
            {
                Inimigos scriptInimigo = inimigoAlvo.GetComponent<Inimigos>();
                
                sonsPerso.Play();
                scriptInimigo.vidaInimigo -= danoPlayer * 2;
                scriptInimigo.Recuar();
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("inimigo"))
        {
            inimigos.Add(collision.gameObject);
            atacando = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("inimigo"))
        {
            inimigos.Remove(collision.gameObject);
            if (inimigos.Count == 0)
            {
                atacando = false;
            }
        }
    }

    GameObject ProcurarInimigoAlvo()
    {
        foreach (GameObject inimigo in inimigos)
        {
            if (EstaOlhandoParaInimigo(inimigo))
            {
                return inimigo;
            }
        }
        return null;
    }

    void LimparInimigosMortos()
    {
        inimigos.RemoveAll(inimigo => inimigo == null || inimigo.GetComponent<Inimigos>() == null || inimigo.GetComponent<Inimigos>().vidaInimigo <= 0);
        if (inimigos.Count == 0)
        {
            atacando = false;
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
