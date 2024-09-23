using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntrarMercado : MonoBehaviour
{
    public GameObject[] player;
    public List<GameObject> dinosRua;
    public Transform posicaoMercadoEntrada;
    public TelaSelecionarPerso selecaoPerso;

    public AparecerTeclasTexto scriptAparecerTeclas;
    public AparecerTextos textoObjetivo;

    public GameObject transicao;

    private bool entrarMercado = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (entrarMercado)
        {
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

        yield return new WaitForSeconds(1f);

        player[selecaoPerso.numPerso].GetComponent<Movimento>().enabled = true;
        transicao.GetComponent<Animator>().SetInteger("transition", 2);

        yield return new WaitForSeconds(0.5f);

        transicao.GetComponent<Animator>().SetInteger("transition", 0);
        transicao.SetActive(false);

        yield return new WaitForSeconds(0.5f);

        Destroy(gameObject);
    }
}
