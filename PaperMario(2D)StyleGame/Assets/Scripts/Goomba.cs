using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goomba : MonoBehaviour
{
    public Animator GoombaAnim;

    public CapsuleCollider2D GoombaCC;
    public BoxCollider2D GoombaBC;

    private GameObject goombaObject;
    private Rigidbody2D GoombaObjectRB;

    private CapsuleCollider2D GoombaObjectCC;

    public float speed;

    bool Flip = false;

    public float time;


    // Start is called before the first frame update
    void Start()
    {
        goombaObject = transform.parent.gameObject;
        GoombaObjectRB = goombaObject.GetComponent<Rigidbody2D>();
        GoombaObjectCC = goombaObject.GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        GoombaObjectRB.velocity = new Vector2(1 * speed * Time.deltaTime, GoombaObjectRB.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("wrfuerfu");
            GoombaObjectCC.enabled = false;
            GoombaAnim.SetBool("Dead", true);
            GoombaCC.enabled = false;
            GoombaBC.enabled = false;
            
        }
        // Velocity and scale change
        

    }
    private IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        // Velocity and scale change
        if (other.gameObject.tag == "EnemyWall")
        {
            if (Flip == false && transform.localScale.x > 0)
            {
                Flip = true;
                Debug.Log("Hi");
                for (int i = 0; i < 10; i++)
                {
                    transform.parent.gameObject.transform.localScale += new Vector3(-0.2f, 0, 0);
                    speed += 16;
                    yield return new WaitForSeconds(0.1f);

                }
                yield return new WaitForSeconds(time);
                Flip = false;
            }
            else
            if (Flip == false && transform.localScale.x < 0)
            {
                Flip = true;
                Debug.Log("Hi");
                for (int i = 0; i < 10; i++)
                {
                    transform.parent.gameObject.transform.localScale += new Vector3(0.2f, 0, 0);
                    speed -= 16;
                    yield return new WaitForSeconds(0.1f);

                }
                yield return new WaitForSeconds(time);
                Flip = false;
            }

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //if player already has touched the death collider, just disable the enemy collider too, so the player does not take any damage just in case.
            gameObject.transform.parent.GetComponent<CapsuleCollider2D>().enabled = false;
        }
    }



}
