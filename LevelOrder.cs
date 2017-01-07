using UnityEngine;
using System.Collections;

public class LevelOrder : MonoBehaviour
{
    //know player location
    public bool playerInZone;
    public string levelToLoad;
    //know what level were on
    public string levelTag;
    

	// Use this for initialization
	void Start ()
    {
        playerInZone = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetKeyDown(KeyCode.C) && playerInZone)
        {
            //application will change the scene, loadlevelAsync - to play loading screen 
            //Application.LoadLevel(levelToLoad);
            LoadLevel();
        }   	
	}
    public void LoadLevel()
    {
        //1 leveltag means unlock
        PlayerPrefs.SetInt(levelTag, 1);
        Application.LoadLevel(levelToLoad);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name =="Player")
        {
            playerInZone = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            playerInZone = false;
        }
    }
}
