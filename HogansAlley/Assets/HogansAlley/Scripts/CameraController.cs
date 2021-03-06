﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class CameraController : MonoBehaviour {

    // Time that each bullet will be kept before deleting it
    private const float LIFE_TIME = 3f;

    private ParticleSystem ps;
    private AudioSource audioSource;

    // Como de sensible es el raton
    public float camSens = 0.5f;

    public AudioClip shootSound;
    public AudioClip hitSound;

    // Keeps the last position of the mouse
    Vector3 lastMouse = new Vector3(255, 255, 255);

    // Prefab for the bullets /*It is not used anymore as RayCasting is used for shooting*/
    [SerializeField]
    GameObject bullet;

    Transform cameraTransform;

    private void Start()
    {
        cameraTransform = this.GetComponent<Transform>();

        ps = GetComponent<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update () {

        // Check if the VR device is connected. If it is NOT connected, rotate the camera using the mouse
		if(!XRSettings.enabled)
        {
            CameraPosition();
        }

        // Shoot when the button Fire1 is pressed. It will be needed to release the button and press it
        // again to Shoot again
        if (Input.GetButtonDown("Fire1"))
        {
            ShootABullet();
        }
	}

    // Set the direction where the camera is pointing to
    void CameraPosition()
    {
        // Calcular la diferencia desde la última posición del ratón con la posición actual
        Vector3 deltaMouse = Input.mousePosition - lastMouse;

        // Cambiado el orden del parametro porque se tiene en cuenta el eje sobre el que se rota
        Vector3 newMouse = new Vector3(-deltaMouse.y * camSens, deltaMouse.x * camSens, 0);

        // Obtener la nueva posicion
        Vector3 newMousePosition = new Vector3(transform.eulerAngles.x + newMouse.x, transform.eulerAngles.y + newMouse.y, 0);

        // Le asigna a la camara los angulos calculados
        transform.eulerAngles = newMousePosition;

        // Guarda la posicion actual para compararla luego
        lastMouse = Input.mousePosition;

    }

    // Shoot. Create a bullet and also destroys it after LIFE_TIME seconds
    void ShootABullet()
    {

        /* This was used when shooting bullets. It is kept only for completeness
         */

        /*GameObject bala = Instantiate(bullet, transform.position, transform.rotation);

        BulletHandler bh = bala.GetComponent<BulletHandler>();
        if (bh != null)
        {
            bh.Shoot(transform.forward);
        }
        else
        {
            Debug.Log("La bala no tiene el script BulletHandler");
        }

        // Destroy the bullet when the LIFE_TIME has expired
        Destroy(bala, LIFE_TIME);*/


        // Cast a Ray and store the object it hits
        RaycastHit shootHit;
        Ray shootRay = new Ray()
        {
            origin = cameraTransform.position,
            direction = cameraTransform.forward
        };

        Physics.Raycast(shootRay, out shootHit, 200f);

        // Play shot AudioClip
        audioSource.PlayOneShot(shootSound);

        Debug.Log("Shoting Rays");

        // Play the Particle System
        ps.Play();

        // Check if the Ray hits any object
        if (shootHit.collider != null)
        {
            // Play the Hit AudioClip
            audioSource.PlayOneShot(hitSound);

            // Check if the object it hits is any of the characters
            if (shootHit.collider.gameObject.tag == "Character")
            {
                // Call the character hit method to add the points and to stop the animation
                shootHit.collider.gameObject.GetComponent<GuyDie>().Hit();
                Debug.Log("Character reached");
            }
            else if (shootHit.collider.gameObject.tag == "Bullseye")
            {
                shootHit.collider.gameObject.GetComponent<CambioPosicionCamara>().Hit();
            }
        }
    }
}
