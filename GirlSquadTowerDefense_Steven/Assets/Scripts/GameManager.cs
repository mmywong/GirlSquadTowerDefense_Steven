using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	public GameObject leftCursor;
	public GameObject rightCursor;
	public GameObject enemy;



	public int player1lane = 1;
	public int player2lane = 1;


	void Awake()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);
	}

	void Start()
	{
		leftCursor = Instantiate (leftCursor);
		rightCursor = Instantiate (rightCursor);

	}


	void Update()
	{	
		if (Input.GetKeyDown (KeyCode.LeftShift)) 
		{
			MoveCursor (leftCursor);
			if (player1lane == 3)
				player1lane = 1;
			else
				player1lane += 1;
		}
		if (Input.GetKeyDown (KeyCode.RightShift)) 
		{
			MoveCursor (rightCursor);
			if (player2lane == 3)
				player2lane = 1;
			else
				player2lane += 1;
		}

		if (Input.GetKeyDown (KeyCode.A)) 
		{
			SpawnEnemy (leftCursor.transform.localPosition + new Vector3(1f, 0f, 0f));
		}
		if (Input.GetKeyDown (KeyCode.J)) 
		{
			SpawnEnemy (rightCursor.transform.localPosition - new Vector3(1f, 0f, 0f));

		}

	}

	void MoveCursor(GameObject Cursor)
	{
		if (Cursor.gameObject.transform.localPosition.y == -3.5f)
			Cursor.gameObject.transform.localPosition += new Vector3 (0f, 7f, 0f);
		else 
			Cursor.gameObject.transform.localPosition -= new Vector3 (0f, 3.5f, 0f);
	}

	void SpawnEnemy(Vector3 spawnPosition)
	{
		Instantiate (enemy, spawnPosition, Quaternion.identity);
	}
		

}
