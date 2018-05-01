using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageEnemy : MonoBehaviour 
{
public bool playerContact;
public float damageDealt;
public Vector2 enemyPos;
	
	// Use this for initialization
	void OnEnable () 
	{
		playerContact = false;
	}
	// Use this for initialization
	void OnTriggerEnter2D(Collider2D other) 
	{
        if (other.gameObject.tag == "Enemy") 
		{
            playerContact=true;
			damageDealt = other.GetComponent<Enemy>().damageDealt;
			enemyPos = other.GetComponent<Transform>().position;
    	}
    }
             
    void OnTriggerStay2D(Collider2D other) 
	{
        if (other.gameObject.tag == "Enemy") 
		{
            playerContact=true;
			damageDealt = other.GetComponent<Enemy>().damageDealt;
			enemyPos = other.GetComponent<Transform>().position;
    	}
	}
             
    void OnTriggerExit2D(Collider2D other) 
	{
        if (other.gameObject.tag == "Enemy") 
		{
            playerContact=false;
			damageDealt = 0f;
			enemyPos = Vector2.zero;
    	}
    }
}
