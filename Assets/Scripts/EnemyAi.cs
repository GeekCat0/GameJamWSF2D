using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    public GameObject player;
    public float speed;

    private float distance;
    public float range;

    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);        
        
        if (distance < range) 
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }
}
