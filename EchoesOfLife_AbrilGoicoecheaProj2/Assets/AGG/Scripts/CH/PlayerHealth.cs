using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public PlayerController _playerController;
    public float playerHealth;
    public float playerMaxHealth;
    public bool isInmune;
    public float inmuneTime;
    private Rigidbody2D _playerRB;

    [Header("UI Settings")]
    public Image healthImg;
    public GameObject gameOver;

    [Header("Components")]
    public Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = playerMaxHealth;
        _animator = GetComponent<Animator>();
        _playerController = GetComponent<PlayerController>();
        _playerRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        healthImg.fillAmount = playerHealth / playerMaxHealth;

        if (playerHealth > playerMaxHealth)
        {
            playerHealth = playerMaxHealth;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && !isInmune)
        {

            playerHealth -= collision.GetComponentInParent<Enemy>().damage;
            StartCoroutine(Inmunity());

            if (playerHealth <= 0)
            {
                StartCoroutine(GameOver());
            }

        }
    }

    IEnumerator Inmunity()
    {
        _animator.SetTrigger("Damaged");
        isInmune = true;

        yield return new WaitForSeconds(inmuneTime);
        isInmune = false;
    }

    private IEnumerator GameOver()
    {
        _animator.SetTrigger("Die");
        _playerRB.velocity = Vector2.zero;

        _playerController.enabled = false;
        yield return new WaitForSeconds(3f);
        gameOver.SetActive(true);
    }
}