using UnityEngine;
using System.Collections;

public class ShockMasterAI : MonoBehaviour
{
    public AudioClip[] sounds;
    AudioSource audioSource;

    public bool canStun = true;
    public float stunDuration;
    public float stunDurations;
    public float stunCooldown;
   
    bool canSword = true;
    public float swordCooldown;
    public float swordDurationMove;
    bool canSwords = true;
    public float swordCooldowns;
    public float swordDurationMoves;

    public bool facingLeft = true;
    public GameObject rocket;
    public Transform rightUp3,leftUp3, rightUp1, leftUp1, rightUp2, leftUp2,startAttack,endAttack,shootPoint;
    public Transform sightStart, sightEnd;
    public bool spotted = false;
    public bool shot = false; public bool shot1 = false; public bool shot2 = false; public bool melee = false;

    public float moveSpeed;
    PlayerControl player;
    Rigidbody2D enemRB;
    Animator myAnim;
    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        player = FindObjectOfType<PlayerControl>();
        enemRB = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
    }
    void FixedUpdate()
    {
        Raycasting();
    }
    void Raycasting()
    {
        Debug.DrawLine(sightStart.position, sightEnd.position, Color.green);
        Debug.DrawLine(rightUp1.position, leftUp1.position, Color.green);
        Debug.DrawLine(rightUp2.position, leftUp2.position, Color.green);
        Debug.DrawLine(rightUp3.position, leftUp3.position, Color.green);
        Debug.DrawLine(startAttack.position, endAttack.position, Color.red);
        spotted = Physics2D.Linecast(sightStart.position, sightEnd.position, 1 << LayerMask.NameToLayer("Player"));
        shot = Physics2D.Linecast(rightUp1.position, leftUp1.position, 1 << LayerMask.NameToLayer("Player"));
        shot1 = Physics2D.Linecast(rightUp2.position, leftUp2.position, 1 << LayerMask.NameToLayer("Player"));
        shot2 = Physics2D.Linecast(rightUp3.position, leftUp3.position, 1 << LayerMask.NameToLayer("Player"));
        melee = Physics2D.Linecast(startAttack.position, endAttack.position, 1 << LayerMask.NameToLayer("Player"));
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
    void Update()
    {
        if (myAnim.GetBool("Stun"))
            myAnim.SetBool("Stun", false);
        flip();
        Movement();
        shoot();
        meleeing();
    }
    void flip()
    {
        if (player.transform.position.x > transform.position.x)
        {
            facingLeft = false;
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        if (player.transform.position.x < transform.position.x)
        {
            facingLeft = true;
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    void Movement()
    {
        myAnim.SetBool("run", false);
        if (spotted)
        {
            myAnim.SetBool("run", true);
            if (facingLeft)
            {
                enemRB.velocity = new Vector2(-moveSpeed, enemRB.velocity.y);
            }
            else
            {
                enemRB.velocity = new Vector2(moveSpeed, enemRB.velocity.y);
            }
        }
    }
    void shoot()
    {
        if ((shot || shot1 || shot2)&& canSword)
        {
            StartCoroutine(swordmove(swordDurationMove));
        }
    }
    IEnumerator swordmove(float boostDur) 
    {
        float time = 0; 
        canSword = false; 

        while (boostDur > time)
        {
            time += Time.deltaTime; 
            Instantiate(rocket, shootPoint.transform.position, shootPoint.transform.rotation);
            yield return 0; 
        }
        yield return new WaitForSeconds(swordCooldown); 
        canSword = true;  
    }

    void meleeing()
    {
        if (myAnim.GetBool("melee"))
            myAnim.SetBool("melee", false);
        if (melee && canSwords)
        {
            StartCoroutine(meelee(swordDurationMoves));
        }
    }

    IEnumerator meelee(float boostDur) 
    {
        float time = 0;
        canSwords = false; 
        while (boostDur > time) 
        {
            time += Time.deltaTime; 
            myAnim.SetBool("melee", true);
            yield return 0;
        }
        yield return new WaitForSeconds(swordCooldowns);
        canSwords = true;     
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "bullet" && canStun)
        {
            StartCoroutine(stun(stunDuration));
        }
        if (other.tag == "sword")
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
    IEnumerator stun(float boostDur)
    {
        float time = 0; 
        canStun = false; 

        while (boostDur > time) 
        {
            time += Time.deltaTime;                                   
            myAnim.SetBool("Stun", true);
            yield return 0; 
        }
        yield return new WaitForSeconds(stunCooldown); 
        canStun = true; 
    }
}
