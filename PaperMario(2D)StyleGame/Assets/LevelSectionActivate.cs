using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//I made this script and idea because there is a particular section in the level which heavy in rendering, and this will improve performance, as it only activates when the player tries to go there.
public class LevelSectionActivate : MonoBehaviour
{
    public GameObject LevelSection; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            LevelSection.SetActive(true);
        }
    }
}
