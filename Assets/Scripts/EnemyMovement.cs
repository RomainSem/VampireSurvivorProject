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
    [SerializeField] GameObject _bulletPrefab;

    #endregion

    #region Unity Lyfecycle

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _nbKills = GameObject.Find("ValueTXT");
        _enemyDeadBool = GameObject.Find("RewardsManager").GetComponent<RewardsManager>().IsEnemyDead;
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
        if (_isDead == true)
        {
            StartCoroutine(Death());
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
        yield return new WaitForSeconds(2);
        GameObject.Find("RewardsManager").GetComponent<RewardsManager>().AfterEnemyDeath.Invoke();
        Destroy(gameObject);
        if (_enemyDeadBool)
        {
            Vector3 position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            GameObject projectile = Instantiate(_bulletPrefab, position, Quaternion.identity);
            projectile.GetComponent<Rigidbody2D>().velocity = transform.position.normalized * 10;
            //projectile.transform.parent = _bulletGroup.transform;
            Destroy(projectile, 3);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            _animator.SetBool("isDead", true);
            _isDead = true;
            Destroy(collision.gameObject);
            _nbDeadEnemies.m_value++;
        }
    }

    #endregion

    #region Private & Protected

    GameObject _player;
    Rigidbody2D _rigidbody;
    Animator _animator;
    Vector2 _direction;
    bool _isDead;
    bool _enemyDeadBool;

    static GameObject _nbKills;

    //[SerializeField]
    //static int _nbDeadEnemies = 0;

    // Static pour que la valeur de NbDeadEnemies soit commune à tous les ennemies
    //public static int NbDeadEnemies 
    //{ get { return _nbDeadEnemies; }
    //  set { TextMeshProUGUI _nbKillsUGUI = _nbKills.GetComponent<TextMeshProUGUI>();
    //        _nbKillsUGUI.text = value.ToString();
    //        _nbDeadEnemies = value; }
    //}

    #endregion
}
