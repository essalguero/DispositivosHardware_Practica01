using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHandler : MonoBehaviour {

    // Particle system to be used for firing or collision of the bullet
    public ParticleSystem hitPs;

    // Rigidbody of the bullet
    private Rigidbody rb;

    // Audio source for the bullet
    private AudioSource bulletAS;

    // SudioClips to be played
    public AudioClip shootSound;
    public AudioClip hitSound;

    // Force to be used when firing the bullet
    public float impulseMagnitud = 10f;

	// Use this for initialization
	void Start () {
        
        if (hitPs == null)
        {
            Debug.Log("No se encuentra el ParticleSystem de la bala");
        }

        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.Log("No se encuentra el Rigidbody de la bala");
        }

        bulletAS = GetComponent<AudioSource>();
        if (bulletAS == null)
        {
            Debug.Log("No se encuentra el AudioSource de la bala");
        }
	}


    public void Shoot(Vector3 direction)
    {
        // Initialize the bullet - Safety mechnism in case Start function was still not executed
        Start();

        // Play the sound when firing the weapon
        bulletAS.PlayOneShot(shootSound);

        // Actual "Throw" the bullet
        rb.AddForce(direction.normalized * impulseMagnitud, ForceMode.Impulse);
    }

    // Plays the corresponding sound to the collision of the bullet
    private void OnCollisionEnter(Collision collision)
    {
        hitPs.Play();
        bulletAS.PlayOneShot(hitSound);
    }
}
