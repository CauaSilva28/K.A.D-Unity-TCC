using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VeiculoInimigo : MonoBehaviour
{
    [Header("Elementos inimigo")]
    public float distanciaMinima;
    public float velocidadeInimigo;
    public UnityEngine.AI.NavMeshAgent navMeshAgent;
    public Transform destino;

    private float tempoSpawnarExplosivo = 10f;

    private float veloGiroRoda = 100f;

    public GameObject[] rodas;
    public GameObject explosivo;

    public Slider vidaInimigo;

    private bool spawnExplosivo = false;

    [Header("Objetos finais")]

    public GameObject kombi;

    public GameObject[] objDialogos;

    public GameObject telaTransicao;
    public GameObject telaGameOver;
    public GameObject somGameOver;
    public GameObject cutsceneFinal;
    public GameObject musicaFundo;

    public GameObject somVeiculo;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        navMeshAgent.stoppingDistance = distanciaMinima;
        navMeshAgent.speed = velocidadeInimigo;
    }

    // Update is called once per frame
    void Update()
    {
        if(!spawnExplosivo){
            InvokeRepeating("SpawnarExplosivo", 1, tempoSpawnarExplosivo);
            spawnExplosivo = true;
        }

        float currentVeloGiroRoda = veloGiroRoda * Time.deltaTime+2;

        for (var i = 0; i < rodas.Length; i++)
        {
            rodas[i].transform.Rotate(0f, 0f, -currentVeloGiroRoda);
        }

        navMeshAgent.SetDestination(destino.position);

        // Ajustar a rotação para a direção do movimento
        Vector3 direction = navMeshAgent.velocity.normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction) * Quaternion.Euler(0, -90, 0); // Adicionar -90 graus à rotação
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * navMeshAgent.angularSpeed);

        AcoesPerdaDeVida();
        Finais();
    }

    void SpawnarExplosivo(){
        if(navMeshAgent.speed > 0){
            GameObject explosivoInstance = Instantiate(explosivo, transform.position, Quaternion.identity);
            explosivoInstance.tag = "explosivo";
        }
    }

    void Finais(){
        if(vidaInimigo.value <= 0){
            StartCoroutine(iniciarCutsceneFim());
        }

        if(Vector3.Distance(transform.position, destino.transform.position) <= distanciaMinima || Vector3.Distance(transform.position, kombi.GetComponent<Transform>().transform.position) >= 2500f){
            StartCoroutine(inimigoEscapou());
        }
    }

    void AcoesPerdaDeVida(){
        if(vidaInimigo.value <= 0.7 && vidaInimigo.value >= 0.6){
            tempoSpawnarExplosivo = 6f;     
            CancelInvoke("SpawnarExplosivo");
            spawnExplosivo = false;
            objDialogos[0].SetActive(true);
        }

        if(vidaInimigo.value <= 0.2 && vidaInimigo.value >= 0.1){
            objDialogos[1].SetActive(true);
        }
    }

    void OnCollisionStay(Collision collider){
        if(collider.gameObject.CompareTag("Player") && !collider.gameObject.CompareTag("frenteVeiculoInimigo") && !collider.gameObject.CompareTag("Untagged")){
            if(!kombi.GetComponent<MovimentoKombi>().dandoRe){
                vidaInimigo.value -= 0.2f * Time.deltaTime;
            }
        }
    }

    IEnumerator iniciarCutsceneFim(){
        navMeshAgent.speed = 0;
        veloGiroRoda = 0;
        kombi.GetComponent<MovimentoKombi>().enabled = false;
        kombi.GetComponent<Rigidbody>().isKinematic = true;
        kombi.tag = "Untagged";
        kombi.GetComponent<MovimentoKombi>().turbo.SetActive(false);
        somVeiculo.SetActive(false);
        PlayerPrefs.SetInt("Fase3Completa", 1);

        yield return new WaitForSeconds(3f);
        
        telaTransicao.SetActive(true);
        telaTransicao.GetComponent<Animator>().SetInteger("transition", 2);

        yield return new WaitForSeconds(5f);

        cutsceneFinal.SetActive(true);
        kombi.SetActive(false);
        gameObject.SetActive(false);
        musicaFundo.SetActive(false);
    }

    public void irFase4(){
        SceneManager.LoadScene("Fase4");
    }

    IEnumerator inimigoEscapou(){
        objDialogos[2].SetActive(true);
        kombi.GetComponent<MovimentoKombi>().enabled = false;
        kombi.GetComponent<Rigidbody>().isKinematic = true;
        kombi.tag = "Untagged";
        kombi.GetComponent<MovimentoKombi>().turbo.SetActive(false);
        kombi.GetComponent<MovimentoKombi>().pauseJogo.perdendo = true;
        somVeiculo.SetActive(false);

        yield return new WaitForSeconds(8f);

        telaTransicao.SetActive(true);
        telaTransicao.GetComponent<Animator>().SetInteger("transition", 2);

        yield return new WaitForSeconds(4f);

        somGameOver.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        telaGameOver.SetActive(true);
        telaGameOver.GetComponent<Animator>().SetBool("surgir", true);

        yield return new WaitForSeconds(1f);

        AudioListener.volume = 0;
    }
}