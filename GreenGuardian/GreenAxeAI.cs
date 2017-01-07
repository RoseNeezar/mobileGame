using UnityEngine;
using System.Collections;
using System.Threading;

public class GreenAxeAI : MonoBehaviour {

    public bool canStun = true;
    public float stunDuration;
    public float stunDurations;
    public float stunCooldown;
    PlayerControl player;
    //public Transform target;
    public  AudioSource aud;
    public GameObject debrie;
    public Transform point;
    public Transform sightStart, sightEnd;
    public bool spotted = false;
    Animator myAnim;
   // Animation enemAni;
    bool canSword = true;
    public float swordCooldown;
    public float swordDurationMove;
    // Use this for initialization
    void Start ()
    {
        player = FindObjectOfType<PlayerControl>();
        aud = GetComponent<AudioSource>();
        myAnim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (myAnim.GetBool("Stun"))
            myAnim.SetBool("Stun", false);
        if (player.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-1.25f, 1.25f, 1f);
        }
        if (player.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(1.25f, 1.25f, 1f);
        }/*
        if (!player.facingRight)
        {
            transform.localScale = new Vector3(-1.25f, 1.25f, 1f);
        }
        if (player.facingRight)
        {
            transform.localScale = new Vector3(1.25f, 1.25f, 1f);
        }*/

        Intro();
    }
    void FixedUpdate()
    {
        Raycasting();
        //  Behaviours();

    }
    void Raycasting()
    {
        Debug.DrawLine(sightStart.position, sightEnd.position, Color.green);
        spotted = Physics2D.Linecast(sightStart.position, sightEnd.position, 1 << LayerMask.NameToLayer("Player"));
    }

     void Intro()
    {
        if (myAnim.GetBool("Attack"))
           myAnim.SetBool("Attack", false);

        if (spotted && canSword )
        {
            StartCoroutine(swordmove(swordDurationMove));
           // StartCoroutine(Attack());
        }     
    }

    public IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.5f);
        myAnim.SetBool("Attack", true);
        yield return new WaitForSeconds(2f);

       // Instantiate(metalBlade, end.transform.position, end.transform.rotation);    
        myAnim.SetBool("Attack", false);
        StopCoroutine(Attack());
        yield break;
       // myAnim.SetBool("Attack", true);  
    }
    IEnumerator swordmove(float boostDur) //Coroutine with a single input of a float called boostDur, which we can feed a number when calling
    {

        float time = 0; //create float to store the time this coroutine is operating
        canSword = false; //set canBoost to false so that we can't keep boosting while boosting

        while (boostDur > time) //we call this loop every frame while our custom boostDuration is a higher value than the "time" variable in this coroutine
        {
            time += Time.deltaTime; //Increase our "time" variable by the amount of time that it has been since the last update
            myAnim.SetBool("Attack", true);
            yield return 0; //go to next frame
        }
        yield return new WaitForSeconds(swordCooldown); //Cooldown time for being able to boost again, if you'd like.
        canSword = true; //set back to true so that we can boost again.      
    }
   public void debris()
    {
        aud.Play();
        Instantiate(debrie, point.transform.position, point.transform.rotation);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.tag == "bullet" && canStun)
        {
            StartCoroutine(stun(stunDuration));
        }
        if (other.tag == "sword" )
        {
            myAnim.SetLayerWeight(1, 1);
            
            myAnim.SetTrigger("hurt");
        }
        else
        {
            myAnim.SetLayerWeight(1, 0);
        }
        if (other.tag == "Shield")
        {
            StartCoroutine(stun(stunDurations));
        
        }

    }
    IEnumerator stun(float boostDur) //Coroutine with a single input of a float called boostDur, which we can feed a number when calling
    {
        float time = 0; //create float to store the time this coroutine is operating
        canStun = false; //set canBoost to false so that we can't keep boosting while boosting

        while (boostDur > time) //we call this loop every frame while our custom boostDuration is a higher value than the "time" variable in this coroutine
        {
            time += Time.deltaTime; //Increase our "time" variable by the amount of time that it has been since the last update
           // enemRB.velocity = new Vector2(0, 0);
            myAnim.SetBool("Stun", true);
            yield return 0; //go to next frame

        }

        yield return new WaitForSeconds(stunCooldown); //Cooldown time for being able to boost again, if you'd like.
        canStun = true; //set back to true so that we can boost again.      
    }



}






