using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int curStage;
    public GameObject[] spawnPoints;
    public GameObject[] enemies;
    public GameObject[] enemies2;
    public GameObject player;

    public GameObject bossHp;

    public GameObject GameEnd;
    public Text[] scoreTexts;
    public Text[] nameTexts;

    public InputField inputField;
    public GameObject AskName;

    public Text ResultText;
    private bool isEnd;

    public Image[] life;
    public Image GasImage;
    public Text BombText;
    Player p;
    
    public Text scoreText;
    public static bool KBossMClear;
    public static bool KBossLClear;
    public static bool KBossMStart;
    public static bool KBossLStart;

    public static bool NBossMClear;
    public static bool NBossLClear;
    public static bool NBossMStart;
    public static bool NBossLStart;
    public static bool EnterStage2;
    public Text stageText;

    private void Start()
    {
        EnterStage2 = false;
           curStage = 1;
        KBossMClear = false;
        KBossLClear = false;
        KBossMStart = false;
        KBossLStart = false;
        NBossMClear = false;
        NBossLClear = false;
        NBossMStart = false;
        NBossLStart = false;
        p = player.GetComponent<Player>();
        StartCoroutine(Start__());
        
        
    }
    
    private void Update()
    {
        scoreText.text = string.Format("{0:n0}",p.score);
        UpdateLife();
        GMSys();
        if(EnterStage2 == true)
        {
            EnterStage2 = false;
            StartCoroutine(Start__());
        }
        if (bossHp.activeSelf)
        {
            if (!KBossMClear || (!NBossMClear&& KBossMClear && KBossLClear))
            {
                Enemy e = GameObject.FindGameObjectWithTag("MBoss").GetComponent<Enemy>();
                Image i = bossHp.transform.GetChild(1).GetComponent<Image>();
                i.fillAmount = ((float)e.hp) / (curStage == 1 ? 2000f : 7000f);
                if (i.fillAmount == 0)
                {
                    bossHp.SetActive(false);
                    p.playBosstime = 0;
                }
            }
            else if (KBossMClear||NBossMClear)
            {
                Enemy e = GameObject.FindGameObjectWithTag("LBoss").GetComponent<Enemy>();
                Image i = bossHp.transform.GetChild(1).GetComponent<Image>();
                i.fillAmount = ((float)e.hp) / (curStage == 1 ? 5000f : 10000f);
                if (i.fillAmount == 0)
                {
                    bossHp.SetActive(false);
                    p.playBosstime = 0;
                }
            }
        }
        if((p.playBosstime >= 40f && !KBossMStart && !KBossMClear) 
            || (p.playBosstime >= 40f && KBossLClear  && !NBossMStart && !NBossLClear&&!NBossMClear))
        {
            if (curStage == 1)
            {
                KBossMStart = true;
            }
            else
            {
                NBossMStart = true;
            }
            StartCoroutine(StartMBoss());
        }
        if ((p.playBosstime >= 40f && !KBossLStart && !KBossLClear && KBossMClear) 
            ||(p.playBosstime >= 40f && !NBossLStart && !NBossLClear && NBossMClear))
        {
            if(curStage == 1)
            {
                KBossLStart = true;
            }
            else
            {
                NBossLStart = true;
            }
            StartCoroutine(StartLBoss());
        }
        if (!isEnd)
        {
            if (!player.activeSelf)
            {
                isEnd = true;
                ResultText.text = "GAMEOVER";
                StartCoroutine(Lose());

            }
            else if (player.activeSelf && NBossLClear)
            {
                isEnd = true;
                ResultText.text = "CLEAR";
                AskName.SetActive(true);
            }
            
        }
    }
    private IEnumerator Start__()
    {
        stageText.gameObject.SetActive(true);
        stageText.text = curStage == 1 ? "Stage 1" : "Stage 2";
        yield return new WaitForSeconds(2f);
        stageText.gameObject.SetActive(false);
        if (curStage == 1)
        {
            StartCoroutine("Spawn");
            StartCoroutine("Spawn_");
            StartCoroutine("Spawn__");
            StartCoroutine("SpawnMeteor");
        }

    }
    private IEnumerator SpawnMeteor()
    {
        yield return new WaitForSeconds(Random.Range(8f, 10f));
        if (!KBossLStart && !KBossMStart && !NBossLStart && !NBossMStart && !isEnd)
        {
            GameObject e = Instantiate(enemies[7], spawnPoints[2].transform.position, Quaternion.identity);
            Meteor eL = e.GetComponent<Meteor>();
            eL.player = player;
        }


        StartCoroutine("SpawnMeteor");
    }
    public void BackToPickStage()
    {
        SceneManager.LoadScene("PickStage");
    }
    public void ReTry()
    {
        SceneManager.LoadScene("Stage 1");
    }
    private IEnumerator Lose()
    {
        transform.GetChild(0).GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(1f);
        
        GameEnd.SetActive(true);
        ResultText.text = "Gameover";
    }
    
    private IEnumerator Clear(string name)
    {
       
        ScoreInfo s = new ScoreInfo();
        s.Score = p.score;
        s.name = name == null ? "Unknown" : name;
        if (SceneManager.GetActiveScene().name == "Stage 1")
        {
            Ranking.scoreListStage.Add(s);
            Ranking.SortList();
            for (int i = 0; i < Ranking.scoreListStage.Count; i++)
            {
                scoreTexts[i].text = Ranking.scoreListStage[i].Score.ToString();
                nameTexts[i].text = Ranking.scoreListStage[i].name.ToString();
            }
        }
        
        yield return new WaitForSeconds(1f);
        GameEnd.SetActive(true);
        ResultText.text = "Clear";
    }
    private IEnumerator StartLBoss()
    {
        
        Quaternion rot = Quaternion.Euler(0, 0, 180);
        yield return new WaitForSeconds(Random.Range(1f, 3f));
        GameObject sE = curStage == 1 ? enemies[6] : enemies2[6];
        GameObject e = Instantiate(sE, spawnPoints[11].transform.position, rot);
        bossHp.SetActive(true);
        Enemy eL = e.GetComponent<Enemy>();
        Rigidbody2D r = e.GetComponent<Rigidbody2D>();
        r.AddForce(Vector2.down * eL.speed, ForceMode2D.Impulse);
        eL.player = player;
        StartCoroutine(StopBoss(r));
    }
    private IEnumerator StartMBoss()
    {
        Quaternion rot = Quaternion.Euler(0, 0, 180);
        yield return new WaitForSeconds(Random.Range(1f, 3f));
        GameObject sE = curStage == 1 ? enemies[5] : enemies2[5];
        GameObject e = Instantiate(sE, spawnPoints[11].transform.position, rot);
        bossHp.SetActive(true);
        Enemy eL = e.GetComponent<Enemy>();
        Rigidbody2D r = e.GetComponent<Rigidbody2D>();
        r.AddForce(Vector2.down * eL.speed, ForceMode2D.Impulse);
        eL.player = player;
        StartCoroutine(StopBoss(r));
    }
    private IEnumerator StopBoss (Rigidbody2D rigid)
    {
        yield return new WaitForSeconds(2.5f);
        rigid.velocity = Vector2.zero;
    }
    private IEnumerator Spawn()
    {
        Quaternion rot = Quaternion.Euler(0, 0, 180);
        yield return new WaitForSeconds(Random.Range(1f,3f));
        if(!KBossLClear  && !KBossLStart && !KBossMStart && curStage == 1)
        {
            int rS = Random.Range(0, 3);
            int eS = Random.Range(0, 5);
            GameObject e = Instantiate(enemies[rS], spawnPoints[eS].transform.position, rot);
            Enemy eL = e.GetComponent<Enemy>();
            eL.nextPos = (Vector2.down).normalized;
            eL.player = player;
            if (KBossMClear && KBossLClear && NBossMClear)
            {
                eL.hp = (int)(eL.hp * 4f);
                eL.enemyScore *= 2;
            }
            else if (KBossMClear && KBossLClear)
            {
                eL.hp = (int)(eL.hp * 3f);
                eL.enemyScore *= 2;
            }
            else if (KBossMClear)
            {
                eL.hp = (int)(eL.hp * 2f);
                eL.enemyScore *= 2;
            }
        }
        else if (!NBossLClear && !NBossLStart && !NBossMStart && curStage == 2)
        {
            int rS = Random.Range(0, 3);
            int eS = Random.Range(0, 5);
            GameObject e = Instantiate(enemies2[rS], spawnPoints[eS].transform.position, rot);
            Enemy eL = e.GetComponent<Enemy>();
            eL.nextPos = (Vector2.down).normalized;
            eL.player = player;
            if (KBossMClear && KBossLClear && NBossMClear)
            {
                eL.hp = (int)(eL.hp * 4f);
                eL.enemyScore *= 2;
            }
            else if (KBossMClear && KBossLClear)
            {
                eL.hp = (int)(eL.hp * 3f);
                eL.enemyScore *= 2;
            }
            else if (KBossMClear)
            {
                eL.hp = (int)(eL.hp * 2f);
                eL.enemyScore *= 2;
            }
        }
        StartCoroutine("Spawn");
    }
    private IEnumerator Spawn_()
    {
        Quaternion rot = Quaternion.Euler(0, 0, 180);
        yield return new WaitForSeconds(Random.Range(3, 5f));
        if (!KBossLClear && !KBossMStart && !KBossLStart && curStage == 1)
        {

            int rS = Random.Range(5, 10);
            int eS = Random.Range(2, 4);
            GameObject e = Instantiate(enemies[eS], spawnPoints[rS].transform.position, rot);
            Enemy eL = e.GetComponent<Enemy>();

            if (rS <= 7)
            {
                eL.nextPos = (Vector2.down + (Vector2.left * 2f)).normalized;
            }
            else
            {
                eL.nextPos = (Vector2.down + (Vector2.right * 2f)).normalized;
            }

            eL.player = player; 
            if (KBossMClear && KBossLClear && NBossMClear)
            {
                eL.hp = (int)(eL.hp * 4f);
                eL.enemyScore *= 2;
            }
            else if (KBossMClear && KBossLClear)
            {
                eL.hp = (int)(eL.hp * 3f);
                eL.enemyScore *= 2;
            }
            else if (KBossMClear)
            {
                eL.hp = (int)(eL.hp * 2f);
                eL.enemyScore *= 2;
            }
        }
        else if (!NBossLClear && !NBossMStart && !NBossLStart && curStage == 2)
        {

            int rS = Random.Range(5, 10);
            int eS = Random.Range(2, 4);
            GameObject e = Instantiate(enemies2[eS], spawnPoints[rS].transform.position, rot);
            Enemy eL = e.GetComponent<Enemy>();
            eL.player = player;
            if (rS <= 7)
            {
                eL.nextPos = (Vector2.down + (Vector2.left * 2f)).normalized;
            }
            else
            {
                eL.nextPos = (Vector2.down + (Vector2.right * 2f)).normalized;
            }

            if (KBossMClear && KBossLClear && NBossMClear)
            {
                eL.hp = (int)(eL.hp * 4f);
                eL.enemyScore *= 2;
            }
            else if (KBossMClear && KBossLClear)
            {
                eL.hp = (int)(eL.hp * 3f);
                eL.enemyScore *= 2;
            }
            else if (KBossMClear)
            {
                eL.hp = (int)(eL.hp * 2f);
                eL.enemyScore *= 2;
            }
        }
        StartCoroutine("Spawn_");
    }
    private IEnumerator Spawn__()
    {
        yield return new WaitForSeconds(Random.Range(4f,5f));
        Quaternion rot = Quaternion.Euler(0, 0, 180);
        if (!KBossLClear && !KBossMStart && !KBossLStart&& curStage == 1)
        {
            GameObject e = Instantiate(enemies[4], spawnPoints[Random.Range(0, 2)].transform.position, rot);
            Enemy eL = e.GetComponent<Enemy>();
            eL.nextPos = (Vector2.down).normalized;

            eL.player = player;
            if (KBossMClear && KBossLClear && NBossMClear)
            {
                eL.hp = (int)(eL.hp * 4f);
                eL.enemyScore *= 4;
            }
            else if (KBossMClear && KBossLClear)
            {
                eL.hp = (int)(eL.hp * 3f);
                eL.enemyScore *= 3;
            }
            else if (KBossMClear)
            {
                eL.hp = (int)(eL.hp * 2f);
                eL.enemyScore *= 2;
            }
        }
        else if(!NBossLClear && !NBossMStart && !NBossLStart && curStage == 2)
        {
            GameObject e = Instantiate(enemies2[4], spawnPoints[Random.Range(0, 2)].transform.position, rot);
            Enemy eL = e.GetComponent<Enemy>();
            eL.nextPos = (Vector2.down).normalized;

            eL.player = player;
            if (KBossMClear && KBossLClear && NBossMClear)
            {
                eL.hp = (int)(eL.hp * 4f);
                eL.enemyScore *= 4;
            }
            else if (KBossMClear && KBossLClear)
            {
                eL.hp = (int)(eL.hp * 3f);
                eL.enemyScore *= 3;
            }
            else if (KBossMClear) 
            {
                eL.hp = (int)(eL.hp * 2f);
                eL.enemyScore *= 2;
            }
        }
        StartCoroutine("Spawn__");
    }

    public void UpdateLife()
    {
        BombText.text = "x" + p.bombs;
        GasImage.fillAmount = p.Gas / 100f;
        if (p.Gas <= 0f) p.hp = 0;
        for(int i = 0; i < 3; i++)
        {
            life[i].color = new Color(1, 1, 1, 0);
        }
        for (int i = 0; i < p.hp; i++)
        {
            life[i].color = new Color(1, 1, 1, 1);
        }
    }
    
    public void GMSys()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            GameObject[] bullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
            foreach (GameObject g in bullets)
            {
                Destroy(g);
            }
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject e in enemies)
            {
                Enemy eL = e.GetComponent<Enemy>();
                eL.hp -= 1000;
            }
            if (KBossLStart || NBossLStart)
            {
                GameObject e = GameObject.FindGameObjectWithTag("LBoss");
                Enemy eL = e.GetComponent<Enemy>();
                eL.hp -= 10000;
            }
            if (KBossMStart || NBossMStart)
            {
                GameObject e = GameObject.FindGameObjectWithTag("MBoss");
                Enemy eL = e.GetComponent<Enemy>();
                eL.hp -= 10000;
            }
        }
        if (Input.GetKeyDown(KeyCode.F2)) {
            p.bulletLvl = 4;
        }
        if (Input.GetKeyDown(KeyCode.F3)){
            p.skillHpCoolTime = 0f;
        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            p.hp = 3;
        }
        if (Input.GetKeyDown(KeyCode.F5))
        {
            p.Gas = 100f;
        }
        if (Input.GetKeyDown(KeyCode.F6))
        {
            if(curStage == 1)
            {
                KBossMClear = true;
                KBossLClear = true;
                NBossMClear = false;
                NBossLClear = false;
                curStage = 2;
                p.playBosstime = 0f;
            }
            else
            {
                KBossMClear = false;
                KBossLClear = false;
                NBossMClear = false;
                NBossLClear = false;
                curStage = 1;
                p.playBosstime = 0f;
            }
        }
    }
    public void EnterName(bool yes)
    {
        if (yes)
        {
            string playerName = inputField.text;
            AskName.SetActive(false);
            StartCoroutine(Clear(playerName));
        }
        else
        {
            AskName.SetActive(false);
            StartCoroutine(Clear(null));
        }
        
    }
}
