using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber : Enemy
{
    // Start is called before the first frame update
    protected override void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (hp < 0 && !died)
        {
            rigid.velocity = Vector2.zero;
            collider.enabled = false;
            died = true;
            audio.Play();
            anim.SetTrigger("Destroy");
        }
    } 
}
