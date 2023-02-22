using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : StateMachineBehaviour
{
    [SerializeField]
    private GameObject _bulletPrefab;
    //[SerializeField]
    //private float _shootSpeed;
    private GameObject _bulletGroup;
    private GameObject _player;
    float _playerSpeed;

    private void Awake()
    {
        _bulletGroup = GameObject.Find("BulletGroup");
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerSpeed = _player.GetComponent<PlayerMovement>().PlayerSpeed;
    }
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject projectileUp = Instantiate(_bulletPrefab, new Vector3( animator.gameObject.transform.position.x, animator.gameObject.transform.position.y +1, animator.gameObject.transform.position.z), Quaternion.identity);
        GameObject projectileDown = Instantiate(_bulletPrefab, new Vector3(animator.gameObject.transform.position.x, animator.gameObject.transform.position.y -1, animator.gameObject.transform.position.z), Quaternion.identity);
        GameObject projectileRight = Instantiate(_bulletPrefab, new Vector3(animator.gameObject.transform.position.x +1, animator.gameObject.transform.position.y, animator.gameObject.transform.position.z), Quaternion.identity);
        GameObject projectileLeft = Instantiate(_bulletPrefab, new Vector3(animator.gameObject.transform.position.x -1, animator.gameObject.transform.position.y, animator.gameObject.transform.position.z), Quaternion.identity);

        projectileUp.GetComponent<Rigidbody2D>().velocity = Vector2.up * _playerSpeed;
        projectileDown.GetComponent<Rigidbody2D>().velocity = Vector2.down * _playerSpeed;
        projectileRight.GetComponent<Rigidbody2D>().velocity = Vector2.right * _playerSpeed;
        projectileLeft.GetComponent<Rigidbody2D>().velocity = Vector2.left * _playerSpeed;
        projectileUp.transform.parent = _bulletGroup.transform;
        projectileDown.transform.parent = _bulletGroup.transform;
        projectileRight.transform.parent = _bulletGroup.transform;
        projectileLeft.transform.parent = _bulletGroup.transform;
        
        _player.GetComponentInChildren<SpriteRenderer>().color = Color.green;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _player.GetComponentInChildren<SpriteRenderer>().color = Color.white;
        GameObject.Find("RewardsManager").GetComponent<RewardsManager>().AfterAttack.Invoke();


    }
}