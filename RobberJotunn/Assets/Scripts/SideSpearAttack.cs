using UnityEngine;

public class SideSpearAttack : MonoBehaviour
{
    private float y1 = -0.7f;
    private float y2 = -0.25f;
    private float y3 = 0.25f;
    private float x1 = -0.95f;
    private float x2 = 0.95f;
    private float x;
    private float y;
    public float velocity;
    private Vector3 direction;
    private int secondRandomInt;
    private Rigidbody2D myRigidBody;
    public GameObject SpearWarning;
    public float timeToDestroy;
    private bool isMoving = false;
    // private var numbers = new Array(-0.7f, -0.25f, 0.25f);
    

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        int randInt = Random.Range(0,2);
        if(randInt == 0)
        {
            float[] sidefloats = {y1, y2, y3};
            int randomInt = Random.Range(0,3);
            y = sidefloats[randomInt];
            
            secondRandomInt = Random.Range(0,2);
            if(secondRandomInt == 0)
            {
                direction = new Vector3(-1, 0, 0);
                transform.position = new Vector3(4,y,0);
            }
            else
            {
                direction = new Vector3(1, 0, 0);
                transform.position = new Vector3(-4,y,0);
                transform.eulerAngles = new Vector3(0,0,270);
            }
            SpearWarning.transform.position = new Vector3(0.01f, y, 0);
        }
        else
        {
            float[] floats = {x1,x2};
            int randomInt = Random.Range(0,2);
            x = floats[randomInt];

            direction = new Vector3(0, -1, 0);
            transform.position = new Vector3(x, 4, 0);
            transform.eulerAngles = new Vector3(0,0,180);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!isMoving)
        {
            myRigidBody.velocity = direction * velocity;
            isMoving = true;
        }
        timeToDestroy -= Time.deltaTime;
        if(timeToDestroy <= 0)
        {
            Destroy(SpearWarning);
            Destroy(gameObject);
            
        }
    }


}
