using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class JotunSpear : MonoBehaviour
{
    public float startingStapSpeed;
    private float stapSpeed;
    public bool stap;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (stap)
        {
            transform.position += new Vector3(0,2* Time.deltaTime,0);
        }
    }
}
