using UnityEngine;
using System.Collections;

public class fireball : MonoBehaviour
{

    public Rigidbody2D rb;
   
    public Vector2 velocity;

    private GameObject player;

    private SpriteRenderer spriterenderer;

    public ParticleSystem FireDissolve;

    


    // Use this for initialization
    void Start()
    {
       
        rb = GetComponent<Rigidbody2D>();
        velocity = rb.velocity;

        spriterenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {

        // If the downwards velocity of the fireball is less than it is supposed to be, it will stay the velocity it is supposed to be to avoid arcs
        if (rb.velocity.y < velocity.y)
        {
            rb.velocity = velocity;
        }

        Physics2D.IgnoreLayerCollision(8, 9);

        

    }


    void OnCollisionEnter2D(Collision2D col)
    {

        rb.velocity = new Vector2(velocity.x, -velocity.y);

        if (col.gameObject.tag == "PlayerDamage")
        {
            Destroy(col.gameObject);
            FireDissolve.transform.position = transform.position;
            FireDissolve.Play();
            Destroy(gameObject);
            Score.Coins++; //Coins is a static variable so it can be accessed from the class directly.
            
        }
        if (col.gameObject.tag == "GoombaDeath")
        {
            Destroy(col.transform.parent.gameObject);
            FireDissolve.transform.position = transform.position;
            FireDissolve.Play();
            Destroy(gameObject);
            Score.Coins++;
        }

        // If the player is facing right
        if (player.transform.localScale.x < 0)
        {
            //If the fireball makes contact with a surface that has a perpendicular on the x axis which is less than 0, destroy object
            if (col.contacts[0].normal.x < 0 )
            {
                FireDissolve.transform.position = transform.position;
                FireDissolve.Play();
                Destroy(gameObject);
            }
        }

        // If player is facing left
        if (player.transform.localScale.x > 0)
        {
            // If the fireball makes contact with a surface that has a perpendicular on the x axis which is greatr than 0, destroy the object
            if (col.contacts[0].normal.x > 0)
            {
                FireDissolve.transform.position = transform.position;
                FireDissolve.Play();
                Destroy(gameObject);
            }
        }

        




    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Water")
        {
            spriterenderer.sortingOrder = -3;
        }
    }

}