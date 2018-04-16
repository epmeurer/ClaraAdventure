using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyclingPlataform : MonoBehaviour {

	[SerializeField] Vector3 nextPos;
	[SerializeField] bool circleOrientation = true;
	[SerializeField] Vector2 posPlataform2D, circleCenter2D;
	[SerializeField] float plataformSpeed = 2.0f,angleNow, angleToGo,  angularSpeed = 0.1f, distanceFromCenter;
	[SerializeField] float x, y;
	[SerializeField] Transform plataformTransform;
	[SerializeField] Transform circleCenter;
	// Use this for initialization
	void Start () 
	{
		SetNextPos();
	}
	void SetNextPos ()
	{
		//set plataform and circle positions
		posPlataform2D = new Vector2 (plataformTransform.position.x, plataformTransform.position.y);
		circleCenter2D = new Vector2 (circleCenter.position.x, circleCenter.position.y);
		distanceFromCenter = Vector2.Distance(circleCenter2D,posPlataform2D);
		
		//calculate angle between both
		angleNow = Vector2.Angle(circleCenter2D,posPlataform2D);
		if (circleCenter2D.y > posPlataform2D.y)
			{angleNow += Mathf.PI;}

		if (circleOrientation)
		{
			angleToGo = angleNow + angularSpeed; 
			while (angleToGo > 2* Mathf.PI)
				{ angleToGo -= 2* Mathf.PI;; }
		}
		else
		{
			angleToGo = angleNow - angularSpeed; 
			while (angleToGo < 0)
				{ angleToGo += 2* Mathf.PI; }
		}
		// make new positions from scratch
		x = Mathf.Cos(angleToGo) * distanceFromCenter;
        y = Mathf.Sin(angleToGo) * distanceFromCenter;
		
		nextPos = new Vector3 (x + circleCenter.position.x, y + circleCenter.position.y, plataformTransform.position.z);
	}
	// Update is called once per frame
	void Update () 
	{
		posPlataform2D = new Vector2 (plataformTransform.position.x, plataformTransform.position.y);
		if (plataformTransform.position == nextPos) SetNextPos();
		Move();
	}

	void Move ()
	{
		plataformTransform.position = Vector3.MoveTowards(plataformTransform.position, nextPos, plataformSpeed*Time.deltaTime);
	}
}
