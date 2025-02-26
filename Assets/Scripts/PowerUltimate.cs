using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class PowerUltimate : MonoBehaviour
{
    [SerializeField] private List<GameObject> ultimatePrefabs;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float paddingLeft;
    [SerializeField] private float paddingRight;
    [SerializeField] private float timeBetweenSpawn;
    [SerializeField] private float speed;
    [SerializeField] private float destroyTime;
    private StorePower storePower;
    private AudioPlayer audioPlayer;
    private void Awake()
    {
        storePower = FindObjectOfType<StorePower>();  
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }
    public void UseUltimate()
    {
        audioPlayer.PlayUltimateClip();
        StartCoroutine(SpawnUltimate());
        storePower.DecreaseUltimateCount();
    }
    private IEnumerator SpawnUltimate()
    {
        foreach(GameObject prefab in ultimatePrefabs)
        {
            GameObject instance = Instantiate(prefab, RandomPosition(), Quaternion.identity);
            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>(); 
            if(rb != null)
            {
                rb.velocity = transform.up * speed;
            }
            audioPlayer.PlayShootingClip();
            Destroy(instance, destroyTime);
            yield return new WaitForSeconds(timeBetweenSpawn);
        }
    }
    private Vector3 RandomPosition()
    {
        return new Vector3(Random.Range(playerTransform.position.x - paddingLeft, playerTransform.position.x + paddingRight), playerTransform.position.y, 0);
    }
}
