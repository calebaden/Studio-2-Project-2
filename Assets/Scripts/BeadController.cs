using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeadController : MonoBehaviour
{
    public TunnelController tunnelController;

    public float baseSpeed = 5;
    public float moveSpeed;
    int direction;

	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        direction = tunnelController.direction;
        moveSpeed = baseSpeed * direction;

        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
	}

    void OnTriggerEnter (Collider otherObject)
    {
        // When the object collides with the Bead Void, destroy this game object
        if (otherObject.tag == "BeadVoid")
        {
            Destroy(gameObject);
        }
    }
}
