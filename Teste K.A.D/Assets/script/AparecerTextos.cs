using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AparecerTextos : MonoBehaviour
{
    public Text textoCima;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            textoCima.text = "Leve o Seu Zé para consertar a Kombi";
            textoCima.enabled = true;
        }
    }
}
