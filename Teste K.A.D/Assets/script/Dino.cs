using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dino : MonoBehaviour
{
    public float distanciaMinima;

    private Animator anim;
    public Transform personagem;
    private NavMeshAgent navMeshAgent;

    public Transform rotacionarDino;

    private AudioSource SomDino;

    public AudioClip Rosnando;

    private bool sofrendoAcao = false;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        navMeshAgent.stoppingDistance = distanciaMinima;

        SomDino = GetComponent<AudioSource>();

        //Personagem esta andando de costas, esse código faz ele andar corretamente
        rotacionarDino.transform.Rotate(-180f, 180f, 0f);

        //Inicia som
        StartCoroutine(TocarSomACada10Segundos());
    }

    //Som de rosnando a cada 10 segundos
    IEnumerator TocarSomACada10Segundos()
    {
        while (true)
        {
            SomDino.PlayOneShot(Rosnando);
            yield return new WaitForSeconds(10f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.SetDestination(personagem.position);

        float distancia = Vector3.Distance(transform.position, personagem.position);

        if(!sofrendoAcao){
            if(navMeshAgent.speed >= 50){
                anim.SetInteger("transition", 1);
            }
            else if(navMeshAgent.speed <= 0){
                anim.SetInteger("transition", 0);
            }
        }
    }

    void OnCollisionStay(Collision collision){
        if(collision.gameObject.CompareTag("Player")){
            anim.SetInteger("transition", 2);
            sofrendoAcao = true;
        }
    }

    void OnCollisionExit(Collision collision){
        if(collision.gameObject.CompareTag("Player")){
            sofrendoAcao = false;
        }
    }
}