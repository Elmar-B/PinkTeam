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

    public void Damage()
    {
        damageSound.Play();
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
