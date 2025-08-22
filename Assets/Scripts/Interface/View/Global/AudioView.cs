using UnityEngine;

namespace Interface.Global.Advertisement;

public interface IBgmSourceView : IPausableView
{
    public void Play(AudioClip clip);
}

public interface ISeSourceView : IPausableView
{
    public void Play(AudioClip clip);
}

public interface IUiSourceView : IPausableView
{
    public void Play(AudioClip clip);
}

public interface IPausableView
{
    public void Stop();
    public void Continue();
}