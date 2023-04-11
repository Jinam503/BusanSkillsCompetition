using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    private bool touchL;
    private bool touchR;
    private bool touchT;
    private bool touchB;

    private float h;
    private float v;
    public float moveSpeed;

    public GameObject playerBullet1;
    public GameObject playerBullet2;
    public GameObject playerBullet3; 
    public GameObject playerBullet4;
    public GameObject playerBullet5;

    public float maxFireDelay;
    private float curFireDelay = 0f;

    public int bulletLvl;
    public int hp;
    public int score;
    public float playTime;
    public float playBosstime;
    public float Gas;
    public int bombs;

    public float skillHpCoolTime;
    public Text hptext;
    public Image hpimage;
    public Image boomPanel;
    public Text cantUseSkillText;
    private bool isShield;

    private bool canDamage;
    public bool isSubSkill;
    public Text isSubSkillText;
    private SpriteRenderer sr;
    void Start()
    {
        isSubSkill = false;
        skillHpCoolTime = 10f;
        bombs = 0;
        canDamage = true;
        score = 0;
        playTime =0f ;
        Gas = 100f;
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayTimeIc();
        Inputs();
        Move();
        Fire();
        GasDc();
        Skills();
        if (hp <= 0)
        {
            gameObject.SetActive(false);
            return;
        }
    }
    public void Skills()
    {
        skillHpCoolTime -= Time.deltaTime;
        int a = ((int)skillHpCoolTime);
        if (a < 0) hptext.text = "";
        else hptext.text = a.ToString();
        hpimage.color = new Color(1, 0.7f, 0.7f, 0.5f);
        if (skillHpCoolTime < 0f)
        {
            hpimage.color = new Color(1, 0.7f, 0.7f, 1f);
            if (Input.GetKeyDown(KeyCode.E))
            {
                if(hp !=3)hp++;
                skillHpCoolTime = 10f;
                transform.GetChild(0).GetChild(4).gameObject.GetComponent<AudioSource>().Play();
                
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                transform.GetChild(0).GetChild(5).gameObject.GetComponent<AudioSource>().Play();
                StopCoroutine("CantUseSkill");
                StartCoroutine("CantUseSkill");
            }
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            if(bombs > 0)
            {
                bombs--;
                transform.GetChild(0).GetChild(6).gameObject.GetComponent<AudioSource>().Play();
                StopCoroutine("Boom");
                StartCoroutine("Boom");
                GameObject[] bullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
                foreach(GameObject g in bullets)
                {
                    Destroy(g);
                }
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                foreach (GameObject e in enemies)
                {
                    Enemy eL = e.GetComponent<Enemy>();
                    eL.hp -= 1000;
                }
                if(GameManager.KBossLStart || GameManager.NBossLStart)
                {
                    GameObject e = GameObject.FindGameObjectWithTag("LBoss");
                    Enemy eL = e.GetComponent<Enemy>();
                    eL.hp -= 2000;
                }
                if (GameManager.KBossMStart || GameManager.NBossMStart)
                {
                    GameObject e = GameObject.FindGameObjectWithTag("MBoss");
                    Enemy eL = e.GetComponent<Enemy>();
                    eL.hp -= 2000;
                }
            }
            else
            {
                transform.GetChild(0).GetChild(5).gameObject.GetComponent<AudioSource>().Play();
                StopCoroutine("CantUseSkill");
                StartCoroutine("CantUseSkill");
            }
        }
    }
    private IEnumerator Boom()
    {
        boomPanel.gameObject.SetActive(true);
        boomPanel.color = new Color(0, 0, 0, 0.5f);
        yield return new WaitForSeconds(1f);
        float a = 0.5f;
        for(int i = 0; i < 25; i++)
        {
            yield return new WaitForSeconds(0.1f);
            a -= 0.2f;
            boomPanel.color = new Color(0, 0, 0, a);
        }
        boomPanel.gameObject.SetActive(false);
    }
    private IEnumerator CantUseSkill()
    {
        cantUseSkillText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        cantUseSkillText.gameObject.SetActive(false);
    }
    private void GasDc()
    {
        if (gameObject.activeSelf)
        {
            if (isSubSkill)
            {
                Gas -= Time.deltaTime * 10;
            }
            else Gas -= Time.deltaTime * 2;
        }
        if(Gas < 0f)
        {
            hp = 0;
        }
    }
    private void PlayTimeIc()
    {
        if (gameObject.activeSelf)
        {
            playTime += Time.deltaTime;
            playBosstime += Time.deltaTime;
        }
    }
    private void Fire()
    {
        Delay();
        if (curFireDelay < maxFireDelay) return;
        Quaternion rot = Quaternion.Euler(0, 0, 90);
        switch (bulletLvl)
        {
            
            case 1:
                GameObject b = Instantiate(playerBullet1, transform.position, rot);
                
                Rigidbody2D r = b.GetComponent<Rigidbody2D>();
                r.AddForce(Vector2.up * 20f, ForceMode2D.Impulse);
                curFireDelay = 0f;
                break;
            case 2:
                GameObject b1 = Instantiate(playerBullet2, transform.position, rot);

                Rigidbody2D r1 = b1.GetComponent<Rigidbody2D>();
                r1.AddForce(Vector2.up * 20f, ForceMode2D.Impulse);
                curFireDelay = 0f;
                break;
            case 3:
                GameObject b2 = Instantiate(playerBullet3, transform.position, rot);

                Rigidbody2D r2 = b2.GetComponent<Rigidbody2D>();
                r2.AddForce(Vector2.up * 20f, ForceMode2D.Impulse);
                curFireDelay = 0f;
                break;
            case 4:
                GameObject b3 = Instantiate(playerBullet4, transform.position, rot);

                Rigidbody2D r3 = b3.GetComponent<Rigidbody2D>();
                r3.AddForce(Vector2.up * 20f, ForceMode2D.Impulse);
                curFireDelay = 0f;
                break;
        }
        if (isSubSkill)
        {
            GameObject b3 = Instantiate(playerBullet5, transform.position + (Vector3.right * 1f), rot);

            Rigidbody2D r3 = b3.GetComponent<Rigidbody2D>();
            r3.AddForce(Vector2.up * 20f, ForceMode2D.Impulse);
            GameObject b4 = Instantiate(playerBullet5, transform.position + (Vector3.left * 1f), rot);

            Rigidbody2D r4 = b4.GetComponent<Rigidbody2D>();
            r4.AddForce(Vector2.up * 20f, ForceMode2D.Impulse);
        }
    }
    private void Delay()
    {
        curFireDelay += Time.deltaTime;
    }
    private void Move()
    {
        Vector3 nextPos = new Vector3(h, v, 0);
        transform.position = transform.position + nextPos * moveSpeed *Time.deltaTime;
    }
    private void Inputs()
    {
        isSubSkillText.text = isSubSkill == true ? "On" : "Off";
        h = Input.GetAxisRaw("Horizontal");
        if ((touchL && h == -1)|| (touchR && h == 1)) h = 0;
        v = Input.GetAxisRaw("Vertical");
        if ((touchB && v == -1) || (touchT && v == 1)) v = 0;
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isSubSkill = !isSubSkill;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerWall")
        {
            switch (collision.gameObject.name)
            {
                case "L":
                    touchL = true;
                    break;
                case "R":
                    touchR = true;
                    break;
                case "T":
                    touchT = true;
                    break;
                case "B":
                    touchB = true;
                    break;

            }
        }
        else if (collision.gameObject.tag == "EnemyBullet")
        {
            if (!canDamage) return;
            GameObject b = collision.gameObject;
            Destroy(b);
            if (isShield) return;
            transform.GetChild(0).GetChild(3).GetComponent<AudioSource>().Play();
            canDamage = false;
            
            hp--;
            
            StartCoroutine("Return");
            
        }
        else if( collision.gameObject.tag == "Item")
        {
            Item.ItemType it = collision.gameObject.GetComponent<Item>().iT;
            switch (it)
            {
                case Item.ItemType.Gas:
                    score += 100;
                    Destroy(collision.gameObject);
                    transform.GetChild(0).GetChild(2).GetComponent<AudioSource>().Play();
                    Gas += 20;
                    if (Gas > 100) Gas = 100;
                    break;
                case Item.ItemType.Fix:
                    score += 100;
                    Destroy(collision.gameObject);
                    transform.GetChild(0).GetChild(2).GetComponent<AudioSource>().Play();
                    if(hp!=3)hp++;
                    break;
                case Item.ItemType.Coin:
                    Destroy(collision.gameObject);
                    transform.GetChild(0).GetChild(0).GetComponent<AudioSource>().Play();
                    score += 1000;
                    break;
                case Item.ItemType.Shield:
                    score += 100;
                    Destroy(collision.gameObject);
                    transform.GetChild(0).GetChild(2).GetComponent<AudioSource>().Play();
                    StopCoroutine("ShieldOn");
                    StartCoroutine("ShieldOn");
                    break;
                case Item.ItemType.Upgrade:
                    score += 100;
                    Destroy(collision.gameObject);
                    transform.GetChild(0).GetChild(1).GetComponent<AudioSource>().Play();
                    if (bulletLvl != 4) bulletLvl++;
                    break;
                case Item.ItemType.Bomb:
                    score += 100;
                    Destroy(collision.gameObject);
                    transform.GetChild(0).GetChild(2).GetComponent<AudioSource>().Play();
                    if (bombs != 3) bombs++;
                    break;
            }
        }
    }
    private IEnumerator ShieldOn()
    {
        transform.GetChild(1).gameObject.SetActive(true);
        isShield = true;
        yield return new WaitForSeconds(3f);
        transform.GetChild(1).gameObject.SetActive(false);
        isShield = false;
    }
    private IEnumerator Return()
    {
        sr.color = new Color(1, 1, 1, 0.4f);
        yield return new WaitForSeconds(0.5f);
        sr.color = new Color(1, 1, 1, 1f);
        yield return new WaitForSeconds(0.5f);
        sr.color = new Color(1, 1, 1, 0.4f);
        yield return new WaitForSeconds(0.5f);
        sr.color = new Color(1, 1, 1, 1);
        yield return new WaitForSeconds(0.5f);
        sr.color = new Color(1, 1, 1, 0.4f);
        yield return new WaitForSeconds(0.5f);
        sr.color = new Color(1, 1, 1, 1);
        canDamage = true;
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.WakeUp();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerWall")
        {
            switch (collision.gameObject.name)
            {
                case "L":
                    touchL = false;
                    break;
                case "R":
                    touchR = false;
                    break;
                case "T":
                    touchT = false;
                    break;
                case "B":
                    touchB = false;
                    break;

            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy") {
            if (!canDamage) return;
            transform.GetChild(0).GetChild(3).GetComponent<AudioSource>().Play();
            canDamage = false;
            hp--;
            if (hp <= 0)
            {
                gameObject.SetActive(false);
                return;
            }
            StartCoroutine("Return");
        }
    }
}