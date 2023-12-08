using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class JotunnHandsController : MonoBehaviour
{
    public GameObject player;
    public GameObject jotunnHandSmokePrefab;
    public float attackOdds;
    public float range;
    public float attackTime;
    private CapsuleCollider2D handCollider;
    private bool preparing;
    private bool attacking;
    private bool resting;
    private bool retracting;
    private float restingTime;
    private Vector3 defaultPos;
    private Vector3 target;
    private Vector3 startingPos;
    private float passedTime;

    void Start()
    {
        handCollider = gameObject.GetComponent<CapsuleCollider2D>();
        defaultPos = startingPos = target = transform.position;
        transform.position = transform.position + (Vector3.up * 0.2f);
        Attacking(defaultPos);
        //PrepareAttack();
    }

    void FixedUpdate()
    {
        if (preparing)
        {
            // Reached target height and will strike !!
            if (transform.position == target)
            {
                Attacking(player.transform.position);
            }
            // Moving into position for attacking
            else
            {
                passedTime += Time.deltaTime/(attackTime*0.80f);
                transform.position = Vector3.Lerp(startingPos, target, passedTime);
            }
        }
        else if (attacking)
        {
            // Reached the attacking target
            if (transform.position == target)
            {
                Strike();
            }
            else
            {
                passedTime += Time.deltaTime/(attackTime*0.20f);
                transform.position = Vector3.Lerp(startingPos, target, passedTime);
            }
        }
        else if (resting)
        {
            passedTime += Time.deltaTime;
            if (passedTime > 0.1f)
                handCollider.isTrigger = false;
            if (passedTime > restingTime)
                Retract();
        }
        // Going back to default position
        else if (retracting)
        {
            if (transform.position == default)
                retracting = false;
            else
            {
                passedTime += Time.deltaTime/attackTime;
                transform.position = Vector3.Lerp(startingPos, defaultPos, passedTime);
            }
        }
    }

    private void PrepareAttack()
    {
        gameObject.tag = "Damage";
        handCollider.isTrigger = true;
        handCollider.enabled = false;
        startingPos = transform.position;
        target += new Vector3(0, 0.3f, 0);
        passedTime = 0f;
        preparing = true;
    }

    private void Attacking(Vector3 playerPos)
    {
        preparing = false;
        startingPos = transform.position;
        Vector3 targetVector = playerPos - defaultPos;
        if (targetVector.magnitude > range)
        {
            targetVector = targetVector.normalized * range;
        }
        target = targetVector + defaultPos;
        passedTime = 0f;
        attacking = true;
    }

    private void Strike()
    {
        attacking = false;
        handCollider.enabled = true;
        SplashEffect();
        passedTime = 0f;
        restingTime = 2f;
        resting = true;
    }

    private void Retract()
    {
        resting = false;
        handCollider.isTrigger = false;
        gameObject.tag = "Damageable";
        startingPos = transform.position;
        passedTime = 0f;
        retracting = true;
    }

    private void SplashEffect()
    {
        GameObject jotunnHandSmoke = Instantiate(jotunnHandSmokePrefab);
        jotunnHandSmoke.transform.position = transform.position - (Vector3.down * -0.2f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Weapon") && !attacking && !resting && !retracting)
        {
            float rnum = Random.Range(0f, 1f);
            if (rnum > attackOdds)
                PrepareAttack();
        }
    }
}
