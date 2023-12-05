using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JotunnController : MonoBehaviour
{
    public JotunHealth jotunHealth;
    public int maxHealth = 100;
    private float health;

    void Awake()
    {
        health = (int) maxHealth;
        jotunHealth.SetMaxHealth(maxHealth);
    }

    public void Damage(int damage)
    {
        health -= damage;
        jotunHealth.SetHealth(health);
    }
}
