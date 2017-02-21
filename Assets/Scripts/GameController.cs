using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public bool isPaused = false;

	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Check if the player presses the cancel input
		if (Input.GetButtonDown("Cancel"))
        {
            // If the game is already paused, unpause it
            if (isPaused)
            {
                Time.timeScale = 1;
                isPaused = false;
            }
            // If the game is not paused, pause it
            else
            {
                Time.timeScale = 0;
                isPaused = true;
            }
        }
	}
}
