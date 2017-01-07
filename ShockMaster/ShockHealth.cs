using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ShockHealth : MonoBehaviour {

    public int enemyHealth, maxHealth;
    public bool isDead;
    
    public Slider healthBar;

    void Start()
    {
        healthBar = FindObjectOfType<Slider>();
        enemyHealth = maxHealth;
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHealth <= 0) 
        {
            enemyHealth = 0;
            Destroy((gameObject));
            healthBar.gameObject.SetActive(false);
            isDead = true;
        }
        if (enemyHealth > maxHealth)
            enemyHealth = maxHealth;
     //   healthBar.value = enemyHealth;
    }
    public void giveDamage(int damageToGive)
    {
        enemyHealth -= damageToGive;
        healthBar.value = enemyHealth;
    }

}
