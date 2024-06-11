using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class SonicBoom : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform objectTransform;
    [SerializeField] private float sonicBoomExistTime;
    private Vector2 magnitudeBoom;
    private LevelManager levelManager;
    private bool isLoad;
    private Coroutine loadGameOver;
    [Header("Player")]
    [SerializeField] private bool isPlayer;
    [Header("Boss")]
    [SerializeField] private bool isBoss;
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
        if (isPlayer)
        {
            if (gameObject.activeSelf)
            {
                if (objectTransform != null)
                {
                    gameObject.transform.position = objectTransform.position;
                }
                IncreaseExplosion();
                isLoad = true;
            }

            LoadGameOverManager();
        }
        else if (isBoss)
        {
            IncreaseExplosion();
        }
    }
    public float GetSonicBoomExistTime()
    {
        return sonicBoomExistTime;
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
