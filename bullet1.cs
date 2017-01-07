using UnityEngine;
using System.Collections;

public class bullet1 : MonoBehaviour {


    Sprite defaultSprite;
    public Sprite muzzleFlash;

    public int framestoFlash = 12;
    public float destroyTime = 6;
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

        if (player.transform.localScale.x < 0)
        {
            rotationSpeed *= -1;
            speed *= -1;
            transform.localScale = new Vector3(-1f, 1f, 1f);
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
        for(int i = 0;i<framestoFlash; i++)
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
        if (other.gameObject.tag == "Enemy")//|| other.gameObject.tag == "GreenGuardian")
        {
                  other.GetComponentInParent<EnemyHealth>().giveDamage(damageToGive);
        }
        if (other.gameObject.tag == "GreenGuardian")
        {
            other.gameObject.GetComponentInParent<GreenGuardianHealth>().giveDamage(damageToGive);
        }
        Instantiate(impactFX, transform.position, transform.rotation);
        Destroy(gameObject);
    }


    }
