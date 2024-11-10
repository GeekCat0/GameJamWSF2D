using UnityEngine;

public class enemyBullet : MonoBehaviour
{
    public float lifeTime = 1;
    float timeAlive = 0;
    public int bulletType;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            FindFirstObjectByType<PlayerController>().health -= 1;
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
