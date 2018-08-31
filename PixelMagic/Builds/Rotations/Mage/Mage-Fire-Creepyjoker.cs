// noaxeqtr@gmail.com
// ReSharper disable UnusedMember.Global
// ReSharper disable ConvertPropertyToExpressionBody

using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using PixelMagic.Helpers;

namespace PixelMagic.Rotation
{
    public class FireMageCJ : CombatRoutine
    {
        private readonly Stopwatch stopwatch = new Stopwatch();
        private CheckBox BarrierBox;
        private CheckBox CinderstormBox;


        private CheckBox CombustionBox;
        private CheckBox DragBrBox;
        private CheckBox IceBlockBox;
        private CheckBox LegendaryBox;
        private CheckBox LivingBombBox;
        private CheckBox MeteorBox;
        private CheckBox MirrorsBox;
        private CheckBox ROFBox;
        private CheckBox RuneOfPowerBox;
        private CheckBox SpellLockBox;

        public static bool DragBr
        {
            get
            {
                var DragBr = ConfigFile.ReadValue("Fire", "DragBr").Trim();
                return DragBr != "" && Convert.ToBoolean(DragBr);
            }
            set { ConfigFile.WriteValue("Fire", "DragBr", value.ToString()); }
        }

        //
        public static bool Mirrors
        {
            get
            {
                var Mirrors = ConfigFile.ReadValue("Fire", "Mirrors").Trim();
                return Mirrors != "" && Convert.ToBoolean(Mirrors);
            }
            set { ConfigFile.WriteValue("Fire", "Mirrors", value.ToString()); }
        }

        //
        public static bool Combustion
        {
            get
            {
                var Combustion = ConfigFile.ReadValue("Fire", "Combustion").Trim();

                return Combustion != "" && Convert.ToBoolean(Combustion);
            }
            set { ConfigFile.WriteValue("Fire", "Combustion", value.ToString()); }
        }

        // 

        public static bool Meteor
        {
            get
            {
                var Meteor = ConfigFile.ReadValue("Fire", "Meteor").Trim();
                return Meteor != "" && Convert.ToBoolean(Meteor);
            }
            set { ConfigFile.WriteValue("Fire", "Meteor", value.ToString()); }
        }

        //
        public static bool RuneOfPower
        {
            get
            {
                var RuneOfPower = ConfigFile.ReadValue("Fire", "RuneOfPower").Trim();
                return RuneOfPower != "" && Convert.ToBoolean(RuneOfPower);
            }
            set { ConfigFile.WriteValue("Fire", "RuneOfPower", value.ToString()); }
        }

        // 

        public static bool IceBlock
        {
            get
            {
                var IceBlock = ConfigFile.ReadValue("Fire", "IceBlock").Trim();
                return IceBlock != "" && Convert.ToBoolean(IceBlock);
            }
            set { ConfigFile.WriteValue("Fire", "IceBlock", value.ToString()); }
        }

        //
        public static bool SpellLock
        {
            get
            {
                var SpellLock = ConfigFile.ReadValue("Fire", "SpellLock").Trim();
                return SpellLock != "" && Convert.ToBoolean(SpellLock);
            }
            set { ConfigFile.WriteValue("Fire", "SpellLock", value.ToString()); }
        }

        //
        public static bool Legendary
        {
            get
            {
                var Legendary = ConfigFile.ReadValue("Fire", "Legendary").Trim();
                return Legendary != "" && Convert.ToBoolean(Legendary);
            }
            set { ConfigFile.WriteValue("Fire", "Legendary", value.ToString()); }
        }

        // 
        public static bool LivingBomb
        {
            get
            {
                var LivingBomb = ConfigFile.ReadValue("Fire", "LivingBomb").Trim();
                return LivingBomb != "" && Convert.ToBoolean(LivingBomb);
            }
            set { ConfigFile.WriteValue("Fire", "LivingBomb", value.ToString()); }
        }

        //
        public static bool Cinderstorm
        {
            get
            {
                var Cinderstorm = ConfigFile.ReadValue("Fire", "Cinderstorm").Trim();
                return Cinderstorm != "" && Convert.ToBoolean(Cinderstorm);
            }
            set { ConfigFile.WriteValue("Fire", "Cinderstorm", value.ToString()); }
        }

        //
        public static bool ROF
        {
            get
            {
                var ROF = ConfigFile.ReadValue("Fire", "ROF").Trim();
                return ROF != "" && Convert.ToBoolean(ROF);
            }
            set { ConfigFile.WriteValue("Fire", "ROF", value.ToString()); }
        }

        //
        public static bool Barrier
        {
            get
            {
                var Barrier = ConfigFile.ReadValue("Fire", "Barrier").Trim();
                return Barrier != "" && Convert.ToBoolean(Barrier);
            }
            set { ConfigFile.WriteValue("Fire", "Barrier", value.ToString()); }
        }

        public override Form SettingsForm { get; set; }


        public override string Name
        {
            get { return "Fire Mage"; }
        }

        public override string Class
        {
            get { return "Mage"; }
        }


        public override void Initialize()
        {
            Log.Write("CreepyFireMage Rotation V2.8. Please report any issues #mage with @Creepyjoker tag, on PM Discord channel.", Color.Green);
            Log.Write("Supported talents - Check rotation settings.", Color.Green);
            Log.Write(
                "Make sure you created the macro /stopcasting /cast Phoenix's Flames ; /cast [@cursor] Flamestrike ; /cast [@cursor] Ring of Frost and /cast [@cursor] Meteor and it's also set on your keybind.",
                Color.Red);
            Log.Write("Check README from Rotation Settings for further informations.", Color.Red);
            //
            SettingsForm = new Form
            {
                Text = "Fire Mage Rotation Settings || Developed by CreepyJoker. ",
                StartPosition = FormStartPosition.CenterScreen,
                Width = 480,
                Height = 500,
                ShowIcon = false
            };

            var picBox = new PictureBox {Left = 0, Top = 0, Width = 800, Height = 100};
            SettingsForm.Controls.Add(picBox);

            var lblCombustionText = new Label {Text = "Combustion(C)", Size = new Size(81, 13), Left = 305, Top = 163};
            SettingsForm.Controls.Add(lblCombustionText);

            var lblDefensivesText = new Label {Text = "Defensives/Utilities", Size = new Size(115, 13), Left = 15, Top = 105};
            SettingsForm.Controls.Add(lblDefensivesText);
            var lblCooldownzText = new Label {Text = "Cooldowns/Talents", Size = new Size(115, 13), Left = 310, Top = 105};
            SettingsForm.Controls.Add(lblCooldownzText);

            var lblTextBox = new Label
            {
                Text =
                    "Please select your settings for the current specialization. " + Environment.NewLine +
                    "If a Talent/Defensive/Cooldown/Utility is checked and it's not specced into, the rotation will fail. " + Environment.NewLine +
                    "Please report any issues on #mage PM Discord Channel.",
                Size = new Size(270, 220),
                Left = 15,
                Top = 340
            };
            lblTextBox.ForeColor = Color.Red;
            SettingsForm.Controls.Add(lblTextBox);

            CombustionBox = new CheckBox {Checked = Combustion, TabIndex = 2, Size = new Size(15, 14), Left = 390, Top = 163};
            SettingsForm.Controls.Add(CombustionBox);
            //save
            var cmdSave = new Button {Text = "Save", Width = 65, Height = 25, Left = 332, Top = 425, Size = new Size(120, 31)};

            var cmdReadme = new Button {Text = "Readme", Width = 65, Height = 25, Left = 332, Top = 370, Size = new Size(120, 31)};
            //
            var lblMeteorText = new Label {Text = "Meteor(T)", Size = new Size(81, 13), Left = 305, Top = 180};
            SettingsForm.Controls.Add(lblMeteorText);

            MeteorBox = new CheckBox {Checked = Meteor, TabIndex = 4, Size = new Size(15, 14), Left = 390, Top = 180};
            SettingsForm.Controls.Add(MeteorBox);
            //

            var lblSpellLockText = new Label {Text = "Spell Lock(NYI)", Size = new Size(81, 13), Left = 12, Top = 129};
            SettingsForm.Controls.Add(lblSpellLockText);

            SpellLockBox = new CheckBox {Checked = SpellLock, TabIndex = 6, Size = new Size(15, 14), Left = 115, Top = 129};
            SettingsForm.Controls.Add(SpellLockBox);
            //
            var lblIceBlockText = new Label {Text = "Ice Block(D)", Size = new Size(81, 13), Left = 12, Top = 145};
            SettingsForm.Controls.Add(lblIceBlockText);
            IceBlockBox = new CheckBox {Checked = IceBlock, TabIndex = 8, Size = new Size(15, 14), Left = 115, Top = 145};
            SettingsForm.Controls.Add(IceBlockBox);
            //
            var lblDragBrText = new Label {Text = "DragonBr(LEG)", Size = new Size(81, 13), Left = 305, Top = 197};
            SettingsForm.Controls.Add(lblDragBrText);
            DragBrBox = new CheckBox {Checked = DragBr, TabIndex = 10, Size = new Size(15, 14), Left = 390, Top = 197};
            SettingsForm.Controls.Add(DragBrBox);
            //
            var lblRuneOfPowerText = new Label {Text = "ROPower(C)", Size = new Size(81, 13), Left = 305, Top = 214};
            SettingsForm.Controls.Add(lblRuneOfPowerText);
            RuneOfPowerBox = new CheckBox {Checked = RuneOfPower, TabIndex = 12, Left = 390, Top = 214, Size = new Size(15, 14)};
            SettingsForm.Controls.Add(RuneOfPowerBox);
            //
            var lblLegendaryText = new Label {Text = "Bracers(LEG)", Size = new Size(81, 13), Left = 305, Top = 231};
            SettingsForm.Controls.Add(lblLegendaryText);
            LegendaryBox = new CheckBox {Checked = Legendary, TabIndex = 14, Size = new Size(15, 14), Left = 390, Top = 231};
            SettingsForm.Controls.Add(LegendaryBox);
            //
            var lblLivingBombText = new Label {Text = "Living Bomb(T)", Size = new Size(81, 13), Left = 305, Top = 248};
            SettingsForm.Controls.Add(lblLivingBombText);
            LivingBombBox = new CheckBox {Checked = LivingBomb, Size = new Size(15, 14), TabIndex = 16, Left = 390, Top = 248};
            SettingsForm.Controls.Add(LivingBombBox);
            //
            var lblMirrorsText = new Label {Text = "Mirrors(T)", Size = new Size(81, 13), Left = 305, Top = 265};
            SettingsForm.Controls.Add(lblMirrorsText);
            MirrorsBox = new CheckBox {Checked = Mirrors, TabIndex = 18, Size = new Size(15, 14), Left = 390, Top = 265};
            SettingsForm.Controls.Add(MirrorsBox);
            //
            var lblCinderstormText = new Label {Text = "Cinderstorm(T)", Size = new Size(81, 13), Left = 305, Top = 146};
            SettingsForm.Controls.Add(lblCinderstormText);
            CinderstormBox = new CheckBox {Checked = Cinderstorm, TabIndex = 20, Size = new Size(15, 14), Left = 390, Top = 146};
            SettingsForm.Controls.Add(CinderstormBox);
            //
            var lblROFText = new Label {Text = "Ring of Frost(T)", Size = new Size(81, 13), Left = 305, Top = 129};
            SettingsForm.Controls.Add(lblROFText);
            ROFBox = new CheckBox {Checked = ROF, TabIndex = 22, Size = new Size(15, 14), Left = 390, Top = 129};
            SettingsForm.Controls.Add(ROFBox);
            //
            var lblBarrierText = new Label {Text = "Blazing Barrier(D)", Size = new Size(81, 13), Left = 12, Top = 162};
            SettingsForm.Controls.Add(lblBarrierText);
            BarrierBox = new CheckBox {Checked = Barrier, TabIndex = 22, Size = new Size(15, 14), Left = 115, Top = 162};
            SettingsForm.Controls.Add(BarrierBox);
            //

            // Box Check
            CombustionBox.Checked = Combustion;
            MeteorBox.Checked = Meteor;
            SpellLockBox.Checked = SpellLock;
            IceBlockBox.Checked = IceBlock;
            DragBrBox.Checked = DragBr;
            RuneOfPowerBox.Checked = RuneOfPower;
            LegendaryBox.Checked = Legendary;
            LivingBombBox.Checked = LivingBomb;
            MirrorsBox.Checked = Mirrors;
            CinderstormBox.Checked = Cinderstorm;
            ROFBox.Checked = ROF;
            BarrierBox.Checked = Barrier;


            //cmdSave
            cmdSave.Click += CmdSave_Click;
            cmdReadme.Click += CmdReadme_Click;
            CombustionBox.CheckedChanged += Combustion_Click;
            MeteorBox.CheckedChanged += Meteor_Click;
            SpellLockBox.CheckedChanged += SpellLock_Click;
            IceBlockBox.CheckedChanged += IceBlock_Click;
            DragBrBox.CheckedChanged += DragBr_Click;
            RuneOfPowerBox.CheckedChanged += RuneOfPower_Click;
            LegendaryBox.CheckedChanged += Legendary_Click;
            LivingBombBox.CheckedChanged += LivingBomb_Click;
            MirrorsBox.CheckedChanged += Mirrors_Click;
            CinderstormBox.CheckedChanged += Cinderstorm_Click;
            ROFBox.CheckedChanged += ROF_Click;
            BarrierBox.CheckedChanged += Barrier_Click;

            SettingsForm.Controls.Add(cmdSave);
            SettingsForm.Controls.Add(cmdReadme);
            lblCombustionText.BringToFront();
            lblMeteorText.BringToFront();
            lblSpellLockText.BringToFront();
            lblIceBlockText.BringToFront();
            lblDragBrText.BringToFront();
            lblRuneOfPowerText.BringToFront();
            lblLegendaryText.BringToFront();
            lblLivingBombText.BringToFront();
            lblMirrorsText.BringToFront();
            lblCinderstormText.BringToFront();
            lblROFText.BringToFront();
            lblDefensivesText.BringToFront();
            lblCooldownzText.BringToFront();
            lblBarrierText.BringToFront();
        }

        // Privates cmdsave/checkboxes
        private void CmdReadme_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "README:\n \n \n Toogle Cooldown Hotkey : NUMPAD0 - Make sure to disable the Jump bind from keybindings. \n The rotation is pooling 3 charges of Fire Blast and minimum 2 Phoenix's Flames before using Combustion. \n In the opener, the first of 3 charges of Phoenix's Flames will be used on the first Heating Up proc. \n If Cooldowns are disabled, the rotation will always keep one charge of Phoenix's Flames. \n \n Make sure you've set your keybindings corresponding the Spellbook and viceversa. \n \n \n Please report any bugs on Discord Mage channel. ",
                "PixelMagic", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CmdSave_Click(object sender, EventArgs e)
        {
            Combustion = CombustionBox.Checked;
            Meteor = MeteorBox.Checked;
            SpellLock = SpellLockBox.Checked;
            IceBlock = IceBlockBox.Checked;
            Legendary = LegendaryBox.Checked;
            LivingBomb = LivingBombBox.Checked;
            Mirrors = MirrorsBox.Checked;
            RuneOfPower = RuneOfPowerBox.Checked;
            DragBr = DragBrBox.Checked;
            Cinderstorm = CinderstormBox.Checked;
            ROF = ROFBox.Checked;
            Barrier = BarrierBox.Checked;
            MessageBox.Show("Settings saved.", "PixelMagic", MessageBoxButtons.OK, MessageBoxIcon.Information);
            SettingsForm.Close();
        }

        // Checkboxes
        private void Combustion_Click(object sender, EventArgs e)
        {
            Combustion = CombustionBox.Checked;
        }

        private void Mirrors_Click(object sender, EventArgs e)
        {
            Mirrors = MirrorsBox.Checked;
        }

        private void Meteor_Click(object sender, EventArgs e)
        {
            Meteor = MeteorBox.Checked;
        }

        private void SpellLock_Click(object sender, EventArgs e)
        {
            SpellLock = SpellLockBox.Checked;
        }

        private void IceBlock_Click(object sender, EventArgs e)
        {
            IceBlock = IceBlockBox.Checked;
        }

        private void DragBr_Click(object sender, EventArgs e)
        {
            DragBr = DragBrBox.Checked;
        }

        private void RuneOfPower_Click(object sender, EventArgs e)
        {
            RuneOfPower = RuneOfPowerBox.Checked;
        }

        private void Legendary_Click(object sender, EventArgs e)
        {
            Legendary = LegendaryBox.Checked;
        }

        private void LivingBomb_Click(object sender, EventArgs e)
        {
            LivingBomb = LivingBombBox.Checked;
        }

        private void Cinderstorm_Click(object sender, EventArgs e)
        {
            Cinderstorm = CinderstormBox.Checked;
        }

        private void ROF_Click(object sender, EventArgs e)
        {
            ROF = ROFBox.Checked;
        }

        private void Barrier_Click(object sender, EventArgs e)
        {
            Barrier = BarrierBox.Checked;
        }

        //end checkboxes
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


                    if (WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsInCombat)
                    {
                        if (UseCooldowns && RuneOfPower && WoW.PlayerSpellCharges("Rune of Power") >= 1 && WoW.SpellCooldownTimeRemaining("Combustion") >= 40 &&
                            WoW.PlayerHasBuff("Hot Streak!") && !WoW.PlayerIsCasting && !WoW.IsMoving && !WoW.PlayerHasBuff("Rune of Power") && !WoW.IsSpellOnCooldown("Rune of Power") ||
                            (UseCooldowns && RuneOfPower && !WoW.PlayerIsCasting && WoW.PlayerSpellCharges("Fire Blast") == 3 && WoW.PlayerSpellCharges("Phoenix's Flames") >= 2 &&
                             !WoW.IsMoving && !WoW.IsSpellOnCooldown("Combustion") && WoW.PlayerSpellCharges("Rune of Power") >= 1 && WoW.PlayerHasBuff("Hot Streak!") &&
                             !WoW.PlayerHasBuff("Rune of Power") && !WoW.IsSpellOnCooldown("Rune of Power")))
                        {
                            WoW.CastSpell("Rune of Power");
                            return;
                        }
                        if (UseCooldowns && Mirrors && WoW.CanCast("Mirror Image") && !WoW.IsSpellOnCooldown("Mirror Image") && WoW.PlayerHasBuff("Combustion"))
                        {
                            WoW.CastSpell("Mirror Image");
                            return;
                        }
                        if (WoW.CanCast("Scorch") && WoW.IsMoving && !WoW.PlayerHasBuff("Hot Streak!"))
                        {
                            WoW.CastSpell("Scorch");
                            return;
                        }
                        if (WoW.IsInCombat && Meteor && Control.ModifierKeys == Keys.Alt && !WoW.PlayerIsCasting)
                        {
                            WoW.CastSpell("Meteor");
                            return;
                        }
                        if (WoW.CanCast("Phoenix's Flames") && !WoW.PlayerHasBuff("Hot Streak!") && WoW.IsSpellOnCooldown("Fire Blast") && WoW.SpellCooldownTimeRemaining("Fire Blast") > 1.1 &&
                            WoW.PlayerHasBuff("Heating Up") && UseCooldowns && Combustion && WoW.PlayerSpellCharges("Phoenix's Flames") == 3 && !WoW.IsSpellOnCooldown("Combustion"))
                        {
                            WoW.CastSpell("Phoenix's Flames");
                            return;
                        }
                        if (WoW.PlayerIsCasting && UseCooldowns && WoW.PlayerHasBuff("Heating Up") && Combustion && WoW.PlayerSpellCharges("Phoenix's Flames") == 3 &&
                            !WoW.IsSpellOnCooldown("Combustion"))
                        {
                            WoW.CastSpell("Phoenix's Flames");
                            return;
                        }
                        if (WoW.CanCast("Phoenix's Flames") && !WoW.PlayerHasBuff("Hot Streak!") && WoW.PlayerHasBuff("Heating Up") && WoW.LastSpell != "Fire Blast" &&
                            WoW.LastSpell != "Phoenix's Flames" && WoW.IsSpellOnCooldown("Fire Blast") && WoW.SpellCooldownTimeRemaining("Fire Blast") > 1.1 && !UseCooldowns && Combustion &&
                            WoW.PlayerSpellCharges("Phoenix's Flames") >= 2 && !WoW.IsSpellOnCooldown("Combustion"))
                        {
                            WoW.CastSpell("Phoenix's Flames");
                            return;
                        }
                        if (!UseCooldowns && Combustion && WoW.PlayerIsCasting && WoW.LastSpell != "Phoenix's Flame" && WoW.LastSpell != "Fire Blast" && WoW.PlayerHasBuff("Heating Up") &&
                            WoW.PlayerSpellCharges("Fire Blast") >= 1 && !WoW.PlayerHasBuff("Hot Streak!"))
                        {
                            WoW.CastSpell("Fire Blast");
                            return;
                        }
                        if (WoW.CanCast("Phoenix's Flames") && WoW.IsSpellOnCooldown("Fire Blast") && WoW.SpellCooldownTimeRemaining("Fire Blast") > 1.1 && WoW.PlayerHasBuff("Heating Up") &&
                            Combustion && WoW.PlayerSpellCharges("Phoenix's Flames") >= 1 && WoW.SpellCooldownTimeRemaining("Combustion") > 60 && !WoW.PlayerHasBuff("Hot Streak!"))
                        {
                            WoW.CastSpell("Phoenix's Flames");
                            return;
                        }

                        if (WoW.IsInCombat && Control.ModifierKeys == Keys.Alt && ROF && !WoW.PlayerIsCasting)
                        {
                            WoW.CastSpell("Ring of Frost");
                            return;
                        }
                        if (Barrier && WoW.CanCast("Blazing Barrier") && WoW.HealthPercent <= 80 && !WoW.IsSpellOnCooldown("Blazing Barrier") && !WoW.PlayerHasBuff("Blazing Barrier"))
                        {
                            WoW.CastSpell("Blazing Barrier");
                            return;
                        }

                        if (!UseCooldowns && WoW.CanCast("Cinderstorm") && Cinderstorm && WoW.TargetHasDebuff("Ignite") && !WoW.PlayerHasBuff("Combustion") && !WoW.PlayerHasBuff("Hot Streak!"))
                        {
                            WoW.CastSpell("Cinderstorm");
                            return;
                        }
                        if (UseCooldowns && WoW.CanCast("Cinderstorm") && Cinderstorm && WoW.TargetHasDebuff("Ignite") && !WoW.PlayerHasBuff("Combustion") &&
                            WoW.IsSpellOnCooldown("Combustion") && WoW.SpellCooldownTimeRemaining("Combustion") > 5 && !WoW.PlayerHasBuff("Hot Streak!"))
                        {
                            WoW.CastSpell("Cinderstorm");
                            return;
                        }
                        if (UseCooldowns && Combustion && !WoW.IsSpellOnCooldown("Combustion") && WoW.PlayerHasBuff("Rune of Power") && WoW.PlayerSpellCharges("Fire Blast") == 3 &&
                            WoW.PlayerSpellCharges("Phoenix's Flames") >= 2 && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling ||
                            UseCooldowns && Combustion && Mirrors && WoW.PlayerSpellCharges("Fire Blast") == 3 && WoW.PlayerSpellCharges("Phoenix's Flames") >= 2 &&
                            WoW.PlayerHasBuff("Hot Streak!") && !WoW.IsSpellOnCooldown("Combustion") && !WoW.IsSpellOnCooldown("Mirror Image") && !WoW.PlayerIsCasting &&
                            !WoW.PlayerIsChanneling)
                        {
                            WoW.CastSpell("Combustion");
                            return;
                        }

                        if (DragBr && WoW.CanCast("Dragon's Breath") && WoW.IsSpellInRange("Scorch") && !WoW.PlayerHasBuff("Combustion"))
                        {
                            WoW.CastSpell("Dragon's Breath");
                            return;
                        }
                        if (WoW.PlayerSpellCharges("Phoenix's Flames") > 2 && !WoW.PlayerHasBuff("Combustion") && WoW.SpellCooldownTimeRemaining("Combustion") > 20 &&
                            WoW.CanCast("Phoenix's Flames") && WoW.PlayerHasBuff("Heating Up") && !WoW.PlayerHasBuff("Hot Streak!") && WoW.PlayerSpellCharges("Phoenix's Flames") >= 1 &&
                            WoW.IsSpellOnCooldown("Fire Blast") && WoW.SpellCooldownTimeRemaining("Fire Blast") > 1.1 && WoW.LastSpell != "Phoenix's Flames" && WoW.LastSpell != "Fire Blast" ||
                            WoW.SpellCooldownTimeRemaining("Combustion") < 20 && WoW.CanCast("Phoenix's Flames") && WoW.PlayerHasBuff("Heating Up") && !WoW.PlayerHasBuff("Hot Streak!") &&
                            WoW.PlayerSpellCharges("Phoenix's Flames") == 3 && WoW.LastSpell != "Phoenix's Flames" && WoW.LastSpell != "Fire Blast")
                        {
                            WoW.CastSpell("Phoenix's Flames");
                            return;
                        }
                        if (WoW.CanCast("Phoenix's Flames") && WoW.PlayerHasBuff("Heating Up") && WoW.PlayerHasBuff("Combustion") && !WoW.PlayerHasBuff("Hot Streak!") &&
                            WoW.PlayerSpellCharges("Phoenix's Flames") >= 1 && WoW.SpellCooldownTimeRemaining("Fire Blast") > 0.5 && WoW.LastSpell != "Phoenix's Flames" &&
                            WoW.LastSpell != "Fire Blast")
                        {
                            WoW.CastSpell("Phoenix's Flames");
                            return;
                        }

                        if (WoW.CanCast("Living Bomb") && LivingBomb && !WoW.IsSpellOnCooldown("Living Bomb") && !WoW.PlayerHasBuff("Hot Streak!"))
                        {
                            WoW.CastSpell("Living Bomb");
                            return;
                        }
                        if (WoW.CanCast("Ice Block") && !WoW.PlayerHasBuff("Ice Block") && IceBlock && WoW.HealthPercent < 20 && !WoW.IsSpellOnCooldown("Ice Block"))
                        {
                            WoW.CastSpell("Ice Block");
                            Log.Write("--------Activating Ice Block, you were below 20%HealthPoints.--------");
                            return;
                        }
                        // Legendary Bracers Support.
                        if (Legendary && WoW.CanCast("Pyroblast") && !WoW.WasLastCasted("Pyroblast") && !WoW.PlayerIsCasting && !WoW.PlayerHasBuff("Combustion") &&
                            WoW.PlayerBuffTimeRemaining("Marquee Bindings of the Sun King") > 4 && WoW.PlayerHasBuff("Marquee Bindings of the Sun King") && !WoW.PlayerHasBuff("Hot Streak!") ||
                            (!WoW.PlayerHasBuff("Combustion") && !WoW.WasLastCasted("Pyroblast") && WoW.PlayerHasBuff("Ice Floes") && !WoW.PlayerIsCasting &&
                             WoW.PlayerHasBuff("Marquee Bindings of the Sun King") && !WoW.PlayerHasBuff("Hot Streak!")))
                        {
                            WoW.CastSpell("Pyroblast");
                            return;
                        }
                        if (WoW.CanCast("Pyroblast") && WoW.PlayerHasBuff("Hot Streak!") && WoW.IsSpellOnCooldown("Combustion"))
                        {
                            WoW.CastSpell("Pyroblast");
                            return;
                        }
                        if (WoW.CanCast("Pyroblast") && WoW.PlayerHasBuff("Hot Streak!") && !WoW.IsSpellOnCooldown("Combustion") && WoW.PlayerSpellCharges("Phoenix's Flames") < 2)
                        {
                            WoW.CastSpell("Pyroblast");
                            return;
                        }
                        if (WoW.CanCast("Pyroblast") && WoW.PlayerHasBuff("Hot Streak!") && WoW.LastSpell == "Combustion")
                        {
                            WoW.CastSpell("Pyroblast");
                            return;
                        }
                        if (WoW.CanCast("Pyroblast") && !UseCooldowns && Combustion && WoW.PlayerHasBuff("Hot Streak!"))
                        {
                            WoW.CastSpell("Pyroblast");
                            return;
                        }

                        if (WoW.CanCast("Fireball") && WoW.IsSpellOnCooldown("Fire Blast") && !WoW.PlayerIsCasting && WoW.IsSpellOnCooldown("Phoenix's Flames") &&
                            WoW.PlayerHasBuff("Heating Up") && !WoW.PlayerHasBuff("Hot Streak!"))
                        {
                            WoW.CastSpell("Fireball");
                            return;
                        }
                        if (WoW.SpellCooldownTimeRemaining("Combustion") > 20 && WoW.SpellCooldownTimeRemaining("Phoenix's Flames") > 1.3 && WoW.PlayerIsCasting &&
                            WoW.LastSpell != "Phoenix's Flame" && WoW.LastSpell != "Fire Blast" && WoW.PlayerHasBuff("Heating Up") && WoW.PlayerSpellCharges("Fire Blast") >= 1 &&
                            !WoW.PlayerHasBuff("Hot Streak!"))
                        {
                            WoW.CastSpell("Fire Blast");
                            return;
                        }
                        if (WoW.SpellCooldownTimeRemaining("Combustion") > 20 && WoW.LastSpell != "Phoenix's Flame" && WoW.LastSpell != "Fire Blast" && WoW.PlayerHasBuff("Heating Up") &&
                            WoW.PlayerSpellCharges("Fire Blast") >= 1 && !WoW.PlayerHasBuff("Hot Streak!"))
                        {
                            WoW.CastSpell("Fire Blast");
                            return;
                        }


                        if (!WoW.PlayerIsCasting && !WoW.PlayerHasBuff("Heating Up") && WoW.CanCast("Fireball") && WoW.IsSpellInRange("Fireball") && !WoW.IsMoving && !WoW.PlayerIsChanneling &&
                            !WoW.PlayerHasBuff("Hot Streak!"))
                        {
                            WoW.CastSpell("Fireball");
                            return;
                        }
                        if (!WoW.PlayerIsCasting && WoW.CanCast("Fireball") && WoW.IsSpellInRange("Fireball") && !WoW.IsMoving && !WoW.PlayerIsChanneling && WoW.PlayerHasBuff("Heating Up") &&
                            WoW.IsSpellOnCooldown("Fire Blast") && WoW.IsSpellOnCooldown("Phoenix's Flames") && !WoW.PlayerHasBuff("Hot Streak!"))
                        {
                            WoW.CastSpell("Fireball");
                            return;
                        }
                        if (WoW.IsMoving && WoW.PlayerHasBuff("Ice Floes") && WoW.IsSpellInRange("Fireball") && !WoW.PlayerHasBuff("Hot Streak!") && !WoW.PlayerHasBuff("Heating Up"))
                        {
                            WoW.CastSpell("Fireball");
                            return;
                        }
                        if (!WoW.PlayerHasBuff("Combustion") && !WoW.IsMoving && WoW.CanCast("Fireball") && !WoW.PlayerIsCasting && WoW.PlayerHasBuff("Heating Up") &&
                            WoW.SpellCooldownTimeRemaining("Combustion") <= 20 && WoW.PlayerSpellCharges("Fire Blast") >= 1 && WoW.PlayerSpellCharges("Phoenix's Flames") >= 1 ||
                            WoW.CanCast("Fireball") && !WoW.IsMoving && !WoW.PlayerIsCasting && !WoW.PlayerHasBuff("Combustion") && WoW.PlayerHasBuff("Heating Up") &&
                            WoW.IsSpellOnCooldown("Fire Blast") && WoW.IsSpellOnCooldown("Phoenix's Flames") && WoW.SpellCooldownTimeRemaining("Combustion") <= 20 ||
                            WoW.CanCast("Fireball") && !WoW.IsMoving && !WoW.PlayerIsCasting && !WoW.PlayerHasBuff("Combustion") && WoW.PlayerHasBuff("Heating Up") &&
                            !WoW.IsSpellOnCooldown("Fire Blast") && WoW.IsSpellOnCooldown("Phoenix's Flames") && WoW.SpellCooldownTimeRemaining("Combustion") <= 20 ||
                            WoW.CanCast("Fireball") && !WoW.IsMoving && !WoW.PlayerIsCasting && !WoW.PlayerHasBuff("Combustion") && WoW.PlayerHasBuff("Heating Up") &&
                            WoW.IsSpellOnCooldown("Fire Blast") && !WoW.IsSpellOnCooldown("Phoenix's Flames") && WoW.SpellCooldownTimeRemaining("Combustion") >= 20 ||
                            WoW.CanCast("Fireball") && !WoW.IsMoving && !WoW.PlayerIsCasting && !WoW.PlayerHasBuff("Combustion") && WoW.PlayerHasBuff("Heating Up") &&
                            WoW.IsSpellOnCooldown("Fire Blast") && !WoW.IsSpellOnCooldown("Phoenix's Flames") && WoW.SpellCooldownTimeRemaining("Combustion") <= 20)
                        {
                            WoW.CastSpell("Fireball");
                            return;
                        }
                    }
            }
            if (combatRoutine.Type == RotationType.AOE)
            {
                if (UseCooldowns && RuneOfPower && WoW.PlayerSpellCharges("Rune of Power") >= 1 && WoW.SpellCooldownTimeRemaining("Combustion") >= 40 && WoW.PlayerHasBuff("Hot Streak!") &&
                    !WoW.PlayerIsCasting && !WoW.IsMoving && !WoW.PlayerHasBuff("Rune of Power") && !WoW.IsSpellOnCooldown("Rune of Power") ||
                    (UseCooldowns && RuneOfPower && !WoW.PlayerIsCasting && WoW.PlayerSpellCharges("Fire Blast") == 3 && WoW.PlayerSpellCharges("Phoenix's Flames") >= 2 && !WoW.IsMoving &&
                     !WoW.IsSpellOnCooldown("Combustion") && WoW.PlayerSpellCharges("Rune of Power") >= 1 && WoW.PlayerHasBuff("Hot Streak!") && !WoW.PlayerHasBuff("Rune of Power") &&
                     !WoW.IsSpellOnCooldown("Rune of Power")))
                {
                    WoW.CastSpell("Rune of Power");
                    return;
                }
                if (UseCooldowns && Mirrors && WoW.CanCast("Mirror Image") && !WoW.IsSpellOnCooldown("Mirror Image") && WoW.PlayerHasBuff("Combustion"))
                {
                    WoW.CastSpell("Mirror Image");
                    return;
                }
                if (WoW.CanCast("Scorch") && WoW.IsMoving && !WoW.PlayerHasBuff("Hot Streak!"))
                {
                    WoW.CastSpell("Scorch");
                    return;
                }
                if (WoW.IsInCombat && Meteor && Control.ModifierKeys == Keys.Alt && !WoW.PlayerIsCasting)
                {
                    WoW.CastSpell("Meteor");
                    return;
                }
                if (WoW.CanCast("Phoenix's Flames") && !WoW.PlayerHasBuff("Hot Streak!") && WoW.IsSpellOnCooldown("Fire Blast") && WoW.SpellCooldownTimeRemaining("Fire Blast") > 1.1 &&
                    WoW.PlayerHasBuff("Heating Up") && UseCooldowns && Combustion && WoW.PlayerSpellCharges("Phoenix's Flames") == 3 && !WoW.IsSpellOnCooldown("Combustion"))
                {
                    WoW.CastSpell("Phoenix's Flames");
                    return;
                }
                if (WoW.PlayerIsCasting && UseCooldowns && WoW.PlayerHasBuff("Heating Up") && Combustion && WoW.PlayerSpellCharges("Phoenix's Flames") == 3 &&
                    !WoW.IsSpellOnCooldown("Combustion"))
                {
                    WoW.CastSpell("Phoenix's Flames");
                    return;
                }
                if (WoW.CanCast("Phoenix's Flames") && !WoW.PlayerHasBuff("Hot Streak!") && WoW.PlayerHasBuff("Heating Up") && WoW.LastSpell != "Fire Blast" &&
                    WoW.LastSpell != "Phoenix's Flames" && WoW.IsSpellOnCooldown("Fire Blast") && WoW.SpellCooldownTimeRemaining("Fire Blast") > 1.1 && !UseCooldowns && Combustion &&
                    WoW.PlayerSpellCharges("Phoenix's Flames") >= 2 && !WoW.IsSpellOnCooldown("Combustion"))
                {
                    WoW.CastSpell("Phoenix's Flames");
                    return;
                }
                if (!UseCooldowns && Combustion && WoW.PlayerIsCasting && WoW.LastSpell != "Phoenix's Flame" && WoW.LastSpell != "Fire Blast" && WoW.PlayerHasBuff("Heating Up") &&
                    WoW.PlayerSpellCharges("Fire Blast") >= 1 && !WoW.PlayerHasBuff("Hot Streak!"))
                {
                    WoW.CastSpell("Fire Blast");
                    return;
                }
                if (WoW.CanCast("Phoenix's Flames") && WoW.IsSpellOnCooldown("Fire Blast") && WoW.SpellCooldownTimeRemaining("Fire Blast") > 1.1 && WoW.PlayerHasBuff("Heating Up") &&
                    Combustion && WoW.PlayerSpellCharges("Phoenix's Flames") >= 1 && WoW.SpellCooldownTimeRemaining("Combustion") > 60 && !WoW.PlayerHasBuff("Hot Streak!"))
                {
                    WoW.CastSpell("Phoenix's Flames");
                    return;
                }

                if (WoW.IsInCombat && Control.ModifierKeys == Keys.Alt && ROF && !WoW.PlayerIsCasting)
                {
                    WoW.CastSpell("Ring of Frost");
                    return;
                }
                if (Barrier && WoW.CanCast("Blazing Barrier") && WoW.HealthPercent <= 80 && !WoW.IsSpellOnCooldown("Blazing Barrier") && !WoW.PlayerHasBuff("Blazing Barrier"))
                {
                    WoW.CastSpell("Blazing Barrier");
                    return;
                }

                if (!UseCooldowns && WoW.CanCast("Cinderstorm") && Cinderstorm && WoW.TargetHasDebuff("Ignite") && !WoW.PlayerHasBuff("Combustion") && !WoW.PlayerHasBuff("Hot Streak!"))
                {
                    WoW.CastSpell("Cinderstorm");
                    return;
                }
                if (UseCooldowns && WoW.CanCast("Cinderstorm") && Cinderstorm && WoW.TargetHasDebuff("Ignite") && !WoW.PlayerHasBuff("Combustion") && WoW.IsSpellOnCooldown("Combustion") &&
                    WoW.SpellCooldownTimeRemaining("Combustion") > 5 && !WoW.PlayerHasBuff("Hot Streak!"))
                {
                    WoW.CastSpell("Cinderstorm");
                    return;
                }
                if (UseCooldowns && Combustion && !WoW.IsSpellOnCooldown("Combustion") && WoW.PlayerHasBuff("Rune of Power") && WoW.PlayerSpellCharges("Fire Blast") == 3 &&
                    WoW.PlayerSpellCharges("Phoenix's Flames") >= 2 && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling ||
                    UseCooldowns && Combustion && Mirrors && WoW.PlayerSpellCharges("Fire Blast") == 3 && WoW.PlayerSpellCharges("Phoenix's Flames") >= 2 && WoW.PlayerHasBuff("Hot Streak!") &&
                    !WoW.IsSpellOnCooldown("Combustion") && !WoW.IsSpellOnCooldown("Mirror Image") && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling)
                {
                    WoW.CastSpell("Combustion");
                    return;
                }

                if (DragBr && WoW.CanCast("Dragon's Breath") && WoW.IsSpellInRange("Scorch") && !WoW.PlayerHasBuff("Combustion"))
                {
                    WoW.CastSpell("Dragon's Breath");
                    return;
                }
                if (WoW.PlayerSpellCharges("Phoenix's Flames") > 2 && !WoW.PlayerHasBuff("Combustion") && WoW.SpellCooldownTimeRemaining("Combustion") > 20 && WoW.CanCast("Phoenix's Flames") &&
                    WoW.PlayerHasBuff("Heating Up") && !WoW.PlayerHasBuff("Hot Streak!") && WoW.PlayerSpellCharges("Phoenix's Flames") >= 1 && WoW.IsSpellOnCooldown("Fire Blast") &&
                    WoW.SpellCooldownTimeRemaining("Fire Blast") > 1.1 && WoW.LastSpell != "Phoenix's Flames" && WoW.LastSpell != "Fire Blast" ||
                    WoW.SpellCooldownTimeRemaining("Combustion") < 20 && WoW.CanCast("Phoenix's Flames") && WoW.PlayerHasBuff("Heating Up") && !WoW.PlayerHasBuff("Hot Streak!") &&
                    WoW.PlayerSpellCharges("Phoenix's Flames") == 3 && WoW.LastSpell != "Phoenix's Flames" && WoW.LastSpell != "Fire Blast")
                {
                    WoW.CastSpell("Phoenix's Flames");
                    return;
                }
                if (WoW.CanCast("Phoenix's Flames") && WoW.PlayerHasBuff("Heating Up") && WoW.PlayerHasBuff("Combustion") && !WoW.PlayerHasBuff("Hot Streak!") &&
                    WoW.PlayerSpellCharges("Phoenix's Flames") >= 1 && WoW.SpellCooldownTimeRemaining("Fire Blast") > 0.5 && WoW.LastSpell != "Phoenix's Flames" &&
                    WoW.LastSpell != "Fire Blast")
                {
                    WoW.CastSpell("Phoenix's Flames");
                    return;
                }

                if (WoW.CanCast("Living Bomb") && LivingBomb && !WoW.IsSpellOnCooldown("Living Bomb") && !WoW.PlayerHasBuff("Hot Streak!"))
                {
                    WoW.CastSpell("Living Bomb");
                    return;
                }
                if (WoW.CanCast("Ice Block") && !WoW.PlayerHasBuff("Ice Block") && IceBlock && WoW.HealthPercent < 20 && !WoW.IsSpellOnCooldown("Ice Block"))
                {
                    WoW.CastSpell("Ice Block");
                    Log.Write("--------Activating Ice Block, you were below 20%HealthPoints.--------");
                    return;
                }
                // Legendary Bracers Support.
                if (Legendary && WoW.CanCast("Pyroblast") && !WoW.WasLastCasted("Pyroblast") && !WoW.PlayerIsCasting && !WoW.PlayerHasBuff("Combustion") &&
                    WoW.PlayerBuffTimeRemaining("Marquee Bindings of the Sun King") > 4 && WoW.PlayerHasBuff("Marquee Bindings of the Sun King") && !WoW.PlayerHasBuff("Hot Streak!") ||
                    (!WoW.PlayerHasBuff("Combustion") && !WoW.WasLastCasted("Pyroblast") && WoW.PlayerHasBuff("Ice Floes") && !WoW.PlayerIsCasting &&
                     WoW.PlayerHasBuff("Marquee Bindings of the Sun King") && !WoW.PlayerHasBuff("Hot Streak!")))
                {
                    WoW.CastSpell("Pyroblast");
                    return;
                }
                if (!WoW.PlayerIsCasting && WoW.CanCast("Flamestrike") && WoW.PlayerHasBuff("Hot Streak!") && WoW.IsSpellOnCooldown("Combustion"))
                {
                    WoW.CastSpell("Flamestrike");
                    return;
                }
                if (!WoW.PlayerIsCasting && WoW.CanCast("Flamestrike") && WoW.PlayerHasBuff("Hot Streak!") && !WoW.IsSpellOnCooldown("Combustion") &&
                    WoW.PlayerSpellCharges("Phoenix's Flames") < 2)
                {
                    WoW.CastSpell("Flamestrike");
                    return;
                }
                if (!WoW.PlayerIsCasting && WoW.CanCast("Flamestrike") && WoW.PlayerHasBuff("Hot Streak!") && WoW.LastSpell == "Combustion")
                {
                    WoW.CastSpell("Flamestrike");
                    return;
                }
                if (!WoW.PlayerIsCasting && WoW.CanCast("Flamestrike") && !UseCooldowns && Combustion && WoW.PlayerHasBuff("Hot Streak!"))
                {
                    WoW.CastSpell("Flamestrike");
                    return;
                }
                if (WoW.CanCast("Fireball") && WoW.IsSpellOnCooldown("Fire Blast") && !WoW.PlayerIsCasting && WoW.IsSpellOnCooldown("Phoenix's Flames") && WoW.PlayerHasBuff("Heating Up") &&
                    !WoW.PlayerHasBuff("Hot Streak!"))
                {
                    WoW.CastSpell("Fireball");
                    return;
                }
                if (WoW.SpellCooldownTimeRemaining("Combustion") > 20 && WoW.SpellCooldownTimeRemaining("Phoenix's Flames") > 1.3 && WoW.PlayerIsCasting && WoW.LastSpell != "Phoenix's Flame" &&
                    WoW.LastSpell != "Fire Blast" && WoW.PlayerHasBuff("Heating Up") && WoW.PlayerSpellCharges("Fire Blast") >= 1 && !WoW.PlayerHasBuff("Hot Streak!"))
                {
                    WoW.CastSpell("Fire Blast");
                    return;
                }
                if (WoW.SpellCooldownTimeRemaining("Combustion") > 20 && WoW.LastSpell != "Phoenix's Flame" && WoW.LastSpell != "Fire Blast" && WoW.PlayerHasBuff("Heating Up") &&
                    WoW.PlayerSpellCharges("Fire Blast") >= 1 && !WoW.PlayerHasBuff("Hot Streak!"))
                {
                    WoW.CastSpell("Fire Blast");
                    return;
                }


                if (!WoW.PlayerIsCasting && !WoW.PlayerHasBuff("Heating Up") && WoW.CanCast("Fireball") && WoW.IsSpellInRange("Fireball") && !WoW.IsMoving && !WoW.PlayerIsChanneling &&
                    !WoW.PlayerHasBuff("Hot Streak!"))
                {
                    WoW.CastSpell("Fireball");
                    return;
                }
                if (!WoW.PlayerIsCasting && WoW.CanCast("Fireball") && WoW.IsSpellInRange("Fireball") && !WoW.IsMoving && !WoW.PlayerIsChanneling && WoW.PlayerHasBuff("Heating Up") &&
                    WoW.IsSpellOnCooldown("Fire Blast") && WoW.IsSpellOnCooldown("Phoenix's Flames") && !WoW.PlayerHasBuff("Hot Streak!"))
                {
                    WoW.CastSpell("Fireball");
                    return;
                }
                if (WoW.IsMoving && WoW.PlayerHasBuff("Ice Floes") && WoW.IsSpellInRange("Fireball") && !WoW.PlayerHasBuff("Hot Streak!") && !WoW.PlayerHasBuff("Heating Up"))
                {
                    WoW.CastSpell("Fireball");
                    return;
                }
                if (!WoW.PlayerHasBuff("Combustion") && !WoW.IsMoving && WoW.CanCast("Fireball") && !WoW.PlayerIsCasting && WoW.PlayerHasBuff("Heating Up") &&
                    WoW.SpellCooldownTimeRemaining("Combustion") <= 20 && WoW.PlayerSpellCharges("Fire Blast") >= 1 && WoW.PlayerSpellCharges("Phoenix's Flames") >= 1 ||
                    WoW.CanCast("Fireball") && !WoW.IsMoving && !WoW.PlayerIsCasting && !WoW.PlayerHasBuff("Combustion") && WoW.PlayerHasBuff("Heating Up") &&
                    WoW.IsSpellOnCooldown("Fire Blast") && WoW.IsSpellOnCooldown("Phoenix's Flames") && WoW.SpellCooldownTimeRemaining("Combustion") <= 20 ||
                    WoW.CanCast("Fireball") && !WoW.IsMoving && !WoW.PlayerIsCasting && !WoW.PlayerHasBuff("Combustion") && WoW.PlayerHasBuff("Heating Up") &&
                    !WoW.IsSpellOnCooldown("Fire Blast") && WoW.IsSpellOnCooldown("Phoenix's Flames") && WoW.SpellCooldownTimeRemaining("Combustion") <= 20 ||
                    WoW.CanCast("Fireball") && !WoW.IsMoving && !WoW.PlayerIsCasting && !WoW.PlayerHasBuff("Combustion") && WoW.PlayerHasBuff("Heating Up") &&
                    WoW.IsSpellOnCooldown("Fire Blast") && !WoW.IsSpellOnCooldown("Phoenix's Flames") && WoW.SpellCooldownTimeRemaining("Combustion") >= 20 ||
                    WoW.CanCast("Fireball") && !WoW.IsMoving && !WoW.PlayerIsCasting && !WoW.PlayerHasBuff("Combustion") && WoW.PlayerHasBuff("Heating Up") &&
                    WoW.IsSpellOnCooldown("Fire Blast") && !WoW.IsSpellOnCooldown("Phoenix's Flames") && WoW.SpellCooldownTimeRemaining("Combustion") <= 20)
                {
                    WoW.CastSpell("Fireball");
                }
            }
        }
    }
}

/*
[AddonDetails.db]
AddonAuthor=Creepyjoker
AddonName=Sucstobeyou
WoWVersion=Legion - 70300
[SpellBook.db]
Spell,190319,Combustion,E
Spell,198929,Cinderstorm,C
Spell,113724,Ring of Frost,D1
Spell,44457,Living Bomb,Q
Spell,45438,Ice Block,C
Spell,55342,Mirror Image,X
Spell,31661,Dragon's Breath,D9
Spell,2948,Scorch,D5
Spell,235313,Blazing Barrier,X
Spell,133,Fireball,D1
Spell,205029,Flame On,F
Spell,153561,Meteor,1
Spell,2120,Flamestrike,V
Spell,108853,Fire Blast,D2
Spell,11366,Pyroblast,D3
Spell,194466,Phoenix's Flames,D4
Spell,116011,Rune of Power,D0
Spell,108839,Ice Floes,R
Aura,190319,Combustion
Aura,235313,Blazing Barrier
Aura,194466,Phoenix's Flames
Aura,108839,Ice Floes
Aura,48108,Hot Streak!
Aura,48107,Heating Up
Aura,44457,Living Bomb
Aura,45438,Ice Block
Aura,12654,Ignite
Aura,116011,Rune of Power
Aura,209455,Marquee Bindings of the Sun King
*/
