using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollSprite : MonoBehaviour
{
    [SerializeField] private Vector2 scrollSpeed;
    private Vector2 offset;
    private Material material;
    private void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
    }
    private void Start()
    {
        material.mainTextureOffset = new Vector2(0, 0);
    }
    void Update()
    {
        offset = scrollSpeed * Time.deltaTime;
        material.mainTextureOffset += offset;
    }
}
