using Unity.Collections;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public GameObject grass;
    public GameObject grass2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for(int i = 0; i < 1000; i++)
        {
           Vector3 position = new Vector2(Random.Range(-50.0F, 50.0F), Random.Range(-50.0F, 50.0F));
	        Instantiate(grass, position, transform.rotation);
        }
        for(int i = 0; i < 1000; i++)
        {
           Vector3 position = new Vector2(Random.Range(-50.0F, 50.0F), Random.Range(-50.0F, 50.0F));
	        Instantiate(grass2, position, transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
