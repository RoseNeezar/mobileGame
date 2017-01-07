using UnityEngine;
using System.Collections;

public class lifePickup : MonoBehaviour
{
    public GameObject lifeFX;
    public Transform lifeLocation;
    //sound fx
    public AudioSource lifeSoundFX;

   
    LifeManager lifeSystem;
	// Use this for initialization
	void Start ()
    {
       
        lifeSystem = FindObjectOfType<LifeManager>();
	}
	
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            lifeSystem.giveLife();
            lifeSoundFX.Play();
            Instantiate(lifeFX, lifeLocation.transform.position, lifeLocation.transform.rotation);
            Destroy(gameObject);
        }
    }
}
