using UnityEngine;

public class BulletScript : MonoBehaviour
{
    float timeAlive = 0;
    private void OnTriggerEnter2D(Collider2D col)
    { 
        if (col.CompareTag("Enemy"))
        {
            Debug.Log("hit");
            Destroy(gameObject);
        } 
    }

    private void Update()
    {
        timeAlive += 1 * Time.deltaTime;
        if (timeAlive > 1)
        {
            Debug.Log("life ended");
            Destroy(gameObject);
        }
    }
}
