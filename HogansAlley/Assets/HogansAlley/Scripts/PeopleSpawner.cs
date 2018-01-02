using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleSpawner : MonoBehaviour {

    // Instance for the Singleton Pattern implementation
    private static PeopleSpawner instance;

    // Type of Character that can be used during the game
    [SerializeField]
    GameObject[] PersonTypes;

    public float minSpawnTime = 1f;
    public float maxSpawnTime = 5f;

    // Controls the status of the game. Set to true once the game is finished
    private bool stop = false;

    // List of SpawnPoints registered (Observer Pattern)
    private List<GameObject> spawnPointsList = new List<GameObject>();

    // Register the instance to implement the Singleton
    private void Awake()
    {
        // If there is already another instance (Singleton already created) delete this one.
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        // Register the Singleton Object
        instance = this;
    }

    // Use this for initialization
    void Start () {
        StartCoroutine(SpawnPeople());
	}

    // Coroutine that controls the instantiation of Characters
    IEnumerator SpawnPeople()
    {
        // Only spawn people while the variable stop is set to false
        while(!stop)
        {
            // Wait a random number of seconds before spawning a character
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));

            // Select the character to be spawned
            int personType = Random.Range(0, PersonTypes.Length);

            // Select randomly the SpawnPoint where the character will be placed
            int spawnPoint = Random.Range(0, spawnPointsList.Count);
            Transform spawnTransform = spawnPointsList[spawnPoint].transform;

            // Spawn the character - Instantiate
            Instantiate(PersonTypes[personType], spawnTransform);
        }
    }

    // Stop spawning people
    public void Stop()
    {
        stop = true;
    }

    // Return the instance of the singleton
    public static PeopleSpawner GetInstance()
    {
        return instance;
    }

    // The SpawnPoints to be used during the game must register themselves (Observer Pattern)
    public void AddSpawnPoint(GameObject spawnPoint)
    {
        spawnPointsList.Add(spawnPoint);
    }
}
