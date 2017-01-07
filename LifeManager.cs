using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{
    public int startingLife;
    int lifeCounter;
    LevelManager respawning;
    Text text;
    public GameObject gameOverScreen;
    public GameObject controlsUI;
   // public PlayerControl player;
   // public GameObject player;
    public string mainMenu;
    public float waitAfterGameOver;


	// Use this for initialization
	void Start ()
    {
        text = GetComponent<Text>();
       // lifeCounter = PlayerPrefs.GetInt("PlayerCurrentLives");
        lifeCounter = startingLife;
      
       
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(lifeCounter<0)
        {
            gameOverScreen.SetActive(true);
            controlsUI.SetActive(false);
          //  player.gameObject.SetActive(false);
            //Time.timeScale = 0f;
        }
       
      
        text.text = "x" + lifeCounter;
        //activeself means the gameoverscreen is playing
        
        if(gameOverScreen.activeSelf)
        {
            // for loop to finish is the amount of time
            waitAfterGameOver -= Time.deltaTime;
        }
        if(waitAfterGameOver <0)
        {
            Application.LoadLevel(mainMenu);
        }
	}
    public void giveLife()
    {
        lifeCounter++;
      //  PlayerPrefs.SetInt("PlayerCurrentLives", lifeCounter);
    }
    public void takeLife()
    {
        lifeCounter--;
   //     PlayerPrefs.SetInt("PlayerCurrentLives", lifeCounter);
    }
}
