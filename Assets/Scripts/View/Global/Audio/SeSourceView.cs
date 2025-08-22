using Interface.Global.Advertisement;
using UnityEngine;

namespace View.Global.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class SeSourceView : MonoBehaviour, ISeSourceView
    {
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.playOnAwake = false;
        }

        public void Play(AudioClip clip)
        {
            _audioSource.PlayOneShot(clip);
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