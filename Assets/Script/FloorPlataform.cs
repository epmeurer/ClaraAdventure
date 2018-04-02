using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorPlataform : MonoBehaviour {

	Rigidbody2D floor;
	Collider2D platCollider;
	[SerializeField] GameObject playerFeet;
	[SerializeField] float posY;
	// Use this for initialization
	void Start () 
	{
		floor = GetComponent<Rigidbody2D>();
		floor.isKinematic = true;
		platCollider = GetComponent<Collider2D>();
		platCollider.enabled = false;

		playerFeet = GameObject.FindWithTag("PlayerFeet");
	}
	
	// Update is called once per frame
	void Update () 
	{
		posY = GameObject.FindWithTag("PlayerFeet").transform.position.y;
		if (this.transform.position.y < posY)
		{
			platCollider.enabled = true;
		}
		else
		{
			platCollider.enabled = false;
		}
	}
}
