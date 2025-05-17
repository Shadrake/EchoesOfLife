using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    public Animator _crystalAnimator;
    private GameObject _player;
    private PlayerHealth _playerHealth;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip breakSound;

    void Start()
    {
        _crystalAnimator = GetComponent<Animator>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerHealth = _player.GetComponent<PlayerHealth>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            _playerHealth.playerHealth += 2f;
            StartCoroutine(DestroyCrystal());
        }
    }

    IEnumerator DestroyCrystal()
    {
        _crystalAnimator.SetBool("Break", true);

        // 🔊 Reproducir sonido de cristal roto
        if (audioSource != null && breakSound != null)
        {
            audioSource.PlayOneShot(breakSound);
        }

        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
