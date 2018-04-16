using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestructPlataform : MonoBehaviour 
{
	[SerializeField] bool playerIsTouching= false;
	[SerializeField] float duration = 3f;
	[SerializeField] float timeSpent = 0f;

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (playerIsTouching)
			{
				timeSpent += Time.deltaTime;
			}
		if (timeSpent > duration)
			Die();
	}

	void OnEnable () 
	{
		
	}
	// Use this for initialization
	void OnTriggerEnter2D(Collider2D other) 
	{
        if (other.gameObject.tag == "PlayerFeet") 
		{
            playerIsTouching=true;
    	}
    }
             
    void OnTriggerStay2D(Collider2D other) 
	{
        if (other.gameObject.tag == "PlayerFeet") 
		{
            playerIsTouching=true;
    	}
	}
             
    void OnTriggerExit2D(Collider2D other) 
	{
        if (other.gameObject.tag == "PlayerFeet") 
		{
            playerIsTouching=false;
    	}
    }

	void Die()
	{
		Destroy(gameObject);
	}
}
