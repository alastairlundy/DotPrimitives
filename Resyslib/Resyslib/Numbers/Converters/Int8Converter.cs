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
    public interface IInt8Converter
    {
        sbyte ToInt8(byte value);
        
        sbyte ToInt8(ushort value);
        
        sbyte ToInt8(long value);
        sbyte ToInt8(ulong value);

        sbyte ToInt8(int value);
        sbyte ToInt8(uint value);

        sbyte ToInt8(float value);
        sbyte ToInt8(double value);
        sbyte ToInt8(decimal value);
        
#if NET7_0_OR_GREATER
        sbyte ToInt8(Int128 value);
        sbyte ToInt8(UInt128 value);
        
        byte ToUInt8(Int128 value);
        byte ToUInt8(UInt128 value);
#endif
    
        byte ToUInt8(uint value);
        byte ToUInt8(int value);
        
        byte ToUInt8(short value);
        byte ToUInt8(ushort value);
    
        byte ToUInt8(sbyte value);

        byte ToUInt8(long value);
        byte ToUInt8(ulong value);

        byte ToUInt8(float value);
        byte ToUInt8(double value);
        byte ToUInt8(decimal value);
    }
}