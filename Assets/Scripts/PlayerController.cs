using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    UIController uiController;

    public GameObject target;

    public float baseSpeed = 0.1f;
    float moveSpeed;
    public float boostMulti = 2f;

	// Use this for initialization
	void Start ()
    {
        moveSpeed = baseSpeed;
        uiController = GameObject.FindGameObjectWithTag("UIController").GetComponent<UIController>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown("Submit"))
        {
            moveSpeed *= boostMulti;
        }
        else if (Input.GetButtonUp("Submit"))
        {
            moveSpeed = baseSpeed;
        }

        if (!uiController.isPaused)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
        }
	}

    private void OnTriggerEnter(Collider otherObject)
    {
        if (otherObject.tag == "Intersection")
        {
            IntersectionScript intersectionScript = otherObject.GetComponent<IntersectionScript>();
            if (intersectionScript.targets.Length > 1)
            {
                target = intersectionScript.targets[Random.Range(0, intersectionScript.targets.Length)];
            }
            else if (intersectionScript.targets.Length == 1)
            {
                target = intersectionScript.targets[0];
            }
            else
            {
                Debug.Log("There are no available targets!");
            }
        }
    }
}
