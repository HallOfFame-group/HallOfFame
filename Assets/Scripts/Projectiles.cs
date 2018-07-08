using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Projectiles : MonoBehaviour
{
    public GameObject target;
    [SerializeField]
    private float secondsBeforeDestroy = .1f;
    [SerializeField]
    private bool canRotate = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (canRotate)
            transform.Rotate(0,0,15);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == target)
            StartCoroutine(DelayedDestroy());
    }

    private IEnumerator DelayedDestroy()
    {
        yield return new WaitForSeconds(secondsBeforeDestroy);
        Destroy(this.gameObject);
    }
}
