using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    [Header("Enemy Settings")]
    public bool isDamaged;

    [Header("Componentes")]
    Enemy _enemy;
    public Animator _animator;
    public Rigidbody2D _enemyRb;
    public GameObject rockSpawn;

    public GameObject _endTrigger;

    [Header("Health UI Settings")]
    public Image healthBar;
    public GameObject healthUI;

    [Header("Audio Settings")]
    public AudioSource damageAudioSource;  // Referencia al AudioSource
    public AudioClip damageSound;  // Sonido de daño
    public float minPitch = 0.8f;  // Pitch mínimo
    public float maxPitch = 2.4f;  // Pitch máximo

    void Start()
    {
        _enemy = GetComponent<Enemy>();
        _animator = GetComponent<Animator>();
        _enemyRb = GetComponent<Rigidbody2D>();

        if (healthBar != null)
        {
            healthBar.fillAmount = 1f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon") && !isDamaged)
        {
            _enemy.enemyLife -= 1f;

            if (healthBar != null)
            {
                healthBar.fillAmount = _enemy.enemyLife / _enemy.maxHealth;
            }

            if (_enemy.enemyLife <= 0)
            {
                StartCoroutine(EnemyDead());
            }
            else
            {
                StartCoroutine(EnemyDamaged());
            }
        }
    }

    private IEnumerator EnemyDamaged()
    {
        isDamaged = true;
        _animator.SetTrigger("Damaged");

        // Cambiar el pitch aleatoriamente antes de reproducir el sonido de daño
        if (damageAudioSource != null && damageSound != null)
        {
            damageAudioSource.pitch = Random.Range(minPitch, maxPitch);  // Cambiar el pitch aleatoriamente
            damageAudioSource.PlayOneShot(damageSound);
        }

        yield return new WaitForSeconds(0.5f);
        isDamaged = false;
    }

    private IEnumerator EnemyDead()
    {
        _animator.SetTrigger("Dead");
        healthUI.SetActive(false);
        rockSpawn.SetActive(false);

        // Rugido del jefe sin detener música
        GameManager gm = FindObjectOfType<GameManager>();
        if (gm != null)
        {
            gm.PlayBossDefeatedSFX();
        }

        yield return new WaitForSeconds(5.5f);
        _endTrigger.SetActive(true);
        Destroy(gameObject);
    }
}