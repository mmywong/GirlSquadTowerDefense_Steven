using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyByBoundary2 : MonoBehaviour {
	public Text castle2text;

	public int health2 = 5;


	void Start()
	{
		castle2text = GameObject.Find ("Castle2Health").GetComponent<Text> ();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		Destroy (other.gameObject);
		health2 -= 1;
	}
	void Update()
	{
		CheckGameOver ();
		castle2text.text = "Health: " + health2.ToString();
	}

	void CheckGameOver()
	{
		if (health2 <= 0)
			gameObject.GetComponent<ChangeScene> ().ChangeToScene ("GameOver");
	}

}
