using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public TMPro.TextMeshProUGUI healthLabel;

    private void Update(){
        healthLabel.text = "HEALTH: 3";
    }
}
