using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigosMovimentoAuto : MonoBehaviour
{
    public Transform[] pontosCaminho;
    public float speed = 3f;
    private int pontoAtual = 0;
    public bool podeMover = false;

    private Animator anim;

    private bool andandoFrente;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (podeMover && pontoAtual < pontosCaminho.Length)
        {
            moveAtePonto(pontosCaminho[pontoAtual]);

            if (Vector3.Distance(transform.position, pontosCaminho[pontoAtual].position) < 0.2f)
            {
                if (andandoFrente)
                {
                    pontoAtual++;
                }
                else
                {
                    pontoAtual--;
                }
                
                if (pontoAtual >= pontosCaminho.Length)
                {
                    StartCoroutine(IntervaloEntrePontos());
                }

                if(pontoAtual <= 0)
                {
                    StartCoroutine(IntervaloEntrePontos());
                }
            }
        }
    }

    IEnumerator IntervaloEntrePontos()
    {
        anim.SetBool("andando", false);

        yield return new WaitForSeconds(5f);

        if (pontoAtual >= pontosCaminho.Length)
        {
            andandoFrente = false;
        }
        if (pontoAtual <= 0)
        {
            andandoFrente = true;
        }
    }

    private void moveAtePonto(Transform targetPoint)
    {
        Vector3 direction = (targetPoint.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
        transform.LookAt(targetPoint);
        anim.SetBool("andando", true);
    }
}
