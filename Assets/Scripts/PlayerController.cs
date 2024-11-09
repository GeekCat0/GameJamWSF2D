using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed;
    float speedX, speedY;
    Rigidbody2D body;
    public GameObject weapon;
    Vector2 mousePos;

    public Camera cam;

    public bool[] inventory = { false, false, false, false, false };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        speedX = Input.GetAxisRaw("Horizontal") * moveSpeed;
        speedY = Input.GetAxisRaw("Vertical") * moveSpeed;
        body.linearVelocity = new Vector2 (speedX, speedY);

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        Vector2 lookDirection = mousePos - body.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        weapon.transform.rotation = Quaternion.Euler(Vector3.forward * angle);

        if (inventory[0]) { weapon.SetActive(true); }

    }

    private void FixedUpdate()
    {
        
    }
}
