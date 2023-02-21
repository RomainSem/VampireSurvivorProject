using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : StateMachineBehaviour
{
    [SerializeField]
    private GameObject _bulletPrefab;
    [SerializeField]
    private float _shootSpeed;
    [SerializeField]
    private GameObject _bulletGroup;
    private GameObject _player;

    private void Awake()
    {
        _bulletGroup = GameObject.Find("BulletGroup");
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject projectileUp = Instantiate(_bulletPrefab, animator.gameObject.transform.position, Quaternion.identity);
        GameObject projectileDown = Instantiate(_bulletPrefab, animator.gameObject.transform.position, Quaternion.identity);
        GameObject projectileRight = Instantiate(_bulletPrefab, animator.gameObject.transform.position, Quaternion.identity);
        GameObject projectileLeft = Instantiate(_bulletPrefab, animator.gameObject.transform.position, Quaternion.identity);

        projectileUp.GetComponent<Rigidbody2D>().velocity = Vector2.up * _shootSpeed;
        projectileDown.GetComponent<Rigidbody2D>().velocity = Vector2.down * _shootSpeed;
        projectileRight.GetComponent<Rigidbody2D>().velocity = Vector2.right * _shootSpeed;
        projectileLeft.GetComponent<Rigidbody2D>().velocity = Vector2.left * _shootSpeed;
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

    }
}