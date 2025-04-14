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
    public interface IInt64Converter
    {
        long ToInt64(sbyte value);
        long ToInt64(byte value);
        long ToInt64(short value);
        long ToInt64(ushort value);

        long ToInt64(int value);
        long ToInt64(uint value);

        long ToInt64(float value);
        long ToInt64(double value);
        long ToInt64(decimal value);
        
#if NET7_0_OR_GREATER
        long ToInt64(Int128 value);
        long ToInt64(UInt128 value);
        
        ulong ToUInt64(Int128 value);
        ulong ToUInt64(UInt128 value);
#endif
    
        ulong ToUInt64(uint value);
        ulong ToUInt64(int value);
    
        ulong ToUInt64(sbyte value);
        ulong ToUInt64(byte value);
        ulong ToUInt64(short value);
        ulong ToUInt64(ushort value);

        ulong ToUInt64(long value);
        ulong ToUInt64(ulong value);

        ulong ToUInt64(float value);
        ulong ToUInt64(double value);
        ulong ToUInt64(decimal value);
    }
}