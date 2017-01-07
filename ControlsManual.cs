using UnityEngine;
using System.Collections;

public class ControlsManual : MonoBehaviour {

    public GameObject Manual;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == ("Player"))
        {
            Manual.gameObject.SetActive(true);

        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == ("Player"))
        {
            Manual.gameObject.SetActive(false);

        }
    }
}
