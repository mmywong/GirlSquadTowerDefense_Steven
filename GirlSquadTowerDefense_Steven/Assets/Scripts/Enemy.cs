using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public bool player1;
	public float speed = 2f;
	public float health = 5f;
	public float dps = 1f;
	public bool attacking = false;
    public bool healing = false;
	public float cooldown = 2f;
	public float invulcooldown = 4f;
	public float nextFire = 1f;
	public bool invulnerable = false;

	public GameObject target;
	private Animator animator;


	void Start()
	{
		animator = GetComponent<Animator> ();
		if (transform.position.x < 0)
			player1 = true;
		else
			player1 = false;

        //----Special Minion Stats----//
        if(this.tag == "p1_warrior" || this.tag == "p2_warrior")
        {
            dps = 1.3f;
        }
        else if (this.tag == "p1_healer" || this.tag == "p2_healer")
        {
            cooldown = 1.4f;
                // call healer function
        }
        else if (this.tag == "p1_mage" || this.tag == "p2_mage")
        {
            speed = 3f;
			cooldown = 1.6f;
        }
        else if (this.tag == "p1_tank" || this.tag == "p2_tank")
        {
            health = 7;
        }
        //else it's regular minion. do nothing.
	}


	void Update () 
	{
		cooldown -= Time.deltaTime;
		nextFire -= Time.deltaTime;

		if (!attacking && !healing) {
			Movement ();
		}
		if (invulnerable) 
		{
			invulcooldown -= Time.deltaTime;
			if (invulcooldown <= 0)
				invulnerable = false;
		}

        else if (attacking && !CheckIfEnemyDead() && cooldown <= 0)
        {
            Attack();
			if (this.tag == "p1_mage" || this.tag == "p2_mage")
				cooldown = 1.6f;
			if (this.tag == "p1_healer" || this.tag == "p2_healer")
				cooldown = 1.4f;
			else
            	cooldown = 2.0f;
			animator.SetTrigger ("EnemyAttack");
        }
		if ((this.tag == "p1_healer" || this.tag == "p2_healer") && target != null) {
			target = null;
		}

        else if(healing && !CheckIfAllyDead() && cooldown <= 0)
        {
            Heal();
            cooldown = 1.5f;
        }

	}

	void OnTriggerStay2D(Collider2D other)
    {
        //other is an enemy, attack!
		if ((this.tag [1] != other.tag [1]) && target == null) {
			attacking = true;
			healing = false;
			target = other.gameObject;
		}
        //this is a healer and other is an ally, heal!
        else if (((this.tag == "p1_healer" && this.tag [1] == other.tag [1]) ||
		               (this.tag == "p2_healer" && this.tag [1] == other.tag [1])) && target == null) {
			healing = true;
			attacking = false;
			target = other.gameObject;
		} 
//		else if (other.tag == "carrot") {
//			health--;
//			Destroy (other);
//		}
        // else, minion sees its own healer. continue
    }

	void Attack()
	{
		if (target.GetComponent<Enemy>().invulnerable)
			target.GetComponent<Enemy> ().health -= 0;
		else
			target.GetComponent<Enemy> ().health -= dps;
		if (target.GetComponent<Enemy> ().health <= 0) 
		{
			Destroy (target);
			target = null;
			attacking = false;
		}

	}

    void Heal()
    { 
        //print("health:  " + target.GetComponent<Enemy>().health);
        target.GetComponent<Enemy>().health += 1.0f;
        if(target.GetComponent<Enemy>().health >= 5)
        {
            healing = false;
        }
        else if (target.GetComponent<Enemy>().health <= 0)
        {
            Destroy(target);
            target = null;
            healing = false;
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

    bool CheckIfAllyDead()
    {
        if (target == null)
        {
            healing = false;
            return true;
        }
        return false;
    }
}
