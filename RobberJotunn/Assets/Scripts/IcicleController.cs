using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcicleController : MonoBehaviour
{
    public float speed;
    public float angle;
    public float aliveDuration;
    private Rigidbody2D body;
    private bool isMoving;
    private float timeAlive;

    void Start()
    {
        transform.Rotate(0, 0, angle+90);
        body = GetComponent<Rigidbody2D>();
        isMoving = false;
        timeAlive = Time.deltaTime;
    }
    void FixedUpdate()
    {
        timeAlive += Time.deltaTime;
        if (timeAlive > aliveDuration)
            Destroy(gameObject);
        if (!isMoving)
        {
            body.velocity = new Vector2(Mathf.Cos(Mathf.Deg2Rad*angle), Mathf.Sin(Mathf.Deg2Rad*angle)) * speed;
            isMoving = true;
        }
        
    }
}
