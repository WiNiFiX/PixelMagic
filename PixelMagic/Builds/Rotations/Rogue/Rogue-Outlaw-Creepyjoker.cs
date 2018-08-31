// ReSharper disable UnusedMember.Global
// ReSharper disable ConvertPropertyToExpressionBody
// ReSharper disable UseStringInterpolation
// ReSharper disable CheckNamespace

using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using PixelMagic.Helpers;

namespace PixelMagic.Rotation
{
    public class Outlaw : CombatRoutine
    {
        private readonly Stopwatch stopwatch = new Stopwatch();
        /*public int GetRolltheBonesBuffs()
    {
        string[] idBuffs = { "Shark Infested Waters", "True Bearing", "Jolly Roger", "Broadsides", "Buried Treasure", "Grand Melee"};
        int buffs = idBuffs;
        for (int i = 0; i < idBuffs.Length; i++)
            if (WoW.PlayerHasBuff(buffs[i]))
                buffs++;
        return buffs;
    } */
        public override Form SettingsForm { get; set; }
        public SettingsFormCJO SettingsFormCJO { get; set; }

        public int GetRolltheBonesBuffs
        {
            get
            {
                string[] idBuffs = {"Shark Infested Waters", "True Bearing", "Jolly Roger", "Broadsides", "Buried Treasure", "Grand Melee"};
                var buffs = 0;
                for (var i = 0; i < idBuffs.Length; i++)
                    if (WoW.PlayerHasBuff(idBuffs[i]))
                        buffs++;
                return buffs;
            }
        }

        /*public string GetRolltheBonesBuffs()
	{
		string[] = new string[] idBuffs { "Shark Infested Waters", "True Bearing", "Jolly Roger", "Broadsides", "Buried Treasure", "Grand Melee"};
		for (i = 0; i < idBuffs.Count; i++)
		{
			if (!WoW.PlayerHasBuff(buffs[i]))
				return;
		}

	}*/


        public static int rtbCount
        {
            get
            {
                var rtb = 0;
                for (var x = 1; x <= 6; x++)
                {
                    var c = WoW.GetPixelColor(5 + x, 3);
                    var e = WoW.GetPixelColor(5 + 5, 3);
                    if ((e.R != 255) && (e.G != 255) && (e.B != 255))
                    {
                        rtb = 2;
                    }
                    if ((c.R != 255) && (c.G != 255) && (c.B != 255))
                    {
                        rtb++;
                    }
                }
                return rtb;
            }
        }

        public override string Name
        {
            get { return "Rogue-Outlaw"; }
        }

        public override string Class
        {
            get { return "Rogue"; }
        }

        private static int cooldownKey
        {
            set { ConfigFile.WriteValue("Outlaw", "cooldownKey", value.ToString()); }
        }

        private static int cooldownModifier
        {
            set { ConfigFile.WriteValue("Outlaw", "cooldownModifier", value.ToString()); }
        }

        private static string cooldownHotKeyString
        {
            get
            {
                var hotKeyString = ConfigFile.ReadValue("Outlaw", "cooldownHotKeyString").Trim();

                if (hotKeyString != "")
                {
                    return hotKeyString;
                }

                return "Click to Set";
            }
            set { ConfigFile.WriteValue("Outlaw", "cooldownHotKeyString", value); }
        }

        private static bool isTalentSND
        {
            get
            {
                var value = ConfigFile.ReadValue("Outlaw", "isTalentSND").Trim();

                return value == "" || Convert.ToBoolean(value);
            }
            set { ConfigFile.WriteValue("Outlaw", "isTalentSND", value.ToString()); }
        }

        private static bool isTalentRTB
        {
            get
            {
                var value = ConfigFile.ReadValue("Outlaw", "isTalentRTB").Trim();
                return value == "" || Convert.ToBoolean(value);
            }
            set { ConfigFile.WriteValue("Outlaw", "isTalentRTB", value.ToString()); }
        }

        private static bool isTalentMFD
        {
            get
            {
                var value = ConfigFile.ReadValue("Outlaw", "isTalentMFD").Trim();
                return value == "" || Convert.ToBoolean(value);
            }
            set { ConfigFile.WriteValue("Outlaw", "isTalentMFD", value.ToString()); }
        }

        private static bool isTalentGhostlySt
        {
            get
            {
                var value = ConfigFile.ReadValue("Outlaw", "isTalentGhostlySt").Trim();
                return value == "" || Convert.ToBoolean(value);
            }
            set { ConfigFile.WriteValue("Outlaw", "isTalentGhostlySt", value.ToString()); }
        }

        private static bool isCheckHotkeysOutlawVial
        {
            get
            {
                var value = ConfigFile.ReadValue("Outlaw", "isCheckHotkeysOutlawVial").Trim();
                return value == "" || Convert.ToBoolean(value);
            }
            set { ConfigFile.WriteValue("Outlaw", "isCheckHotkeysOutlawVial", value.ToString()); }
        }

        private static int RTBEnergy
        {
            get
            {
                var value = ConfigFile.ReadValue("Outlaw", "RTBEnergy").Trim();
                return value != "" ? Convert.ToInt32(value) : -1;
            }
            set { ConfigFile.WriteValue("Outlaw", "RTBEnergy", value.ToString()); }
        }

        private static int ValueRTB
        {
            get
            {
                var value = ConfigFile.ReadValue("Outlaw", "ValueRTB").Trim();
                return value != "" ? Convert.ToInt32(value) : -1;
            }
            set { ConfigFile.WriteValue("Outlaw", "ValueRTB", value.ToString()); }
        }

        private static bool isCheckHotkeysOutlawRiposte
        {
            get
            {
                var value = ConfigFile.ReadValue("Outlaw", "isCheckHotkeysOutlawRiposte").Trim();
                return value == "" || Convert.ToBoolean(value);
            }
            set { ConfigFile.WriteValue("Outlaw", "isCheckHotkeysOutlawRiposte", value.ToString()); }
        }

        private static int RunThroughEnergy
        {
            get
            {
                var value = ConfigFile.ReadValue("Outlaw", "RunThroughEnergy").Trim();
                return value != "" ? Convert.ToInt32(value) : -1;
            }
            set { ConfigFile.WriteValue("Outlaw", "RunThroughEnergy", value.ToString()); }
        }

        private static bool isCheckHotkeysOutlawOffensiveAR
        {
            get
            {
                var value = ConfigFile.ReadValue("Outlaw", "isCheckHotkeysOutlawOffensiveAR").Trim();
                return value == "" || Convert.ToBoolean(value);
            }
            set { ConfigFile.WriteValue("Outlaw", "isCheckHotkeysOutlawOffensiveAR", value.ToString()); }
        }

        private static bool OutlawOffensiveAROnBossOnly
        {
            get
            {
                var value = ConfigFile.ReadValue("Outlaw", "OutlawOffensiveAROnBossOnly").Trim();
                return value == "" || Convert.ToBoolean(value);
            }
            set { ConfigFile.WriteValue("Outlaw", "OutlawOffensiveAROnBossOnly", value.ToString()); }
        }

        private static bool isCheckHotkeysOutlawOffensiveCursedBl
        {
            get
            {
                var value = ConfigFile.ReadValue("Outlaw", "isCheckHotkeysOutlawOffensiveCursedBl").Trim();
                return value == "" || Convert.ToBoolean(value);
            }
            set { ConfigFile.WriteValue("Outlaw", "isCheckHotkeysOutlawOffensiveCursedBl", value.ToString()); }
        }

        private static bool OutlawOffensiveCursedBlOnBossOnly
        {
            get
            {
                var value = ConfigFile.ReadValue("Outlaw", "OutlawOffensiveCursedBlOnBossOnly").Trim();
                return value == "" || Convert.ToBoolean(value);
            }
            set { ConfigFile.WriteValue("Outlaw", "OutlawOffensiveCursedBlOnBossOnly", value.ToString()); }
        }


        public override void Initialize()
        {
            Log.Write("Welcome to CreepyOutlaw rotation. V2.3. Report any issues on #rogue Discord channel with @Creepyjoker tag for further fixes.", Color.Red);
            Log.Write("Suggested build: 1212231");
            SettingsFormCJO = new SettingsFormCJO();
            SettingsForm = SettingsFormCJO;

            SettingsFormCJO.btnHotkeysOutlawOffensiveCooldowns.Text = cooldownHotKeyString;
            SettingsFormCJO.checkTalentGhostlySt.Checked = isTalentGhostlySt;
            SettingsFormCJO.checkTalentSND.Checked = isTalentSND;
            SettingsFormCJO.checkTalentRTB.Checked = isTalentRTB;
            SettingsFormCJO.checkTalentMFD.Checked = isTalentMFD;

            SettingsFormCJO.checkHotkeysOutlawOffensiveAR.Checked = isCheckHotkeysOutlawOffensiveAR;
            SettingsFormCJO.checkHotkeysOutlawOffensiveCursedBl.Checked = isCheckHotkeysOutlawOffensiveCursedBl;
            SettingsFormCJO.checkHotkeysOutlawOffensiveAROnBossOnly.Checked = OutlawOffensiveAROnBossOnly;
            SettingsFormCJO.checkHotkeysOutlawOffensiveCursedBlOnBossOnly.Checked = OutlawOffensiveCursedBlOnBossOnly;
            SettingsFormCJO.checkHotkeysOutlawRiposte.Checked = isCheckHotkeysOutlawRiposte;
            SettingsFormCJO.checkHotkeysOutlawVial.Checked = isCheckHotkeysOutlawVial;
            SettingsFormCJO.checkHotkeysOutlawRTBEnergy.Text = RTBEnergy.ToString();
            SettingsFormCJO.checkHotkeysOutlawRunEnergy.Text = RunThroughEnergy.ToString();
            SettingsFormCJO.checkHotkeysOutlawValueRTB.Text = ValueRTB.ToString();

            SettingsFormCJO.checkTalentGhostlySt.CheckedChanged += isTalentGhostlySt_Click;
            SettingsFormCJO.checkTalentRTB.CheckedChanged += isTalentRTB_Click;
            SettingsFormCJO.checkTalentSND.CheckedChanged += isTalentSND_Click;
            SettingsFormCJO.checkTalentMFD.CheckedChanged += isTalentMFD_Click;
            SettingsFormCJO.checkHotkeysOutlawVial.CheckedChanged += isCheckHotkeysOutlawVial_Click;
            SettingsFormCJO.checkHotkeysOutlawRTBEnergy.TextChanged += isCheckHotkeysOutlawRTBEnergy_Click;
            SettingsFormCJO.checkHotkeysOutlawValueRTB.TextChanged += isCheckHotkeysOutlawValueRTB_Click;
            SettingsFormCJO.checkHotkeysOutlawRiposte.CheckedChanged += isCheckHotkeysOutlawRiposte_Click;
            SettingsFormCJO.checkHotkeysOutlawRunEnergy.TextChanged += isCheckHotkeysOutlawRunEnergy_Click;
            SettingsFormCJO.checkHotkeysOutlawOffensiveCursedBl.CheckedChanged += isCheckHotkeysOutlawOffensiveCursedBl_Click;
            SettingsFormCJO.checkHotkeysOutlawOffensiveAR.CheckedChanged += isCheckHotkeysOutlawOffensiveAR_Click;
            SettingsFormCJO.checkHotkeysOutlawOffensiveCursedBlOnBossOnly.CheckedChanged += OutlawOffensiveCursedBlOnBossOnly_Click;
            SettingsFormCJO.checkHotkeysOutlawOffensiveAROnBossOnly.CheckedChanged += OutlawOffensiveAROnBossOnly_Click;
            SettingsFormCJO.btnHotkeysOutlawOffensiveCooldowns.KeyDown += KeyDown;
        }

        private void KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Menu || e.KeyCode == Keys.ControlKey || e.KeyCode == Keys.ShiftKey)
                return;
            SettingsFormCJO.btnHotkeysOutlawOffensiveCooldowns.Text = "Hotkey : ";
            if (e.Shift)
            {
                cooldownModifier = (int) Keys.ShiftKey;
                SettingsFormCJO.btnHotkeysOutlawOffensiveCooldowns.Text += Keys.Shift + " + ";
            }
            if (e.Alt)
            {
                cooldownModifier = (int) Keys.Menu;
                SettingsFormCJO.btnHotkeysOutlawOffensiveCooldowns.Text += Keys.Alt + " + ";
            }
            if (e.Control)
            {
                cooldownModifier = (int) Keys.ControlKey;
                SettingsFormCJO.btnHotkeysOutlawOffensiveCooldowns.Text += Keys.Control + " + ";
            }
            cooldownKey = (int) e.KeyCode;
            SettingsFormCJO.btnHotkeysOutlawOffensiveCooldowns.Text += e.KeyCode;
            cooldownHotKeyString = SettingsFormCJO.btnHotkeysOutlawOffensiveCooldowns.Text;
            SettingsFormCJO.checkHotkeysOutlawRTBEnergyLabel.Focus();
        }

        private void isCheckHotkeysOutlawVial_Click(object sender, EventArgs e)
        {
            isCheckHotkeysOutlawVial = SettingsFormCJO.checkHotkeysOutlawVial.Checked;
        }

        private void isCheckHotkeysOutlawRTBEnergy_Click(object sender, EventArgs e)
        {
            int userVal;
            if (int.TryParse(SettingsFormCJO.checkHotkeysOutlawRTBEnergy.Text, out userVal) && userVal >= 0 && userVal <= 100)
            {
                RTBEnergy = userVal;
            }
            else
            {
                SettingsFormCJO.checkHotkeysOutlawRTBEnergy.Text = "";
                Log.Write("Enter a number between 0 and 100 in the text box", Color.DarkRed);
            }
        }

        private void isCheckHotkeysOutlawValueRTB_Click(object sender, EventArgs e)
        {
            int userVal;
            if (int.TryParse(SettingsFormCJO.checkHotkeysOutlawValueRTB.Text, out userVal) && userVal >= 0 && userVal <= 6)
            {
                ValueRTB = userVal;
            }
            else
            {
                SettingsFormCJO.checkHotkeysOutlawValueRTB.Text = "";
                Log.Write("Enter a number between 0 and 6 in the text box", Color.DarkRed);
            }
        }

        private void isCheckHotkeysOutlawRiposte_Click(object sender, EventArgs e)
        {
            isCheckHotkeysOutlawRiposte = SettingsFormCJO.checkHotkeysOutlawRiposte.Checked;
        }

        private void isCheckHotkeysOutlawRunEnergy_Click(object sender, EventArgs e)
        {
            int userVal;
            if (int.TryParse(SettingsFormCJO.checkHotkeysOutlawRunEnergy.Text, out userVal) && userVal >= 0 && userVal <= 100)
            {
                RunThroughEnergy = userVal;
            }
            else
            {
                SettingsFormCJO.checkHotkeysOutlawRunEnergy.Text = "";
                Log.Write("Enter a number between 0 and 100 in the text box", Color.DarkRed);
            }
        }

        private void isCheckHotkeysOutlawOffensiveCursedBl_Click(object sender, EventArgs e)
        {
            isCheckHotkeysOutlawOffensiveCursedBl = SettingsFormCJO.checkHotkeysOutlawOffensiveCursedBl.Checked;
        }

        private void isCheckHotkeysOutlawOffensiveAR_Click(object sender, EventArgs e)
        {
            isCheckHotkeysOutlawOffensiveAR = SettingsFormCJO.checkHotkeysOutlawOffensiveAR.Checked;
        }

        private void OutlawOffensiveCursedBlOnBossOnly_Click(object sender, EventArgs e)
        {
            OutlawOffensiveCursedBlOnBossOnly = SettingsFormCJO.checkHotkeysOutlawOffensiveCursedBlOnBossOnly.Checked;
        }

        private void OutlawOffensiveAROnBossOnly_Click(object sender, EventArgs e)
        {
            OutlawOffensiveAROnBossOnly = SettingsFormCJO.checkHotkeysOutlawOffensiveAROnBossOnly.Checked;
        }

        private void isTalentGhostlySt_Click(object sender, EventArgs e)
        {
            isTalentGhostlySt = SettingsFormCJO.checkTalentGhostlySt.Checked;
        }

        private void isTalentRTB_Click(object sender, EventArgs e)
        {
            isTalentRTB = SettingsFormCJO.checkTalentRTB.Checked;
            isTalentSND = !SettingsFormCJO.checkTalentRTB.Checked;
            SettingsFormCJO.checkTalentSND.Checked = isTalentSND;
        }

        private void isTalentSND_Click(object sender, EventArgs e)
        {
            isTalentSND = SettingsFormCJO.checkTalentSND.Checked;
            isTalentRTB = !SettingsFormCJO.checkTalentSND.Checked;
            SettingsFormCJO.checkTalentRTB.Checked = isTalentRTB;
        }

        private void isTalentMFD_Click(object sender, EventArgs e)
        {
            isTalentMFD = SettingsFormCJO.checkTalentMFD.Checked;
            isTalentSND = !SettingsFormCJO.checkTalentMFD.Checked;
            SettingsFormCJO.checkTalentSND.Checked = isTalentSND;
        }

        public override void Stop()
        {
        }

        public override void Pulse()
        {
            if (stopwatch.ElapsedMilliseconds == 0)
            {
                stopwatch.Start();
                Log.WritePixelMagic("The Cooldown toggle button is now Active (Numpad0). The delay is set to 1000ms ( 1 second )", Color.Black);
                return;
            }
            {
                if (DetectKeyPress.GetKeyState(DetectKeyPress.VK_NUMPAD0) < 0)
                {
                    if (stopwatch.ElapsedMilliseconds > 1000)
                    {
                        combatRoutine.UseCooldowns = !combatRoutine.UseCooldowns;
                        stopwatch.Restart();
                    }
                }
                if (combatRoutine.Type == RotationType.SingleTarget)
                {
                    if (WoW.IsInCombat && WoW.HasTarget && WoW.IsSpellInRange("Saber Slash"))
                    {
                        /*if (WoW.CanCast("Blade Flurry") && WoW.PlayerHasBuff("Blade Flurry"))
                {
                    WoW.CastSpell("Blade Flurry");
                    Log.Write("Getting Blade Flurry off *Single Target rotation*");
                    return;
                }*/
                        if (isTalentSND && !isTalentRTB && WoW.CanCast("Slice and Dice") && !WoW.PlayerHasBuff("Slice and Dice") && WoW.Energy >= RTBEnergy && WoW.CurrentComboPoints >= 5 ||
                            isTalentSND && !isTalentRTB && WoW.CanCast("Slice and Dice") && WoW.PlayerHasBuff("Slice and Dice") && WoW.PlayerBuffTimeRemaining("Slice and Dice") <= 10 &&
                            WoW.Energy >= RTBEnergy && WoW.CurrentComboPoints >= 5)
                        {
                            WoW.CastSpell("Slice and Dice");
                            return;
                        }
                        if (isTalentSND && !isTalentRTB && WoW.CanCast("Run Through") && WoW.PlayerHasBuff("Slice and Dice") && WoW.Energy >= RunThroughEnergy && WoW.CurrentComboPoints >= 5 &&
                            WoW.IsSpellInRange("Run Through") && WoW.PlayerBuffTimeRemaining("Slice and Dice") > 10)
                        {
                            WoW.CastSpell("Run Through");
                            return;
                        }
                        if (isTalentRTB && WoW.CanCast("Roll the Bones") && WoW.Energy >= RTBEnergy && WoW.CurrentComboPoints >= 4 && GetRolltheBonesBuffs < ValueRTB &&
                            !WoW.PlayerHasBuff("True Bearing"))
                        {
                            WoW.CastSpell("Roll the Bones");
                            Log.Write("-------Rolling the boooones!-------", Color.Green);
                            return;
                        }
                        if (isTalentRTB && GetRolltheBonesBuffs >= ValueRTB && ValueRTB >= 2 && WoW.CanCast("Run Through") && WoW.Energy >= RunThroughEnergy && WoW.CurrentComboPoints >= 5 &&
                            !WoW.PlayerHasBuff("Killing Spree") && WoW.IsSpellInRange("Run Through") &&
                            (WoW.PlayerHasBuff("True Bearing") && WoW.PlayerHasBuff("Broadsides") && WoW.PlayerBuffTimeRemaining("True Bearing") > 10 ||
                             WoW.PlayerHasBuff("True Bearing") && WoW.PlayerBuffTimeRemaining("True Bearing") > 10 ||
                             WoW.PlayerHasBuff("True Bearing") && WoW.PlayerHasBuff("Buried Treasure") && WoW.PlayerBuffTimeRemaining("True Bearing") > 10 ||
                             WoW.PlayerHasBuff("True Bearing") && WoW.PlayerHasBuff("Shark Infested Waters") && WoW.PlayerBuffTimeRemaining("True Bearing") > 10 ||
                             WoW.PlayerHasBuff("True Bearing") && WoW.PlayerHasBuff("Jolly Roger") && WoW.PlayerBuffTimeRemaining("True Bearing") > 10 ||
                             WoW.PlayerHasBuff("True Bearing") && WoW.PlayerHasBuff("Grand Melee") && WoW.PlayerBuffTimeRemaining("True Bearing") > 10 ||
                             WoW.PlayerHasBuff("Shark Infested Waters") && WoW.PlayerHasBuff("Grand Melee") && WoW.PlayerBuffTimeRemaining("Shark Infested Waters") > 10 ||
                             WoW.PlayerHasBuff("Shark Infested Waters") && WoW.PlayerHasBuff("Broadsides") && WoW.PlayerBuffTimeRemaining("Shark Infested Waters") > 10 ||
                             WoW.PlayerHasBuff("Shark Infested Waters") && WoW.PlayerHasBuff("Jolly Roger") && WoW.PlayerBuffTimeRemaining("Shark Infested Waters") > 10 ||
                             WoW.PlayerHasBuff("Shark Infested Waters") && WoW.PlayerHasBuff("Buried Treasure") && WoW.PlayerBuffTimeRemaining("Shark Infested Waters") > 10 ||
                             WoW.PlayerHasBuff("Broadsides") && WoW.PlayerHasBuff("Jolly Roger") && WoW.PlayerBuffTimeRemaining("Broadsides") > 10 ||
                             WoW.PlayerHasBuff("Broadsides") && WoW.PlayerHasBuff("Grand Melee") && WoW.PlayerBuffTimeRemaining("Broadsides") > 10 ||
                             WoW.PlayerHasBuff("Broadsides") && WoW.PlayerHasBuff("Buried Treasure") && WoW.PlayerBuffTimeRemaining("Broadsides") > 10 ||
                             WoW.PlayerHasBuff("Jolly Roger") && WoW.PlayerHasBuff("Grand Melee") && WoW.PlayerBuffTimeRemaining("Jolly Roger") > 10 ||
                             WoW.PlayerHasBuff("Jolly Roger") && WoW.PlayerHasBuff("Buried Treasure") && WoW.PlayerBuffTimeRemaining("Jolly Roger") > 10 ||
                             WoW.PlayerHasBuff("Grand Melee") && WoW.PlayerHasBuff("Buried Treasure") && WoW.PlayerBuffTimeRemaining("Grand Melee") > 10))
                        {
                            WoW.CastSpell("Run Through");
                            return;
                        }
                        if (isTalentRTB && ValueRTB >= 2 && WoW.CanCast("Run Through") && WoW.Energy >= RunThroughEnergy && WoW.CurrentComboPoints >= 5 && WoW.PlayerHasBuff("True Bearing") &&
                            WoW.PlayerBuffTimeRemaining("True Bearing") >= 10 && !WoW.PlayerHasBuff("Killing Spree") && WoW.IsSpellInRange("Run Through"))
                        {
                            WoW.CastSpell("Run Through");
                            return;
                        }
                        if (isTalentRTB && ValueRTB <= 1 && WoW.CanCast("Run Through") && WoW.Energy >= RunThroughEnergy && WoW.CurrentComboPoints >= 5 && !WoW.PlayerHasBuff("Killing Spree") &&
                            WoW.IsSpellInRange("Run Through") &&
                            (WoW.PlayerHasBuff("True Bearing") && WoW.PlayerBuffTimeRemaining("True Bearing") > 10 ||
                             WoW.PlayerHasBuff("Jolly Roger") && WoW.PlayerBuffTimeRemaining("Jolly Roger") > 10 ||
                             WoW.PlayerHasBuff("Shark Infested Waters") && WoW.PlayerBuffTimeRemaining("Shark Infested Waters") > 10 ||
                             WoW.PlayerHasBuff("Grand Melee") && WoW.PlayerBuffTimeRemaining("Grand Melee") > 10 ||
                             WoW.PlayerHasBuff("Buried Treasure") && WoW.PlayerBuffTimeRemaining("Buried Treasure") > 10 ||
                             WoW.PlayerHasBuff("Broadsides") && WoW.PlayerBuffTimeRemaining("Broadsides") > 10))

                        {
                            WoW.CastSpell("Run Through");
                            return;
                        }

                        /*if (WoW.CanCast("Run Through") && WoW.Energy > 26 && WoW.CurrentComboPoints >= 5 && !WoW.PlayerHasBuff("Killing Spree") && GetRolltheBonesBuffs >= 1 && WoW.PlayerBuffTimeRemaining(GetRolltheBonesBuffs) > 10 )
				{
					WoW.CastSpell("Run Through");
					return;
				} */

                        if (!WoW.PlayerHasBuff("Killing Spree") && WoW.IsInCombat && WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsSpellInRange("Run Through"))
                        {
                            if (isTalentGhostlySt && WoW.Energy >= 30 && WoW.CanCast("Ghostly Strike") && WoW.CurrentComboPoints < 5 && !WoW.TargetHasDebuff("Ghostly Strike") ||
                                isTalentGhostlySt && WoW.CanCast("Ghostly Strike") && WoW.TargetDebuffTimeRemaining("Ghostly Strike") <= 3 && WoW.Energy >= 30 && WoW.CurrentComboPoints < 5)
                            {
                                WoW.CastSpell("Ghostly Strike");
                                return;
                            }


                            if (isTalentRTB && WoW.CanCast("Roll the Bones") && WoW.CurrentComboPoints >= 4 && WoW.Energy > RTBEnergy &&
                                (WoW.PlayerHasBuff("True Bearing") && WoW.PlayerBuffTimeRemaining("True Bearing") < 10 ||
                                 WoW.PlayerHasBuff("Shark Infested Waters") && WoW.PlayerBuffTimeRemaining("Shark Infested Waters") < 10 ||
                                 WoW.PlayerHasBuff("Jolly Roger") && WoW.PlayerBuffTimeRemaining("Jolly Roger") < 10 ||
                                 WoW.PlayerHasBuff("Grand Melee") && WoW.PlayerBuffTimeRemaining("Grand Melee") < 10 ||
                                 WoW.PlayerHasBuff("Buried Treasure") && WoW.PlayerBuffTimeRemaining("Buried Treasure") < 10 ||
                                 WoW.PlayerHasBuff("Broadsides") && WoW.PlayerBuffTimeRemaining("Broadsides") < 10))
                            {
                                WoW.CastSpell("Roll the Bones");
                                Log.Write("-------Rerolling the bones!--------", Color.Green);
                                return;
                            }


                            if (WoW.CanCast("Pistol Shot") && WoW.PlayerHasBuff("Opportunity") && WoW.CurrentComboPoints < 5)
                            {
                                WoW.CastSpell("Pistol Shot");
                                return;
                            }
                            if (isTalentGhostlySt && WoW.CanCast("Saber Slash") && !WoW.PlayerHasBuff("Killing Spree") && WoW.Energy >= 50 && WoW.CurrentComboPoints < 5 &&
                                !WoW.PlayerHasBuff("Opportunity") && WoW.TargetHasDebuff("Ghostly Strike"))
                            {
                                WoW.CastSpell("Saber Slash");

                                return;
                            }
                            if (!isTalentGhostlySt && WoW.CanCast("Saber Slash") && !WoW.PlayerHasBuff("Killing Spree") && WoW.Energy >= 50 && WoW.CurrentComboPoints < 5 &&
                                !WoW.PlayerHasBuff("Opportunity"))
                            {
                                WoW.CastSpell("Saber Slash");

                                return;
                            }
                            /*if (WoW.IsSpellKnown("Death from Above") && WoW.IsSpellOnCooldown("Adrenaline Rush") && WoW.CurrentComboPoints >= 5 && !WoW.IsSpellOnCooldown("Death from Above") && WoW.Energy >= 25 )
					{
						WoW.CastSpell("Death from Above"); // No talent check implemented yet.
						return;
					} */
                            if (isTalentMFD && !WoW.IsSpellOnCooldown("Marked for Death") && WoW.CurrentComboPoints <= 1)
                            {
                                WoW.CastSpell("Marked for Death");
                                return;
                            }
                            /*if (!WoW.IsSpellOnCooldown("Killing Spree") && !WoW.PlayerHasBuff("Adrenaline Rush") && GetRolltheBonesBuffs >= 2  )
                    {
                        WoW.CastSpell("Killing Spree");
                        return;
                    }*/
                            if (WoW.PlayerHasBuff("Adrenaline Rush") && WoW.PlayerBuffTimeRemaining("Adrenaline Rush") > 4 && !WoW.IsSpellOnCooldown("Curse of Dreadblades"))
                                // Curse of dreadblades + AR check
                            {
                                WoW.CastSpell("Curse of Dreadblades");
                                return;
                            }
                            if (isTalentRTB && isCheckHotkeysOutlawOffensiveAR && !WoW.IsSpellOnCooldown("Adrenaline Rush") && GetRolltheBonesBuffs >= 2 && UseCooldowns)
                            {
                                WoW.CastSpell("Adrenaline Rush");
                                return;
                            }
                            if (isTalentSND && isCheckHotkeysOutlawOffensiveAR && !WoW.IsSpellOnCooldown("Adrenaline Rush") && UseCooldowns && WoW.PlayerHasBuff("Slice and Dice") &&
                                WoW.PlayerBuffTimeRemaining("Slice and Dice") > 15)
                            {
                                WoW.CastSpell("Adrenaline Rush");
                                return;
                            }
                        }
                    }
                }


                // AoE Rotation.


                if (combatRoutine.Type == RotationType.AOE)
                {
                    if (WoW.IsInCombat && WoW.HasTarget && WoW.IsSpellInRange("Saber Slash"))
                    {
                        /*if (WoW.CanCast("Blade Flurry") && WoW.PlayerHasBuff("Blade Flurry"))
                {
                    WoW.CastSpell("Blade Flurry");
                    Log.Write("Getting Blade Flurry off *Single Target rotation*");
                    return;
                }*/
                        if (isTalentSND && !isTalentRTB && WoW.CanCast("Slice and Dice") && !WoW.PlayerHasBuff("Slice and Dice") && WoW.Energy >= RTBEnergy && WoW.CurrentComboPoints >= 5 ||
                            isTalentSND && !isTalentRTB && WoW.CanCast("Slice and Dice") && WoW.PlayerHasBuff("Slice and Dice") && WoW.PlayerBuffTimeRemaining("Slice and Dice") <= 10 &&
                            WoW.Energy >= RTBEnergy && WoW.CurrentComboPoints >= 5)
                        {
                            WoW.CastSpell("Slice and Dice");
                            return;
                        }
                        if (isTalentSND && !isTalentRTB && WoW.CanCast("Run Through") && WoW.PlayerHasBuff("Slice and Dice") && WoW.Energy >= RunThroughEnergy && WoW.CurrentComboPoints >= 5 &&
                            WoW.IsSpellInRange("Run Through") && WoW.PlayerBuffTimeRemaining("Slice and Dice") > 10)
                        {
                            WoW.CastSpell("Run Through");
                            return;
                        }
                        if (isTalentRTB && WoW.CanCast("Roll the Bones") && WoW.Energy >= RTBEnergy && WoW.CurrentComboPoints >= 4 && GetRolltheBonesBuffs < ValueRTB &&
                            !WoW.PlayerHasBuff("True Bearing"))
                        {
                            WoW.CastSpell("Roll the Bones");
                            Log.Write("-------Rolling the boooones!-------", Color.Green);
                            return;
                        }
                        if (isTalentRTB && GetRolltheBonesBuffs >= ValueRTB && ValueRTB >= 2 && WoW.CanCast("Run Through") && WoW.Energy >= RunThroughEnergy && WoW.CurrentComboPoints >= 5 &&
                            !WoW.PlayerHasBuff("Killing Spree") && WoW.IsSpellInRange("Run Through") &&
                            (WoW.PlayerHasBuff("True Bearing") && WoW.PlayerHasBuff("Broadsides") && WoW.PlayerBuffTimeRemaining("True Bearing") > 10 ||
                             WoW.PlayerHasBuff("True Bearing") && WoW.PlayerBuffTimeRemaining("True Bearing") > 10 ||
                             WoW.PlayerHasBuff("True Bearing") && WoW.PlayerHasBuff("Buried Treasure") && WoW.PlayerBuffTimeRemaining("True Bearing") > 10 ||
                             WoW.PlayerHasBuff("True Bearing") && WoW.PlayerHasBuff("Shark Infested Waters") && WoW.PlayerBuffTimeRemaining("True Bearing") > 10 ||
                             WoW.PlayerHasBuff("True Bearing") && WoW.PlayerHasBuff("Jolly Roger") && WoW.PlayerBuffTimeRemaining("True Bearing") > 10 ||
                             WoW.PlayerHasBuff("True Bearing") && WoW.PlayerHasBuff("Grand Melee") && WoW.PlayerBuffTimeRemaining("True Bearing") > 10 ||
                             WoW.PlayerHasBuff("Shark Infested Waters") && WoW.PlayerHasBuff("Grand Melee") && WoW.PlayerBuffTimeRemaining("Shark Infested Waters") > 10 ||
                             WoW.PlayerHasBuff("Shark Infested Waters") && WoW.PlayerHasBuff("Broadsides") && WoW.PlayerBuffTimeRemaining("Shark Infested Waters") > 10 ||
                             WoW.PlayerHasBuff("Shark Infested Waters") && WoW.PlayerHasBuff("Jolly Roger") && WoW.PlayerBuffTimeRemaining("Shark Infested Waters") > 10 ||
                             WoW.PlayerHasBuff("Shark Infested Waters") && WoW.PlayerHasBuff("Buried Treasure") && WoW.PlayerBuffTimeRemaining("Shark Infested Waters") > 10 ||
                             WoW.PlayerHasBuff("Broadsides") && WoW.PlayerHasBuff("Jolly Roger") && WoW.PlayerBuffTimeRemaining("Broadsides") > 10 ||
                             WoW.PlayerHasBuff("Broadsides") && WoW.PlayerHasBuff("Grand Melee") && WoW.PlayerBuffTimeRemaining("Broadsides") > 10 ||
                             WoW.PlayerHasBuff("Broadsides") && WoW.PlayerHasBuff("Buried Treasure") && WoW.PlayerBuffTimeRemaining("Broadsides") > 10 ||
                             WoW.PlayerHasBuff("Jolly Roger") && WoW.PlayerHasBuff("Grand Melee") && WoW.PlayerBuffTimeRemaining("Jolly Roger") > 10 ||
                             WoW.PlayerHasBuff("Jolly Roger") && WoW.PlayerHasBuff("Buried Treasure") && WoW.PlayerBuffTimeRemaining("Jolly Roger") > 10 ||
                             WoW.PlayerHasBuff("Grand Melee") && WoW.PlayerHasBuff("Buried Treasure") && WoW.PlayerBuffTimeRemaining("Grand Melee") > 10))
                        {
                            WoW.CastSpell("Run Through");
                            return;
                        }
                        if (isTalentRTB && ValueRTB >= 2 && WoW.CanCast("Run Through") && WoW.Energy >= RunThroughEnergy && WoW.CurrentComboPoints >= 5 && WoW.PlayerHasBuff("True Bearing") &&
                            WoW.PlayerBuffTimeRemaining("True Bearing") >= 10 && !WoW.PlayerHasBuff("Killing Spree") && WoW.IsSpellInRange("Run Through"))
                        {
                            WoW.CastSpell("Run Through");
                            return;
                        }
                        if (isTalentRTB && ValueRTB <= 1 && WoW.CanCast("Run Through") && WoW.Energy >= RunThroughEnergy && WoW.CurrentComboPoints >= 5 && !WoW.PlayerHasBuff("Killing Spree") &&
                            WoW.IsSpellInRange("Run Through") &&
                            (WoW.PlayerHasBuff("True Bearing") && WoW.PlayerBuffTimeRemaining("True Bearing") > 10 ||
                             WoW.PlayerHasBuff("Jolly Roger") && WoW.PlayerBuffTimeRemaining("Jolly Roger") > 10 ||
                             WoW.PlayerHasBuff("Shark Infested Waters") && WoW.PlayerBuffTimeRemaining("Shark Infested Waters") > 10 ||
                             WoW.PlayerHasBuff("Grand Melee") && WoW.PlayerBuffTimeRemaining("Grand Melee") > 10 ||
                             WoW.PlayerHasBuff("Buried Treasure") && WoW.PlayerBuffTimeRemaining("Buried Treasure") > 10 ||
                             WoW.PlayerHasBuff("Broadsides") && WoW.PlayerBuffTimeRemaining("Broadsides") > 10))

                        {
                            WoW.CastSpell("Run Through");
                            return;
                        }

                        /*if (WoW.CanCast("Run Through") && WoW.Energy > 26 && WoW.CurrentComboPoints >= 5 && !WoW.PlayerHasBuff("Killing Spree") && GetRolltheBonesBuffs >= 1 && WoW.PlayerBuffTimeRemaining(GetRolltheBonesBuffs) > 10 )
				{
					WoW.CastSpell("Run Through");
					return;
				} */

                        if (!WoW.PlayerHasBuff("Killing Spree") && WoW.IsInCombat && WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsSpellInRange("Run Through"))
                        {
                            if (isTalentGhostlySt && WoW.Energy >= 30 && WoW.CanCast("Ghostly Strike") && WoW.CurrentComboPoints < 5 && !WoW.TargetHasDebuff("Ghostly Strike") ||
                                isTalentGhostlySt && WoW.CanCast("Ghostly Strike") && WoW.TargetDebuffTimeRemaining("Ghostly Strike") <= 3 && WoW.Energy >= 30 && WoW.CurrentComboPoints < 5)
                            {
                                WoW.CastSpell("Ghostly Strike");
                                return;
                            }


                            if (isTalentRTB && WoW.CanCast("Roll the Bones") && WoW.CurrentComboPoints >= 4 && WoW.Energy > RTBEnergy &&
                                (WoW.PlayerHasBuff("True Bearing") && WoW.PlayerBuffTimeRemaining("True Bearing") < 10 ||
                                 WoW.PlayerHasBuff("Shark Infested Waters") && WoW.PlayerBuffTimeRemaining("Shark Infested Waters") < 10 ||
                                 WoW.PlayerHasBuff("Jolly Roger") && WoW.PlayerBuffTimeRemaining("Jolly Roger") < 10 ||
                                 WoW.PlayerHasBuff("Grand Melee") && WoW.PlayerBuffTimeRemaining("Grand Melee") < 10 ||
                                 WoW.PlayerHasBuff("Buried Treasure") && WoW.PlayerBuffTimeRemaining("Buried Treasure") < 10 ||
                                 WoW.PlayerHasBuff("Broadsides") && WoW.PlayerBuffTimeRemaining("Broadsides") < 10))
                            {
                                WoW.CastSpell("Roll the Bones");
                                Log.Write("-------Rerolling the bones!--------", Color.Green);
                                return;
                            }


                            if (WoW.CanCast("Pistol Shot") && WoW.PlayerHasBuff("Opportunity") && WoW.CurrentComboPoints < 5)
                            {
                                WoW.CastSpell("Pistol Shot");
                                return;
                            }
                            if (isTalentGhostlySt && WoW.CanCast("Saber Slash") && !WoW.PlayerHasBuff("Killing Spree") && WoW.Energy >= 50 && WoW.CurrentComboPoints < 5 &&
                                !WoW.PlayerHasBuff("Opportunity") && WoW.TargetHasDebuff("Ghostly Strike"))
                            {
                                WoW.CastSpell("Saber Slash");

                                return;
                            }
                            if (!isTalentGhostlySt && WoW.CanCast("Saber Slash") && !WoW.PlayerHasBuff("Killing Spree") && WoW.Energy >= 50 && WoW.CurrentComboPoints < 5 &&
                                !WoW.PlayerHasBuff("Opportunity"))
                            {
                                WoW.CastSpell("Saber Slash");

                                return;
                            }
                            /*if (WoW.IsSpellKnown("Death from Above") && WoW.IsSpellOnCooldown("Adrenaline Rush") && WoW.CurrentComboPoints >= 5 && !WoW.IsSpellOnCooldown("Death from Above") && WoW.Energy >= 25 )
					{
						WoW.CastSpell("Death from Above"); // No talent check implemented yet.
						return;
					} */
                            if (isTalentMFD && !WoW.IsSpellOnCooldown("Marked for Death") && WoW.CurrentComboPoints <= 1)
                            {
                                WoW.CastSpell("Marked for Death");
                                return;
                            }
                            /*if (!WoW.IsSpellOnCooldown("Killing Spree") && !WoW.PlayerHasBuff("Adrenaline Rush") && GetRolltheBonesBuffs >= 2  )
                    {
                        WoW.CastSpell("Killing Spree");
                        return;
                    }*/
                            if (WoW.PlayerHasBuff("Adrenaline Rush") && WoW.PlayerBuffTimeRemaining("Adrenaline Rush") > 4 && !WoW.IsSpellOnCooldown("Curse of Dreadblades"))
                                // Curse of dreadblades + AR check
                            {
                                WoW.CastSpell("Curse of Dreadblades");
                                return;
                            }
                            if (isTalentRTB && isCheckHotkeysOutlawOffensiveAR && !WoW.IsSpellOnCooldown("Adrenaline Rush") && GetRolltheBonesBuffs >= 2 && UseCooldowns)
                            {
                                WoW.CastSpell("Adrenaline Rush");
                                return;
                            }
                            if (isTalentSND && isCheckHotkeysOutlawOffensiveAR && !WoW.IsSpellOnCooldown("Adrenaline Rush") && UseCooldowns && WoW.PlayerHasBuff("Slice and Dice") &&
                                WoW.PlayerBuffTimeRemaining("Slice and Dice") > 15)
                            {
                                WoW.CastSpell("Adrenaline Rush");
                            }
                        }
                    }
                }
            }
        }
    }

    public class SettingsFormCJO : Form
    {
        private readonly Label checkHotkeysOutlawRunEnergyLabel;

        private readonly GroupBox groupBox12;
        private readonly GroupBox groupBox13;
        private readonly GroupBox groupBox22;
        private readonly TabControl tabControl3;

        private readonly TabPage tabPage5;
        public Button btnHotkeysOutlawOffensiveCooldowns;
        public CheckBox checkHotkeysOutlawOffensiveAR;
        public CheckBox checkHotkeysOutlawOffensiveAROnBossOnly;
        public CheckBox checkHotkeysOutlawOffensiveCursedBl;
        public CheckBox checkHotkeysOutlawOffensiveCursedBlOnBossOnly;
        public CheckBox checkHotkeysOutlawRiposte;
        public TextBox checkHotkeysOutlawRTBEnergy;
        public Label checkHotkeysOutlawRTBEnergyLabel;
        public TextBox checkHotkeysOutlawRunEnergy;
        public TextBox checkHotkeysOutlawValueRTB;
        public Label checkHotkeysOutlawValueRTBLabel;
        public CheckBox checkHotkeysOutlawVial;
        public CheckBox checkTalentGhostlySt;
        public CheckBox checkTalentMFD;
        public CheckBox checkTalentRTB;
        public CheckBox checkTalentSND;
        public TabPage tabPage2;

        #region Windows Form Designer generated code

        /// <summary>
        ///     Required method for Designer support - do not modify
        ///     the contents of this method with the code editor.
        /// </summary>
        public SettingsFormCJO()
        {
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.groupBox22 = new System.Windows.Forms.GroupBox();
            this.checkTalentMFD = new System.Windows.Forms.CheckBox();
            this.checkTalentSND = new System.Windows.Forms.CheckBox();
            this.checkTalentRTB = new System.Windows.Forms.CheckBox();
            this.checkTalentGhostlySt = new System.Windows.Forms.CheckBox();
            this.checkHotkeysOutlawVial = new System.Windows.Forms.CheckBox();
            this.checkHotkeysOutlawRiposte = new System.Windows.Forms.CheckBox();
            this.checkHotkeysOutlawRTBEnergy = new System.Windows.Forms.TextBox();
            this.checkHotkeysOutlawValueRTB = new System.Windows.Forms.TextBox();
            this.checkHotkeysOutlawValueRTBLabel = new System.Windows.Forms.Label();
            this.checkHotkeysOutlawRTBEnergyLabel = new System.Windows.Forms.Label();
            this.checkHotkeysOutlawRunEnergy = new System.Windows.Forms.TextBox();
            this.checkHotkeysOutlawRunEnergyLabel = new System.Windows.Forms.Label();
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.checkHotkeysOutlawOffensiveAR = new System.Windows.Forms.CheckBox();
            this.checkHotkeysOutlawOffensiveCursedBl = new System.Windows.Forms.CheckBox();
            this.checkHotkeysOutlawOffensiveAROnBossOnly = new System.Windows.Forms.CheckBox();
            this.checkHotkeysOutlawOffensiveCursedBlOnBossOnly = new System.Windows.Forms.CheckBox();
            this.btnHotkeysOutlawOffensiveCooldowns = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabControl3 = new System.Windows.Forms.TabControl();
            this.tabPage5.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.groupBox22.SuspendLayout();
            this.groupBox13.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabControl3.SuspendLayout();
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.groupBox12);
            this.tabPage5.Controls.Add(this.groupBox13);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(582, 406);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Hotkeys";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // groupBox22
            // 
            this.groupBox22.Controls.Add(this.checkTalentMFD);
            this.groupBox22.Controls.Add(this.checkTalentSND);
            this.groupBox22.Controls.Add(this.checkTalentRTB);
            this.groupBox22.Controls.Add(this.checkTalentGhostlySt);
            this.groupBox22.Location = new System.Drawing.Point(8, 8);
            this.groupBox22.Name = "groupBox22";
            this.groupBox22.Size = new System.Drawing.Size(561, 80);
            this.groupBox22.TabIndex = 2;
            this.groupBox22.TabStop = false;
            this.groupBox22.Text = "Talents";
            // 
            // checkTalentMFD
            // 
            this.checkTalentMFD.AutoSize = true;
            this.checkTalentMFD.Location = new System.Drawing.Point(300, 28);
            this.checkTalentMFD.Name = "checkTalentMFD";
            this.checkTalentMFD.Size = new System.Drawing.Size(200, 28);
            this.checkTalentMFD.TabIndex = 9;
            this.checkTalentMFD.Text = "Marked for Death";
            this.checkTalentMFD.UseVisualStyleBackColor = true;
            //
            // checkTalentRTB
            //
            this.checkTalentRTB.AutoSize = true;
            this.checkTalentRTB.Location = new System.Drawing.Point(300, 50);
            this.checkTalentRTB.Name = "checkTalentRTB";
            this.checkTalentRTB.Size = new System.Drawing.Size(200, 28);
            this.checkTalentRTB.TabIndex = 9;
            this.checkTalentRTB.Text = "Roll The Bones";
            this.checkTalentRTB.UseVisualStyleBackColor = true;
            //
            // checkTalentSND
            // 
            this.checkTalentSND.AutoSize = true;
            this.checkTalentSND.Location = new System.Drawing.Point(8, 28);
            this.checkTalentSND.Name = "checkTalentSND";
            this.checkTalentSND.Size = new System.Drawing.Size(100, 28);
            this.checkTalentSND.TabIndex = 9;
            this.checkTalentSND.Text = "SnD Talent";
            this.checkTalentSND.UseVisualStyleBackColor = true;
            // 
            // checkTalentGhostlySt
            // 
            this.checkTalentGhostlySt.AutoSize = true;
            this.checkTalentGhostlySt.Location = new System.Drawing.Point(8, 50);
            this.checkTalentGhostlySt.Name = "checkTalentGhostlySt";
            this.checkTalentGhostlySt.Size = new System.Drawing.Size(100, 28);
            this.checkTalentGhostlySt.TabIndex = 9;
            this.checkTalentGhostlySt.Text = "Ghostly";
            this.checkTalentGhostlySt.UseVisualStyleBackColor = true;
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.checkHotkeysOutlawVial);
            this.groupBox12.Controls.Add(this.checkHotkeysOutlawRTBEnergy);
            this.groupBox12.Controls.Add(this.checkHotkeysOutlawRTBEnergyLabel);
            this.groupBox12.Controls.Add(this.checkHotkeysOutlawValueRTB);
            this.groupBox12.Controls.Add(this.checkHotkeysOutlawValueRTBLabel);
            this.groupBox12.Controls.Add(this.checkHotkeysOutlawRiposte);
            this.groupBox12.Controls.Add(this.checkHotkeysOutlawRunEnergy);
            this.groupBox12.Controls.Add(this.checkHotkeysOutlawRunEnergyLabel);

            this.groupBox12.Location = new System.Drawing.Point(8, 100);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(561, 170);
            this.groupBox12.TabIndex = 2;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "Energy costs modifications";
            // 
            // checkHotkeysOutlawVial
            // 
            this.checkHotkeysOutlawVial.AutoSize = true;
            this.checkHotkeysOutlawVial.Location = new System.Drawing.Point(151, 28);
            this.checkHotkeysOutlawVial.Name = "checkHotkeysOutlawVial";
            this.checkHotkeysOutlawVial.Size = new System.Drawing.Size(100, 28);
            this.checkHotkeysOutlawVial.TabIndex = 9;
            this.checkHotkeysOutlawVial.Text = "RTB/SND";
            this.checkHotkeysOutlawVial.UseVisualStyleBackColor = true;
            // 
            // checkHotkeysOutlawRTBEnergy
            // 
            this.checkHotkeysOutlawRTBEnergy.AutoSize = true;
            this.checkHotkeysOutlawRTBEnergy.Location = new System.Drawing.Point(300, 28);
            this.checkHotkeysOutlawRTBEnergy.Name = "checkHotkeysOutlawRTBEnergy";
            this.checkHotkeysOutlawRTBEnergy.Size = new System.Drawing.Size(20, 28);
            this.checkHotkeysOutlawRTBEnergy.TabIndex = 9;
            this.checkHotkeysOutlawRTBEnergy.Text = "19";
            // 
            // checkHotkeysOutlawRTBEnergy
            // 
            this.checkHotkeysOutlawValueRTB.AutoSize = true;
            this.checkHotkeysOutlawValueRTB.Location = new System.Drawing.Point(300, 72);
            this.checkHotkeysOutlawValueRTB.Name = "checkHotkeysOutlawValueRTB";
            this.checkHotkeysOutlawValueRTB.Size = new System.Drawing.Size(20, 28);
            this.checkHotkeysOutlawValueRTB.TabIndex = 9;
            this.checkHotkeysOutlawValueRTB.Text = "19";

            // auudhiahidauasd
            this.checkHotkeysOutlawValueRTBLabel.AutoSize = true;
            this.checkHotkeysOutlawValueRTBLabel.Location = new System.Drawing.Point(321, 73);
            this.checkHotkeysOutlawValueRTBLabel.Name = "checkHotkeysOutlawValueRTBLabel";
            this.checkHotkeysOutlawValueRTBLabel.Size = new System.Drawing.Size(20, 28);
            this.checkHotkeysOutlawValueRTBLabel.TabIndex = 9;
            this.checkHotkeysOutlawValueRTBLabel.Text = "Value RTB";
            // checkHotkeysOutlawRTBEnergy
            // 
            this.checkHotkeysOutlawRTBEnergyLabel.AutoSize = true;
            this.checkHotkeysOutlawRTBEnergyLabel.Location = new System.Drawing.Point(321, 30);
            this.checkHotkeysOutlawRTBEnergyLabel.Name = "checkHotkeysOutlawRTBEnergyLabel";
            this.checkHotkeysOutlawRTBEnergyLabel.Size = new System.Drawing.Size(20, 28);
            this.checkHotkeysOutlawRTBEnergyLabel.TabIndex = 9;
            this.checkHotkeysOutlawRTBEnergyLabel.Text = "Energy";

            // 
            // checkHotkeysOutlawRiposte
            // 
            this.checkHotkeysOutlawRiposte.AutoSize = true;
            this.checkHotkeysOutlawRiposte.Location = new System.Drawing.Point(151, 50);
            this.checkHotkeysOutlawRiposte.Name = "checkHotkeysOutlawRiposte";
            this.checkHotkeysOutlawRiposte.Size = new System.Drawing.Size(104, 17);
            this.checkHotkeysOutlawRiposte.TabIndex = 8;
            this.checkHotkeysOutlawRiposte.Text = "RunThrough";
            this.checkHotkeysOutlawRiposte.UseVisualStyleBackColor = true;
            // 
            // checkHotkeysOutlawRunEnergy
            // 
            this.checkHotkeysOutlawRunEnergy.AutoSize = true;
            this.checkHotkeysOutlawRunEnergy.Location = new System.Drawing.Point(300, 50);
            this.checkHotkeysOutlawRunEnergy.Name = "checkHotkeysOutlawRTBEnergy";
            this.checkHotkeysOutlawRunEnergy.Size = new System.Drawing.Size(20, 28);
            this.checkHotkeysOutlawRunEnergy.TabIndex = 9;
            this.checkHotkeysOutlawRunEnergy.Text = "29";
            // 
            // checkHotkeysOutlawRunEnergyLabel
            // 
            this.checkHotkeysOutlawRunEnergyLabel.AutoSize = true;
            this.checkHotkeysOutlawRunEnergyLabel.Location = new System.Drawing.Point(321, 52);
            this.checkHotkeysOutlawRunEnergyLabel.Name = "checkHotkeysOutlawRunEnergyLabel";
            this.checkHotkeysOutlawRunEnergyLabel.Size = new System.Drawing.Size(20, 28);
            this.checkHotkeysOutlawRunEnergyLabel.TabIndex = 9;
            this.checkHotkeysOutlawRunEnergyLabel.Text = "Energy";
            // 
            // groupBox13
            // 
            this.groupBox13.Controls.Add(this.checkHotkeysOutlawOffensiveAR);
            this.groupBox13.Controls.Add(this.checkHotkeysOutlawOffensiveCursedBl);
            this.groupBox13.Controls.Add(this.checkHotkeysOutlawOffensiveAROnBossOnly);
            this.groupBox13.Controls.Add(this.checkHotkeysOutlawOffensiveCursedBlOnBossOnly);
            this.groupBox13.Controls.Add(this.btnHotkeysOutlawOffensiveCooldowns);
            this.groupBox13.Location = new System.Drawing.Point(8, 8);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Size = new System.Drawing.Size(556, 90);
            this.groupBox13.TabIndex = 3;
            this.groupBox13.TabStop = false;
            this.groupBox13.Text = "Offensive Cooldowns";

            // 
            // checkHotkeysOutlawOffensiveAR
            // 
            this.checkHotkeysOutlawOffensiveAR.AutoSize = true;
            this.checkHotkeysOutlawOffensiveAR.Location = new System.Drawing.Point(151, 60);
            this.checkHotkeysOutlawOffensiveAR.Name = "checkHotkeysOutlawOffensiveAR";
            this.checkHotkeysOutlawOffensiveAR.Size = new System.Drawing.Size(48, 17);
            this.checkHotkeysOutlawOffensiveAR.TabIndex = 3;
            this.checkHotkeysOutlawOffensiveAR.Text = "AdrenalineR";
            this.checkHotkeysOutlawOffensiveAR.UseVisualStyleBackColor = true;

            // 
            // checkHotkeysOutlawOffensiveAROnBossOnly
            // 
            this.checkHotkeysOutlawOffensiveAROnBossOnly.AutoSize = true;
            this.checkHotkeysOutlawOffensiveAROnBossOnly.Location = new System.Drawing.Point(260, 60);
            this.checkHotkeysOutlawOffensiveAROnBossOnly.Name = "checkHotkeysOutlawOffensiveAROnBossOnly";
            this.checkHotkeysOutlawOffensiveAROnBossOnly.Size = new System.Drawing.Size(48, 17);
            this.checkHotkeysOutlawOffensiveAROnBossOnly.TabIndex = 3;
            this.checkHotkeysOutlawOffensiveAROnBossOnly.Text = "Boss ( NYI )";
            this.checkHotkeysOutlawOffensiveAROnBossOnly.UseVisualStyleBackColor = true;

            // 
            // checkHotkeysOutlawOffensiveCursedBl
            // 
            this.checkHotkeysOutlawOffensiveCursedBl.AutoSize = true;
            this.checkHotkeysOutlawOffensiveCursedBl.Location = new System.Drawing.Point(151, 32);
            this.checkHotkeysOutlawOffensiveCursedBl.Name = "checkHotkeysOutlawOffensiveCursedBl";
            this.checkHotkeysOutlawOffensiveCursedBl.Size = new System.Drawing.Size(99, 17);
            this.checkHotkeysOutlawOffensiveCursedBl.TabIndex = 2;
            this.checkHotkeysOutlawOffensiveCursedBl.Text = "Cursed Blade";
            this.checkHotkeysOutlawOffensiveCursedBl.UseVisualStyleBackColor = true;

            // 
            // checkHotkeysOutlawOffensiveCursedBlOnBossOnly
            // 
            this.checkHotkeysOutlawOffensiveCursedBlOnBossOnly.AutoSize = true;
            this.checkHotkeysOutlawOffensiveCursedBlOnBossOnly.Location = new System.Drawing.Point(260, 32);
            this.checkHotkeysOutlawOffensiveCursedBlOnBossOnly.Name = "checkHotkeysOutlawOffensiveCursedBlOnBossOnly";
            this.checkHotkeysOutlawOffensiveCursedBlOnBossOnly.Size = new System.Drawing.Size(99, 17);
            this.checkHotkeysOutlawOffensiveCursedBlOnBossOnly.TabIndex = 2;
            this.checkHotkeysOutlawOffensiveCursedBlOnBossOnly.Text = "Boss ( NYI )";
            this.checkHotkeysOutlawOffensiveCursedBlOnBossOnly.UseVisualStyleBackColor = true;

            // 
            // btnHotkeysOutlawOffensiveCooldowns
            // 
            this.btnHotkeysOutlawOffensiveCooldowns.Location = new System.Drawing.Point(18, 28);
            this.btnHotkeysOutlawOffensiveCooldowns.Name = "btnHotkeysOutlawOffensiveCooldowns";
            this.btnHotkeysOutlawOffensiveCooldowns.Size = new System.Drawing.Size(113, 23);
            this.btnHotkeysOutlawOffensiveCooldowns.TabIndex = 1;
            this.btnHotkeysOutlawOffensiveCooldowns.Text = "Click to Set";
            this.btnHotkeysOutlawOffensiveCooldowns.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox22);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(582, 406);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Options";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabControl3
            // 
            this.tabControl3.Controls.Add(this.tabPage5);
            this.tabControl3.Controls.Add(this.tabPage2);
            this.tabControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl3.Location = new System.Drawing.Point(0, 0);
            this.tabControl3.Name = "tabControl3";
            this.tabControl3.SelectedIndex = 0;
            this.tabControl3.Size = new System.Drawing.Size(590, 432);
            this.tabControl3.TabIndex = 0;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 432);
            this.Controls.Add(this.tabControl3);
            this.Name = "SettingsForm";
            this.Text = "OutlawStuff";
            this.tabPage5.ResumeLayout(false);
            this.groupBox12.ResumeLayout(false);
            this.groupBox12.PerformLayout();
            this.groupBox22.ResumeLayout(false);
            this.groupBox22.PerformLayout();
            this.groupBox13.ResumeLayout(false);
            this.groupBox13.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabControl3.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion
    }
}

/*
[AddonDetails.db]
AddonAuthor=Creepyjoker
AddonName=Sucstobeyou
WoWVersion=Legion - 70300
[SpellBook.db]
Spell,137619,Marked for Death,F
Spell,13877,Blade Flurry,D0
Spell,51690,Killing Spree,D9
Spell,152150,Death from Above,D6
Spell,2098,Run Through,D3
Spell,185763,Pistol Shot,D2
Spell,193315,Saber Slash,D1
Spell,202665,Curse of Dreadblades,X
Spell,196937,Ghostly Strike,D4
Spell,13750,Adrenaline Rush,E
Spell,193316,Roll the Bones,D5
Spell,5171,Slice and Dice,D5
Aura,13877,Blade Flurry
Aura,195627,Opportunity
Aura,5171,Slice and Dice
Aura,193357,Shark Infested Waters
Aura,193358,Grand Melee
Aura,193359,True Bearing
Aura,199603,Jolly Roger
Aura,199600,Buried Treasure
Aura,51690,Killing Spree
Aura,196937,Ghostly Strike
Aura,193356,Broadsides
Aura,13750,Adrenaline Rush
*/
