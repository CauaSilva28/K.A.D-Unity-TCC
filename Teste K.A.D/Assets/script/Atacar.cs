using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atacar : MonoBehaviour
{
    public bool atacando = false;
    public float anguloDeVisao = 45f;

    public AudioClip somSoco;
    private AudioSource sonsPerso;

    public AnimacoesPerso animPerso;
    public Movimento movePerso;

    private List<GameObject> inimigos = new List<GameObject>();
    private bool emAtaque = false;

    void Start()
    {
        sonsPerso = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!emAtaque && atacando)
        {
            GameObject inimigoAlvo = ProcurarInimigoAlvo();
            Dino scriptDino = inimigoAlvo.GetComponent<Dino>();
            
            if(movePerso.isDashing && Input.GetKey(KeyCode.W)){
                scriptDino.Recuar();
            }

            if (Input.GetMouseButtonDown(0))
            {
                
                if (inimigoAlvo != null)
                {
                    StartCoroutine(tempoAtaque(inimigoAlvo));
                }
            }
        }

        LimparInimigosMortos();
    }

    IEnumerator tempoAtaque(GameObject inimigo)
    {
        emAtaque = true;
        animPerso.emAcao = true;
        animPerso.anim.SetTrigger("attack");
        animPerso.anim.SetInteger("transition", 4);

        yield return new WaitForSeconds(0.3f);
        sonsPerso.PlayOneShot(somSoco);

        yield return new WaitForSeconds(0.2f);

        Dino scriptDino = inimigo.GetComponent<Dino>();
        if (scriptDino != null)
        {
            scriptDino.vidaDino--;
        }

        yield return new WaitForSeconds(0.5f);

        float tempoMaximoEspera = 0.7f;
        float tempoDecorrido = 0f;
        bool botaoPressionado = false;

        while (tempoDecorrido < tempoMaximoEspera)
        {
            if (Input.GetMouseButtonDown(0))
            {
                botaoPressionado = true;
                break;
            }
            tempoDecorrido += Time.deltaTime;
            yield return null;
        }

        if (botaoPressionado && scriptDino != null)
        {
            sonsPerso.PlayOneShot(somSoco);
            scriptDino.vidaDino-=2;
        }

        yield return new WaitForSeconds(1f);

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
        inimigos.RemoveAll(inimigo => inimigo == null || inimigo.GetComponent<Dino>().vidaDino <= 0);
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
