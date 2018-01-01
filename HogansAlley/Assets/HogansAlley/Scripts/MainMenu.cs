using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UI;


public class MainMenu : MonoBehaviour {

    private static MainMenu instance;

    private List<float> scoreList = new List<float>();

    public Text[] scoreTextFields;
    public Canvas menuCanvas;

    bool updated = false;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (FindObjectsOfType(GetType()).Length > 1)
        {
            MainMenu.GetInstance().ActivateCanvas();
            Destroy(gameObject);
            return;
        }

        instance = this;

        for(int i = 0; i < scoreTextFields.Length; i++)
        {
            AddNewScore(float.MinValue);
        }

        updated = true;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (updated)
        {
            if (scoreList[scoreList.Count - 1] == float.MinValue)
            {
                scoreTextFields[0].text = "1. -";
            }
            else
            {
                scoreTextFields[0].text = "1. " + scoreList[scoreList.Count - 1].ToString();
            }

            if (scoreList[scoreList.Count - 2] == float.MinValue)
            {
                scoreTextFields[1].text = "2. -";
            }
            else
            {
                scoreTextFields[1].text = "2. " + scoreList[scoreList.Count - 2].ToString();
            }

            if (scoreList[scoreList.Count - 3] == float.MinValue)
            {
                scoreTextFields[2].text = "3. -";
            }
            else
            {
                scoreTextFields[2].text = "3. " + scoreList[scoreList.Count - 3].ToString();
            }

            updated = false;
        }
	}

    public void LoadScene()
    {
        menuCanvas.enabled = false;
        SceneManager.LoadScene(1);
    }

    public void AddNewScore(float newScore)
    {
        scoreList.Add(newScore);

        scoreList.Sort();

        updated = true;
    }

    public static MainMenu GetInstance()
    {
        return instance;
    }

    public void ActivateCanvas()
    {
        menuCanvas.enabled = true;
    }
}
