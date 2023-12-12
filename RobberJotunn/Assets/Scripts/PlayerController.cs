using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Sorces")]
    [SerializeField] public Rigidbody2D body;
    [SerializeField] Animator animator;
    [SerializeField] AudioSource dashSound;
    [SerializeField] AudioSource damageSound;
    [SerializeField] TrailRenderer trailRenderer;

    [Header("Health")]
    [SerializeField] float speed;
    [SerializeField] public int maxHealth;
    public int health;
    [SerializeField] float damageDuration;
    [SerializeField] float invincibilityTime;
    Vector2 movement;
    private bool takingDamage;
    private bool canTakeDamage = true;

    [Header("Dash")]
    [SerializeField] float dashSpeed = 10f;
    [SerializeField] float dashDuration = 1f;
    [SerializeField] float dashCooldown = 1f;
    private bool isDashing;
    private bool canDash = true;
    private bool hasWeapon = true;
    public bool isAttacking = false;
    public bool canAttack = true;

    void Awake()
    {
        health = maxHealth;
    }
    
    void Update()
    {
        if (isDashing || takingDamage)
        {
            return;
        }

        if (isAttacking)
        {
            body.velocity = Vector2.zero;
            return;
        }

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement != Vector2.zero)
        {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
        }
        animator.SetFloat("Speed", movement.sqrMagnitude);
        
        if (Input.GetKeyDown(KeyCode.K) && hasWeapon && canAttack)
        {
            animator.SetTrigger("Attack");
            StartCoroutine(Attack());
        }

        if (Input.GetKeyDown(KeyCode.Space) && canDash && movement != Vector2.zero)
        {
            StartCoroutine(Dash());
        }
    }

    void FixedUpdate()
    {
        if (!isDashing && !takingDamage && !isAttacking)
        {
            body.velocity = movement.normalized * speed;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Damage"))
        {       
            if (canTakeDamage && !isDashing)
            {
                StartCoroutine(TakeDamage());   
            }
            if (health <= 0)
                PlayerDied();
        }
    }

    private void OnParticleCollision(GameObject other){
        //Debug.Log("Partical Collison");
        if (other.gameObject.CompareTag("Damage"))
        {       
            if (canTakeDamage && !isDashing)
            {
                StartCoroutine(TakeDamage());   
            }
            if (health <= 0)
                PlayerDied();
        }
    }

    public void GiveWeapon()
    {
        hasWeapon = true;
    }

    private IEnumerator TakeDamage()
    {
        damageSound.Play();
        health -= 1;
        takingDamage = true;

        canTakeDamage = false;

        transform.GetChild(0).gameObject.SetActive(true);

        body.velocity = new Vector2(0, 0);

        yield return new WaitForSeconds(damageDuration);

        transform.GetChild(0).gameObject.SetActive(false);

        takingDamage = false;

        yield return new WaitForSeconds(invincibilityTime);

        canTakeDamage = true;
    }
    
    private void PlayerDied()
    {
        GameManager.instance.GameOver();
    }

    private IEnumerator Dash()
    {
        dashSound.Play();
        canDash = false;
        isDashing = true;
        body.velocity = movement.normalized * dashSpeed;
        trailRenderer.emitting = true;
        yield return new WaitForSeconds(dashDuration);

        isDashing = false;
        trailRenderer.emitting = false;

        yield return new WaitForSeconds(dashCooldown);

        canDash = true;
    }

    public IEnumerator Attack()
    {
        isAttacking = true;
        canAttack = false;


        WeaponController weapon = transform.GetChild(1).GetComponent<WeaponController>();

        yield return new WaitForSeconds(weapon.attackDuration);
        
        isAttacking = false;

        yield return new WaitForSeconds(weapon.attackCooldown);

        canAttack = true;
    }
}
