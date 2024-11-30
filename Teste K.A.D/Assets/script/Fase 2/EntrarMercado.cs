using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EntrarMercado : MonoBehaviour
{
    public GameObject[] player;
    public List<GameObject> dinosRua;
    public Transform posicaoMercadoEntrada;
    public TelaSelecionarPerso selecaoPerso;

    public AparecerTeclasTexto scriptAparecerTeclas;
    public AparecerTextos textoObjetivo;

    public GameObject transicao;
    public GameObject Mirela;
    public Transform posicaoMirelaForaMercado;

    private bool entrarMercado = false;

    public bool entrandoNoMercado;

    // Update is called once per frame
    void Update()
    {
        
        if (entrarMercado)
        {
            if(entrandoNoMercado){
                if(dinosRua.Count == 0){
                    scriptAparecerTeclas.aparecer = true;
                    scriptAparecerTeclas.texto = "Aperte \"F\" para entrar no mercado";
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        StartCoroutine(teleporteMercado());
                        entrarMercado = false;
                    }
                }
                else{
                    scriptAparecerTeclas.aparecer = true;
                    scriptAparecerTeclas.texto = "Derrote todos os dinossauros!";
                }
            }
            else{      
                scriptAparecerTeclas.aparecer = true;
                scriptAparecerTeclas.texto = "Aperte \"F\" para sair do mercado";

                if (Input.GetKeyDown(KeyCode.F))
                {
                    StartCoroutine(teleporteMercado());
                    entrarMercado = false;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            entrarMercado = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            entrarMercado = false;
            scriptAparecerTeclas.aparecer = false;
            scriptAparecerTeclas.texto = "";
        }
    }

    IEnumerator teleporteMercado(){
        transicao.SetActive(true);
        transicao.GetComponent<Animator>().SetInteger("transition", 1);

        yield return new WaitForSeconds(1f);

        Vector3 posicaoPlayerMercado = posicaoMercadoEntrada.position;
        player[selecaoPerso.numPerso].GetComponent<Transform>().position = posicaoPlayerMercado;
        player[selecaoPerso.numPerso].GetComponent<Movimento>().enabled = false; //é necessário desabilitar o script de movimento para que o personagem possa teleportar até o local determinado
        
        textoObjetivo.aparecerTexto = false;
        textoObjetivo.texto = "";
        scriptAparecerTeclas.aparecer = false;
        scriptAparecerTeclas.texto = "";

        if(!entrandoNoMercado){
            Mirela.GetComponent<Transform>().transform.position = posicaoMirelaForaMercado.position;
            Mirela.GetComponent<MovimentoNpcs>().enabled = false;
            Mirela.GetComponent<NavMeshAgent>().enabled = false;
        }

        yield return new WaitForSeconds(1f);

        player[selecaoPerso.numPerso].GetComponent<Movimento>().enabled = true;
        transicao.GetComponent<Animator>().SetInteger("transition", 2);

        yield return new WaitForSeconds(0.5f);

        transicao.GetComponent<Animator>().SetInteger("transition", 0);
        transicao.SetActive(false);

        if(!entrandoNoMercado){
            Mirela.GetComponent<MovimentoNpcs>().enabled = true;
            Mirela.GetComponent<NavMeshAgent>().enabled = true;
        }

        yield return new WaitForSeconds(0.5f);

        Destroy(gameObject);
    }
}
