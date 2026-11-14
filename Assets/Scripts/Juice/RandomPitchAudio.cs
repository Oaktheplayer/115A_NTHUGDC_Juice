using UnityEngine;

namespace Juice
{
    /// <summary>
    /// 包一層 AudioSource：每次播放隨機挑一個音效、隨機改一點音高，同一個聲音重複播才不會膩。
    /// Play() 是 public、沒有參數，所以可以直接在 UnityEvent 裡拖。
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class RandomPitchAudio : MonoBehaviour
    {
        [SerializeField] private AudioClip[] clips;
        [SerializeField] private Vector2 pitchRange = new(0.9f, 1.1f);
#pragma warning disable CS0414 // 做完 JUICE-3 之前 volume 還沒被用到，先關掉這個警告
        [SerializeField, Range(0f, 1f)] private float volume = 1f;
#pragma warning restore CS0414

        private AudioSource source;

        private void Awake()
        {
            source = GetComponent<AudioSource>();
        }

        public void Play()
        {
            AudioClip clip = clips != null && clips.Length > 0
                ? clips[Random.Range(0, clips.Length)]
                : source.clip;
            if (clip == null)
                return;

            source.pitch = Random.Range(pitchRange.x, pitchRange.y);
            source.PlayOneShot(clip, volume);
        }
    }
}
