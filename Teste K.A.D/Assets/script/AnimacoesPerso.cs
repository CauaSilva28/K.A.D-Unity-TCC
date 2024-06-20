using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacoesPerso : MonoBehaviour
{
    private Animator anim;

    private bool atacando = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!atacando){
            anim.SetInteger("transition", 0);

            if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)){
                anim.SetInteger("transition", 1);
            }
            if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)){
                anim.SetInteger("transition", 3);
            }

            if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.LeftShift)){
                anim.SetInteger("transition", 2);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(PararAtaque());
        }

        IEnumerator PararAtaque(){
            atacando = true;
            anim.SetInteger("transition", 4);

            yield return new WaitForSeconds(0.5f);

            atacando = false;
        }
    }
}
