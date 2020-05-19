using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionBlockScript : MonoBehaviour
{
    public GameObject player;
    public Rigidbody2D PlayerRB;

    // Item id of 0 will represent regular brick block, item id 1 will spawn a coin, and item id 2 will spawn a fire flower
    public int ITEMID;
    public GameObject coin;
    public GameObject Fireflower;

    

    public GameObject QuestionBlock;

    public SpriteRenderer QuestionBlockSprite;

    
    //This bool will keep track if the block was hit or not
    public bool QuestionBlockHit;
    // Start is called before the first frame update
    void Start()
    {
        
        
        // These will we useful when the game detects if the player is juming to hit the question block or is just touching it, determined by his/her velocity.y, and will activate the block hitting code.
        player = GameObject.FindWithTag("Player");
        PlayerRB = player.GetComponent<Rigidbody2D>();
        // Assigning a component to the question block's sprite renderer
        QuestionBlockSprite = transform.parent.gameObject.GetComponent<SpriteRenderer>();

        


        // Question block is not hit in start of game
        QuestionBlockHit = false;

        QuestionBlock = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator OnCollisionEnter2D(Collision2D collision)
    {
        // If the player is actually moving up to hit the question block as determined by the velocity.y, then the question block will be hit
        if (collision.gameObject.tag == "Player" && PlayerRB.velocity.y >= 0 && QuestionBlockHit == false) // 3 conditions
        {
            if (ITEMID != 0)
            {
                QuestionBlockHit = true;
            }
            
            Debug.Log("QuestionBlockHit!");
            //Moves the QuestionBlock up and back down
            for (int i = 0; i < 5; i++)
            {
                transform.parent.gameObject.transform.position += new Vector3(0f, 0.08f, 0);
                yield return new WaitForSeconds(0.01f);
            }
            for (int i = 0; i < 4; i++)
            {
                transform.parent.gameObject.transform.position += new Vector3(0f, -0.1f, 0);
                yield return new WaitForSeconds(0.01f);
            }
            // Makes question Block invisible, so the empty block can show, only if it is not a brick block
            if (ITEMID !=0)
            {
                QuestionBlockSprite.enabled = false;
            }
            // 1 represents coin
            if (ITEMID == 1)
            {
                // Spawn clone of a coin and refer it to gameobject Clone
                GameObject Clone =  Instantiate(coin, QuestionBlock.transform.position, QuestionBlock.transform.rotation);
                Score.Coins++;
                // Coin Animation (coming out of block)
                for (int i = 0; i < 8; i++)
                {
                    Clone.transform.position += new Vector3(0, 0.15f, 0);
                    yield return new WaitForSeconds(0.01f);
                }
                //Buffer time
                yield return new WaitForSeconds(0.25f);
                
                Destroy(Clone);
            }
            // 2 represents fire flower
            if (ITEMID == 2)
            {
                GameObject clone = Instantiate(Fireflower, QuestionBlock.transform.position, QuestionBlock.transform.rotation);
                clone.transform.position -= new Vector3(0, 0.5f, 0);
                //Fire flower animation (coming out of block)
                for (int i = 0; i < 10; i++)
                {
                    clone.transform.position += new Vector3(0, 0.1f, 0);
                    yield return new WaitForSeconds(0.01f);
                }
            }
            if (ITEMID == 3)
            {
                // Spawn clone of a coin and refer it to gameobject Clone

                for (int i = 0; i < 10; i++)
                {
                    // Spawn clone of a coin and refer it to gameobject Clone
                    GameObject Clone = Instantiate(coin, QuestionBlock.transform.position, QuestionBlock.transform.rotation);
                    Score.Coins++;
                    // Coin Animation (coming out of block)
                    for (int j = 0; j < 8; j++)
                    {
                        Clone.transform.position += new Vector3(0, 0.15f, 0);
                        yield return new WaitForSeconds(0.01f);
                    }
                    //Buffer time
                    yield return new WaitForSeconds(0.25f);

                    Destroy(Clone);

                }
                
                
                
                
            }


        }
    }

   
}
