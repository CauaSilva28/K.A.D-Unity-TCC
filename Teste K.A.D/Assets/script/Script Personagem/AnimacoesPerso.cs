using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacoesPerso : MonoBehaviour
{
    public Animator anim;

    public bool emAcao = false;
    private bool cooldownAtaque = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!emAcao){
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

        if(!cooldownAtaque){
            if (Input.GetMouseButtonDown(0) || Input.GetKey(KeyCode.W) && Input.GetMouseButtonDown(0) || Input.GetKey(KeyCode.S) && Input.GetMouseButtonDown(0) || Input.GetKey(KeyCode.S) && Input.GetMouseButtonDown(0) || Input.GetKey(KeyCode.D) && Input.GetMouseButtonDown(0))
            {
                StartCoroutine(PararAtaque());
            }
        }
    }

    IEnumerator PararAtaque(){
        emAcao = true;
        anim.SetTrigger("attack");
        anim.SetInteger("transition", 4);

        yield return new WaitForSeconds(1f);

        emAcao = false;
        cooldownAtaque = true;

        yield return new WaitForSeconds(0.8f);

        cooldownAtaque = false;
    }
}
