using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType { Fix, Bomb, Gas, Coin, Upgrade, Shield }
    public ItemType iT;
    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.gameObject.tag == "DestroyWall")
        {
            Destroy(gameObject);
        }
    }
}
