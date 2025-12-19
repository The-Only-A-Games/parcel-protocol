using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject playeMenu;
    public GameObject gameOver;
    public TextMeshProUGUI scoreText;
    public bool IsPaused = false;

    public Slider healthSlider;

    void Start()
    {
        pauseMenu.SetActive(false);
        IsPaused = false;
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        playeMenu.SetActive(false);
        gameOver.SetActive(false);
        Time.timeScale = 0f;
        IsPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        gameOver.SetActive(false);
        playeMenu.SetActive(true);
        Time.timeScale = 1f;
        IsPaused = false;
    }


    public void GameOver()
    {
        pauseMenu.SetActive(false);
        playeMenu.SetActive(false);
        gameOver.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        pauseMenu.SetActive(false);
        playeMenu.SetActive(true);
        Time.timeScale = 1f;
        IsPaused = false;
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void SetScore(int score)
    {
        scoreText.text = "Score: " + score;
    }

    public float GetHealth()
    {
        return healthSlider.value;
    }
}
