using TMPro.Examples;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
   
   
	public AudioSource audio;
    public AudioClip clip;
    bool paused = false;
    public float cooldown = 0;
    private float step = 3;
    public Animator steps;
    public float moveSpeed;
    float speedX, speedY;
    Rigidbody2D body;
    public int health = 3;
    public GameObject deathscreen;

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

    public GameObject[] hearts;
    public GameObject[] items;

    public GameObject journal;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && paused) { paused = false; deathscreen.SetActive(false); }
        moveSpeed = 6.5f;
        cooldown += (2 * Time.deltaTime);
        if ((cooldown > 5 && Input.GetKeyDown("space")))
        {
            step = 0;
            cooldown = 0;
            moveSpeed = 15;
        }
        if (step < 1)
        {
            moveSpeed = 15;
            step += (1 * Time.deltaTime);
        }
        steps.speed = moveSpeed;
        speedX = Input.GetAxisRaw("Horizontal") * moveSpeed;
        speedY = Input.GetAxisRaw("Vertical") * moveSpeed;
        body.linearVelocity = new Vector2 (speedX, speedY);

        if (speedX == 0 && speedY == 0)
        {
            steps.enabled = false;
        }else { steps.enabled = true; }


        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        Vector2 lookDirection = mousePos - body.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            journal.SetActive(!journal.activeSelf);
        }
        if (journal.activeSelf) { Time.timeScale = 0.0f; }
        else if (paused) { Time.timeScale = 0.0f; deathscreen.SetActive(true); }
        else { Time.timeScale = 1.0f; }

        if (Input.GetKeyDown(KeyCode.Alpha1)){ equiped = 0; }
        else if (Input.GetKeyDown(KeyCode.Alpha2)){ equiped = 1; }
        else if (Input.GetKeyDown(KeyCode.Alpha3)){ equiped = 2; }
        else if (Input.GetKeyDown(KeyCode.Alpha4)){ equiped = 3; gunEquiped = 0; }
        else if (Input.GetKeyDown(KeyCode.Alpha5)){ equiped = 4; gunEquiped = 1; }
        else if (Input.GetKeyDown(KeyCode.Alpha6)){ equiped = 5; gunEquiped = 2; }
        

        weapon[equiped].transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        Vector2 scale = weapon[equiped].transform.localScale;
        if (angle > -90 && angle < 90)
        {
            if (equiped > 2)
            {
                weapon[equiped].GetComponentInChildren<SpriteRenderer>().flipY = false;
            }
            else 
            {
                scale.y = -1;
            }
        }
        else if (equiped > 2) { weapon[equiped].GetComponentInChildren<SpriteRenderer>().flipY = true; }
        else 
        {
            scale.y = 1;
        }

        weapon[equiped].transform.localScale = scale;

        if (inventory[equiped])
        {
            for (int i = 0; i < inventory.Length; i++)
            {
                if (i == equiped) { weapon[i].SetActive(true); held = i; } else { weapon[i].SetActive(false); }
            }
            if (Input.GetButtonDown("Fire1") && equiped <= 2) { mele(weapon[equiped]); }
            else { shoot(); }
        } else { for (int i = 0; i < inventory.Length; i++) { weapon[i].SetActive(false); } }
        if (health <= 0) 
        {
            //audio.PlayOneShot(clip);
            Debug.Log("Died");
            paused = true;
            EnemyAi[] enemies = GameObject.FindObjectsOfType<EnemyAi>();
            FindAnyObjectByType<GameManager>().enemiesAlive = 0;
            foreach (EnemyAi go in enemies) { Destroy(go.gameObject); }
            health = 3;
            for (int i = 0; i < inventory.Length; i++) { inventory[i] = false; }
            gameObject.transform.position = new Vector3(0, 0, 0);
            for (int i = 0; i < inventory.Length; i++) {weapon[i].SetActive(false); }
        }

        for (int i = 0; i < hearts.Length; i++) 
        {
            if (health >= i + 1) { hearts[i].SetActive(true); } else { hearts[i].SetActive(false); }
            
        }

        for (int i = 0; i < items.Length; i++)
        {
            items[i].SetActive(inventory[i]);
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

    void mele(GameObject point)
    {
        if (weapon[held].GetComponentInChildren<Animator>() != null)
        {
            weapon[held].GetComponentInChildren<Animator>().SetTrigger("attack");
        }
        //animator.SetTrigger("attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(point.transform.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies) 
        {
            
            enemy.GetComponent<EnemyAi>().attacked(held);
        }
    }
}
