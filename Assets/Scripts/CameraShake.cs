using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

    Vector3 originalPos;

    public Transform m_camera;
    [SerializeField]
    private float duration = .15f;
    [SerializeField]
    private float magnitude = .15f;

    private void Start()
    {
        if (!m_camera)
            m_camera = Camera.main.transform;
        originalPos = m_camera.localPosition;
    }

    public IEnumerator Shake()
    {
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            m_camera.localPosition += new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        m_camera.localPosition = originalPos;
    }
}
