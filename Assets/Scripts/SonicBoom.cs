using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class SonicBoom : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float sonicBoomExistTime;
    private Vector2 magnitudeBoom;
    private LevelManager levelManager;
    private bool isLoad;
    private Coroutine loadGameOver;
    private void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }
    private void Start()
    {
        magnitudeBoom = new Vector2(1, 1);
    }
    private void Update()
    {
        if (gameObject.activeSelf)
        {
            if(playerTransform != null)
            {
                gameObject.transform.position = playerTransform.position;
            }
            IncreaseExplosion();
            isLoad = true;
        }
        LoadGameOverManager();
    }
    private void LoadGameOverManager()
    {
        if (isLoad && loadGameOver == null)
        {
            loadGameOver = StartCoroutine(WaitForLoadGameOver());
        }
        else if (!isLoad && loadGameOver!= null)
        {
            StartCoroutine(WaitForLoadGameOver());
            loadGameOver = null;
        }
    }
    public void IncreaseExplosion()
    {
        magnitudeBoom += new Vector2(speed * Time.deltaTime, speed * Time.deltaTime);
        gameObject.transform.localScale = magnitudeBoom;
    }
    private IEnumerator WaitForLoadGameOver()
    {
        yield return new WaitForSeconds(sonicBoomExistTime);
        levelManager.LoadGameOver();

    }
    
}
