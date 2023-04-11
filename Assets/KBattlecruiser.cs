using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KBattlecruiser : Enemy
{
    public GameObject bullet1;
    public GameObject bullet2;
    public GameObject bullet3;
    // Start is called before the first frame update
    protected override void Start()
    {
        collider = GetComponent<PolygonCollider2D>();
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
        yield return new WaitForSeconds(GameManager.curStage == 1 ? 2f : 1f);
        for (int i = 0; i < 4; i++)
        {
            if(GameManager.curStage == 1)
            {
                yield return new WaitForSeconds(0.2f);

                GameObject b = Instantiate(bullet3, transform.position, transform.rotation);
                Rigidbody2D r = b.GetComponent<Rigidbody2D>();
                r.AddForce(Vector2.down * 5f, ForceMode2D.Impulse);
                b = Instantiate(bullet3, transform.position, transform.rotation);
                r = b.GetComponent<Rigidbody2D>();
                r.AddForce(Vector2.down * 5f + Vector2.left * -3f, ForceMode2D.Impulse);
                b = Instantiate(bullet3, transform.position, transform.rotation);
                r = b.GetComponent<Rigidbody2D>();
                r.AddForce(Vector2.down * 5f + Vector2.right * -3f, ForceMode2D.Impulse);
            }
            else
            {
                GameObject b = Instantiate(bullet3, transform.position, transform.rotation);
                Rigidbody2D r = b.GetComponent<Rigidbody2D>();
                Vector2 v = (player.transform.position - transform.position).normalized;
                r.AddForce((v * 5f) + (Vector2.left * -3f), ForceMode2D.Impulse);
                b = Instantiate(bullet3, transform.position, transform.rotation);
                r = b.GetComponent<Rigidbody2D>();
                r.AddForce(v * 5f, ForceMode2D.Impulse);
                b = Instantiate(bullet3, transform.position, transform.rotation);
                r = b.GetComponent<Rigidbody2D>();
                r.AddForce((v * 5f) + (Vector2.right * -3f), ForceMode2D.Impulse);
            }
            
        }
        
        yield return new WaitForSeconds(GameManager.curStage == 1 ? 2f : 1f);
        for (int j = 0; j < 2; j++)
        {
                yield return new WaitForSeconds(0.1f);
                GameObject b = Instantiate(bullet3, transform.position, transform.rotation);
                Rigidbody2D r = b.GetComponent<Rigidbody2D>();
                Vector2 vec = player.transform.position - transform.position;
                r.AddForce(vec.normalized * 10f, ForceMode2D.Impulse);
            
           
            
        }
        if(GameManager.curStage == 2)
        {
            yield return new WaitForSeconds(1f);
            for (int j = 0; j < 2; j++)
            {
                yield return new WaitForSeconds(0.1f);
                GameObject b = Instantiate(bullet3, transform.position, transform.rotation);
                Rigidbody2D r = b.GetComponent<Rigidbody2D>();
                Vector2 vec = player.transform.position - transform.position;
                r.AddForce(vec.normalized * 10f, ForceMode2D.Impulse);
            }
        }
        
        yield return new WaitForSeconds(GameManager.curStage == 1 ? 2f:1f);
        for (int j = 0; j < 49; j++)
        {
            yield return new WaitForSeconds(0.1f);
            GameObject b = Instantiate(bullet2, transform.position, transform.rotation);
            Rigidbody2D r = b.GetComponent<Rigidbody2D>();
            Vector2 vec = new Vector2(Mathf.Sin(Mathf.PI * 10f * (float)j/99), -1) ;
            r.AddForce(vec.normalized * (GameManager.curStage == 1 ? 6f : 8f), ForceMode2D.Impulse);
        }
        yield return new WaitForSeconds(2f);
        
        StartCoroutine("Fire");
    }
}
