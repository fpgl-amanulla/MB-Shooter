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
    public class LevelFailPanel : MonoBehaviour
    {
        #region Variables

        [SerializeField] private GameObject _visual;
        [SerializeField] private Image _transitionImage;
        [SerializeField] private TextMeshProUGUI _totalKillCountText;
        [SerializeField] private TextMeshProUGUI _totalTimeSurvived;

        private Action OnLoadScene;

        #endregion

        #region Unity Methods

        private void Awake() => DisablePanel();

        private void Start()
        {
            if (UIManager.Instance.IsNotNull(nameof(UIManager))) UIManager.Instance.OnLevelFail += UIManager_OnLevelFail;
        }

        private void OnDestroy()
        {
            //_transitionImage.DOKill();
            if (UIManager.Instance) UIManager.Instance.OnLevelFail -= UIManager_OnLevelFail;
        }

        #endregion

        #region Custom Methods

        private void UIManager_OnLevelFail(float duration, Action onTransitionComplete)
        {
            OnLoadScene = onTransitionComplete;
            _totalKillCountText.text = "Total Kill: " + GameDataManager.Instance.GetTotalKillCount();
            _totalTimeSurvived.text = "Survived Time: " + GameDataManager.Instance.GetSurvivedTime().GetFloatToClockFormat();
            _visual.SetActive(true);
            //LevelReloadTransition(duration, onTransitionComplete);
        }

        private void LevelReloadTransition(float duration, Action onTransitionComplete)
        {
            EnablePanel();
            _transitionImage.DOFade(1f, duration).SetEase(Ease.Linear).OnComplete(() => { onTransitionComplete(); });
        }

        private void EnablePanel() => _transitionImage.gameObject.SetActive(true);
        private void DisablePanel() => _transitionImage.gameObject.SetActive(false);
        public void ReloadScene() => OnLoadScene?.Invoke();

        #endregion
    }
}