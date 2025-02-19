using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField][Range(0f, 1f)] private float effectVolume;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private MusicManagerData musicManagerData;

    [Header("Shooting")]
    [SerializeField] private AudioClip shootingClip;
    [SerializeField] private AudioClip enemyShootingClip;

    [Header("Explosion")]
    [SerializeField] private AudioClip explosionClip;

    [Header("UseSkill")]
    [SerializeField] private AudioClip ultimateClip;
    [SerializeField] private AudioClip shieldClip;
    [SerializeField] private AudioClip breakShieldClip;

    [Header("Impact")]
    [SerializeField] private AudioClip impactClip;
    [SerializeField] private AudioClip impactShieldClip;

    private static AudioPlayer instance;
    private void Awake()
    {
        ManageAudioSingleton();
    }
    private void Start()
    {
        audioSource.clip = musicManagerData.GetOpening();
    }
    private void OnEnable()
    {
        Debug.Log("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "MainMenu")
        {
            audioSource.clip = musicManagerData.GetOpening();
        }
        else if(scene.name == "Game")
        {
            audioSource.clip = musicManagerData.GetCombat();
        }
        else if(scene.name == "GameOver")
        {
            audioSource.clip= musicManagerData.GetClosing();
        }
        audioSource.Play();
    }
    private void OnDisable()
    {
        Debug.Log("OnEnable called");
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    public void SetAudioBoss()
    {
        audioSource.clip = musicManagerData.GetBoss();
        audioSource.Play();
    }
    public void SetAudioCombat()
    {
        audioSource.clip = musicManagerData.GetCombat();
        audioSource.Play();
    }
    private void ManageAudioSingleton()
    {
        if(instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public void SetEffectVolume(float volume)
    {
        effectVolume = volume;
    }
    public void SetMusicVolume(float volume)
    {
        audioSource.volume = volume;
    }
    public void MuteMusic()
    {
        audioSource.mute = true;
    }
    public void PlayMusic() 
    { 
        audioSource.mute = false;
    }
    public void PlayShootingClip()
    {
        if(shootingClip != null)
        {
            AudioSource.PlayClipAtPoint(shootingClip, Camera.main.transform.position, effectVolume * 0.5f);
        }
    }
    public void PlayEnemyShootingClip()
    {
        if (enemyShootingClip != null)
        {
            AudioSource.PlayClipAtPoint(enemyShootingClip, Camera.main.transform.position, effectVolume * 0.5f);
        }
    }
    public void PlayExplosionClip()
    {
        if(explosionClip != null)
        {
            AudioSource.PlayClipAtPoint(explosionClip, Camera.main.transform.position, effectVolume);
        }
    }
    public void PlayUltimateClip()
    {
        if (ultimateClip != null)
        {
            AudioSource.PlayClipAtPoint(ultimateClip, Camera.main.transform.position, effectVolume * 0.5f);
        }
    }
    public void PlayShieldClip()
    {
        if (shieldClip != null)
        {
            AudioSource.PlayClipAtPoint(shieldClip, Camera.main.transform.position, effectVolume);
        }
    }
    public void PlayBreakShieldClip()
    {
        if (breakShieldClip != null)
        {
            AudioSource.PlayClipAtPoint(breakShieldClip, Camera.main.transform.position, effectVolume);
        }
    }
    public void PlayImpactClip()
    {
        if (impactClip != null)
        {
            AudioSource.PlayClipAtPoint(impactClip, Camera.main.transform.position, effectVolume);
        }
    }

    public void PlayImpactShieldClip()
    {
        if (impactShieldClip != null)
        {
            AudioSource.PlayClipAtPoint(impactShieldClip, Camera.main.transform.position, effectVolume);
        }
    }
}
