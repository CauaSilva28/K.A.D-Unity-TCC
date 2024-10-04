using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class VisaoInimigos : MonoBehaviour
{
    public Light enemySpotlight;
    public Color originalColor;
    public Color alertColor;
    public float detectionRange;
    public GameObject[] player;
    public TelaSelecionarPerso persoSelecionado;
    public LayerMask LayersBloqueadas;

    public GameObject costasInimigo;

    public GameObject barraVisao;

    public AparecerTeclasTexto scriptAparecerTeclas;

    private bool pegou;

    void Start()
    {
        if (enemySpotlight != null)
        {
            enemySpotlight.color = originalColor;
        }
    }

    void Update()
    {
        if(!pegou){
            DistanciaPlayer();

            // Verifica se o jogador está dentro do alcance da visão do inimigo
            if (PlayerNaArea())
            {         
                StartCoroutine(perseguirPlayer());
                pegou = true;
            }
            else
            {
                // Volta a cor original do Spot Light
                enemySpotlight.color = originalColor;
            }
        }
        else{
            // Muda a cor do Spot Light para vermelho
            enemySpotlight.color = alertColor;
            barraVisao.SetActive(false);
        }
    }

    IEnumerator perseguirPlayer(){  
        gameObject.tag = "inimigo";
        scriptAparecerTeclas.aparecer = false;
        scriptAparecerTeclas.texto = "";
        Destroy(costasInimigo);
        Destroy(GetComponent<InimigosMovimentoAuto>());  
        GetComponent<NavMeshAgent>().enabled = true;

        yield return new WaitForSeconds(0.5f);

        GetComponent<Inimigos>().enabled = true;
    }

    void DistanciaPlayer(){
        if(Vector3.Distance(transform.position, player[persoSelecionado.numPerso].GetComponent<Transform>().transform.position) <= 70f){
            barraVisao.GetComponent<Slider>().value -= 0.3f * Time.deltaTime;
        }
        else{
            barraVisao.GetComponent<Slider>().value = 1;
        }

        if(barraVisao.GetComponent<Slider>().value <= 0){
            StartCoroutine(perseguirPlayer());
            pegou = true;
        }
    }

    bool PlayerNaArea()
    {
         // Calcula a direção do inimigo para o jogador
        Vector3 directionToPlayer = player[persoSelecionado.numPerso].GetComponent<Transform>().position - transform.position;

        // Verifica se o jogador está dentro do cone de visão do Spot Light
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

        if (angleToPlayer < enemySpotlight.spotAngle / 2 && directionToPlayer.magnitude <= detectionRange)
        {
            // Verifica se há algum objeto na linha de visão que está em uma layer que bloqueia a visão
            if (!Physics.Linecast(transform.position, player[persoSelecionado.numPerso].GetComponent<Transform>().position, LayersBloqueadas))
            {
                return true; // Retorna verdadeiro se não houver bloqueio
            }
        }

        return false; // Retorna falso se o jogador estiver fora da visão ou bloqueado
    }
}
