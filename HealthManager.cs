using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    
    public int maxPlayerHealth;
    public static int playerHealth;
    public bool isDead;
    
    LifeManager lifeSystem;
    static AudioSource aud;
    LevelManager levelManager;

    //TimeManager time;

    //Text text;
    //health slider
    public Slider healthBar;

    // Use this for initialization
    void Start()
    {
      //  aud = GetComponent<AudioSource>();
        healthBar = GetComponent<Slider>();
        aud = GetComponent<AudioSource>();
        //text = GetComponent<Text>();
        playerHealth = maxPlayerHealth;
       // playerHealth = PlayerPrefs.GetInt("PlayerCurrentHealth");
     //   playerHealth = PlayerPrefs.GetInt("PlayerMaxHealth");
        levelManager = FindObjectOfType<LevelManager>();
        lifeSystem = FindObjectOfType<LifeManager>();
        //time = FindObjectOfType<TimeManager>();
        isDead = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth <= 0 && !isDead)
        {
            playerHealth = 0;
            levelManager.respawnPlayer();
            isDead = true;
            lifeSystem.takeLife();          
        }
        if (playerHealth > maxPlayerHealth)
            playerHealth = maxPlayerHealth;
        healthBar.value = playerHealth;
    } 
    public static void hurtPlayer(int damageGive)
    {
       
        playerHealth -= damageGive;
        aud.Play();
        //PlayerPrefs.SetInt("PlayerCurrentHealth", playerHealth);
    }
    public void fullHealth()
    {
        playerHealth = maxPlayerHealth;
     //   playerHealth = PlayerPrefs.GetInt("PlayerMaxHealth");
       //PlayerPrefs.SetInt("PlayerCurrentHealth", playerHealth);
    }
   
    public void killPlayer()
    {
        playerHealth = 0;
    }

}
