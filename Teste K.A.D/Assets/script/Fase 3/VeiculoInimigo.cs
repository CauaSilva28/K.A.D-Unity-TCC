using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VeiculoInimigo : MonoBehaviour
{
    public float distanciaMinima;
    public float velocidadeInimigo;
    private UnityEngine.AI.NavMeshAgent navMeshAgent;
    public Transform destino;
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
        navMeshAgent.SetDestination(destino.position);
    }
}
