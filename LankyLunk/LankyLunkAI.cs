using UnityEngine;
using System.Collections;

public class LankyLunkAI : MonoBehaviour {

    public AudioClip[] sounds;
    AudioSource audioSource;

    public bool canStun = true;
    public float stunDuration;
    public float stunDurations;
    public float stunCooldown;
    PlayerControl player;

    public AudioClip aud;
    AudioSource audsource;
    public GameObject missles;
    public Transform point;
    public Transform sightStart, sightEnd;
    public Transform sightStartM, sightEndM;
    public bool spotted = false;
    public bool spottedM = false;
    Animator myAnim;
    
    bool canSword = true;
    public float swordCooldown;
    public float swordDurationMove;


    // Use this for initialization
    void Start ()
    {
        audioSource = GetComponent<AudioSource>();
        player = FindObjectOfType<PlayerControl>();
        audsource = GetComponent<AudioSource>();
        myAnim = GetComponent<Animator>();
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
    // Update is called once per frame
    void Update ()
    {
        if (myAnim.GetBool("Stun"))
            myAnim.SetBool("Stun", false);
        if (player.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-1.5f, 1.5f, 1f);
        }
        if (player.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(1.5f, 1.5f, 1f);
        }

        Intro();
    }
    void FixedUpdate()
    {
        Raycasting();
        RaycastingM();
    }
    void Raycasting()
    {
        Debug.DrawLine(sightStart.position, sightEnd.position, Color.green);
        spotted = Physics2D.Linecast(sightStart.position, sightEnd.position, 1 << LayerMask.NameToLayer("Player"));
    }
    void RaycastingM()
    {
        Debug.DrawLine(sightStartM.position, sightEndM.position, Color.green);
        spottedM = Physics2D.Linecast(sightStartM.position, sightEndM.position, 1 << LayerMask.NameToLayer("Player"));
    }
    void Intro()
    {
        if (myAnim.GetBool("shoot"))
            myAnim.SetBool("shoot", false);

        if (spotted && canSword)
        {
            StartCoroutine(swordmove(swordDurationMove));
        }
        if (spottedM)
        {
            StartCoroutine(Attack());
        }
    }

    public IEnumerator Attack()
    {
        yield return new WaitForSeconds(1f);
        myAnim.SetBool("melee", true);
        yield return new WaitForSeconds(2f);

        // Instantiate(metalBlade, end.transform.position, end.transform.rotation);    
        myAnim.SetBool("melee", false);
        yield return new WaitForSeconds(2f);
      //  myAnim.SetBool("melee", true);

    }
    IEnumerator swordmove(float boostDur) //Coroutine with a single input of a float called boostDur, which we can feed a number when calling
    {

        float time = 0; //create float to store the time this coroutine is operating
        canSword = false; //set canBoost to false so that we can't keep boosting while boosting

        while (boostDur > time) //we call this loop every frame while our custom boostDuration is a higher value than the "time" variable in this coroutine
        {
            time += Time.deltaTime; //Increase our "time" variable by the amount of time that it has been since the last update
            myAnim.SetBool("shoot", true);
            yield return 0; //go to next frame
        }
        yield return new WaitForSeconds(swordCooldown); //Cooldown time for being able to boost again, if you'd like.
        canSword = true; //set back to true so that we can boost again.      
    }

    public void missle()
    {
        audsource.PlayOneShot(aud, 0.5f);
        Instantiate(missles, point.transform.position, point.transform.rotation);
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
