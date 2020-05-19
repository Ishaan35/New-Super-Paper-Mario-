using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy;

    public float spawnTime = 5;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", 1, spawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Spawn()
    {
        Instantiate(Enemy, transform.position, transform.rotation);
    }
}
