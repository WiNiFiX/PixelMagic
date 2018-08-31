using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using PixelMagic.Helpers;
using PixelMagic.Rotation;

namespace PixelMagic.GUI
{
    public partial class Overlay : Form
    {
        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;
        private static Overlay overlay;

        private Overlay()
        {
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.EnableNotifyMessage, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, false);
            BackColor = Color.Black;
            TransparencyKey = Color.White;
        }

        public sealed override Color BackColor
        {
            get { return base.BackColor; }
            set { base.BackColor = value; }
        }

        public static void updateLabels()
        {
            if (overlay == null)
                return;

            if (frmMain.combatRoutine.Type == CombatRoutine.RotationType.SingleTarget) overlay.RotationMode.Text = "Single-Target";
            if (frmMain.combatRoutine.Type == CombatRoutine.RotationType.SingleTargetCleave) overlay.RotationMode.Text = "Cleave-Target";
            if (frmMain.combatRoutine.Type == CombatRoutine.RotationType.AOE) overlay.RotationMode.Text = "AoE-Target";
        }

        public static void StartLabelUpdate()
        {
            if (overlay == null)
                return;

            overlay.Status.Text = "ON";
            overlay.Status.ForeColor = Color.DarkGreen;
            overlay.Status.BackColor = Color.GreenYellow;
        }

        public static void StopLabelUpdate()
        {
            if (overlay == null)
                return;

            overlay.Status.Text = "OFF";
            overlay.Status.ForeColor = Color.WhiteSmoke;
            overlay.Status.BackColor = Color.Red;
        }

        public static void UpdateLabelsCooldowns()
        {
            if (overlay == null)
                return;
            
            Threads.UpdateButton(overlay.Cooldowns, frmMain.combatRoutine.UseCooldowns ? "ON" : "OFF");
            overlay.Cooldowns.BackColor = overlay.Cooldowns.Text == "ON" ? Color.GreenYellow : Color.Red;
            overlay.Cooldowns.ForeColor = overlay.Cooldowns.Text == "ON" ? Color.DarkGreen : Color.WhiteSmoke;
        }

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        
        //protected override void OnPaintBackground(PaintEventArgs pevent)
        //{
        //    //do not allow the background to be painted 
        //}

        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    var rect = new Rectangle(439, 190, Width, Height);
        //    var b = new SolidBrush(Color.FromArgb(100, Color.Black));
        //    e.Graphics.FillRectangle(b, rect);

        //    b.Dispose();
        //}

        public static void ShowOverlay()
        {
            try
            {
                var p = new Point(ConfigFile.ReadValue<int>("Overlay", "X"), ConfigFile.ReadValue<int>("Overlay", "Y"));
                Log.Write($"Showing overlay at X = {p.X}, Y = {p.Y}");
                overlay = new Overlay {Location = p};
                overlay.Show();

                overlay.MouseDown += Overlay_MouseDown;
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message, Color.Red);
            }
        }

        private static void Overlay_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(overlay.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                ConfigFile.WriteValue("Overlay", "X", overlay.Location.X.ToString());
                ConfigFile.WriteValue("Overlay", "Y", overlay.Location.Y.ToString());
                Log.Write($"Saving new overlay location: X={overlay.Location.X} Y={overlay.Location.Y}", Color.Green);
            }
        }

        public static void HideOverlay()
        {
            overlay?.Hide();
        }

        private void Overlay_Load(object sender, EventArgs e)
        {
        }

        private void cmdRotationSettings_Click(object sender, EventArgs e)
        {
            try 
            {
                frmMain.combatRoutine.SettingsForm.ShowDialog();
            }
            catch 
            {
                MessageBox.Show("The selected rotation does not have settings.");
            }
            
        }

        private void Status_Click(object sender, EventArgs e)
        {
            if (Status.Text == "OFF") {
                frmMain.combatRoutine.Start();
                //frmMain.cmdStartBot.Text = "Stop rotation";
                //frmMain.cmdStartBot.BackColor = Color.Salmon;
            }
            else {
                frmMain.combatRoutine.Pause();
                //frmMain.cmdStartBot.Text = "Start rotation";
                //frmMain.cmdStartBot.BackColor = Color.LightGreen;
            }
        }

        private void Cooldowns_Click(object sender, EventArgs e)
        {
            frmMain.combatRoutine.UseCooldowns = Cooldowns.Text == "OFF";
        }

        private void RotationMode_Click(object sender, EventArgs e)
        {
            frmMain.combatRoutine.ChangeType(frmMain.combatRoutine.Type == CombatRoutine.RotationType.AOE ? CombatRoutine.RotationType.SingleTarget : CombatRoutine.RotationType.AOE);
        }
    }
}