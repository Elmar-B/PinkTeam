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
    public GameObject threeSpearAttack;
    private JotunnHandsController rightHandController;
    private JotunnHandsController leftHandController;

    // Health bar:
    public Slider slider;
    private float timePassed;
    private enum State{
        Phase1, Phase2, Phase3, Phase4
    };
    private State state;

    public GameObject bigHammerAttack;
    private bool hammerIsAttacking = false;
    public GameObject lightingAttack;
    public JotunHearts jotunHearts;

    private SpriteRenderer jotunSprite;
    private SpriteRenderer rightHandSpriteRenderer;
    private SpriteRenderer leftHandSpriteRenderer;
    private GameObject lighting;

    private BigHammer hammerController;
    [SerializeField] GameObject playerObject;


    void Awake()
    {
        state = State.Phase1;
        slider.maxValue = maxHealth;
        health = maxHealth;
        slider.value = health;

        rightHandController = transform.Find("RightHand").gameObject.GetComponent<JotunnHandsController>();
        leftHandController = transform.Find("LeftHand").gameObject.GetComponent<JotunnHandsController>();

        rightHandSpriteRenderer = transform.Find("RightHand").gameObject.GetComponent<SpriteRenderer>();
        leftHandSpriteRenderer = transform.Find("LeftHand").gameObject.GetComponent<SpriteRenderer>();
        jotunSprite = transform.Find("Sprite").gameObject.GetComponent<SpriteRenderer>();
        //StartCoroutine(JotunnDied());        
        timePassed = 0f;
    }

    public void FirstAttack()
    {
        GameManager.instance.backgroundMusic.Play();
        GameManager.instance.snowSound.Stop();
        slider.gameObject.SetActive(true);
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
        if (timePassed > 4f)
        {
            timePassed = 0f;
            Attack();
        }
    }

    void Attack()
    {
        switch(state){
            case State.Phase1:
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
                    // Next attack quicker to arive
                    timePassed += 1f;
                }
                else if (rnum < 3f)
                {
                    icicleAttack(1f);

                    // Next attack quicker to arive
                    timePassed += 2f;
                }
                break;
            }
            case State.Phase2:
            {
                float rnum = Random.Range(0f, 3f);
                if(rnum < 1f)
                {
                    GameObject spearSideAttack = Instantiate(sideSpearAttack);
                }
                else if(rnum < 2f)
                {
                    GameObject spearThreeAttack = Instantiate(threeSpearAttack);

                    timePassed += 1f;
                }
                else if (rnum < 3f)
                {
                    icicleAttack(1f);

                    timePassed += 3f;
                }
              
                break;
            }
            case State.Phase3:
            {
                float rnum = Random.Range(0f,3f);
                
                if(!hammerIsAttacking)
                {
                    GameObject hammerBigAttack = Instantiate(bigHammerAttack);
                    hammerController = hammerBigAttack.GetComponent<BigHammer>();
                    hammerIsAttacking = true;
                    lighting = Instantiate(lightingAttack);
                    lighting.SetActive(true);
                    
                }
                if(rnum < 1f)
                {
                    icicleAttack(1f);
                    
                }
                timePassed += 3f;
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
                jotunHearts.StateChange(2);
                state = State.Phase2;
                slider.maxValue = maxHealth;
                health = maxHealth;
                slider.value = health;

                // Add icicles to hand attack
                rightHandController.numIcicles = 8;
                leftHandController.numIcicles = 8;

                //timePassed = 4f;
                break;
            }
            case State.Phase2:
            {
                //regenerate jotunn health move to phase 3;
                jotunHearts.StateChange(3);
                state = State.Phase3;
                slider.maxValue = maxHealth;
                health = maxHealth;
                slider.value = health;

                //timePassed = 0f;
                break;
            }
            case State.Phase3:
            {
                playerObject.GetComponent<Collider2D>().enabled = false;
                StartCoroutine(ReturnHammerToThor());
                jotunHearts.StateChange(4);
                state = State.Phase4;
                Destroy(lighting);
                StartCoroutine(fadeAway());

                break;
            }
        }
        
    }

    void icicleAttack(float speed)
    {
        GameObject icicleObject = Instantiate(iciclePrefab);
        icicleObject.transform.position = new Vector3(Random.Range(-0.18f, 0.18f), 0.6f, 0) + transform.position;

        icicleObject.GetComponent<SpriteRenderer>().sortingOrder = 2;

        IcicleController icicleController = icicleObject.GetComponent<IcicleController>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector3 direction = icicleObject.transform.position - player.transform.position;
        icicleController.angle = Vector2.SignedAngle(Vector3.up, direction) - 90f;
        icicleController.speed = speed;
    }

    private IEnumerator ReturnHammerToThor()
    {
        hammerController.ReturnHammer(5f);
        yield return new WaitForSeconds(5f);
        hammerController.DestroyHammer();
        playerObject.GetComponent<PlayerController>().cutscene = true;;
        playerObject.GetComponent<Animator>().SetTrigger("Victory");

        yield return new WaitForSeconds(5f);

        GameManager.instance.Victory();
    }

    public IEnumerator fadeAway()
    {
        float fadeSpeed = 0.2f;

        while(jotunSprite.color.a > 0){
            Color objectColor = jotunSprite.color;
            float fadeAmount = objectColor.a -(fadeSpeed*Time.deltaTime);
            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b,fadeAmount);
            jotunSprite.color = objectColor;
            rightHandSprite.color = objectColor;
            leftHandSpriteRenderer.color = objectColor;

            yield return null;
            
        }
            
    }
        
}


