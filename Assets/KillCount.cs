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
    [SerializeField] int _nbKillsNeeded = 10;

    private void Awake()
    {
        _killCounterText = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {

        _killEnemy.m_value = 0;
        
    }

    private void Update()
    {
        _killCounterText.text = _killEnemy.m_value.ToString();
        if (_killEnemy.m_value >= _nbKillsNeeded)
        {
            _rewardUI.SetActive(true);
            Time.timeScale = 0;
            _killEnemy.m_value = 0;
            _nbKillsNeeded *= 2;
        }
    }
    private TextMeshProUGUI _killCounterText;
}
