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
        [SerializeField, Range(0f, 1f)] private float volume = 1f;

        private AudioSource source;

        private void Awake()
        {
            source = GetComponent<AudioSource>();
        }

        public void Play()
        {
            // TODO [JUICE-3] 隨機挑一個音效、隨機音高，播出來。
            // 提示：
            // 1. 從 clips 隨機挑一個：Random.Range(0, clips.Length)（int 版本不含上限）
            //    clips 是空的話，就用 source.clip
            // 2. source.pitch = Random.Range(pitchRange.x, pitchRange.y);
            // 3. 用 source.PlayOneShot(clip, volume) 播放。為什麼不用 source.Play()？
            //    （想一想：連續被打兩下時，Play() 會怎樣？）
        }
    }
}
