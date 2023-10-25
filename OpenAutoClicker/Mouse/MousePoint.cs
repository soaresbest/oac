using System.Runtime.InteropServices;

namespace OpenAutoClicker.Mouse
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MousePoint
    {
        public int X;
        public int Y;

        public MousePoint(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static bool operator ==(MousePoint a, MousePoint b)
        {
            return a.X == b.X && a.Y == b.Y;
        }

        public static bool operator !=(MousePoint a, MousePoint b)
        {
            return !(a == b);
        }
    }
}
