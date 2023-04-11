using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KDreadnought : Enemy
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
        
        float a, c;
        if(GameManager.curStage == 2)
        {
            yield return new WaitForSeconds(2f);
            a = 0.7f;
            c = 4f;
            for (float i = 0; i < 3; i++)
            {

                for (int k = 0; k < 5; k++)
                {
                    a -= 0.3f;
                    c += 0.3f;
                    yield return new WaitForSeconds(0.1f);
                    for (int j = 0; j < 2; j++)
                    {
                        GameObject b3 = Instantiate(bullet1, transform.GetChild(0).position, transform.rotation);
                        Rigidbody2D r3 = b3.GetComponent<Rigidbody2D>();
                        Vector2 vec = new Vector2(Mathf.Cos(Mathf.PI * 2f * (float)j / 10 + a), Mathf.Sin(Mathf.PI * 2f * (float)j / 10 + a));
                        r3.AddForce(vec.normalized * 6f, ForceMode2D.Impulse);
                    }
                    for (int j = 0; j < 2; j++)
                    {
                        GameObject b3 = Instantiate(bullet1, transform.GetChild(1).position, transform.rotation);
                        Rigidbody2D r3 = b3.GetComponent<Rigidbody2D>();
                        Vector2 vec = new Vector2(Mathf.Cos(Mathf.PI * 2f * (float)j / 10 + c), Mathf.Sin(Mathf.PI * 2f * (float)j / 10 + c));
                        r3.AddForce(vec.normalized * 6f, ForceMode2D.Impulse);
                    }
                }

            }
        }
        yield return new WaitForSeconds(2f);
        a = 0f;
        for (int i = 0; i < 6; i++)
        {
            a += 0.3f;
            yield return new WaitForSeconds(0.1f);
            for (int j = 0; j < 6; j++)
            {
                GameObject b = Instantiate(bullet1, transform.position, transform.rotation);
                Rigidbody2D r = b.GetComponent<Rigidbody2D>();
                Vector2 vec = new Vector2(Mathf.Sin(Mathf.PI * 2f * (float)j / 6+a), Mathf.Cos(Mathf.PI * 2f * (float)j / 6+a));
                r.AddForce(vec.normalized * 10f, ForceMode2D.Impulse);
            }
        }
        if(GameManager.curStage == 2)
        {
            yield return new WaitForSeconds(0.5f);
            a = 0f;
            for (int i = 0; i < 6; i++)
            {
                a -= 0.3f;
                yield return new WaitForSeconds(0.1f);
                for (int j = 0; j < 6; j++)
                {
                    GameObject b = Instantiate(bullet1, transform.position, transform.rotation);
                    Rigidbody2D r = b.GetComponent<Rigidbody2D>();
                    Vector2 vec = new Vector2(Mathf.Sin(Mathf.PI * 2f * (float)j / 6 + a), Mathf.Cos(Mathf.PI * 2f * (float)j / 6 + a));
                    r.AddForce(vec.normalized * 10f, ForceMode2D.Impulse);
                }
            }
        }
        yield return new WaitForSeconds(2f);
        a = 1.5f;
        c = 2f;
        for (float i = 0; i < 15; i++)
        {
            a -= 0.1f;
            c += 0.1f;
            yield return new WaitForSeconds(0.6f);
            for (int j = 0; j < 5; j++)
            {
                GameObject b3 = Instantiate(bullet1, transform.GetChild(0).position, transform.rotation);
                Rigidbody2D r3 = b3.GetComponent<Rigidbody2D>();
                Vector2 vec = new Vector2(Mathf.Sin(Mathf.PI * 2f * (float)j / 10 + a), Mathf.Cos(Mathf.PI * 2f * (float)j / 10 + a));
                r3.AddForce(vec.normalized * 3f, ForceMode2D.Impulse);
            }
            for (int j = 0; j < 5; j++)
            {
                GameObject b3 = Instantiate(bullet1, transform.GetChild(1).position, transform.rotation);
                Rigidbody2D r3 = b3.GetComponent<Rigidbody2D>();
                Vector2 vec = new Vector2(Mathf.Sin(Mathf.PI * 2f * (float)j / 10 +c), Mathf.Cos(Mathf.PI * 2f * (float)j / 10 + c));
                r3.AddForce(vec.normalized * 3f, ForceMode2D.Impulse);
            }
        }
        yield return new WaitForSeconds(2f);
        for(int i = 0; i< 4; i++)
        {
            yield return new WaitForSeconds(0.6f);
            for (int j = 0; j < 30; j++)
            {
                GameObject b = Instantiate(bullet2, transform.position, transform.rotation);
                Rigidbody2D r = b.GetComponent<Rigidbody2D>();
                Vector2 vec = new Vector2(Mathf.Sin(Mathf.PI * 2f * (float)j / 30), Mathf.Cos(Mathf.PI * 2f * (float)j / 30));
                r.AddForce(vec.normalized * 3f, ForceMode2D.Impulse);
            }
            yield return new WaitForSeconds(0.6f);
            for (int j = 0; j < 25; j++)
            {
                GameObject b1 = Instantiate(bullet2, transform.position, transform.rotation);
                Rigidbody2D r1 = b1.GetComponent<Rigidbody2D>();
                Vector2 vec = new Vector2(Mathf.Sin(Mathf.PI * 2f * (float)j / 25), Mathf.Cos(Mathf.PI * 2f * (float)j / 25));
                r1.AddForce(vec.normalized * 3f, ForceMode2D.Impulse);
            }
        }
        yield return new WaitForSeconds(2f);
        a = 0f;
        for (float i = 0; i < 20; i++)
        {
            a += 0.3f;
            yield return new WaitForSeconds(0.1f);
            for (int j = 0; j < 4; j++)
            {
                GameObject b2 = Instantiate(bullet2, transform.position, transform.rotation);
                Rigidbody2D r2 = b2.GetComponent<Rigidbody2D>();
                Vector2 vec = new Vector2(Mathf.Sin(Mathf.PI * 2f * (float)j / 4 + a), Mathf.Cos(Mathf.PI * 2f * (float)j / 4 + a));
                r2.AddForce(vec.normalized * 6f, ForceMode2D.Impulse);
            }
        }
        StartCoroutine("Fire");
    }
}
