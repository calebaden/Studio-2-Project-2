using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelController : MonoBehaviour
{
    AudioController audioController;

    public GameObject[] threads;
    public GameObject beadObject;

    public int direction;
    public float spawnInterval;
    public float shiftCooldown;
    public float shiftTimer;
    float zOffset = 20;

	// Use this for initialization
	void Start ()
    {
        audioController = GameObject.FindGameObjectWithTag("UIController").GetComponent<AudioController>();
        zOffset *= direction;
	}
	
	// Update is called once per frame
	void Update ()
    {
        SpawnBead();

        if (shiftTimer > 0)
        {
            shiftTimer -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Submit") && shiftTimer <= 0)
        {
            audioController.PlayShiftClip();
            direction = ChangeDirection(direction);
            shiftTimer += shiftCooldown;
        }
	}


    // Function that spawns a bead
    void SpawnBead ()
    {
        if (spawnInterval > 0)
        {
            spawnInterval -= Time.deltaTime;
        }
        else
        {
            for (int i = 0; i < threads.Length; i++)
            {
                StartCoroutine(waitSeconds(Random.Range(0.5f, 1f), i));
                spawnInterval = 0.2f;
            }
        }
    }

    IEnumerator waitSeconds (float seconds, int i)
    {
        yield return new WaitForSeconds(seconds);

        Vector3 zOffVec = new Vector3(0, 0, zOffset);
        GameObject newBead = Instantiate(beadObject, transform);
        BeadController bCon = newBead.GetComponent<BeadController>();
        newBead.transform.localPosition = threads[i].transform.localPosition - zOffVec;
        newBead.transform.localRotation = Quaternion.Euler(0, 0, 0);
        bCon.baseSpeed += i;
        bCon.tunnelController = this;
    }

    public int ChangeDirection (int dir)
    {
        if (dir == 1)
        {
            dir = -1;
        }
        else if (dir == -1)
        {
            dir = 1;
        }
        else
        {
            Debug.Log("Direction is 0");
        }

        zOffset *= -1;
        return dir;
    }
}
