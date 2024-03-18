using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private Camera mainCamera;
    void Start()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        ProcessInput();
    }
    private void FixedUpdate()
    {
        if (moveDirection == Vector2.zero) return;
        rb.velocity = moveDirection * speed * Time.deltaTime;
    }
    public void ModifySpeed(int bonusSpeed)
    {
        speed += bonusSpeed;
    }
    private void ProcessInput()
    {
        Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
        Vector2 worldPosition = mainCamera.ScreenToWorldPoint(touchPosition);
        if (Touchscreen.current.primaryTouch.press.IsPressed() && !CheckSamePosition(worldPosition, transform.position))
        {
            moveDirection = worldPosition - (Vector2)transform.position;
            moveDirection.Normalize();
        }
        else
        {
            moveDirection = Vector2.zero;
            rb.velocity = Vector2.zero;
            return;
        }
    }
    private bool CheckSamePosition(Vector2 source, Vector2 target)
    {
        return Mathf.Abs(source.x - target.x) < 0.1f && Mathf.Abs(source.y - target.y) < 0.1f;
    }
}
