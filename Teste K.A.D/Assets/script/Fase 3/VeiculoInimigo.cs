using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VeiculoInimigo : MonoBehaviour
{
    public float distanciaMinima;
    public float velocidadeInimigo;
    public UnityEngine.AI.NavMeshAgent navMeshAgent;
    public Transform destino;

    public GameObject explosivo;

    public Slider vidaInimigo;

    public MovimentoKombi moveKombi;
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        navMeshAgent.stoppingDistance = distanciaMinima;
        navMeshAgent.speed = velocidadeInimigo;

        InvokeRepeating("SpawnarExplosivo", 1, 10);
    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.SetDestination(destino.position);
    }

    void SpawnarExplosivo(){
        if(navMeshAgent.speed > 0){
            GameObject explosivoInstance = Instantiate(explosivo, transform.position, Quaternion.identity);
            explosivoInstance.tag = "explosivo";
        }
    }

    void OnCollisionStay(Collision collider){
        if(collider.gameObject.CompareTag("Player") && !collider.gameObject.CompareTag("frenteVeiculoInimigo")){
            if(!moveKombi.dandoRe){
                vidaInimigo.value -= 0.15f * Time.deltaTime;
            }
        }
    }
}
