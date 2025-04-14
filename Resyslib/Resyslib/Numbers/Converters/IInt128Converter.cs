/*
    Resyslib
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */


#if NET7_0_OR_GREATER
using System;
#endif

namespace AlastairLundy.Resyslib.Numbers.Converters
{
#if NET7_0_OR_GREATER

    public interface IInt128Converter
    {
        Int128 ToInt128(sbyte value);
        Int128 ToInt128(byte value);
        Int128 ToInt128(short value);
        Int128 ToInt128(ushort value);

        Int128 ToInt128(int value);
        Int128 ToInt128(uint value);

        Int128 ToInt128(float value);
        Int128 ToInt128(double value);
        Int128 ToInt128(decimal value);
        
        Int128 ToInt128(UInt128 value);
        
        UInt128 ToUInt128(Int128 value);
    
        UInt128 ToUInt128(uint value);
        UInt128 ToUInt128(int value);
    
        UInt128 ToUInt128(sbyte value);
        UInt128 ToUInt128(byte value);
        UInt128 ToUInt128(short value);
        UInt128 ToUInt128(ushort value);

        UInt128 ToUInt128(long value);
        UInt128 ToUInt128(ulong value);

        UInt128 ToUInt128(float value);
        UInt128 ToUInt128(double value);
        UInt128 ToUInt128(decimal value);
    }
#endif
}