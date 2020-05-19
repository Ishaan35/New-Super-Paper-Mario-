using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheepCheep : MonoBehaviour
{
    public float speed;

    private Rigidbody2D CheepCHeepObjectRB;

    private CapsuleCollider2D CheepCheepObjectCC;

    bool Flip = false;
    // Start is called before the first frame update
    void Start()
    {
        CheepCHeepObjectRB = GetComponent<Rigidbody2D>();
        CheepCheepObjectCC = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheepCHeepObjectRB.velocity = new Vector2(1 * speed * Time.deltaTime, CheepCHeepObjectRB.velocity.y);
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
                    transform.localScale += new Vector3(-0.2f, 0, 0);
                    speed += 13;
                    yield return new WaitForSeconds(0.1f);
                    
                }
                yield return new WaitForSeconds(3);
                Flip = false;
            }
            else
            if (Flip == false && transform.localScale.x < 0)
            {
                Flip = true;
                Debug.Log("Hi");
                for (int i = 0; i < 10; i++)
                {
                    transform.localScale += new Vector3(0.2f, 0, 0);
                    speed -= 13;
                    yield return new WaitForSeconds(0.1f);

                }
                Flip = false;
            }

        }
    }
}
