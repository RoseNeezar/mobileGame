using UnityEngine;
using System.Collections;

public class HurtPlayerOnContact : MonoBehaviour {
    public int pointsOnDeath;
    // public AudioSource audioImpact;
    public int damageToGive;
    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" )
        {
            //get directly to player with no other script
         //  audioImpact.Play();
            HealthManager.hurtPlayer(damageToGive);
            ScoreManager.addPoints(-pointsOnDeath);
            //effect the player script
            var player = other.GetComponent<PlayerControl>();
            //same so can decrease the lengh of  knockback to zero
            player.knockbackCount = player.knockbackLength;
         //   audioImpact.Play();
            player.gameObject.GetComponent<Animation>().Play("Player_hurt");
         
            //if the other is less than enemy of x position thus other is on the left
            if (other.transform.position.x < transform.position.x)
             player.knockfromRight = true;
            else
                player.knockfromRight = false;
        }
    }

}
