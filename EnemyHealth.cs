using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour {

    public int enemyHealth, maxHealth;
    private Image healthBars;

    public GameObject CBTprefab;

    public Transform itemDrop;
    public GameObject drops;
    public GameObject deathFX;
    public int pointsOnDeath;
  
    public AudioSource deathSound;
  
    public int Health { get { return enemyHealth; } }

    void Start()
    {
        enemyHealth = maxHealth;
        healthBars = transform.FindChild("EnemyCanvas").FindChild("HealthBG").FindChild("Health").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
       
        if (enemyHealth <= 0)
        {
           
            Instantiate(deathFX, transform.position, transform.rotation);
            ScoreManager.addPoints(pointsOnDeath);
            Destroy(gameObject);
            if(drops)
            {
                Instantiate(drops, itemDrop.transform.position, itemDrop.transform.rotation);
            }
        }
    }
    public void giveDamage(int damageToGive)
    {
        // gameObject.GetComponent<Animation>().Play("EnemyHurt");
        //audios.Play();
        //ScoreManager.addPoints(-pointsOnDeath);
        enemyHealth -= damageToGive;
        healthBars.fillAmount = (float)enemyHealth / (float)maxHealth;
        InitCBT(damageToGive.ToString());
        //gameObject.GetComponent<Animation>().Play("Hurt")
    }

    void InitCBT(string text)
    {
        GameObject temp = Instantiate(CBTprefab) as GameObject;
        RectTransform tempRect = temp.GetComponent<RectTransform>();
        temp.transform.SetParent(transform.FindChild("EnemyCanvas"));
        tempRect.transform.localPosition = CBTprefab.transform.localPosition;
        tempRect.transform.localScale = CBTprefab.transform.localScale;
        tempRect.transform.localRotation = CBTprefab.transform.localRotation;
        temp.GetComponent<Text>().text = text;
        Destroy(temp.gameObject, .75f);
    }

}
