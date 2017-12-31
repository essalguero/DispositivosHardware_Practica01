using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHandler : MonoBehaviour {

    public ParticleSystem hitPs;
    private Rigidbody rb;
    private AudioSource bulletAS;

    public AudioClip shootSound;
    public AudioClip hitSound;
    public float impulseMagnitud = 10f;

	// Use this for initialization
	void Start () {
        //hitPs = GetComponent<ParticleSystem>();
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
	
	// Update is called once per frame
	/*void Update () {
		
	}*/

    public void Shoot(Vector3 direction)
    {
        Start();
        bulletAS.PlayOneShot(shootSound);
        rb.AddForce(direction.normalized * impulseMagnitud, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        hitPs.Play();
        bulletAS.PlayOneShot(hitSound);
    }
}
