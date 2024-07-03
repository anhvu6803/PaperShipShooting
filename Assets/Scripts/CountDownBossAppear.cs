using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CountDownBossAppear : MonoBehaviour
{
    [SerializeField] private Slider coutDownBossSlider;
    [SerializeField] private float countDownTime;
    [SerializeField] private GameObject dangerIcon;
    [SerializeField] private float maxXBound;
    [SerializeField] private float maxYBound;
    [SerializeField] private float minXBound;
    [SerializeField] private float minYBound;
    [SerializeField] private float delayTime;
    [SerializeField] private CameraShake cameraShake;
    [SerializeField] private CurtainManager curtainManager;
    private Vector2 previousPosition;
    public event Action<bool> onBossBattle;
    private void Start()
    {
        previousPosition = Vector2.zero;
        coutDownBossSlider.maxValue = countDownTime;
        coutDownBossSlider.value = coutDownBossSlider.maxValue;
    }
    private void Update()
    {
        coutDownBossSlider.value -= Time.deltaTime;
    }
    public void ResetCountDownBar()
    {
        coutDownBossSlider.value = coutDownBossSlider.maxValue;
    }
    public void ChangeBossScene()
    {
        if(coutDownBossSlider.value <= 0)
        {
            onBossBattle(true);
            StartCoroutine(DelayToChangeScene());
        }
    }
    private IEnumerator DelayToChangeScene()
    {
        while(delayTime > 0)
        {
            Transform instance = Instantiate(dangerIcon.transform, DangerIconSpawnPosition(previousPosition), Quaternion.identity, transform);
            instance.GetComponent<KeepInScene>().enabled = false;
            previousPosition = instance.position;
            delayTime -= Time.deltaTime;
            cameraShake.Play();
            yield return null;
        }
        StartCoroutine(curtainManager.CurtainDown());
        yield return new WaitForSeconds(.2f);
        DestroyAllDangerIcon();
        yield return new WaitForSeconds(.8f);
        StartCoroutine(curtainManager.CurtainUp());
    }
    private void DestroyAllDangerIcon()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
    private Vector2 DangerIconSpawnPosition(Vector2 previousPosition)
    {
        float x;
        float y;
        do
        {
            x = Random.Range(minXBound, maxXBound);
            y = Random.Range(minYBound, maxYBound);
        } while (x == previousPosition.x + 0.5f || x == previousPosition.x - 0.5f 
        || y == previousPosition.y + 0.5f || y == previousPosition.y - 0.5f);

        return new Vector2(x, y);
    }

}
