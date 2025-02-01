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
        cameraShake.Stop();
        Time.timeScale = 0;
        LoadPauseUI(true);
    }
    private void LoadPauseUI(bool isActive)
    {
        pauseUI.SetActive(isActive);
    }
    public void ResumeGame()
    {
        LoadPauseUI(false);
        LoadSettingUI(false);
        if (!bodyUI.activeSelf)
        {
            cameraShake = FindObjectOfType<CameraShake>();
            Time.timeScale = 1f;
            cameraShake.ShakeDurationBack();
        }
    }
    public void SettingScene()
    {
        cameraShake = FindObjectOfType<CameraShake>();
        cameraShake.Stop();
        Time.timeScale = 0;
        LoadSettingUI(true);
    }
    private void LoadSettingUI(bool isActive)
    {
        settingUI.SetActive(isActive);
    }
}
