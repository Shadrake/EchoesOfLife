using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class movementLock : MonoBehaviour
{
    public PlayerController playerController;
    public Rigidbody2D _playerRB;
    public Image cooldownBar;
    public BoxCollider2D _trigger;
    public Animator _playerAnimator;

    private void Start()
    {
        _playerRB = playerController.GetComponent<Rigidbody2D>();
        _trigger = GetComponent<BoxCollider2D>();
        _playerAnimator = playerController.GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            StartCoroutine(Unlock());
        }
    }

    IEnumerator Unlock()
    {
        _playerAnimator.SetTrigger("PowerUp");

        playerController.enabled = false;
        _playerRB.velocity = Vector2.zero;

        _trigger.enabled = false;
        yield return new WaitForSeconds(5f);
        cooldownBar.gameObject.SetActive(true);
        playerController.enabled = true;
    }
}
