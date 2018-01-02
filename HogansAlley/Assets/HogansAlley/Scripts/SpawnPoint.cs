using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

    /*private void Awake()
    {
        PeopleSpawner.GetInstance().AddSpawnPoint(gameObject);
    }*/

    // Use this for initialization
    void Start () {
		PeopleSpawner.GetInstance().AddSpawnPoint(gameObject);
	}
	
	/*// Update is called once per frame
	void Update () {
		
	}*/
}
