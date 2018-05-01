using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScoreIncrement : MonoBehaviour 
{
public bool catchCoin;
public bool catchLife;
public GameObject collectedObject;
	
	// Use this for initialization
	void OnEnable () 
	{
		catchCoin = false;
	}
	// Use this for initialization
	void OnTriggerEnter2D(Collider2D other) 
	{
        if (other.gameObject.tag == "Coin") 
		{
			catchCoin = true;
            collectedObject = other.gameObject;
    	}

        if (other.gameObject.tag == "Life") 
		{
			catchLife = true;
            collectedObject = other.gameObject;
    	}
    }         
    void OnTriggerExit2D(Collider2D other) 
	{
        if (other.gameObject.tag == "Coin")
		{
            catchCoin=false;
    	}
        if (other.gameObject.tag == "Life")
		{
            catchLife=false;
    	}
    }
}
