using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof (Rigidbody2D))]
public class PlayerMotor : MonoBehaviour {

	//general game physics of movement
	[SerializeField] bool isGrounded = false;
	[SerializeField] bool isJumping = false;
	[SerializeField] bool enemyContact = false;
	[SerializeField] bool isCrouching = false;
	[SerializeField] bool isGameRunning = false;
	[SerializeField] bool isGameOver = false;

	[SerializeField] bool isVulnerable = true;
	[SerializeField] bool direction = true;

	[SerializeField] float jumpForce = 6f;
	[SerializeField] float speedMultiplier = 1.0f;
	[SerializeField] float maxSpeed = 10f;
	[SerializeField] float movementX, movementY;
	[SerializeField] float hitPoints = 50f;

	[SerializeField] Rigidbody2D playerBody;
	[SerializeField] GroundDetect gd;
	[SerializeField] PlayerDamageEnemy dmgScript;

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

	// Use this for initialization
	void Start () 
	{
		playerBody = GetComponent<Rigidbody2D>();
		gd = GetComponentInChildren<GroundDetect>();
		dmgScript = GetComponentInChildren<PlayerDamageEnemy>();
	}

	public void Move (Vector2 _input)
	{
		movementX = _input.x;
		movementY = _input.y;
	}
	// Update is called once per frame
	void Update () 
	{
		isGrounded = gd.isGrounded;
		if (isGrounded) 
			{ isJumping = false; }

		enemyContact = dmgScript.playerContact;
		if (enemyContact)
		{
			TakeDamage();
		}
			
		if (!isReloading) 
		{
			if (movementX !=0)
			{
				playerBody.AddForce(new Vector2 (movementX*15f,0f) );
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
			speedMultiplier = 1.5f;
		}
		else if ( playerBody.velocity.x <= -maxSpeed )
		{
			playerBody.velocity = new Vector2 (-maxSpeed, playerBody.velocity.y);
			speedMultiplier = 1.5f;
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
	public void Crouch ()
	{
		if (isGrounded)
		{
			isCrouching = true;
			// later will use transform to reescale the player
		}
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
			playerBody.AddForce(throwFromEnemy * 100);
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
		Destroy(gameObject);
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
		isReloading = true;
		//animator.SetBool("Reloading", true);
		yield return new WaitForSeconds(reloadTime);
		
		//animator.SetBool("Reloading", false);
		currentAmmo = maxAmmo;
		isReloading = false;
	}

	IEnumerator InvulnerableTime ()
	{
		yield return new WaitForSeconds(invulnerableTime);
		isVulnerable = true;
	}
}
