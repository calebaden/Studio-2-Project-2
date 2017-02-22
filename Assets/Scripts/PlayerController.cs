using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    UIController uiController;
    TunnelController tunnelController;

    Rigidbody rb;

    public float moveSpeed = 10;
    public float elevateSpeed = 8;
    float horizontal;
    float vertical;
    bool isElevator;

	// Use this for initialization
	void Start ()
    {
        uiController = GameObject.FindGameObjectWithTag("UIController").GetComponent<UIController>();
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        // Check if the game is paused or not
        if (!uiController.isPaused)
        {
            // Set the horizontal and vertical variables to the inputs multiplied by movespeed and delta time
            horizontal = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
            vertical = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

            transform.Translate(horizontal, 0, vertical);
        }

        if (isElevator && tunnelController)
        {
            float speed = (elevateSpeed * tunnelController.direction) * Time.deltaTime;
            transform.Translate(Vector3.up * speed);
        }
	}

    private void OnTriggerEnter(Collider otherObject)
    {
        if (otherObject.tag == "TunnelTrigger")
        {
            rb.useGravity = false;
            tunnelController = otherObject.GetComponent<TunnelController>();
            isElevator = true;
        }
    }

    private void OnTriggerExit(Collider otherObject)
    {
        if (otherObject.tag == "TunnelTrigger")
        {
            rb.useGravity = true;
            tunnelController = null;
            isElevator = false;
        }
    }
}
