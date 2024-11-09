using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public int itemNum;

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
