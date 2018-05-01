using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurnDetect : MonoBehaviour 
{
public bool turningPointHole;
	
	//criar uma lista e adicionar os objetos a lista
	//caso A LISTA não tenha objetos ele vira
	List <Collider2D> groundList;

	void Start ()
	{
		groundList = new List<Collider2D>();
	}
	// Use this for initialization
	void OnEnable () 
	{
		turningPointHole = false;
	}
	// Use this for initialization
	void OnTriggerEnter2D(Collider2D other) 
	{
		
        if (other.gameObject.tag == "Ground") 
		{
			groundList.Add(other);
            turningPointHole=false;
    	}
    }
             
    void OnTriggerExit2D(Collider2D other) 
	{
		if (other.gameObject.tag == "Ground") 
		{
			groundList.Remove(other);
		}
		
        if (groundList.Count == 0) 
		{
            turningPointHole=true;
    	}
    }
}
