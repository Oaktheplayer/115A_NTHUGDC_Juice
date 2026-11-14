using System;
using Combat;
using UnityEngine;

namespace Juice
{
    /// <summary>
    /// Event Bus（事件頻道），用 ScriptableObject 做。和上一堂的 AudioSettingsData 是同一個想法：
    /// 一份 asset 放在 Project 裡，大家都去拖「同一份」asset。
    /// - 發送端（敵人 Prefab 上的 EnemyDeathBroadcaster）呼叫 Raise
    /// - 接收端（場景裡的 DeathEffectSpawner）訂閱 OnRaised。之後要加計分、成就，只要再寫一個 listener 聽同一份 asset
    /// 雙方只認識這份 asset，彼此不認識。Prefab 不能拖場景物件，但「可以」拖 asset，所以敵人 Prefab 丟到哪個場景都能用。
    /// （Cinemachine Impulse 也是同樣的結構：ImpulseSource → ImpulseManager → ImpulseListener）
    /// </summary>
    [CreateAssetMenu(fileName = "EnemyKilledChannel", menuName = "Events/Enemy Killed Channel")]
    public class EnemyKilledChannel : ScriptableObject
    {
        /// <summary>有敵人死掉了。參數：死亡位置、那隻敵人的 EnemyData（可以用來分辨是哪種敵人）。</summary>
        public event Action<Vector3, EnemyData> OnRaised;

        public void Raise(Vector3 position, EnemyData data)
        {
            OnRaised?.Invoke(position, data);
        }
    }
}
