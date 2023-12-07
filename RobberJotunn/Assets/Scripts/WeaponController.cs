using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Animator animator;
    public JotunnController jotunnController;
    Vector2 movement;

    void Start()
    {
        jotunnController = GameObject.FindGameObjectWithTag("Jotunn").GetComponent<JotunnController>();

    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement != Vector2.zero)
        {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
        }

        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Attack");
        }
    }

    public void Damage()
    {
        jotunnController.Damage(1);
    }
}
