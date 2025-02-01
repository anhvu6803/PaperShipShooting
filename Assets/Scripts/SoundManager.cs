using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private Slider soundSlider;
    [SerializeField] private Slider musicSlider;

    private AudioPlayer audioPlayer;
    private void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }
    public void SoundChange()
    {
        audioPlayer.SetEffectVolume(soundSlider.value);
    }
    public void MusicChange()
    {
        audioPlayer.SetMusicVolume(musicSlider.value);
    }
}
