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
	[SerializeField] Transform destination, enemyTransform;
	[SerializeField] Vector3 nextPos, startPoint, destinationPoint, currentPos;
	

	[SerializeField] bool facingLeft = true;
	[SerializeField] float walkSpeed = -2f;
	[SerializeField] Rigidbody2D rigidBody;


	[SerializeField] bool isGrounded = false, isFacingWall = false, isFacingHole = false;
	[SerializeField] bool isPlayerInRange = false, isAttacking = false;


	[SerializeField] bool isVulnerable = true;
	[SerializeField] bool isHedgehog;
	[SerializeField] bool isSnake;
	[SerializeField] bool isBat;
	[SerializeField] bool isSpike;


	// Use this for initialization
	void Start () 
	{
		gd = GetComponentInChildren<GroundDetect>();
		turnAwayFromHole = GetComponentInChildren<EnemyTurnDetect>();
		turnAwayFromWall = GetComponentInChildren<EnemyWallDetect>();
		playerDetect = GetComponentInChildren<EnemyPlayerDetect>();
		rigidBody = GetComponent<Rigidbody2D>();

		startPoint = enemyTransform.position;
		destinationPoint = nextPos = destination.transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		isGrounded = gd.isGrounded;
		isFacingWall = turnAwayFromWall.turningPointWall;
		isFacingHole = turnAwayFromHole.turningPointHole;
		isPlayerInRange = playerDetect.isPlayerInRange;

		
		//attack routine


		//movement routine
		if (isHedgehog)
		{
			if (isFacingHole && isGrounded) 
			{
				facingLeft = !facingLeft;
				Debug.Log("hole");
			}
			if (isFacingWall && isGrounded) 
			{
				facingLeft = !facingLeft;
				Debug.Log("wall");
			}
			
			if (facingLeft || !isAttacking)
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
		if (isSnake)
		{
			if (isFacingHole && isGrounded) 
			{
				facingLeft = !facingLeft;
			}
			if (isFacingWall && isGrounded) 
			{
				facingLeft = !facingLeft;
			}
			
			if (facingLeft && !isAttacking)
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

		if (isBat)
		{
			currentPos = enemyTransform.position;
			if (currentPos == destinationPoint)
				{ nextPos = startPoint; }
			if (currentPos == startPoint)
				{ nextPos = destinationPoint; }
			enemyTransform.position = Vector3.MoveTowards(enemyTransform.position, nextPos, walkSpeed*Time.deltaTime);
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
