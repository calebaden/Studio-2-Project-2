using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeadController : MonoBehaviour
{
    MeshRenderer meshRen;
    public TunnelController tunnelController;

    public float baseSpeed = 5;
    public float moveSpeed;
    int direction;

	// Use this for initialization
	void Start ()
    {
        meshRen = GetComponent<MeshRenderer>();
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
        if (otherObject.tag == "BeadVoid")
        {
            Destroy(gameObject);
        }

        if (otherObject.tag == "Finish")
        {
            meshRen.enabled = false;
        }
    }
    void OnTriggerExit (Collider otherObject)
    {
        if (otherObject.tag == "Finish")
        {
            meshRen.enabled = true;
        }
    } 
}
