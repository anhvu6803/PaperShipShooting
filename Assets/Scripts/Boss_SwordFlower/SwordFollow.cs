using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordFollow : MonoBehaviour
{
    [SerializeField] private float speedFollow;
    [SerializeField] private float waitingTime;
    [SerializeField] private AnimationClip swordChangeClip;
    private Rigidbody2D rb;
   
    private void Start()
    {
        waitingTime += swordChangeClip.length;
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        waitingTime -= Time.deltaTime;
        if(waitingTime <= 0)
        {
            rb.velocity =  Vector2.down * speedFollow * Time.deltaTime;
        }
    } 
}
