using UnityEngine;
using System.Collections;

public class DestroyOveTime : MonoBehaviour {

    public float lifetime;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //start at amount of lifetime
        lifetime -= Time.deltaTime;
        //when lifetime hit 0 poof
        if (lifetime < 0)
        {
            Destroy(gameObject);
        }
    }
}
