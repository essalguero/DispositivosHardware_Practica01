using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleSpawner : MonoBehaviour {

    [SerializeField]
    GameObject[] SpawnPoints;
    [SerializeField]
    GameObject[] PersonTypes;

    public float minSpawnTime = 1f;
    public float maxSpawnTime = 5f;

    private bool stop = false;

	// Use this for initialization
	void Start () {
        StartCoroutine(SpawnPeople());
	}
	
	// Update is called once per frame
	/*void Update () {
		
	}*/

    IEnumerator SpawnPeople()
    {
        while(!stop)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));

            int personType = Random.Range(0, PersonTypes.Length);
            int spawnPoint = Random.Range(0, SpawnPoints.Length);
            Transform spawnTransform = SpawnPoints[spawnPoint].transform;
            Instantiate(PersonTypes[personType], spawnTransform);
        }
    }

    public void Stop()
    {
        stop = true;
    }
}
