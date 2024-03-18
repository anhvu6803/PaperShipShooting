using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OverScene : MonoBehaviour
{
    private Camera mainCamera;
    private Vector2 minBound;
    private Vector2 maxBound;
    private void Start()
    {
        InitBound();
    }
    private void Update()
    {
        if(transform.position.y > maxBound.y || transform.position.y < minBound.y)
        {
            Destroy(gameObject);
        }
    }
    private void InitBound()
    {
        mainCamera = Camera.main;
        minBound = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxBound = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
    }
}
