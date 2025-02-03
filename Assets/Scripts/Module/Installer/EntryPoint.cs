using System;

namespace Module.Installer
{
    // `vcontainer`と一緒
    public interface ITickable
    {
        public void Tick(float deltaTime);
    }

    public interface IFixedTickable
    {
        public void FixedTick(float deltaTime);
    }
    
    
    /// <summary>
    /// FIXME: 書いてみたはいいけど思ったほどきれいにはならなかった
    /// </summary>
    [Flags]
    public enum EntryPointFlag
    {
        Disposable = 1,
        Tickable = 1 << 1,
        FixedTickable = 1 << 2,
    }

    public static class InstallerExtension
    {
        public static EntryPointFlag MakeFlag<T>(T obj)
        {
            EntryPointFlag flag = 0;

            if (obj is IDisposable)
            {
                flag.Set(EntryPointFlag.Disposable);
            }

            if (obj is ITickable)
            {
                flag.Set(EntryPointFlag.Tickable);
            }

            if (obj is IFixedTickable)
            {
                flag.Set(EntryPointFlag.FixedTickable);
            }

            return flag;
        }

        private static void Set(this ref EntryPointFlag flag, EntryPointFlag setFlag)
        {
            flag |= setFlag;
        }
    }
}