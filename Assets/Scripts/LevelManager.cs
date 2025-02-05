using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<DamageDealer> damageDealers;
    [SerializeField] private SceneData sceneData;
    
    private ScoreKeeper scoreKeeper;
    private StorePower storePower;

    public void LoadGame()
    {
        Time.timeScale = 1f;

        if (scoreKeeper != null)
        {
            scoreKeeper.ResetScore();
        }

        if (storePower != null)
        {
            storePower.ResetPower();
        }

        sceneData.SetLoadName("Game");
        SceneManager.LoadScene("LoadSceneGame");
    }
    public void LoadMainMenu()
    {
        sceneData.SetLoadName("MainMenu");
        SceneManager.LoadScene("LoadSceneGame");
    }
    public void LoadGameOver()
    {
        Time.timeScale = 1f;
        ResetDamageDealer();
        sceneData.SetLoadName("GameOver");
        SceneManager.LoadScene("LoadSceneGame");
    }
    public void QuitGame()
    {
        Debug.Log("Quit Game .....");
        Application.Quit();
    }
    private void ResetDamageDealer()
    {
        foreach(DamageDealer damage in  damageDealers)
        {
            damage.ResetDamage();
        }
    }
}
