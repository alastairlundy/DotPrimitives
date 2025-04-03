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