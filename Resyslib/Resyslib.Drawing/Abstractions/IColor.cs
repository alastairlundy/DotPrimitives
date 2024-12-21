using System;

namespace Resyslib.Drawing.Abstractions;

public interface IColor : IEquatable<IColor>
{
    public byte Red { get; }
    public byte Green { get; }
    public byte Blue { get; }
    public byte Alpha { get; }
    
    public new bool Equals(object other);
    public new bool Equals(IColor other);
    
    public float GetBrightness();
    public float GetHue();
    public float GetSaturation();
    
    public int ToArgb();
    public string ToString();
    public Resyslib.Drawing.KnownColor ToKnownColor();
}