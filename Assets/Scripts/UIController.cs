using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    int selectNum;
    float selectTimer;
    float cooldown = 0.2f;
    public Image selectImg;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (selectTimer > 0)
        {
            selectTimer -= Time.deltaTime;
        }

        if (Input.GetKey("s") && selectTimer <= 0)
        {
            selectNum++;
            selectTimer = cooldown;
        }
        if (Input.GetKey("w") && selectTimer <= 0)
        {
            selectNum--;
            selectTimer = cooldown;
        }

        if (Input.GetKeyDown("return"))
        {
            if (selectNum == 0)
            {
                OnStartClick();
            }
            else if (selectNum == 1)
            {
                OnCreditsClick();
            }
            else if (selectNum == 2)
            {
                OnQuitClick();
            }
        }

        selectNum = Mathf.Clamp(selectNum, 0, 2);

        Vector3 selectImagePos = selectImg.transform.localPosition;
        selectImagePos.y = selectNum * -200;
        selectImg.transform.localPosition = selectImagePos;

	}

    // Function that loads the level scene
    public void OnStartClick ()
    {
        SceneManager.LoadScene(1);
    }

    // Function that shows the credits
    public void OnCreditsClick ()
    {
        // Show credits here
    }

    // Function that closes the application
    public void OnQuitClick ()
    {
        Application.Quit();
    }

    // Function that changes the highlighted button
    public void OnMouseHover ()
    {

    }
}
