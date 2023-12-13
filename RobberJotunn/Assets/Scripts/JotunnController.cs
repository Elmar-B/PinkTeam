using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class JotunnController : MonoBehaviour
{
    public int maxHealth = 100;
    private float health;
    public GameObject swordSwipeAttackPrefab;
    public GameObject flyingSwordPrefab;
    public GameObject iciclePrefab;
    [SerializeField] SpriteRenderer bodySprite;
    [SerializeField] SpriteRenderer rightHandSprite;
    [SerializeField] SpriteRenderer leftHandSprite;
    public float damageTime;
    public GameObject sideSpearAttack;
    private JotunnHandsController rightHandController;
    private JotunnHandsController leftHandController;

    // Health bar:
    public Slider slider;
    private float timePassed;
    private enum State{
        Phase1, Phase2, Phase3
    };
    private State state;

    void Awake()
    {
        state = State.Phase1;
        slider.maxValue = maxHealth;
        health = maxHealth;
        slider.value = health;

        rightHandController = transform.Find("RightHand").gameObject.GetComponent<JotunnHandsController>();
        leftHandController = transform.Find("LeftHand").gameObject.GetComponent<JotunnHandsController>();

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
        Color oldColor = bodySprite.color;
        bodySprite.color = new Color(1, 0, 0);
        rightHandSprite.color = new Color(1, 0, 0);
        leftHandSprite.color = new Color(1, 0, 0);

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
        Debug.Log("Phase: "+state);
        switch(state){
            case State.Phase1:
            {
                float rnum = Random.Range(2f, 3f);
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
                    GameObject icicleObject = Instantiate(iciclePrefab);
                    icicleObject.transform.position = new Vector3(Random.Range(-0.18f, 0.18f), 1.1f, 0);

                    IcicleController icicleController = icicleObject.GetComponent<IcicleController>();
                    GameObject player = GameObject.FindGameObjectWithTag("Player");
                    Vector3 direction = icicleObject.transform.position - player.transform.position;
                    icicleController.angle = Vector2.SignedAngle(Vector3.up, direction) - 90f;
                }
                break;
            }
            case State.Phase2:
            {
                float rnum = Random.Range(0f, 1f);
                if(rnum < 1f)
                {
                    GameObject spearSideAttack = Instantiate(sideSpearAttack);
                }
              
                break;
            }
            case State.Phase3:
            {
                break;
            }
            default: break;
        }
        
    
    }

    private void JotunnDied()
    {
        switch(state){
            case State.Phase1:
            {
                //regenerate jotunn health move to phase 2;
                state = State.Phase2;
                slider.maxValue = maxHealth;
                health = maxHealth;
                slider.value = health;

                // Add icicles to hand attack
                rightHandController.numIcicles = 8;
                leftHandController.numIcicles = 8;

                timePassed = 0f;
                break;
            }
            case State.Phase2:
            {
                //regenerate jotunn health move to phase 3;
                state = State.Phase3;
                slider.maxValue = maxHealth;
                health = maxHealth;
                slider.value = health;

                timePassed = 0f;
                break;
            }
            case State.Phase3:
            {
                GameManager.instance.Victory();
                break;
            }
        }
        
    }


}
