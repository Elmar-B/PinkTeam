using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.AssetImporters;
using UnityEngine;
using UnityEngine.UI;

public class JotunnController : MonoBehaviour
{
    public int maxHealth = 100;
    private float health;
    public GameObject swordSwipeAttackPrefab;
    public GameObject flyingSwordPrefab;
    public GameObject basicProjectilePrefab;
    [SerializeField] SpriteRenderer bodySprite;
    [SerializeField] SpriteRenderer rightHandSprite;
    [SerializeField] SpriteRenderer leftHandSprite;
    public float damageTime;

    // Health bar:
    public Slider slider;
    private float timePassed;

    void Awake()
    {
        slider.maxValue = maxHealth;
        health = maxHealth;
        slider.value = health;

        timePassed = 0f;
    }

    public void Damage(int damage)
    {
        health -= damage;
        slider.value = health;
        StartCoroutine(Blink());
        if (health <= 0)
            JotunnDied();
    }

    private IEnumerator Blink()
    {
        Color oldColor = new Color(1, 1, 1);
        Color newColor = new Color(1, 0, 0);
        bodySprite.color = newColor;
        rightHandSprite.color = newColor;
        leftHandSprite.color = newColor;

        yield return new WaitForSeconds(damageTime);

        bodySprite.color = oldColor;
        rightHandSprite.color = oldColor;
        leftHandSprite.color = oldColor;
    }

    void FixedUpdate()
    {
        timePassed += Time.deltaTime;
        if (timePassed > 6f)
        {
            Attack();
            timePassed = 0f;
        }
    }

    void Attack()
    {
        float rnum = Random.Range(0f, 3f);
        // Sword swipe
        if (rnum < 1f)
        {
            GameObject swordSwipeAttack = Instantiate(swordSwipeAttackPrefab);
            SwordSwipeAttackPhysics script = swordSwipeAttack.GetComponent<SwordSwipeAttackPhysics>();
            script.swing = true;
            script.startingRotationSpeed = 150;
            if (rnum > 0.5f)
                script.rightSwing = true;
            else
                script.rightSwing = false;
        }
        else if (rnum < 2f)
        {
            GameObject flyingSword = Instantiate(flyingSwordPrefab);
        }
        else if (rnum < 3f)
        {
            GameObject projectileSpawner = Instantiate(basicProjectilePrefab);
            Projectile script = projectileSpawner.GetComponent<Projectile>();
            script.attackTime = 10f;
        }
    }

    private void JotunnDied()
    {
        GameManager.instance.Victory();
    }


}
