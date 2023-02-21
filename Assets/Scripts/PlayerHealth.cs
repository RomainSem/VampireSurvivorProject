using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    #region Expose

    [SerializeField] GameObject _enemyGenerators;
    [SerializeField] GameObject _gameOverPanel;
    
    #endregion

    #region Unity Lyfecycle

    private void Awake()
    {
        
    }

    void Start()
    {
        Time.timeScale= 1.0f;
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
            _gameOverPanel.SetActive(true);
        }
    }


    #endregion

    #region Private & Protected

    #endregion
}
