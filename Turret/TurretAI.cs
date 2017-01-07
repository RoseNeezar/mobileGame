using UnityEngine;
using System.Collections;

public class TurretAI : MonoBehaviour
{
    PlayerControl player;
    public bool canStun = true;
    public float stunDuration;
    public float stunCooldown;
    public float waitBetweenShoots;
//    public int currHealth;
  //  public int maxHealth;
    public float distance;
    public float wakeRange;
    public float shootInterval;
    public float bulletSpeed;
    public float bulletTime;
    public bool awake = false;
    public bool lookingRight = true;
    public GameObject bullet;
    //public Transform target;
    public Animator anim;
    public Transform shootPointLeft;
    public Transform shootPointRight;

	// Use this for initialization
	void Awake ()
    {
        player = FindObjectOfType<PlayerControl>();
        anim = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Start ()
    {
       // currHealth = maxHealth;
	}
    void Update()
    {
        anim.SetBool("Awake", awake);
        anim.SetBool("lookRight", lookingRight);
        RangeCheck();

        if(player.transform.position.x> transform.position.x)
        {
            lookingRight = true;
        }
        if (player.transform.position.x < transform.position.x)
        {
            lookingRight = false;
        }

    }
    void RangeCheck()
    {
        distance = Vector3.Distance(transform.position, player.transform.position);
        //player to the left
        if(distance < wakeRange)
        {
            awake = true;
        }
        if(distance > wakeRange)
        {
            awake = false;
        }
    }

    public void Fire(bool fireRight)
    {
        bulletTime += Time.deltaTime;

        if(bulletTime >= shootInterval)
        {
            //return player direction
            Vector2 direction = player.transform.position - transform.position;
            direction.Normalize();
            //bullet
            if(!fireRight)
            {
                /* GameObject bulletClone;
                  bulletClone = Instantiate(bullet, shootPointLeft.transform.position, shootPointLeft.transform.rotation) as GameObject;
                  bulletClone.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;*/
                Instantiate(bullet, shootPointLeft.transform.position, shootPointLeft.transform.rotation);
                bulletTime = 0;
              
            }
            if (fireRight)
            {/*
                GameObject bulletClone;
                bulletClone = Instantiate(bullet, shootPointRight.transform.position, shootPointRight.transform.rotation) as GameObject;
                bulletClone.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;*/
                Instantiate(bullet, shootPointRight.transform.position, shootPointRight.transform.rotation);
                bulletTime = 0;
              
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "bullet" && canStun)
        {
            StartCoroutine(stun(stunDuration));
        }

    }

    IEnumerator stun(float boostDur) //Coroutine with a single input of a float called boostDur, which we can feed a number when calling
    {
        float time = 0; //create float to store the time this coroutine is operating
        canStun = false; //set canBoost to false so that we can't keep boosting while boosting

        while (boostDur > time) //we call this loop every frame while our custom boostDuration is a higher value than the "time" variable in this coroutine
        {
            time += Time.deltaTime; //Increase our "time" variable by the amount of time that it has been since the last update
            
            yield return 0; //go to next frame

        }

        yield return new WaitForSeconds(stunCooldown); //Cooldown time for being able to boost again, if you'd like.
        canStun = true; //set back to true so that we can boost again.      
    }


}
