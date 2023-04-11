using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenResolution : MonoBehaviour
{
    void Awake()
    {
        R();
    }
    private void R()
    {
        int w = 640;
        int h = 1080;
        Screen.SetResolution(w, h, true);
    }
}
