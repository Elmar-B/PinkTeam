using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Animator animator;
    public JotunnController jotunnController;
    Vector2 movement;
    [Header("Stats")]
    [SerializeField] int damage;
    [SerializeField] public float attackDuration;
    [SerializeField] public float attackCooldown;
    [SerializeField] AudioSource attackSound;
    [SerializeField] AudioSource damageSound;
    private bool canAttack = true;
    public string weaponName;

    void Start()
    {
        jotunnController = Resources.FindObjectsOfTypeAll<JotunnController>()[0];
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement != Vector2.zero)
        {
            if (Math.Abs(movement.x) == Math.Abs(movement.y))
            {
                animator.SetFloat("Horizontal", 0);
                animator.SetFloat("Vertical", movement.y);
            }
            else
            {
                animator.SetFloat("Horizontal", movement.x);
                animator.SetFloat("Vertical", movement.y);
            }
        }

        if (Input.GetKeyDown(KeyCode.K) && canAttack)
        {
            StartCoroutine(Attack());
            animator.SetTrigger(weaponName);
        }
    }

    public void Damage()
    {
        damageSound.Play();
        Debug.Log(jotunnController);
        jotunnController.Damage(damage);
    }

    public IEnumerator Attack()
    {
        attackSound.Play();
        canAttack = false;

        yield return new WaitForSeconds(attackDuration + attackCooldown);

        canAttack = true;
    }
}
