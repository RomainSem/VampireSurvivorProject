using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KillCount : MonoBehaviour
{
    [SerializeField]
    private IntVariable _killEnemy;
    [SerializeField]
    private GameObject _rewardUI;

    private void Awake()
    {
        _killCounterText = GetComponent<TextMeshProUGUI>();
        _killEnemy.m_value = 0;
    }

    private void Update()
    {
        _killCounterText.text = _killEnemy.m_value.ToString();
        if (_killEnemy.m_value == 5)
        {
            _rewardUI.SetActive(true);
            Time.timeScale = 0;
            _killEnemy.m_value = 0;
        }
    }
    private TextMeshProUGUI _killCounterText;
}
