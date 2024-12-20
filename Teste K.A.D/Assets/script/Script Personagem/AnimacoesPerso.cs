using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacoesPerso : MonoBehaviour
{
    public Animator anim;

    public bool morrendo = false;

    private Movimento movePerso;

    public PausarJogo pauseJogo;

    public GameObject ferramenta;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        movePerso = GetComponent<Movimento>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!morrendo && !pauseJogo.pausado){
            anim.SetInteger("transition", 0);

            if(Input.GetKey(KeyCode.W)){
                anim.SetInteger("transition", 1);
            }
            if(Input.GetKey(KeyCode.A)){
                anim.SetInteger("transition", 3);
            }
            if(Input.GetKey(KeyCode.D)){
                anim.SetInteger("transition", 4);
            }
            if(Input.GetKey(KeyCode.S)){
                anim.SetInteger("transition", 5);
            }

            if(!movePerso.cansou){
                if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift)){
                    anim.SetInteger("transition", 2);
                }
                if(Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.LeftShift)){
                    anim.SetInteger("transition", 6);
                }
            }
            
            if (Input.GetMouseButtonDown(0))
            {
                if(ferramenta.activeSelf){
                    anim.SetTrigger("attack");
                    anim.SetInteger("transition", 7);
                }
                else{
                    anim.SetTrigger("attack");
                    anim.SetInteger("transition", 8);
                }
            }
        }
    }

}
