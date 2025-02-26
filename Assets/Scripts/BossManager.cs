using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class BossManager : MonoBehaviour
{
    [SerializeField] private GameObject bossObject;
    [SerializeField] private float delayTime;
    [SerializeField] private GameObject sonicBoomBoss;
    [SerializeField] private CurtainManager curtainManager;
    [SerializeField] private GameObject bossCountDownSlider;
    [SerializeField] private CountDownBossAppear bossAppear;

    private AudioPlayer audioPlayer;
    private bool isBossDie;
    private Coroutine bossDieCoroutine;
    private void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }
    private void OnEnable()
    {
        bossAppear.onBossBattle += IsBossBattle;
    }
    private void OnDisable()
    {
        bossAppear.onBossBattle -= IsBossBattle;
    }
    private void Update()
    {
        if(bossObject == null)
        {
            isBossDie = true;
        }
        else
        {
            isBossDie = false;
        }
        ManageDelayToLoadGame();
    }
    public void IsBossBattle(bool isActive)
    {
        bossCountDownSlider.SetActive(!isActive);
        audioPlayer.SetAudioBoss();
    }
    private void ManageDelayToLoadGame()
    {
        if(isBossDie && bossDieCoroutine == null)
        {
            bossDieCoroutine = StartCoroutine(DelayToLoadGame());
        }
        else if(!isBossDie && bossDieCoroutine != null)
        {
            StopCoroutine(bossDieCoroutine);
            bossDieCoroutine = null;
        }
    }
    private IEnumerator DelayToLoadGame()
    {
        yield return new WaitForSeconds(sonicBoomBoss.GetComponent<SonicBoom>().GetSonicBoomExistTime());
        Destroy(sonicBoomBoss);
        while (delayTime > 0)
        {
            GeneratePower power = FindObjectOfType<GeneratePower>();
            Vector2 randomMagnitude = new Vector2(Random.Range(-1, 1), 0);
            power.PowerGenerate((Vector2)gameObject.transform.position + randomMagnitude);
            yield return new WaitForSeconds(0.2f);
            delayTime -= Time.deltaTime;
        }
        StartCoroutine(curtainManager.CurtainDown());
        yield return new WaitForSeconds(1f);
        StartCoroutine(curtainManager.CurtainUp());
        audioPlayer.SetAudioCombat();

    }

}
