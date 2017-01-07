using UnityEngine;
using System.Collections;

public class FlyingEnemyMove : MonoBehaviour
{
    public bool spotted = false;
    public Transform sightStart, sightEnd;
    Animator enemAnim;


    public bool canStun = true;
    public float stunDuration;
    public float stunDurations;
    public float stunCooldown;

    public AudioClip[] sounds;
    AudioSource audioSource;

    PlayerControl  player;
    public float moveSpeed;
    public float playerRange;
    //layer to detect player
    public LayerMask playerLayer;
    public bool playerInRange;
    //player facing away from enemy
    public bool facingAway;
    public bool followOnLookAway;
         
	// Use this for initialization
	void Start ()
    {
        audioSource = GetComponent<AudioSource>();
        enemAnim = GetComponent<Animator>();
        player = FindObjectOfType<PlayerControl>();
	}
    void FixedUpdate()
    {
        Raycasting();
    }
    // Update is called once per frame
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
    void Update ()
    {
        if (enemAnim.GetBool("Stun"))
            enemAnim.SetBool("Stun", false);
        //draaw circle like the ground check        own gameobject  rangeToMoveTowrds  layerToMoveTowards
        playerInRange = Physics2D.OverlapCircle(transform.position, playerRange,playerLayer);
        if (!followOnLookAway)
        {
            if (playerInRange)
            {
                // just follow  and hit player                            flying pos        playerpos                speed to go to player
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
                
            }
        }
        //player left/right and facing left/right from enemy                  
        if((player.transform.position.x<transform.position.x && player.transform.localScale.x<0) || (player.transform.position.x > transform.position.x && player.transform.localScale.x > 0))
        {
            facingAway = true;
        }
        else
        {
            facingAway = false;
        }
        //move towards player  when player away from enemy and not if player move towards the enemy
        if (playerInRange )//&& facingAway)
        {
            if (enemAnim.GetBool("attack"))
                enemAnim.SetBool("attack", false);

            if(spotted)
            {
                enemAnim.SetBool("attack", true);
            }
            // follow                                      flying pos        playerpos                speed to go to player
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
            return;//wont effect movement towards player(operate seprately)
        }

    }
    //draw area of effect(player proximation)
    void OnDrawGizmosSelected()
    {
        //                     center point     radius
        Gizmos.DrawSphere(transform.position, playerRange);
    }
    void Raycasting()
    {
        Debug.DrawLine(sightStart.position, sightEnd.position, Color.green);
        spotted = Physics2D.Linecast(sightStart.position, sightEnd.position, 1 << LayerMask.NameToLayer("Ground"));
    }
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "bullet" && canStun)
        {
            StartCoroutine(stun(stunDuration));
        }
        if (other.tag == "sword")
        {
           // enemAnim.SetLayerWeight(1, 1);

            enemAnim.SetTrigger("hurt");
        }
        else
        {
            enemAnim.SetLayerWeight(1, 0);
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
            enemAnim.SetBool("Stun", true);
            yield return 0; //go to next frame

        }

        yield return new WaitForSeconds(stunCooldown); //Cooldown time for being able to boost again, if you'd like.
        canStun = true; //set back to true so that we can boost again.      
    }



}
