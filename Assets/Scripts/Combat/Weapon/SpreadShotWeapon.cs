using UnityEngine;

namespace Combat
{
    /// <summary>
    /// 散彈槍：繼承 RangedWeapon，改寫 PerformAttack，一次朝扇形方向射出多顆子彈。
    /// 注意：父類別 Weapon.Attack 每次只扣一發彈藥，所以一次散射（pelletCount 顆子彈）只消耗 1 發，這是預期行為。
    /// </summary>
    public class SpreadShotWeapon : RangedWeapon
    {
        [SerializeField] private int pelletCount = 3;
        [SerializeField] private float spreadAngle = 30f;

        protected override void PerformAttack(Vector2 direction, Transform target)
        {
            if (pelletCount <= 1)
            {
                base.PerformAttack(direction, target);
                return;
            }

            float startAngle = -spreadAngle * 0.5f;
            float step = spreadAngle / (pelletCount - 1);

            for (int i = 0; i < pelletCount; i++)
            {
                Vector2 pelletDirection = Quaternion.Euler(0f, 0f, startAngle + step * i) * direction;
                base.PerformAttack(pelletDirection, target);
            }
        }

        private void OnValidate()
        {
            pelletCount = Mathf.Max(1, pelletCount);
            spreadAngle = Mathf.Clamp(spreadAngle, 0f, 360f);
        }
    }
}
