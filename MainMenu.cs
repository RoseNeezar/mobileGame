using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{

    public string startLevel;

    public string levelSelect;

    //public int playerLives;

   // public int playerHealth;

  //  public string level1Tag;
    //public string level2Tag;
    //public string level3Tag;

    public void NewGame()
    {

        //go to player set the playerlives value

      //  PlayerPrefs.SetInt("PlayerCurrentHealth", playerHealth);
        //PlayerPrefs.SetInt("PlayerCurrentLives", playerLives);
        PlayerPrefs.SetInt("CurrentPlayerScore", 0);
       // PlayerPrefs.SetInt("PlayerMaxHealth", playerHealth);
        //PlayerPrefs.SetInt(level1Tag, 1);
       // PlayerPrefs.SetInt(level2Tag, 0);
       // PlayerPrefs.SetInt(level3Tag, 0);
        //PlayerPrefs.SetInt("PlayerLevelSelectPosition", 0);
        Application.LoadLevel(startLevel);

    }


    public void LevelSelector()
    {
       // PlayerPrefs.SetInt("PlayerCurrentHealth", playerHealth);
       // PlayerPrefs.SetInt("PlayerCurrentLives", playerLives);
        PlayerPrefs.SetInt("CurrentPlayerScore", 0);
      //  PlayerPrefs.SetInt("PlayerMaxHealth", playerHealth);
       // PlayerPrefs.SetInt(level1Tag, 1);
       // PlayerPrefs.SetInt(level2Tag, 0);
        //PlayerPrefs.SetInt(level3Tag, 0);
        //if playerpref has no value
       /* if (!PlayerPrefs.HasKey("PlayerLevelSelectPosition"))
        {
            PlayerPrefs.SetInt("PlayerLevelSelectPosition", 0);
        }*/
        Application.LoadLevel(levelSelect);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void Reset()
    {
        PlayerPrefs.DeleteAll();
    }
}
