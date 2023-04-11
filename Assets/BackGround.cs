using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{ 
    void Update()
    {
        transform.position = transform.position +Vector3.down * Time.deltaTime;
        if(transform.position.y < -12)
        {
            transform.position += Vector3.up * 24;
        }
    }
}
