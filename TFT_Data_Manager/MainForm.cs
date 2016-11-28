using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace TFT_Data_Manager
{
    public partial class MainForm : Form
    {
        public struct imageData
        {
            public string SourcePath; //fullpath or relative to location of database file

            [JsonIgnore]
            public Bitmap SourceBitmap;

            public int top, left, width, height;
            public int index;
        }

        public Dictionary<string, imageData> imageDataList = new Dictionary<string, imageData>();

        public struct rawTFTDTA
        {
            public byte COLMOD; // 444:03h 666:06h : set COLMOD
            public byte MADCTL; //set MADCTL
            public byte num_images; // 0-255
            public ushort bytes_per_image; // 444:7800h 666:F000h
            public byte[][] data; // new byte[num_images][bytes_per_image] - sector alignment to be done on transfer

        }

        private Brush clearBrush = new SolidBrush(Color.Green);
        private const float sourceAR = 640f / 480f;
        private bool updatingTrackBars;

        private Bitmap currentWorkingBitmap;
        private string currentWorkingImagePath;

        public MainForm()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            comboBox1.Text = comboBox1.Items[0].ToString();

        }


        private void addButton_Click(object sender, EventArgs e)
        {
            if (currentWorkingBitmap != null)
            {
                if (listView1.Items.Count < 255)
                {
                    var new_key = Guid.NewGuid().ToString();

                    imageDataList.Add(new_key, new imageData()
                    {
                        SourceBitmap = new Bitmap(currentWorkingBitmap),
                        SourcePath = currentWorkingImagePath,
                        top = Convert.ToInt32(TopTextBox.Text),
                        left = Convert.ToInt32(LeftTextBox.Text),
                        width = Convert.ToInt32(WidthTextBox.Text),
                        height = Convert.ToInt32(HeightTextBox.Text)
                    });

                    addElementToList(new_key, imageDataList[new_key]);
                }
                else MessageBox.Show("Nombre Maximum d'images atteint: 255", "Erreur", MessageBoxButtons.OK);
            }
        }

        private void addElementToList(string key, imageData img)
        {
            using (var bmpBuffer = new Bitmap(80, 64))
            using (var g = Graphics.FromImage(bmpBuffer))
            {
                g.DrawImage(img.SourceBitmap, new Rectangle(0, 0, 80, 64), img.left, img.top, img.width, img.height, GraphicsUnit.Pixel);
                thumbnailList.Images.Add(key, bmpBuffer);
            }

            listView1.Items.Add(Path.GetFileName(img.SourcePath), key);
            label5.Text = listView1.Items.Count.ToString() + " / 255";
        }

        private void OpenDataMenuItem_Click(object sender, EventArgs e)
        {
            if (openLIBFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //open library file

                try
                {

                    var data = File.ReadAllText(openLIBFileDialog1.FileName);
                    var buffer = JsonConvert.DeserializeObject<Dictionary<string, imageData>>(data);

                    thumbnailList.Images.Clear();
                    listView1.Items.Clear();
                    imageDataList.Clear();

                    foreach (var VARIABLE in buffer)
                    {
                        imageDataList.Add(VARIABLE.Key, addSourceBitmap(buffer[VARIABLE.Key]));
                        addElementToList(VARIABLE.Key, imageDataList[VARIABLE.Key]);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error Loading Library", MessageBoxButtons.OK);
                }
            }
        }

        private void SaveAsMenuItem_Click(object sender, EventArgs e)
        {
            if (saveLIBFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //save library file

                for (var i = 0; i < listView1.Items.Count; i++)
                {
                    imageDataList[listView1.Items[i].ImageKey] = addIndex(imageDataList[listView1.Items[i].ImageKey], listView1.Items[i].Index);
                }
                var data = JsonConvert.SerializeObject(imageDataList, Formatting.Indented);
                Debug.WriteLine(data);

                File.WriteAllText(saveLIBFileDialog1.FileName, data);
            }
        }

        private static imageData addIndex(imageData img, int newIndex)
        {
            return new imageData()
            {
                SourceBitmap = img.SourceBitmap,
                SourcePath = img.SourcePath,
                top = img.top,
                left = img.left,
                width = img.width,
                height = img.height,
                index = newIndex
            };
        }

        private static imageData addSourceBitmap(imageData img)
        {
            try
            {
                var bmpBuffer = new Bitmap(img.SourcePath);
                return new imageData()
                {
                    SourceBitmap = bmpBuffer,
                    SourcePath = img.SourcePath,
                    top = img.top,
                    left = img.left,
                    width = img.width,
                    height = img.height,
                    index = img.index
                };
            }
            catch (Exception)
            {
                return img;
            }
            
        }

        private void FlashMemoryMenuItem_Click(object sender, EventArgs e)
        {
            if (saveDTAFileDialog2.ShowDialog() == DialogResult.OK)
            {
                //save TFT data file
                var dump = new rawTFTDTA
                {
                    COLMOD = (comboBox1.SelectedIndex == 0) ? (byte)0x06 : (byte)0x03, // RGB666 - RGB444
                    MADCTL = 0xA0, // 10100000 // MY mx MV ml rgb mh na na
                    num_images = (byte)listView1.Items.Count,
                    bytes_per_image = (comboBox1.SelectedIndex == 0) ? (ushort)0xF000 : (ushort)0x7800
                };

                //init 2D array
                dump.data = new byte[dump.num_images][];
                for (var i = 0; i < dump.data.Length; i++)
                {
                    dump.data[i] = new byte[dump.bytes_per_image];
                }


                var destrect = new Rectangle(0, 0, 160, 128);
                for (var i = 0; i < dump.num_images; i++)
                {
                    using (var bmpBuffer = new Bitmap(160, 128))
                    using (var g = Graphics.FromImage(bmpBuffer))
                    {
                        var img = imageDataList[listView1.Items[i].ImageKey];
                        g.DrawImage(img.SourceBitmap, destrect, img.left, img.top, img.width, img.height, GraphicsUnit.Pixel);

                        //convert
                        if (comboBox1.SelectedIndex == 0)
                        { //RGB666
                            for (var y = 0; y < 128; y++)
                            {
                                for (var x = 0; x < 160; x++)
                                {
                                    var col = bmpBuffer.GetPixel(x, y); // todo optimize to marshal copymem
                                    dump.data[i][(3 * ((y * 160) + x)) + 0] = col.R;
                                    dump.data[i][(3 * ((y * 160) + x)) + 1] = col.G;
                                    dump.data[i][(3 * ((y * 160) + x)) + 2] = col.B;
                                }
                            }
                        }
                        else
                        { //RGB444
                            for (var y = 0; y < 128; y++)
                            {
                                for (var x = 0; x < 80; x++)
                                {
                                    // todo optimize to marshal copymem
                                    var col = bmpBuffer.GetPixel(2 * x, y);
                                    dump.data[i][(3 * ((y * 160) + (2 * x))) + 0] = (byte)(col.R & 0xF0);
                                    dump.data[i][(3 * ((y * 160) + (2 * x))) + 0] |= (byte)(col.G >> 4);
                                    dump.data[i][(3 * ((y * 160) + (2 * x))) + 1] = (byte)(col.B & 0xF0);
                                    col = bmpBuffer.GetPixel((2 * x) + 1, y);
                                    dump.data[i][(3 * ((y * 160) + (2 * x))) + 1] |= (byte)(col.R >> 4);
                                    dump.data[i][(3 * ((y * 160) + (2 * x))) + 2] = (byte)(col.G & 0xF0);
                                    dump.data[i][(3 * ((y * 160) + (2 * x))) + 2] |= (byte)(col.B >> 4);
                                }
                            }
                        }
                    }
                }

                var fileStream = File.Create(saveDTAFileDialog2.FileName);
                var writer = new BinaryWriter(fileStream);

                writer.Write(dump.COLMOD);
                writer.Write(dump.MADCTL);
                writer.Write(dump.num_images);
                writer.Write(dump.bytes_per_image);

                for (var i = 0; i < dump.data.Length; i++)
                {
                    writer.Write(dump.data[i], 0, dump.data[i].Length);
                }

                fileStream.Flush();
                fileStream.Dispose();
            }
        }

        private void loadSourceButton_Click(object sender, EventArgs e)
        {
            openImageFileDialog1.FileName = "*.*";
            if (openImageFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //open image file and display in sourceLabel

                currentWorkingBitmap?.Dispose();
                try
                {
                    currentWorkingBitmap = new Bitmap(openImageFileDialog1.FileName);
                    currentWorkingImagePath = openImageFileDialog1.FileName;
                    sourceLabel.Refresh();
                    fitMostButton_Click(this, null);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error Loading Image File", MessageBoxButtons.OK);
                    currentWorkingBitmap = null;
                }
            }
        }

        private void sourceLabel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(clearBrush, sourceLabel.ClientRectangle);

            if (currentWorkingBitmap != null)
            {
                e.Graphics.DrawImage(currentWorkingBitmap, sourceLabel.ClientRectangle,
                    new Rectangle(0, 0, currentWorkingBitmap.Width, currentWorkingBitmap.Height), GraphicsUnit.Pixel);
            }
        }

        private void fitMostButton_Click(object sender, EventArgs e)
        {
            updatingTrackBars = true;
            if (currentWorkingBitmap != null)
            {
                var imageAR = currentWorkingBitmap.Width / (float)currentWorkingBitmap.Height;

                var x = 0;
                var y = 0;
                var w = 100;
                var h = 100;

                if (imageAR > sourceAR)
                {
                    x = (int)(50 - (sourceAR / imageAR * 50));
                    w = 100 - x;
                }
                else if (imageAR < sourceAR)
                {
                    y = (int)(50 - (imageAR / sourceAR * 50));
                    h = 100 - y;
                }

                leftTrackBar.Value = x;
                rightTrackBar.Value = w;
                topTrackBar.Value = -y;
                bottomTrackBar.Value = -h;
                updatingTrackBars = false;

                TrackBar_ValueChanged(sender, e);
            }
        }


        private void TrackBar_ValueChanged(object sender, EventArgs e)
        {
            //update previewLabel
            if (!updatingTrackBars)
            {
                updatingTrackBars = true; // lock to avoid infinite recursion
                if (topTrackBar.Value < bottomTrackBar.Value) topTrackBar.Value = bottomTrackBar.Value;
                if (bottomTrackBar.Value > topTrackBar.Value) bottomTrackBar.Value = topTrackBar.Value;
                if (leftTrackBar.Value > rightTrackBar.Value) leftTrackBar.Value = rightTrackBar.Value;
                if (rightTrackBar.Value < leftTrackBar.Value) rightTrackBar.Value = leftTrackBar.Value;
                previewLabel.Refresh();
                updatingTrackBars = false;
            }
        }

        private void previewLabel_Paint(object sender, PaintEventArgs e)
        {
            if (currentWorkingBitmap != null)
            {
                var dta = getXYWH();
                LeftTextBox.Text = dta.Item1.ToString();
                TopTextBox.Text = dta.Item2.ToString();
                WidthTextBox.Text = dta.Item3.ToString();
                HeightTextBox.Text = dta.Item4.ToString();
                e.Graphics.DrawImage(currentWorkingBitmap, previewLabel.ClientRectangle, new Rectangle(dta.Item1, dta.Item2, dta.Item3, dta.Item4), GraphicsUnit.Pixel);
            }
        }

        private Tuple<int, int, int, int> getXYWH()
        {
            return new Tuple<int, int, int, int>(

                leftTrackBar.Value * currentWorkingBitmap.Width / 100, //x
                -topTrackBar.Value * currentWorkingBitmap.Height / 100, //y
                (rightTrackBar.Value - leftTrackBar.Value) * currentWorkingBitmap.Width / 100, //w
                (-bottomTrackBar.Value + topTrackBar.Value) * currentWorkingBitmap.Height / 100 //h
                );
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 1)
            {
                thumbnailList.Images.RemoveByKey(listView1.SelectedItems[0].ImageKey);
                imageDataList.Remove(listView1.SelectedItems[0].ImageKey);
                listView1.Items.RemoveAt(listView1.SelectedIndices[0]);
            }

            label5.Text = listView1.Items.Count.ToString() + " / 255";
        }

        private void fitAllButton_Click(object sender, EventArgs e)
        {
            updatingTrackBars = true;
            topTrackBar.Value = 0;
            bottomTrackBar.Value = -100;
            leftTrackBar.Value = 0;
            rightTrackBar.Value = 100;
            updatingTrackBars = false;
            TrackBar_ValueChanged(sender, e);
        }

        private void moveUpButton_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 1)
            {
                var original_item = listView1.SelectedItems[0];
                var original_index = original_item.Index;
                if (original_index > 0)
                {
                    listView1.Items.RemoveAt(original_index);
                    listView1.Items.Insert(original_index - 1, original_item);

                    forceUpdateCrappyListViewBehaviour();

                    listView1.Focus();
                    listView1.Items[original_index - 1].Selected = true;
                }

            }
        }

        private void moveDownButton_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 1)
            {
                var original_item = listView1.SelectedItems[0];
                var original_index = original_item.Index;
                if (original_index < listView1.Items.Count - 1)
                {
                    listView1.Items.RemoveAt(original_index);
                    listView1.Items.Insert(original_index + 1, original_item);

                    forceUpdateCrappyListViewBehaviour();

                    listView1.Focus();
                    listView1.Items[original_index + 1].Selected = true;
                }

            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (listView1.SelectedIndices.Count > 0) Text = listView1.SelectedIndices[0].ToString();
            //TODO set the sourceLabel + previewLabel + textboxes + sliders according to listviewitem
        }

        private void forceUpdateCrappyListViewBehaviour()
        {
            listView1.BeginUpdate();
            listView1.Alignment = ListViewAlignment.Default;
            listView1.Alignment = ListViewAlignment.Top;
            listView1.EndUpdate();
        }

        private void addFromLibToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openLIBFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //open and add library file

                try
                {

                    var data = File.ReadAllText(openLIBFileDialog1.FileName);
                    var buffer = JsonConvert.DeserializeObject<Dictionary<string, imageData>>(data);

                   // thumbnailList.Images.Clear();
                   // listView1.Items.Clear();
                   // imageDataList.Clear();

                    foreach (var VARIABLE in buffer)
                    {
                        imageDataList.Add(VARIABLE.Key, addSourceBitmap(buffer[VARIABLE.Key]));
                        addElementToList(VARIABLE.Key, imageDataList[VARIABLE.Key]);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error Loading Library", MessageBoxButtons.OK);
                }
            }
        }
    }
}
