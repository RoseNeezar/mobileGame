using UnityEngine;
using System.Collections;

public class movingPlateform : MonoBehaviour
{
    //the actual plate form
    public GameObject plateform;
    public float moveSpeed;
    public Transform currentPoint;
    //array for starting and ending points of the plateform
    public Transform[] points;
    //points start and end for the elements in the array
    public int pointSelection;
    bool canSword;
    public float swordCooldown;
    public float swordDurationMove;
    // Use this for initialization
    void Start ()
    {
        //start according to point selection(ex, element 0/1)
        currentPoint = points[pointSelection];
	}
	
	// Update is called once per frame
	void Update ()
    {
        //movetowards mean from from one place to another(every frame),start plateform position to current position(element 0/1) with speed
        plateform.transform.position = Vector3.MoveTowards(plateform.transform.position, currentPoint.position,Time.deltaTime*moveSpeed);

        if (plateform.transform.position == currentPoint.position )
        {
           
                StartCoroutine(swordmove());
            
            //loop the rest of the array
            if(pointSelection == points.Length)
            {
                //back to element 0 ie the starting position
                pointSelection = 0;
            }
            currentPoint = points[pointSelection];
        }
	}
    IEnumerator swordmove() //Coroutine with a single input of a float called boostDur, which we can feed a number when calling
    {

     

       
            pointSelection++;
        
        yield return new WaitForSeconds(swordCooldown); //Cooldown time for being able to boost again, if you'd like.
        StopCoroutine(swordmove());
    }
}
