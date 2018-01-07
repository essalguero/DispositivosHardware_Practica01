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

    // Camera transform
    public Transform cameraTransform;

    // TextMesh showing the score and the current playing time
    public TextMesh textMesh;

    // Points the user is scoring in the current game
    public float points = 0f;


    // Variables needed to Spawn Characters

    // Type of Character that can be used during the game
    [SerializeField]
    GameObject[] PersonTypes;

    [SerializeField]
    public GameObject SpawPointsHolder;

    // Time range to define the min and max time elapsed before spawning a new character
    public float minSpawnTime = 4f;
    public float maxSpawnTime = 6f;

    public Transform zonePosition;
    public int minSpawnPoint;
    public int maxSpawnPoint;

    // Controls the status of the game. Set to true once the game is finished
    private bool stop = false;

    // List of SpawnPoints registered (Observer Pattern)
    private List<GameObject> spawnPointsList = new List<GameObject>();

    private List<GameObject> spawnPointInUseList = new List<GameObject>();

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

        
        setActiveZone(zonePosition, minSpawnPoint, maxSpawnPoint);

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
            
            int spawnPoint = 0;
            do
            {
                // Select randomly the SpawnPoint where the character will be placed
                spawnPoint = Random.Range(0, spawnPointsList.Count);
            } while ((!spawnPointsList[spawnPoint].activeSelf) || (spawnPointInUseList.Contains(spawnPointsList[spawnPoint])));

            spawnPointInUseList.Add(spawnPointsList[spawnPoint]);

            Transform spawnTransform = spawnPointsList[spawnPoint].transform;

            // Spawn the character - Instantiate
            GameObject character = (GameObject) Instantiate(PersonTypes[personType], spawnTransform);

            GuyDie gdScript = character.GetComponentInChildren<GuyDie>();

            // If the character creater is bad, register the function BadGuyShoot in the event OnShoot (delegates) in GuyDie
            if (PersonTypes[personType].name.ToLower().Contains("bad"))
            {
                
                gdScript.OnShoot += BadGuyShoot;
            }

            gdScript.setSpawmPoint(spawnPointsList[spawnPoint]);
        }
    }

    // Function to be called when the badguys shot. It is called using a delegate function and an event in GuyDie
    private void BadGuyShoot(float shootPoints)
    {
        points += shootPoints;
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

    public void setActiveZone(Transform zoneTransform, int minSpawnPoint, int maxSpawnPoint)
    {

        cameraTransform.position = zoneTransform.position;

        foreach (GameObject spawnPoint in spawnPointsList)
        {
            List<GameObject> childGameObjectList = new List<GameObject>(spawnPoint.transform.childCount);
            if (childGameObjectList.Count > 0)
                Debug.Log("This spawnPoint has children");

            foreach (GameObject characterObject in childGameObjectList)
            {
                characterObject.SetActive(false);
                DestroyImmediate(characterObject);
            }

            int valorSpawnPoint;
            string stringNumeroSpawnPoint = spawnPoint.name.Substring(spawnPoint.tag.Length, 2);
            int.TryParse(stringNumeroSpawnPoint, out valorSpawnPoint);

            if (valorSpawnPoint < minSpawnPoint || valorSpawnPoint > maxSpawnPoint)
            {
                spawnPoint.SetActive(false);
            }
            else
            {
                spawnPoint.SetActive(true);
            }
        }

    }

    public void Finished(GuyDie guyDie)
    {
        spawnPointInUseList.Remove(guyDie.GetSpawnPoint());
    }

}
