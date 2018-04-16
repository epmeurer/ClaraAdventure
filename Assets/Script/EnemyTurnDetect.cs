using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurnDetect : MonoBehaviour 
{
public bool turningPoint;
	
	// Use this for initialization
	void OnEnable () 
	{
		turningPoint = false;
	}
	// Use this for initialization
	void OnTriggerEnter2D(Collider2D other) 
	{
        if (other.gameObject.tag == "Ground") 
		{
            turningPoint=false;
    	}
    }
             
    void OnTriggerStay2D(Collider2D other) 
	{
        if (other.gameObject.tag == "Ground") 
		{
            turningPoint=false;
    	}
	}
             
    void OnTriggerExit2D(Collider2D other) 
	{
        if (other.gameObject.tag == "Ground") 
		{
            turningPoint=true;
    	}
    }
}
