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
        Vector2 movement = new Vector2(horizontal, vertical);
        movement = movement.normalized;
        _rigidbody.velocity = movement * _playerSpeed;
    }


    #endregion

    #region Methods


    #endregion

    #region Private & Protected

    Rigidbody2D _rigidbody;
    float horizontal;
    float vertical;

    public float PlayerSpeed { get => _playerSpeed; set => _playerSpeed = value; }

    #endregion

}
