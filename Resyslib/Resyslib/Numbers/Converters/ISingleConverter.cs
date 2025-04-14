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