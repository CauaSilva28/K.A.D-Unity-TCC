using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Atacar : MonoBehaviour
{
    public float danoSoco;
    public bool atacando = false;
    public float anguloDeVisao;

    public AudioSource sonsPerso;
    public AudioSource somSoco;
    public Movimento movePerso;

    public float danoFerramenta;
    public Slider vidaFerramenta;
    public GameObject ferramenta;
    public GameObject quebrandoFerramenta;

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

        if(vidaFerramenta.value <= 0){
            quebrandoFerramenta.SetActive(true);
            ferramenta.SetActive(false);
        }
        else{
            quebrandoFerramenta.SetActive(false);
            ferramenta.SetActive(true);
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

                if(ferramenta.activeSelf){
                    scriptInimigo.vidaInimigo -= danoFerramenta;
                    vidaFerramenta.value -= 0.05f;
                    sonsPerso.Play();
                }
                else{
                    scriptInimigo.vidaInimigo -= danoSoco;
                    somSoco.Play();
                }
            }
        }
    }

    void Ataque2(){
        if(atacando){
            GameObject inimigoAlvo = ProcurarInimigoAlvo();
            if (inimigoAlvo != null)
            {
                Inimigos scriptInimigo = inimigoAlvo.GetComponent<Inimigos>();
                
                scriptInimigo.Recuar();

                if(ferramenta.activeSelf){
                    scriptInimigo.vidaInimigo -= danoFerramenta * 2;
                    vidaFerramenta.value -= 0.05f;
                    sonsPerso.Play();
                }
                else{
                    scriptInimigo.vidaInimigo -= danoSoco * 2;
                    somSoco.Play();
                }
            }
        }
    }
    //---------------------------------------

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
