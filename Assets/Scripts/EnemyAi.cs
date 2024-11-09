using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    GameObject player;
    public float speed;

    private float distance;
    public float range;

    public int weakness;
    public int buff;

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
        }
        else if (type == weakness)
        {
            Destroy(gameObject);
        }
        else 
        {
            Debug.Log("other");
        }
    }
}
