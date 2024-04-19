using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordFollow : MonoBehaviour
{
    [SerializeField] private float speedRotation;
    [SerializeField] private float speedFollow;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float waitingTime;
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private float angleY;
    private float sign;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (transform.position.x > 0)
        {
            angleY = 180;
            sign = 1;
        }
        else
        {
            sign = -1;
            angleY = 0;
        }
        moveDirection = (playerTransform.position - transform.position).normalized;
        Debug.Log(moveDirection);
        RotateToFacePlayer();
    }
    private void Update()
    {
        waitingTime -= Time.deltaTime;
        if(waitingTime <= 0)
        {
            rb.velocity = moveDirection * speedFollow * Time.deltaTime;
        }
    }
    private void RotateToFacePlayer()
    {
        float angle = Mathf.Atan(moveDirection.y / moveDirection.x) * Mathf.Rad2Deg;
        Debug.Log(angle);
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, angleY, 90 - Mathf.Abs(angle)));
        transform.rotation = targetRotation;
    } 
}
