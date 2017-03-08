using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	public GameObject leftCursor;
	public GameObject rightCursor;
	public GameObject enemy1;
	public GameObject enemy2;
	public float cooldown1 = 0f;
	public float cooldown2 = 0f;
	public int resource1 = 0;
	public int resource2 = 0;
	public int cost = 200;
	public Text P1Text;
	public Text P2Text;
	public int player1lane = 1;
	public int player2lane = 1;

	private int player1;
	private int player2;

	void Awake()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);
	}

	void Start()
	{
		player1 = GameObject.Find("CharacterChooser").GetComponent<ChooseCharacter>().player1;
		player2 = GameObject.Find("CharacterChooser").GetComponent<ChooseCharacter>().player2;
		leftCursor = Instantiate (leftCursor);
		rightCursor = Instantiate (rightCursor);
		P1Text = GameObject.Find ("P1Resource").GetComponent<Text>();
		P2Text = GameObject.Find ("P2Resource").GetComponent<Text>();
	}


	void Update()
	{	
		cooldown1 -= Time.deltaTime;
		cooldown2 -= Time.deltaTime;
		P1Text.text = "Resource: " + resource1.ToString ();
		P2Text.text = "Resource: " + resource2.ToString ();

		resource1 += 1;
		resource2 += 1;
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

		if (Input.GetKeyDown (KeyCode.A) && cooldown1 <= 0 && resource1 >= cost) 
		{
			resource1 -= cost;
			cooldown1 = 2.0f;
			SpawnEnemy (enemy1, leftCursor.transform.localPosition + new Vector3(1f, 0f, 0f));
		}
		if (Input.GetKeyDown (KeyCode.J) && cooldown2 <= 0 && resource2 >= cost) 
		{
			resource2 -= cost;
			cooldown2 = 2.0f;
			SpawnEnemy (enemy2, rightCursor.transform.localPosition - new Vector3(1f, 0f, 0f));

		}
	}

	void MoveCursor(GameObject Cursor)
	{
		if (Cursor.gameObject.transform.localPosition.y == -3.5f)
			Cursor.gameObject.transform.localPosition += new Vector3 (0f, 7f, 0f);
		else 
			Cursor.gameObject.transform.localPosition -= new Vector3 (0f, 3.5f, 0f);
	}

	void SpawnEnemy(GameObject enemy, Vector3 spawnPosition)
	{
		Instantiate (enemy, spawnPosition, Quaternion.identity);
	}
		

}
