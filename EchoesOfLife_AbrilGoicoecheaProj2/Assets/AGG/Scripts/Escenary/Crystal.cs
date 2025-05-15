using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    public Animator _crystalAnimator;
    private GameObject _player;
    private PlayerHealth _playerHealth;

    // Start is called before the first frame update
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
            //_crystalAnimator.SetBool("Break", true);
            StartCoroutine(DestroyCrystal());
        }
    }

    IEnumerator DestroyCrystal()
    {
        _crystalAnimator.SetBool("Break", true);
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
