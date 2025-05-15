using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Enemy type")]
    public bool isStatic;
    public bool isWalker;

    [Header("Walk settings")]
    float speed;
    public bool walksRight;
    public float walkTime;
    private float walkTimer;  // Temporizador para el tiempo de caminata

    [Header("Components")]
    public Animator _enemyAnimator;
    
    /*[Header("Checks")]
    public Transform pitCheck;
    public Transform wallCheck;
    public Transform groundCheck;
    public bool pitDetected, groundDetected;
    public float detectionRadius;
    public LayerMask whatIsGround;*/

    // Start is called before the first frame update
    void Start()
    {
        speed = GetComponent<Enemy>().enemySpeed; // Obtiene la velocidad del enemigo de otro componente
        Debug.Log(speed);

        walkTimer = walkTime; // Inicializamos el temporizador con el valor de walkTime
        _enemyAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        /*pitDetected = !Physics2D.OverlapCircle(pitCheck.position, detectionRadius, whatIsGround); // Será verdadero cuando no se detecte el suelo.
        if(pitDetected)
        {
            Flip();
        }*/
        
        if (isStatic)
        {
            // Si el enemigo es estático, congelamos su Rigidbody2D
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }

        if (isWalker)
        {
            _enemyAnimator.SetBool("Run",true);
            MoveEnemy();
            Debug.Log("Walker");
        }
    }

    void MoveEnemy()
    {
        // Desplazamos al enemigo en la dirección correcta
        if (walksRight)
        {
            transform.Translate(Vector2.right * Time.deltaTime * speed, Space.World);
            Debug.Log("R"+ Time.deltaTime * speed);

        }
        else
        {
            transform.Translate(Vector2.left * Time.deltaTime * speed, Space.World);
            Debug.Log("L" + Time.deltaTime * speed);

        }

        // Reducimos el temporizador de caminata
        walkTimer -= Time.deltaTime;

        // Si el temporizador llega a 0, cambiamos la dirección
        if (walkTimer <= 0)
        {
            Flip(); // Cambiar la dirección del movimiento
            walkTimer = walkTime; // Reiniciar el temporizador de caminata
        }
    }

    public void Flip()
    {
        walksRight = !walksRight; // Cambio de dirección
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y); // Giramos el enemigo
    }
}
