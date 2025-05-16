using System.Collections;
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
    public AudioClip screamBoss;
    public AudioClip bossDefeated;
    public AudioClip gameOverMusic;
    public AudioClip victoryMusic;

    private bool isPaused = false;

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        PlayMusicForScene(SceneManager.GetActiveScene().name);
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
                OpenPause();
            else
                ClosePause();
        }
    }

    public void OpenPause()
    {
        pauseUI?.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

        if (player != null)
        {
            var playerController = player.GetComponent<PlayerController>();
            if (playerController != null)
                playerController.enabled = false;
        }
    }

    public void ClosePause()
    {
        pauseUI?.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

        if (player != null)
        {
            var playerController = player.GetComponent<PlayerController>();
            if (playerController != null)
                playerController.enabled = true;
        }
    }

    public void OpenControls()
    {
        controlsUI?.SetActive(true);
    }

    public void CloseControls()
    {
        controlsUI?.SetActive(false);
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

    // ================================
    // AUDIO
    // ================================

    public void PlayMusic(AudioClip clip)
    {
        if (musicSource == null || clip == null) return;

        musicSource.Stop();
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void StopMusic()
    {
        if (musicSource != null)
        {
            musicSource.Stop();
            musicSource.clip = null;
        }
    }

    private void PlayMusicForScene(string sceneName)
    {
        if (sceneName == "scn_MainMenu")
            PlayMusic(mainMenuMusic);
        else if (sceneName == "scn_Lvl1")
            PlayMusic(levelMusic);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayMusicForScene(scene.name);
    }

    // ================================
    // EVENTOS DE JUEGO
    // ================================

    public void OnBossFightStart()
    {
        PlayMusic(bossMusic);
    }

    public void OnBossDefeated()
    {
        PlayMusic(bossDefeated);
    }

    public void OnGameOver()
    {
        PlayMusic(gameOverMusic);
    }

    public void OnVictory()
    {
        PlayMusic(victoryMusic);
    }

    // ================================
    // EFECTOS DE SONIDO (SFX)
    // ================================

    // 🎵 Rugido de muerte del jefe (no detiene música)
    public void PlayBossDefeatedSFX()
    {
        if (musicSource != null && bossDefeated != null)
        {
            musicSource.PlayOneShot(bossDefeated);
        }
    }

    // 🎵 Rugido de aparición del jefe (no detiene música)
    public void PlayBossScreamSFX()
    {
        if (musicSource != null && screamBoss != null)
        {
            musicSource.PlayOneShot(screamBoss);
        }
    }
}