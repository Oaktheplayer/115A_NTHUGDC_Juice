using Combat;
using UnityEngine;

namespace Juice
{
    /// <summary>
    /// 場景裡放一個就好。聽 EnemyKilledChannel，在敵人死掉的位置生成死亡粒子。
    /// 粒子是新生成的獨立物件，不是敵人的子物件，所以敵人被 Destroy 也不會跟著消失。
    /// </summary>
    public class DeathEffectSpawner : MonoBehaviour
    {
        [Tooltip("和敵人身上的 EnemyDeathBroadcaster 拖「同一份」asset")]
        [SerializeField] private EnemyKilledChannel channel;
        [SerializeField] private ParticleSystem deathPrefab;

        private void OnEnable()
        {
            if (channel != null)
                channel.OnRaised += Spawn;
        }

        private void OnDisable()
        {
            if (channel != null)
                channel.OnRaised -= Spawn;
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
