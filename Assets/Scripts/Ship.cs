using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public Camera cam;
    public GameObject ship;
    public Rigidbody shipRigidbody;
    public Vector3 shipVelocity;

    public float topOfScreen;
    public float bottomOfScreen;

    public float rightOfScreen;
    public float leftOfScreen;

    void Start()
    {
        ship = this.gameObject;
        shipRigidbody = ship.GetComponent<Rigidbody>();
        
        topOfScreen = Camera.main.orthographicSize;
        bottomOfScreen = -Camera.main.orthographicSize;

        rightOfScreen = (topOfScreen) * Camera.main.aspect;
        leftOfScreen = -((topOfScreen) * Camera.main.aspect);

    }

    // Update is called once per frame
    void Update()
    {
        shipVelocity = shipRigidbody.velocity;
        if (shipVelocity.y > 6.0f) shipRigidbody.velocity = new Vector3(shipVelocity.x, 6.0f, shipVelocity.z);
        if (shipVelocity.y < -8.0f) shipRigidbody.velocity = new Vector3(shipVelocity.x, -8.0f, shipVelocity.z);

        BoundariesCheck();

        if (Input.touchCount > 0)
        {
            Movement();
        }
    }

    public void Movement()
    {
        if (Input.GetTouch(0).phase == TouchPhase.Began && shipRigidbody.velocity.y < 0.0f) shipRigidbody.velocity = Vector3.up * 3;
        if (Input.GetTouch(0).position.x > Screen.width / 2) shipRigidbody.AddForce(Vector3.right * 5);
        if (Input.GetTouch(0).position.x < Screen.width / 2) shipRigidbody.AddForce(Vector3.left * 5);

        if (Input.GetTouch(0).phase == TouchPhase.Ended && shipRigidbody.velocity.y > 3.0f) shipRigidbody.velocity = Vector3.zero;
        //if (Input.GetTouch(0).phase == TouchPhase.Ended && shipRigidbody.velocity.y < -3.0f) shipRigidbody.velocity = Vector3.zero;
        shipRigidbody.AddForce(Vector3.up * 5);
    }

    public void BoundariesCheck()
    {
        if (ship.gameObject.transform.position.y > topOfScreen)
        {
            ship.transform.position = new Vector3(ship.transform.position.x, topOfScreen, ship.transform.position.z);
            if (shipRigidbody.velocity.y > Vector3.zero.y) shipRigidbody.velocity = Vector3.zero;
        }

        if (ship.gameObject.transform.position.y < bottomOfScreen)
        {
            ship.transform.position = new Vector3(ship.transform.position.x, bottomOfScreen, ship.transform.position.z);
            if (shipRigidbody.velocity.y < Vector3.zero.y) shipRigidbody.velocity = Vector3.zero;
        }

        if (ship.gameObject.transform.position.x > rightOfScreen)
        {
            ship.transform.position = new Vector3(rightOfScreen, ship.transform.position.y, ship.transform.position.z);
            if (shipVelocity.x > Vector3.zero.x) shipRigidbody.velocity = new Vector3(0, shipVelocity.y, shipVelocity.z);
        }

        if (ship.gameObject.transform.position.x < leftOfScreen)
        {
            ship.transform.position = new Vector3(leftOfScreen, ship.transform.position.y, ship.transform.position.z);
            if (shipVelocity.x < Vector3.zero.x) shipRigidbody.velocity = new Vector3(0, shipVelocity.y, shipVelocity.z);
        }

    }


    private void OnCollisionEnter(Collision collision)
    {
        Destroy(ship);
    }

}
