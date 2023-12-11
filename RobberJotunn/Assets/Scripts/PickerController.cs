using UnityEngine;
public class PickerController : MonoBehaviour
{
    public GameObject weapon;
    public GameObject player;
    public SpriteRenderer weaponRenderer;
    private bool playerInRange = false;
    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("test");
            Destroy(GameObject.FindGameObjectWithTag("Weapon"));

            GameObject weaponObject = Instantiate(weapon, player.transform.position, player.transform.rotation);
            
            weaponObject.transform.parent = player.transform;
            weaponObject.transform.localScale = new Vector3(1, 1, 1);

            player.GetComponent<PlayerController>().GiveWeapon();            
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInRange = true;
            weaponRenderer.color = new Color(1, 1, 0);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInRange = false;
            weaponRenderer.color = new Color(1, 1, 1);
        }
    }
}