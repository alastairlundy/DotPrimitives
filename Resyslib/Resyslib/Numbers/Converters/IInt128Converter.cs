/*
        MIT License
       
       Copyright (c) 2025 Alastair Lundy
       
       Permission is hereby granted, free of charge, to any person obtaining a copy
       of this software and associated documentation files (the "Software"), to deal
       in the Software without restriction, including without limitation the rights
       to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
       copies of the Software, and to permit persons to whom the Software is
       furnished to do so, subject to the following conditions:
       
       The above copyright notice and this permission notice shall be included in all
       copies or substantial portions of the Software.
       
       THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
       IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
       FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
       AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
       LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
       OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
       SOFTWARE.
   */

using System;

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