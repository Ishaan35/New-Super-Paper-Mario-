using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private bool touchinglevel = false;
   

    private PlayerMove playerscript;

    public int Level;

    public Animator level1Anim;
    


    
    // Start is called before the first frame update
    void Start()
    {

        playerscript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerMove.LEVEL1COMPLETE)
        {
            level1Anim.SetBool("Completed", true);
        }
        if (touchinglevel)
        {

            
            if (Input.GetKeyDown(KeyCode.Space))
            {

                SceneManager.LoadScene("Level1");
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
