using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed, jumpForce = 200f;
    float velX, velY;
    Rigidbody2D _rbPlayer;

    public Transform groundCheck;
    public bool isGrounded;
    public float groundCheckRadius;
    public LayerMask whatIsGround;

    [Header("Bullet Settings")]
    public GameObject projectilePrefab;
    public GameObject weaponPositionInstantiate;
    public float timeDelayShoot = 0.25f;
    private bool shootUnlocked = false;

    [Header("Audio")]
    public AudioSource sfxSource;
    public AudioClip shootClip;

    [Header("Component Settings")]
    public Animator _animator;

    [Header("UI Settings")]
    public Image cooldownImg;

    private float currentCooldown;

    void Start()
    {
        _rbPlayer = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
        currentCooldown = timeDelayShoot;
    }

    void Update()
    {
        FlipCharacter();

        if (currentCooldown < timeDelayShoot)
        {
            currentCooldown += Time.deltaTime;
            cooldownImg.fillAmount = currentCooldown / timeDelayShoot;
        }
        else
        {
            cooldownImg.fillAmount = 1f;
        }

        if (Input.GetButton("Fire1") && currentCooldown >= timeDelayShoot)
        {
            if (shootUnlocked)
            {
                Shoot();
                currentCooldown = 0f;
            }
        }
    }

    private void FixedUpdate()
    {
        CheckGrounded(); // << Añadido
        Movement();
        Jump();
    }

    private void CheckGrounded()
    {
        // Permite detectar el suelo si el personaje está descendiendo o casi quieto en Y
        if (_rbPlayer.velocity.y < 0.1f)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        }
        else
        {
            isGrounded = false;
        }
    }

    public void Movement()
    {
        velX = Input.GetAxisRaw("Horizontal");
        velY = _rbPlayer.velocity.y;

        _rbPlayer.velocity = new Vector2(velX * speed, velY);

        _animator.SetBool("Running", _rbPlayer.velocity.x != 0);
        _animator.SetBool("Jumping", !isGrounded);
    }

    public void FlipCharacter()
    {
        if ((_rbPlayer.velocity.x > 0) || velX == 1)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (Input.GetAxisRaw("Horizontal") == -1)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public void Jump()
    {
        if (Input.GetButton("Jump") && isGrounded)
        {
            _rbPlayer.velocity = new Vector2(_rbPlayer.velocity.x, jumpForce);

            if (sfxSource != null && shootClip != null)
            {
                sfxSource.pitch = 1.4f;
                sfxSource.PlayOneShot(shootClip);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PowerUp"))
        {
            shootUnlocked = true;
        }
    }

    public void Shoot()
    {
        Vector3 direction = (transform.localScale.x == 1.0f) ? Vector3.right : Vector3.left;

        GameObject projectile = Instantiate(projectilePrefab,
            weaponPositionInstantiate.transform.position + direction * 0.01f, Quaternion.identity);

        projectile.GetComponent<PlayerProjectile>().SetDirection(direction);

        
    }
}