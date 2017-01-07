using UnityEngine;
using System.Collections;

public class FireTurret : MonoBehaviour {

    public bool canStun = true;
    public float stunDuration;
    public float stunCooldown;
    public TurretAI turret;

    public bool isLeft = false;
	// Use this for initialization
	void Awake ()
    {
        turret = gameObject.GetComponentInParent<TurretAI>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.transform.tag == "Player")
        {
            if (isLeft)
            {
                turret.Fire(false);
            }
            else
            {
                turret.Fire(true);
            }
        }
    }
    

}
