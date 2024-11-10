using UnityEditor;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float lifeTime = 1;
    float timeAlive = 0;
    public int bulletType;
    private void OnTriggerEnter2D(Collider2D col)
    { 
        if (col.CompareTag("Enemy"))
        {
            col.GetComponent<EnemyAi>().attacked(bulletType);
            Destroy(gameObject);
        } 
    }

    private void Update()
    {
        timeAlive += 1 * Time.deltaTime;
        if (timeAlive > lifeTime)
        {
            Destroy(gameObject);
        }
    }
}
