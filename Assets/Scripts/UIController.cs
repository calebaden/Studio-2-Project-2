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
    int sceneIndex;
    public GameObject pauseElements;
    public bool isPaused = false;

    // Use this for initialization
    void Start ()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (sceneIndex == 0)
        {

        }
        else
        {
            pauseElements.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        MenuNavigation();

        if (sceneIndex == 0)
        {
            MainMenuControls();
        }
        else
        {
            InLevelControls();
        }

        if (selectImg)
        {
            Vector3 selectImagePos = selectImg.transform.localPosition;
            selectImagePos.y = selectNum * -200;
            selectImg.transform.localPosition = selectImagePos;
        }
    }

    // Function that handles menu navigation
    void MenuNavigation ()
    {
        if (selectTimer > 0)
        {
            selectTimer -= Time.deltaTime;
        }

        if (Input.GetAxis("Vertical") < 0 && selectTimer <= 0)
        {
            selectNum++;
            selectTimer = cooldown;
        }
        if (Input.GetAxis("Vertical") > 0 && selectTimer <= 0)
        {
            selectNum--;
            selectTimer = cooldown;
        }
    }

    // Function that controls main menu update
    void MainMenuControls ()
    {
        selectNum = Mathf.Clamp(selectNum, 0, 2);

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
    }

    // Function that controls in level update
    void InLevelControls ()
    {
        selectNum = Mathf.Clamp(selectNum, 0, 1);

        if (Input.GetButtonDown("Submit"))
        {
            if (selectNum == 0)
            {
                OnResumeClick();
            }
            else if (selectNum == 1)
            {
                OnReturnClick();
            }
        }

        if (Input.GetButtonDown("Cancel"))
        {
            PauseGame(isPaused);
        }
    }

    // Function that handles pausing the level
    void PauseGame (bool pauseCheck)
    {
        if (pauseCheck)
        {
            OnResumeClick();
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            isPaused = true;
            pauseElements.SetActive(true);
            //Time.timeScale = 0;
        }
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

    // Function that resumes the level
    public void OnResumeClick ()
    {
        isPaused = false;
        pauseElements.SetActive(false);
        //Time.timeScale = 1;
    }

    // Function that loads the main menu
    public void OnReturnClick ()
    {
        SceneManager.LoadScene(0);
    }

    // Function that changes the highlighted button
    public void OnMouseHover ()
    {

    }
}
