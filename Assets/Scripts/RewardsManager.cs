using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RewardsManager : MonoBehaviour
{
    #region Expose

    public UnityEvent AfterAttack;
    public UnityEvent AfterEnemyDeath;
    public static GameObject _rewardPanel;
    
    #endregion

    #region Unity Lyfecycle

    private void Awake()
    {
        _rewardPanel = GameObject.Find("RewardsPanel");
    }

    void Start()
    {
        //AfterAttack.AddListener(DoubleAttack);
        //AfterEnemyDeath.AddListener(CreateBonusBullet);
    }

    void Update()
    {
        
    }

    
    #endregion

    #region Methods

    public void AddAttack()
    {
        AfterAttack.AddListener(DoubleAttack);
        _rewardPanel.SetActive(false);
    }

    public void AddBulletAfterEnemyDeath()
    {
        AfterEnemyDeath.AddListener(CreateBonusBullet);
        _rewardPanel.SetActive(false);
    }

    public void DoubleAttack()
    {
        Debug.Log("Double Attack");
    }

    public void CreateBonusBullet()
    {
        Debug.Log("Bonus Bullet");
    }

    #endregion

    #region Private & Protected

    #endregion
}
