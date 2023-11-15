using System;
using _Game.Managers;
using _Game.Scripts.Runtime.DataManager;
using _Tools.Extensions;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.UI
{
    public class GamePanel : MonoBehaviour
    {
        [SerializeField] private GameObject _visual;
        [SerializeField] private TextMeshProUGUI _killCountText;
        [SerializeField] private TextMeshProUGUI _timeCountText;
        [SerializeField] private Button _pauseButton;
        [SerializeField] private Button _unpauseButton;
        [SerializeField] private GameObject _pausePanel;

        private float _timeCounter;
        private bool _shouldCountTime;
        private float _survivedTime;

        private void Start()
        {
            _visual.SetActive(false);
            _pausePanel.SetActive(false);
            GameManager.Instance.OnLevelStart += OnLevelStart;
            GameManager.Instance.OnLevelFail += OnLevelFail;
            GameEventManager.Instance.onEnemyKilled += UpdateKillCount;

            _pauseButton.onClick.AddListener(() =>
            {
                Time.timeScale = 0;
                _pauseButton.gameObject.SetActive(false);
                _pausePanel.SetActive(true);
            });
            _unpauseButton.onClick.AddListener(() =>
            {
                Time.timeScale = 1;
                _pauseButton.gameObject.SetActive(true);
                _pausePanel.SetActive(false);
            });
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