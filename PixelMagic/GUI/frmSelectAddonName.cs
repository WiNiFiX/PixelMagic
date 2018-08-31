//////////////////////////////////////////////////
//                                              //
//   See License.txt for Licensing information  //
//                                              //
//////////////////////////////////////////////////

using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using PixelMagic.Helpers;

namespace PixelMagic.GUI
{
    public partial class frmSelectAddonName : Form
    {
        public frmSelectAddonName()
        {
            InitializeComponent();
        }
        
        private void SelectWoWProcessToAttachTo_Load(object sender, EventArgs e)
        {
            var allfiles = Directory.GetFiles(WoW.AddonPath, "*.lua", SearchOption.AllDirectories);
            foreach (var file in allfiles)
            {
                try
                {
                    var line1 = File.ReadLines(file).First();
                    if (!line1.Contains("local cooldowns = { --These should be spellIDs for the spell you want to track for cooldowns"))
                        continue;

                    txtAddonName.Text = Path.GetFileName(Path.GetDirectoryName(file));
                    txtAddonName.Enabled = false;
                }
                catch (Exception)
                {
                    // ignored
                }
            }
        }

        private void cmdConnect_Click(object sender, EventArgs e)
        {
            if (txtAddonName.Text.Trim() == "")
            {
                MessageBox.Show("Please enter in a valid wow addon name", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ConfigFile.WriteValue("PixelMagic", "AddonName", txtAddonName.Text);
            Close();
        }
                
        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
