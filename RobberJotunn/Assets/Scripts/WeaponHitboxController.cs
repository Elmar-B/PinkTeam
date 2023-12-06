using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponHitboxController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Damageable"))
        {          
            transform.parent.GetComponent<WeaponController>().Damage();
        }
    }
}
