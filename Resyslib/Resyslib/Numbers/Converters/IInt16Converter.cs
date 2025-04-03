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