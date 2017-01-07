using UnityEngine;
using System.Collections;

public class HealthPickup : MonoBehaviour
{
    //coin value
    public int healthToGive;
    public GameObject coinFX;
    public Transform HeartLocation;
    //sound fx
    public AudioSource HealthSoundFX;


    void OnTriggerEnter2D(Collider2D other)
    {
        //if not player that it can't get the coin// only player can get coin
        if (other.GetComponent<PlayerControl>() == null)
            return;

        HealthSoundFX.Play();
        HealthManager.hurtPlayer(-healthToGive);
        Instantiate(coinFX, HeartLocation.transform.position, HeartLocation.transform.rotation);
        Destroy(gameObject);

    }

}
