using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject bodyUI;
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
        if (!bodyUI.activeSelf)
        {
            cameraShake = FindObjectOfType<CameraShake>();
            Time.timeScale = 1f;
            cameraShake.ShakeDurationBack();
        }
    }
}
