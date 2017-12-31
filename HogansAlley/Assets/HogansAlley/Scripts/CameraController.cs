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

        if (Input.GetButton("Fire1"))
        {
            ShootABullet();
        }
	}

    void CameraPosition()
    {
        Vector3 deltaMouse = Input.mousePosition - lastMouse;

        Vector3 newMouse = new Vector3(-deltaMouse.y * camSens, deltaMouse.x * camSens, 0);
        Vector3 newMousePosition = new Vector3(transform.eulerAngles.x + newMouse.x, transform.eulerAngles.y + newMouse.y, 0);

        transform.eulerAngles = newMousePosition;

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
