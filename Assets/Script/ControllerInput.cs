using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof (PlayerMotor))]
public class ControllerInput : MonoBehaviour {

	[SerializeField] Vector2 playerInput;
	[SerializeField] bool left, right, jump, crouch, space;
	private PlayerMotor motor;

	// Use this for initialization
	void Start () 
	{
		motor = GetComponent<PlayerMotor>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		left = right = jump = crouch = space = false;

		left = Input.GetKey(KeyCode.A);
		right = Input.GetKey(KeyCode.D);
		jump = Input.GetKey(KeyCode.W);
		crouch = Input.GetKey(KeyCode.S);

		space = Input.GetKey(KeyCode.Space);

		playerInput = Vector2.zero;

		playerInput.x = Input.GetAxis("Horizontal");
		playerInput.y = Input.GetAxis("Vertical");

		motor.Move(playerInput);
		if (space) 
		{
			motor.Shoot(space);
		}
	}
}