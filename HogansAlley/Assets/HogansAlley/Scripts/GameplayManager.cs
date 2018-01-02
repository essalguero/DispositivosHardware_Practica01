using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour {

    // Instance of the object to implement the Singleton
    private static GameplayManager instance;

    // Variables to control the time the user is playing
    private float timeZero;
    public float playingTime = 60f;

    // TextMesh showing the score and the current playing time
    public TextMesh textMesh;

    // Points the user is scoring in the current game
    public float points = 0f;


    // Variables needed to Spawn Characters

    // Type of Character that can be used during the game
    [SerializeField]
    GameObject[] PersonTypes;

    // Time range to define the min and max time elapsed before spawning a new character
    public float minSpawnTime = 4f;
    public float maxSpawnTime = 6f;

    // Controls the status of the game. Set to true once the game is finished
    private bool stop = false;

    // List of SpawnPoints registered (Observer Pattern)
    private List<GameObject> spawnPointsList = new List<GameObject>();

    // Sets the instance for the Singleton pattern
    private void Awake()
    {
        // If another instance of the objects exists (Not possible because it implements a Singleton) this object is destroyed
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        // Initialize the instance for the Singleton Pattern
        instance = this;
    }

    // Use this for initialization
    void Start () {

        timeZero = Time.realtimeSinceStartup;

        // Starts coroutine to spawn characters
        StartCoroutine(SpawnPeople());
    }

    // Coroutine that controls the instantiation of Characters
    IEnumerator SpawnPeople()
    {
        // Only spawn people while the variable stop is set to false
        while (!stop)
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

    // Update is called once per frame
    void Update () {
		if (Time.realtimeSinceStartup > (playingTime + timeZero))
        {
            // Once the playing time is over, the application returns to the menu screen
            SceneManager.LoadScene(0);

            // It gets the instance of the MainMenu object (Singleton) to add the score of the game to the scores list
            MainMenu mainMenu = MainMenu.GetInstance();
            mainMenu.AddNewScore(points);
        }

        textMesh.text = "Time: " + (int)(Time.realtimeSinceStartup - timeZero) + "\nScore: " + points;
	}

    // Return the instance of the Singleton
    public static GameplayManager GetInstance()
    {
        return instance;
    }

    // The SpawnPoints to be used during the game must register themselves (Observer Pattern)
    public void AddSpawnPoint(GameObject spawnPoint)
    {
        spawnPointsList.Add(spawnPoint);
    }
    
    // Stop spawning people
    public void Stop()
    {
        stop = true;
    }

}
