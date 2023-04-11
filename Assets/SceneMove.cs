using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneMove : MonoBehaviour
{
    public void GoPickStage(int a)
    {
        if ( a== 1)
            SceneManager.LoadScene("PickStage");
        if(a == 0)
        {
            SceneManager.LoadScene("Stage 1");
        }
    }
}
