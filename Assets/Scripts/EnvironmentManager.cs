using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> cloudObjects;
    [SerializeField] private List<Vector2> xyDirections;
    [SerializeField] private float speed;
    [SerializeField] private float magnitude;

    private Camera mainCamera;
    [SerializeField] private Vector2 maxBound;
    [SerializeField] private Vector2 minBound;
    private void Start()
    {
        InitBound();
        StartCoroutine(RespawnCloud());
    }
    private void FixedUpdate()
    {
        if (cloudObjects[0].activeSelf)
        {
            CloudMovement(cloudObjects[0], xyDirections[0]);
        }
        if (cloudObjects[1].activeSelf)
        {
            CloudMovement(cloudObjects[1], xyDirections[1]);
        }
        if (cloudObjects[2].activeSelf)
        {
            CloudMovement(cloudObjects[2], xyDirections[2]);
        }
    }
    private void InitBound()
    {
        mainCamera = Camera.main;
        minBound = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxBound = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
    }
    private Vector2 RandomPosition()
    {
        return new Vector2(maxBound.x + magnitude, Random.Range(minBound.y + magnitude, maxBound.y));
    }
    private Vector2 MoveDirection(float horizontal, float vertical)
    {
        return new Vector2(horizontal, vertical);
    }
    private void CloudMovement(GameObject cloudObject, Vector2 xyDirection)
    {
        Rigidbody2D rb = cloudObject.GetComponent<Rigidbody2D>();
        rb.velocity = MoveDirection(xyDirection.x, xyDirection.y) * speed * Time.deltaTime;
    }
    private IEnumerator RespawnCloud()
    {
        while (true)
        {
            foreach (GameObject gameObject in cloudObjects)
            {
                gameObject.transform.position = RandomPosition();
                gameObject.SetActive(true);
                yield return new WaitForSeconds(5f);
            }
            yield return new WaitForSeconds(1f);
        }

    }
}
