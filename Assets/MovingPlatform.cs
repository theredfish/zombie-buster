using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {
	public Transform teleportPosition;

	void Awake() {
	}
	void OnCollisionEnter2D(Collision2D col)
    {
		Debug.Log(col.gameObject);
        if (col.gameObject.tag == "Player") {
			col.gameObject.GetComponent<Player>().transform.position = teleportPosition.position;
		}
    }
}
