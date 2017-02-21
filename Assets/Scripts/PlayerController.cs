using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    GameController gameController;

    public float moveSpeed = 10;
    float horizontal;
    float vertical;

	// Use this for initialization
	void Start ()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        // Check if the game is paused or not
        if (!gameController.isPaused)
        {
            // Set the horizontal and vertical variables to the inputs multiplied by movespeed and delta time
            horizontal = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
            vertical = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

            transform.Translate(horizontal, 0, vertical);
        }
	}
}
