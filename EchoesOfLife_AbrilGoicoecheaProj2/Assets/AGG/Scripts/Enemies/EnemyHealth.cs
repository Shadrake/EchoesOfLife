using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public bool isDamaged;

    [Header("Componentes")]
    Enemy _enemy;
    public Animator _animator;
    public Rigidbody2D _enemyRb;

    void Start()
    {
        _enemy = GetComponent<Enemy>();
        _animator = GetComponent<Animator>();
        _enemyRb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if(collision.CompareTag("Weapon") && !isDamaged)
        {
            _enemy.enemyLife -= 1f;
            /*if(collision.transform.position.x < transform.position.x)
            {
                _enemyRb.AddForce(new Vector2(_enemy.knockbackForceX, _enemy.knockbackForceY), ForceMode2D.Force);
            }

            else
            {
                _enemyRb.AddForce(new Vector2(-_enemy.knockbackForceX, _enemy.knockbackForceY), ForceMode2D.Force);
            }*/
            
            StartCoroutine(EnemyDamaged());
            

            if(_enemy.enemyLife <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private IEnumerator EnemyDamaged()
    {
        isDamaged = true;

        _animator.SetBool("Damaged", true);

        yield return new WaitForSeconds(0.5f);
        isDamaged = false;
        _animator.SetBool("Damaged", false);  
    }
}
