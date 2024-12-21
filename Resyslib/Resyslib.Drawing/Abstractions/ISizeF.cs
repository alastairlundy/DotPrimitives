using System;
using System.Numerics;

namespace Resyslib.Drawing.Abstractions;

public interface ISizeF : IEquatable<ISizeF>
{
    float Height { get; set; }
    float Width { get; set; }
    
    bool IsEmpty { get; }
    
    ISizeF Add(ISizeF size1, ISizeF size2);
    
    new bool Equals(ISizeF other);
    new bool Equals(object other);
    
    ISizeF Subtract(ISizeF size1, ISizeF size2);

    ISize ToSize();
    new string ToString();
    Vector2 ToVector2();
}