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

        // Ajustar a rotação para a direção do movimento
        Vector3 direction = navMeshAgent.velocity.normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction) * Quaternion.Euler(0, -90, 0); // Adicionar -90 graus à rotação
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * navMeshAgent.angularSpeed);
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
