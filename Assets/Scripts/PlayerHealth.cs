using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    #region Expose

    [SerializeField] GameObject _enemyGenerators;
    [SerializeField] GameObject _gameOverPanel;
    [SerializeField] GameObject _scorePanel;
    
    #endregion

    #region Unity Lyfecycle

    private void Awake()
    {
        
    }

    void Start()
    {
        Time.timeScale= 1.0f;
        IsPlayerDead = false;
    }

    void Update()
    {
        
    }


    #endregion

    #region Methods

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Time.timeScale = 0;
            _enemyGenerators.SetActive(false);
            _scorePanel.SetActive(false);
            _gameOverPanel.SetActive(true);
            IsPlayerDead = true;
            
        }
    }


    #endregion

    #region Private & Protected

    static bool _isPlayerDead = false;

    public static bool IsPlayerDead { get => _isPlayerDead; set => _isPlayerDead = value; }

    #endregion
}
