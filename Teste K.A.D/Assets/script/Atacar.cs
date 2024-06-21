using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atacar : MonoBehaviour
{
    private bool atacando = false;
    private GameObject inimigo;
    public float anguloDeVisao = 45f;

    void Update()
    {
        if (atacando)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (EstaOlhandoParaInimigo(inimigo))
                {
                    Destroy(inimigo);
                    atacando = false;
                }
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("inimigo"))
        {
            atacando = true;
            inimigo = collision.gameObject;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("inimigo"))
        {
            atacando = false;
            inimigo = null;
        }
    }

    bool EstaOlhandoParaInimigo(GameObject inimigo)
    {
        if (inimigo == null)
            return false;

        Vector3 direcaoParaInimigo = (inimigo.transform.position - transform.position).normalized;
        float angulo = Vector3.Angle(transform.forward, direcaoParaInimigo);

        return angulo < anguloDeVisao;
    }
}
