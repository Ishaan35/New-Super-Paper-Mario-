using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//This script is for the second level. I started with the original idea of creating a separate script for loading levels, but now i realized that i have to make multiple levels, so I did not want to scrap the whole idea
//So instead, since i am only making 2 levels, I decided to just make 2 scripts instead of incorporating the whole thing inside the player by scraping this whole idea. At the time of writing this, i already created level 1 loader.
public class SceneLoader1 : MonoBehaviour
{
    private bool touchinglevel = false;
   

    private PlayerMove playerscript;

    public int Level;

    public Animator level2Anim;
    


    
    // Start is called before the first frame update
    void Start()
    {

        playerscript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerMove.LEVEL2COMPLETE)
        {
            level2Anim.SetBool("Completed", true);
        }
        if (touchinglevel)
        {

            
            if (Input.GetKeyDown(KeyCode.Space))
            {

                SceneManager.LoadScene("Level2");
            }
            
        }
        
        



    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            
            touchinglevel = true;
            
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            touchinglevel = false;
        }
    }
}
