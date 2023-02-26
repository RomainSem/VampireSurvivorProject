using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KillCount : MonoBehaviour
{
    [SerializeField] private IntVariable _killEnemy;
    [SerializeField] private IntVariable _totalEnemiesKilled;
    [SerializeField] private GameObject _rewardUI;
    [SerializeField] private GameObject _doubleAttackButton;
    [SerializeField] int _nbKillsNeeded = 10;

    private void Awake()
    {
        _killCounterText = GetComponent<TextMeshProUGUI>();
        _totalKilledEnemies = GameObject.Find("TotalKilledEnemies").GetComponent<TextMeshProUGUI>();
        _killsNeededTxt = GameObject.Find("KillsNeededTxt").GetComponent<TextMeshProUGUI>();
        _rewardManagerRef = GameObject.Find("RewardsManager").GetComponent<RewardsManager>();
    }

    private void Start()
    {
        _killEnemy.m_value = 0;
        _totalEnemiesKilled.m_value = 0;

    }

    private void Update()
    {
        _totalKilledEnemies.text = _totalEnemiesKilled.m_value.ToString();
        _killCounterText.text = _killEnemy.m_value.ToString();
        if (_killEnemy.m_value >= _nbKillsNeeded)
        {
            _rewardUI.SetActive(true);
            if (_rewardManagerRef.IsDoubleAttacking)
            {
                _doubleAttackButton.SetActive(false);
            }
            Time.timeScale = 0;
            _killEnemy.m_value = 0;
            _nbKillsNeeded *= 2;
            _killsNeededTxt.text = _nbKillsNeeded.ToString();
        }
    }

    
    private TextMeshProUGUI _killCounterText;
    private TextMeshProUGUI _killsNeededTxt;
    private TextMeshProUGUI _totalKilledEnemies;
    RewardsManager _rewardManagerRef;
}
