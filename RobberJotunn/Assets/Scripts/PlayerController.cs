using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Sorces")]
    [SerializeField] Rigidbody2D body;
    [SerializeField] Animator animator;
    [SerializeField] AudioSource dashSound;
    [SerializeField] AudioSource damageSound;

    [Header("Health")]
    [SerializeField] float speed;
    [SerializeField] public int maxHealth;
    public int health;
    [SerializeField] float damageBuffer;
    Vector2 movement;
    private float invincibilityTime;

    [Header("Dash")]
    [SerializeField] float dashSpeed = 10f;
    [SerializeField] float dashDuration = 1f;
    [SerializeField] float dashCooldown = 1f;
    private bool isDashing;
    private bool canDash = true;

    void Awake()
    {
        health = maxHealth;
    }
    
    void Update()
    {
        if (isDashing)
        {
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

        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Attack");
        }

        if (Input.GetKeyDown(KeyCode.Space) && canDash && movement != Vector2.zero)
        {
            StartCoroutine(Dash());
        }

        invincibilityTime -= Time.deltaTime;
    }

    void FixedUpdate()
    {
        if (!isDashing)
        {
            body.velocity = movement.normalized * speed;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Damage"))
        {       
            if (invincibilityTime < 0 && !isDashing)
            {
                damageSound.Play();
                health -= 1;
                invincibilityTime = damageBuffer;
            }
        }
    }

    private IEnumerator Dash()
    {
        dashSound.Play();
        canDash = false;
        isDashing = true;
        body.velocity = movement.normalized * dashSpeed;
        yield return new WaitForSeconds(dashDuration);

        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);

        canDash = true;
    }
}
