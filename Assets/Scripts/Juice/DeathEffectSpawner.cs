using Combat;
using UnityEngine;

namespace Juice
{
    /// <summary>
    /// 場景裡放一個就好。聽 GameEvents.EnemyKilled，在敵人死掉的位置生成死亡粒子。
    /// 粒子是新生成的獨立物件，不是敵人的子物件，所以敵人被 Destroy 也不會跟著消失。
    /// </summary>
    public class DeathEffectSpawner : MonoBehaviour
    {
        [SerializeField] private ParticleSystem deathPrefab;

        private void OnEnable()
        {
            // TODO [JUICE-6] 訂閱 GameEvents.EnemyKilled，有敵人死掉時呼叫 Spawn。
            // 提示：跟上一堂 BgmPlayer 訂閱音量事件一樣，用 += 。
        }

        private void OnDisable()
        {
            // TODO [JUICE-6] 取消訂閱（和 OnEnable 成對，用 -= ）。
            // 想一想：GameEvents 是 static，場景重新載入後它還在。忘記取消訂閱會發生什麼事？
        }

        private void Spawn(Vector3 position, EnemyData data)
        {
            if (deathPrefab == null)
                return;

            ParticleSystem ps = Instantiate(deathPrefab, position, Quaternion.identity);
            var main = ps.main;
            Destroy(ps.gameObject, main.duration + main.startLifetime.constantMax);
        }
    }
}
