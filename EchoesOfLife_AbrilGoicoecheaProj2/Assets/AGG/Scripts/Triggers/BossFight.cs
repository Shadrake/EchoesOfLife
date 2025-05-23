﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossFight : MonoBehaviour
{
    public BoxCollider2D _trigger;

    [Header("Player components")]
    public PlayerController playerController;
    public Animator _playerAnimator;
    public Rigidbody2D _playerRB;
    public Animator _cameraFocus;

    [Header("Boss Components")]
    public Canvas bossCanvas;
    public GameObject boss;

    [Header("Rock Components")]
    public GameObject rockSpawner;

    void Start()
    {
        _playerRB = playerController.GetComponent<Rigidbody2D>();
        _trigger = GetComponent<BoxCollider2D>();
        _playerAnimator = _playerAnimator.GetComponent<Animator>();
        _cameraFocus = _cameraFocus.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(StartBattle());
        }
    }

    IEnumerator StartBattle()
    {
        FindObjectOfType<GameManager>().OnBossFightStart();

        boss.SetActive(true);

        _playerAnimator.SetBool("Running", false);
        playerController.enabled = false;
        _playerRB.velocity = Vector2.zero;

        _cameraFocus.SetTrigger("Active");

        // 🔊 Rugido al segundo 3.33
        Invoke(nameof(PlayBossIntroRoar), 3.33f);

        _trigger.enabled = false;
        yield return new WaitForSeconds(5f);

        bossCanvas.gameObject.SetActive(true);
        playerController.enabled = true;
        rockSpawner.SetActive(true);
    }

    void PlayBossIntroRoar()
    {
        GameManager gm = FindObjectOfType<GameManager>();
        if (gm != null)
        {
            gm.PlayBossScreamSFX();
        }
    }
}