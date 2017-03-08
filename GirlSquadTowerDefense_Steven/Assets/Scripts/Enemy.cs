using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public bool player1;
	public float speed = 2f;
	public float health = 5f;
	public float dps = 1f;
	public bool attacking = false;
	public float cooldown = 2f;

	public GameObject target;

	void Start()
	{
		if (transform.position.x < 0)
			player1 = true;
		else
			player1 = false;

        //----Special Minion Stats----//
        if(this.tag == "warrior")
        {
            dps = 1.2f;
        }
        else if (this.tag == "healer")
        {
            cooldown = 1.5f;
                // call healer function
        }
        else if (this.tag == "mage")
        {
            speed = 2.5f;
        }
        else if (this.tag == "tank")
        {
            health = 7;
            speed = 1.5f;
        }
        //else it's regular minion. do nothing.
	}


	void Update () 
	{
		cooldown -= Time.deltaTime;
		if (!attacking)
			Movement ();
		else if (!CheckIfEnemyDead() && cooldown <= 0) 
		{
			Attack ();
			cooldown = 2f;
		}
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.tag != this.tag && target == null) {
			attacking = true;
			target = other.gameObject;
		}
	}

	void Attack()
	{
		target.GetComponent<Enemy> ().health -= dps;
		if (target.GetComponent<Enemy> ().health <= 0) 
		{
			Destroy (target);
			target = null;
			attacking = false;
		}

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


	bool CheckIfEnemyDead()
	{
		if (target == null) 
		{
			attacking = false;
			return true;
		}
		return false;
	}
}
