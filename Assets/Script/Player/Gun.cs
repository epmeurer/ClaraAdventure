using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {
	
	public float damage = 10f;
	public float range = 100f;
	public float impactForce = 30f;
	public float fireRate = 15f;

	public int maxAmmo = 10;
	private int currentAmmo = -1;
	public float reloadTime = 1f;

	private float nextTimeToFire = 0f;
	bool isReloading = false;
	
	[SerializeField] GameObject gunBody;
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	
	void Shoot()
	{
		
	}
}
