using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformScript : MonoBehaviour 
{
	[SerializeField] bool isSelfDestruct = false, isMoving = false, isCycling = false;


	[SerializeField] Vector3 initialPos, destinationPos, nextPos;
	[SerializeField] float plataformSpeed = 3.0f;
	[SerializeField] Transform plataformTransform;
	[SerializeField] Transform pinPoint;


	[SerializeField] bool circleOrientation = true;
	float orientation = 1f;
	//[SerializeField] Transform circleCenter;

	[SerializeField] bool playerIsTouching= false;
	[SerializeField] float plataformDuration = 3f;
	[SerializeField] float timeSpent = 0f;

	// Use this for initialization
	void Start () 
	{
		initialPos = plataformTransform.position;
		destinationPos = pinPoint.position;
		nextPos = destinationPos;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (isSelfDestruct)
		{
			//ajustar cor
			plataformTransform.GetComponent<SpriteRenderer>().color = new Color (1f,0.7f,0.7f,1f);
			if (playerIsTouching)
			{
				timeSpent += Time.deltaTime;
			}
			if (timeSpent > plataformDuration)
			Die();

		}
		//for one direction movement towards one point - back and foward
		if (isMoving)
		{
			if (plataformTransform.position == destinationPos)
			{ nextPos = initialPos;}
			if (plataformTransform.position == initialPos)
			{ nextPos = destinationPos;}
			Move();
		}
		
		//to rotate around one point
		if (isCycling)
		{
		if (circleOrientation)
			orientation = 1f;
		else
			orientation = -1f;
		CycleMove();
		}
	}

	void Move ()
	{
		plataformTransform.position = Vector3.MoveTowards(plataformTransform.position, nextPos, plataformSpeed*Time.deltaTime);
	}

	void CycleMove ()
	{
		//plataformTransform.position = Vector3.MoveTowards(plataformTransform.position, nextPos, plataformSpeed*Time.deltaTime);
		transform.RotateAround(pinPoint.transform.position, Vector3.forward, 10*plataformSpeed * orientation * Time.deltaTime);
		plataformTransform.Rotate(Vector3.forward,  10*plataformSpeed * -orientation * Time.deltaTime);
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
