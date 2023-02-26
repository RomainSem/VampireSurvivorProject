using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : StateMachineBehaviour
{
    [SerializeField]
    private GameObject _bulletPrefab;
    private GameObject _bulletGroup;
    private GameObject _player;
    [SerializeField]
    private float _shootSpeed = 10f;

    private void Awake()
    {
        _bulletGroup = GameObject.Find("BulletGroup");
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        RewardsManager _rewardsManager = GameObject.Find("RewardsManager").GetComponent<RewardsManager>();
        //_rewardsManager.PiercingShot.Invoke();
        Transform _playerTransform = _player.transform;

        // Create list of positions
        List<Vector3> projectilePositions = new List<Vector3>()
    {
        new Vector3(_playerTransform.position.x, _playerTransform.position.y + 1, _playerTransform.position.z),
        new Vector3(_playerTransform.position.x, _playerTransform.position.y - 1, _playerTransform.position.z),
        new Vector3(_playerTransform.position.x + 1, _playerTransform.position.y, _playerTransform.position.z),
        new Vector3(_playerTransform.position.x - 1, _playerTransform.position.y, _playerTransform.position.z)
    };

        // Generate bullet at position
        foreach (Vector3 position in projectilePositions)
        {
            GameObject projectile = Instantiate(_bulletPrefab, position, Quaternion.identity);
            projectile.GetComponent<Rigidbody2D>().velocity = (position - _playerTransform.position).normalized * _shootSpeed;
            projectile.transform.parent = _bulletGroup.transform;
            //if (_rewardsManager.IsBulletPiercing == true)
            //{
            //    // Piercing Bullet
            //    projectile.GetComponent<CircleCollider2D>().isTrigger = true;
            //}
            Destroy(projectile, 3);
        }

        //_player.GetComponentInChildren<SpriteRenderer>().color = Color.green;
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //_player.GetComponentInChildren<SpriteRenderer>().color = Color.white;
        GameObject.Find("RewardsManager").GetComponent<RewardsManager>().AfterAttack.Invoke();


    }
}