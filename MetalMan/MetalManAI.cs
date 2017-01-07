using UnityEngine;
using System.Collections;
using System.Threading;

public class MetalManAI : MonoBehaviour {

    public GameObject transitions;
    public int enemyHealth;
    //Animator enemAnim;
    public GameObject deathFX;
    public int pointsOnDeath;
    //public AudioSource deadAudio;
   // AudioSource audios;
    public AudioSource deathSound;

    // Use this for initialization
    Animator myAnim;
    public Transform end;
    public GameObject metalBlade;
    public GameObject metalBladeRight;
    // Use this for initialization
    void Start()
    {
       // enemAnim = GetComponent<Animator>();
       // audios = GetComponent<AudioSource>();

        StartCoroutine(Intro());
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHealth <= 0)
        {

        //    Instantiate(deathFX, transform.position, transform.rotation);
            //ScoreManager.addPoints(pointsOnDeath);
            Destroy(gameObject);
           // deathSound.Play();

        }
        
        // LeanTween.moveX(gameObject.GetComponent<RectTransform>(), end.transform.position.x, 2f).setDelay(1f);
      
    }

    public void giveDamage(int damageToGive)
    {
        // gameObject.GetComponent<Animation>().Play("EnemyHurt");
       // audios.Play();
        enemyHealth -= damageToGive;

        //enemAnim.SetTrigger("hurt");
    }

    public IEnumerator Intro()
    {
        yield return new WaitForSeconds(.25f);
        StartCoroutine(Attack());
        yield break;
        
    }

    public IEnumerator Attack()
    {
        
        myAnim.SetBool("AttackRight", true);
        yield return new WaitForSeconds(.6f);
        Instantiate(metalBlade, end.transform.position, end.transform.rotation);
        yield return new WaitForSeconds(.55f);
        myAnim.SetBool("AttackRight", false);
        StopCoroutine(Attack());
        // yield return new WaitForSeconds(.5f);
        if (enemyHealth <= 250 && enemyHealth >= 100)
        {
            StartCoroutine(moveLeft());
        }
        else
        {
            StartCoroutine(Attack1());
        }

        
       
        
    }
    public IEnumerator moveLeft()
    {
        myAnim.SetBool("moveLeft", true);
        yield return new WaitForSeconds(1.3f);
        // Instantiate(metalBlade, Vector3(0,1.5,45f), end.rotation);
       // yield return new WaitForSeconds(1f);
        myAnim.SetBool("moveLeft", false);
        StopCoroutine(moveLeft());
       // yield return new WaitForSeconds(.5f);
        StartCoroutine(Attack1());
        
        
    }
    public IEnumerator Attack1()
    {
       
        myAnim.SetBool("AttackLeft", true);
        yield return new WaitForSeconds(.6f);
        Instantiate(metalBladeRight, end.transform.position, end.transform.rotation);
         yield return new WaitForSeconds(.6f);
        myAnim.SetBool("AttackLeft", false);
        StopCoroutine(Attack1());
        //yield return new WaitForSeconds(.5f);
        if (enemyHealth <= 250 && enemyHealth >= 100)
        {
            StartCoroutine(moveRight());
        }
        else
        {
            StartCoroutine(Attack());
        }
        
       
     
    }
    public IEnumerator moveRight()
    {
        myAnim.SetBool("moveRight", true);
        yield return new WaitForSeconds(1.2f);
        // Instantiate(metalBlade, Vector3(0,1.5,45f), end.rotation);
        //yield return new WaitForSeconds(1f);
        myAnim.SetBool("moveRight", false);
        StopCoroutine(moveRight());
        // yield return new WaitForSeconds(.5f);
        if (enemyHealth <= 250 && enemyHealth >= 100)
        {
            StartCoroutine(Intro());
        }
        else
        {
            StartCoroutine(Attack());
        }
   
       
    }
    public void transition()
    {
        if (enemyHealth < 100)
        {
            Instantiate(transitions, transform.position, transform.rotation);
        }
    }

    
}
