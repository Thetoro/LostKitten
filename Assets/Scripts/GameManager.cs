using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private SistemaVidas sistemaVidas;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject youWin;
    [SerializeField] private GameObject pause;

    private bool isPaused;
    private bool isPlayerDead;

    public bool IsPlayerDead { set => isPlayerDead = value; }

    // Start is called before the first frame update
    void Start()
    {
        gameOver.SetActive(false);
        youWin.SetActive(false);
        pause.SetActive(false);
        isPaused = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isPlayerDead)
        {
            GameOver();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
                PauseGame();
            else
                ResumeGame();
        }
    }

    void GameOver()
    {
        gameOver.SetActive(true);
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void YouWin()
    {
        youWin.SetActive(true);
        Time.timeScale = 0;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0;
        pause.SetActive(true);
        isPaused = true;
    }

    void ResumeGame()
    {
        Time.timeScale = 1;
        pause.SetActive(false);
        isPaused = false;
    }
}
