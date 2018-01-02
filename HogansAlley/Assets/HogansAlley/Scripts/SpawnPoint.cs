using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

    // Use this for initialization. The spawnPoint subscribe itself to the list of available SpawnPoints
    void Start () {
		GameplayManager.GetInstance().AddSpawnPoint(gameObject);
	}
}
