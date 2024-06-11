using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CurtainManager : MonoBehaviour
{
    [SerializeField] private RectTransform curtainTransform;
    [SerializeField] private float speed;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject boss;
    [SerializeField] private GameObject enemySpawner;
    private void SetActiveGameObject(bool isActive)
    {
        if (player != null)
        {
            player.SetActive(isActive);
            player.GetComponent<Shooter>().enabled = isActive;
        }
        if (enemySpawner != null && !isActive || boss == null)
        {
            enemySpawner.SetActive(isActive);
            enemySpawner.GetComponent<EnemySpawner>().enabled = isActive;
        }
        if (boss != null)
        {
            boss.SetActive(isActive);
        }
    }
    public IEnumerator CurtainDown()
    {
        float delta = 1;
        while(curtainTransform.anchorMin.y > 0)
        {
            yield return null;
            delta -= speed * Time.deltaTime;
            curtainTransform.anchorMin = new Vector2(0, delta);
        }
        foreach (Transform t in enemySpawner.transform)
        {
            if (t.childCount > 0)
            {
                foreach (Transform child in t)
                {
                    Destroy(child.gameObject);
                }
            }
        }
        SetActiveGameObject(false);
    }
    public IEnumerator CurtainUp()
    {
        float delta = 0;
        while (curtainTransform.anchorMin.y < 1)
        {
            yield return null;
            delta += speed * Time.deltaTime;
            curtainTransform.anchorMin = new Vector2(0, delta);
        }
        SetActiveGameObject(true);
    }
}
