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
            GameEvents.EnemyKilled += Spawn;
        }

        private void OnDisable()
        {
            GameEvents.EnemyKilled -= Spawn;
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
