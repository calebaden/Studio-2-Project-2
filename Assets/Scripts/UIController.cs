using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    AudioController audioController;

    int selectNum;
    public Image selectImg;
    int sceneIndex;
    public GameObject pauseElements;
    public bool isPaused = false;
    public int buttonSum;
    public float buttonDelay;
    public Slider audioSlider;
    public GameObject credits;
    bool creditsOn;

    // Use this for initialization
    void Start ()
    {
        audioController = GetComponent<AudioController>();

        audioSlider.value = PlayerPrefs.GetFloat("volume");

        // Reset time scale to 1 if it is not 1
        if (Time.timeScale != 1)
            Time.timeScale = 1;

        // Set the scene index to the build index
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        // Disable the pause menu on the level scene
        if (sceneIndex == 1)
        {
            pauseElements.SetActive(false);
        }
        else
        {
            credits.SetActive(false);
            creditsOn = false;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (audioSlider)
        {
            audioController.volume = audioSlider.value;
        }

        if (isPaused)
        {
            MenuNavigation();
        }
        

        // Call the appropriate update fuction depending on the current scene
        if (sceneIndex == 0)
        {
            MainMenuControls();
        }
        else
        {
            InLevelControls();
        }

        // Set the selection image position relative to the select number
        if (selectImg)
        {
            Vector3 selectImagePos = selectImg.transform.localPosition;
            selectImagePos.y = selectNum * -200;
            selectImg.transform.localPosition = selectImagePos;
        }
    }

    void MenuNavigation ()
    {
        // If the player tries to navigate down, increment the select number
        if (Input.GetButtonDown("Down") && selectNum < buttonSum - 1)
        {
            audioController.PlayNavClip();
            selectNum++;
        }
        // If the player tries to navigate up, decrement the select number
        if (Input.GetButtonDown("Up") && selectNum > 0)
        {
            audioController.PlayNavClip();
            selectNum--;
        }
    }
    // Function that controls main menu update
    void MainMenuControls ()
    {
        // Ensure the select number is clamped between the min and max number of buttons
        selectNum = Mathf.Clamp(selectNum, 0, buttonSum - 1);

        // If the player presses the submit button, check what the select number is and call the appropriate function
        if (Input.GetButtonDown("Submit"))
        {
            int selected = selectNum;
            StartCoroutine(MenuButtonDelay(selected));
        }
    }

    IEnumerator MenuButtonDelay (int selected)
    {
        selectNum = selected;
        audioController.PlayButtonClip();
        yield return new WaitForSeconds(buttonDelay);
        if (selected == 0)
        {
            OnStartClick();
        }
        else if (selected == 1)
        {
            OnCreditsClick();
        }
        else if (selected == 2)
        {
            OnQuitClick();
        }
    }

    // Function that controls in level update
    void InLevelControls ()
    {
        // Ensure the select number is clamped between the min and max number of buttons
        selectNum = Mathf.Clamp(selectNum, 0, buttonSum - 1);

        // If the player presses the submit button, check what the select number is and call the appropriate function
        if (Input.GetButtonDown("Submit") && isPaused)
        {
            Time.timeScale = 1;
            int selected = selectNum;
            StartCoroutine(LevelButtonDelay(selected));
        }

        // When the player presses cancel, call the pause function
        if (Input.GetButtonDown("Cancel"))
        {
            PauseGame(isPaused);
        }
    }

    IEnumerator LevelButtonDelay(int selected)
    {
        audioController.PlayButtonClip();
        yield return new WaitForSeconds(buttonDelay);
        if (selected == 0)
        {
            OnResumeClick();
        }
        else if (selected == 1)
        {
            OnReturnClick();
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
            selectNum = 0;
            Cursor.lockState = CursorLockMode.None;
            isPaused = true;
            pauseElements.SetActive(true);
            Time.timeScale = 0;
        }
    }

    // Function that loads the level scene
    public void OnStartClick ()
    {
        PlayerPrefs.SetFloat("volume", audioController.volume);
        SceneManager.LoadScene(1);
    }

    // Function that shows the credits
    public void OnCreditsClick ()
    {
        if (creditsOn)
        {
            credits.SetActive(false);
            creditsOn = false;
        }
        else
        {
            credits.SetActive(true);
            creditsOn = true;
        }
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
        Time.timeScale = 1;
    }

    // Function that loads the main menu
    public void OnReturnClick ()
    {
        PlayerPrefs.SetFloat("volume", audioController.volume);
        SceneManager.LoadScene(0);
    }
}
