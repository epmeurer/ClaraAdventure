using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public bool direction;
	[SerializeField] Vector2 speed = new Vector2 (10f,0f);
	[SerializeField] float damage = 10f;
	[SerializeField] float impactForce = 10f;
	Rigidbody2D bulletBody;
	Enemy target;


	// Use this for initialization
	void Start () 
	{
		bulletBody = GetComponent<Rigidbody2D>();
		bulletBody.isKinematic = true;
		if (direction)
		{
			bulletBody.velocity = speed;
		}
		else
		{
			bulletBody.velocity = -speed;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	// Use this for initialization
	void OnTriggerEnter2D(Collider2D other) 
	{
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Wall")
		{
			target = other.GetComponent<Enemy>();
			if (target != null)
			{
				target.TakeDamage(damage);
			if (other.GetComponent<Rigidbody2D>() != null)
				{ other.GetComponent<Rigidbody2D>().AddForce(bulletBody.velocity * impactForce);}
			}

			Destroy(gameObject);
    	}

    }
}
