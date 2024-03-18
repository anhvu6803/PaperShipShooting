using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearDanger: MonoBehaviour
{
    [SerializeField] private SpriteRenderer dangerSprite;
    [SerializeField] private float magitudeChange;
    private Color startColor;
    private Color endColor;
    private Camera mainCamera;
    private void Start()
    {
        startColor = dangerSprite.color;
        startColor.a = 0.5f;
        endColor = dangerSprite.color;
    }
    private void Update()
    {
        DangerEffect();
    }
    private void DangerEffect()
    {
        Color changeColor = Color.Lerp(startColor, endColor, Mathf.PingPong(Time.time * magitudeChange, 1f));
        dangerSprite.color = changeColor;
    }
}
