using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // Asegúrate de tener esta librería para trabajar con UI

public class PlayerController : MonoBehaviour
{
    // Movimiento del personaje
    public float speed, jumpForce = 200f;
    float velX, velY;
    Rigidbody2D _rbPlayer;

    // Detección del suelo
    public Transform groundCheck;
    public bool isGrounded;
    public float groundCheckRadius;
    public LayerMask whatIsGround;

    // Disparo
    [Header("Bullet Settings")]
    public GameObject projectilePrefab; 
    public GameObject weaponPositionInstantiate;
    public float timeDelayShoot = 0.25f;  // Tiempo de recarga del disparo
    private bool shootUnlocked = false;
    //public float animTime;


    // Animaciones
    [Header("Component Settings")]
    public Animator _animator;

    // UI - Barra de cooldown
    [Header("UI Settings")]
    public Image cooldownImg; // Barra de cooldown

    private float currentCooldown; // Tiempo actual de cooldown

    void Start()
    {
        // Almacenamos el componente en la variable correspondiente.
        _rbPlayer = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        currentCooldown = timeDelayShoot;  // Inicializamos el cooldown con el valor completo
    }

    void Update()
    {
        // Averiguamos si está en contacto con el suelo.
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        FlipCharacter();

        // Actualizamos la barra de cooldown
        if (currentCooldown < timeDelayShoot)
        {
            currentCooldown += Time.deltaTime; // Aumentamos el cooldown gradualmente
            cooldownImg.fillAmount = currentCooldown / timeDelayShoot; // Rellenamos la barra
        }
        else
        {
            cooldownImg.fillAmount = 1f; // La barra está completamente llena cuando se puede disparar
        }

        // Disparo
        if (Input.GetButton("Fire1") && currentCooldown >= timeDelayShoot) // Verificamos si el cooldown ha terminado
        {
            if(shootUnlocked)
            {
                Shoot();
                currentCooldown = 0f; // Vacíamos la barra inmediatamente al disparar
            }
        }
    }

    // Lo que tenga que ver con físicas es mejor en el FixedUpdate. Por tema de frames evita que en cada ordenador se vea diferente.
    private void FixedUpdate()
    {
        Movement();
        Jump();
    }

    /* MOVIMIENTO DE PERSONAJE */
    public void Movement()
    {
        // Hacemos que se mueva correctamente al pulsar las teclas de movimiento
        velX = Input.GetAxisRaw("Horizontal");
        velY = _rbPlayer.velocity.y;

        _rbPlayer.velocity = new Vector2(velX * speed, velY);

        if (_rbPlayer.velocity.x != 0)
        {
            _animator.SetBool("Running", true);
        }

        else
        {
            _animator.SetBool("Running", false);
        }

        if (isGrounded)
        {
            _animator.SetBool("Jumping", false );
        }

        else 
        {
            _animator.SetBool("Jumping", true);
        }
    }

    public void FlipCharacter()
    {
        // Rotamos el personaje cuando cambia de dirección.
        if ((_rbPlayer.velocity.x > 0) || velX == 1)
        {
            transform.localScale = new Vector3(1, 1, 1); //md
        }
        else if (Input.GetAxisRaw("Horizontal") == -1)
        {
            transform.localScale = new Vector3(-1, 1, 1); //mi
        }
    }

    public void Jump()
    {
        if (Input.GetButton("Jump") && isGrounded)
        {     
            _rbPlayer.AddForce(Vector2.up * jumpForce);
        }
    }

    /* HABILIDADES */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PowerUp"))
        {
            shootUnlocked = true;
        }
    }
    public void Shoot()
    {
        Vector3 direction;

        if (transform.localScale.x == 1.0f)
        {
            direction = Vector3.right; // dcha
        }
        else
        {
            direction = Vector3.left; // izq
        }
        GameObject projectile = Instantiate(projectilePrefab,
            weaponPositionInstantiate.transform.position + direction * 0.01f, Quaternion.identity);

        projectile.GetComponent<PlayerProjectile>().SetDirection(direction);
    }
}
