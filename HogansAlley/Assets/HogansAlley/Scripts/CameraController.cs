using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class CameraController : MonoBehaviour {

    public float camSens = 0.5f; // Como de sensible es el raton
    Vector3 lastMouse = new Vector3(255, 255, 255);
    [SerializeField]
    GameObject bullet;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update () {
		if(!XRSettings.enabled)
        {
            CameraPosition();
        }

        if (Input.GetButtonDown("Fire1"))
        {
            ShootABullet();
        }
	}

    void CameraPosition()
    {
        // Calcular la diferencia desde la última posición del ratón con la posición actual
        Vector3 deltaMouse = Input.mousePosition - lastMouse;

        // Cambiado el orden del parametro porque se tiene en cuenta el eje sobre el que se rota
        Vector3 newMouse = new Vector3(-deltaMouse.y * camSens, deltaMouse.x * camSens, 0);


        Vector3 newMousePosition = new Vector3(transform.eulerAngles.x + newMouse.x, transform.eulerAngles.y + newMouse.y, 0);

        // Le asigna a la camara los angulos calculados
        transform.eulerAngles = newMousePosition;

        // Guarda la posicion actual para compararla luego
        lastMouse = Input.mousePosition;

    }

    void ShootABullet()
    {
        GameObject bala = Instantiate(bullet, transform.position, transform.rotation);

        BulletHandler bh = bala.GetComponent<BulletHandler>();
        if (bh != null)
        {
            bh.Shoot(transform.forward);
        }
        else
        {
            Debug.Log("La bala no tiene el script BulletHandler");
        }

        Destroy(bala, 3f);
    }
}
