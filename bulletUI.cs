using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class bulletUI : MonoBehaviour {

    public Sprite[] bulletSprite;
    public Image bulletsUI;
    private PlayerControl player;
	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        bulletsUI.sprite = bulletSprite[player.CurrentBullet];
	}
}
