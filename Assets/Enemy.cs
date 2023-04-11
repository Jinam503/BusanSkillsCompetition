using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator anim;
    public int hp;
    public float speed;
    protected Rigidbody2D rigid;
    protected new Collider2D collider;
    public new  AudioSource audio;
    public bool died;

    public int enemyScore;
    public GameObject player;

    public GameObject[] items;
    public Vector3 nextPos;
    protected virtual void Start()
    {
        died = false;
        
    }
    protected virtual void Update()
    {
        if (gameObject.tag != "MBoss" && gameObject.tag != "LBoss" && !died)
        {
            
            transform.position = transform.position + nextPos * speed * Time.deltaTime;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            GameObject g = collision.gameObject;
            Destroy(g);
            Bullet b = collision.GetComponent<Bullet>();
            hp -= b.damage;
            if (gameObject.tag == "LBoss" || gameObject.tag == "MBoss")
            {
                int randNum = Random.Range(0, 200);
                if (randNum == 1) // Gas
                {
                    Vector3 v = new Vector3(Random.Range(-1f, 1f), 0, 0);
                    GameObject item = Instantiate(items[0], transform.position + v, Quaternion.identity);
                    Rigidbody2D r = item.GetComponent<Rigidbody2D>();
                    r.AddForce(Vector2.down * 1f, ForceMode2D.Impulse);
                }
            }
        }
        else if(collision.gameObject.tag == "DestroyWall")
        {
            if (gameObject.tag == "MBoss"||gameObject.tag == "LBoss") return;
            Destroy(gameObject);
        }
    }
    public void Destroys()
    {
        player.GetComponent<Player>().score += enemyScore;
        //æ∆¿Ã≈€
        if (gameObject.tag == "Enemy") {
            int randNum = Random.Range(0, 20);
            if (randNum < 3) // Gas
            {
                Vector3 v = new Vector3(Random.Range(-1f, 1f), 0, 0);
                GameObject item = Instantiate(items[0], transform.position + v, Quaternion.identity);
                Rigidbody2D r = item.GetComponent<Rigidbody2D>();
                r.AddForce(Vector2.down * 1f, ForceMode2D.Impulse);
            } else if (randNum < 4)// Life
            {
                Vector3 v = new Vector3(Random.Range(-1f, 1f), 0, 0);
                GameObject item = Instantiate(items[1], transform.position + v, Quaternion.identity);
                Rigidbody2D r = item.GetComponent<Rigidbody2D>();
                r.AddForce(Vector2.down * 1f, ForceMode2D.Impulse);
            }
            randNum = Random.Range(0, 30);
            if (randNum < 10) // coin
            {
                Vector3 v = new Vector3(Random.Range(-1f, 1f), 0, 0);
                GameObject item = Instantiate(items[2], transform.position + v, Quaternion.identity);
                Rigidbody2D r = item.GetComponent<Rigidbody2D>();
                r.AddForce(Vector2.down * 1f, ForceMode2D.Impulse);
            }
            else if (randNum < 11) // shield
            {
                Vector3 v = new Vector3(Random.Range(-1f, 1f), 0, 0);
                GameObject item = Instantiate(items[3], transform.position + v, Quaternion.identity);
                Rigidbody2D r = item.GetComponent<Rigidbody2D>();
                r.AddForce(Vector2.down * 1f, ForceMode2D.Impulse);
            }
            else if (randNum < 12) // upgrade
            {
                Vector3 v = new Vector3(Random.Range(-1f, 1f), 0, 0);
                GameObject item = Instantiate(items[4], transform.position + v, Quaternion.identity);
                Rigidbody2D r = item.GetComponent<Rigidbody2D>();
                r.AddForce(Vector2.down * 1f, ForceMode2D.Impulse);
            }
            else if (randNum < 13) // bomb
            {
                Vector3 v = new Vector3(Random.Range(-1f, 1f), 0, 0);
                GameObject item = Instantiate(items[5], transform.position + v, Quaternion.identity);
                Rigidbody2D r = item.GetComponent<Rigidbody2D>();
                r.AddForce(Vector2.down * 1f, ForceMode2D.Impulse);
            }
            else return;
        }

        if (gameObject.tag == "MBoss")
        {
            if (GameManager.curStage == 1)
            {
                
                GameManager.KBossMClear = true;
                GameManager.KBossMStart = false;
            }
            else{
                GameManager.NBossMClear = true;
                GameManager.NBossMStart = false;
            }
        }
        if (gameObject.tag == "LBoss")
        {
            if (GameManager.curStage == 1)
            {
                GameManager.EnterStage2 = true;
                GameManager.KBossLClear = true;
                GameManager.KBossLStart = false;
                GameManager.curStage = 2;
            }
            else
            {
                GameManager.NBossLClear = true;
                GameManager.NBossLStart = false;
            }
        }
        Destroy(gameObject);
    }
}
