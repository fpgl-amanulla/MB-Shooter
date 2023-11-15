using _Tools.Helpers;
using UnityEngine;
using UnityEngine.Events;

namespace _Game.Managers
{
    public class GameEventManager : Singleton<GameEventManager>
    {
        public UnityAction<int> onEnemyKilled;
    }
}