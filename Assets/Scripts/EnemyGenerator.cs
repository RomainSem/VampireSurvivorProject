using System.Collections;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    #region Expose

    [SerializeField] GameObject _enemy;
    [SerializeField] private float _spawnDelay;
    [SerializeField] private Vector2 _spawnerRadius = new Vector2(20, 20);

    [Header("Valeurs pour dessiner le gizmo")]
    [SerializeField] private Color _gizmoColor = Color.red;
    [SerializeField] private bool _drawGizmo = true;

    #endregion

    #region Unity Lyfecycle

    private void Awake()
    {
    }

    private void Start()
    {
        StartCoroutine(Spawn(_spawnDelay, _enemy));
    }

    #endregion

    #region Methods

    private void OnDrawGizmos()
    {
        if (_drawGizmo)
        {
            Gizmos.color = _gizmoColor;
            Gizmos.DrawWireCube(transform.position, _spawnerRadius);
        }
    }

    IEnumerator Spawn(float delay, GameObject enemy)
    {
        while (true)
        {
            delay = Mathf.Max(_spawnDelay - Time.timeSinceLevelLoad * 0.05f, 0.05f);
            yield return new WaitForSeconds(delay);
            Vector2 position = Random.insideUnitCircle * _spawnerRadius + (Vector2)transform.position;
            GameObject newEnemy = Instantiate(enemy, position, Quaternion.identity);
            newEnemy.transform.parent = transform;
        }
    }

    #endregion

    #region Private & Protected


    #endregion
}
