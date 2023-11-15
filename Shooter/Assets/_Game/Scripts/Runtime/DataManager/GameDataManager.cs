using _Game.Managers;
using JetBrains.Annotations;
using UnityEngine;

namespace _Game.Scripts.Runtime.DataManager
{
    public class GameDataManager
    {
        public const string BEST_KILL_COUNT = "BestKillCountKey";
        public const string BEST_TIME_SURVIVED = "BestTimeSurvived";

        private static GameDataManager instance;
        public static GameDataManager Instance => instance ??= new GameDataManager();

        private int _killCount = 0;
        private float _survivedTime;

        public void ResetData()
        {
            _killCount = 0;
            _survivedTime = 0;
        }

        public void UpdateKillCount()
        {
            _killCount++;
            GameEventManager.Instance.onEnemyKilled?.Invoke(_killCount);
        }

        public int GetTotalKillCount() => _killCount;

        public void CheckBestKill()
        {
            if (_killCount >= GetBestKillCount())
            {
                SaveBestKillCount(_killCount);
            }
        }

        public void CheckTimeSurvived()
        {
            if (_survivedTime >= GetBestTimeSurvived())
            {
                SaveBestTimeSurvived(_survivedTime);
            }
        }

        public int GetBestKillCount() => PlayerPrefs.GetInt(BEST_KILL_COUNT, 0);
        public void SaveBestKillCount(int value) => PlayerPrefs.SetInt(BEST_KILL_COUNT, value);

        public int GetBestTimeSurvived() => PlayerPrefs.GetInt(BEST_TIME_SURVIVED, 0);
        public void SaveBestTimeSurvived(float value) => PlayerPrefs.SetInt(BEST_TIME_SURVIVED, (int)value);

        public void UpdateTimer(float survivedTime) => _survivedTime = survivedTime;
        public float GetSurvivedTime() => _survivedTime;
    }
}