using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Expose
    //[SerializeField] GameObject _enemyPrefab;

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
        
    }

    
    #endregion

    #region Methods

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //private void OpenRewardsPanel()
    //{
    //    EnemyMovement r = _enemyPrefab.GetComponent<EnemyMovement>();
    //    if (r.NbDeadEnemies == 5)
    //    {
    //        GameObject rr = RewardsManager._rewardPanel;
    //        rr.SetActive(true);
    //        Time.timeScale = 0;
    //        //NbDeadEnemies= 0;
    //    }
    //}
    #endregion

    #region Private & Protected


    #endregion
}
