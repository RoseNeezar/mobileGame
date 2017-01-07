using UnityEngine;
using System.Collections;
using CnControls;
using UnityEngine.EventSystems;

public class PlayerControl : MonoBehaviour
{
    public bool isjumping= false;
    public int MaxBullet;
    public int CurrentBullet;

    public GameObject dust;
    public Transform boots;
    //walljump
    /*
    public bool wallSliding;
    public Transform wallCheckPoint;
    public bool wallCheck;
    public LayerMask wallLayerMask;
  
   */
    public bool facingRight = true;
    // dashCollider dashs;
    public float timeLeft;
    public Transform sightStart, sightEnd;
    public bool spotted = false;
    public int noOfClicks = 0;
    public int noOfClickss = 0;
    public int noOfClicksss = 0;
    //Time when last button was clicked
    float lastClickedTime = 0;
    float lastClickedTimes = 0;
    float lastClickedTimess = 0;
    //Delay between clicks for which clicks will be considered as combo
    float maxComboDelay = 1;
    float maxComboDelays = .75f;
    float maxComboDelayss = .75f;

    //  public fioSource jumpSoundFX;
    public AudioClip[] sounds;
    AudioSource audioSource;

  
    public GameObject impactFX;
    //basic movement n shooting
    public float moveSpeed;
    public float jumpHeight;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    public float groundCheckRadius;
    public bool grounded;
    public Transform firePoint;
    public GameObject bullet1;
    public float timeBetweenShots = 0.0111f;  // Allow 3 shots per second
  //  bool slash1;
  //  bool slash2;
   // bool slash3;
    public bool dash;

    public float dashSpeed;
    float dashDirection;

    bool canBoost = true;
    bool canSword = true;
    
    bool canShoot = true;
    public float boostCooldown;
    public float swordCooldown;
    

    public float shootCooldown;
    public float dashDuration;
 
    public float swordDurationMove;
  
    public float shootDuration;

    public Vector2 savedVelocity;
    private float timestamp;
    bool doubleJump;
    float moveDirection;
     //float downTime, upTime, pressTime = 0;
     //float countDown = 0.5f;
     //bool ready = false;

    public float knockback;
    public float knockbackLength;
    public float knockbackCount;
    public bool knockfromRight;

    Rigidbody2D myRB;
    Animator myAnim;
    // Use this for initialization
    void Start()
    {
        CurrentBullet = MaxBullet;
        audioSource = GetComponent<AudioSource>();
        myAnim = GetComponent<Animator>();
        myRB = GetComponent<Rigidbody2D>();
        myAnim.SetFloat("Speed", 0);
    }
    public void PlaySound(string name)
    {
        if (!audioSource.enabled) return;
        foreach (AudioClip clip in sounds)
        {
            if (clip.name == name)
            {
                audioSource.clip = clip;
                audioSource.Play();
                return;
            }
        }
        Debug.LogWarning(gameObject + ": AudioClip not found: " + name);
    }

    void FixedUpdate()
    {
        Raycasting();
      //  Behaviours();
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Time.time - lastClickedTime > maxComboDelay)
        {
            noOfClicks = 0;
        }

        if (Time.time - lastClickedTimes > maxComboDelays)
        {
            noOfClickss = 0;
        }
        if (Time.time - lastClickedTimess > maxComboDelayss)
        {
            noOfClicksss = 0;
        }

        shield();
        move();
        flip();
        jump();
        //runSword();
        airAttack();
       // shoot();
        combo();
        dashing();
        knockBack();
       

        GameObject[] remaining = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject clone in remaining)
        {
            if (clone.name == "Player(Clone)")
            {
                GameObject.Destroy(clone);
            }
        }


    }
    void Raycasting()
    {
        Debug.DrawLine(sightStart.position, sightEnd.position, Color.green);
        spotted = Physics2D.Linecast(sightStart.position, sightEnd.position,1<< LayerMask.NameToLayer("Ground"));
    }
   
    void flip()
    {
        if (myRB.velocity.x < 0)
        {
            facingRight = false;
            transform.localScale = new Vector3(-0.75f, 0.75f, 1f);
        }
        if (myRB.velocity.x > 0)
        {
            facingRight = true;
            transform.localScale = new Vector3(0.75f, 0.75f, 1f);
        }
    }

   public void move()
    {
        if (!dash)
        {
            myRB.velocity = new Vector2(moveDirection, myRB.velocity.y);

            moveDirection = moveSpeed * CnInputManager.GetAxis("Horizontal");

            myAnim.SetFloat("Speed", Mathf.Abs(myRB.velocity.x));

        }

    } 
   void jump()
    {
       
        // if (isjumping)
        //   isjumping = false;
        //myAnim.SetFloat("Jump/Land", myRB.velocity.y);
        if (myRB.velocity.y < 0)
        {
           // isjumping = false;
            myAnim.SetBool("land", true);
        }
       
        if (!grounded)
        {
            myAnim.SetLayerWeight(1, 1);
        }
        else
        {
            myAnim.SetLayerWeight(1, 0);
        }
            if (grounded)
            {
           
            myAnim.SetBool("land", false);
            myAnim.ResetTrigger("jump");
            doubleJump = true;
            }

            if (CnInputManager.GetButtonDown("Jump") && grounded)
            {
                 StartCoroutine(jumpss());
                // isjumping = true;
                 myAnim.SetTrigger("jump");
                 myRB.velocity = new Vector2(0, jumpHeight);
             }
            if (CnInputManager.GetButtonDown("Jump") && !grounded && doubleJump && !dash)
        {
            StartCoroutine(jumpss());
            //isjumping = true;
            myRB.velocity = new Vector2(0, jumpHeight);
                doubleJump = false;
            }
       

    }
    IEnumerator jumpss()
    {
        isjumping = true;
        yield return new WaitForSeconds(0.0075f);
        isjumping = false;
        yield break;
    }
    void shield()
    {
        if (myAnim.GetBool("Shield"))
                  myAnim.SetBool("Shield", false);

        if (CnInputManager.GetButtonDown("Submit") && grounded)
        {
            lastClickedTimess = Time.time;
            noOfClicksss++;
            if (noOfClicksss == 1)
            {
                myAnim.SetBool("Shield", true);
            }
            //limit/clamp no of clicks between 0 and 3 because you have combo for 3 clicks
            noOfClicksss = Mathf.Clamp(noOfClicksss, 0, 1);
        }
    }

    void runSword()
    {
        
        if (myAnim.GetBool("Sword"))
            myAnim.SetBool("Sword", false);
      
        if (CnInputManager.GetButton("Fire2")  && CnInputManager.GetAxis("Horizontal") != 0 && canSword &&grounded && !canShoot)
        {
           
            StartCoroutine(swordmove(swordDurationMove));
       }
    }
    IEnumerator swordmove(float boostDur) //Coroutine with a single input of a float called boostDur, which we can feed a number when calling
    {
        
        float time = 0; //create float to store the time this coroutine is operating
        canSword = false; //set canBoost to false so that we can't keep boosting while boosting

        while (boostDur > time) //we call this loop every frame while our custom boostDuration is a higher value than the "time" variable in this coroutine
        {
            time += Time.deltaTime; //Increase our "time" variable by the amount of time that it has been since the last update
            myAnim.SetBool("Sword", true);

            yield return 0; //go to next frame
           
        }
        yield return new WaitForSeconds(swordCooldown); //Cooldown time for being able to boost again, if you'd like.
        canSword = true; //set back to true so that we can boost again.      
    }
    void dashing()
    {
        
        if (myAnim.GetBool("Dash"))
            myAnim.SetBool("Dash", false);
        /*
        if (myAnim.GetBool("jumpDash"))
            myAnim.SetBool("jumpDash", false);
*/
        if (Input.GetButtonDown("Fire3") && canBoost && !spotted && CnInputManager.GetAxis("Horizontal") !=0 && grounded)
        {
            dash = true;

            StartCoroutine(Boost(dashDuration));
        }
    }
    IEnumerator Boost(float boostDur) //Coroutine with a single input of a float called boostDur, which we can feed a number when calling
    {
        if (myRB.velocity.x > 0 )
            dashDirection = dashSpeed;
        if (myRB.velocity.x < 0 )
            dashDirection = -dashSpeed;

        Vector2 boostSpeed = new Vector2(dashDirection, 0);
            float time = 0; //create float to store the time this coroutine is operating
            canBoost = false; //set canBoost to false so that we can't keep boosting while boosting

            while (boostDur > time) //we call this loop every frame while our custom boostDuration is a higher value than the "time" variable in this coroutine
            {
                time += Time.deltaTime; //Increase our "time" variable by the amount of time that it has been since the last update
                                        // if(grounded)
            myAnim.SetBool("Dash", true);
            gameObject.layer = 14;
                                                                                                                                          // if(!grounded)
                                                                                                                                          //   myAnim.SetBool("jumpDash", true);
            myRB.velocity = boostSpeed; //set our rigidbody velocity to a custom velocity every frame, so that we get a steady boost direction like in Megaman
                yield return 0; //go to next frame
                dash = false;

            }
            yield return new WaitForSeconds(boostCooldown); //Cooldown time for being able to boost again, if you'd like.
            canBoost = true; //set back to true so that we can boost again.  
        gameObject.layer = 9;
    }
    void airAttack()
    {
        if (myAnim.GetBool("air"))
            myAnim.SetBool("air", false);

        if (CnInputManager.GetButtonDown("Fire3") && !grounded && !CnInputManager.GetButtonDown("Jump"))
        {
            lastClickedTimes = Time.time;
            noOfClickss++;
            if (noOfClickss == 1)
            {      
                myAnim.SetBool("air",true);
            }
            //limit/clamp no of clicks between 0 and 3 because you have combo for 3 clicks
            noOfClickss = Mathf.Clamp(noOfClickss, 0, 1);
        }

    }
    void combo()
    {
       
        if (myAnim.GetBool("SwordCombo1"))
            myAnim.SetBool("SwordCombo1", false);
        if (myAnim.GetBool("SwordCombo2"))
            myAnim.SetBool("SwordCombo2", false);
        if (myAnim.GetBool("SwordCombo3"))
            myAnim.SetBool("SwordCombo3", false);
        
        if (CnInputManager.GetButtonDown("Fire3") && grounded)// && CnInputManager.GetAxis("Horizontal") == 0)// && CnInputManager.GetAxis("Horizontal") == 0)//&& CnInputManager.GetAxis("Horizontal") == 0)
        {
            lastClickedTime = Time.time;
            noOfClicks++;
            if (noOfClicks == 1)
            {
                myAnim.SetBool("SwordCombo1", true);
            }
            if (noOfClicks == 2)
            {
               
                myAnim.SetBool("SwordCombo2", true);
            }
            if (noOfClicks == 3)
            {
                myAnim.SetBool("SwordCombo3", true);
                noOfClicks = 0;
            }

            //limit/clamp no of clicks between 0 and 3 because you have combo for 3 clicks
            noOfClicks = Mathf.Clamp(noOfClicks, 0, 3);
        }
    }
    


    IEnumerator shot(float boostDur) //Coroutine with a single input of a float called boostDur, which we can feed a number when calling
    {
        float time = 0; //create float to store the time this coroutine is operating
        canShoot = false; //set canBoost to false so that we can't keep boosting while boosting

        while (boostDur > time) //we call this loop every frame while our custom boostDuration is a higher value than the "time" variable in this coroutine
        {
            time += Time.deltaTime; //Increase our "time" variable by the amount of time that it has been since the last update
            Instantiate(bullet1, firePoint.position, firePoint.rotation);
            myAnim.SetBool("Gun", true);
            yield return 0; //go to next frame

        }
        
        yield return new WaitForSeconds(shootCooldown); //Cooldown time for being able to boost again, if you'd like.
        canShoot = true; //set back to true so that we can boost again.      
    }
    


    void knockBack()
    {
        if(knockbackCount<=0)
        {
            myRB.velocity = new Vector2(moveDirection, myRB.velocity.y);
        }
        else
        {
            if (knockfromRight)
                myRB.velocity = new Vector2(-knockback, knockback);
            if (!knockfromRight)
                myRB.velocity = new Vector2(knockback, knockback);
            knockbackCount -= Time.deltaTime;
        }
    }
    
    public void tshoot()
    {
        
        if (canShoot && CurrentBullet !=0)
        {
            CurrentBullet -= 1;
            StartCoroutine(shot(shootDuration));
        }
    }
    public void noshoot()
    {
        myAnim.SetBool("Gun", false);
    }
   
      
    
    public void dashes()
    {
       // myAnim.SetBool("Shield", true);
        /*
        if (grounded && canBoost && CnInputManager.GetAxis("Horizontal") != 0 && !spotted )//&& myRB.velocity.x!=0 &&canBoost)
        {
            dash = true;
           
            StartCoroutine(Boost(dashDuration));
        }*/
    }
    public void stopdash()
    {
       //  myAnim.SetBool("Dash", false);
     myAnim.SetBool("Shield", false);

    }
    //2 collider hit or touch each other
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.transform.tag == "MovingPlateform")
        {
            transform.parent = other.transform;
        }
        
    }
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.transform.tag == "MovingPlateform")
        {
            transform.parent = null;//revert or nothing
        }

    }
    public void movedust()
    {
        Instantiate(dust,boots.transform.position,boots.transform.rotation);
    }

}
