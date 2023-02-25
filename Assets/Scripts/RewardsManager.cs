using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RewardsManager : MonoBehaviour
{
    #region Expose

    public UnityEvent AfterAttack;
    public UnityEvent AfterEnemyDeath;
    public UnityEvent PiercingShot;
    [SerializeField] GameObject _rewardPanel;
    [SerializeField] float _shootSpeed = 20;
    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] GameObject _bulletGroup;


    #endregion

    #region Unity Lyfecycle

    private void Awake()
    {
        //_rewardPanel = GameObject.Find("RewardsPanel");
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerTransform = _player.transform;
    }

    void Start()
    {

    }

    void Update()
    {

    }

    #endregion

    #region Methods

    public void AddAttack()
    {
        AfterAttack.AddListener(DoubleAttack);
        _rewardPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void AddBulletAfterEnemyDeath()
    {
        AfterEnemyDeath.AddListener(CreateBonusBullet);
        _rewardPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void AddPiercingShot()
    {
        PiercingShot.AddListener(PiercingBullet);
        _rewardPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void DoubleAttack()
    {
        if (Random.Range(0, 2) == 0)
        {
            List<Vector3> projectilePositions = new List<Vector3>()
            {
             new Vector3(_playerTransform.position.x +1, _playerTransform.position.y + 1, _playerTransform.position.z),
             new Vector3(_playerTransform.position.x -1, _playerTransform.position.y - 1, _playerTransform.position.z),
             new Vector3(_playerTransform.position.x + 1, _playerTransform.position.y -1, _playerTransform.position.z),
             new Vector3(_playerTransform.position.x - 1, _playerTransform.position.y +1, _playerTransform.position.z)
            };

            foreach (Vector3 position in projectilePositions)
            {
                GameObject projectile = Instantiate(_bulletPrefab, position, Quaternion.identity);
                projectile.GetComponent<Rigidbody2D>().velocity = (position - _playerTransform.position).normalized * _shootSpeed;
                projectile.transform.parent = _bulletGroup.transform;
                Destroy(projectile, 3);

            }
        }
    }

    public void CreateBonusBullet()
    {
        _lastDeadEnemyRef.GetComponent<EnemyMovement>().GenerateBullet();
    }

    public void PiercingBullet()
    {
        IsBulletPiercing = true;
    }
        

    #endregion

    #region Private & Protected

    GameObject _player;
    Transform _playerTransform;
    bool _isEnemyDead;
    bool _isBulletPiercing;
    public GameObject _lastDeadEnemyRef;

    public bool IsEnemyDead { get => _isEnemyDead; set => _isEnemyDead = value; }
    public bool IsBulletPiercing { get => _isBulletPiercing; set => _isBulletPiercing = value; }

    #endregion
}
