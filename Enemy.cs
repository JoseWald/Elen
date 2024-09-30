using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb;
    public  bool die=false;
    Animator animator;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator= animator=GetComponent<Animator>();
        StartCoroutine(Expire());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayFootStep();
        Mourir();
        rb.velocity = new Vector2(-3  , rb.velocity.y);
    }

    public void PlayFootStep(){
       //
    }

    IEnumerator Expire(){
        yield return new WaitForSeconds(3f);
        die=true;
    }

    void Mourir(){
        if(die){
            animator.SetBool("Dying",true);
            StartCoroutine(Vanish());
        }
    }

    IEnumerator Vanish(){
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }

    public void TakeDamage(){
        this.die=true;
    }



}
