using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof (Rigidbody2D))]
public class PlayerMotor : MonoBehaviour {

	[SerializeField] bool isGrounded = true;
	[SerializeField] bool isCrouching = false;
	[SerializeField] bool isGameRunning = false;
	[SerializeField] bool isGameOver = false;
	[SerializeField] float jumpForce = 5f;
	[SerializeField] float speedMultiplier = 1.0f;
	[SerializeField] float maxSpeed = 10f;
	[SerializeField] float movementX, movementY;
	[SerializeField] Vector2 playerSpeed = Vector2.zero;
	[SerializeField] Rigidbody2D playerBody;
	[SerializeField] GroundDetect gd;

	// Use this for initialization
	void Start () 
	{
		playerBody = GetComponent<Rigidbody2D>();
		gd = GetComponentInChildren<GroundDetect>();
	}
	// Update is called once per frame
	void Update () 
	{
		isGrounded = gd.isGrounded;
		movementX += movementX*maxSpeed;

		if ( movementX >= maxSpeed )
		{
			movementX = maxSpeed;
			speedMultiplier = 1.5f;
		}
		else if ( movementX <= -maxSpeed )
		{
			movementX = -maxSpeed;
			speedMultiplier = 1.5f;
		}
		else 
		{
			speedMultiplier = 1.0f;
		}
		
		playerBody.velocity = new Vector2 (movementX, playerBody.velocity.y);

		if (movementY > 0) // add variable isGrounded later!!!
		{
			Jump();
		}

	}
	void OnCollisionEnter2D (Collision2D collision)
	{

	}
	public void Move (Vector2 _input)
	{
		movementX = _input.x;
		movementY = _input.y;

	}
	public void Jump ()
	{
		if (isGrounded)
		{
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

	}
}
