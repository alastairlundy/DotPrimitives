using System;

namespace Resyslib.Drawing.Abstractions;

public interface IPoint : IEquatable<IPoint>
{
    int X { get; }
    int Y { get; }
    
    IPoint Add(IPoint point, ISize size);
    IPoint Ceiling(IPointF point);
    
    new bool Equals(IPoint other);
    bool Equals(object? other);
    
    int GetHashCode();
    
    void Offset(int dx, int dy);
    void Offset(IPoint point);
    
    IPoint Round(IPointF point);
    IPoint Subtract(IPoint point, ISize size);

    new string ToString();
    
    IPoint Truncate(IPointF point);
}