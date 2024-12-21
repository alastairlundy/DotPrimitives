using System;
using System.Numerics;

namespace Resyslib.Drawing.Abstractions;

public interface ISize : IEquatable<ISize>
{
    int Height { get; set; }
    int Width { get; set; }
    
    bool IsEmpty { get; }

    ISize Add(ISize size1, ISize size2);
    ISize Ceiling(ISizeF size);
    
    string ToString();
    Vector2 ToVector2();
    
    int GetHashCode();
}