using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torpedo : Enemy
{
    public GameObject bullet;
    public GameObject[] spawnBullets;
    // Start is called before the first frame update
    protected override void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        rigid = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();
        StartCoroutine("Fire");
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (hp < 0 && !died)
        {
            died = true;
            rigid.velocity = Vector2.zero;
            collider.enabled = false;
            
            audio.Play();
            anim.SetTrigger("Destroy");
            StopAllCoroutines();
        }
    }
    private IEnumerator Fire()
    {
        yield return new WaitForSeconds(3f);
        anim.SetTrigger("Attack");
        for(int i = 0; i< 6; i++)
        {
            yield return new WaitForSeconds(0.2f);
            GameObject b = Instantiate(bullet, spawnBullets[i].transform.position, spawnBullets[i].transform.rotation);
            Rigidbody2D r = b.GetComponent<Rigidbody2D>();
            r.AddForce(Vector2.down * 5f, ForceMode2D.Impulse);
        }
        StartCoroutine("Fire");
    }
}
