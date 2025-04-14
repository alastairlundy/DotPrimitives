/*
    Resyslib
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */


using System;

namespace AlastairLundy.Resyslib.Numbers.Converters
{
    public interface IInt16Converter
    {
        short ToInt16(sbyte value);
        short ToInt16(byte value);
        
        short ToInt16(long value);
        short ToInt16(ulong value);

        short ToInt16(int value);
        short ToInt16(uint value);

        short ToInt16(float value);
        short ToInt16(double value);
        short ToInt16(decimal value);
        
#if NET7_0_OR_GREATER
        short ToInt16(Int128 value);
        short ToInt16(UInt128 value);
        
        ushort ToUInt16(Int128 value);
        ushort ToUInt16(UInt128 value);
#endif
    
        ushort ToUInt16(uint value);
        ushort ToUInt16(int value);
    
        ushort ToUInt16(sbyte value);
        ushort ToUInt16(byte value);

        ushort ToUInt16(long value);
        ushort ToUInt16(ulong value);

        ushort ToUInt16(float value);
        ushort ToUInt16(double value);
        ushort ToUInt16(decimal value);
    }
}