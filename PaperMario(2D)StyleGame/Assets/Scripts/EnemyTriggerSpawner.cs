using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTriggerSpawner : MonoBehaviour
{
    bool collide = false;

    public GameObject spawnlocation;
    public GameObject SpawnEnemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!collide)
            {
                Instantiate(SpawnEnemy, spawnlocation.transform.position, spawnlocation.transform.rotation);
                collide = true;
            }
            yield return new WaitForSeconds(2);
            collide = false;
        }
    }
}
