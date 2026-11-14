using Combat;
using Unity.Cinemachine;
using UnityEngine;

namespace Juice
{
    /// <summary>
    /// 參考用：受傷越重，鏡頭晃得越大。
    /// UnityEvent 只能傳 Inspector 裡寫死的數字；要依「這次打多重」決定力道，就得從程式呼叫。
    /// </summary>
    [RequireComponent(typeof(CinemachineImpulseSource), typeof(Health))]
    public class DamageImpulse : MonoBehaviour
    {
        [SerializeField] private float forcePerDamage = 0.5f;
        [SerializeField] private float maxForce = 2f;

        private CinemachineImpulseSource source;
        private Health health;

        private void Awake()
        {
            source = GetComponent<CinemachineImpulseSource>();
            health = GetComponent<Health>();
        }

        private void OnEnable() => health.OnDamageTaken += Shake;
        private void OnDisable() => health.OnDamageTaken -= Shake;

        private void Shake(int damage)
        {
            source.GenerateImpulseWithForce(Mathf.Min(damage * forcePerDamage, maxForce));
        }
    }
}
