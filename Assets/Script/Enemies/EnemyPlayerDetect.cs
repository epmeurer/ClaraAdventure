using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlayerDetect : MonoBehaviour 
{
	public bool isPlayerInRange;
	
	// Use this for initialization
	void OnEnable () 
	{
		isPlayerInRange = false;
	}
	// Use this for initialization
	void OnTriggerEnter2D(Collider2D other) 
	{
        if (other.gameObject.tag == "Player") 
		{
            isPlayerInRange=true;
    	}
    }
             
    void OnTriggerStay2D(Collider2D other) 
	{
        if (other.gameObject.tag == "Player") 
		{
            isPlayerInRange=true;
    	}
	}
             
    void OnTriggerExit2D(Collider2D other) 
	{
        if (other.gameObject.tag == "Player") 
		{
            isPlayerInRange=false;
    	}
    }

}
