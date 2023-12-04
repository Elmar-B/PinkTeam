using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public int health;
    public float damageBuffer;
    public Rigidbody2D body;
    public Animator animator;
    Vector2 movement;
    private float invincibilityTime;

    [Header("Dash")]
    [SerializeField] float dashSpeed = 10f;
    [SerializeField] float dashDuration = 1f;
    [SerializeField] float dashCooldown = 1f;
    private bool isDashing;
    private bool canDash = true;
    
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
            invincibilityTime = dashDuration;
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
            if (invincibilityTime < 0)
            {        
                health -= 1;
                invincibilityTime = damageBuffer;
            }
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        body.velocity = movement.normalized * dashSpeed;
        yield return new WaitForSeconds(dashDuration);

        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);

        canDash = true;
    }
}
