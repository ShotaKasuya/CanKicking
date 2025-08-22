using Interface.Global.Advertisement;
using UnityEngine;

namespace View.Global.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class BgmSourceView : MonoBehaviour, IBgmSourceView
    {
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.loop = true; // BGM用にループ有効化
            _audioSource.playOnAwake = false;
        }

        public void Play(AudioClip clip)
        {
            if (_audioSource.isPlaying)
            {
                _audioSource.Stop();
            }

            _audioSource.clip = clip;
            _audioSource.Play();
        }

        public void Stop()
        {
            if (_audioSource.isPlaying)
            {
                _audioSource.Pause();
            }
        }

        public void Continue()
        {
            if (_audioSource.clip != null && !_audioSource.isPlaying)
            {
                _audioSource.UnPause();
            }
        }
    }
}