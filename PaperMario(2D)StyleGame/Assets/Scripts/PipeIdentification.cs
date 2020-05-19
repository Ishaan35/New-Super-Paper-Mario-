using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeIdentification : MonoBehaviour
{
   

    private bool Pipe1 = false;
    private bool Pipe2 = false;
    private bool Pipe3 = false;

    private bool MoveDown = false;
    private bool MoveUp = false;

    private GameObject player;
    private CapsuleCollider2D PlayerCC;
    private Rigidbody2D PlayerRB;

    
    private GameObject MarioSprite;
    private GameObject FireMarioSprite;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        PlayerCC = player.GetComponent<CapsuleCollider2D>();
        PlayerRB = player.GetComponent<Rigidbody2D>();

        MarioSprite = player.transform.Find("Mario").gameObject;
        FireMarioSprite = player.transform.Find("FireMario").gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        if (Pipe1)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {

                PlayerRB.gravityScale = 0;
                MarioSprite.GetComponent<SpriteRenderer>().sortingOrder = -11;
                FireMarioSprite.GetComponent<SpriteRenderer>().sortingOrder = -11;
                MoveDown = true;
            }
        }

        if (MoveDown)
        {

            transform.position += new Vector3(0, -40, 0);
            MoveDown = false;

            PlayerRB.gravityScale = 1.5f;
            MarioSprite.GetComponent<SpriteRenderer>().sortingOrder = 0;
            FireMarioSprite.GetComponent<SpriteRenderer>().sortingOrder = 0;

        }

        if (Pipe2 || Pipe3) // Same mechanic
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {

                PlayerRB.gravityScale = 0;
                MarioSprite.GetComponent<SpriteRenderer>().sortingOrder = -11;
                FireMarioSprite.GetComponent<SpriteRenderer>().sortingOrder = -11;
                MoveUp = true;
            }
        }
        if (MoveUp)
        {
            MoveUp = false;
            transform.position += new Vector3(0, 40, 0);
            

            PlayerRB.gravityScale = 1.5f;
            MarioSprite.GetComponent<SpriteRenderer>().sortingOrder = 0;
            FireMarioSprite.GetComponent<SpriteRenderer>().sortingOrder = 0;

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Pipe")
        {
            Pipe1 = true;

            
        }
        if (other.gameObject.tag == "Pipe2")
        {
            Pipe2 = true;
        }
        if (other.gameObject.tag == "Pipe3")
        {
            Pipe3 = true;
        }


        if (other.gameObject.tag == "Ground")
        {
            PlayerCC.enabled = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Pipe")
        {
            Pipe1 = false;


        }
        if (other.gameObject.tag == "Pipe2")
        {
            Pipe2 = false;
        }
        if (other.gameObject.tag == "Pipe3")
        {
            Pipe3 = false;
        }
    }

     


}
