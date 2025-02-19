using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneUI : MonoBehaviour
{
    [SerializeField] private GameObject ship;
    [SerializeField] private Slider loadBar;
    [SerializeField] private float loadSpeed;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float paddingBottom;
    [SerializeField] private SceneData sceneData;

    private Vector2 startPosition;
    private Vector2 endPosition;
    private Rigidbody2D rb;
    private AudioPlayer audioPlayer;
    private void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }
    void Start()
    {
        SetPositonLoading();
        loadBar.maxValue = endPosition.x - startPosition.x;
        ship.transform.position = startPosition;
        rb = ship.GetComponent<Rigidbody2D>();
        audioPlayer.MuteMusic();
    }
    private void SetPositonLoading()
    {
        Vector2 minBound = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 maxBound = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
        startPosition = new Vector2(minBound.x, minBound.y + paddingBottom);
        endPosition = new Vector2(maxBound.x, minBound.y + paddingBottom);
    }
    void Update()
    {
        ShipRun();
    }
    private void ShipRun()
    {
        rb.velocity = ship.transform.up * Random.Range(0, loadSpeed);
        ship.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(-85, -95)));
        loadBar.value = ship.transform.position.x - startPosition.x;
    }
    public void ChangeScene()
    {
        if (loadBar.value == loadBar.maxValue)
        {
            audioPlayer.PlayMusic();
            SceneManager.LoadScene(sceneData.LoadName());
        }
    }
}
