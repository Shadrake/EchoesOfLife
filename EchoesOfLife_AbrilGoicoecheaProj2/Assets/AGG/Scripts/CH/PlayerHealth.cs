using System.Collections;
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
    public CapsuleCollider2D _capsuleCollider;
    private bool isDead = false;

    [Header("UI Settings")]
    public Image healthImg;
    public GameObject gameOver;

    [Header("Components")]
    public Animator _animator;

    void Start()
    {
        playerHealth = playerMaxHealth;
        _animator = GetComponentInChildren<Animator>();
        _playerController = GetComponent<PlayerController>();
        _playerRB = GetComponent<Rigidbody2D>();
    }

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
        if ((collision.CompareTag("Enemy") || collision.CompareTag("Rock")) && !isInmune && !isDead)
        {
            float damage = 0;

            if (collision.CompareTag("Enemy"))
                damage = collision.GetComponentInParent<Enemy>().damage;
            else if (collision.CompareTag("Rock"))
                damage = collision.GetComponent<FallingRock>().damage;

            playerHealth -= damage;
            StartCoroutine(Inmunity());

            if (playerHealth <= 0)
            {
                isDead = true; // Solo se activa una vez
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
        _capsuleCollider.enabled = false;

        // Reproducir música de Game Over una sola vez
        FindObjectOfType<GameManager>()?.OnGameOver();

        yield return new WaitForSeconds(3f);
        gameOver.SetActive(true);
    }
}