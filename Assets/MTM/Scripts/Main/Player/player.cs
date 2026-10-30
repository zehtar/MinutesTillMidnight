using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class player : MonoBehaviour
{
    public Joystick joystick;
    public Rigidbody2D _rb;
    public float speed;
    public float slowSpeed;
    public float normalSpeed;
    private Vector2 _moveVector;
    public bool flipRight;

    public Animator anim;
    public PlayerDeath plrDeath;
    public GameObject bigBlackImage;
    public FireScript gunFireScript;

    public int health;
    public int numOfHearts;
    public List<Image> hearts;
    public Sprite fullHeart;
    public Sprite whiteHeart;
    public Sprite emptyHeart;
    
    public float timer;
    public float hpRegenTime = 999999f;

    public StatsSaved st;

    public GameObject knockBackPrefab;
    public GameObject playerHitParticles;

    public static event System.Action<player> OnPlayerSpawned;
    
    public PlayerDash playerDash;
    
    public Point gameEnemySpawnerPoint;
    public InfPoint infgameEnemySpawnerPoint;
    
    public PlayerLowHpEffect lowHpScript;

    void Awake()
    {
        OnPlayerSpawned?.Invoke(this);
    }

    void Start()
    {
        st = FindObjectOfType<StatsSaved>();
        lowHpScript = FindObjectOfType<PlayerLowHpEffect>();
        gunFireScript = FindObjectOfType<FireScript>();
        plrDeath = FindObjectOfType<PlayerDeath>();
        playerDash = FindObjectOfType<PlayerDash>();
        numOfHearts = (int)st.playerhealth;
        health = numOfHearts;
        speed = st.playerspeed;
        _rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        gameEnemySpawnerPoint = FindObjectOfType<Point>();
        slowSpeed = speed--;
        normalSpeed = speed;
        timer = 0f;
        hpRegenTime = st.regenerationRate;
    }

    void Update()
    { 
        if(_moveVector.x > 0 && flipRight) Flip();
        else if (_moveVector.x < 0 && !flipRight) Flip();
        timer += Time.deltaTime;

        if(timer >= hpRegenTime)
        {
            timer = 0;
            if (hearts.Count > health)
            {
                health++;
            }
        }
    }

    void FixedUpdate()
    {
        if(st.controlType == ControlType.PC)
        {
            anim.SetFloat("MoveX", Mathf.Abs(Input.GetAxisRaw("Horizontal")));
            anim.SetFloat("MoveY", Mathf.Abs(Input.GetAxisRaw("Vertical")));
            _moveVector.x = Input.GetAxis("Horizontal");
            _moveVector.y = Input.GetAxis("Vertical");
        }
        else if(st.controlType == ControlType.Android)
        {
            anim.SetFloat("MoveX", Mathf.Abs(joystick.Horizontal));
            anim.SetFloat("MoveY", Mathf.Abs(joystick.Vertical));
            _moveVector.x = joystick.Horizontal;
            _moveVector.y = joystick.Vertical;
        }

        if (playerDash.isDashing == false)
        {
            if(_moveVector.x != 0 && _moveVector.y != 0)
            {
                speed = normalSpeed;
            }
            else 
            {
                speed = slowSpeed;
            }
        }

        _rb.MovePosition(_rb.position + _moveVector * speed * Time.deltaTime);

        HeartCheckOfNum();
        /*
        for(int i = 0; i< hearts.Length; i++)
        {
            if(i<Mathf.RoundToInt(health)) hearts[i].sprite = fullHeart;
            //else( функция с всякими та мээфектами красивыми)
            if(i<numOfHearts) hearts[i].enabled = true;
            else hearts[i].enabled = false;
        }
        */
        if (health < 3 && health >= 1)
        {
            lowHpScript.UpdateVignette(0.1f,true);
        } 
        else if (health == 3)
        {
            lowHpScript.UpdateVignette(0.12f,false);
        }
        else 
        {
            lowHpScript.UpdateVignette(1f);
        }

        if(health < 1)
        {
            lowHpScript.UpdateVignette(1f);
            bigBlackImage.SetActive(true);
            if(plrDeath == null)
            {                
                plrDeath = FindObjectOfType<PlayerDeath>();
            }
            plrDeath.TriggerDeathScreen();
            
            if(gunFireScript == null)
            {
                gunFireScript = FindObjectOfType<FireScript>();
            }
            gunFireScript.canfire = false;
            
            if(gameEnemySpawnerPoint == null)
            {                
                gameEnemySpawnerPoint = FindObjectOfType<Point>();
            }

            if (gameEnemySpawnerPoint == null)
            {
                infgameEnemySpawnerPoint = FindObjectOfType<InfPoint>();
                infgameEnemySpawnerPoint.canSpawn = false;
            }
            else
            {
                gameEnemySpawnerPoint.canSpawn = false;
                for (int i = 0; i < gameEnemySpawnerPoint.enemyAtGameList.Count; i++)
                {
                    gameEnemySpawnerPoint.enemyAtGameList[i].speed = 0f;
                }
            }
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
    }

    public void PlayerRevive()
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            hearts[i].sprite = fullHeart;
            health = hearts.Count;
            Instantiate(knockBackPrefab, transform.position, Quaternion.identity); 
        }
        
        plrDeath.TriggerReviveScreen();
        DeathPanelButtonAnim dpba = plrDeath.deathPanel.GetComponentInChildren<DeathPanelButtonAnim>();
        dpba.UnActiveButtons();
        bigBlackImage.SetActive(false);
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        gunFireScript.canfire = true;
        gameEnemySpawnerPoint.canSpawn = true;
    }

    private void Flip()
    {
        flipRight = ! flipRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void TakeDamage(int damage)
    {
        AudioManager.Instance.PlayHit();
        
        for (int i = 0; i < damage; i++)
        {
            health -= 1;
            HeartRemove();
            if (health >= 0) 
            { 
                Instantiate(knockBackPrefab, transform.position, Quaternion.identity); 
                FindObjectOfType<CameraMovement>().Shake(0.5f, 8f); 
                Instantiate(playerHitParticles, transform.position, Quaternion.identity);
            }
        }
    }
    public void HeartAdd()
    {
        if (health < numOfHearts)
        {
            health++;
            hearts[health - 1].sprite = fullHeart;
            
            
        }
    }

    // Убирает сердце (уменьшает здоровье и запускает эффект)
    public void HeartRemove()
    {
        if (health >= 0)
        {
            Image heart = hearts[health];
            heart.sprite = whiteHeart;
            Instantiate(playerHitParticles, heart.transform.position, Quaternion.identity);
            StartCoroutine(EmptyHeartAfterDelay(heart, 1f));
            
           
        }
    }

    private IEnumerator EmptyHeartAfterDelay(Image heart, float delay)
    {
        yield return new WaitForSeconds(delay);
        heart.sprite = emptyHeart;
    }
    
    public void HeartCheckOfNum()
    {
        while (hearts.Count < numOfHearts)
        {
            Image clone = Instantiate(hearts[0], hearts[0].transform.parent);
            hearts.Add(clone);
        }
        
        while (hearts.Count > numOfHearts)
        {
            Destroy(hearts[hearts.Count - 1].gameObject);
            hearts.RemoveAt(hearts.Count - 1);
        }
    }
    
    public void puSpeed(float speedBuff = 0.5f)
    {
        speed += speedBuff;
        slowSpeed += speedBuff;
        normalSpeed += speedBuff;
    }

    public void puMaksHpAdd(int hpBuff = 1)
    {
        health += hpBuff;
        numOfHearts += (int)hpBuff;
    }

    public void puHpAdd()
    {
        health++;
    }

    public void puHealAdd(float healBuff = 0.05f)
    {
        hpRegenTime = 60;
    }
}
