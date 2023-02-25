using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    #region Exposed

    [SerializeField] EnemyHealth _enemyHealthRef;

    #endregion

    #region Unity Lifecycle

    private void Awake()
    {
        _rewardsManagerRef = GameObject.Find("RewardsManager").GetComponent<RewardsManager>();

    }

    #endregion

    #region Methods

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Enemy")
    //    {
    //        if (_rewardsManagerRef.IsBulletPiercing)
    //        {
    //            _enemyHealthRef._EnemyHealth--;
    //            if (_enemyHealthRef._EnemyHealth <= 0)
    //            {
    //                _enemyHealthRef.IsDead = true;
    //                _enemyHealthRef.Death();
    //            }
    //        }
    //    }
    //}

    #endregion

    #region Private & Protected

    RewardsManager _rewardsManagerRef;


    #endregion
}
