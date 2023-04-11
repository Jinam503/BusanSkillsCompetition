using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PickStage : MonoBehaviour
{
    public GameObject StartStage1;
    public GameObject HelpPage;
    public GameObject ExitPage;
    public Text[] scoreTexts1;
    public Text[] nameTexts1;
    public Text[] scoreTexts2;
    public Text[] nameTexts2;
    private void Start()
    {
        for (int i = 0; i < Ranking.scoreListStage.Count; i++)
        {
            scoreTexts1[i].text = Ranking.scoreListStage[i].Score.ToString();
            nameTexts1[i].text = Ranking.scoreListStage[i].name.ToString();
        }
    }
    public void Page(string name)
    {
        switch (name)
        {
            case "page1":
                StartStage1.SetActive(true);
                break;
            case "page1C":
                StartStage1.SetActive(false);
                break;
            case "help":
                HelpPage.SetActive(true);
                break;
            case "helpC":
                HelpPage.SetActive(false);
                break;
            case "ExitC":
                ExitPage.SetActive(false);
                break;
            case "Exit":
                ExitPage.SetActive(true);
                break;
            case "RealExit":
                Application.Quit();
                break;
        }
    }
    public void GoStage(int r)
    {
        if( r== 1)
        {
            SceneManager.LoadScene("Stage 1");
        }
    }
}
