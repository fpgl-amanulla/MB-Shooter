using System;
using _Tools.Extensions;
using _Game.Managers;
using _Game.Scripts.Runtime.DataManager;
using TMPro;
using UnityEngine;

namespace _Game.UI
{
    public class LevelStartPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _totalKillCountText;
        [SerializeField] private TextMeshProUGUI _totalTimeSurvived;

        #region Custom Methods

        private void Start()
        {
            _totalKillCountText.text = "Best Kill: " + GameDataManager.Instance.GetBestKillCount();
            _totalTimeSurvived.text = "Best Time: " + ((float)GameDataManager.Instance.GetBestTimeSurvived()).GetFloatToClockFormat();
        }

        public void LevelStart()
        {
            if (GameManager.Instance.IsNotNull(nameof(GameManager))) GameManager.Instance.LevelStart();
            DisablePanel();
        }

        private void DisablePanel() => gameObject.SetActive(false);

        #endregion
    }
}