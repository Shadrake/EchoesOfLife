using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [Header("Enemy Settings")]
    public bool isDamaged;

    [Header("Componentes")]
    Enemy _enemy;
    public Animator _animator;
    public Rigidbody2D _enemyRb;

    [Header("Audio Settings")]
    public AudioSource damageAudioSource;
    public AudioClip damageSound;
    public float minPitch = 0.9f;
    public float maxPitch = 1.1f;

    void Start()
    {
        _enemy = GetComponent<Enemy>();
        _animator = GetComponent<Animator>();
        _enemyRb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon") && !isDamaged)
        {
            _enemy.enemyLife -= 1f;
            StartCoroutine(EnemyDamaged());

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

        // 🔊 Reproducir audio con pitch aleatorio
        if (damageAudioSource != null && damageSound != null)
        {
            damageAudioSource.pitch = UnityEngine.Random.Range(minPitch, maxPitch);
            damageAudioSource.PlayOneShot(damageSound);
        }

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
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}