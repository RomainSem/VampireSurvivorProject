using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMovement : MonoBehaviour
{
    #region Expose

    [SerializeField] float _speed = 1f;
    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] int _generatedBulletSpeed = 10;

    #endregion

    #region Unity Lyfecycle

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _rigidbody = GetComponent<Rigidbody2D>();
        _bonusProjectileParent = GameObject.Find("BonusProjectile").transform;
        _enemyHealthRef = GetComponent<EnemyHealth>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();

    }


    private void FixedUpdate()
    {
        MoveToPlayer();
    }

    #endregion

    #region Methods

    private void MoveToPlayer()
    {
        if (!_enemyHealthRef.IsDead)
        {
            _direction = _player.transform.position - transform.position;
            _rigidbody.velocity = _direction.normalized * _speed;
            if (_direction.x > 0)
            {
                _isEnemyFacingRight = false;
            }
            else if (_direction.x < 0)
            {
                _isEnemyFacingRight = true;
            }
            _spriteRenderer.flipX = !_isEnemyFacingRight;
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
    Vector2 _direction;
    Transform _bonusProjectileParent;
    EnemyHealth _enemyHealthRef;
    SpriteRenderer _spriteRenderer;
    bool _isEnemyFacingRight = false;

    private float _spawnerRadius = 3;

    #endregion
}
