using UnityEngine;
using UnityEngine.Advertisements;
using System.Collections;

public class LevelManager : MonoBehaviour {


    [SerializeField]
    string gameID = "1232146";
    LifeManager lifeSystem;

    public GameObject currentCheckpoint;
    //particle effect
    public GameObject deathParticle;
    public GameObject respawnParticle;
    //variable for delay
    public float respawnDelay;

    //camera controller
    CameraControl cameras;

    public HealthManager healthManager;

    //gravity
    float gravityStore;
    //decrease point
    public int pointPenaltyOnDeath;
    PlayerControl player;

    // Use this for initialization
    void Start()
    {
        lifeSystem = FindObjectOfType<LifeManager>();
        //find player in the scene & camera
        //initialise to use the player controller script
        player = FindObjectOfType<PlayerControl>();
        cameras = FindObjectOfType<CameraControl>();
        healthManager = FindObjectOfType<HealthManager>();
    }

    void Awake()
    {
        Advertisement.Initialize(gameID, true);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void respawnPlayer()
    {
        //initialize delay
        StartCoroutine("respawnPlayerCo");

    }
    //function for delay
    public IEnumerator respawnPlayerCo()
    {
        //gravity scale to position player when dead adn respawn
        //copy particles at player position and rotation for deathFX
        Instantiate(deathParticle, player.transform.position, player.transform.rotation);
        //kill player
        player.enabled = false;
        player.GetComponent<Renderer>().enabled = false;
       // cameras.isFollowing = false;
        gravityStore = player.GetComponent<Rigidbody2D>().gravityScale;
        player.GetComponent<Rigidbody2D>().gravityScale = 0f;
        //prevent player slide after death
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        //remove point
        ShowAd();
        ScoreManager.addPoints(-pointPenaltyOnDeath);
        Debug.Log("Player Respawn");
        //delay time for in the inspector
        yield return new WaitForSeconds(respawnDelay);
        player.GetComponent<Rigidbody2D>().gravityScale = gravityStore;
        //for respawn at checkpoint
        //change position of player from current to the checkpoint position
        player.transform.position = currentCheckpoint.transform.position;
        //show player
        player.enabled = true;
        player.GetComponent<Renderer>().enabled = true;
        healthManager.fullHealth();
        healthManager.isDead = false;
        //cameras.isFollowing = true;
        Instantiate(respawnParticle, currentCheckpoint.transform.position, currentCheckpoint.transform.rotation);
        //player.knockbackCount = 0;
    }
    public void ShowAd()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show("rewardedVideo", new ShowOptions() { resultCallback= HandleAdResult});
        }
    }
    private void HandleAdResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                lifeSystem.giveLife();
                break;
            case ShowResult.Skipped:
                Debug.Log("Player not watch");
                break;
            case ShowResult.Failed:
                Debug.Log("Player no internet?");
                break;
        }
    }

}
