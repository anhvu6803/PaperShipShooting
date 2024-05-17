using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepInScene : MonoBehaviour
{
    [SerializeField] private float paddingTop;
    [SerializeField] private float paddingBottom;
    [SerializeField] private float paddingLeft;
    [SerializeField] private float paddingRight;
    private Camera mainCamera;
    private Vector2 minBound;
    private Vector2 maxBound;
    private void Start()
    {
        InitBound();
    }
    private void Update()
    {
        KeepingInScreen();
    }
    private void InitBound()
    {
        mainCamera = Camera.main;
        minBound = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxBound = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
    }
    private void KeepingInScreen()
    {
        Vector2 newPostion;
        newPostion.x = Mathf.Clamp(transform.position.x, minBound.x + paddingLeft, maxBound.x - paddingRight);
        newPostion.y = Mathf.Clamp(transform.position.y, minBound.y + paddingBottom, maxBound.y - paddingTop);
        transform.position = newPostion;
    }
}
