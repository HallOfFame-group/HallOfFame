using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotionEffect : MonoBehaviour {

    [SerializeField]
    private float newTimeScale = .25f;
    [SerializeField]
    private float duration = .1f;

    private float oriTimeScale;


	// Use this for initialization
	void Start () {
        oriTimeScale = Time.timeScale;
	}

    public IEnumerator SlowMotion()
    {
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            Time.timeScale = newTimeScale;

            elapsed += Time.deltaTime;

            yield return null;
        }

        Time.timeScale = oriTimeScale;
    }
}
