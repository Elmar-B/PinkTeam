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
    [SerializeField] float dashSpeed;
    [SerializeField] float dashDuration;
    [SerializeField] float dashInvicibilityTime;

    [SerializeField] float dashCooldown;
    private bool isDashing;
    private bool canDash = true;
    private bool hasWeapon;
    private WeaponController weaponController;
    private Animator weaponAnimator;
    public bool isAttacking;
    public bool canAttack = true;
    private bool isAlive = true;
    public bool cutscene;
    private GameObject dashGap;
    private bool atGap;

    void Awake()
    {
        health = maxHealth;
    }
    
    void Update()
    {
        if (isAttacking || cutscene)
        {
            body.velocity = Vector2.zero;
            
            animator.SetFloat("Speed", 0);
            return;
        }

        if (isDashing || takingDamage || !isAlive)
        {
            return;
        }

        if (weaponController)
            weaponAnimator = weaponController.GetComponent<Animator>();

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement != Vector2.zero)
        {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);

            if (weaponAnimator)
            {
                if (Math.Abs(movement.x) == Math.Abs(movement.y))
                {
                    weaponAnimator.SetFloat("Horizontal", 0);
                    weaponAnimator.SetFloat("Vertical", movement.y);
                }
                else
                {
                    weaponAnimator.SetFloat("Horizontal", movement.x);
                    weaponAnimator.SetFloat("Vertical", movement.y);
                }
            }
            
        }
        animator.SetFloat("Speed", movement.sqrMagnitude);
        
        if (Input.GetKeyDown(KeyCode.K) && hasWeapon && canAttack)
        {
            animator.SetTrigger(weaponController.weaponName);
            StartCoroutine(Attack());

            if (weaponAnimator)
            {
                weaponAnimator.SetTrigger(weaponController.weaponName);
                StartCoroutine(weaponController.Attack());
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    void FixedUpdate()
    {
        if (!isDashing && !takingDamage && !isAttacking && isAlive && !cutscene)
        {
            body.velocity = movement.normalized * speed;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Damage"))
        {       
            if (canTakeDamage)
            {
                StartCoroutine(TakeDamage());   
            }
            if (health <= 0)
                StartCoroutine(PlayerDied());
        }
        else if (other.gameObject.CompareTag("Gap"))
        {
            dashGap = other.gameObject;
            atGap = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Gap"))
        {
            dashGap = other.gameObject;
            atGap = false;
        }
    }

    private void OnParticleCollision(GameObject other){
        if (other.gameObject.CompareTag("Damage"))
        {       
            if (canTakeDamage)
            {
                StartCoroutine(TakeDamage());   
            }
            if (health <= 0)
                StartCoroutine(PlayerDied());
        }
    }

    public void GiveWeapon(WeaponController weapon)
    {
        hasWeapon = true;
        weaponController = weapon;
    }

    private IEnumerator TakeDamage()
    {
        if (health > 0)
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
    }
    
    private IEnumerator PlayerDied()
    {
        animator.SetTrigger("Death");
        body.Sleep();
        isAlive = false;
        yield return new WaitForSeconds(3);
        GameManager.instance.GameOver();
    }

    private IEnumerator Dash()
    {
        dashSound.Play();
        canDash = false;
        isDashing = true;
        canTakeDamage = false;
        body.velocity = movement.normalized * dashSpeed;
        Debug.Log(dashGap);

        if (atGap)
        {
            dashGap.SetActive(false);
            body.velocity = new Vector2(0, 4);
        }
        
        trailRenderer.emitting = true;
        yield return new WaitForSeconds(dashDuration);

        isDashing = false;
        trailRenderer.emitting = false;

        if (dashGap)
        {
            dashGap.SetActive(true);
            dashGap = null;
        }
        
        yield return new WaitForSeconds(dashInvicibilityTime);

        canTakeDamage = true;

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
