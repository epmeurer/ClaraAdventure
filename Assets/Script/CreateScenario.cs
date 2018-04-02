using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateScenario : MonoBehaviour {

	public GameObject floor;
	public GameObject player;
	// Use this for initialization
	void Start () 
	{
		Invoke("SpawnPlayer",0);
		//Invoke("CreateMap",0);
	}
	void SpawnPlayer ()
	{
		Vector2 pos = Vector2.zero;
		GameObject newPlayer = Instantiate(player);
		player.transform.position = pos;
	}
	
	// Update is called once per frame
	void CreateMap ()
	{
		Vector2 pos = Vector2.zero;
		int a = 0, b = 0;
		GameObject newPlataform;
		while (b < 4)
		{
			a = 0;
			while (a<140)
			{
				pos = new Vector2 (-15.0f + 0.2f * (float)a , -4f + 2*(float)b);
				newPlataform = Instantiate(floor);
				newPlataform.transform.position = pos;
				a++;
			}
			b++;
		}

		

	}
}
