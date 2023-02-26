using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Exposed

    [SerializeField] float _playerSpeed = 5f;


    #endregion

    #region Unity Lyfecycle

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _spriteRenderer.flipX = false;
    }

    void Start()
    {

    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

    }

    private void FixedUpdate()
    {
        Move();
    }


    #endregion

    #region Methods

    private void Move()
    {
        Vector2 movement = new Vector2(horizontal, vertical);
        movement = movement.normalized;
        _rigidbody.velocity = movement * _playerSpeed;
        if (movement.x > 0)
        {
            _isFacingRight = true;
        }
        else if (movement.x < 0)
        {
            _isFacingRight = false;
        }
        _spriteRenderer.flipX = !_isFacingRight;
    }


    #endregion

    #region Private & Protected

    Rigidbody2D _rigidbody;
    SpriteRenderer _spriteRenderer;
    float horizontal;
    float vertical;
    bool _isFacingRight = true;

    public float PlayerSpeed { get => _playerSpeed; set => _playerSpeed = value; }

    #endregion

}
