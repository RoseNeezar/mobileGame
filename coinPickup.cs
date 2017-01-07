using UnityEngine;
using System.Collections;

public class coinPickup : MonoBehaviour {


    //coin value
    public int pointsToAdd;
    public GameObject coinFX;
    public Transform coinLocation;
    //sound fx
    public AudioSource coinSoundFX;
    

    void OnTriggerEnter2D(Collider2D other)
    {
        //if not player that it can't get the coin// only player can get coin
        if (other.GetComponent<PlayerControl>() == null)
            return;

        coinSoundFX.Play();
        ScoreManager.addPoints(pointsToAdd);
        Instantiate(coinFX, coinLocation.transform.position, coinLocation.transform.rotation);
        Destroy(gameObject);

    }

}
