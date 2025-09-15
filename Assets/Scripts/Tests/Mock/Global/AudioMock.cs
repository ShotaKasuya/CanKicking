using Interface.View.Global;
using UnityEngine;

namespace Tests.Mock.Global
{
    public class MockSeSourceView : ISeSourceView
    {
        public AudioClip PlayedClip { get; private set; }
        public void Play(AudioClip clip) => PlayedClip = clip;

        public void Stop()
        {
        }

        public void Continue()
        {
        }
    }
}