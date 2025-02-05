using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject bodyUI;
    [SerializeField] private GameObject settingUI;

    private CameraShake cameraShake;
    public void PauseGame()
    {
        cameraShake = FindObjectOfType<CameraShake>();

        if (cameraShake != null)
        { 
            cameraShake.Stop();
        }

        Time.timeScale = 0;
        LoadPauseUI(true);
    }
    private void LoadPauseUI(bool isActive)
    {
        pauseUI.SetActive(isActive);
    }
    public void ResumeGame()
    {
        if (pauseUI != null)
        {
            LoadPauseUI(false);
        }

        if(settingUI != null)
        {
            LoadSettingUI(false);
        }

        if (bodyUI != null)
        {
            if (!bodyUI.activeSelf)
            {
                cameraShake = FindObjectOfType<CameraShake>();

                if (cameraShake != null)
                {
                    cameraShake.ShakeDurationBack();
                }
            }
        }

        Time.timeScale = 1f;
    }
    public void SettingScene()
    {
        cameraShake = FindObjectOfType<CameraShake>();

        if (cameraShake != null)
        {        
            cameraShake.Stop();
        }

        Time.timeScale = 0;
        LoadSettingUI(true);
    }
    private void LoadSettingUI(bool isActive)
    {
        settingUI.SetActive(isActive);
    }
}
