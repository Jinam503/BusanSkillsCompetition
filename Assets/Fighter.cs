using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : Enemy
{
    public GameObject bullet;
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
            rigid.velocity = Vector2.zero;
            collider.enabled = false;
            died = true;
            audio.Play();
            anim.SetTrigger("Destroy");
        }
    }
    private IEnumerator Fire()
    {
        yield return new WaitForSeconds(2f);
        GameObject b = Instantiate(bullet, transform.position, transform.rotation);
        Rigidbody2D r = b.GetComponent<Rigidbody2D>();
        Vector2 vec = player.transform.position - transform.position;
        r.AddForce(vec.normalized * 6f, ForceMode2D.Impulse);
        StartCoroutine("Fire");
    }
}
