// ReSharper disable UnusedMember.Global

/* 	Mixo's Demonology Rotation v0.1 (alpha)

	v0.1 (alpha)
		- Rotation creation started.
		- Simcraft rotation in place and working.

*/

using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using ShadowMagic.Helpers;

namespace ShadowMagic.Rotation
{
    public class DemonologyWarlockMixo : CombatRoutine
    {
        private static bool empowered;
        private static bool threeimps;

        private static readonly float OneFiveCast = Convert.ToSingle(1.5f/(1 + WoW.HastePercent/100f));
        private static readonly float TwoSecondCast = Convert.ToSingle(2f/(1 + WoW.HastePercent/100f));
        private static readonly float SBExecuteTime = Math.Max(TwoSecondCast, gcd);

        //Boss bool (easier for testing)

        private static readonly bool boss = true;
        private CheckBox BossToggleBox;

        private readonly Stopwatch CombatTime = new Stopwatch();
        private CheckBox DemonboltBox;
        private readonly Stopwatch DreadstalkersTime = new Stopwatch();
        private CheckBox GrimoireOfServiceBox;
        private CheckBox ImplosionBox;
        private readonly Stopwatch ImpTime = new Stopwatch();

        //Settings
        private CheckBox OdrBox;

        private readonly string readme = "Mixo's Demonology Rotation v0.1 (beta)" + Environment.NewLine + "";

        //Talents
        private CheckBox ShadowflameBox;
        private CheckBox SummonDarkglareBox;

        private static float gcd
        {
            get
            {
                if (Convert.ToSingle(1.5f/(1 + WoW.HastePercent/100f)) > 1)
                {
                    return Convert.ToSingle(1.5f/(1 + WoW.HastePercent/100f));
                }
                return 1;
            }
        }

        private static bool BossToggle
        {
            get
            {
                var bossToggle = ConfigFile.ReadValue("Destruction", "BossToggle").Trim();
                return bossToggle != "" && Convert.ToBoolean(bossToggle);
            }
            set { ConfigFile.WriteValue("Destruction", "BossToggle", value.ToString()); }
        }

        private static bool Odr
        {
            get
            {
                var odr = ConfigFile.ReadValue("Demonology", "Odr").Trim();
                return odr != "" && Convert.ToBoolean(odr);
            }
            set { ConfigFile.WriteValue("Demonology", "Odr", value.ToString()); }
        }

        private static bool Shadowflame
        {
            get
            {
                var shadowflame = ConfigFile.ReadValue("Demonology", "Shadowflame").Trim();
                return shadowflame != "" && Convert.ToBoolean(shadowflame);
            }
            set { ConfigFile.WriteValue("Demonology", "Shadowflame", value.ToString()); }
        }

        private static bool Implosion
        {
            get
            {
                var implosion = ConfigFile.ReadValue("Demonology", "Implosion").Trim();
                return implosion != "" && Convert.ToBoolean(implosion);
            }
            set { ConfigFile.WriteValue("Demonology", "Implosion", value.ToString()); }
        }

        private static bool GrimoireOfService
        {
            get
            {
                var grimoireOfService = ConfigFile.ReadValue("Demonology", "GrimoireOfService").Trim();
                return grimoireOfService != "" && Convert.ToBoolean(grimoireOfService);
            }
            set { ConfigFile.WriteValue("Demonology", "GrimoireOfService", value.ToString()); }
        }

        private static bool SummonDarkglare
        {
            get
            {
                var summonDarkglare = ConfigFile.ReadValue("Demonology", "SummonDarkglare").Trim();
                return summonDarkglare != "" && Convert.ToBoolean(summonDarkglare);
            }
            set { ConfigFile.WriteValue("Demonology", "SummonDarkglare", value.ToString()); }
        }

        private static bool Demonbolt
        {
            get
            {
                var demonbolt = ConfigFile.ReadValue("Demonology", "Demonbolt").Trim();
                return demonbolt != "" && Convert.ToBoolean(demonbolt);
            }
            set { ConfigFile.WriteValue("Demonology", "Demonbolt", value.ToString()); }
        }

        public override Form SettingsForm { get; set; }

        public override string Name => "Demonology Warlock";

        public override string Class => "Warlock";

        public override void Initialize()
        {
            Log.Write("-----", Color.DarkViolet);
            Log.Write("WELCOME TO MIXO's DEMO ROTATION!", Color.DarkViolet);
            Log.Write("- Version: 0.1 (alpha) -", Color.DarkViolet);
            Log.Write("", Color.DarkViolet);
            Log.Write("Don't forget to change the Rotation settings based on your preferences.", Color.DarkViolet);
            Log.Write("", Color.DarkViolet);
            Log.Write("And please take a look at the Read Me to further understand this rotation ", Color.DarkViolet);
            Log.Write(" and the choices behind it.", Color.DarkViolet);
            Log.Write("", Color.DarkViolet);
            Log.Write("Feedback is appreciated in the Warlock section on PixelMagic's Discord.", Color.DarkViolet);
            Log.Write("Tag it with @[EU] Mixo to notify me. :)", Color.DarkViolet);
            Log.Write("-----", Color.DarkViolet);
            Log.Write("Mix Oh! Demon Ology!");

            SettingsForm = new Form {Text = "Mixo's Demo Warlock Rotation - Settings", StartPosition = FormStartPosition.CenterScreen, Width = 480, Height = 318, ShowIcon = false};

            var lblDefensivesText = new Label {Text = "Settings", Size = new Size(115, 13), Left = 15, Top = 15};
            lblDefensivesText.ForeColor = Color.Maroon;
            SettingsForm.Controls.Add(lblDefensivesText);
            var lblCooldownzText = new Label {Text = "Talents", Size = new Size(115, 13), Left = 260, Top = 15};
            lblCooldownzText.ForeColor = Color.DarkViolet;
            SettingsForm.Controls.Add(lblCooldownzText);

            var lblTextBox = new Label
            {
                Text =
                    "Please select your settings for the current specialization. " + Environment.NewLine +
                    "If a Talent, Trait, or Legendary is checked and it's not used in-game, the rotation will fail. " + Environment.NewLine +
                    "Please report any issues on #warlock PM Discord Channel, use @[EU] Mixo tag. ",
                Size = new Size(270, 220),
                Left = 15,
                Top = 195
            };
            lblTextBox.ForeColor = Color.DarkGreen;
            SettingsForm.Controls.Add(lblTextBox);

            var cmdSave = new Button {Text = "Save", Width = 65, Height = 25, Left = 332, Top = 243, Size = new Size(120, 31)};
            var cmdReadme = new Button {Text = "Read Me", Width = 65, Height = 25, Left = 332, Top = 213, Size = new Size(120, 31)};

            var lblOdrText = new Label {Text = "PLACEHOLDER", Size = new Size(120, 13), Left = 15, Top = 39};
            SettingsForm.Controls.Add(lblOdrText);
            OdrBox = new CheckBox {Checked = Odr, TabIndex = 6, Size = new Size(15, 14), Left = 145, Top = 39};
            SettingsForm.Controls.Add(OdrBox);

            var lblBossToggleText = new Label {Text = "Only Auto CDs on Boss", Size = new Size(120, 13), Left = 15, Top = 128};
            SettingsForm.Controls.Add(lblBossToggleText);
            BossToggleBox = new CheckBox {Checked = BossToggle, TabIndex = 6, Size = new Size(15, 14), Left = 145, Top = 128};
            SettingsForm.Controls.Add(BossToggleBox);

            var lblShadowflameText = new Label {Text = "Shadowflame", Size = new Size(120, 13), Left = 260, Top = 39};
            SettingsForm.Controls.Add(lblShadowflameText);
            ShadowflameBox = new CheckBox {Checked = Shadowflame, TabIndex = 22, Size = new Size(15, 14), Left = 390, Top = 39};
            SettingsForm.Controls.Add(ShadowflameBox);

            var lblImplosionText = new Label {Text = "Implosion", Size = new Size(120, 13), Left = 260, Top = 56};
            SettingsForm.Controls.Add(lblImplosionText);
            ImplosionBox = new CheckBox {Checked = Implosion, TabIndex = 22, Size = new Size(15, 14), Left = 390, Top = 56};
            SettingsForm.Controls.Add(ImplosionBox);

            var lblGrimoireOfServiceText = new Label {Text = "Grimoire Of Service", Size = new Size(120, 13), Left = 260, Top = 124};
            SettingsForm.Controls.Add(lblGrimoireOfServiceText);
            GrimoireOfServiceBox = new CheckBox {Checked = GrimoireOfService, TabIndex = 22, Size = new Size(15, 14), Left = 390, Top = 124};
            SettingsForm.Controls.Add(GrimoireOfServiceBox);

            var lblSummonDarkglareText = new Label {Text = "Summon Darkglare", Size = new Size(120, 13), Left = 260, Top = 141};
            SettingsForm.Controls.Add(lblSummonDarkglareText);
            SummonDarkglareBox = new CheckBox {Checked = SummonDarkglare, TabIndex = 22, Size = new Size(15, 14), Left = 390, Top = 141};
            SettingsForm.Controls.Add(SummonDarkglareBox);

            var lblDemonboltText = new Label {Text = "Demonbolt", Size = new Size(120, 13), Left = 260, Top = 158};
            SettingsForm.Controls.Add(lblDemonboltText);
            DemonboltBox = new CheckBox {Checked = Demonbolt, TabIndex = 22, Size = new Size(15, 14), Left = 390, Top = 158};
            SettingsForm.Controls.Add(DemonboltBox);

            OdrBox.Checked = Odr;
            BossToggleBox.Checked = BossToggle;
            ShadowflameBox.Checked = Shadowflame;
            ImplosionBox.Checked = Implosion;
            GrimoireOfServiceBox.Checked = GrimoireOfService;
            SummonDarkglareBox.Checked = SummonDarkglare;
            DemonboltBox.Checked = Demonbolt;


            cmdSave.Click += CmdSave_Click;
            cmdReadme.Click += CmdReadme_Click;
            OdrBox.CheckedChanged += Odr_Click;
            BossToggleBox.CheckedChanged += BossToggle_Click;
            ShadowflameBox.CheckedChanged += Shadowflame_Click;
            ImplosionBox.CheckedChanged += Implosion_Click;
            GrimoireOfServiceBox.CheckedChanged += GrimoireOfService_Click;
            SummonDarkglareBox.CheckedChanged += SummonDarkglare_Click;
            DemonboltBox.CheckedChanged += Demonbolt_Click;


            SettingsForm.Controls.Add(cmdSave);
            SettingsForm.Controls.Add(cmdReadme);
            lblOdrText.BringToFront();
            lblBossToggleText.BringToFront();
            lblShadowflameText.BringToFront();
            lblImplosionText.BringToFront();
            lblGrimoireOfServiceText.BringToFront();
            lblSummonDarkglareText.BringToFront();
            lblDemonboltText.BringToFront();
        }

        private void CmdReadme_Click(object sender, EventArgs e)
        {
            MessageBox.Show(readme, "PixelMagic", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CmdSave_Click(object sender, EventArgs e)
        {
            Odr = OdrBox.Checked;
            BossToggle = BossToggleBox.Checked;
            Shadowflame = ShadowflameBox.Checked;
            Implosion = ImplosionBox.Checked;
            GrimoireOfService = GrimoireOfServiceBox.Checked;
            SummonDarkglare = SummonDarkglareBox.Checked;
            Demonbolt = DemonboltBox.Checked;
            MessageBox.Show("Settings saved.", "PixelMagic", MessageBoxButtons.OK, MessageBoxIcon.Information);
            SettingsForm.Close();
        }

        // Checkboxes

        private void Odr_Click(object sender, EventArgs e)
        {
            Odr = OdrBox.Checked;
        }

        private void BossToggle_Click(object sender, EventArgs e)
        {
            BossToggle = BossToggleBox.Checked;
        }

        private void Shadowflame_Click(object sender, EventArgs e)
        {
            Shadowflame = ShadowflameBox.Checked;
        }

        private void Implosion_Click(object sender, EventArgs e)
        {
            Implosion = ImplosionBox.Checked;
        }

        private void GrimoireOfService_Click(object sender, EventArgs e)
        {
            GrimoireOfService = GrimoireOfServiceBox.Checked;
        }

        private void SummonDarkglare_Click(object sender, EventArgs e)
        {
            SummonDarkglare = SummonDarkglareBox.Checked;
        }

        private void Demonbolt_Click(object sender, EventArgs e)
        {
            Demonbolt = DemonboltBox.Checked;
        }

        public override void Stop()
        {
        }

        public override void Pulse()
        {
            //Combat Time
            if (CombatTime.IsRunning && !WoW.IsInCombat)
            {
                CombatTime.Reset();
            }
            if (!CombatTime.IsRunning && WoW.IsInCombat)
            {
                CombatTime.Start();
            }

            //Dreadstalkers Time Remaining (12000 ms) (EXPERIMENTAL)
            if (DreadstalkersTime.IsRunning && WoW.DreadstalkersCount == 0)
            {
                DreadstalkersTime.Reset();
            }
            if (!DreadstalkersTime.IsRunning && WoW.DreadstalkersCount >= 1)
            {
                DreadstalkersTime.Start();
            }

            //Imp Time Remaining (12000 ms) (EXPERIMENTAL)
            if (ImpTime.IsRunning && WoW.WildImpsCount == 0)
            {
                ImpTime.Reset();
            }
            if (!ImpTime.IsRunning && WoW.WildImpsCount >= 1)
            {
                ImpTime.Start();
            }

            var DreadstalkersRemainingDuration = Convert.ToSingle((12000f - DreadstalkersTime.ElapsedMilliseconds)/1000f);
            var ImpsRemainingDuration = Convert.ToSingle((12000f - ImpTime.ElapsedMilliseconds)/1000f + OneFiveCast);

            // Single Target Rotation
            if (combatRoutine.Type == RotationType.SingleTarget)
            {
                // Normal Rotation
                if (WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsInCombat && !WoW.PlayerIsCasting)
                {
                    Log.Write("Imp Time: " + ImpsRemainingDuration, Color.DarkViolet);
                    Log.Write("Dread Time: " + DreadstalkersRemainingDuration, Color.DarkViolet);
                    //Implosion (if talent)
                    if (WoW.CanCast("Implosion") && Implosion && WoW.IsSpellInRange("Doom"))
                    {
                        if (ImpsRemainingDuration <= SBExecuteTime && WoW.PlayerHasBuff("DemonicSynergy"))
                        {
                            WoW.CastSpell("Implosion");
                            return;
                        }
                        if (WoW.LastSpell == "HandOfGuldan" && WoW.WildImpsCount == 1 && WoW.PlayerHasBuff("DemonicSynergy"))
                        {
                            WoW.CastSpell("Implosion");
                            return;
                        }
                    }

                    //Shadowflame (if talent)
                    if (WoW.CanCast("Shadowflame") && Shadowflame && WoW.IsSpellInRange("Doom") && WoW.TargetHasDebuff("Shadowflame") &&
                        WoW.TargetDebuffTimeRemaining("Shadowflame") < TwoSecondCast + 2)
                    {
                        WoW.CastSpell("Shadowflame");
                        return;
                    }

                    //Service Pet (if talent)
                    if (WoW.CanCast("GrimoireFelguard") && GrimoireOfService && WoW.IsSpellInRange("Doom") && WoW.CurrentSoulShards >= 1 && boss)
                    {
                        WoW.CastSpell("GrimoireFelguard");
                        empowered = false;
                        return;
                    }

                    // Doomguard
                    if (WoW.CanCast("Doomguard") && WoW.CurrentSoulShards >= 1 && boss && WoW.IsSpellInRange("Doom"))
                    {
                        WoW.CastSpell("Doomguard");
                        empowered = false;
                        return;
                    }

                    //Felstorm
                    if (WoW.CanCast("Felstorm") && WoW.HasPet && WoW.PetHasBuff("DemonicEmpowerment"))
                    {
                        WoW.CastSpell("Felstorm");
                        return;
                    }

                    //Call Dreadstalkers (if NOT talent Summon Darkglare)
                    if (WoW.CanCast("CallDreadstalkers") && !SummonDarkglare && (WoW.CurrentSoulShards >= 2 || WoW.PlayerHasBuff("DemonicCalling")) && WoW.IsSpellInRange("Doom") &&
                        (!WoW.IsMoving || WoW.PlayerHasBuff("Norgannon")))
                    {
                        WoW.CastSpell("CallDreadstalkers");
                        empowered = false;
                        DreadstalkersTime.Restart();
                        return;
                    }

                    //Hand Of Guldan (if NOT talent Summon Darkglare)
                    if (WoW.CanCast("HandOfGuldan") && !SummonDarkglare && WoW.LastSpell != "HandOfGuldan" && WoW.CurrentSoulShards >= 4 && WoW.IsSpellInRange("Doom") &&
                        (!WoW.IsMoving || WoW.PlayerHasBuff("Norgannon")))
                    {
                        WoW.CastSpell("HandOfGuldan");
                        empowered = false;
                        threeimps = false;
                        ImpTime.Restart();
                        return;
                    }

                    //Summon Darkglare (if talent)
                    if (WoW.CanCast("SummonDarkglare") && SummonDarkglare && WoW.CurrentSoulShards >= 1 && WoW.TargetHasDebuff("Doom") && WoW.IsSpellInRange("Doom") &&
                        (!WoW.IsMoving || WoW.PlayerHasBuff("Norgannon")))
                    {
                        if (WoW.LastSpell == "HandOfGuldan" || WoW.LastSpell == "CallDreadstalkers")
                        {
                            WoW.CastSpell("SummonDarkglare");
                            Log.Write("1", Color.Red);
                            empowered = false;
                            return;
                        }
                        if (WoW.SpellCooldownTimeRemaining("CallDreadstalkers") > 5 && WoW.CurrentSoulShards < 3)
                        {
                            WoW.CastSpell("SummonDarkglare");
                            Log.Write("2", Color.Red);
                            empowered = false;
                            return;
                        }
                        if (WoW.SpellCooldownTimeRemaining("CallDreadstalkers") <= gcd && WoW.CurrentSoulShards >= 3)
                        {
                            WoW.CastSpell("SummonDarkglare");
                            Log.Write("3", Color.Red);
                            empowered = false;
                            return;
                        }
                        if (WoW.SpellCooldownTimeRemaining("CallDreadstalkers") <= gcd && WoW.CurrentSoulShards >= 1 && WoW.PlayerHasBuff("DemonicCalling"))
                        {
                            WoW.CastSpell("SummonDarkglare");
                            Log.Write("4", Color.Red);
                            empowered = false;
                            return;
                        }
                    }

                    //Call Dreadstalkers (if talent Summon Darkglare)
                    if (WoW.CanCast("CallDreadstalkers") && SummonDarkglare && (WoW.CurrentSoulShards >= 2 || WoW.PlayerHasBuff("DemonicCalling")) && WoW.IsSpellInRange("Doom") &&
                        (!WoW.IsMoving || WoW.PlayerHasBuff("Norgannon")))
                    {
                        if (WoW.SpellCooldownTimeRemaining("SummonDarkglare") <= TwoSecondCast && WoW.CurrentSoulShards >= 3)
                        {
                            WoW.CastSpell("CallDreadstalkers");
                            empowered = false;
                            DreadstalkersTime.Restart();
                            return;
                        }
                        if (WoW.SpellCooldownTimeRemaining("SummonDarkglare") <= TwoSecondCast && WoW.CurrentSoulShards >= 1 && WoW.PlayerHasBuff("DemonicCalling"))
                        {
                            WoW.CastSpell("CallDreadstalkers");
                            empowered = false;
                            DreadstalkersTime.Restart();
                            return;
                        }
                    }

                    //Hand Of Guldan
                    if (WoW.CanCast("HandOfGuldan") && WoW.LastSpell != "HandOfGuldan" && WoW.CurrentSoulShards >= 3 && WoW.LastSpell == "CallDreadstalkers" && WoW.IsSpellInRange("Doom") &&
                        (!WoW.IsMoving || WoW.PlayerHasBuff("Norgannon")))
                    {
                        WoW.CastSpell("HandOfGuldan");
                        empowered = false;
                        threeimps = true;
                        ImpTime.Restart();
                        return;
                    }

                    //Hand Of Guldan (if talent Summon Darkglare)
                    if (WoW.CanCast("HandOfGuldan") && SummonDarkglare && WoW.LastSpell != "HandOfGuldan" && WoW.CurrentSoulShards >= 5 &&
                        WoW.SpellCooldownTimeRemaining("SummonDarkglare") <= OneFiveCast && WoW.IsSpellInRange("Doom") && (!WoW.IsMoving || WoW.PlayerHasBuff("Norgannon")))
                    {
                        WoW.CastSpell("HandOfGuldan");
                        threeimps = false;
                        empowered = false;
                        ImpTime.Restart();
                        return;
                    }

                    //Hand Of Guldan (if talent Summon Darkglare)
                    if (WoW.CanCast("HandOfGuldan") && SummonDarkglare && WoW.LastSpell != "HandOfGuldan" && WoW.CurrentSoulShards >= 4 && WoW.SpellCooldownTimeRemaining("SummonDarkglare") > 2 &&
                        WoW.IsSpellInRange("Doom") && (!WoW.IsMoving || WoW.PlayerHasBuff("Norgannon")))
                    {
                        WoW.CastSpell("HandOfGuldan");
                        threeimps = false;
                        empowered = false;
                        ImpTime.Restart();
                        return;
                    }

                    //Demonic Empowerment (if last spell was Hand Of Guldan)
                    if (WoW.CanCast("DemonicEmpowerment") && WoW.LastSpell == "HandOfGuldan" && WoW.LastSpell != "DemonicEmpowerment" && WoW.IsSpellInRange("Doom") &&
                        (!WoW.IsMoving || WoW.PlayerHasBuff("Norgannon")))
                    {
                        WoW.CastSpell("DemonicEmpowerment");
                        empowered = true;
                        return;
                    }

                    //Demonic Empowerment
                    if (WoW.CanCast("DemonicEmpowerment") && (!empowered || !WoW.PetHasBuff("DemonicEmpowerment")) && WoW.LastSpell != "DemonicEmpowerment" && WoW.IsSpellInRange("Doom") &&
                        (!WoW.IsMoving || WoW.PlayerHasBuff("Norgannon")))
                    {
                        WoW.CastSpell("DemonicEmpowerment");
                        empowered = true;
                        return;
                    }
/*
					//Doom (if NOT talent Hand of Doom)
					if(WoW.CanCast("Doom") && (!WoW.TargetHasDebuff("Doom") || WoW.TargetDebuffTimeRemaining("Doom") < 5) && WoW.IsSpellInRange("Doom"))
					{
						WoW.CastSpell("Doom");
						return;
					}					

					//Soul Harvest
					if(WoW.CanCast("SoulHarvest") && WoW.IsSpellInRange("Doom"))
					{
						WoW.CastSpell("SoulHarvest");
						return;
					}
*/
                    //Shadowflame (if talent)
                    if (WoW.CanCast("Shadowflame") && Shadowflame && WoW.IsSpellInRange("Doom") && WoW.PlayerSpellCharges("Shadowflame") == 2)
                    {
                        WoW.CastSpell("Shadowflame");
                        return;
                    }

                    //Thal'kiel's Consumption
                    if (WoW.CanCast("TK") && WoW.DreadstalkersCount >= 1 && DreadstalkersRemainingDuration > TwoSecondCast && ((WoW.WildImpsCount >= 1 && !threeimps) || WoW.WildImpsCount >= 2) &&
                        ImpsRemainingDuration > TwoSecondCast && WoW.IsSpellInRange("Doom") && (!WoW.IsMoving || WoW.PlayerHasBuff("Norgannon")))
                    {
                        WoW.CastSpell("TK");
                        return;
                    }

                    //Life Tap
                    if (WoW.CanCast("LifeTap") && WoW.Mana <= 30)
                    {
                        WoW.CastSpell("LifeTap");
                        return;
                    }

                    //Demonwrath (if moving)
                    if (WoW.CanCast("Demonwrath") && WoW.IsMoving && !WoW.PlayerHasBuff("Norgannon"))
                    {
                        WoW.CastSpell("Demonwrath");
                        return;
                    }

                    //Demonbolt (if talent)
                    if (WoW.CanCast("Demonbolt") && Demonbolt && WoW.IsSpellInRange("Doom") && (!WoW.IsMoving || WoW.PlayerHasBuff("Norgannon")))
                    {
                        WoW.CastSpell("Demonbolt");
                        return;
                    }

                    //Shadowbolt (if NOT Demonbolt talent)
                    if (WoW.CanCast("Shadowbolt") && !Demonbolt && WoW.IsSpellInRange("Doom") && (!WoW.IsMoving || WoW.PlayerHasBuff("Norgannon")))
                    {
                        WoW.CastSpell("Demonbolt");
                        return;
                    }

                    //Life Tap
                    if (WoW.CanCast("LifeTap") && WoW.Mana < 100)
                    {
                        WoW.CastSpell("LifeTap");
                        return;
                    }
                }
            }

            // AoE Rotation
            if (combatRoutine.Type == RotationType.AOE)
            {
                Log.Write("Imp Time: " + ImpsRemainingDuration, Color.DarkViolet);
                // AoE Rotation
                if (WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsInCombat && !WoW.PlayerIsCasting)
                {
                    //Implosion (if talent)
                    if (WoW.CanCast("Implosion") && Implosion && WoW.IsSpellInRange("Doom"))
                    {
                        if (ImpsRemainingDuration <= SBExecuteTime && WoW.PlayerHasBuff("DemonicSynergy"))
                        {
                            WoW.CastSpell("Implosion");
                            return;
                        }
                        if (WoW.LastSpell == "HandOfGuldan" && WoW.WildImpsCount == 1 && WoW.PlayerHasBuff("DemonicSynergy"))
                        {
                            WoW.CastSpell("Implosion");
                            return;
                        }
                        if (WoW.WildImpsCount == 1 && ImpsRemainingDuration <= SBExecuteTime)
                        {
                            WoW.CastSpell("Implosion");
                            return;
                        }
                        if (WoW.LastSpell == "HandOfGuldan" && WoW.WildImpsCount == 1)
                        {
                            WoW.CastSpell("Implosion");
                            return;
                        }
                    }

                    //Shadowflame (if talent)
                    if (WoW.CanCast("Shadowflame") && Shadowflame && WoW.IsSpellInRange("Doom") && WoW.TargetHasDebuff("Shadowflame") &&
                        WoW.TargetDebuffTimeRemaining("Shadowflame") < TwoSecondCast + 2)
                    {
                        WoW.CastSpell("Shadowflame");
                        return;
                    }

                    //Felstorm
                    if (WoW.CanCast("Felstorm") && WoW.HasPet && WoW.PetHasBuff("DemonicEmpowerment"))
                    {
                        WoW.CastSpell("Felstorm");
                        return;
                    }

                    //Call Dreadstalkers (if NOT talent Summon Darkglare or Implosion)
                    if (WoW.CanCast("CallDreadstalkers") && !SummonDarkglare && (WoW.CurrentSoulShards >= 2 || WoW.PlayerHasBuff("DemonicCalling")) && !Implosion && WoW.IsSpellInRange("Doom") &&
                        (!WoW.IsMoving || WoW.PlayerHasBuff("Norgannon")))
                    {
                        WoW.CastSpell("CallDreadstalkers");
                        empowered = false;
                        DreadstalkersTime.Restart();
                        return;
                    }

                    //Hand Of Guldan (if NOT talent Summon Darkglare)
                    if (WoW.CanCast("HandOfGuldan") && !SummonDarkglare && WoW.LastSpell != "HandOfGuldan" && WoW.CurrentSoulShards >= 4 && WoW.IsSpellInRange("Doom") &&
                        (!WoW.IsMoving || WoW.PlayerHasBuff("Norgannon")))
                    {
                        WoW.CastSpell("HandOfGuldan");
                        empowered = false;
                        threeimps = false;
                        ImpTime.Restart();
                        return;
                    }

                    //Summon Darkglare (if talent)
                    if (WoW.CanCast("SummonDarkglare") && SummonDarkglare && WoW.CurrentSoulShards >= 1 && WoW.TargetHasDebuff("Doom") && WoW.IsSpellInRange("Doom") &&
                        (!WoW.IsMoving || WoW.PlayerHasBuff("Norgannon")))
                    {
                        if (WoW.LastSpell == "HandOfGuldan" || WoW.LastSpell == "CallDreadstalkers")
                        {
                            WoW.CastSpell("SummonDarkglare");
                            Log.Write("1", Color.Red);
                            empowered = false;
                            return;
                        }
                        if (WoW.SpellCooldownTimeRemaining("CallDreadstalkers") > 5 && WoW.CurrentSoulShards < 3)
                        {
                            WoW.CastSpell("SummonDarkglare");
                            Log.Write("2", Color.Red);
                            empowered = false;
                            return;
                        }
                        if (WoW.SpellCooldownTimeRemaining("CallDreadstalkers") <= gcd && WoW.CurrentSoulShards >= 3)
                        {
                            WoW.CastSpell("SummonDarkglare");
                            Log.Write("3", Color.Red);
                            empowered = false;
                            return;
                        }
                        if (WoW.SpellCooldownTimeRemaining("CallDreadstalkers") <= gcd && WoW.CurrentSoulShards >= 1 && WoW.PlayerHasBuff("DemonicCalling"))
                        {
                            WoW.CastSpell("SummonDarkglare");
                            Log.Write("4", Color.Red);
                            empowered = false;
                            return;
                        }
                    }

                    //Call Dreadstalkers (if talent Summon Darkglare and not Implosion)
                    if (WoW.CanCast("CallDreadstalkers") && SummonDarkglare && (WoW.CurrentSoulShards >= 2 || WoW.PlayerHasBuff("DemonicCalling")) && !Implosion && WoW.IsSpellInRange("Doom") &&
                        (!WoW.IsMoving || WoW.PlayerHasBuff("Norgannon")))
                    {
                        if (WoW.SpellCooldownTimeRemaining("SummonDarkglare") <= TwoSecondCast && WoW.CurrentSoulShards >= 3)
                        {
                            WoW.CastSpell("CallDreadstalkers");
                            empowered = false;
                            DreadstalkersTime.Restart();
                            return;
                        }
                        if (WoW.SpellCooldownTimeRemaining("SummonDarkglare") <= TwoSecondCast && WoW.CurrentSoulShards >= 1 && WoW.PlayerHasBuff("DemonicCalling"))
                        {
                            WoW.CastSpell("CallDreadstalkers");
                            empowered = false;
                            DreadstalkersTime.Restart();
                            return;
                        }
                    }

                    //Hand Of Guldan
                    if (WoW.CanCast("HandOfGuldan") && WoW.LastSpell != "HandOfGuldan" && WoW.CurrentSoulShards >= 3 && WoW.LastSpell == "CallDreadstalkers" && WoW.IsSpellInRange("Doom") &&
                        (!WoW.IsMoving || WoW.PlayerHasBuff("Norgannon")))
                    {
                        WoW.CastSpell("HandOfGuldan");
                        empowered = false;
                        threeimps = true;
                        ImpTime.Restart();
                        return;
                    }

                    //Hand Of Guldan (if talent Summon Darkglare)
                    if (WoW.CanCast("HandOfGuldan") && SummonDarkglare && WoW.LastSpell != "HandOfGuldan" && WoW.CurrentSoulShards >= 5 &&
                        WoW.SpellCooldownTimeRemaining("SummonDarkglare") <= OneFiveCast && WoW.IsSpellInRange("Doom") && (!WoW.IsMoving || WoW.PlayerHasBuff("Norgannon")))
                    {
                        WoW.CastSpell("HandOfGuldan");
                        threeimps = false;
                        empowered = false;
                        ImpTime.Restart();
                        return;
                    }

                    //Hand Of Guldan (if talent Summon Darkglare)
                    if (WoW.CanCast("HandOfGuldan") && SummonDarkglare && WoW.LastSpell != "HandOfGuldan" && WoW.CurrentSoulShards >= 4 && WoW.SpellCooldownTimeRemaining("SummonDarkglare") > 2 &&
                        WoW.IsSpellInRange("Doom") && (!WoW.IsMoving || WoW.PlayerHasBuff("Norgannon")))
                    {
                        WoW.CastSpell("HandOfGuldan");
                        threeimps = false;
                        empowered = false;
                        ImpTime.Restart();
                        return;
                    }

                    //Demonic Empowerment (if last spell was Hand Of Guldan)
                    if (WoW.CanCast("DemonicEmpowerment") && WoW.LastSpell == "HandOfGuldan" && WoW.LastSpell != "DemonicEmpowerment" && WoW.IsSpellInRange("Doom") &&
                        (!WoW.IsMoving || WoW.PlayerHasBuff("Norgannon")))
                    {
                        WoW.CastSpell("DemonicEmpowerment");
                        empowered = true;
                        return;
                    }

                    //Demonic Empowerment
                    if (WoW.CanCast("DemonicEmpowerment") && (!empowered || !WoW.PetHasBuff("DemonicEmpowerment")) && WoW.LastSpell != "DemonicEmpowerment" && WoW.IsSpellInRange("Doom") &&
                        (!WoW.IsMoving || WoW.PlayerHasBuff("Norgannon")))
                    {
                        WoW.CastSpell("DemonicEmpowerment");
                        empowered = true;
                        return;
                    }
/*
					//Doom (if NOT talent Hand of Doom)
					if(WoW.CanCast("Doom") && (!WoW.TargetHasDebuff("Doom") || WoW.TargetDebuffTimeRemaining("Doom") < 5) && WoW.IsSpellInRange("Doom"))
					{
						WoW.CastSpell("Doom");
						return;
					}					

					//Soul Harvest
					if(WoW.CanCast("SoulHarvest") && WoW.IsSpellInRange("Doom"))
					{
						WoW.CastSpell("SoulHarvest");
						return;
					}
*/
                    //Shadowflame (if talent)
                    if (WoW.CanCast("Shadowflame") && Shadowflame && WoW.IsSpellInRange("Doom") && WoW.PlayerSpellCharges("Shadowflame") == 2)
                    {
                        WoW.CastSpell("Shadowflame");
                        return;
                    }

                    //Thal'kiel's Consumption
                    if (WoW.CanCast("TK") && ((WoW.DreadstalkersCount >= 1 && DreadstalkersRemainingDuration > TwoSecondCast) || Implosion) &&
                        ((WoW.WildImpsCount >= 1 && !threeimps) || WoW.WildImpsCount >= 2) && ImpsRemainingDuration > TwoSecondCast && WoW.IsSpellInRange("Doom") &&
                        (!WoW.IsMoving || WoW.PlayerHasBuff("Norgannon")))
                    {
                        WoW.CastSpell("TK");
                        return;
                    }

                    //Life Tap
                    if (WoW.CanCast("LifeTap") && WoW.Mana <= 30)
                    {
                        WoW.CastSpell("LifeTap");
                        return;
                    }

                    //Demonwrath
                    if (WoW.CanCast("Demonwrath") && !WoW.PlayerIsChanneling)
                    {
                        WoW.CastSpell("Demonwrath");
                        return;
                    }

                    //Life Tap
                    if (WoW.CanCast("LifeTap") && WoW.Mana < 100 && !WoW.PlayerIsChanneling)
                    {
                        WoW.CastSpell("LifeTap");
                    }
                }
            }
        }
    }
}

/*
[AddonDetails.db]
AddonAuthor=Mixo
AddonName=Kontrol
WoWVersion=Legion - 70300
[SpellBook.db]
Spell,157695,Demonbolt,F6
Spell,686,Shadowbolt,F6
Spell,193440,Demonwrath,F12
Spell,1454,LifeTap,R
Spell,205181,Shadowflame,F5
Spell,603,Doom,F4
Spell,105174,HandOfGuldan,F1
Spell,104316,CallDreadstalkers,F2
Spell,193396,DemonicEmpowerment,Q
Spell,196277,Implosion,C
Spell,119914,Felstorm,F
Spell,211714,TK,F3
Spell,5512,Healthstone,F10
Spell,18540,Doomguard,T
Spell,111898,GrimoireFelguard,E
Spell,196098,SoulHarvest,F11
Spell,205180,SummonDarkglare,F7
Aura,603,Doom
Aura,205181,Shadowflame
Aura,205146,DemonicCalling
Aura,193396,DemonicEmpowerment
Aura,171982,DemonicSynergy
Aura,193440,Demonwrath
Aura,208215,Norgannon
*/