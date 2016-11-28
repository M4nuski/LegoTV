using System;
using System.Windows.Forms;

namespace SPI_FLASH
{
    static class GlobalProperties
    {
        //Interface config
        public static uint baudRate = 115000;
        public static byte portDirectionMask = 253;//253 = 0xFD = b'11111101' 0000 0010


    }
}
