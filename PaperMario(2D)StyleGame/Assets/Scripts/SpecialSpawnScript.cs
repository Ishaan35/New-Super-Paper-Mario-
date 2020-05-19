using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialSpawnScript : MonoBehaviour
{
  

    public GameObject[] spawners;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            for(int i = 0; i < spawners.Length; i++)
            {
                spawners[i].SetActive(true);
            }
        }
    }
}
