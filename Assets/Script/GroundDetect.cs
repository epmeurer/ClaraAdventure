using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetect : MonoBehaviour 
{

	[SerializeField] Collider2D feetCollider;
	public bool isGrounded;
	

	// Use this for initialization
	void OnEnable () 
	{
		feetCollider = GetComponentInChildren<Collider2D>();
		isGrounded = false;
	}
	// Use this for initialization
	void OnTriggerEnter2D(Collider2D other) 
	{
        if (other.gameObject.tag == "Ground") 
		{
            isGrounded=true;
    	}
    }
             
    void OnTriggerStay2D(Collider2D other) 
	{
        if (other.gameObject.tag == "Ground") 
		{
            isGrounded=true;
    	}
	}
             
    void OnTriggerExit2D(Collider2D other) 
	{
        if (other.gameObject.tag == "Ground") 
		{
            isGrounded=false;
    	}
    }

}
