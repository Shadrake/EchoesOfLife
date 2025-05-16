using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject controlsUI;
    public GameObject pauseUI;
    public GameObject player;

    [Header("Audio")]
    public AudioSource musicSource;
    public AudioClip mainMenuMusic;
    public AudioClip levelMusic;
    public AudioClip bossMusic;
    public AudioClip gameOverMusic;
    public AudioClip victoryMusic;

    private bool isPaused = false;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded; 
    }

    void Start()
    {
        PlayMusicForScene(SceneManager.GetActiveScene().name);
    }

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
        Time.timeScale = 1f;
        SceneManager.LoadScene("scn_Lvl1");
    }

    public void MainMenuButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("scn_MainMenu");
    }

    public void Exit()
    {
        Application.Quit();
    }

    // MÚSICA 

    public void PlayMusic(AudioClip clip)
    {
        if (musicSource == null || clip == null) return;

        if (musicSource.clip == clip) return;

        musicSource.Stop();
        musicSource.clip = clip;
        musicSource.Play();
    }

    void PlayMusicForScene(string sceneName)
    {
        if (sceneName == "scn_MainMenu")
        {
            PlayMusic(mainMenuMusic);
        }
        else if (sceneName == "scn_Lvl1")
        {
            PlayMusic(levelMusic);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayMusicForScene(scene.name);
    }

    public void OnBossFightStart()
    {
        PlayMusic(bossMusic);
    }

    public void OnGameOver()
    {
        PlayMusic(gameOverMusic);
    }

    public void OnVictory()
    {
        PlayMusic(victoryMusic);
    }
}