using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    public JotunnController jotunnController;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Damageable"))
        {          
            jotunnController.Damage(1);
        }
    }
}
