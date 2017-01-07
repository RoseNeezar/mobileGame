using UnityEngine;
using System.Collections;

public class EnemyPatrol : MonoBehaviour {
    public float moveSpeed;
    public bool moveRight;
   
   // EnemyHealth enemHealth;
    public Transform wallCheck;
    public float wallCheckRadius;
    public LayerMask whatIsWall;
    bool hittingWall;

   public bool canStun = true;
    public float stunDuration;
    public float stunCooldown;

    public float knockbackx;
    public float knockbacky;
    public float knockbackLength;
    public float knockbackCount;
    public bool knockfromRight;

    bool notAtEdge;
    public Transform edgeCheck;

    Rigidbody2D enemRB;
    Animator enemyAnim;

    // Use this for initialization
    void Start()
    {
        enemyAnim = GetComponent<Animator>();
        enemRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        knockBack();
        if (enemyAnim.GetBool("Stun"))
            enemyAnim.SetBool("Stun", false);
        hittingWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, whatIsWall);
        notAtEdge = Physics2D.OverlapCircle(edgeCheck.position, wallCheckRadius, whatIsWall);
        //hit wall change position(if ground hit change pos)
        //at edge change position(if ground not hit change pos)
        if (hittingWall || !notAtEdge)
            moveRight = !moveRight;
        if (moveRight)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            enemRB.velocity = new Vector2(moveSpeed, enemRB.velocity.y);

        }
        else
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            enemRB.velocity = new Vector2(-moveSpeed, enemRB.velocity.y);
        }
    }
    void knockBack()
    {
        if (knockbackCount <= 0)
        {
            enemRB.velocity = new Vector2(moveSpeed, enemRB.velocity.y);
        }
        else
        {
            if (knockfromRight)
                enemRB.velocity = new Vector2(-knockbackx, knockbacky);
            if (!knockfromRight)
                enemRB.velocity = new Vector2(knockbackx, knockbacky);
            knockbackCount -= Time.deltaTime;
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
       
        if (other.tag == "bullet" && canStun)
        {
            StartCoroutine(stun(stunDuration));
        }
        if(other.tag == "sword")
        {
            enemyAnim.SetTrigger("hurt");
        }
        if (other.tag == "Shield")
        {
            StartCoroutine(stun(stunDuration));/*
            knockbackCount = knockbackLength;
          
            if (other.transform.position.x < transform.position.x)
                knockfromRight = true;
            else
                knockfromRight = false;*/
        }

    }
    
    IEnumerator stun(float boostDur) //Coroutine with a single input of a float called boostDur, which we can feed a number when calling
    {
        float time = 0; //create float to store the time this coroutine is operating
        canStun = false; //set canBoost to false so that we can't keep boosting while boosting
      
        while (boostDur > time) //we call this loop every frame while our custom boostDuration is a higher value than the "time" variable in this coroutine
        {
            time += Time.deltaTime; //Increase our "time" variable by the amount of time that it has been since the last update
            enemRB.velocity = new Vector2(0, 0);
            enemyAnim.SetBool("Stun", true);
            yield return 0; //go to next frame

        }

        yield return new WaitForSeconds(stunCooldown); //Cooldown time for being able to boost again, if you'd like.
        canStun = true; //set back to true so that we can boost again.      
    }
    
}
