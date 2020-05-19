using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShoot : MonoBehaviour
{
    public GameObject Fireball;
    public Vector2 velocity;

    public GameObject Firemario;

    private bool canshoot = true;

    public AudioSource Fire;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V) && PlayerMove.firemario)
        {
            Fire.Play();
            // Instantiates the fireball a certain distance away from player on x axis, depending on which direction mario is facing
            GameObject Clone = Instantiate(Fireball, new Vector3(transform.position.x + (1 * -PlayerMove.localscale), transform.position.y, transform.position.z) , Fireball.transform.rotation);

            Firemario.GetComponent<Animator>().SetTrigger("Throw");
            
            
            // Initial velocity of fireball going down depending on the side player is facing determined by player's localscale
            Clone.GetComponent<Rigidbody2D>().velocity = new Vector2(velocity.x * -transform.localScale.x, velocity.y);

            Destroy(Clone, 10);
        }

        

    }
}
