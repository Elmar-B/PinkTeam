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

        yield return new WaitForSeconds(attackDuration + attackCooldown);
    }
}
