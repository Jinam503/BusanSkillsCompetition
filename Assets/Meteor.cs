using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public GameObject player;
    private bool down;
    private Rigidbody2D r;
    int hp = 5;
    void Start()
    {
        down = false;
        r = GetComponent<Rigidbody2D>();
        StartCoroutine(F());
    }
    IEnumerator F()
    {
        r.gravityScale = 0f;
        yield return new WaitForSeconds(3f);
        down = true;
        r.gravityScale = 1f;
        transform.GetChild(0).gameObject.SetActive(false);
    }
    void Update()
    {
        if (!down)
        {
            Vector3 n = Vector3.zero;
            if(player.transform.position.x > transform.position.x)
            {
                n = Vector3.right * 0.005f;
            }
            else if (player.transform.position.x < transform.position.x)
            {
                n = Vector3.left * 0.005f;
            }
            transform.position = transform.position + n;
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "PlayerBullet")
        {
            Bullet b = collision.GetComponent<Bullet>();
            hp -= b.damage;
            Destroy(collision.gameObject);
            if(hp < 0)
            {
                player.transform.GetChild(0).GetChild(7).GetComponent<AudioSource>().Play();
                Destroy(gameObject);
            }
        }
        else if(collision.gameObject.tag == "DestroyWall")
        {
            Destroy(gameObject);
        }
    }
}
