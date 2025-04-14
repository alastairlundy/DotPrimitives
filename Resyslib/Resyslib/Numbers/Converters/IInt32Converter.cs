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
    public interface IInt32Converter
    {
        int ToInt32(sbyte value);
        int ToInt32(byte value);
        int ToInt32(short value);
        int ToInt32(ushort value);

        int ToInt32(long value);
        int ToInt32(ulong value);

        int ToInt32(float value);
        int ToInt32(double value);
        int ToInt32(decimal value);
        
#if NET7_0_OR_GREATER
        int ToInt32(Int128 value);
        int ToInt32(UInt128 value);
        
        uint ToUInt32(Int128 value);
        uint ToUInt32(UInt128 value);
#endif
    
        int ToInt32(uint value);
        uint ToUInt32(int value);
    
        uint ToUInt32(sbyte value);
        uint ToUInt32(byte value);
        uint ToUInt32(short value);
        uint ToUInt32(ushort value);

        uint ToUInt32(long value);
        uint ToUInt32(ulong value);

        uint ToUInt32(float value);
        uint ToUInt32(double value);
        uint ToUInt32(decimal value);
    }
}