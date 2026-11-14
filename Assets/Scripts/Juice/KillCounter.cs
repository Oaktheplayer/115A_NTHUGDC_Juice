using Combat;
using TMPro;
using UnityEngine;

namespace Juice
{
    /// <summary>
    /// 得分 = 擊殺數。聽 GameEvents.EnemyKilled，每死一隻就 +1。
    /// filter 有填的話，只算那一種敵人（例如「擊殺 Boss 數」）。
    /// </summary>
    public class KillCounter : MonoBehaviour
    {
        [SerializeField] private TMP_Text label;
        [SerializeField] private string prefix = "Kills: ";
        [Tooltip("留空 = 每種敵人都算；有填 = 只算這種敵人")]
        [SerializeField] private EnemyData filter;

        private int count;

        private void OnEnable()
        {
            GameEvents.EnemyKilled += HandleEnemyKilled;
            Refresh();
        }

        private void OnDisable()
        {
            GameEvents.EnemyKilled -= HandleEnemyKilled;
        }

        private void HandleEnemyKilled(Vector3 position, EnemyData data)
        {
            if (filter != null && data != filter)
                return;

            count++;
            Refresh();
        }

        private void Refresh()
        {
            if (label != null)
                label.text = prefix + count;
        }
    }
}
