using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;


public class PlayerMove : MonoBehaviour
{
    public float MoveSpeed = 8f; // Accessed by another script
    private Rigidbody2D rb;
    //ToDetect if grounded or not
    public bool Grounded;
    private bool Coincollect;

    private float jumpforce = 1800;
    private float swimforce = 400;
    //For detecting if swimming or not
    private  bool Swimming;


    public static bool firemario = false;
    public static bool regularmario = true;

    // A bool that turns off when mario exits water, so he does not have the ability to double jump
    public bool CanJump;


    public ParticleSystem splash;
    public ParticleSystem CoinSparkle;
    public ParticleSystem WalkDust;

    //Animators
    public Animator PlayerAnim;
    public Animator FireMarioAnim;
    

   
    // Sprite renderers
    public SpriteRenderer Playersprite;
    public SpriteRenderer FireMarioSprite;
    
    //Sprites gameobjects
    public GameObject Mario;
    public GameObject FireMario;

    // Used for direction later
    public static float localscale;

    //USed for the end scene function
    bool course_clear = false;
    bool end_game = false;


    public GameObject camera;

    public AudioSource CourseClear;
    public AudioSource CoinCollect;
    public AudioSource Overworld;
    private AudioSource Powerup;
    private AudioSource Powerdown;

    public AudioSource Underground_Theme;
    


    public Animator Course_Clear_Anim;
    public Animator FadeOUT;


    bool isDead = false;
    public Timer timer;


    public static bool LEVEL1COMPLETE = false;
    public static bool LEVEL2COMPLETE = false;

    public static int Lives = 5;
    


    

    
    


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        CanJump = true;


        CourseClear = camera.GetComponent<AudioSource>();



        Powerup = FireMario.transform.Find("PowerUpSound").GetComponent<AudioSource>();
        Powerdown = Mario.transform.Find("PowerDownSound").GetComponent<AudioSource>();

    }

    

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Grounded == true)
        {
            PlayerAnim.SetBool("Jump", false);
            FireMarioAnim.SetBool("Jump", false);
        }
        localscale = this.transform.localScale.x;

        //If timer ends
        if (timer.time < 1)
        {
            if (!end_game)
            {
                //a way for trying to disable player withoug actually destroying, so script can stll run its functions
                Mario.GetComponent<SpriteRenderer>().enabled = false;
                FireMario.GetComponent<SpriteRenderer>().enabled = false;
                rb.isKinematic = true;
                GetComponent<CapsuleCollider2D>().enabled = false;
                isDead = true;
                Overworld.Stop();
                StartCoroutine(GameOver());
                end_game = true;
            }

        }

        if (!course_clear && !isDead)
        {
            // Detects if the Player is moving or not
            if (Input.GetKey(KeyCode.RightArrow) || (Input.GetKey(KeyCode.LeftArrow)))
            {
                //Detects which way he is moving (right)
                if (Input.GetKey(KeyCode.RightArrow))
                {

                    {
                        transform.Translate(MoveSpeed * Time.deltaTime, 0, 0);
                        transform.localScale = new Vector3(-0.5f, transform.localScale.y, 0.5f);
                        if (Swimming == false)
                        {
                            PlayerAnim.SetBool("Run", true);
                            FireMarioAnim.SetBool("Run", true);

                        }
                    }

                }
                //Detects which way hes moving (left)
                if (Input.GetKey(KeyCode.LeftArrow))
                {

                    {
                        transform.Translate(-MoveSpeed * Time.deltaTime, 0, 0);
                        transform.localScale = new Vector3(0.5f, transform.localScale.y, 0.5f);
                        if (Swimming == false)
                        {
                            PlayerAnim.SetBool("Run", true);
                            FireMarioAnim.SetBool("Run", true);

                        }
                    }


                }

            }
            else
            {
                PlayerAnim.SetBool("Run", false);
                FireMarioAnim.SetBool("Run", false);

            }
            if (Input.GetKeyDown(KeyCode.Space) && CanJump)
            {
                if (Grounded == true && Swimming == false)
                {
                    rb.AddForce(transform.up * jumpforce);
                    PlayerAnim.SetBool("Jump", true);
                    FireMarioAnim.SetBool("Jump", true);

                    Grounded = false;
                }
                if (Swimming == true)
                {

                    rb.AddForce(transform.up * swimforce);


                }

            }
        }
        //If course clear bool is true
        else if (course_clear)
        {
            if (!end_game)
            {
                transform.Translate(0, 0, 0);
                StartCoroutine(wait_seconds(2));
                CourseClear.Play();
                end_game = true; //ensures that function gets called once in update();
            }
            
            camera.GetComponent<CameraFollow>().enabled = false;
            
            transform.Translate(6 * Time.deltaTime, 0, 0);
        }

        

        

       

    }

    private IEnumerator OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Grounded = true;
            PlayerAnim.SetBool("Jump", false);
            FireMarioAnim.SetBool("Jump", false);
            WalkDust.Play();
            CanJump = true;
        }
        if (collision.gameObject.tag == "Coin")
        {
            Score.Coins++;
            ParticleSystem CoinSparkleClone = Instantiate(CoinSparkle, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(CoinSparkleClone, 5);
            CoinSparkleClone.Play();
            CoinCollect.Play();
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "FireFlower")
        {

            Mario.SetActive(false);
            FireMario.SetActive(true);
            firemario = true;
            regularmario = false;
            Powerup.Play();
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Pipe")
        {
            
            Grounded = true;
            PlayerAnim.SetBool("Jump", false);
            FireMarioAnim.SetBool("Jump", false);

            CanJump = true;
        }

        // Colliding with enemies and getting injured
        if (collision.gameObject.tag == "PlayerDamage")
        {
            bool Invincible = false;
            //If he has no powerup
            if (firemario == false && Invincible == false && regularmario == true)
            {
                //a way for trying to disable player withoug actually destroying, so script can stll run its functions
                Mario.GetComponent<SpriteRenderer>().enabled = false;
                rb.isKinematic = true;
                GetComponent<CapsuleCollider2D>().enabled = false;
                isDead = true;
                Overworld.Stop();
                Lives--;
                camera.GetComponent<CameraFollow>().MoveWithPlayer = false;
                StartCoroutine(GameOver());
                

                

            }
            //if he has a powerup on
            else if (firemario == true && regularmario == false && Invincible == false)
            {
                firemario = false;
                regularmario = true;
                Mario.SetActive(true);
                FireMario.SetActive(false);
                Powerdown.Play();
                Invincible = true;
                if (Swimming)
                {
                    PlayerAnim.SetBool("Swim", true);
                }
                yield return new WaitForSeconds(2);
                Invincible = false;
            }

            

        }

        



    }

    

    private void OnTriggerEnter2D (Collider2D collision)
    {
        //Water enter
        if (collision.gameObject.tag == "Water" )
        {
            rb.gravityScale = 0.5f;
            Swimming = true;
            MoveSpeed = 5f;
            Playersprite.sortingOrder = -5;
            FireMarioSprite.sortingOrder = -5;
            
            //Fire and regular mario anim
            PlayerAnim.SetBool("Swim", true);
            FireMarioAnim.SetBool("Swim", true);

            PlayerAnim.SetBool("Jump", false);
            FireMarioAnim.SetBool("Jump", false);

            splash.transform.position = PlayerAnim.transform.position;
            splash.Play();
        }


        //For the first level ONLY
        if (collision.gameObject.tag == "CourseClear")
        {
            
            Overworld.Stop();
            Course_Clear_Anim.SetBool("Courseclear", true);
            course_clear = true;
            StartCoroutine(GameOver());
            LEVEL1COMPLETE = true;       
        }


        //For the second level course clear
        if (collision.gameObject.tag == "CourseClear2")
        {
            Overworld.Stop();
            Course_Clear_Anim.SetBool("Courseclear", true);
            course_clear = true;
            StartCoroutine(GameOver());
            LEVEL2COMPLETE = true;
        }


        if (collision.gameObject.tag == "Underground")
        {
            
            Underground_Theme.Play();
            Overworld.Stop();
        }

        if (collision.gameObject.tag == "DeathZones")
        {
            //a way for trying to disable player withoug actually destroying, so script can stll run its functions
            Mario.GetComponent<SpriteRenderer>().enabled = false;
            FireMario.GetComponent<SpriteRenderer>().enabled = false;
            rb.isKinematic = true;
            GetComponent<CapsuleCollider2D>().enabled = false;
            isDead = true;
            Overworld.Stop();
            Underground_Theme.Stop();
            Lives--;
            camera.GetComponent<CameraFollow>().MoveWithPlayer = false;
            StartCoroutine(GameOver());
            
        }


        
       

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Water")
        {
            Swimming = false;
            
            MoveSpeed = 8f;
            Playersprite.sortingOrder = 1;
            FireMarioSprite.sortingOrder = 1;

            //Fire and regular mario anim
            PlayerAnim.SetBool("Swim", false);
            FireMarioAnim.SetBool("Swim", false);
            PlayerAnim.SetBool("Jump", true);
            FireMarioAnim.SetBool("Jump", true);

            rb.gravityScale = 1.5f;
            splash.transform.position = PlayerAnim.transform.position;
            splash.Play();
            CanJump = false;
            

        }

        if (collision.gameObject.tag == "Underground")
        {
            Underground_Theme.Stop();
            Overworld.Play();
        }


    }

    IEnumerator wait_seconds(int seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(1);
        FadeOUT.SetBool("FadeOUT", true);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("WorldMap");
        
    }
    

    



}




