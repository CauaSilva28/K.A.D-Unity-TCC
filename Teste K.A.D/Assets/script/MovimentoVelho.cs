using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovimentoVelho : MonoBehaviour
{
    public float distanciaMinima;

    private Animator anim;

    public Transform personagem;

    private NavMeshAgent navMeshAgent;
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
        navMeshAgent.SetDestination(personagem.position);

        if (navMeshAgent.speed > 20)
        {
            anim.SetInteger("transition", 1);
        }
        else
        {
            anim.SetInteger("transition", 0);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("AreaPlayer"))
        {
            navMeshAgent.speed = 0;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("AreaPlayer"))
        {
            navMeshAgent.speed = 30;
        }
    }
}
