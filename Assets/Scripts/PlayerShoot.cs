using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    #region Expose

    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] float _nextTimeToShoot;
    [SerializeField] float _shootSpeed = 2f;
    [SerializeField] float _timeToDestroy = 3.0f;

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

        Shoot(Vector2.up);

    }


    #endregion

    #region Methods

    private void Shoot(Vector2 direction)
    {
        if (Time.timeSinceLevelLoad > _nextTimeToShoot)
        {
            GameObject bullet = Instantiate(_bulletPrefab, transform.up, transform.rotation);
            bullet.GetComponent<Rigidbody2D>().velocity = direction;
            _nextTimeToShoot = Time.timeSinceLevelLoad + _shootSpeed;
        }
    }

    #endregion

    #region Private & Protected

    #endregion
}
