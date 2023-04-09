using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceWobble : MonoBehaviour
{
    public float shakeDuration = 0.5f;
    public float shakeMagnitude = 0.1f;

    private Vector3 initialPosition;
    public bool isSad = false;

    private void Awake()
    {
        initialPosition = transform.localPosition;
    }

    public void SetSad(bool isSad)
    {
        this.isSad = isSad;
    }

    private void Update()
    {
        if (isSad)
        {
            Shake();
        }
    }

    public void Shake()
    {
        StartCoroutine(ShakeCoroutine());
    }

    private IEnumerator ShakeCoroutine()
    {
        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            float x = initialPosition.x + Random.Range(-1f, 1f) * shakeMagnitude;
            float y = initialPosition.y + Random.Range(-1f, 1f) * shakeMagnitude;

            transform.localPosition = new Vector3(x, y, initialPosition.z);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = initialPosition;
    }
}