using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Expose
    //[SerializeField] GameObject _enemyPrefab;
    [SerializeField] GameObject _inGameMenuPanel;
    [SerializeField] GameObject _scorePanel;


    #endregion

    #region Unity Lyfecycle

    private void Awake()
    {
    }

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (_isGameMenuOpen)
            {
                if (PlayerHealth.IsPlayerDead == true)
                {
                }
                else
                {
                    ContinueGame();
                }
            }
            else
            {
                if (PlayerHealth.IsPlayerDead == true)
                {
                }
                else
                {
                    PauseGame();
                }
            }
        }
    }


    #endregion

    #region Methods

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PauseGame()
    {
        _isGameMenuOpen = true;
        _inGameMenuPanel.SetActive(true);
        _scorePanel.SetActive(false);
        Time.timeScale = 0;
    }

    public void ContinueGame()
    {
        _inGameMenuPanel.SetActive(false);
        _isGameMenuOpen = false;
        _scorePanel.SetActive(true);
        Time.timeScale = 1;
    }

    #endregion

    #region Private & Protected

    bool _isGameMenuOpen;

    #endregion
}
