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
            // TODO [JUICE-5] 透過事件頻道廣播「有敵人死了」。
            // 提示：channel.Raise 需要兩個參數：位置（transform.position）和 enemy.Data。
            //       channel 可能還沒拖（null），先檢查。
        }
    }
}
