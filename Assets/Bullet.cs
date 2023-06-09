using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "PlayerWall" && gameObject.tag == "PlayerBullet")
        {
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag == "DestroyWall" && gameObject.tag == "EnemyBullet")
        {
            Destroy(gameObject);
        }
    }
}
