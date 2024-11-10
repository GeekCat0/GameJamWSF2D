using UnityEngine;

public class BossAi : MonoBehaviour
{
    GameObject player;
    public float speed;
    public Rigidbody2D gun;
    public float cooldown;
    public GameObject bullet;

    public Transform shootPoint;

    private float distance;
    public float range;
    public float minDistance;

    public int weakness;
    public int buff;

    public int weakPage = 0;
    public int buffPage = 0;

    public int typeOfBuff = 0;

    private void Start()
    {
        player = FindAnyObjectByType<PlayerController>().gameObject;
    }

    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 lookDir = player.transform.position - gun.transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        gun.rotation = angle;
        
        if (distance < range && distance > minDistance) 
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        }
        cooldown += (2 * Time.deltaTime);
        if (cooldown > 5) 
        {
            cooldown = 0;
            GameObject shot = Instantiate(bullet, shootPoint.position, shootPoint.rotation);
            shot.GetComponent<Rigidbody2D>().AddForce(shootPoint.up * 3, ForceMode2D.Impulse);
        }
    }

    public void attacked(int type)
    {
        if (type == buff)
        {
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

            }
        }
    }
