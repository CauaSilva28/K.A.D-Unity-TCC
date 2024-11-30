using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FerramentasNaCena : MonoBehaviour
{
    public GameObject[] ferramentas;
    public TelaSelecionarPerso selecaoPerso;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(ferramentas[selecaoPerso.numPerso], transform.position, Quaternion.identity);
    }
}
