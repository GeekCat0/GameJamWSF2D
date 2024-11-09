using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    public GameObject[] enemies;
    float spawnTimer = 0;
    int choice;

    void Update()
    {
        if (FindAnyObjectByType<GameManager>().enemiesAlive <= 25)
        {
            if (spawnTimer >= 20)
            {
                spawnTimer = 0;
                choice = Random.Range(0, enemies.Length);
                Instantiate(enemies[choice],gameObject.transform.position,gameObject.transform.rotation);
                FindAnyObjectByType<GameManager>().enemiesAlive++;
            }
            else { spawnTimer += (2 * Time.deltaTime); }
        }
    }
}
