using UnityEngine;
using System.Collections;

public class BlobV2Beam : MonoBehaviour {

    public int pointDeducted;
    Sprite defaultSprite;
    public Sprite muzzleFlash;

    public int framestoFlash = 4;
    public float destroyTime = 4;
    SpriteRenderer spriteRend;

    //public GameObject enemyDeathFX;
    public float speed;
    PlayerControl player;
    public float rotationSpeed;
    Rigidbody2D starRB;
    public int damageToGive;
    public GameObject impactFX;

    // Use this for initialization
    void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        defaultSprite = spriteRend.sprite;

        StartCoroutine(FlashMuzzleFlash());
        StartCoroutine(TimedDestroy());

        player = FindObjectOfType<PlayerControl>();
        starRB = GetComponent<Rigidbody2D>();
        
        if (player.transform.position.x < transform.position.x)
        {
            rotationSpeed *= -1;
            speed *= -1;
            transform.localScale = new Vector3(-0.25f, 0.3f, 1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        starRB.velocity = new Vector2(speed, starRB.velocity.y);
        //for spinning projectile
        starRB.angularVelocity = rotationSpeed;
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
        }
        if(other.tag == "sword")
        {
            Destroy(gameObject);
        }
        Instantiate(impactFX, transform.position, transform.rotation);
        Destroy(gameObject);
    }


}
