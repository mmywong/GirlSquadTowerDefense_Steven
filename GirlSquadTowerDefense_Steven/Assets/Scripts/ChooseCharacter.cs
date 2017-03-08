using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseCharacter : MonoBehaviour {

	public GameObject RedCursor;
	public int player1 = 1;
	public GameObject BlueCursor;
	public int player2 = 1;

	public bool p1canmove = true;
	public bool p2canmove = true;
	private bool p1ready = false;
	private bool p2ready = false;


	void Awake()
	{
		DontDestroyOnLoad (gameObject);
	}

	void Start () 
	{
		RedCursor = Instantiate (RedCursor);
		BlueCursor = Instantiate (BlueCursor);
	}

	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.LeftShift) && p1canmove) 
		{
			MoveCursor (RedCursor);
			if (player1 == 4)
				player1 = 1;
			else
				player1 += 1;
		}
		if (Input.GetKeyDown (KeyCode.RightShift) && p2canmove) 
		{
			MoveCursor (BlueCursor);
			if (player2 == 4)
				player2 = 1;
			else
				player2 += 1;
		}
		if (Input.GetKeyDown (KeyCode.A)) 
		{
			SelectCharacter1 ();
		}
		else if (Input.GetKeyDown (KeyCode.A) && !p1canmove) 
		{
			SelectCharacter1 ();
		}
		if (Input.GetKeyDown (KeyCode.J)) 
		{
			SelectCharacter2 ();
		}
		else if (Input.GetKeyDown (KeyCode.J) && !p2canmove) 
		{
			SelectCharacter2 ();
		}
	}

	void MoveCursor(GameObject Cursor)
	{
		if (Cursor.gameObject.transform.localPosition.x == 5.0f)
			Cursor.gameObject.transform.localPosition -= new Vector3 (15.0f, 0f, 0f);
		else 
			Cursor.gameObject.transform.localPosition += new Vector3 (5.0f, 0f, 0f);
	}

	void SelectCharacter1()
	{
		//if (p1ready && p2ready)
			//ChangeScene to the gamescene
		if (!p1canmove)
			p1canmove = true;
		else
			p1canmove = false;

	}

	void SelectCharacter2()
	{
		//if (p1ready && p2ready
		//	changescene to the gamescene
		if (!p2canmove)
			p2canmove = true;
		else
			p2canmove = false;
	}
}
