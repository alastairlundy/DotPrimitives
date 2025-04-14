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
    public interface IDoubleConverter
    {
        double ToDouble(sbyte value);
        double ToDouble(byte value);
        
        double ToDouble(ushort value);
        double ToDouble(short value);
        
        double ToDouble(long value);
        double ToDouble(ulong value);

        double ToDouble(int value);
        double ToDouble(uint value);

        double ToDouble(float value);
        double ToDouble(decimal value);
        
#if NET7_0_OR_GREATER
    double ToDouble(Int128 value);
    double ToDouble(UInt128 value);
#endif
    }
}