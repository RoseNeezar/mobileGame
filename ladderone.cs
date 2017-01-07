using UnityEngine;
using System.Collections;

public class ladderone : MonoBehaviour
{
    PlayerControl player;
	// Use this for initialization
	void Start ()
    {
        player = FindObjectOfType<PlayerControl>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}/*
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == "Player")
        {
            player.onLadder = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            player.onLadder = false;
        }
    }*/
}
