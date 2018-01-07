using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambioPosicionCamara : MonoBehaviour {

    public Transform newPosition;
    public Transform cameraTransform;

    public int minSpawnPoint;
    public int maxSpawnPoint;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Hit()
    {
        GameplayManager.GetInstance().setActiveZone(newPosition, minSpawnPoint, maxSpawnPoint);
        //cameraTransform.position = newPosition.position;
    }
}
