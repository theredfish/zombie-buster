using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBullet : MonoBehaviour {
	public IEnumerator Hit() {
		yield return new WaitForSeconds(0.5f);
		gameObject.SetActive(false);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Enemy") {
			other.gameObject.GetComponent<Enemy>().Kill();
			Destroy(other.gameObject);
		}
	}
}