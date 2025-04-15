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
    public interface IDecimalConverter
    {
        decimal ToDecimal(sbyte value);
        decimal ToDecimal(byte value);
        
        decimal ToDecimal(ushort value);
        decimal ToDecimal(short value);
        
        decimal ToDecimal(long value);
        decimal ToDecimal(ulong value);

        decimal ToDecimal(int value);
        decimal ToDecimal(uint value);

        decimal ToDecimal(float value);
        decimal ToDecimal(decimal value);
        
#if NET7_0_OR_GREATER
        decimal ToDecimal(Int128 value);
        decimal ToDecimal(UInt128 value);
#endif
    }
}