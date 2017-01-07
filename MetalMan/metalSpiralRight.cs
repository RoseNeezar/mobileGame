using UnityEngine;
using System.Collections;

public class metalSpiralRight : MonoBehaviour {

    public Vector3 eulerAngles;
    Sprite defaultSprite;
    public Sprite muzzleFlash;

    public int framestoFlash = 4;
    public float destroyTime = 4;
    SpriteRenderer spriteRend;
    MetalManAI metal;
    //public GameObject enemyDeathFX;
    public float speed;
   // PlayerControl player;
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
        metal = FindObjectOfType<MetalManAI>();
      //  player = FindObjectOfType<PlayerControl>();
        starRB = GetComponent<Rigidbody2D>();

        if (metal.transform.position.x < transform.position.x)
        {
            rotationSpeed *= -1;
            speed *= -1;
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 angel = new Vector3(8, 6, 1);
        Vector3 newForce = Quaternion.Euler(eulerAngles) * angel;
        starRB.AddForce(newForce);
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

            HealthManager.hurtPlayer(damageToGive);
        }
        Instantiate(impactFX, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
