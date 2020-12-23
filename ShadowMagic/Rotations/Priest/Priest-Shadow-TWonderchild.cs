//-TWonderchilds Shadow Priest
//-ToDo:
//          - AoE Rotation
//          - Legendary Trinket
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System;
using ShadowMagic.Helpers;


namespace ShadowMagic.Rotation
{
    public class TWonderchildSP2 : CombatRoutine
    {
        //-Constants----------------//
        // Spells
        private const string SHADOWFORM = "Shadowform";
        private const string VAMPIRIC_TOUCH = "Vampiric Touch";
        private const string VAMPIRIC_EMBRACE = "Vampiric Embrace";
        private const string SHADOW_WORD_PAIN = "Shadow Word: Pain";
        private const string SHADOW_WORD_DEATH = "Shadow Word: Death";
        private const string MIND_BLAST = "Mind Blast";
        private const string MIND_FLAY = "Mind Flay";
        private const string VOID_ERUPTION = "Void Eruption";
        private const string VOID_TORRENT = "Void Torrent";
        private const string VOID_BOLT = "Void Bolt";
        private const string POWER_WORD_SHIELD = "Power Word: Shield";
        private const string SHADOW_MEND = "Shadow Mend";
        private const string POWER_INFUSION = "Power Infusion";
        private const string SHADOW_FIEND = "Shadowfiend";
        private const string SILENCE = "Silence";

        // Buffs/Auras
        private const string SHADOWFORM_AURA = "Shadowform";
        private const string POWER_INFUSION_AURA = "Power Infusion";
        private const string VOIDFORM_AURA = "Voidform";
        private const string T19_VOID = "Void";
        private const string LEG_BOOTS = "Norgannon's Foresight";
        //Items
        private const string ITEM_KILJAEDEN = "Kil'jaeden's Burning Wish";

        //--------------------------//
        //-Form-Stuff---------------//
        // Cooldowns
        private CheckBox UseCDBox;

        private CheckBox PIBloodlustBox;
        private CheckBox PIStacksBox;
        private CheckBox PIWaitBox;
        private TextBox PIWaitText;
        private CheckBox SFWaitBox;
        private TextBox SFWaitText;
        private CheckBox SFPIBox;
        private CheckBox VoidTorrentBox;
        private TextBox SWDText;
        private TextBox PWSText;
        private TextBox VEText;
        private CheckBox SilenceBox;
        private RadioButton VoidTorrentRadio1;
        private RadioButton VoidTorrentRadio2;
        private CheckBox LegTrinketBox;
        //--------------------------//

        private readonly Stopwatch interruptwatch = new Stopwatch();
        public override string Name { get { return "TWonderchild's Shadow Priest"; } }
        public override string Class { get { return "Priest"; } }

        public override Form SettingsForm{ get; set; }
        //--------------------------//
        public static bool chkPIBloodlust
        {
            get
            {
                var PIBloodlust = ConfigFile.ReadValue("TWShadow", "PIBloodlust").Trim();

                if (PIBloodlust != "")
                {
                    return Convert.ToBoolean(PIBloodlust);
                }

                return true;
            }
            set { ConfigFile.WriteValue("TWShadow", "PIBloodlust", value.ToString()); }
        }
        public static bool chkPIStacks
        {
            get
            {
                var PIStacks = ConfigFile.ReadValue("TWShadow", "PIStacks").Trim();

                if (PIStacks != "")
                {
                    return Convert.ToBoolean(PIStacks);
                }

                return true;
            }
            set { ConfigFile.WriteValue("TWShadow", "PIStacks", value.ToString()); }
        }
        public static bool chkPIWait
        {
            get
            {
                var PIWait = ConfigFile.ReadValue("TWShadow", "PIWait").Trim();

                if (PIWait != "")
                {
                    return Convert.ToBoolean(PIWait);
                }

                return true;
            }
            set { ConfigFile.WriteValue("TWShadow", "PIWait", value.ToString()); }
        }
        public static int txtPIWait
        {
            get
            {
                var PIWait = ConfigFile.ReadValue("TWShadow", "txtPIWait").Trim();
                if (PIWait != "")
                {
                    return Convert.ToInt32(PIWait);
                }

                return 60;
            }
            set { ConfigFile.WriteValue("TWShadow", "txtPIWait", value.ToString()); }
        }
        public static bool chkSFPI
        {
            get
            {
                var SFPI = ConfigFile.ReadValue("TWShadow", "SFPI").Trim();

                if (SFPI != "")
                {
                    return Convert.ToBoolean(SFPI);
                }

                return true;
            }
            set { ConfigFile.WriteValue("TWShadow", "SFPI", value.ToString()); }
        }
        public static bool chkSFWait
        {
            get
            {
                var SFWait = ConfigFile.ReadValue("TWShadow", "SFWait").Trim();

                if (SFWait != "")
                {
                    return Convert.ToBoolean(SFWait);
                }

                return true;
            }
            set { ConfigFile.WriteValue("TWShadow", "SFWait", value.ToString()); }
        }
        public static int txtSFWait
        {
            get
            {
                var SFWait = ConfigFile.ReadValue("TWShadow", "txtSFWait").Trim();
                if (SFWait != "")
                {
                    return Convert.ToInt32(SFWait);
                }

                return 60;
            }
            set { ConfigFile.WriteValue("TWShadow", "txtSFWait", value.ToString()); }
        }
        public static bool chkVoidTorrent
        {
            get
            {
                var VoidTorrent = ConfigFile.ReadValue("TWShadow", "VoidTorrent").Trim();

                if (VoidTorrent != "")
                {
                    return Convert.ToBoolean(VoidTorrent);
                }

                return true;
            }
            set { ConfigFile.WriteValue("TWShadow", "VoidTorrent", value.ToString()); }
        }
        public static int txtSWD
        {
            get
            {
                var SWD = ConfigFile.ReadValue("TWShadow", "txtSWD").Trim();
                if (SWD != "")
                {
                    return Convert.ToInt32(SWD);
                }

                return 50;
            }
            set { ConfigFile.WriteValue("TWShadow", "txtSWD", value.ToString()); }
        }
        public static int txtPWS
        {
            get
            {
                var PWS = ConfigFile.ReadValue("TWShadow", "txtPWS").Trim();
                if (PWS != "")
                {
                    return Convert.ToInt32(PWS);
                }

                return 30;
            }
            set { ConfigFile.WriteValue("TWShadow", "txtPWS", value.ToString()); }
        }
        public static int txtVE
        {
            get
            {
                var VE = ConfigFile.ReadValue("TWShadow", "txtVE").Trim();
                if (VE != "")
                {
                    return Convert.ToInt32(VE);
                }

                return 20;
            }
            set { ConfigFile.WriteValue("TWShadow", "txtVE", value.ToString()); }
        }
        public static bool chkSilence
        {
            get
            {
                var Silence = ConfigFile.ReadValue("TWShadow", "Silence").Trim();

                if (Silence != "")
                {
                    return Convert.ToBoolean(Silence);
                }

                return true;
            }
            set { ConfigFile.WriteValue("TWShadow", "Silence", value.ToString()); }
        }
        public static bool radVoidTorrent1
        {
            get
            {
                var VoidTorrent1 = ConfigFile.ReadValue("TWShadow", "VoidTorrent1").Trim();

                if (VoidTorrent1 != "")
                {
                    return Convert.ToBoolean(VoidTorrent1);
                }
                return false;
            }
            set { ConfigFile.WriteValue("TWShadow", "VoidTorrent1", value.ToString()); }
        }
        public static bool radVoidTorrent2
        {
            get
            {
                var VoidTorrent2 = ConfigFile.ReadValue("TWShadow", "VoidTorrent2").Trim();

                if (VoidTorrent2 != "")
                {
                    return Convert.ToBoolean(VoidTorrent2);
                }

                return true;
            }
            set { ConfigFile.WriteValue("TWShadow", "VoidTorrent2", value.ToString()); }
        }
        public static bool chkLegTrinket
        {
            get
            {
                var LegTrinket = ConfigFile.ReadValue("TWShadow", "LegTrinket").Trim();

                if (LegTrinket != "")
                {
                    return Convert.ToBoolean(LegTrinket);
                }

                return true;
            }
            set { ConfigFile.WriteValue("TWShadow", "LegTrinket", value.ToString()); }
        }

        //-Init---------------------//
        public override void Initialize()
        {
            Log.DrawHorizontalLine();
            Log.WritePixelMagic("Welcome to TWonderchild's Shadow Priest", Color.Black);
            Log.DrawHorizontalLine();
            Log.WritePixelMagic("Please use the following Talents: 1211111", Color.Black);
            Log.WritePixelMagic("Surrender to Madness is not supported.", Color.Black);
            Log.WritePixelMagic("Check the ToDo-List before reporting bugs/requesting features", Color.Black);
            Log.WritePixelMagic("Cooldown Hotkey: F7", Color.Black);

            SettingsForm = new Form { Text = "Shadow Priest Settings", StartPosition = FormStartPosition.CenterScreen, Height = 420, Width = 360 };
            var labelCooldowns = new Label { Text = "Cooldown Usage", Size = new Size(180, 20), Left = 8, Top = 10 };
            SettingsForm.Controls.Add(labelCooldowns);
            PIBloodlustBox = new CheckBox { Checked = chkPIBloodlust, TabIndex = 1, Size = new Size(380, 20), Left = 25, Top = 30, Text = "Use Power Infusion with Bloodlust (Ignores other Conditions)" };
            PIBloodlustBox.CheckedChanged += chkPIBloodlust_Click;
            SettingsForm.Controls.Add(PIBloodlustBox);
            PIStacksBox = new CheckBox { Checked = chkPIStacks, TabIndex = 1, Size = new Size(380, 20), Left = 25, Top = 50, Text = "Use Power Infusion at 20 Voidform Stacks" };
            PIStacksBox.CheckedChanged += chkPIStacks_Click;
            SettingsForm.Controls.Add(PIStacksBox);
            PIWaitBox = new CheckBox { Checked = chkPIWait, TabIndex = 1, Size = new Size(275, 20), Left = 25, Top = 70, Text = "Use PI and ignore Stacks if Shadow Fiend CD > " };
            PIWaitBox.CheckedChanged += chkPIWait_Click;
            SettingsForm.Controls.Add(PIWaitBox);
            PIWaitText = new TextBox { Size = new Size(30, 20), Left = 300, Top = 70, Text = txtPIWait.ToString()};
            PIWaitText.TextChanged += txtPIWait_TextChanged;
            SettingsForm.Controls.Add(PIWaitText);
            SFPIBox = new CheckBox { Checked = chkSFPI, TabIndex = 1, Size = new Size(380, 20), Left = 25, Top = 90, Text = "Use Shadow Fiend at 12 Sec Bloodlust/Power Infusion"};
            SFPIBox.CheckedChanged += chkSFPI_Click;
            SettingsForm.Controls.Add(SFPIBox);
            SFWaitBox = new CheckBox { Checked = chkSFWait, TabIndex = 1, Size = new Size(275, 20), Left = 25, Top = 110, Text = "Use Shadow Fiend if Power Infusion CD > " };
            SFWaitBox.CheckedChanged += chkSFWait_Click;
            SettingsForm.Controls.Add(SFWaitBox);
            SFWaitText = new TextBox { Size = new Size(30, 20), Left = 300, Top = 110, Text = txtSFWait.ToString() };
            SFWaitText.TextChanged += txtSFWait_TextChanged;
            SettingsForm.Controls.Add(SFWaitText);
            VoidTorrentBox = new CheckBox { Checked = chkVoidTorrent, TabIndex = 1, Size = new Size(275, 20), Left = 25, Top = 130, Text = "Use Void Torrent" };
            VoidTorrentBox.CheckedChanged += chkVoidTorrent_Click;
            SettingsForm.Controls.Add(VoidTorrentBox);
            var labelRotation = new Label { Text = "Rotation", Size = new Size(180, 20), Left = 8, Top = 150 };
            SettingsForm.Controls.Add(labelRotation);
            var labelSWD = new Label { Text = "Use 2nd SWD Stack if Insanity drops below ", Size = new Size(225, 20), Left = 8, Top = 170 };
            SettingsForm.Controls.Add(labelSWD);
            SWDText = new TextBox { Size = new Size(30, 20), Left = 300, Top = 170, Text = txtSWD.ToString() };
            SWDText.TextChanged += txtSWD_TextChanged;
            SettingsForm.Controls.Add(SWDText);
            var labelOthers = new Label { Text = "Other spells", Size = new Size(180, 20), Left = 8, Top = 190 };
            SettingsForm.Controls.Add(labelOthers);
            var labelPWS = new Label { Text = "Use PWS at X% Health ", Size = new Size(225, 20), Left = 8, Top = 210 };
            SettingsForm.Controls.Add(labelPWS);
            PWSText = new TextBox { Size = new Size(30, 20), Left = 300, Top = 210, Text = txtPWS.ToString() };
            PWSText.TextChanged += txtPWS_TextChanged;
            SettingsForm.Controls.Add(PWSText);
            var labelVE = new Label { Text = "Use Vampiric Embrace at X% Health ", Size = new Size(225, 20), Left = 8, Top = 230 };
            SettingsForm.Controls.Add(labelVE);
            VEText = new TextBox { Size = new Size(30, 20), Left = 300, Top = 230, Text = txtVE.ToString() };
            VEText.TextChanged += txtVE_TextChanged;
            SettingsForm.Controls.Add(VEText);
            SilenceBox = new CheckBox { Checked = chkSilence, TabIndex = 1, Size = new Size(380, 20), Left = 10, Top = 250, Text = "Auto Silence" };
            SilenceBox.CheckedChanged += chkSilence_Click;
            SettingsForm.Controls.Add(SilenceBox);
            VoidTorrentRadio1 = new RadioButton { Checked = radVoidTorrent1, TabIndex = 1, Size = new Size(200, 20), Left=10, Top=270, Text= "Use VT as CD",};
            VoidTorrentRadio1.CheckedChanged += radVoidTorrent1_Click;
            SettingsForm.Controls.Add(VoidTorrentRadio1);
            VoidTorrentRadio2 = new RadioButton { Checked = radVoidTorrent2, TabIndex = 1, Size = new Size(200, 20), Left = 210, Top = 270, Text = "Use VT in Rota" };
            VoidTorrentRadio2.CheckedChanged += radVoidTorrent2_Click;
            SettingsForm.Controls.Add(VoidTorrentRadio2);
            var labelItems = new Label { Text = "Items", Size = new Size(180, 20), Left = 8, Top = 290 };
            SettingsForm.Controls.Add(labelItems);
            LegTrinketBox = new CheckBox { Checked = chkLegTrinket, TabIndex = 1, Size = new Size(275, 20), Left = 25, Top = 310, Text = "Use Kil'jaeden's Burning Wish" };
            LegTrinketBox.CheckedChanged += chkLegTrinket_Click;
            SettingsForm.Controls.Add(LegTrinketBox);
            var labelTalentsLink = new LinkLabel { Text = "Talents", Size = new Size(300, 20), Left = 8, Top = 330};
            labelTalentsLink.Links.Add(0,7,"http://eu.battle.net/wow/en/tool/talent-calculator#Xba!0100000");
            labelTalentsLink.LinkClicked += labelTalentsLink_LinkClicked;
            SettingsForm.Controls.Add(labelTalentsLink);
            var labelDonationLink = new LinkLabel { Text = "Appreciated but NOT necessary: Donate via Bitcoin", Size = new Size(300, 20), Left = 8, Top = 350 };
            labelDonationLink.Links.Add(31, 18, "https://github.com/TWonderchild/TWonderchild.github.io");
            labelDonationLink.LinkClicked += labelDonationLink_LinkClicked;
            SettingsForm.Controls.Add(labelDonationLink);
        }
        //--------------------------//
        private void chkPIBloodlust_Click(object sender, EventArgs e)
        {
            chkPIBloodlust = PIBloodlustBox.Checked;
        }
        private void chkPIStacks_Click(object sender, EventArgs e)
        {
            chkPIStacks = PIStacksBox.Checked;
        }
        private void chkPIWait_Click(object sender, EventArgs e)
        {
            chkPIWait = PIWaitBox.Checked;
        }
        private void txtPIWait_TextChanged(object sender, EventArgs e)
        {
            int userInput;
            if (int.TryParse(PIWaitText.Text, out userInput) && userInput >= 0)
            {
                txtPIWait = userInput;
            }
            else
            {
                PIWaitText.Text = "";
                Log.Write("Invalid value! Check Shadow Word:Death settings!", Color.Red);
            }
        }
        private void chkSFPI_Click(object sender, EventArgs e)
        {
            chkSFPI = SFPIBox.Checked;
        }
        private void chkSFWait_Click(object sender, EventArgs e)
        {
            chkSFWait = SFWaitBox.Checked;
        }
        private void txtSFWait_TextChanged(object sender, EventArgs e)
        {
            int userInput;
            if (int.TryParse(SFWaitText.Text, out userInput) && userInput >= 0)
            {
                txtSFWait = userInput;
            }
            else
            {
                SFWaitText.Text = "";
                Log.Write("Invalid value! Check Shadow Word:Death settings!", Color.Red);
            }
        }
        private void chkVoidTorrent_Click(object sender, EventArgs e)
        {
            chkVoidTorrent = VoidTorrentBox.Checked;
        }
        private void txtSWD_TextChanged(object sender, EventArgs e)
        {
            int userInput;
            if (int.TryParse(SWDText.Text, out userInput) && userInput >= 0 && userInput <= 100)
            {
                txtSWD = userInput;
            }
            else
            {
                SWDText.Text = "";
                Log.Write("Invalid value! Check Shadow Word:Death settings!", Color.Red);
            }
        }
        private void txtPWS_TextChanged(object sender, EventArgs e)
        {
            int userInput;
            if (int.TryParse(PWSText.Text, out userInput) && userInput >= 0 && userInput <= 100)
            {
                txtPWS = userInput;
            }
            else
            {
                PWSText.Text = "";
                Log.Write("Invalid value! Check Power Word:Shield settings!", Color.Red);
            }
        }
        private void txtVE_TextChanged(object sender, EventArgs e)
        {
            int userInput;
            if (int.TryParse(VEText.Text, out userInput) && userInput >= 0 && userInput <= 100)
            {
                txtVE = userInput;
            }
            else
            {
                VEText.Text = "";
                Log.Write("Invalid value! Check Vampiric Embrace settings!", Color.Red);
            }
        }
        private void chkSilence_Click(object sender, EventArgs e)
        {
            chkSilence = SilenceBox.Checked;
        }
        private void radVoidTorrent1_Click(object sender, EventArgs e)
        {
            radVoidTorrent1 = VoidTorrentRadio1.Checked;
            radVoidTorrent2 = VoidTorrentRadio2.Checked;
        }
        private void radVoidTorrent2_Click(object sender, EventArgs e)
        {
            radVoidTorrent2 = VoidTorrentRadio2.Checked;
            radVoidTorrent1 = VoidTorrentRadio1.Checked;
        }
        private void chkLegTrinket_Click(object sender, EventArgs e)
        {
            chkLegTrinket = LegTrinketBox.Checked;
        }
        private void labelTalentsLink_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData.ToString());
        }
        private void labelDonationLink_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData.ToString());
        }
        //--------------------------//
        public override void Stop()
        {
        }
        //-Pulse--------------------//
        public override void Pulse()
        {
            if (DetectKeyPress.GetKeyState(0x76) < 0)
            {
                UseCooldowns = !UseCooldowns;
                Thread.Sleep(150);
            }
                

            if (WoW.IsInCombat)
                interruptwatch.Start();
            else
                return;

            if (combatRoutine.Type != RotationType.SingleTarget && combatRoutine.Type != RotationType.AOE) return;
            if (!WoW.HasTarget || !WoW.TargetIsEnemy) return;

            if (WoW.HealthPercent < txtPWS && !WoW.PlayerHasBuff(POWER_WORD_SHIELD) && !WoW.IsSpellOnCooldown(POWER_WORD_SHIELD))
            {
                Log.Write("Health below " + txtPWS + "% - Using PWS now", Color.Red);
                SpellCast(POWER_WORD_SHIELD);
                return;
            }
            if (SilenceBox.Checked && WoW.TargetIsCastingAndSpellIsInterruptible && interruptwatch.ElapsedMilliseconds > 900)
            {
                if (!WoW.IsSpellOnCooldown(SILENCE))
                {
                    SpellCast(SILENCE);
                    interruptwatch.Reset();
                    interruptwatch.Start();
                    return;
                }
            }

            if (!(WoW.PlayerHasBuff(SHADOWFORM_AURA) || WoW.PlayerHasBuff(VOIDFORM_AURA)))
            {
                SpellCast(SHADOWFORM);
            }
            CooldownUsage();
            SingleTargetRotation();
        }

        private void SingleTargetRotation()
        {
            if (!WoW.PlayerHasBuff(VOIDFORM_AURA) && WoW.Insanity >= 65 && !MoveCheck() && DotsUp())
                SpellCast(VOID_ERUPTION);

            if(WoW.PlayerHasBuff(VOIDFORM_AURA))
            {
                if(!MoveCheck() && VoidTorrentRadio2.Checked && DotsUp() && !WoW.PlayerHasBuff(T19_VOID) && WoW.IsSpellOnCooldown(MIND_BLAST) && WoW.IsSpellOnCooldown(VOID_BOLT))
                    SpellCast(VOID_TORRENT);
                if(WoW.PlayerHasBuff(T19_VOID) || DotsUp())
                    SpellCast(VOID_BOLT);
            }

            if (!MoveCheck() && DotsUp() && !WoW.PlayerHasBuff(T19_VOID) && WoW.LastSpell != VOID_ERUPTION)
                SpellCast(MIND_BLAST);

            if(WoW.PlayerSpellCharges(SHADOW_WORD_DEATH) == 2 && (WoW.Insanity <= 80 || (WoW.IsSpellOnCooldown(MIND_BLAST) && WoW.IsSpellOnCooldown(VOID_BOLT))) && WoW.TargetHealthPercent <= 20 && DotsUp())
                SpellCast(SHADOW_WORD_DEATH);
            if (WoW.PlayerSpellCharges(SHADOW_WORD_DEATH) == 1 && WoW.Insanity <= txtSWD && WoW.TargetHealthPercent <= 20 && DotsUp())
                SpellCast(SHADOW_WORD_DEATH);

            if ((!WoW.TargetHasDebuff(VAMPIRIC_TOUCH) || WoW.TargetDebuffTimeRemaining(VAMPIRIC_TOUCH) <= 4) && !MoveCheck() && WoW.LastSpell!= VAMPIRIC_TOUCH && !WoW.PlayerHasBuff(T19_VOID)) //Messy workaround to fix the double VT-Cast, since addon/BLizz API is returning weird values
                SpellCast(VAMPIRIC_TOUCH);
                
            if ((!WoW.TargetHasDebuff(SHADOW_WORD_PAIN) || WoW.TargetDebuffTimeRemaining(SHADOW_WORD_PAIN) <= 3) && !WoW.PlayerHasBuff(T19_VOID))
                SpellCast(SHADOW_WORD_PAIN);
            
            if(WoW.TargetHasDebuff(SHADOW_WORD_PAIN) && WoW.TargetHasDebuff(VAMPIRIC_TOUCH) && !MoveCheck() && WoW.IsSpellOnCooldown(MIND_BLAST) && !WoW.PlayerHasBuff(T19_VOID) && WoW.LastSpell != VOID_ERUPTION)
                SpellCast(MIND_FLAY);
        }

        private void SpellCast(string spellName)
        {
            if (WoW.PlayerIsCasting || (WoW.LastSpell == VOID_TORRENT && WoW.PlayerIsChanneling))
                return;
            if (spellName == MIND_FLAY && WoW.LastSpell == MIND_FLAY && WoW.PlayerIsChanneling)
                return;
            if (WoW.CanCast(spellName) && WoW.IsSpellInRange(SHADOW_WORD_PAIN))
            {
                WoW.CastSpell(spellName);
                if (WoW.IsSpellOnGCD(spellName))
                    Thread.Sleep(WoW.SpellCooldownTimeRemaining(spellName));
            }
        }

        private void CooldownUsage()
        {
            if (!UseCooldowns)
                return;
            if (PIBloodlustBox.Checked && CheckBloodlust() >= 20)
                SpellCast(POWER_INFUSION);
            if (PIStacksBox.Checked && WoW.PlayerHasBuff(VOIDFORM_AURA) && WoW.PlayerBuffStacks(VOIDFORM_AURA) >= 20)
                SpellCast(POWER_INFUSION);
            if (PIWaitBox.Checked && WoW.IsSpellOnCooldown(SHADOW_FIEND) && WoW.SpellCooldownTimeRemaining(SHADOW_FIEND) > txtPIWait)
                SpellCast(POWER_INFUSION);
            if (SFPIBox.Checked && (WoW.PlayerBuffTimeRemaining(POWER_INFUSION_AURA) == 12 || CheckBloodlust() == 12))
                SpellCast(SHADOW_FIEND);
            if (SFWaitBox.Checked && WoW.IsSpellOnCooldown(POWER_INFUSION) && WoW.SpellCooldownTimeRemaining(POWER_INFUSION) > txtSFWait)
                SpellCast(SHADOW_FIEND);
            if (!MoveCheck() && VoidTorrentRadio1.Checked && DotsUp() && VoidTorrentBox.Checked && WoW.IsSpellOnCooldown(MIND_BLAST) && WoW.IsSpellOnCooldown(VOID_BOLT))
                SpellCast(VOID_TORRENT);
            if (WoW.HealthPercent < txtVE && !WoW.IsSpellOnCooldown(VAMPIRIC_EMBRACE))
            {
                Log.Write("Health below " + txtVE + "% - Using Vampiric Embrace now", Color.Red);
                SpellCast(VAMPIRIC_EMBRACE);
                return;
            }
            if (!WoW.ItemOnCooldown(ITEM_KILJAEDEN) && LegTrinketBox.Checked && DotsUp())
                SpellCast(ITEM_KILJAEDEN);
        }

        private int CheckBloodlust()
        {
            if (WoW.PlayerHasBuff("Bloodlust"))
                return WoW.PlayerBuffTimeRemaining("Bloodlust");
            if (WoW.PlayerHasBuff("Heroism"))
                return WoW.PlayerBuffTimeRemaining("Heroism");
            if (WoW.PlayerHasBuff("Time Warp"))
                return WoW.PlayerBuffTimeRemaining("Time Warp");
            if (WoW.PlayerHasBuff("Ancient Hysteria"))
                return WoW.PlayerBuffTimeRemaining("Ancient Hysteria");
            if (WoW.PlayerHasBuff("Netherwinds"))
                return WoW.PlayerBuffTimeRemaining("Netherwinds");
            if (WoW.PlayerHasBuff("Drums of Fury"))
                return WoW.PlayerBuffTimeRemaining("Drums of Fury");
            return 0;
        }

        private bool DotsUp()
        {
            if (WoW.TargetHasDebuff(SHADOW_WORD_PAIN) && WoW.TargetHasDebuff(VAMPIRIC_TOUCH))
                return true;
            else
                return false;
        }

        private bool MoveCheck()
        {
            if (WoW.PlayerHasBuff(LEG_BOOTS) || !WoW.IsMoving)
                return false;
            else
                return true;
        }
    }
}
/*
[AddonDetails.db]
AddonAuthor=TWonderchild
AddonName=Pawner
WoWVersion=Legion - 70300
[SpellBook.db]
Spell,232698,Shadowform,NumPad9
Spell,34914,Vampiric Touch,D1
Spell,15286,Vampiric Embrace,NumPad6
Spell,589,Shadow Word: Pain,D2
Spell,32379,Shadow Word: Death,X
Spell,8092,Mind Blast,D3
Spell,15407,Mind Flay,D4
Spell,228260,Void Eruption,Q
Spell,205065,Void Torrent,NumPad1
Spell,205448,Void Bolt,Q
Spell,17,Power Word: Shield,D5
Spell,186263,Shadow Mend,D0
Spell,10060,Power Infusion,NumPad2
Spell,34433,Shadowfiend,NumPad3
Spell,15487,Silence,E
Spell,47585,Dispersion,NumPad5
Aura,232698,Shadowform
Aura,194249,Voidform
Aura,211657,Void
Aura,34914,Vampiric Touch
Aura,589,Shadow Word: Pain
Aura,17,Power Word: Shield
Aura,10060,Power Infusion
Aura,197937,Lingering Insanity
Aura,2825,Bloodlust
Aura,32182,Heroism
Aura,80353,Time Warp
Aura,90355,Ancient Hysteria
Aura,160452,Netherwinds
Aura,178207,Drums of Fury
Aura,236430,Norgannon's Foresight
Item,144259,Kil'jaeden's Burning Wish
Spell,144259,Kil'jaeden's Burning Wish,D6
*/
