using System;
using Combat;
using UnityEngine;

namespace Juice
{
    /// <summary>
    /// Event Bus：全域的事件中心。
    /// 發送端（EnemyDeathBroadcaster）和接收端（DeathEffectSpawner、KillCounter）互相不認識，
    /// 只認識這個類別。所以敵人 Prefab 不需要引用場景裡的任何物件。
    /// （Cinemachine Impulse 也是同樣的結構：ImpulseSource → ImpulseManager → ImpulseListener）
    /// </summary>
    public static class GameEvents
    {
        /// <summary>有敵人死掉了。參數：死亡位置、那隻敵人的 EnemyData（可以用來分辨是哪種敵人）。</summary>
        public static event Action<Vector3, EnemyData> EnemyKilled;

        public static void RaiseEnemyKilled(Vector3 position, EnemyData data)
        {
            EnemyKilled?.Invoke(position, data);
        }
    }
}
