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
        InvokeRepeating("StrikeOnPlayer",6.0f,3.0f);
    }

    private void SummonStrike(){
        float yAxis = Random.Range(2.5f, 0.85f);
        float xAxis = Random.Range(1.5f, -1.5f);
        GameObject lightning = Instantiate(lightningStrike);
        lightning.transform.position = new Vector3(xAxis,yAxis,0);
    }

    private void StrikeOnPlayer(){
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject lightning = Instantiate(lightningStrike);
        lightning.transform.position = player.transform.position;
    }
}
