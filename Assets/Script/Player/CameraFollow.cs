using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour 
{
	[SerializeField] Rigidbody2D playerBody;
	[SerializeField] GameObject goCam;
	[SerializeField] Vector2 playerPosition;

	[SerializeField] Vector2 cameraSpeed;
	[SerializeField] Vector2 cameraPosition;
	[SerializeField] bool cameraFarFromPlayer;

	[SerializeField] float followFrameRate;


	// Use this for initialization
	void Start () 
	{
		playerBody = GetComponent<Rigidbody2D>();
		goCam = GameObject.FindGameObjectWithTag("MainCamera");
		playerPosition =cameraPosition = cameraSpeed = Vector2.zero;
		followFrameRate = 10f;

		FocusOnPlayer();
	}
	void Update ()
	{
		float distanceFromPlayer;
		playerPosition = playerBody.position;
		cameraPosition = new Vector2 (goCam.transform.position.x, goCam.transform.position.y);

		distanceFromPlayer = Vector2.Distance(playerPosition,cameraPosition);
		cameraSpeed = new Vector2 ( (playerPosition.x - cameraPosition.x)*distanceFromPlayer/(followFrameRate), (playerPosition.y - cameraPosition.y)*distanceFromPlayer/(followFrameRate));

		if (Vector2.Distance(playerPosition,cameraPosition) > 4f)
		{
			cameraFarFromPlayer = true;
		}
		if (cameraFarFromPlayer)
		{
			cameraPosition += cameraSpeed;
			goCam.transform.position = new Vector3 (cameraPosition.x, cameraPosition.y, goCam.transform.position.z);
			if (Vector2.Distance(playerPosition,cameraPosition) < 0.1f)
			{
				cameraFarFromPlayer = false;
			}
		}
	}
	public void FocusOnPlayer ()
	{
		playerPosition = playerBody.position;
		goCam.transform.position =  new Vector3 (playerPosition.x, playerPosition.y, goCam.transform.position.z);
	}
}