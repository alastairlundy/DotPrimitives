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