using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    #region Exposed

    [SerializeField] int _enemyHealth = 1;
    [SerializeField] IntVariable _nbDeadEnemies;
    [SerializeField] IntVariable _nbTotalDeadEnemies;

    #endregion

    #region Unity Lifecycle

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rewardsManager = GameObject.Find("RewardsManager").GetComponent<RewardsManager>();
        _animator = GetComponent<Animator>();
        _lastHealthIncrement = _nbTotalDeadEnemies.m_value;
    }

    void Update()
    {
        if (_nbTotalDeadEnemies.m_value >= _lastHealthIncrement + 20)
        {
            Health++;
            _lastHealthIncrement = _nbTotalDeadEnemies.m_value;
        }
        //if (_nbTotalDeadEnemies.m_value >= 200 && !_isHealthIncremented)
        //{
        //    Health++;
        //    _isHealthIncremented = true;
        //}
        Death();
        
    }

    #endregion

    #region Methods

    private void Death()
    {
        if (IsDead)
        {
            StartCoroutine(DeathCoroutine());
            _rigidbody.constraints = RigidbodyConstraints2D.FreezePosition;
            GetComponent<BoxCollider2D>().enabled = false;
            IsDead = false;
        }
    }

    IEnumerator DeathCoroutine()
    {
        yield return new WaitForSeconds(1.5f);
        _rewardsManager._lastDeadEnemyRef = gameObject;
        _rewardsManager.AfterEnemyDeath.Invoke();
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Health--;
            if (!_rewardsManager.IsBulletPiercing)
            {
                Destroy(collision.gameObject);
            }
            if (Health <= 0)
            {
                IsDead = true;
                _animator.SetBool("isDead", true);
                _nbDeadEnemies.m_value++;
                _nbTotalDeadEnemies.m_value++;
            }
        }
    }

    #endregion

    #region Private & Protected

    bool _isDead;
    //private bool _isHealthIncremented;
    Rigidbody2D _rigidbody;
    RewardsManager _rewardsManager;
    Animator _animator;
    int _lastHealthIncrement;

    public bool IsDead { get => _isDead; set => _isDead = value; }
    public int Health { get => _enemyHealth; set => _enemyHealth = value; }


    #endregion
}
