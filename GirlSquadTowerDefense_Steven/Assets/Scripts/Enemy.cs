using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public bool player1;
	public float speed;
	public int health;

	void Start()
	{
		if (transform.position.x < 0)
			player1 = true;
		else
			player1 = false;
	}


	void Update () 
	{
		Movement ();
	}
		
	void Movement()
	{
		if (player1) 
		{
			transform.position += new Vector3 (speed * Time.deltaTime, 0.0f, 0.0f);
		} 
		else
			transform.position -= new Vector3 (speed * Time.deltaTime, 0.0f, 0.0f);
	}

}
