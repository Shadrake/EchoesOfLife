using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject controlsUI;

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
    
    public void OpenControls()
    {
        controlsUI.SetActive(true);
    }

    public void CloseControls()
    {
        controlsUI.SetActive(false);
    }
}
