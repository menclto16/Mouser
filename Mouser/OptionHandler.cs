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
        public const UInt32 SPI_SETMOUSESPEED = 0x0071;
        public const UInt32 SPI_SETDOUBLECLICKTIME = 0x0020;
        public const UInt32 SPI_SETWHEELSCROLLLINES = 0x0069;

        [DllImport("User32.dll")]
        static extern Boolean SystemParametersInfo(UInt32 uiAction, UInt32 uiParam, UInt32 pvParam, UInt32 fWinIni);

        public void ChangeMouseSensitivity(uint value)
        {
            SystemParametersInfo(SPI_SETMOUSESPEED, 0, value, 0);
        }
        public void ChangeMouseClickSpeed(uint value)
        {
            uint clickSpeed = 5000 - value;
            SystemParametersInfo(SPI_SETDOUBLECLICKTIME, clickSpeed, 0, 0);
        }
        public void ChangeScrollSpeed(uint value)
        {
            SystemParametersInfo(SPI_SETWHEELSCROLLLINES, value, 0, 0);
        }
    }
}
