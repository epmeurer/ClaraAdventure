using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlataform : MonoBehaviour 
{

	[SerializeField] Vector3 initialPos, destinationPos, nextPos;
	[SerializeField] float plataformSpeed = 1.0f;
	[SerializeField] Transform plataformTransform;
	[SerializeField] Transform destination;
	// Use this for initialization
	void Start () 
	{
		initialPos = plataformTransform.position;
		destinationPos = destination.position;
		nextPos = destinationPos;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (plataformTransform.position == destinationPos)
			{ nextPos = initialPos;}
		if (plataformTransform.position == initialPos)
			{ nextPos = destinationPos;}
		Move();
	}

	void Move ()
	{
		plataformTransform.position = Vector3.MoveTowards(plataformTransform.position, nextPos, plataformSpeed*Time.deltaTime);
	}
}
