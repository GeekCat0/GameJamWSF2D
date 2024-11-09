using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public int itemNum;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player") && Input.GetButton("Fire1")) 
        {
            Debug.Log("picked up!");
            FindFirstObjectByType<PlayerController>().inventory[itemNum] = true;
            Destroy(gameObject);
        } 
    }
}
