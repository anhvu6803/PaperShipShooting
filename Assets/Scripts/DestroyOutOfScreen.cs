using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfScreen : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet") || collision.CompareTag("Power"))
            Destroy(collision.gameObject);
    }
}
