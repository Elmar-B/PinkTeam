using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningScript : MonoBehaviour
{
    public GameObject lightningStrike;
    public float delayTimer;

    // Start is called before the first frame update
    void Start()
    {
        // Call SummonStrike every delayTimer second repete n
        InvokeRepeating("SummonStrike",2.0f,delayTimer);
    }

    private void SummonStrike(){
        float yAxis = Random.Range(0.5f, -1.0f);
        float xAxis = Random.Range(1.5f, -1.5f);
        GameObject lightning = Instantiate(lightningStrike);
        lightning.transform.position = new Vector3(xAxis,yAxis,0);
    }
}
