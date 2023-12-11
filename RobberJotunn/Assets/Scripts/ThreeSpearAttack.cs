using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeSpearAttack : MonoBehaviour
{
    public GameObject spear1;
    public GameObject spear2;
    public GameObject spear3;
    // Start is called before the first frame update
    void Start()
    {
        
        spear1.transform.position = transform.position;
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
