using System.Windows.Forms;

namespace SPI_FLASH
{
    internal static class SignalGenerator
    {
        // SPI version directly to FTDI UM245R
        //Interface
        public static byte[] OutputBytes = new byte[3200];
        public static int OutputLength;
        public static byte[] InputBytes = new byte[3200];

        private const int SPI_CS_bit = 0;
        private const bool SPI_CS_default = true;

        public const int SPI_MISO_bit = 1; // Input
        public const int SPI_MOSI_bit = 2; // Output

        private const int SPI_SCK_bit = 3; // latch on clk up
        private const bool SPI_SCK_default = true;

        public const int SPI_HOLD_bit = 4; // Hold
        private const bool SPI_HOLD_default = true;
        public const int SPI_WRITE_bit = 5; // WriteProtect
        private const bool SPI_WRITE_default = true;

        private static int _buffer_index;

        public static bool GetBit(byte data, int bit)
        {
            return ((data & (1 << bit)) > 0);
        }

        public static int SetBit(int input, int bitToSet, bool set)
        {
          //  return (input | (int) (set ? Powers[bitToSet] : 0));
            return (set) ? (input | (1 << bitToSet)) : (input & ~(1 << bitToSet));
        }

        public static byte SetBit(byte input, int bitToSet, bool set)
        {
           // return (byte) (input | (set ? Powers[bitToSet] : 0));
            return (set) ? (byte)(input | (1 << bitToSet)) : (byte)(input & ~(1 << bitToSet));
        }



        public static int Serialize(ref byte[] buffer)
        {
            //reset index
            _buffer_index = 0;

            //dummy bit to set CS low
            buffer[_buffer_index] = genByte(!SPI_CS_default, false, SPI_SCK_default, SPI_HOLD_default, SPI_WRITE_default);

            for (var d = 0; d < OutputLength; d++)
            {
                
                for (var i = 7; i >=0; i--)
                {
                    buffer[_buffer_index] = genByte(!SPI_CS_default, GetBit(OutputBytes[d], i), !SPI_SCK_default, SPI_HOLD_default, SPI_WRITE_default);
                    buffer[_buffer_index] = genByte(!SPI_CS_default, GetBit(OutputBytes[d], i), SPI_SCK_default, SPI_HOLD_default, SPI_WRITE_default);
                }

            } 

            //dummy bit to read last bit and set CS high
            buffer[_buffer_index] = genByte(SPI_CS_default, false, SPI_SCK_default, SPI_HOLD_default, SPI_WRITE_default);

            return _buffer_index;
        }

        public static void Deserialize(byte[] buffer)
        {

            //clocked out on falling edge
            var bit_index = 2;
            for (var d = 0; d < OutputLength; d++)
            {
                //InputBytes[d] = 0;
                for (var i = 7; i >= 0; i--)
                {
                    InputBytes[d] = SetBit(InputBytes[d], i, (buffer[bit_index] & (1 << SPI_MISO_bit)) > 0);
                    bit_index += 2;
                }
            }
        }

        public static byte genByte(bool cs, bool data, bool clock, bool hold, bool wp)
        {
            byte result = 0;

            result = SetBit(result, SPI_CS_bit, cs);
            result = SetBit(result, SPI_MOSI_bit, data);
            result = SetBit(result, SPI_SCK_bit, clock);
            result = SetBit(result, SPI_HOLD_bit, hold);
            result = SetBit(result, SPI_WRITE_bit, wp);

            _buffer_index++;

            return result;
        }

    }
}
