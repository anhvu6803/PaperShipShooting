using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<DamageDealer> damageDealers;
    private ScoreKeeper scoreKeeper;
    private void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }
    public void LoadGame()
    {
        Time.timeScale = 1f;
        scoreKeeper.ResetScore();
        SceneManager.LoadScene("LoadSceneGame");
    }
    public void LoadGameOver()
    {
        Time.timeScale = 1f;
        ResetDamageDealer();
        SceneManager.LoadScene("LoadSceneGameOver");
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
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
