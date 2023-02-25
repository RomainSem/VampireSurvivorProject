using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMovement : MonoBehaviour
{
    #region Expose

    [SerializeField] float _speed = 1f;
    [SerializeField] IntVariable _nbDeadEnemies;
    [SerializeField] IntVariable _nbTotalDeadEnemies;
    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] int _generatedBulletSpeed = 10;
    [SerializeField] int _enemyHealth = 1;

    #endregion

    #region Unity Lyfecycle

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _nbKillsTMP = GameObject.Find("ValueTXT");
        _bonusProjectileParent = GameObject.Find("BonusProjectile").transform;
        _rewardsManager = GameObject.Find("RewardsManager").GetComponent<RewardsManager>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (_nbTotalDeadEnemies.m_value >= 100 && !_isHealthIncremented)
        {
            _enemyHealth++;
            _isHealthIncremented = true;
        }
    }

    private void FixedUpdate()
    {
        MoveToPlayer();
    }

    #endregion

    #region Methods

    private void MoveToPlayer()
    {
        if (_isDead == true)
        {
            StartCoroutine(Death());
            _isDead = false;
            _rigidbody.constraints = RigidbodyConstraints2D.FreezePosition;
            GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            _direction = _player.transform.position - transform.position;
        }
        _rigidbody.velocity = _direction.normalized * _speed;

    }

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
                _isDead = true;
                _animator.SetBool("isDead", true);
                _nbDeadEnemies.m_value++;
                _nbTotalDeadEnemies.m_value++;
            }
        }
    }

    public void GenerateBullet()
    {
        Vector2 position = Random.insideUnitCircle * _spawnerRadius + (Vector2)transform.position;
        GameObject projectile = Instantiate(_bulletPrefab, position, Quaternion.identity);
        gameObject.tag = "Dead Enemy";
        projectile.GetComponent<Rigidbody2D>().velocity = (GameObject.FindGameObjectWithTag("Enemy").transform.position - transform.position).normalized * _generatedBulletSpeed;
        projectile.name = "Projectile Bonus";
        projectile.transform.parent = _bonusProjectileParent;
        Destroy(projectile, 5);
    }

    #endregion

    #region Private & Protected

    GameObject _player;
    Rigidbody2D _rigidbody;
    Animator _animator;
    Vector2 _direction;
    bool _isDead;
    Transform _bonusProjectileParent;
    RewardsManager _rewardsManager;
    BoxCollider2D _boxCollider;

    static GameObject _nbKillsTMP;
    private float _spawnerRadius = 5;
    private bool _isHealthIncremented;

    #endregion
}
