using UnityEngine;
public class PickerController : MonoBehaviour
{
    public GameObject weapon;
    public GameObject player;
    public SpriteRenderer weaponRenderer;
    public AudioSource sound;
    private bool playerInRange = false;
    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Destroy(GameObject.FindGameObjectWithTag("Weapon"));

            GameObject weaponObject = Instantiate(weapon, player.transform.position, player.transform.rotation);
            
            weaponObject.transform.parent = player.transform;
            weaponObject.transform.localScale = new Vector3(1, 1, 1);

            WeaponController weaponController = weaponObject.GetComponent<WeaponController>();

            player.GetComponent<PlayerController>().GiveWeapon(weaponController);

            GameObject[] pedastals = GameObject.FindGameObjectsWithTag("Pedastal");

            foreach (GameObject pedastal in pedastals)
            {
                GameObject pedastalWeapon = pedastal.transform.GetChild(0).gameObject;
                if (!pedastalWeapon.activeSelf)
                {
                    pedastalWeapon.SetActive(true);
                    break;
                }
            }
          
            transform.GetChild(0).gameObject.SetActive(false);

            sound.Play();
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