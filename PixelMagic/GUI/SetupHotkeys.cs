//////////////////////////////////////////////////
//                                              //
//   See License.txt for Licensing information  //
//                                              //
//////////////////////////////////////////////////

using System;
using System.Windows.Forms;
using PixelMagic.Helpers;

// ReSharper disable once CheckNamespace

namespace PixelMagic.GUI
{
    public partial class SetupHotkeys : Form
    {
        public SetupHotkeys()
        {
            InitializeComponent();
        }

        private void SetupHotkeys_Load(object sender, EventArgs e)
        {
            if (ConfigFile.ReadValue("Hotkeys", cmbStartRotationModifierKey.Name) != "")
                cmbStartRotationModifierKey.Text = ConfigFile.ReadValue("Hotkeys", cmbStartRotationModifierKey.Name);
            if (ConfigFile.ReadValue("Hotkeys", cmbStartRotationKey.Name) != "")
                cmbStartRotationKey.Text = ConfigFile.ReadValue("Hotkeys", cmbStartRotationKey.Name);

            if (ConfigFile.ReadValue("Hotkeys", cmbSingleTargetModifierKey.Name) != "")
                cmbSingleTargetModifierKey.Text = ConfigFile.ReadValue("Hotkeys", cmbSingleTargetModifierKey.Name);
            if (ConfigFile.ReadValue("Hotkeys", cmbSingleTargetKey.Name) != "")
                cmbSingleTargetKey.Text = ConfigFile.ReadValue("Hotkeys", cmbSingleTargetKey.Name);

            if (ConfigFile.ReadValue("Hotkeys", cmbAOEModifierKey.Name) != "")
                cmbAOEModifierKey.Text = ConfigFile.ReadValue("Hotkeys", cmbAOEModifierKey.Name);
            if (ConfigFile.ReadValue("Hotkeys", cmbAOEKey.Name) != "")
                cmbAOEKey.Text = ConfigFile.ReadValue("Hotkeys", cmbAOEKey.Name);
        }

        private static void Error(string msg)
        {
            MessageBox.Show(msg, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (cmbStartRotationModifierKey.Text == cmbSingleTargetModifierKey.Text &&
                cmbStartRotationKey.Text == cmbSingleTargetKey.Text)
            {
                Error("Start rotation and single target keys cannot be the same, please correct");
                return;
            }

            if (cmbStartRotationModifierKey.Text == cmbAOEModifierKey.Text &&
                cmbStartRotationKey.Text == cmbAOEKey.Text)
            {
                Error("Start rotation and AOE keys cannot be the same, please correct");
                return;
            }

            ConfigFile.WriteValue("Hotkeys", cmbStartRotationModifierKey.Name, cmbStartRotationModifierKey.Text);
            ConfigFile.WriteValue("Hotkeys", cmbStartRotationKey.Name, cmbStartRotationKey.Text);

            ConfigFile.WriteValue("Hotkeys", cmbSingleTargetModifierKey.Name, cmbSingleTargetModifierKey.Text);
            ConfigFile.WriteValue("Hotkeys", cmbSingleTargetKey.Name, cmbSingleTargetKey.Text);

            ConfigFile.WriteValue("Hotkeys", cmbAOEModifierKey.Name, cmbAOEModifierKey.Text);
            ConfigFile.WriteValue("Hotkeys", cmbAOEKey.Name, cmbAOEKey.Text);

            MessageBox.Show("Settings saved.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }

        private void cmdDefaults_Click(object sender, EventArgs e)
        {
            ConfigFile.WriteValue("Hotkeys", cmbStartRotationModifierKey.Name, "");
            ConfigFile.WriteValue("Hotkeys", cmbStartRotationKey.Name, "");

            ConfigFile.WriteValue("Hotkeys", cmbSingleTargetModifierKey.Name, "");
            ConfigFile.WriteValue("Hotkeys", cmbSingleTargetKey.Name, "");

            ConfigFile.WriteValue("Hotkeys", cmbAOEModifierKey.Name, "");
            ConfigFile.WriteValue("Hotkeys", cmbAOEKey.Name, "");

            MessageBox.Show("Defaults restored and saved.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }
    }
}