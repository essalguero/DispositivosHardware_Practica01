using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuyDie : MonoBehaviour {

    // Animator to control the animations of the characters
    public Animator anim;

    // Time the character will be on the screen
    public float minStayTime = 3f;
    public float maxStayTime = 5f;

    // Points the user will get if the character is hit. Negative value for "friendly" people
    public float pointsHit = 0f;
    public float pointsMiss = 0f;

    // Variables controlling the time the character is on the screen
    private float timeZero = 0f;
    private float waitTime = 0f;

    // Added Delegate method and event for the budguys to be able to shoot
    public delegate void ShootEventHandler(float points);
    public event ShootEventHandler OnShoot;

	// Use this for initialization
	void Start () {
        
        // Check there is an animator available
        if(anim == null)
        {
            Debug.Log("No encuentra el animator");
        }

        // Initialize timeZero with the time when the character was created
        timeZero = Time.realtimeSinceStartup;

        // Get how much time the character will be on the screen if not hit first
        waitTime = Random.Range(minStayTime, maxStayTime);
	}
	
	// Update is called once per frame
	void Update () {
        // The character has to be enabled to take into account the collision. If the elapsed time since the character was instantiated is
        // greated than the waiting time, the character dissapears
        if ((Time.realtimeSinceStartup >= (timeZero + waitTime)) && (this.enabled))
        {
            // Triggers the corresponding animation
            anim.SetTrigger("EndNow");

            // Raise the event OnShoot if there is any subscriber
            if (OnShoot != null)
            {
                OnShoot(pointsMiss);
            }


            // Set this character as not enabled
            this.enabled = false;

            Debug.Log("Elapsed time greater than waiting time");
        }
	}

    /* This method is now used to add the points to the score if a ray casted hits the character. It is also in charge of change the animation
     * from waiting to hiding
     * 
     * This method replaces the OnCollisionEnter used when the user was shooting bullets. The OnCollisionEnter method is commented at the end of this file
     * for completeness
    */
    public void Hit()
    {
        // Triggers the corresponding animation
        anim.SetTrigger("EndNow");

        // Add the corresponding points
        GameplayManager.GetInstance().points += this.pointsHit;

        // Set this character as not enabled
        this.enabled = false;

        Debug.Log("Collision");
    }

    /*// Detects collisions with other objects
    void OnCollisionEnter(Collision collision)
    {
        // Only take into account collisions with objects of type Bala. Also this character has to be enabled to take into account the collision
        if ((collision.gameObject.name == "Bala(Clone)") && (this.enabled))
        {
            // Triggers the corresponding animation
            anim.SetTrigger("EndNow");

            // Add the corresponding points
            GameplayManager.GetInstance().points += this.pointsHit;

            // Set this character as not enabled
            this.enabled = false;
            
            Debug.Log("Collision");
        }
    }*/

    


}
