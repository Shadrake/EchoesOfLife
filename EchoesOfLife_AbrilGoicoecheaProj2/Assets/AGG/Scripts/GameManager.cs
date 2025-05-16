using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject controlsUI;
    public GameObject pauseUI;
    public GameObject player;  
    private bool isPaused = false; 

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                OpenPause();
            }
            else
            {
                ClosePause();
            }
        }
    }

    public void OpenPause()
    {
        pauseUI.SetActive(true); 
        Time.timeScale = 0f;
        isPaused = true; 

        if (player != null)
        {
            var playerController = player.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.enabled = false;  
            }
        }
    }

    public void ClosePause()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false; 

        if (player != null)
        {

            var playerController = player.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.enabled = true; 
            }
        }
    }
    public void OpenControls()
    {
        controlsUI.SetActive(true); 
    }

    public void CloseControls()
    {
        controlsUI.SetActive(false);
    }

    public void Lvl1Button()
    {
        SceneManager.LoadScene("scn_Lvl1");
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("scn_MainMenu");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
