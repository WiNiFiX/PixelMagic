//////////////////////////////////////////////////
//                                              //
//   See License.txt for Licensing information  //
//                                              //
//////////////////////////////////////////////////

using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using ShadowMagic.Helpers;

namespace ShadowMagic.GUI
{
    public partial class SelectWoWProcessToAttachTo : Form
    {
        private readonly frmMain parent;

        public SelectWoWProcessToAttachTo(frmMain parent)
        {
            this.parent = parent;
            InitializeComponent();
        }

        private void refreshProcessList()
        {
            cmbWoW.Items.Clear();
            

            var processes = Process.GetProcessesByName("Wow");

            foreach (var process in processes)
            {
                cmbWoW.Items.Add($"WoW x86 [Live] => {process.Id}");
            }

            processes = Process.GetProcessesByName("Wow-64");

            foreach (var process in processes)
            {
                cmbWoW.Items.Add($"WoW x64 [Live] => {process.Id}");
            }
            
            processes = Process.GetProcessesByName("WowB-64");

            foreach (var process in processes)
            {
                cmbWoW.Items.Add($"WoW x64 [Beta] => {process.Id}");
            }

            processes = Process.GetProcessesByName("WowT-64");

            foreach (var process in processes)
            {
                cmbWoW.Items.Add($"WoW x64 [PTR] => {process.Id}");
            }

            if (cmbWoW.Items.Count > 0)
            {
                cmbWoW.SelectedIndex = 0;
                cmbWoW.Enabled = true;
                cmdConnect.Enabled = true;
            }
            else
            {
                cmbWoW.Items.Add("Please open WoW then click 'Refresh' button.");
                cmbWoW.SelectedIndex = 0;
                cmbWoW.Enabled = false;
                cmdConnect.Enabled = false;
            }
            
            foreach (var fileName in Directory.GetFiles(Application.StartupPath + "\\Rotations", "*.*", SearchOption.AllDirectories))
            {
                cmbRotation.Items.Add(fileName.Replace(Application.StartupPath + "\\Rotations", "").Substring(1));
            }

            if (cmbRotation.Items.Count > 0)
            {
                var lastRotation = ConfigFile.ReadValue("PixelMagic", "LastProfile");

                if (lastRotation != "")
                {
                    lastRotation = lastRotation.Replace(Application.StartupPath + "\\Rotations", "").Substring(1);

                    cmbClass.Text = lastRotation.Split('\\')[0];
                    cmbRotation.Text = lastRotation;
                }
                else
                {
                    if (cmbClass.Text != "")
                        cmbRotation.SelectedIndex = 0;
                }
                
                cmbRotation.Enabled = true;
            }
            else
            {
                cmbRotation.Enabled = false;
            }
        }

        private void SelectWoWProcessToAttachTo_Load(object sender, EventArgs e)
        {
            cmbWoW.KeyDown += CmbWoW_KeyDown;
            FormClosing += SelectWoWProcessToAttachTo_FormClosing;

            cmbWoW.Focus();

            refreshProcessList();            
        }

        private void SelectWoWProcessToAttachTo_FormClosing(object sender, FormClosingEventArgs e)
        {
            //parent.process = null;            
        }

        private void CmbWoW_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmdConnect.PerformClick();
            }
        }
        
        private void cmdConnect_Click(object sender, EventArgs e)
        {
            if (cmbWoW.Text.Contains(">"))
            {
                var PID = int.Parse(cmbWoW.Text.Split('>')[1]);
                parent.process = Process.GetProcessById(PID);

                ConfigFile.WriteValue("PixelMagic", "LastProfile", Application.StartupPath + "\\Rotations\\" + cmbRotation.Text);

                Close();
            }
            else
            {
                MessageBox.Show("Please select a valid wow process to connect to", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmdRefresh_Click(object sender, EventArgs e)
        {
            refreshProcessList();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            //parent.process = null;
            Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbRotation.Items.Clear();
            foreach (var fileName in Directory.GetFiles(Application.StartupPath + $"\\Rotations\\{cmbClass.Text.Trim()}\\", "*.cs", SearchOption.AllDirectories)) 
            {
                cmbRotation.Items.Add(fileName.Replace(Application.StartupPath + "\\Rotations\\", ""));
            }
            foreach (var fileName in Directory.GetFiles(Application.StartupPath + $"\\Rotations\\{cmbClass.Text.Trim()}\\", "*.enc", SearchOption.AllDirectories))
            {
                cmbRotation.Items.Add(fileName.Replace(Application.StartupPath + "\\Rotations\\", ""));
            }
        }
    }
}
