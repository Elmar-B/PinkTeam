using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JotunnHandsController : MonoBehaviour
{
    public GameObject player;
    public GameObject jotunnHandSmokePrefab;
    public GameObject ShadowPrefab;
    public GameObject IciclePrefab;
    public AudioSource fistSound;
    public float attackOdds;
    public float range;
    public float attackTime;
    public int numIcicles;
    private SpriteRenderer shadowSprite;
    private GameObject shadowObject;
    private Collider2D triggerCollider;
    private Collider2D physiscsCollider;
    private float restingTime;
    private Vector3 defaultPos;
    private Vector3 target;
    private Vector3 startingPos;
    private Vector3 shadowDefaultPos;
    private Vector3 shadowStartingScale;
    private Vector3 shadowSmallScale;
    private Vector3 shadowTargetScale;
    private float passedTime;
    private enum State{
        Waiting, Preparing, Attacking, Resting, Retracting
    };
    private State state;

    void Start()
    {
        defaultPos = startingPos = target = transform.position;
        state = State.Waiting;

        // Get shadow sprite renderer
        shadowObject = Instantiate(ShadowPrefab);
        shadowObject.transform.position = transform.position + (Vector3.down * 0.2f);
        shadowObject.transform.parent = transform.parent;
        shadowDefaultPos = shadowObject.transform.position;
        shadowSprite = shadowObject.GetComponent<SpriteRenderer>();
        shadowSprite.enabled = false;

        shadowStartingScale = shadowObject.transform.localScale;
        shadowSmallScale = shadowStartingScale / 2f;
        shadowTargetScale = shadowStartingScale * 1.3f;

        // Get colliders
        triggerCollider = gameObject.GetComponent<CapsuleCollider2D>();
        physiscsCollider = gameObject.transform.GetChild(0).gameObject.GetComponent<Collider2D>();
        
        // Dramatic starting slam
        transform.position = transform.position + (Vector3.up * 0.2f);
        Attacking(defaultPos);
    }

    void FixedUpdate()
    {
        switch(state) {
            case State.Preparing:
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

                    shadowObject.transform.localScale = Vector3.Lerp(shadowStartingScale, shadowSmallScale, passedTime);
                }
                break;
            }
            case State.Attacking:
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

                    shadowObject.transform.position = Vector3.Lerp(shadowDefaultPos, target, passedTime);
                    shadowObject.transform.localScale = Vector3.Lerp(shadowSmallScale, shadowTargetScale, passedTime);
                }
                break;
            }
            case State.Resting:
            {
                passedTime += Time.deltaTime;
                if (passedTime > 0.1f)
                    gameObject.tag = "Damageable";
                    physiscsCollider.enabled = true;
                if (passedTime > restingTime)
                    Retract();
                break;
            }
            case State.Retracting:
            {
                // Going back to default position
                if (transform.position == defaultPos)
                    state = State.Waiting;
                else
                {
                    passedTime += Time.deltaTime/attackTime;
                    transform.position = Vector3.Lerp(startingPos, defaultPos, passedTime);
                }
                break;
            }
            default: break;
        }
    }

    private void PrepareAttack()
    {
        state = State.Preparing;

        gameObject.tag = "Damage";
        triggerCollider.enabled = false;
        physiscsCollider.enabled = false;
        startingPos = transform.position;
        target = defaultPos + new Vector3(0, 0.3f, 0);

        shadowObject.transform.localScale = shadowStartingScale;
        shadowSprite.enabled = true;

        passedTime = 0f;
    }

    private void Attacking(Vector3 playerPos)
    {
        state = State.Attacking;

        // Target location limited by range
        startingPos = transform.position;
        Vector3 targetVector = playerPos - defaultPos;
        if (targetVector.magnitude > range)
        {
            targetVector = targetVector.normalized * range;
        }
        target = targetVector + defaultPos;

        passedTime = 0f;
    }

    private void Strike()
    {
        fistSound.Play();
        state = State.Resting;

        triggerCollider.enabled = true;
        IcicleAttack();
        SplashEffect();

        shadowSprite.enabled = false;
        shadowObject.transform.position = shadowDefaultPos;

        passedTime = 0f;
        restingTime = 2f;
    }

    private void Retract()
    {
        state = State.Retracting;
        startingPos = transform.position;
        passedTime = 0f;
    }

    private void SplashEffect()
    {
        GameObject jotunnHandSmoke = Instantiate(jotunnHandSmokePrefab);
        jotunnHandSmoke.transform.position = transform.position - (Vector3.down * -0.2f);
    }

    private void IcicleAttack()
    {
        if (numIcicles > 0)
        {
            float offset = Random.Range(0f, 360/numIcicles);
            GameObject[] icicles = new GameObject[numIcicles];
            for (int i = 0; i < numIcicles; i++)
            {
                GameObject icicle = Instantiate(IciclePrefab);
                icicle.transform.position = transform.position;

                IcicleController icicleScript = icicle.GetComponent<IcicleController>();
                icicleScript.angle = i*(360/numIcicles) + offset;
                icicleScript.velocity = 1f;

                icicles[i] = icicle;
            }
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Weapon") && state == State.Waiting)
        {
            float rnum = Random.Range(0f, 1f);
            if (rnum < attackOdds)
                PrepareAttack();
        }
    }
}
