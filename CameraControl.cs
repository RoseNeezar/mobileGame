using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {
    //public bool isFollowing;
    //BoxCollider2D cameraBox;
    //Transform player;

    Vector2 velocity;
    public float smoothaTimeY;
    public float smoothTiimeX;
   public GameObject player;

    public bool bounds;
    public Vector3 minCameraPos;
    public Vector3 maxCameraPos;
   
    //private Vector3 velocity = Vector3.zero;
    
   

    public float xoffset;
    public float yoffset;
    // Use this for initialization
    void Start ()
    {
        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
      //  cameraBox = GetComponent<BoxCollider2D>();
                   player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update ()
    {
       
        //FollowPlayer();
        
        float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x + xoffset, ref velocity.x, smoothTiimeX);
        float posY = Mathf.SmoothDamp(transform.position.y , player.transform.position.y + yoffset, ref velocity.y, smoothaTimeY);
        //transform.position = new Vector3(player.transform.position.x , player.transform.position.y , transform.position.z);
        transform.position = new Vector3(posX, posY, transform.position.z);

        if(bounds)
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x + xoffset, minCameraPos.x, maxCameraPos.x),
                                    Mathf.Clamp(transform.position.y + yoffset, minCameraPos.y, maxCameraPos.y),
                                    Mathf.Clamp(transform.position.z, minCameraPos.z, maxCameraPos.z));
        }

    }/*
    void FollowPlayer()
    {
        if(GameObject.Find("Boundary"))
        {
            transform.position = new Vector3(Mathf.Clamp(player.position.x, GameObject.Find("Boundary").GetComponent<BoxCollider2D>().bounds.min.x + cameraBox.size.x / 2, GameObject.Find("Boundary").GetComponent<BoxCollider2D>().bounds.max.x - cameraBox.size.x / 2),
                                             Mathf.Clamp(player.position.y, GameObject.Find("Boundary").GetComponent<BoxCollider2D>().bounds.min.y + cameraBox.size.y / 2, GameObject.Find("Boundary").GetComponent<BoxCollider2D>().bounds.max.y - cameraBox.size.y / 2),
                                             transform.position.z);
        }
    }
    void aspect()
    {
        if(Camera.main.aspect>=(1.6f) && Camera.main.aspect<1.7f)
        {
            cameraBox.size = new Vector3(23, 14.3f);
        }
    }
   
    */
}
