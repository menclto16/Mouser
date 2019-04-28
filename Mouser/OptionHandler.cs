using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Mouser
{
    class OptionHandler
    {
        public const UInt32 SPI_GETMOUSE = 0x0003;
        public const UInt32 SPI_SETMOUSE = 0x0004;
        public const UInt32 SPI_SETDOUBLECLICKTIME = 0x0020;
        public const UInt32 SPI_GETMOUSESPEED = 0x0070;
        public const UInt32 SPI_SETMOUSESPEED = 0x0071;
        public const UInt32 SPI_GETWHEELSCROLLLINES = 0x0068;
        public const UInt32 SPI_SETWHEELSCROLLLINES = 0x0069;

        [DllImport("User32.dll")]
        static extern Boolean SystemParametersInfo(UInt32 uiAction, UInt32 uiParam, IntPtr pvParam, UInt32 fWinIni);
        [DllImport("user32.dll")]
        static extern uint GetDoubleClickTime();

        public void ChangeMouseSensitivity(double value)
        {
            IntPtr ptr = new IntPtr(Convert.ToUInt32(value));
            SystemParametersInfo(SPI_SETMOUSESPEED, 0, ptr, 0);
        }
        public void ChangeMousePrecision(bool b)
        {
            int[] mouseParams = new int[3];
            SystemParametersInfo(SPI_GETMOUSE, 0, GCHandle.Alloc(mouseParams, GCHandleType.Pinned).AddrOfPinnedObject(), 0);
            mouseParams[2] = b ? 1 : 0;
            SystemParametersInfo(SPI_SETMOUSE, 0, GCHandle.Alloc(mouseParams, GCHandleType.Pinned).AddrOfPinnedObject(), 0);
        }
        public void ChangeMouseClickSpeed(double value)
        {
            double clickSpeed = 5000 - value;
            SystemParametersInfo(SPI_SETDOUBLECLICKTIME, Convert.ToUInt32(clickSpeed), new IntPtr(0), 0);
        }
        public void ChangeScrollSpeed(double value)
        {

            SystemParametersInfo(SPI_SETWHEELSCROLLLINES, Convert.ToUInt32(value), new IntPtr(0), 0);
        }
        public int GetSensitivity()
        {
            int sensitivity = 0;
            IntPtr ptr;
            ptr = Marshal.AllocCoTaskMem(4);
            SystemParametersInfo(SPI_GETMOUSESPEED, 0, ptr, 0);
            sensitivity = Marshal.ReadInt32(ptr);
            Marshal.FreeCoTaskMem(ptr);

            return sensitivity;
        }
        public bool GetMousePrecision()
        {
            int[] mouseParams = new int[3];
            SystemParametersInfo(SPI_GETMOUSE, 0, GCHandle.Alloc(mouseParams, GCHandleType.Pinned).AddrOfPinnedObject(), 0);
            bool precisionBool = mouseParams[2] == 1 ? true : false;
            return precisionBool;
        }
        public int GetDoubleClick()
        {
            uint doubleClickTime = GetDoubleClickTime();
            return Convert.ToInt32(doubleClickTime);
        }
        public int GetScrollSpeed()
        {
            int scrollSpeed = 0;
            IntPtr ptr;
            ptr = Marshal.AllocCoTaskMem(4);
            SystemParametersInfo(SPI_GETWHEELSCROLLLINES, 0, ptr, 0);
            scrollSpeed = Marshal.ReadInt32(ptr);
            Marshal.FreeCoTaskMem(ptr);

            return scrollSpeed;
        }
    }
}
