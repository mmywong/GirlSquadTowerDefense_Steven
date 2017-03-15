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

    public GameObject p1_warrior;
    public GameObject p1_mage;
    public GameObject p1_tank;
    public GameObject p1_healer;
    public GameObject p2_warrior;
    public GameObject p2_mage;
    public GameObject p2_tank;
    public GameObject p2_healer;

    public float cooldown1 = 0f;
	public float cooldown2 = 0f;
	public int resource1 = 300;
	public int resource2 = 300;
	public int cost = 200;
    public int spec_cost = 300;
	public Text P1Text;
	public Text P2Text;
	public int player1lane = 1;
	public int player2lane = 1;
	public bool p1ult = true;
	public bool p2ult = true;

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
        leftCursor = Instantiate(leftCursor);
        rightCursor = Instantiate(rightCursor);
        player1 = GameObject.Find("CharacterChooser").GetComponent<ChooseCharacter>().player1;
		player2 = GameObject.Find("CharacterChooser").GetComponent<ChooseCharacter>().player2;
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

        //------------Player 1------------//
		if (Input.GetKeyDown (KeyCode.A) && cooldown1 <= 0 && resource1 >= cost) 
		{
			resource1 -= cost;
			cooldown1 = 2.0f;
			SpawnEnemy (enemy1, leftCursor.transform.localPosition + new Vector3(1f, 0f, 0f));
		}
        if (Input.GetKeyDown(KeyCode.S) && cooldown1 <= 0 && resource1 >= spec_cost)
        {
            resource1 -= spec_cost;
            cooldown1 = 2.0f;

            switch(player1)
            {
                case 1: // michelle
                    SpawnEnemy(p1_mage, leftCursor.transform.localPosition + new Vector3(1f, 0f, 0f));
                    break;
                case 2: // steven
                    SpawnEnemy(p1_warrior, leftCursor.transform.localPosition + new Vector3(1f, 0f, 0f));
                    break;
                case 3: // valerie
                    SpawnEnemy(p1_tank, leftCursor.transform.localPosition + new Vector3(1f, 0f, 0f));
                    break;
                case 4: // jinnie
                    SpawnEnemy(p1_healer, leftCursor.transform.localPosition + new Vector3(1f, 0f, 0f));
                    break;
            }
        }
		if (Input.GetKeyDown (KeyCode.D) && p1ult) 
		{
			switch(player1)
			{
				case 1: // michelle
					GameObject[] listofopponents1;
					listofopponents1 = GameObject.FindGameObjectsWithTag ("p2_minion");
					for (int i = 0; i < listofopponents1.Length; i++) {
						listofopponents1 [i].GetComponent<Enemy> ().health -= 1;
					}
					break;
				case 2: // steven
					GameObject[] listofopponents2;
					listofopponents2 = GameObject.FindGameObjectsWithTag ("p2_minion");
					for (int i = 0; i < listofopponents2.Length; i++) {
						listofopponents2 [i].GetComponent<Enemy> ().speed = 0.6f;
					}
					break;
				case 3: // valerie
					GameObject[] listofself;
					listofself = GameObject.FindGameObjectsWithTag ("p1_tank");
					for (int i = 0; i < listofself.Length; i++) {
						listofself [i].GetComponent<Enemy> ().invulnerable = true;
					}
					break;
				case 4: // jinnie
					GameObject.Find("p1boundary").GetComponent<DestroyByBoundary>().health1++;
					break;
			}
			p1ult = false;
		}

        //------------Player 2------------//
        if (Input.GetKeyDown (KeyCode.J) && cooldown2 <= 0 && resource2 >= cost) 
		{
			resource2 -= cost;
			cooldown2 = 2.0f;
			SpawnEnemy (enemy2, rightCursor.transform.localPosition - new Vector3(1f, 0f, 0f));
		}
        if (Input.GetKeyDown(KeyCode.K) && cooldown2 <= 0 && resource2 >= spec_cost)
        {
            resource2 -= spec_cost;
            cooldown2 = 2.0f;
            switch (player2)
            {
                case 1: // michelle
                    SpawnEnemy(p2_mage, rightCursor.transform.localPosition - new Vector3(1f, 0f, 0f));
                    break;
                case 2: // steven
                    SpawnEnemy(p2_warrior, rightCursor.transform.localPosition - new Vector3(1f, 0f, 0f));
                    break;
                case 3: // valerie
                    SpawnEnemy(p2_tank, rightCursor.transform.localPosition - new Vector3(1f, 0f, 0f));
                    break;
                case 4: // jinnie
                    SpawnEnemy(p2_healer, rightCursor.transform.localPosition - new Vector3(1f, 0f, 0f));
                    break;
            }
        }
		if (Input.GetKeyDown (KeyCode.L) && p2ult) {
			switch (player2) {
			case 1: // michelle
				GameObject[] listofopponents1;
				listofopponents1 = GameObject.FindGameObjectsWithTag ("p1_minion");
				for (int i = 0; i < listofopponents1.Length; i++) {
					listofopponents1 [i].GetComponent<Enemy> ().health -= 1;
				}
				break;
			case 2: // steven
				GameObject[] listofopponents2;
				listofopponents2 = GameObject.FindGameObjectsWithTag ("p1_minion");
				for (int i = 0; i < listofopponents2.Length; i++) {
					listofopponents2 [i].GetComponent<Enemy> ().speed = 0.6f;
				}
				break;
			case 3: // valerie
				GameObject[] listofself;
				listofself = GameObject.FindGameObjectsWithTag ("p2_tank");
				for (int i = 0; i < listofself.Length; i++) {
					listofself [i].GetComponent<Enemy> ().invulnerable = true;
				}
				break;
			case 4: // jinnie
				GameObject.Find ("p2boundary").GetComponent<DestroyByBoundary2> ().health2++;
				break;
			}
			p2ult = false;
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
