using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // Necesario para la barra de vida (Image)

public class BossHealth : MonoBehaviour
{
    [Header("Enemy Settings")]
    public bool isDamaged;

    [Header("Componentes")]
    Enemy _enemy;
    public Animator _animator;
    public Rigidbody2D _enemyRb;
    public GameObject rockSpawn;

    [Header("Health UI Settings")]
    public Image healthBar;  // Referencia a la UI de la barra de vida
    public GameObject healthUI;

    void Start()
    {
        _enemy = GetComponent<Enemy>();
        _animator = GetComponent<Animator>();
        _enemyRb = GetComponent<Rigidbody2D>();

        // Asegúrate de que la barra de vida está llena al inicio
        if (healthBar != null)
        {
            healthBar.fillAmount = 1f;  // 100% de vida al inicio
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.CompareTag("Weapon") && !isDamaged)
        {
            // Reducir vida
            _enemy.enemyLife -= 1f;

            // Actualizar barra de vida
            if (healthBar != null)
            {
                healthBar.fillAmount = _enemy.enemyLife / _enemy.maxHealth;  // Asumiendo que tienes una variable maxHealth
            }

            // Si la vida del enemigo llega a 0, muere
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

        yield return new WaitForSeconds(0.5f);
        isDamaged = false;
    }

    private IEnumerator EnemyDead()
    {
        _animator.SetTrigger("Dead");
        healthUI.SetActive(false);
        rockSpawn.SetActive(false);

        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
