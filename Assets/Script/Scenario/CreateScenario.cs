using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateScenario : MonoBehaviour {

	public GameObject player;
	public GameObject startPoint;
	// Use this for initialization
	void Start ()
	{
		PlayerPrefs.SetInt("LEVEL", 1);
		Vector2 pos = Vector2.zero;
		player.GetComponent<PlayerMotor>().StageLoadStats();
		
	}
	
	// Update is called once per frame
	void CreateMap ()
	{

	}
}
