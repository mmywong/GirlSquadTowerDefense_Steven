using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){
		if (this.tag != other.tag) {
			other.GetComponent<Enemy> ().health--;
			Destroy (this);
		}
	}
}
