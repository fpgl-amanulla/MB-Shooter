using System;
using _Game.Managers;
using _Game.Scripts.Runtime.DataManager;
using _Tools.Extensions;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _Game.UI
{
    public class GamePanel : MonoBehaviour
    {
        [SerializeField] private GameObject _visual;
        [SerializeField] private TextMeshProUGUI _killCountText;
        [SerializeField] private TextMeshProUGUI _timeCountText;

        private float _timeCounter;
        private bool _shouldCountTime;
        private float _survivedTime;

        private void Start()
        {
            _visual.SetActive(false);
            GameManager.Instance.OnLevelStart += OnLevelStart;
            GameManager.Instance.OnLevelFail += OnLevelFail;
            GameEventManager.Instance.onEnemyKilled += UpdateKillCount;
        }

        private void Update()
        {
            if (_shouldCountTime)
            {
                _timeCounter += Time.deltaTime;
                if (_timeCounter >= 1)
                {
                    _timeCounter = 0;
                    _survivedTime++;
                    GameDataManager.Instance.UpdateTimer(_survivedTime);
                    _timeCountText.text = _survivedTime.GetFloatToClockFormat();
                }
            }
        }

        private void OnDestroy()
        {
            GameEventManager.Instance.onEnemyKilled -= UpdateKillCount;
            GameManager.Instance.OnLevelStart -= OnLevelStart;
        }

        private void UpdateKillCount(int killCount)
        {
            _killCountText.text = "Score: " + killCount;
            _killCountText.rectTransform.DOShakeScale(.1f, .1f);
        }

        private void OnLevelFail()
        {
            _shouldCountTime = false;
            _visual.SetActive(false);
        }

        private void OnLevelStart()
        {
            _shouldCountTime = true;
            _visual.SetActive(true);
        }
    }
}