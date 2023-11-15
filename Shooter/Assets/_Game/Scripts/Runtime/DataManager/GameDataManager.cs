using _Game.Managers;
using JetBrains.Annotations;
using UnityEngine;

namespace _Game.Scripts.Runtime.DataManager
{
    public class GameDataManager
    {
        public const string BEST_KILL_COUNT = "BestKillCountKey";

        private static GameDataManager instance;
        public static GameDataManager Instance => instance ??= new GameDataManager();

        private int _killCount = 0;
        private float _survivedTime;

        public void UpdateKillCount()
        {
            _killCount++;
            GameEventManager.Instance.onEnemyKilled?.Invoke(_killCount);
        }

        public int GetTotalKillCount() => _killCount;

        public int GetBestKillCount() => PlayerPrefs.GetInt(BEST_KILL_COUNT, 0);

        public void UpdateTimer(float survivedTime) => _survivedTime = survivedTime;
        public float GetSurvivedTime() => _survivedTime;
    }
}