using System;

namespace AlastairLundy.Resyslib.Numbers.Converters
{
    public interface ISingleConverter
    {
        Single ToSingle(sbyte value);
        Single ToSingle(byte value);
        
        Single ToSingle(ushort value);
        Single ToSingle(short value);
        
        Single ToSingle(long value);
        Single ToSingle(ulong value);

        Single ToSingle(int value);
        Single ToSingle(uint value);

        Single ToSingle(double value);
        Single ToSingle(decimal value);
        
#if NET7_0_OR_GREATER
        Single ToSingle(Int128 value);
        Single ToSingle(UInt128 value);
#endif
    }
}