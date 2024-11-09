using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    GameObject player;
    public float speed;

    private float distance;
    public float range;

    public int weakness;
    public int buff;

    public int weakPage = 0;
    public int buffPage = 0;

    private void Start()
    {
        player = FindAnyObjectByType<PlayerController>().gameObject;
    }

    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);        
        
        if (distance < range) 
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }

    public void attacked(int type)
    {
        if (type == buff)
        {
            speed += 1;
            FindAnyObjectByType<GameManager>().pagesCollected[buffPage] = true;
        }
        else if (type == weakness)
        {
            FindAnyObjectByType<GameManager>().enemiesAlive--;
            FindAnyObjectByType<GameManager>().pagesCollected[weakPage] = true;
            Destroy(gameObject);
        }
        else 
        {
            Debug.Log("other");
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player")) 
        {
            FindFirstObjectByType<PlayerController>().health -= 1;
            Destroy(gameObject);
        }
    }
}
