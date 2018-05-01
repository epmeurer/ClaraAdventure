using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof (Rigidbody2D))]
public class PlayerMotor : MonoBehaviour {

	//general game physics of movement
	bool isGrounded = false;
	bool isJumping = false;
	bool enemyContact = false;
	bool coinCatch = false;
	bool lifeCatch = false;
	bool isCrouching = false;
	bool isGameRunning = false;
	bool isGameOver = false;
	bool isVulnerable = true;
	bool direction = true;

	[SerializeField] float jumpForce = 9f, walkImpulse = 30f;
	[SerializeField] float speedMultiplier = 1.0f;
	[SerializeField] float maxSpeed = 8f;
	float movementX, movementY;
	float maxHitPoints = 50f;
	float hitPoints;
	[SerializeField] int score = 0;
	[SerializeField] int lifes = 3;

	[SerializeField] Rigidbody2D playerBody;
	[SerializeField] GroundDetect gd;
	[SerializeField] PlayerDamageEnemy dmgScript;
	[SerializeField] PlayerScoreIncrement scoreScript;
	[SerializeField] GameObject idleAnim;
	[SerializeField] GameObject runAnim;

	public Vector3 lastRespawnPoint;

	//gun
	public float fireRate = 5f;

	public int maxAmmo = 10;
	private int currentAmmo = 10;
	public float reloadTime = 1f;
	public float invulnerableTime = 0.5f;

	private float nextTimeToFire = 0f;
	[SerializeField] bool isReloading = false;
	
	[SerializeField] GameObject firePoint;
	[SerializeField] GameObject bullet;
	[SerializeField] CameraFollow cameraScript;

	// Use this for initialization
	void Start () 
	{
		playerBody = GetComponent<Rigidbody2D>();
		gd = GetComponentInChildren<GroundDetect>();
		dmgScript = GetComponentInChildren<PlayerDamageEnemy>();
		scoreScript = GetComponentInChildren<PlayerScoreIncrement>();
		cameraScript = GetComponent<CameraFollow>();

		hitPoints = maxHitPoints;
		lastRespawnPoint = playerBody.position;
	}

	public void Move (Vector2 _input)
	{
		movementX = _input.x;
		movementY = _input.y;
	}
	// Update is called once per frame
	void Update () 
	{
		if (movementX!=0)
			{
				idleAnim.SetActive(false);
				runAnim.SetActive(true);
			}
		else
			{
				idleAnim.SetActive(true);
				runAnim.SetActive(false);
			}
		isGrounded = gd.isGrounded;
		if (isGrounded) 
			{ isJumping = false; }

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
		lifeCatch = scoreScript.catchLife;
		if (lifeCatch)
		{
			CatchHeart();
		}	
		
		if (!isReloading) 
		{
			if (movementX !=0)
			{
				playerBody.AddForce(new Vector2 (movementX*walkImpulse,0f) );
			}
			else
			{
				if (playerBody.velocity.x != 0 && isVulnerable && !isGrounded)
				{
					playerBody.AddForce(new Vector2 (-playerBody.velocity.x*5,0f));
				}
			}
		}
		else playerBody.velocity = Vector2.zero;

		if ( playerBody.velocity.x >= maxSpeed )
		{
			playerBody.velocity = new Vector2 (maxSpeed, playerBody.velocity.y);
			speedMultiplier = 1.25f;
		}
		else if ( playerBody.velocity.x <= -maxSpeed )
		{
			playerBody.velocity = new Vector2 (-maxSpeed, playerBody.velocity.y);
			speedMultiplier = 1.25f;
		}
		else 
		{
			speedMultiplier = 1.0f;
		}

		//turn the player's facing direction
		if (playerBody.velocity.x > 0)
		{
			this.gameObject.transform.localScale = new Vector3 (1,1,1);
			direction = true;
		}
		else if (playerBody.velocity.x < 0)
		{
			this.gameObject.transform.localScale = new Vector3 (-1,1,1);
			direction = false;
		}
	}
	public void Jump ()
	{
		if (isGrounded && !isJumping)
		{
			isJumping = true;
			playerBody.velocity = new Vector2(playerBody.velocity.x,jumpForce*speedMultiplier);
		}
	}
	public void ScoreIncrease ()
	{
		score++;
		coinCatch = false;
		Destroy(scoreScript.collectedObject);
	}
	public void CatchHeart ()
	{
		lifes++;
		lifeCatch = false;
		Destroy(scoreScript.collectedObject);
	}
	public void Shoot (bool _isShooting)
	{
		if (isReloading)
			{ return; }
		if (Input.GetKeyDown(KeyCode.R) && !isJumping)
			{ StartCoroutine( Reload() ); }
		if (currentAmmo <=0)
			{ return; } // if there is no ammo the update just stops

		if (_isShooting && Time.time > nextTimeToFire)
        {
			nextTimeToFire = Time.time + 1/fireRate;
			Fire();
		}
	}
	void TakeDamage ()
	{
		float damageTaken;
		Vector2 throwFromEnemy;

		throwFromEnemy = playerBody.position - dmgScript.enemyPos;
		damageTaken = dmgScript.damageDealt;

		if (!isVulnerable)
			{ StartCoroutine( InvulnerableTime() ); }
		else
		{
			playerBody.velocity = Vector2.zero;
			playerBody.AddForce(throwFromEnemy * 400);
			hitPoints -= damageTaken;
			isVulnerable = false;
		}

		if (hitPoints <=0f)
		{
			Die();
		}
	}
	void Die()
	{
		hitPoints = maxHitPoints;
		playerBody.velocity = Vector3.zero;
		playerBody.position = lastRespawnPoint;
		lifes--;
		cameraScript.FocusOnPlayer();

		if (lifes <=0)
		{
			Destroy(gameObject);
		}

	}
	void Fire ()
	{
		currentAmmo--;
		Vector2 pos;
		pos = firePoint.transform.position;

		GameObject newbullet = Instantiate(bullet, pos, Quaternion.Euler(0,0,0));
		newbullet.GetComponent<Bullet>().direction = direction;
	}
	IEnumerator Reload ()
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

	IEnumerator InvulnerableTime ()
	{
		yield return new WaitForSeconds(invulnerableTime);
		isVulnerable = true;
	}
}
