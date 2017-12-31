using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour {

    private static GameplayManager instance;

    private float timeZero;
    public float playingTime = 60f;
    public TextMesh textMesh;

    public float points = 0f;

	// Use this for initialization
	void Start () {
        timeZero = Time.realtimeSinceStartup;
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.realtimeSinceStartup > (playingTime + timeZero))
        {
            Application.Quit();
        }

        textMesh.text = "Time: " + (int)(Time.realtimeSinceStartup - timeZero) + "\nScore: " + points;
	}

    public static GameplayManager GetInstance()
    {
        return instance;
    }
}
