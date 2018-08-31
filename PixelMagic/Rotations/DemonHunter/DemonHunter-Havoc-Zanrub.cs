// winifix@gmail.com
// ReSharper disable UnusedMember.Global


using System;
using System.Drawing;
using System.Windows.Forms;
using PixelMagic.Helpers;

namespace PixelMagic.Rotation
{
    public class DemonHunterHavocZR : CombatRoutine
    {
        private NumericUpDown nudBlurPercentValue;

        public override string Name => "Havoc Sample";

        public override string Class => "Demon Hunter";

        public override Form SettingsForm { get; set; }

        public override void Initialize()
        {
            Log.Write("Welcome to PixelMagic Havoc");

            if (ConfigFile.ReadValue("DemonHunter", "Blur Usage Percent") == "")
            {
                ConfigFile.WriteValue("Demonhunter", "Blur Usage Percent", "45");
            }
            Log.Write("Welcome to Havoc", Color.Green);
            Log.Write("Suggested build: http://www.wowhead.com/talent-calc/demon-hunter/havoc/c1Az");

            SettingsForm = new Form {Text = "Settings", StartPosition = FormStartPosition.CenterScreen, Width = 800, Height = 490, ShowIcon = false};
            var lblBlurPercent = new Label {Text = "Blur Health %", Left = 12, Top = 150};
            SettingsForm.Controls.Add(lblBlurPercent);

            nudBlurPercentValue = new NumericUpDown {Minimum = 0, Maximum = 100, Value = ConfigFile.ReadValue<decimal>("Demonhunter", "Blur Usage Percent"), Left = 108, Top = 145};
            SettingsForm.Controls.Add(nudBlurPercentValue);

            var cmdSave = new Button {Text = "Save", Width = 65, Height = 25, Left = 662, Top = 408, Size = new Size(108, 31)};
            cmdSave.Click += CmdSave_Click;

            SettingsForm.Controls.Add(cmdSave);
            nudBlurPercentValue.BringToFront();
        }

        private void CmdSave_Click(object sender, EventArgs e)
        {
            ConfigFile.WriteValue("Demonhunter", "Blur Usage Percent", nudBlurPercentValue.Value.ToString());
        }

        public override void Stop()
        {
        }

        public override void Pulse()
        {
            if (combatRoutine.Type == RotationType.SingleTarget) // Do Single Target Stuff here
            {
                if (WoW.HasTarget && !WoW.PlayerIsChanneling && WoW.TargetIsEnemy && WoW.IsInCombat)
                {
                    if (WoW.PlayerHasBuff("Metamorphosis"))
                    {
                        if (WoW.CanCast("Chaos Blades") && !WoW.IsSpellOnCooldown("Chaos Blades") && UseCooldowns)
                        {
                            WoW.CastSpell("Chaos Blades");
                            return;
                        }
                        if (WoW.CanCast("FOTI") && !WoW.IsSpellOnCooldown("FOTI") && !WoW.IsMoving && WoW.PlayerHasBuff("Momentum") && WoW.IsSpellInRange("Chaos Strike"))
                        {
                            WoW.CastSpell("FOTI");
                            return;
                        }
                        if (WoW.CanCast("Annihilation") && WoW.IsSpellInRange("Chaos Strike") && (WoW.Fury >= 40 && WoW.PlayerHasBuff("Momentum") || WoW.Fury >= 70))
                        {
                            WoW.CastSpell("Annihilation");
                            return;
                        }
                        if (WoW.CanCast("Throw Glaive") && WoW.IsSpellInRange("Throw Glaive") && (WoW.PlayerHasBuff("Momentum") || WoW.PlayerSpellCharges("Throw Glaive") == 2))
                        {
                            WoW.CastSpell("Throw Glaive");
                            return;
                        }
                        if (WoW.CanCast("Demons Bite") && WoW.IsSpellInRange("Chaos Strike") && WoW.Fury <= 70) // Fury Generator
                        {
                            WoW.CastSpell("Demons Bite");
                            return;
                        }
                    }
                    if (WoW.CanCast("Chaos Blades") && WoW.IsSpellInRange("Chaos Strike") && !WoW.IsSpellOnCooldown("Chaos Blades"))
                    {
                        WoW.CastSpell("Chaos Blades");
                        return;
                    }

                    if (WoW.CanCast("FOTI") && !WoW.IsSpellOnCooldown("FOTI") && !WoW.IsMoving && WoW.PlayerHasBuff("Momentum") && WoW.IsSpellInRange("Chaos Strike"))
                    {
                        WoW.CastSpell("FOTI");
                        return;
                    }
                    if (WoW.CanCast("Chaos Strike") && WoW.IsSpellInRange("Chaos Strike") && (WoW.Fury >= 40 && WoW.PlayerHasBuff("Momentum") || WoW.Fury >= 70)) // Fury Spender
                    {
                        WoW.CastSpell("Chaos Strike");
                        return;
                    }
                    if (WoW.CanCast("Throw Glaive") && WoW.IsSpellInRange("Throw Glaive") && (WoW.PlayerHasBuff("Momentum") || WoW.PlayerSpellCharges("Throw Glaive") == 2))
                    {
                        WoW.CastSpell("Throw Glaive");
                        return;
                    }
                    if (WoW.CanCast("Blur") && WoW.IsInCombat && !WoW.IsSpellOnCooldown("Blur") && WoW.HealthPercent <= ConfigFile.ReadValue<int>("Demonhunter", "Blur Usage Percent"))
                    {
                        WoW.CastSpell("Blur");
                        return;
                    }
                    if (WoW.CanCast("Demons Bite") && WoW.IsSpellInRange("Chaos Strike") && WoW.Fury <= 70) // Fury Generator
                    {
                        WoW.CastSpell("Demons Bite");
                        return;
                    }
                }
            }
            if (combatRoutine.Type == RotationType.AOE || combatRoutine.Type == RotationType.SingleTargetCleave)
            {
                // Do AOE Stuff here
                if (WoW.HasTarget && !WoW.PlayerIsChanneling && WoW.TargetIsEnemy && WoW.IsInCombat)
                {
                    if (WoW.PlayerHasBuff("Metamorphosis"))
                    {
                        if (WoW.CanCast("Chaos Blades") && WoW.IsSpellInRange("Chaos Strike") && !WoW.IsSpellOnCooldown("Chaos Blades"))
                        {
                            WoW.CastSpell("Chaos Blades");
                            return;
                        }

                        if (WoW.CanCast("FOTI") && !WoW.IsSpellOnCooldown("FOTI") && !WoW.IsMoving && WoW.PlayerHasBuff("Momentum") && WoW.IsSpellInRange("Chaos Strike"))
                        {
                            WoW.CastSpell("FOTI");
                            return;
                        }
                        if (WoW.CanCast("Eye Beam") && WoW.Fury >= 50 && WoW.PlayerHasBuff("Momentum") && WoW.IsSpellInRange("Chaos Strike"))
                        {
                            WoW.CastSpell("Eye Beam");
                            return;
                        }
                        if (WoW.CanCast("Death Sweep") && WoW.Fury >= 35 && WoW.IsSpellOnCooldown("Eye Beam") && WoW.PlayerHasBuff("Momentum") && WoW.IsSpellInRange("Chaos Strike"))
                        {
                            WoW.CastSpell("Death Sweep");
                            return;
                        }
                        if (WoW.CanCast("Annihilation") && WoW.IsSpellInRange("Chaos Strike") && WoW.IsSpellOnCooldown("Eye Beam") &&
                            (WoW.Fury >= 40 && WoW.PlayerHasBuff("Momentum") || WoW.Fury >= 70))
                        {
                            WoW.CastSpell("Annihilation");
                            return;
                        }
                        if (WoW.CanCast("Throw Glaive") && WoW.IsSpellInRange("Throw Glaive") && (WoW.PlayerHasBuff("Momentum") || WoW.PlayerSpellCharges("Throw Glaive") == 2))
                        {
                            WoW.CastSpell("Throw Glaive");
                            return;
                        }
                        if (WoW.CanCast("Demons Bite") && WoW.IsSpellInRange("Chaos Strike") && WoW.Fury <= 70) // Fury Generator
                        {
                            WoW.CastSpell("Demons Bite");
                            return;
                        }
                    }
                    if (WoW.CanCast("Chaos Blades") && !WoW.IsSpellOnCooldown("Chaos Blades") && UseCooldowns && WoW.IsSpellInRange("Chaos Strike"))
                    {
                        WoW.CastSpell("Chaos Blades");
                        return;
                    }

                    if (WoW.CanCast("FOTI") && WoW.IsSpellInRange("Chaos Strike") && !WoW.IsSpellOnCooldown("FOTI") && !WoW.IsMoving && WoW.PlayerHasBuff("Momentum"))
                    {
                        WoW.CastSpell("FOTI");
                        return;
                    }
                    if (WoW.CanCast("Eye Beam") && WoW.IsSpellInRange("Chaos Strike") && WoW.Fury >= 50 && WoW.PlayerHasBuff("Momentum"))
                    {
                        WoW.CastSpell("Eye Beam");
                        return;
                    }
                    if (WoW.CanCast("Blade Dance") && WoW.IsSpellInRange("Chaos Strike") && WoW.Fury >= 35 && WoW.IsSpellOnCooldown("Eye Beam") && WoW.PlayerHasBuff("Momentum"))
                    {
                        WoW.CastSpell("Blade Dance");
                        return;
                    }
                    if (WoW.CanCast("Chaos Strike") && WoW.IsSpellInRange("Chaos Strike") && WoW.IsSpellOnCooldown("Eye Beam") &&
                        (WoW.Fury >= 40 && WoW.PlayerHasBuff("Momentum") || WoW.Fury >= 70))
                    {
                        WoW.CastSpell("Chaos Strike");
                        return;
                    }
                    if (WoW.CanCast("Throw Glaive") && WoW.IsSpellInRange("Throw Glaive") && (WoW.PlayerHasBuff("Momentum") || WoW.PlayerSpellCharges("Throw Glaive") == 2))
                    {
                        WoW.CastSpell("Throw Glaive");
                        return;
                    }
                    if (WoW.CanCast("Blur") && WoW.IsInCombat && !WoW.IsSpellOnCooldown("Blur") && WoW.HealthPercent <= ConfigFile.ReadValue<int>("Demonhunter", "Blur Usage Percent"))
                    {
                        WoW.CastSpell("Blur");
                        return;
                    }
                    if (WoW.CanCast("Demons Bite") && WoW.IsSpellInRange("Chaos Strike") && WoW.Fury <= 70) // Fury Generator
                    {
                        WoW.CastSpell("Demons Bite");
                    }
                }
            }
        }
    }
}

/*
[AddonDetails.db]
AddonAuthor=Zanrub
AddonName=PixelMagic
WoWVersion=Legion - 70300
[SpellBook.db]
Spell,198013,Eye Beam,D4
Spell,195072,Fel Rush,D1
Spell,162243,Demons Bite,D2
Spell,162794,Chaos Strike,D3
Spell,185123,Throw Glaive,D5
Spell,188499,Blade Dance,D6
Spell,198793,Vengeful Retreat,D7
Spell,179057,Chaos Nova,D8
Spell,201427,Annihilation,D3
Spell,210152,Death Sweep,D6
Spell,191427,Metamorphosis,D9
Spell,198589,Blur,F2
Spell,211053,Fel Barrage,T
Spell,201467,FOTI,F4
Spell,211048,Chaos Blades,F1
Aura,162264,Metamorphosis
Aura,208628,Momentum
*/