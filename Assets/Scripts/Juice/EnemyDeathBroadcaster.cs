using Enemy;
using UnityEngine;

namespace Juice
{
    /// <summary>
    /// 放在敵人 Prefab 上。把 Health.OnKilled 拖到 Broadcast()，
    /// 敵人死掉時就會透過事件頻道「廣播」：我死了、死在這裡、我是哪種敵人。
    /// 收到廣播後要做什麼（粒子、計分……），它都不用知道。
    /// </summary>
    [RequireComponent(typeof(EnemyController))]
    public class EnemyDeathBroadcaster : MonoBehaviour
    {
        [Tooltip("拖 Project 裡的 EnemyKilledChannel asset 進來")]
        [SerializeField] private EnemyKilledChannel channel;

        private EnemyController enemy;

        private void Awake()
        {
            enemy = GetComponent<EnemyController>();
            if (channel == null)
                Debug.LogWarning($"{name}: EnemyDeathBroadcaster has no EnemyKilledChannel assigned.", this);
        }

        public void Broadcast()
        {
            if (channel != null)
                channel.Raise(transform.position, enemy.Data);
        }
    }
}
