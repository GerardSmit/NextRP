using System.Drawing;
using System.Runtime.InteropServices;

namespace NextFramework.Data
{
    [StructLayout(LayoutKind.Explicit, Size = 4)]
    public struct ColorRgba
    {
        [FieldOffset(0)]
        public readonly uint NumberValue;

        public ColorRgba(byte red, byte green, byte blue, byte alpha = 255)
        {
            NumberValue = (uint)((alpha << 24) + (blue << 16) + (green << 8) + red);
        }

        public byte GetRed()
        {
            return (byte)(NumberValue & 0xFF);
        }

        public byte GetGreen()
        {
            return (byte)((NumberValue >> 8) & 0xFF);
        }

        public byte GetBlue()
        {
            return (byte)((NumberValue >> 16) & 0xFF);
        }

        public byte GetAlpha()
        {
            return (byte)((NumberValue >> 24) & 0xFF);
        }

        public static implicit operator Color(ColorRgba v)
        {
            return Color.FromArgb(v.GetAlpha(), v.GetRed(), v.GetGreen(), v.GetBlue());
        }

        public static implicit operator ColorRgba(Color v)
        {
            return new ColorRgba(v.R, v.G, v.B, v.A);
        }
    }
}
