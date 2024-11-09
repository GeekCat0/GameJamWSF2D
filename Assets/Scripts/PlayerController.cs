using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed;
    float speedX, speedY;
    Rigidbody2D body;


    public GameObject[] weapon;
    public shootingWeapon[] gun;
    int gunEquiped = 0;
    int weaponEquiped = 1;

    Vector2 mousePos;

    float shootingDelay = 0;

    public Camera cam;

    public bool[] inventory = { false, false, false, false, false };

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        speedX = Input.GetAxisRaw("Horizontal") * moveSpeed;
        speedY = Input.GetAxisRaw("Vertical") * moveSpeed;
        body.linearVelocity = new Vector2 (speedX, speedY);

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        Vector2 lookDirection = mousePos - body.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        weapon[weaponEquiped].transform.rotation = Quaternion.Euler(Vector3.forward * angle);

        if (inventory[0]) { weapon[weaponEquiped].SetActive(true); }

        if (inventory[1]) { 
            weapon[weaponEquiped].SetActive(true);
            if (shootingDelay >= 600)
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    gun[gunEquiped].Fire();
                    shootingDelay = 0;
                }
            }
            else { shootingDelay++; }
        }
    }

}
