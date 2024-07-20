using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atacar : MonoBehaviour
{
    public float danoPlayer;
    public bool atacando = false;
    public float anguloDeVisao = 45f;

    public AudioSource sonsPerso;
    public AnimacoesPerso animPerso;
    public Movimento movePerso;

    private List<GameObject> inimigos = new List<GameObject>();
    private bool emAtaque = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (!emAtaque && atacando)
        {
            GameObject inimigoAlvo = ProcurarInimigoAlvo();
            if (inimigoAlvo != null)
            {
                Inimigos scriptInimigo = inimigoAlvo.GetComponent<Inimigos>();

                if (movePerso.isDashing && Input.GetKey(KeyCode.W))
                {
                    scriptInimigo.Recuar();
                }

                if (Input.GetMouseButtonDown(0))
                {
                    StartCoroutine(TempoAtaque(inimigoAlvo));
                }
            }
        }

        LimparInimigosMortos();
    }

    IEnumerator TempoAtaque(GameObject inimigo)
    {
        emAtaque = true;
        animPerso.emAcao = true;
        animPerso.anim.SetTrigger("attack");
        animPerso.anim.SetInteger("transition", 4);

        yield return new WaitForSeconds(0.3f);
        sonsPerso.Play();

        yield return new WaitForSeconds(0.2f);

        Inimigos scriptInimigo = inimigo.GetComponent<Inimigos>();
        if (scriptInimigo != null)
        {
            scriptInimigo.vidaInimigo -= danoPlayer;
        }

        yield return new WaitForSeconds(0.5f);

        float tempoMaximoEspera = 0.7f;
        float tempoDecorrido = 0f;
        bool botaoPressionado = false;

        while (tempoDecorrido < tempoMaximoEspera)
        {
            if (Input.GetMouseButtonDown(0) && atacando)
            {
                botaoPressionado = true;
                break;
            }
            tempoDecorrido += Time.deltaTime;
            yield return null;
        }

        if (botaoPressionado && scriptInimigo != null)
        {
            sonsPerso.Play();
            scriptInimigo.vidaInimigo -= danoPlayer * 2;
            scriptInimigo.Recuar();
        }

        yield return new WaitForSeconds(0.5f);

        emAtaque = false;
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
