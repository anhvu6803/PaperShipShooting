using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollImage : MonoBehaviour
{
    [SerializeField] private Vector2 scrollSpeed;
    private Vector2 offset;
    private Material material;
    private void Awake()
    {
        material = GetComponent<Image>().material;
    }
    private void Start()
    {
        material.mainTextureOffset = new Vector2(0,0); 
    }
    void Update()
    {
        offset = scrollSpeed * Time.deltaTime;
        material.mainTextureOffset += offset;
    }
}
