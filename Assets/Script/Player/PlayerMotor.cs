using System.Collections;
using System.Collections.Generic;
using DragonBones;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMotor : MonoBehaviour
{

    //general game physics of movement
    bool isGrounded = false;
    bool isJumping = false;
    bool enemyContact = false;
    bool coinCatch = false;
    bool lifeCatch = false;
    bool isGameRunning = false;
    bool isGameOver = false;
    bool isVulnerable = true;
    bool direction = true;

    float jumpForce = 25f, walkImpulse = 200f;
    float speedMultiplier = 1.0f;
    float maxSpeed = 8f;
    float movementX, movementY;
    float maxHitPoints = 50f;
    float hitPoints;
    [SerializeField] int score = 0;
    [SerializeField] int lifes = 3;

    [SerializeField] Rigidbody2D playerBody;
    [SerializeField] GroundDetect gd;
    [SerializeField] PlayerDamageEnemy dmgScript;
    [SerializeField] PlayerScoreIncrement scoreScript;
    [SerializeField] UnityArmatureComponent armatureComponent;
    [SerializeField] GameObject idleAnim;
    [SerializeField] Text lifeBoard;
    [SerializeField] Text healthBoard;
    [SerializeField] GameObject SpawnPosition;

    //gun
    public float fireRate = 5f;

    [SerializeField] int maxAmmo = 10;
    [SerializeField] int currentAmmo = 10;
    [SerializeField] float reloadTime = 1f;
    [SerializeField] float invulnerableTime = 1f;

    private float nextTimeToFire = 0f;
    bool isReloading = false;

    [SerializeField] GameObject firePoint;
    [SerializeField] GameObject bullet;
    [SerializeField] CameraFollow cameraScript;


    // Use this for initialization
    void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
        gd = GetComponentInChildren<GroundDetect>();
        dmgScript = GetComponentInChildren<PlayerDamageEnemy>();
        scoreScript = GetComponentInChildren<PlayerScoreIncrement>();
        cameraScript = GetComponent<CameraFollow>();
        armatureComponent = GetComponentInChildren<UnityArmatureComponent>();

        lifeBoard.transform.position = new Vector2(20, Screen.height - 10);
        lifeBoard.text = "Continues: " + lifes;

        BoardsUpdate();
    }
    void BoardsUpdate()
    {

        healthBoard.transform.position = new Vector2(Screen.width - 20, Screen.height - 10);
        healthBoard.text = "Vida: " + hitPoints;
    }
	public void StageClearStats ()
	{
        int a;
        a = PlayerPrefs.GetInt("LEVEL");
        a++;
        PlayerPrefs.SetInt("LEVEL", a);

        PlayerPrefs.SetFloat("HEALTH", hitPoints);
        PlayerPrefs.SetInt("LIFES", lifes);
        PlayerPrefs.SetInt("CURRENTAMMO", currentAmmo);
        PlayerPrefs.SetInt("SCORE", score);
	}
	public void StageLoadStats ()
	{
        hitPoints = PlayerPrefs.GetInt("HEALTH");
        lifes = PlayerPrefs.GetInt("LIFES");
        currentAmmo = PlayerPrefs.GetInt("CURRENTAMMO");
        score = PlayerPrefs.GetInt("SCORE");
	}

    public void Move(Vector2 _input)
    {
        movementX = _input.x;
        movementY = _input.y;
    }
    // Update is called once per frame
    void Update()
    {
        BoardsUpdate();

        if (movementX != 0)
        {
            //idleAnim.SetActive(false);
            //runAnim.SetActive(true);
            if (armatureComponent.animation.lastAnimationName != "Running")
            {
                armatureComponent.animation.FadeIn("Running", 0.25f, -1);
            }
        }
        else
        {
            //idleAnim.SetActive(true);
            //runAnim.SetActive(false);
            if (armatureComponent.animation.lastAnimationName != "Idle")
            {
                armatureComponent.animation.FadeIn("Idle", 0.25f, -1);
            }
        }
        isGrounded = gd.isGrounded;
        if (isGrounded)
        { isJumping = false; }

        lifeCatch = scoreScript.catchLife;
        if (lifeCatch)
        {
            CatchHeart();
        }

        enemyContact = dmgScript.playerContact;
        if (enemyContact)
        {
            TakeDamage();
        }

        coinCatch = scoreScript.catchCoin;
        if (coinCatch)
        {
            ScoreIncrease();
        }

        if (!isReloading)
        {
            if (movementX != 0)
            {
                playerBody.AddForce(new Vector2(movementX * walkImpulse, 0f));
            }
            else
            {
                if (playerBody.velocity.x != 0 && isVulnerable && !isGrounded)
                {
                    playerBody.AddForce(new Vector2(-playerBody.velocity.x * 5, 0f));
                }
            }
        }
        else playerBody.velocity = Vector2.zero;

        if (playerBody.velocity.x >= maxSpeed)
        {
            playerBody.velocity = new Vector2(maxSpeed, playerBody.velocity.y);
            speedMultiplier = 1.25f;
        }
        else if (playerBody.velocity.x <= -maxSpeed)
        {
            playerBody.velocity = new Vector2(-maxSpeed, playerBody.velocity.y);
            speedMultiplier = 1.25f;
        }
        else
        {
            speedMultiplier = 1.0f;
        }

        //turn the player's facing direction
        if (playerBody.velocity.x > 0)
        {
            this.gameObject.transform.localScale = new Vector3(1, 1, 1);
            direction = true;
        }
        else if (playerBody.velocity.x < 0)
        {
            this.gameObject.transform.localScale = new Vector3(-1, 1, 1);
            direction = false;
        }
    }
    public void Jump()
    {
        if (isGrounded && !isJumping)
        {
            isJumping = true;
            playerBody.velocity = new Vector2(playerBody.velocity.x, jumpForce * speedMultiplier);
        }
    }
    public void ScoreIncrease()
    {
        score++;
        coinCatch = false;
        Destroy(scoreScript.collectedObject);
    }
    public void CatchHeart()
    {
        lifes++;
        lifeCatch = false;
        Destroy(scoreScript.collectedObject);
    }

    public void Shoot(bool _isShooting)
    {
        if (isReloading)
        { return; }
        if (Input.GetKeyDown(KeyCode.R) && !isJumping)
        { StartCoroutine(Reload()); }
        if (currentAmmo <= 0)
        { return; } // if there is no ammo the update just stops

        if (_isShooting && Time.time > nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1 / fireRate;
            Fire();
        }
    }
    void TakeDamage()
    {
        float damageTaken;
        Vector2 throwFromEnemy;

        throwFromEnemy = playerBody.position - dmgScript.enemyPos;
        damageTaken = dmgScript.damageDealt;

        if (!isVulnerable)
        { StartCoroutine(InvulnerableTime()); }
        else
        {
            playerBody.velocity = Vector2.zero;
            playerBody.AddForce(throwFromEnemy * jumpForce*15);
            hitPoints -= damageTaken;
            isVulnerable = false;
        }

        if (hitPoints <= 0f)
        {
            Die();
        }
    }
    void Die()
    {
        hitPoints = maxHitPoints;
        playerBody.velocity = Vector3.zero;
        playerBody.position = SpawnPosition.transform.position;
        lifes--;
        cameraScript.FocusOnPlayer();

        if (lifes <= 0)
        {
            Debug.Log("gameover");
            SceneManager.LoadScene("GameOver");
            
        }

    }
    void Fire()
    {
        currentAmmo--;
        Vector2 pos;
        pos = firePoint.transform.position;

        GameObject newbullet = Instantiate(bullet, pos, Quaternion.Euler(0, 0, 0));
        newbullet.GetComponent<Bullet>().direction = direction;
    }
    IEnumerator Reload()
    {
        if (isGrounded)
        {
            isReloading = true;
            //animator.SetBool("Reloading", true);
            yield return new WaitForSeconds(reloadTime);

            //animator.SetBool("Reloading", false);
            currentAmmo = maxAmmo;
            isReloading = false;
        }

    }

    IEnumerator InvulnerableTime()
    {

        yield return new WaitForSeconds(invulnerableTime);

        isVulnerable = true;
    }
}
