using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class Skye_TimeLeft : MonoBehaviour {

	Text text;
	public static float timeLeft = 1f;
	void Start () {
		text = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		timeLeft -= Time.deltaTime;
		if (timeLeft < 0)
			timeLeft = 0;
		text.text = "Time Left : " + Mathf.Round (timeLeft);
	}
}
