using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int score;
    public bool isPlaying;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] GameObject StartPanel;
    [SerializeField] GameObject OverPanel;
    [SerializeField] GameObject SuccessPanel;
    public static bool isRestarted, levelUpdated;
    private void Awake()
    {
        
        if (Instance == null) Instance = this;
        else Destroy(this);
        isRestartedControl();
        scoreText.text = PlayerPrefs.GetInt("Score").ToString();
        score = PlayerPrefs.GetInt("Score");
        isPlaying = true;
    }
    public void UpdateScore()
    {
        scoreText.text = score.ToString();
    }
    public void StartGame()
    {
        StartPanel.SetActive(false);
        isPlaying = true;
    }
    public void RestartGame()
    {
        OverPanel.SetActive(false);
        isRestarted = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        AudioTest.Instance.deadMusicstop();
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void GameOver()
    {
        OverPanel.SetActive(true);
    }
    public void LevelSuccess()
    {
        PlayerPrefs.SetInt("Score", score);
        SuccessPanel.SetActive(true);
        isPlaying = false;
    }
    public void LevelUpdate()
    {
        levelUpdated = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    private void isRestartedControl()
    {
        if (isRestarted||levelUpdated)
        {
            StartPanel.SetActive(false);
            isPlaying = true;
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
