using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelController : MonoBehaviour
{
    public GameObject[] threads;
    public GameObject beadObject;

    public int direction;
    float baseInterval = 0.025f;
    public float spawnInterval;
    float zOffset;

	// Use this for initialization
	void Start ()
    {
        zOffset = threads[0].transform.localScale.y * direction;
        Debug.Log(zOffset);
	}
	
	// Update is called once per frame
	void Update ()
    {
        SpawnBead();

        if (Input.GetKeyDown("f"))
        {
            direction = ChangeDirection(direction);
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
        newBead.transform.localPosition = threads[i].transform.localPosition - zOffVec;
        newBead.transform.localRotation = Quaternion.Euler(0, 0, 0);
        newBead.GetComponent<BeadController>().baseSpeed += i;
        newBead.GetComponent<BeadController>().tunnelController = this;
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
