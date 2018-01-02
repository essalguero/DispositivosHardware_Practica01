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

	// Use this for initialization
	void Start () {

        timeZero = Time.realtimeSinceStartup;

        // Initialize the instance for the Singleton Pattern
        instance = this;
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
}
