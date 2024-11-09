using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed;
    float speedX, speedY;
    Rigidbody2D body;
    public int health = 3;

    public GameObject[] weapon;
    public shootingWeapon[] gun;
    int gunEquiped = 0;
    public Animator animator;

    Vector2 mousePos;

    float shootingDelay = 0;

    public Camera cam;

    public bool[] inventory = { false, false, false, false, false, false, false };
    public int equiped;
    int held = 0;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

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
        

        if (Input.GetKeyDown(KeyCode.Alpha1)){ equiped = 0; }
        else if (Input.GetKeyDown(KeyCode.Alpha2)){ equiped = 1; }
        else if (Input.GetKeyDown(KeyCode.Alpha3)){ equiped = 2; }
        else if (Input.GetKeyDown(KeyCode.Alpha4)){ equiped = 3; gunEquiped = 0; }
        else if (Input.GetKeyDown(KeyCode.Alpha5)){ equiped = 4; gunEquiped = 1; }
        else if (Input.GetKeyDown(KeyCode.Alpha6)){ equiped = 5; gunEquiped = 2; }
        

        weapon[equiped].transform.rotation = Quaternion.Euler(Vector3.forward * angle);

        if (inventory[equiped])
        {
            for (int i = 0; i < inventory.Length; i++)
            {
                if (i == equiped) { weapon[i].SetActive(true); held = i; } else { weapon[i].SetActive(false); }
            }
            if (Input.GetButtonDown("Fire1") && equiped <= 2) { mele(); }
            else { shoot(); }
        }

    }

    void shoot()
    {
        if (shootingDelay >= 100)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (weapon[held].GetComponentInChildren<Animator>() != null)
                {
                    weapon[held].GetComponentInChildren<Animator>().SetTrigger("attack");
                }
                gun[gunEquiped].Fire();
                shootingDelay = 0;
            }
        }
        else { shootingDelay++; }
    }

    void mele()
    {
        if (weapon[held].GetComponentInChildren<Animator>() != null)
        {
            weapon[held].GetComponentInChildren<Animator>().SetTrigger("attack");
        }
        //animator.SetTrigger("attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies) 
        {
            enemy.GetComponent<EnemyAi>().attacked(held);
        }
    }
    /*
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    */
}
