using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Atacar : MonoBehaviour
{
    public float danoSoco;
    public float danoFerramenta;
    public float danoEspecial;

    public bool atacando = false;
    private bool EmAreaEspecial = false;
    private bool naAreaObjeto = false;
    public float anguloDeVisao;

    public AudioSource[] sonsPerso;
    public AudioSource somSoco;
    public AudioSource somVulto;
    public Movimento movePerso;

    public Slider barraEspecial;
    public Slider vidaFerramenta;
    public GameObject ferramenta;
    public GameObject quebrandoFerramenta;

    public GameObject areaEspecial;

    private List<GameObject> inimigos = new List<GameObject>();

    void Start()
    {
        
    }

    void Update()
    {
        //Ataque especial
        if(barraEspecial.value >= 1){     
            if(Input.GetKeyDown(KeyCode.E)){
                GetComponent<AnimacoesPerso>().anim.SetBool("Especial", true);
                areaEspecial.SetActive(true);
                barraEspecial.value = 0;
            }
        }
        else{
            barraEspecial.value += 0.02f * Time.deltaTime;
        }

        //Empurrao
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

        //Verificacao vida ferramenta
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
                    scriptInimigo.efeitoDano.Play();
                    vidaFerramenta.value -= 0.05f;
                    sonsPerso[0].Play();
                }
                else{
                    scriptInimigo.vidaInimigo -= danoSoco;
                    scriptInimigo.efeitoDano.Play();
                    somSoco.Play();
                }
            }
        }
        else if(naAreaObjeto){
            if(ferramenta.activeSelf){
                sonsPerso[1].Play();
            }
            else{
                sonsPerso[1].Play();
            }
        }
        else{
            somVulto.Play();
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
                    scriptInimigo.efeitoDano.Play();
                    vidaFerramenta.value -= 0.05f;
                    sonsPerso[0].Play();
                }
                else{
                    scriptInimigo.vidaInimigo -= danoSoco * 2;
                    scriptInimigo.efeitoDano.Play();
                    somSoco.Play();
                }
            }
        }
        else if(naAreaObjeto){
            if(ferramenta.activeSelf){
                sonsPerso[1].Play();
            }
            else{
                sonsPerso[1].Play();
            }
        }
        else{
            somVulto.Play();
        }
    }

    void Especial()
    {
        if (EmAreaEspecial)
        {
            foreach (GameObject inimigoEspecial in inimigos) //Estrutura de loop que permite manipular todos os elementos na lista
            {
                if (inimigoEspecial != null)
                {
                    Inimigos scriptInimigo = inimigoEspecial.GetComponent<Inimigos>();
                    if(scriptInimigo != null){
                        scriptInimigo.vidaInimigo -= danoEspecial;
                        scriptInimigo.Recuar();
                        scriptInimigo.parado = false;
                    }
                }
            }
        }
    }

    void FimEspecial(){
        GetComponent<AnimacoesPerso>().anim.SetBool("Especial", false);
        areaEspecial.SetActive(false);
        inimigos.Clear();
        EmAreaEspecial = false;
    }
    //---------------------------------------

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("inimigo"))
        {
            inimigos.Add(other.gameObject);
            EmAreaEspecial = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("inimigo"))
        {
            inimigos.Remove(other.gameObject);
            if (inimigos.Count == 0)
            {
                EmAreaEspecial = false;
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

        if(collision.gameObject.CompareTag("objetosAtacaveis")){
            naAreaObjeto = true;
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

        if(collision.gameObject.CompareTag("objetosAtacaveis")){
            naAreaObjeto = false;
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
