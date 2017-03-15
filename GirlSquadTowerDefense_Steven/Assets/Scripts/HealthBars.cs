using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBars : MonoBehaviour {


	public float health;
	public GameObject red;
	public GameObject red2;
	public GameObject red3;
	public GameObject red4;
	public GameObject red5;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		health = gameObject.transform.parent.GetComponent<Enemy> ().health;
		CheckHealth (health);
	}


	void CheckHealth(float health)
	{
		if (health <= 4)
			red.SetActive (false);
		if (health <= 3)
			red2.SetActive (false);
		if (health <= 2)
			red3.SetActive (false);
		if (health <= 1)
			red4.SetActive (false);
	}
}
