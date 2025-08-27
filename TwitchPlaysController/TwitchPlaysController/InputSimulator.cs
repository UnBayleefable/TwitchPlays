using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TwitchPlaysController;
public static class InputSimulator
{
    [StructLayout(LayoutKind.Sequential)]
    struct INPUT
    {
        public uint type;
        public InputUnion U;
    }

    [StructLayout(LayoutKind.Explicit)]
    struct InputUnion
    {
        [FieldOffset(0)] public MOUSEINPUT mi;
        [FieldOffset(0)] public KEYBDINPUT ki;
    }

    [StructLayout(LayoutKind.Sequential)]
    struct MOUSEINPUT
    {
        public int dx;
        public int dy;
        public uint mouseData;
        public uint dwFlags;
        public uint time;
        public IntPtr dwExtraInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    struct KEYBDINPUT
    {
        public ushort wVk;
        public ushort wScan;
        public uint dwFlags;
        public uint time;
        public IntPtr dwExtraInfo;
    }

    [DllImport("user32.dll", SetLastError = true)]
    private static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

    private const uint INPUT_MOUSE = 0;
    private const uint INPUT_KEYBOARD = 1;

    private const uint KEYEVENTF_KEYUP = 0x0002;
    private const uint KEYEVENTF_SCANCODE = 0x0008;

    private const uint MOUSEEVENTF_MOVE = 0x0001;
    private const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
    private const uint MOUSEEVENTF_LEFTUP = 0x0004;
    private const uint MOUSEEVENTF_RIGHTDOWN = 0x0008;
    private const uint MOUSEEVENTF_RIGHTUP = 0x0010;

    // ---------------- Keyboard ----------------
    public static void HoldKey(ushort scanCode)
    {
        INPUT[] inputs = new INPUT[1];
        inputs[0].type = INPUT_KEYBOARD;
        inputs[0].U.ki.wVk = 0;
        inputs[0].U.ki.wScan = scanCode;
        inputs[0].U.ki.dwFlags = KEYEVENTF_SCANCODE;
        SendInput(1, inputs, Marshal.SizeOf(typeof(INPUT)));
    }

    public static void ReleaseKey(ushort scanCode)
    {
        INPUT[] inputs = new INPUT[1];
        inputs[0].type = INPUT_KEYBOARD;
        inputs[0].U.ki.wVk = 0;
        inputs[0].U.ki.wScan = scanCode;
        inputs[0].U.ki.dwFlags = KEYEVENTF_SCANCODE | KEYEVENTF_KEYUP;
        SendInput(1, inputs, Marshal.SizeOf(typeof(INPUT)));
    }

    public static void HoldAndReleaseKey(ushort scanCode, int ms)
    {
        HoldKey(scanCode);
        Thread.Sleep(ms);
        ReleaseKey(scanCode);
    }

    // ---------------- Mouse ----------------
    public static void MouseLeftDown()
    {
        SendMouseEvent(MOUSEEVENTF_LEFTDOWN);
    }

    public static void MouseLeftUp()
    {
        SendMouseEvent(MOUSEEVENTF_LEFTUP);
    }

    public static void MouseRightDown()
    {
        SendMouseEvent(MOUSEEVENTF_RIGHTDOWN);
    }

    public static void MouseRightUp()
    {
        SendMouseEvent(MOUSEEVENTF_RIGHTUP);
    }

    public static void MoveMouse(int dx, int dy)
    {
        INPUT[] inputs = new INPUT[1];
        inputs[0].type = INPUT_MOUSE;
        inputs[0].U.mi.dx = dx;
        inputs[0].U.mi.dy = dy;
        inputs[0].U.mi.mouseData = 0;
        inputs[0].U.mi.dwFlags = MOUSEEVENTF_MOVE;
        SendInput(1, inputs, Marshal.SizeOf(typeof(INPUT)));
    }

    private static void SendMouseEvent(uint flag)
    {
        INPUT[] inputs = new INPUT[1];
        inputs[0].type = INPUT_MOUSE;
        inputs[0].U.mi.dx = 0;
        inputs[0].U.mi.dy = 0;
        inputs[0].U.mi.mouseData = 0;
        inputs[0].U.mi.dwFlags = flag;
        SendInput(1, inputs, Marshal.SizeOf(typeof(INPUT)));
    }
}
