using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [Header("Projectile Settings")]
    public GameObject projectile;
    public float speed = 5f;

    public PlayerController playerContScript;
    //public Enemy enemyScript;

    private Vector2 _direction;
    private Rigidbody2D rbProjectile;
    
    void Start()
    {
        rbProjectile = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 1.5f);
    }

    void FixedUpdate()
    {
        rbProjectile.velocity = _direction * speed;
    }

    // Guardar una dirección para disparar.
    public void SetDirection (Vector2 direction)
    {
        GetComponent<SpriteRenderer>().flipX = direction.x>=1f? false : true;
        _direction = direction;
    }

    // Destruirse después de salir/chocar
    public void DestroyProjectile()
    {
        Destroy(gameObject);
        Debug.Log("Destruir proyectil 2");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Cuando choca con un enemigo hace daño.
        bool isWall = collision.CompareTag("Wall");
        if (collision.CompareTag("Enemy") || collision.CompareTag("Lamp") || collision.CompareTag("Crystal") || isWall)
        {
            DestroyProjectile();
            Debug.Log("Destruir proyectil");
        }

        if(isWall)
        {
            Door breakable = collision.GetComponent<Door>();
            if (breakable != null)
            {
                Debug.Log("Projectile: " + _direction);
                breakable.TryBreak(_direction.x >= 1f ? true : false);
            }
        }
    }
}
