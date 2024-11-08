using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    public GameObject player;
    public float speed;

    private float distance;
    public float range;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);        
        
        if (distance < range) 
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }
}
