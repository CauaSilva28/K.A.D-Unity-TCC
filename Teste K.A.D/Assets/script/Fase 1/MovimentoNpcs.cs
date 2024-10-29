using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovimentoNpcs : MonoBehaviour
{
    public float velocidadeAndando;
    public float velocidadeCorrendo;
    public float distanciaMinima;

    private Animator anim;
    public Transform[] personagem;
    private NavMeshAgent navMeshAgent;

    public TelaSelecionarPerso selecaoPerso;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        navMeshAgent.stoppingDistance = distanciaMinima;
    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.SetDestination(personagem[selecaoPerso.numPerso].position);

        float distancia = Vector3.Distance(transform.position, personagem[selecaoPerso.numPerso].position);

        if (distancia <= distanciaMinima)
        {
            navMeshAgent.speed = 0;
            anim.SetInteger("transition", 0);
        }
        else
        {
            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.LeftShift))
            {
                navMeshAgent.speed = velocidadeCorrendo;
            }
            else
            {
                navMeshAgent.speed = velocidadeAndando;
            }

            if (navMeshAgent.speed >= velocidadeAndando)
            {
                anim.SetInteger("transition", 1);
            }
            else
            {
                anim.SetInteger("transition", 0);
            }
        }
    }
}
