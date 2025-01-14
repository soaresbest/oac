using System.Runtime.InteropServices;

namespace OpenAutoClicker.Mouse
{
    public static class MouseCommand
    {
        [DllImport("user32.dll", EntryPoint = "GetCursorPos")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool ImportedGetCursorPos(out MousePoint lpMousePoint);

        [DllImport("user32.dll", EntryPoint = "mouse_event")]
        private static extern void ImportedMouseEvent(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        public static MousePoint GetCursorPosition()
        {
            MousePoint currentMousePoint;

            bool gotPoint = ImportedGetCursorPos(out currentMousePoint);

            if (!gotPoint)
            {
                currentMousePoint = new MousePoint(0, 0);
            }

            return currentMousePoint;
        }

        public static void LeftDown()
        {
            MousePoint position = GetCursorPosition();

            ImportedMouseEvent(
                (int)MouseEventFlags.LeftDown,
                position.X,
                position.Y,
                0,
                0
             );
        }

        public static void LeftUp()
        {
            MousePoint position = GetCursorPosition();

            ImportedMouseEvent(
                (int)MouseEventFlags.LeftUp,
                position.X,
                position.Y,
                0,
                0
             );
        }

        public static void MiddleDown()
        {
            MousePoint position = GetCursorPosition();

            ImportedMouseEvent(
                (int)MouseEventFlags.MiddleDown,
                position.X,
                position.Y,
                0,
                0
             );
        }

        public static void MiddleUp()
        {
            MousePoint position = GetCursorPosition();

            ImportedMouseEvent(
                (int)MouseEventFlags.MiddleUp,
                position.X,
                position.Y,
                0,
                0
             );
        }

        public static void RightDown()
        {
            MousePoint position = GetCursorPosition();

            ImportedMouseEvent(
                (int)MouseEventFlags.RightDown,
                position.X,
                position.Y,
                0,
                0
             );
        }

        public static void RightUp()
        {
            MousePoint position = GetCursorPosition();

            ImportedMouseEvent(
                (int)MouseEventFlags.RightUp,
                position.X,
                position.Y,
                0,
                0
             );
        }

        public static void Down(bool rightButton)
        {
            MousePoint position = GetCursorPosition();

            ImportedMouseEvent(
                rightButton ? (int)MouseEventFlags.RightDown : (int)MouseEventFlags.LeftDown,
                position.X,
                position.Y,
                0,
                0
             );
        }

        public static void Up(bool rightButton)
        {
            MousePoint position = GetCursorPosition();

            ImportedMouseEvent(
                rightButton ? (int)MouseEventFlags.RightUp : (int)MouseEventFlags.LeftUp,
                position.X,
                position.Y,
                0,
                0
             );
        }

        public static void Absolute(int x, int y)
        {
            ImportedMouseEvent(
                (int)MouseEventFlags.Absolute,
                x,
                y,
                0,
                0
             );
        }

        public static void WheelUp()
        {
            MousePoint position = GetCursorPosition();

            ImportedMouseEvent(
                (int)MouseEventFlags.Wheel,
                position.X,
                position.Y,
                120,
                0
             );
        }

        public static void WheelDown()
        {
            MousePoint position = GetCursorPosition();

            ImportedMouseEvent(
                (int)MouseEventFlags.Wheel,
                position.X,
                position.Y,
                -120,
                0
             );
        }

        public static void MoveX(int amout)
        {
            ImportedMouseEvent(
                (int)MouseEventFlags.Move,
                amout,
                0,
                0,
                0
             );
        }

        public static void MoveY(int amout)
        {
            ImportedMouseEvent(
                (int)MouseEventFlags.Move,
                0,
                amout,
                0,
                0
             );
        }
    }
}
