using System;
using System.Collections.Generic;
using System.Numerics;

namespace Resyslib.Drawing.Abstractions;

public interface IPointF : IEquatable<IPointF>
{
    IPointF Empty { get; }
    
    float X { get; }
    float Y { get; }
    
    bool IsEmpty { get; }
    
    IPointF Add(IPointF point, ISize size);
    IPointF Add(IPointF point, ISizeF size);
    
    IPointF Subtract(IPointF point, ISize size);
    IPointF Subtract(IPointF point, ISizeF size);

    string ToString();
    Vector2 ToVector2();
    
    int GetHashCode();
    new bool Equals(IPointF? other);
    new bool Equals(object? other);
}