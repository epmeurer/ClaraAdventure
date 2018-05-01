using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetect : MonoBehaviour
{
	public bool isGrounded;
	[SerializeField] Transform groundedObject;
	
	// Use this for initialization
	void OnEnable () 
	{
		isGrounded = false;
	}
	// Use this for initialization
	void OnTriggerEnter2D(Collider2D other) 
	{
        if (other.gameObject.tag == "Ground") 
		{
            isGrounded=true;
			groundedObject = this.transform.parent;
			groundedObject.transform.SetParent(other.transform);
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
			if (groundedObject != null) 
				{
					groundedObject.transform.parent = null;
					groundedObject.transform.rotation = Quaternion.Euler(Vector3.zero);
				}
    	}
    }

}
