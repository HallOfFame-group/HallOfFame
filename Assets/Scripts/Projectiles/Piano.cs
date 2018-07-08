using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Piano : MonoBehaviour {

    // Use this for initialization
    private void Start()
    {
        transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -1000));
    }

    // Update is called once per frame
    void Update () {
		
	}
}
