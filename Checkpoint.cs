using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {

    public LevelManager levelManager;

    // Use this for initialization
    void Start()
    {
        //find object in level manage in scene
        //initialize to use the levelmanager script
        levelManager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //change player to checkpoint

            levelManager.currentCheckpoint = gameObject;
            Debug.Log("Activated Checkpoint" + transform.position);
        }
    }

}
