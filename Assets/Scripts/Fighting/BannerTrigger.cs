using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BannerTrigger : MonoBehaviour {

    public Animator anim;
    public GameObject banner;
	// Use this for initialization
	void Start () {
        banner.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Q))
        {
            banner.SetActive(true);
            anim.Play("Beethoven");
            
        }
	}
}
