using System;
using UnityEngine;
using UnityEngine.Events;

namespace Combat
{
    public class Health : MonoBehaviour
    {
        public enum State : byte
        {
            Dead = 0,
            Alive = 1
        }

        [Header("Settings")]
        [SerializeField] private int maxValue = 5;
        [SerializeField] private bool destroyOnDeath = true;

        [Header("Runtime")]
        [SerializeField] private int value;

        public int Value => value;
        public int MaxValue => maxValue;
        public State LifeState { get; private set; } = State.Alive;
        public bool IsAlive => LifeState == State.Alive;

        // TODO [JUICE-1] (1/3) 宣告兩個 UnityEvent：OnDamaged（受傷時）和 OnKilled（死亡時）。
        // 提示：public UnityEvent 名字; —— 存檔回到 Unity，Inspector 會多出可以拖函式的欄位。

        public event Action OnKilledEvent;
        public event Action<int> OnDamageTaken;
        /// <summary>Fires whenever Value or MaxValue changes (damage, heal, apply max, death).</summary>
        public event Action OnChanged;

        private void Awake()
        {
            value = maxValue;
            LifeState = State.Alive;
            NotifyChanged();
        }

        public void ApplyMaxHealth(int max)
        {
            maxValue = Mathf.Max(1, max);
            value = maxValue;
            LifeState = State.Alive;
            enabled = true;
            NotifyChanged();
        }

        public void TakeDamage(int amount)
        {
            if (LifeState == State.Dead)
                return;

            if (amount <= 0)
                return;

            int prev = value;
            value = Mathf.Max(0, value - amount);
            int delta = prev - value;

            // ❌ 壞例子：Health 自己去找 AudioSource 來播音效。
            // 把下面這行的註解拿掉，Play 看看：被打會出聲音。
            // 那如果還要粒子？鏡頭晃動？玩家和敵人要不同的聲音？—— 每加一種，就要回來改 Health。
            // GetComponent<AudioSource>().Play();

            OnDamageTaken?.Invoke(delta);
            // TODO [JUICE-1] (2/3) 受傷時呼叫 OnDamaged。（記得把上面的壞例子刪掉）
            NotifyChanged();

            if (value <= 0)
                Kill();
        }

        public void TakeHeal(int amount)
        {
            if (LifeState == State.Dead)
                return;

            int next = Mathf.Min(maxValue, value + Mathf.Max(0, amount));
            if (next == value)
                return;

            value = next;
            NotifyChanged();
        }

        [ContextMenu("Test Kill")]
        public void Kill()
        {
            if (LifeState == State.Dead)
                return;

            LifeState = State.Dead;
            value = 0;
            // TODO [JUICE-1] (3/3) 死亡時呼叫 OnKilled。
            // 想一想：為什麼要在 Destroy(gameObject) 之前呼叫？
            OnKilledEvent?.Invoke();
            NotifyChanged();

            if (destroyOnDeath)
                Destroy(gameObject);
            else
                enabled = false;
        }

        private void NotifyChanged()
        {
            OnChanged?.Invoke();
        }
    }
}
