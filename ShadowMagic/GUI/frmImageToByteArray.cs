//////////////////////////////////////////////////
//                                              //
//   See License.txt for Licensing information  //
//                                              //
//////////////////////////////////////////////////

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace ShadowMagic.GUI.GUI
{
    public partial class frmImageToByteArray : Form
    {
        public frmImageToByteArray()
        {
            InitializeComponent();
        }

        public byte[] imageToByteArray(Image imageIn)
        {
            var ms = new MemoryStream();
            imageIn.Save(ms, ImageFormat.Gif);
            return ms.ToArray();
        }

        private void cmdBrowse_Click(object sender, EventArgs e)
        {
            var res = openFileDialog1.ShowDialog();

            if (res == DialogResult.OK)
            {
                txtPath.Text = openFileDialog1.FileName;

                picSample.Image = Image.FromFile(txtPath.Text);

                Cursor = Cursors.WaitCursor;
                Application.DoEvents();

                var bytes = imageToByteArray(picSample.Image);
                var byteString = string.Join(",", bytes);
                txtByteString.Text = byteString;

                Cursor = Cursors.Arrow;
                txtByteString.SelectAll();
            }
        }

        private void picSample_Click(object sender, EventArgs e)
        {
        }

        private void frmImageToByteArray_Load(object sender, EventArgs e)
        {
        }
    }
}