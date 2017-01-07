using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
   

    BoxCollider2D cameraBox;
    Transform player;
	// Use this for initialization
	void Start ()
    {
        cameraBox = GetComponent<BoxCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        FollowPlayer();
        AspectRatioBoxChange();
	}

    void AspectRatioBoxChange()
    {
       
        cameraBox.size = new Vector3(22f, 13f);
            /*
        if(Camera.main.aspect >= (1.6f) && Camera.main.aspect < 1.7f)
        {
            cameraBox.size = new Vector2(23,14.3f);
        }
        if (Camera.main.aspect >= (1.7f) && Camera.main.aspect < 1.8f)
        {
            cameraBox.size = new Vector2(25.47f, 14.3f);
        }
        if (Camera.main.aspect >= (1.25f) && Camera.main.aspect < 1.3f)
        {
            cameraBox.size = new Vector2(18f, 14.3f);
        }
        if (Camera.main.aspect >= (1.3f) && Camera.main.aspect < 1.4f)
        {
            cameraBox.size = new Vector2(23, 14.3f);
        }
        if (Camera.main.aspect >= (1.5f) && Camera.main.aspect < 1.6f)
        {
            cameraBox.size = new Vector2(21.6f, 14.3f);
        }*/
    }

    void FollowPlayer ()
    {
        if(GameObject.Find("Boundary"))
        {
            transform.position = new Vector3(Mathf.Clamp(player.position.x , GameObject.Find("Boundary").GetComponent<BoxCollider2D>().bounds.min.x + cameraBox.size.x / 2, GameObject.Find("Boundary").GetComponent<BoxCollider2D>().bounds.max.x - cameraBox.size.x / 2),
                                             Mathf.Clamp(player.position.y , GameObject.Find("Boundary").GetComponent<BoxCollider2D>().bounds.min.y + cameraBox.size.y / 2, GameObject.Find("Boundary").GetComponent<BoxCollider2D>().bounds.max.y - cameraBox.size.y / 2),
                                             transform.position.z);

        }
    }
}
