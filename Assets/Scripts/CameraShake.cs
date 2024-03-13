using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float shakeDuration;
    [SerializeField] private float shakeMagnitude;
    private Vector3 initialPosition;
    private float initialShakeDuration;
    void Start()
    {
        initialPosition = transform.position;
        initialShakeDuration = shakeDuration;
    }
    public void ShakeDurationBack()
    {
        shakeDuration = initialShakeDuration;
    }
    public void Stop()
    {
        shakeDuration = 0;
        transform.position = initialPosition;
    }
    public void Play()
    {
        StartCoroutine(Shake());
    }
    private IEnumerator Shake()
    {
        float elapsedTime = 0;
        while(elapsedTime < shakeDuration)
        {
            transform.position = initialPosition + (Vector3)Random.insideUnitCircle * shakeMagnitude;
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.position = initialPosition;
    }
}
