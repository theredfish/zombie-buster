using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {
	public int level;
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			GameObject.Find("GameManager").GetComponent<GameManager>().SetLastCheckpoint(gameObject.GetComponent<CheckPoint>());
		}
	}
}
