using UnityEngine;
using System.Collections;

public class Parallax : MonoBehaviour
{
    //array based on how many background we have
    public Transform[] backgrounds;
    float[] parallaxScales;
    public float smoothing;

    Transform cam;
    Vector3 previousCamPos;

	// Use this for initialization
	void Start ()
    {
        //find camera position
        cam = Camera.main.transform;
        //fist pos where cam start
        previousCamPos = cam.position;
        parallaxScales = new float[backgrounds.Length];
        //size of background
        for(int i = 0;i < backgrounds.Length; i++)
        {
            //depend on z value only, be at the back, with array numbering
            parallaxScales[i] = backgrounds[i].position.z * -1;
        }

	}
	
	// Update is called once per frame
    //dont want to mix with  update function in the main camera
	void LateUpdate ()
    {
        //move parallax
	    for(int i=0; i< backgrounds.Length;i++)
        {
            //amount of movement
            float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];
            // get currenPOs and add on as the layer moves
            float backgroundTargetPosX = backgrounds[i].position.x - parallax;
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);
            //move background smoothly from towards and speed been variable taking account
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }
        previousCamPos = cam.position;
    }
}
