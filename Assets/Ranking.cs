using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using System.Linq;
public class Ranking : MonoBehaviour
{
    public static List<ScoreInfo> scoreListStage = new List<ScoreInfo>();

    private void Start()
    {
        DontDestroyOnLoad(gameObject);   
    }
    public static void SortList()
    {
        List<ScoreInfo> sL1 = scoreListStage.OrderBy(x=>x.Score).Reverse().ToList();
        scoreListStage = sL1;
    }
}
