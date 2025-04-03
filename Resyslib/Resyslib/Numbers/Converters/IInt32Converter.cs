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