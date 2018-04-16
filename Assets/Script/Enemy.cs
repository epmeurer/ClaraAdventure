using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour 
{
	
	public float health = 50f;
	[SerializeField] GroundDetect gd;
	[SerializeField] EnemyTurnDetect turnAwayFromHole;
	[SerializeField] EnemyWallDetect turnAwayFromWall;
	[SerializeField] EnemyPlayerDetect playerDetect;
	public float damageDealt;
	

	[SerializeField] bool facingLeft = true;
	[SerializeField] float walkSpeed = -2f;
	[SerializeField] Rigidbody2D rigidBody;


	[SerializeField] bool isGrounded = false;
	[SerializeField] bool isFacingWall = false;
	[SerializeField] bool isFacingHole = false;
	[SerializeField] bool isPlayerInRange = false;
	[SerializeField] bool isVulnerable = true;
	[SerializeField] bool isHedgehog;
	[SerializeField] bool isSpike;


	// Use this for initialization
	void Start () 
	{
		gd = GetComponentInChildren<GroundDetect>();
		turnAwayFromHole = GetComponentInChildren<EnemyTurnDetect>();
		turnAwayFromWall = GetComponentInChildren<EnemyWallDetect>();
		playerDetect = GetComponentInChildren<EnemyPlayerDetect>();
		rigidBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		isGrounded = gd.isGrounded;
		isFacingWall = turnAwayFromWall.turningPointWall;
		isFacingHole = turnAwayFromHole.turningPoint;
		isPlayerInRange = playerDetect.isPlayerInRange;

		
		//attack routine


		//movement routine
		if (isHedgehog)
		{
			if (isFacingHole || isFacingWall)
			{facingLeft = !facingLeft;}
			
			if (facingLeft)
			{
				rigidBody.velocity = new Vector2 (walkSpeed, rigidBody.velocity.y);
			}
			else
			{
				rigidBody.velocity = new Vector2 (-walkSpeed, rigidBody.velocity.y);
			}

			//turn the enemy's facing direction
			if (rigidBody.velocity.x < 0)
			{
				this.gameObject.transform.localScale = new Vector3 (1,1,1);
			}
			else if (rigidBody.velocity.x > 0)
			{
				this.gameObject.transform.localScale = new Vector3 (-1,1,1);
			}
		}
		


	}

	public void TakeDamage(float amount)
	{
		if (isVulnerable) health -= amount;
		if (health <=0f)
		{
			Die();
		}
	}
	void Die()
	{
		Destroy(gameObject);
	}
}
