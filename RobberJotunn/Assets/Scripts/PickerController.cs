using UnityEngine;
public class PickerController : MonoBehaviour
{
    public GameObject weapon;
    public GameObject player;
    public GameObject jotunn;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Destroy(GameObject.FindGameObjectWithTag("Weapon"));

            GameObject weaponObject = Instantiate(weapon, player.transform.position, player.transform.rotation);
            
            weaponObject.transform.parent = player.transform;
            weaponObject.transform.localScale = new Vector3(1, 1, 1);

            player.GetComponent<PlayerController>().GiveWeapon();
        }
    }
}