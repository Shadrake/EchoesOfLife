using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossFight : MonoBehaviour
{
    public PlayerController playerController;
    public Animator _playerAnimator;
    public Rigidbody2D _playerRB;
    public Canvas bossCanvas;
    public BoxCollider2D _trigger;
    public GameObject boss;

    
    // Start is called before the first frame update
    void Start()
    {
        _playerRB = playerController.GetComponent<Rigidbody2D>();
        _trigger = GetComponent<BoxCollider2D>();
        _playerAnimator = _playerAnimator.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            StartCoroutine(StartBattle());
        }
    }

    IEnumerator StartBattle()
    {
        boss.SetActive(true);

        _playerAnimator.SetBool("Running", false);
        playerController.enabled = false;
        _playerRB.velocity = Vector2.zero;

        _trigger.enabled = false;
        yield return new WaitForSeconds(5f);
        bossCanvas.gameObject.SetActive(true);
        playerController.enabled = true;
    }
}
