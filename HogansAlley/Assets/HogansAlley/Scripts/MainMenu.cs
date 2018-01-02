using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UI;


public class MainMenu : MonoBehaviour {

    // Instance to implement the Singleton pattern
    private static MainMenu instance;

    // List with all the scores since the start of the game
    private List<float> scoreList = new List<float>();

    // Variables to show the 3 greatest scores
    public Text[] scoreTextFields;
    public Canvas menuCanvas;

    // Registers if a new score has been included in the list
    bool updated = false;

    // Executed at the creation of the Object
    private void Awake()
    {
        // Mark the object as persistent between scenes as it stores the Scores list
        DontDestroyOnLoad(gameObject);

        // Check if another instance already exists
        if (instance != null)
        {
            // Activate the canvas showing the Scores list
            MainMenu.GetInstance().ActivateCanvas();

            // When returning from other Scenes a new instance will be created, BUT as this objects needs to be unique, the new objects are destroyed
            Destroy(gameObject);
            return;
        }

        // Keeps the instance reference for the Singleton pattern
        instance = this;

        // Initialize the list of higest scores
        for(int i = 0; i < scoreTextFields.Length; i++)
        {
            AddNewScore(float.MinValue);
        }

        // Marks that the Scores list has been updated
        updated = true;
    }
	
	// Update is called once per frame
	void Update () {
        // Only need to update the list if a new score has been added
		if (updated)
        {
            // The Menu is displaying the 3 higest scores of the list
            for (int i = 0, j = scoreList.Count - 1; i < 3; ++i, --j)
            {
                // If less than 3 games have been played, the list is not full with correct values. Therefore display a '-' character
                if (scoreList[j] == float.MinValue)
                {
                    scoreTextFields[i].text = (i + 1).ToString() + ". -";
                }
                else
                {
                    scoreTextFields[i].text = (i + 1).ToString() + ". " + scoreList[j].ToString();
                }
            }

            // Marks that the list has been updated already
            updated = false;
        }
	}

    // Load the "Game" scene
    public void LoadScene()
    {
        menuCanvas.enabled = false;
        SceneManager.LoadScene(1);
    }

    // Adds a new score to the scoresList
    public void AddNewScore(float newScore)
    {
        scoreList.Add(newScore);

        // Shorts the list as it the menu is only listing the 3 higest scores
        scoreList.Sort();

        // Marks that the list is updated and has to be rendered again in the screen
        updated = true;
    }

    // Return the Singleton instance
    public static MainMenu GetInstance()
    {
        return instance;
    }

    // Sets the canvas active - Used when returned from other scenes
    public void ActivateCanvas()
    {
        menuCanvas.enabled = true;
    }
}
