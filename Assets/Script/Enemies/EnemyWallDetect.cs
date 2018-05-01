using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWallDetect : MonoBehaviour 
{
public bool turningPointWall;
	
	// Use this for initialization
	void OnEnable () 
	{
		turningPointWall = false;
	}
	// Use this for initialization
	void OnTriggerEnter2D(Collider2D other) 
	{
        if (other.gameObject.tag == "Wall" || other.gameObject.tag == "Ground" || other.gameObject.tag == "Enemy") 
		{
            turningPointWall=true;
    	}
    }
             
    void OnTriggerStay2D(Collider2D other) 
	{
        if (other.gameObject.tag == "Wall" || other.gameObject.tag == "Ground" || other.gameObject.tag == "Enemy") 
		{
            turningPointWall=true;
    	}
	}
             
    void OnTriggerExit2D(Collider2D other) 
	{
        if (other.gameObject.tag == "Wall" || other.gameObject.tag == "Ground" || other.gameObject.tag == "Enemy") 
		{
            turningPointWall=false;
    	}
    }
}
