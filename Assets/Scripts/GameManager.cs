using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public Vector2 lastCheckpoint;
	public void RespawnPlayer() {
		Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		player.transform.position = lastCheckpoint;
	}

	public void SetLastCheckpoint(CheckPoint checkpoint) {
		lastCheckpoint = checkpoint.transform.position;
	}
}
