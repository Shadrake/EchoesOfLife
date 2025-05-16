using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossEnd : MonoBehaviour
{
    [Header("Player components")]
    public PlayerController playerController;
    public Animator _playerAnimator;
    public Rigidbody2D _playerRB;
    public Animator _cameraFocus;

    [Header("End components")]
    public GameObject _endUI;

    void Start()
    {
        _playerAnimator = _playerAnimator.GetComponent<Animator>();
        _cameraFocus = _cameraFocus.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(EndBattle());
        }
    }

    IEnumerator EndBattle()
    {
        playerController.enabled = false;

        _cameraFocus.SetTrigger("Active");

        yield return new WaitForSeconds(2f);

        // Reproducir música de victoria
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            gameManager.OnVictory();
        }

        _endUI.SetActive(true);
    }
}
