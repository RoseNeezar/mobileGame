using UnityEngine;
using System.Collections;

public class HomingMissleLanky : MonoBehaviour {

    public int pointDeducted;
    Sprite defaultSprite;
    public Sprite muzzleFlash;

    public int framestoFlash = 4;
    public float destroyTime = 4;
    SpriteRenderer spriteRend;

    public int damageToGive;
    public GameObject impactFX;

    public float speed = 5;
    public float rotatingSpeed = 200;
    public GameObject target;
    Rigidbody2D myRb;

	// Use this for initialization
	void Start ()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        myRb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        Vector2 point2Target = (Vector2)transform.position - (Vector2)target.transform.position;

        point2Target.Normalize();
        float value = Vector3.Cross(point2Target, transform.right).z;
        
        myRb.angularVelocity = rotatingSpeed * value;

        myRb.velocity = transform.right * speed;
	}

    IEnumerator FlashMuzzleFlash()
    {
        spriteRend.sprite = muzzleFlash;
        for (int i = 0; i < framestoFlash; i++)
        {
            yield return 0;
        }
        spriteRend.sprite = defaultSprite;
    }
    IEnumerator TimedDestroy()
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            ScoreManager.addPoints(-pointDeducted);
            HealthManager.hurtPlayer(damageToGive);
            Destroy(gameObject);
            Instantiate(impactFX, transform.position, transform.rotation);
        }
        if (other.tag == "sword")
        {
            Instantiate(impactFX, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        if (other.tag == "Shield")
        {
            Instantiate(impactFX, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        // Instantiate(impactFX, transform.position, transform.rotation);
        //Destroy(gameObject);
    }

}
