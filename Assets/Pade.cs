using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Pade : MonoBehaviour
{
    Text text;
    private void Start()
    {
        text = GetComponent<Text>();
        StartCoroutine("PadeIn");
    }
    void Update()
    {
        
    }
    IEnumerator PadeIn()
    {
        for(float i= 100;  i>0; i-=2)
        {
            yield return new WaitForSeconds(0.02f);
            text.color = new Color(1, 1, 1, i / 50f);
        }
        for (float i = 0; i < 100; i+=2)
        {
            yield return new WaitForSeconds(0.02f);
            text.color = new Color(1, 1, 1, i / 50f);
        }
        StartCoroutine("PadeIn");
    }
}
