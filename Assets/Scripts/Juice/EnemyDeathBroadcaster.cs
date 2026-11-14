using Enemy;
using UnityEngine;

namespace Juice
{
    /// <summary>
    /// 放在敵人 Prefab 上。把 Health.OnKilled 拖到 Broadcast()，
    /// 敵人死掉時就會對整個遊戲「廣播」：我死了、死在這裡、我是哪種敵人。
    /// 收到廣播後要做什麼（粒子、計分……），它都不用知道。
    /// </summary>
    [RequireComponent(typeof(EnemyController))]
    public class EnemyDeathBroadcaster : MonoBehaviour
    {
        private EnemyController enemy;

        private void Awake()
        {
            enemy = GetComponent<EnemyController>();
        }

        public void Broadcast()
        {
            // TODO [JUICE-5] 透過 GameEvents 廣播「有敵人死了」。
            // 提示：GameEvents.RaiseEnemyKilled 需要兩個參數：位置（transform.position）和 enemy.Data。
        }
    }
}
