using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // Necesario para la barra de vida (Image)

public class EnemyHealth : MonoBehaviour
{
    [Header("Enemy Settings")]
    public bool isDamaged;

    [Header("Componentes")]
    Enemy _enemy;
    public Animator _animator;
    public Rigidbody2D _enemyRb;

    //[Header("Health UI Settings")]
    //public Image healthBar; 
    //public GameObject healthUI;

    void Start()
    {
        _enemy = GetComponent<Enemy>();
        _animator = GetComponent<Animator>();
        _enemyRb = GetComponent<Rigidbody2D>();

        // La barra de vida está llena desdde el principio
        /*if (healthBar != null)
        {
            healthBar.fillAmount = 1f;  // 100% de vida al inicio
        }*/
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.CompareTag("Weapon") && !isDamaged)
        {
            // Reducir vida
            _enemy.enemyLife -= 1f;
            StartCoroutine(EnemyDamaged());
            // Actualizar barra de vida
            /*if (healthBar != null)
            {
                healthBar.fillAmount = _enemy.enemyLife / _enemy.maxHealth; 
            }
            */

            // Si la vida del enemigo llega a 0, muere
            if (_enemy.enemyLife <= 0)
            {
                StartCoroutine(EnemyDead());
            }
        }
    }

    private IEnumerator EnemyDamaged()
    {
        isDamaged = true;
        _animator.SetTrigger("Damaged");

        yield return new WaitForSeconds(0.5f);
        isDamaged = false;
        if (_enemy.enemyLife <= 0)
        {
            _animator.SetTrigger("Dead");
        }
    }

    private IEnumerator EnemyDead()
    {
        Debug.Log("Enemy dead");
        //_animator.SetTrigger("Dead");
        //healthUI.SetActive(false);
        
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
