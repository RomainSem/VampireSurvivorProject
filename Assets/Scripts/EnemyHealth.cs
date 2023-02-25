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
    }

    void Update()
    {
        if (_nbTotalDeadEnemies.m_value >= 250 && !_isHealthIncremented)
        {
            _enemyHealth++;
            _isHealthIncremented = true;
        }

        if (IsDead)
        {
            StartCoroutine(Death());
            _isDead = false;
            _rigidbody.constraints = RigidbodyConstraints2D.FreezePosition;
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    #endregion

    #region Methods

    IEnumerator Death()
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
            _enemyHealth--;
            if (!_rewardsManager.IsBulletPiercing)
            {
                Destroy(collision.gameObject);
            }
            if (_enemyHealth <= 0)
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
    private bool _isHealthIncremented;
    Rigidbody2D _rigidbody;
    RewardsManager _rewardsManager;
    Animator _animator;

    public bool IsDead { get => _isDead; set => _isDead = value; }


    #endregion
}
