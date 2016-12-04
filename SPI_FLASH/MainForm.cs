using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
// ReSharper disable UnusedMember.Local

namespace SPI_FLASH
{
    public partial class MainForm : Form
    {

        private const byte SPI_read = 0x03; //Aa3 Du0 Da1-inf
        private const byte SPI_HS_read = 0x0B; //Aa3 Du1 Da0
        private const byte SPI_4K_sector_erease = 0x20; //Aa3 Du0 Da0
        private const byte SPI_32K_block_erease = 0x52; //Aa3 Du0 Da0
        private const byte SPI_64K_block_erease = 0xD8; //Aa3 Du0 Da0
        private const byte SPI_chip_erease = 0x60; //Aa3 Du0 Da0
        private const byte SPI_byte_program = 0x02; //Aa3 Du0 Da1
        private const byte SPI_AAI_program = 0xAD; //Aa3 Du0 Da2-inf

        private const byte SPI_read_SSR = 0x05; //Aa0 Du0 Da1-inf
        private const byte SPI_read_SSR2 = 0x35; //Aa0 Du0 Da1-inf
        private const byte SPI_read_SSR3 = 0x15; //Aa0 Du0 Da1-inf

        private const byte SPI_enable_write_SSR = 0x50; //Aa0 Du0 Da0
        private const byte SPI_write_SSR = 0x01; //Aa0 Du0 Da1

        private const byte SPI_write_SSR2 = 0x31; //Aa0 Du0 Da1
        private const byte SPI_write_SSR3 = 0x11; //Aa0 Du0 Da1

        private const byte SPI_write_enable = 0x06; //Aa0 Du0 Da0
        private const byte SPI_write_disable = 0x04; //Aa0 Du0 Da0
        private const byte SPI_read_ID = 0x90; //Aa3 Du0 Da1-inf
        private const byte SPI_read_JEDEC = 0x9F; //Aa0 Du0 Da1-inf
        private const byte SPI_busy_on_MISO = 0x70; //AAI //Aa0 Du0 Da0
        private const byte SPI_data_on_MISO = 0x80; //Aa0 Du0 Da0

        private const byte SPI_QPI_enable = 0x38; //Aa0 Du0 Da0
        private const byte SPI_reset_enable = 0x66; //Aa0 Du0 Da0
        private const byte SPI_reset_device = 0x99; //Aa0 Du0 Da0

        private const string JEDEC_ID_W25Q128FV_SPI = "EF4018";
        private const string JEDEC_ID_W25Q128FV_QPI = "EF6018";
        private const string JEDEC_ID_SST25VF016B = "BF2541";

        private const int SRR1_bit_Busy = 0;
        private const int SRR1_bit_WriteEnabled = 1;
        private const int SRR1_bit_BP0 = 2;
        private const int SRR1_bit_BP1 = 3;
        private const int SRR1_bit_BP2 = 4;
        private const int SRR1_bit_TP = 5;
        private const int SRR1_bit_SEC = 6;
        private const int SRR1_bit_SRP0 = 7;

        private USB_Control usb = new USB_Control();

        public struct rawTFTDTA //to load and flash TFT data for lego block
        {
            public byte COLMOD; // 444:33h 666:66h : set COLMOD
            public byte MADCTL; //set MADCTL
            public byte num_images; // 0-255
            public ushort bytes_per_image; // 444:7800h 666:F000h
            public byte[][] data; // new byte[num_images][bytes_per_image] - sector alignment to be done on transfer
        }

        public MainForm()
        {
            InitializeComponent();
            ExtLog.bx = log_textbox;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ExtLog.AddLine("Loading Devices:");
            var dl = usb.GetDevicesList();
            if (dl.Count == 1)
            {
                usb.OpenDeviceByLocation(uint.Parse(dl[0].Substring(0, 4)));
                if (usb.IsOpen)
                {
                    usb.SetLatency(2);
                    usb.BitBang(SignalGenerator.genByte(true, true, true, true, true));
                    label_JEDEC_ID.Text = "Unkown Device";
                }
            }
            else
            {
                ExtLog.AddLine("Wrong number of devices found: " + dl.Count);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            setCommand(SPI_read_SSR, 0, 1);
            //SignalGenerator.OutputBytes[0] = SPI_read_SSR;
            //SignalGenerator.OutputBytes[1] = 0; //result byte;
            //SignalGenerator.OutputLength = 2;

            usb.Transfer();

            ExtLog.AddLine("SSR:" + formatSSR(SignalGenerator.InputBytes[1]));
        }

        private static string tobin(byte val)
        {
            return Convert.ToString(val, 2).PadLeft(8, '0');
        }

        private string formatSSR(byte val)
        {
            var sb = new StringBuilder();

            if (JEDEC_ID_textbox.Text == JEDEC_ID_SST25VF016B)
            {
                sb.Append(" BUSY:" + boolTo01(SignalGenerator.GetBit(val, SRR1_bit_Busy)));
                sb.Append(" WEL:" + boolTo01(SignalGenerator.GetBit(val, SRR1_bit_WriteEnabled)));
                sb.Append(" BP0:" + boolTo01(SignalGenerator.GetBit(val, SRR1_bit_BP0)));
                sb.Append(" BP1:" + boolTo01(SignalGenerator.GetBit(val, SRR1_bit_BP1)));

                sb.Append(" BP2:" + boolTo01(SignalGenerator.GetBit(val, SRR1_bit_BP2)));
                sb.Append(" BP3:" + boolTo01(SignalGenerator.GetBit(val, 5)));
                sb.Append(" AAI:" + boolTo01(SignalGenerator.GetBit(val, 6)));
                sb.Append(" BPL:" + boolTo01(SignalGenerator.GetBit(val, 7)));
            }
            else

            if (JEDEC_ID_textbox.Text == JEDEC_ID_W25Q128FV_SPI)
            {
                sb.Append(" BUSY:" + boolTo01(SignalGenerator.GetBit(val, SRR1_bit_Busy)));
                sb.Append(" WEL:" + boolTo01(SignalGenerator.GetBit(val, SRR1_bit_WriteEnabled)));
                sb.Append(" BP0:" + boolTo01(SignalGenerator.GetBit(val, SRR1_bit_BP0)));
                sb.Append(" BP1:" + boolTo01(SignalGenerator.GetBit(val, SRR1_bit_BP1)));

                sb.Append(" BP2:" + boolTo01(SignalGenerator.GetBit(val, SRR1_bit_BP2)));
                sb.Append(" TP:" + boolTo01(SignalGenerator.GetBit(val, SRR1_bit_TP)));
                sb.Append(" SEC:" + boolTo01(SignalGenerator.GetBit(val, SRR1_bit_SEC)));
                sb.Append(" SPR0:" + boolTo01(SignalGenerator.GetBit(val, SRR1_bit_SRP0)));
            }
            else if (JEDEC_ID_textbox.Text == JEDEC_ID_W25Q128FV_QPI)
            {
                sb.Append("W25Q128FV QPI Mode Not Implemented.");
            }
            else
            {
                sb.Append(tobin(val));
            }

            return sb.ToString();
        }

        private static string boolTo01(bool val)
        {
            return val ? "1" : "0";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            setCommand(SPI_read_JEDEC, 0, 3);
            usb.Transfer();
            ExtLog.AddLine("MFRID:" + tobin(SignalGenerator.InputBytes[1]) + " ID:" + tobin(SignalGenerator.InputBytes[2]) + " " + tobin(SignalGenerator.InputBytes[3]));
            JEDEC_ID_textbox.Text = SignalGenerator.InputBytes[1].ToString("X2") + SignalGenerator.InputBytes[2].ToString("X2") +
                            SignalGenerator.InputBytes[3].ToString("X2");

            if (JEDEC_ID_textbox.Text == JEDEC_ID_SST25VF016B) label_JEDEC_ID.Text = "SST25VF016B";
            if (JEDEC_ID_textbox.Text == JEDEC_ID_W25Q128FV_SPI) label_JEDEC_ID.Text = "W25Q128FV";
            if (JEDEC_ID_textbox.Text == JEDEC_ID_W25Q128FV_QPI) label_JEDEC_ID.Text = "W25Q128FV (QPI)";
        }

        private static void setCommand(byte command, byte A23A16, byte A15A08, byte A07A00, int numDummy, int numResult)
        {
            SignalGenerator.OutputBytes[0] = command;

            SignalGenerator.OutputBytes[1] = A23A16;
            SignalGenerator.OutputBytes[2] = A15A08;
            SignalGenerator.OutputBytes[3] = A07A00;

            var length = 4;

            for (var i = 0; i < numDummy; i++)
            {
                SignalGenerator.OutputBytes[length] = 0;
                length++;
            }

            for (var i = 0; i < numResult; i++)
            {
                SignalGenerator.OutputBytes[length] = 0;
                length++;
            }

            SignalGenerator.OutputLength = length;
        }

        private static void setCommand(byte command, int numDummy, int numResult)
        {

            SignalGenerator.OutputBytes[0] = command;
            var length = 1;

            for (var i = 0; i < numDummy; i++)
            {
                SignalGenerator.OutputBytes[length] = 0;
                length++;
            }

            for (var i = 0; i < numResult; i++)
            {
                SignalGenerator.OutputBytes[length] = 0;
                length++;
            }

            SignalGenerator.OutputLength = length;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (label_JEDEC_ID.Text == "W25Q128FV")
            {
                setCommand(SPI_read_SSR, 0, 1);
                usb.Transfer();
                ExtLog.AddLine("SSR1:" + tobin(SignalGenerator.InputBytes[1]));
                setCommand(SPI_read_SSR2, 0, 1);
                usb.Transfer();
                ExtLog.AddLine("SSR2:" + tobin(SignalGenerator.InputBytes[1]));
                setCommand(SPI_read_SSR3, 0, 1);
                usb.Transfer();
                ExtLog.AddLine("SSR3:" + tobin(SignalGenerator.InputBytes[1]));
            }
            else ExtLog.AddLine("Not Supported.");
        }

        private void button5_Click(object sender, EventArgs e)//WE
        {
            setCommand(SPI_write_enable, 0, 0);
            usb.Transfer();
            setCommand(SPI_read_SSR, 0, 1);
            usb.Transfer();
            ExtLog.AddLine("SSR:" + formatSSR(SignalGenerator.InputBytes[1]));
        }

        private void button6_Click(object sender, EventArgs e)//WD
        {
            setCommand(SPI_write_disable, 0, 0);
            usb.Transfer();
            setCommand(SPI_read_SSR, 0, 1);
            usb.Transfer();
            ExtLog.AddLine("SSR:" + formatSSR(SignalGenerator.InputBytes[1]));
        }

        private void button7_Click(object sender, EventArgs e)
        {
            setCommand(SPI_reset_enable, 0, 0);
            usb.Transfer();
            setCommand(SPI_reset_device, 0, 1);
            usb.Transfer();
            ExtLog.AddLine("Reset");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var addr = getAddress(Address_textbox.Text);

            setCommand(SPI_4K_sector_erease, addr.Item3, addr.Item2, addr.Item1, 0, 0);
            usb.Transfer();
            ExtLog.AddLine("4K Sector Erase...");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            var addr = getAddress(Address_textbox.Text);

            setCommand(SPI_32K_block_erease, addr.Item3, addr.Item2, addr.Item1, 0, 0);
            usb.Transfer();
            ExtLog.AddLine("32K Block Erase...");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            var addr = getAddress(Address_textbox.Text);


            setCommand(SPI_64K_block_erease, addr.Item3, addr.Item2, addr.Item1, 0, 0);
            usb.Transfer();
            ExtLog.AddLine("64K Block Erase...");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            setCommand(SPI_chip_erease, 0, 0);
            usb.Transfer();
            ExtLog.AddLine("Chip Erase...");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            var addr = getAddress(Address_textbox.Text);
            var numBytes = int.Parse(numBytes_textbox.Text);
            // if (numBytes > 255) numBytes = 255;
            setCommand(SPI_read, addr.Item3, addr.Item2, addr.Item1, 0, numBytes);
            ExtLog.AddLine("Reading byte(s)...");
            usb.Transfer();
            var sb = "";
            for (var i = 0; i < numBytes; i++)
            {
                sb += SignalGenerator.InputBytes[4 + i].ToString("X2") + " ";
            }
            ExtLog.AddLine(sb);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            var addr = getAddress(Address_textbox.Text);

            var dta = HEX_bytesData_toWrite_textbox.Text.Split(',');
            var bdta = new byte[dta.Length];
            for (var i = 0; i < dta.Length; i++)
            {
                bdta[i] = byte.Parse(dta[i].Trim(), NumberStyles.AllowHexSpecifier);
            }

            setCommand(SPI_byte_program, addr.Item3, addr.Item2, addr.Item1, 0, dta.Length);
            for (var i = 0; i < dta.Length; i++)
            {
                SignalGenerator.OutputBytes[4 + i] = bdta[i];
            }

            ExtLog.AddLine("Writing byte(s)...");
            usb.Transfer();
        }

        private static Tuple<byte, byte, byte> getAddress(string str)
        {
            return getAddress(int.Parse(str));
        }

        private static Tuple<byte, byte, byte> getAddress(int addr)
        {
            return new Tuple<byte, byte, byte>((byte)(addr & 0x000000FF), (byte)((addr & 0x0000FF00) >> 8), (byte)((addr & 0x00FF0000) >> 16));
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var follower = "open";
                var dump = new rawTFTDTA();

                try
                {
                    var fileStream = File.OpenRead(openFileDialog1.FileName);
                    var reader = new BinaryReader(fileStream);
                    follower = "header";
                    dump.COLMOD = reader.ReadByte();
                    dump.MADCTL = reader.ReadByte();
                    dump.num_images = reader.ReadByte();
                    dump.bytes_per_image = reader.ReadUInt16();
                    follower = "data";
                    dump.data = new byte[dump.num_images][];
                    for (var i = 0; i < dump.num_images; i++)
                    {
                        dump.data[i] = reader.ReadBytes(dump.bytes_per_image);
                    }
                }
                catch (Exception ex)
                {
                    ExtLog.AddLine("Error while reading file " + follower + " : " + ex.Message);
                }

                ExtLog.AddLine("COLMOD: " + dump.COLMOD.ToString("X2"));
                ExtLog.AddLine("MADCTL: " + dump.MADCTL.ToString("X2"));
                ExtLog.AddLine("num_images: " + dump.num_images.ToString("X2"));
                ExtLog.AddLine("bytes_per_image: " + dump.bytes_per_image.ToString("X4"));

                try
                {
                    if (!usb.IsOpen) throw new Exception("USB Interface not open");
                    if (JEDEC_ID_textbox.Text != JEDEC_ID_W25Q128FV_SPI) throw new Exception("Flash type not supported");
                    writeHeader(dump);
                    writeData(dump);
                }
                catch (Exception ex)
                {
                    ExtLog.AddLine("Error writing memory: " + ex.Message);
                }
            }
        }

        private void writeHeader(rawTFTDTA dump)
        {
            //writeEnable
            button5_Click(null, null);
            //erease64Block
            Address_textbox.Text = "0";

            if (dump.COLMOD == 0x03)
            {
                button9_Click(null, null);//32k
            } else { 
                button11_Click(null, null);// 64k
            }

            //wait for !busy
            waitFlashNotBusy(50, 20);

            //writeEnable
            button5_Click(null, null);
            //write by 256
            setCommand(SPI_byte_program, 0, 0, 0, 0, 5);

            SignalGenerator.OutputBytes[4] = dump.COLMOD;
            SignalGenerator.OutputBytes[5] = dump.MADCTL;
            SignalGenerator.OutputBytes[6] = dump.num_images;
            SignalGenerator.OutputBytes[7] = (byte)(dump.bytes_per_image >> 8);
            SignalGenerator.OutputBytes[8] = (byte)(dump.bytes_per_image & 0x00FF);
            ExtLog.AddLine("Writing header...");
            usb.Transfer();
        }

        private void writeData(rawTFTDTA dump)
        {
            for (var i = 0; i < dump.data.Length; i++)
            {

                //erease64Block
                setCommand(SPI_write_enable, 0, 0);
                usb.Transfer();
                Address_textbox.Text = ((1 + i) * 65536).ToString("D");
                Address_textbox.Refresh();
                if (dump.COLMOD == 0x03)
                {
                    button9_Click(null, null);//32k
                }
                else
                {
                    button11_Click(null, null);// 64k
                }
                //wait for !busy
                waitFlashNotBusy(60, 20);

                var offset = 0;
                while (offset < dump.data[i].Length)
                {
                    var sentData = Math.Min(256, dump.data[i].Length - offset);

                    //writeEnable
                    setCommand(SPI_write_enable, 0, 0);
                    usb.Transfer();

                    var addr = getAddress(((1 + i) * 65536) + offset);

                    setCommand(SPI_byte_program, addr.Item3, addr.Item2, addr.Item1, 0, sentData);

                    for (var j = 0; j < sentData; j++)
                    {
                        SignalGenerator.OutputBytes[4 + j] = dump.data[i][offset + j];
                    }
                    label_FlashStatusLine1.Text = "Writing " + (100 * offset / dump.data[i].Length) + "% of image " + i;
                    label_FlashStatusLine1.Refresh();
                    usb.Transfer();

                    waitFlashNotBusy(5, 20);

                    offset += sentData;
                }
            }
            label_FlashStatusLine1.Text = "Done loading " + dump.data.Length + " images";
        }

        private void waitFlashNotBusy(int sleep, int num_tries)
        {
            for (var i = 0; i < num_tries; i++)
            {
                Thread.Sleep(sleep);
                label_FlashStatusLine2.Text = "Checking Status...";
                label_FlashStatusLine2.Refresh();
                setCommand(SPI_read_SSR, 0, 1);
                usb.Transfer();
                if (!SignalGenerator.GetBit(SignalGenerator.InputBytes[1], SRR1_bit_Busy))
                {
                    label_FlashStatusLine2.Text = "";
                    label_FlashStatusLine2.Refresh();
                    label_FlashStatusLine3.Text = "";
                    label_FlashStatusLine3.Refresh();
                    return;
                }
                label_FlashStatusLine3.Text = $"Busy ({sleep * i} ms elapsed)";
                label_FlashStatusLine3.Refresh();
            }
            throw new Exception($"Still Busy after timeout of {sleep*num_tries} ms");
        }
    }

    public static class ExtLog
    {
        public static TextBox bx;
        public static void AddLine(string s)
        {
            bx?.AppendText(s + "\r\n");
        }
    }


}
