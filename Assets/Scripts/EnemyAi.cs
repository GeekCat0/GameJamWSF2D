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

    public int typeOfBuff = 0;

    public AudioSource dieSound;
    

    private void Start()
    {
        player = FindAnyObjectByType<PlayerController>().gameObject;
        dieSound = FindAnyObjectByType<GameManager>().dieSound;
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
            FindAnyObjectByType<GameManager>().pagesCollected[buffPage] = true;
            switch (typeOfBuff)
            {
                case 0:
                    speed += 3;
                    FindAnyObjectByType<GameManager>().pagesCollected[buffPage] = true;
                    break;
                case 1:
                    Instantiate(gameObject,gameObject.transform.position + new Vector3(3, 0, 0), gameObject.transform.rotation) ;
                    break;
                case 2:
                    gameObject.transform.localScale += new Vector3(0.3f, 0.3f, 0.3f);
                    break;
                default:
                    break;
            }
        }
        else if (type == weakness)
            {
                dieSound.Play();
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
