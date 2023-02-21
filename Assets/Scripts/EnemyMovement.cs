using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    #region Expose

    [SerializeField] float _speed = 1f;

    #endregion

    #region Unity Lyfecycle

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void Start()
    {

    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        MoveToPlayer();
    }

    #endregion

    #region Methods

    private void MoveToPlayer()
    {
        if (_isMoving == false)
        {
            StartCoroutine(Death());
            _rigidbody.constraints = RigidbodyConstraints2D.FreezePosition;
        }
        else
        {
            _direction = _player.transform.position - transform.position;
        }
        _rigidbody.velocity = _direction.normalized * _speed;

    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            _animator.SetBool("isDead", true);
            _isMoving = false;
            Destroy(collision.gameObject);
        }
    }

    #endregion

    #region Private & Protected

    GameObject _player;
    Rigidbody2D _rigidbody;
    Animator _animator;
    Vector2 _direction;
    bool _isMoving = true;

    #endregion
}
