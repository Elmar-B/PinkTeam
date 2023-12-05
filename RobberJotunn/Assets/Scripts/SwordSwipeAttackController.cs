using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSwipeAttackPhysics : MonoBehaviour
{
    public float startingRotationSpeed;
    private float rotationSpeed;
    public bool swing;
    public bool rightSwing;

    void Start()
    {
        if (rightSwing)
            transform.rotation = Quaternion.Euler(Vector3.forward * -120);
        else
        {
            transform.rotation = Quaternion.Euler(Vector3.forward * 120);
            startingRotationSpeed = -startingRotationSpeed;
        }
    }

    void FixedUpdate()
    {
        rotationSpeed = startingRotationSpeed * (1f - Mathf.Abs(transform.rotation.z));

        if ((transform.rotation.z > 0.9 && startingRotationSpeed > 0) || (transform.rotation.z < -0.9 && startingRotationSpeed < 0))
            //startingRotationSpeed *= -1;
            Destroy(gameObject);

        if (swing)
            transform.Rotate(0, 0, rotationSpeed * Time.fixedDeltaTime);
    }
}
