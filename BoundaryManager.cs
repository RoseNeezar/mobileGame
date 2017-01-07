using UnityEngine;
using System.Collections;

public class BoundaryManager : MonoBehaviour {

    BoxCollider2D managerBox;
    Transform player;
    public GameObject boundary;
	// Use this for initialization
	void Start ()
    {
        managerBox = GetComponent<BoxCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        ManageBoundary();
	}
    void ManageBoundary()
    {
        if(managerBox.bounds.min.x < player.position.x && player.position.x < managerBox.bounds.max.x &&
           managerBox.bounds.min.y < player.position.y && player.position.y < managerBox.bounds.max.y)
        {
            boundary.SetActive(true);
        }
        else
        {
            boundary.SetActive(false);
        }
    }
}
