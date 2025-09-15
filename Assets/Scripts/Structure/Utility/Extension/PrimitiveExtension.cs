using System.Runtime.CompilerServices;

namespace Structure.Utility.Extension;

public static class PrimitiveExtension
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZero(this int self)
    {
        return self == 0;
    }
}