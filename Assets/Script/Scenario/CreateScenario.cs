using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateScenario : MonoBehaviour {

	public GameObject floor;
	public GameObject player;
	public GameObject startPoint;
	// Use this for initialization
	void Start () 
	{
		Vector2 pos = Vector2.zero;
		startPoint = GameObject.FindGameObjectWithTag("Respawn");
	}
	void SpawnPlayer ()
	{
		Vector2 pos = Vector2.zero;
		//GameObject newPlayer = Instantiate(player);
		//startPoint = GameObject.FindGameObjectWithTag("Respawn");
		//player.transform.position = startPoint.transform.position;
		//this.transform.position = player.transform.position;
	}
	
	// Update is called once per frame
	void CreateMap ()
	{

	}
}
