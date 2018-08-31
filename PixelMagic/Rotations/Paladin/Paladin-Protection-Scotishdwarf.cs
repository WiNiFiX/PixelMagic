// winifix@gmail.com
// ReSharper disable UnusedMember.Global


// Changelog :
// Version r16
// - Fixed bugs on SoTR and HoTP/LoTP
// - Changed versioning
// Version r15
// - Fixed Lay on Hands
// Version r14
// - Added suggested talent build for the rotation.
// Version r13
// - Added toggle for talents Seraphim, Hand of the Protector and Blessed Hammer in Rotation settings, enable to use those talents.
// Version r12
// - Removed Interupt handling for Rebuke, will enable again once interupts are fixed.
// Version r11
//- Added Interupt handling for Rebuke and Avenger's Shield
//- Consecration checks if target has Consecration debuff
//- Blessed Hammer 1-2 stacks checks if target has Blessed Hammer debuff
//- Fixed bugs with cooldowns

// Credits to : Xcesius, WiNiFiX

using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using PixelMagic.Helpers;

namespace PixelMagic.Rotation
{
    public class PaladinProtection : CombatRoutine
    {
        private readonly Stopwatch interruptwatch = new Stopwatch();
        private CheckBox BlessedHammerBox;
        private CheckBox HotPBox;
        private CheckBox SeraphimBox;

        public override Form SettingsForm { get; set; }

        public override string Name => "Protection Paladin by Scotishdwarf r16";

        public override string Class => "Paladin";

        private static bool SeraphimBoss
        {
            get
            {
                var seraphimBoss = ConfigFile.ReadValue("PaladinProtection", "SeraphimBoss").Trim();

                return seraphimBoss != "" && Convert.ToBoolean(seraphimBoss);
            }
            set { ConfigFile.WriteValue("PaladinProtection", "SeraphimBoss", value.ToString()); }
        }

        private static bool HotPBoss
        {
            get
            {
                var hotpBoss = ConfigFile.ReadValue("PaladinProtection", "HotPBoss").Trim();

                return hotpBoss != "" && Convert.ToBoolean(hotpBoss);
            }
            set { ConfigFile.WriteValue("PaladinProtection", "HotPBoss", value.ToString()); }
        }

        private static bool BlessedHammerBoss
        {
            get
            {
                var blessedhammerBoss = ConfigFile.ReadValue("PaladinProtection", "BlessedHammerBoss").Trim();

                return blessedhammerBoss != "" && Convert.ToBoolean(blessedhammerBoss);
            }
            set { ConfigFile.WriteValue("PaladinProtection", "BlessedHammerBoss", value.ToString()); }
        }

        public override void Initialize()
        {
            MessageBox.Show(
                "Welcome to Protection Paladin by Scotishdwarf r16.\n\n- Manual use of Divine Shield and Blessings\n- If using Blessed Hammer, Hand of the Protector or Seraphim talents, please enable them from Rotation settings.\n\nSuggested build 2132121\n\nPlease give feedback to Scotishdwarf at PixelMagic Discord.\n\nPress OK to continue loading.");
            Log.Write("Welcome to Protection by Scotishdwarf", Color.Purple);
            Log.Write("Manual use : Divine Shield, Blessings : Protection, Sacrifice and Freedom.", Color.Purple);
            Log.Write("Automatic cooldown usage at 10-30% of health.", Color.Purple);
            Log.Write("TO DO: Targets attacking me, Blessings, Resurrect, Stuns, Interupts", Color.Purple);
            Log.Write("Suggested build: 2132121", Color.Green);
            Log.Write("Welcome to PixelMagic Protection Paladin");

            SettingsForm = new Form { Text = "Settings", StartPosition = FormStartPosition.CenterScreen, Width = 600, Height = 200, ShowIcon = false };

            var lblSeraphimBossText = new Label { Text = "Talent : Seraphim", Size = new Size(200, 13), Left = 12, Top = 14 };
            SettingsForm.Controls.Add(lblSeraphimBossText);

            SeraphimBox = new CheckBox { Checked = SeraphimBoss, TabIndex = 2, Size = new Size(15, 14), Left = 220, Top = 14 };
            SettingsForm.Controls.Add(SeraphimBox);

            var lblHotPBossText = new Label { Text = "Talent : Hand of the Protector", Size = new Size(200, 13), Left = 12, Top = 29 };
            SettingsForm.Controls.Add(lblHotPBossText);

            HotPBox = new CheckBox { Checked = HotPBoss, TabIndex = 4, Size = new Size(15, 14), Left = 220, Top = 29 };
            SettingsForm.Controls.Add(HotPBox);

            var lblBlessedHammerBossText = new Label { Text = "Talent : Blessed Hammer", Size = new Size(200, 13), Left = 12, Top = 44 };
            SettingsForm.Controls.Add(lblBlessedHammerBossText);

            BlessedHammerBox = new CheckBox { Checked = BlessedHammerBoss, TabIndex = 6, Size = new Size(15, 14), Left = 220, Top = 44 };
            SettingsForm.Controls.Add(BlessedHammerBox);

            var cmdSave = new Button { Text = "Save", Width = 65, Height = 25, Left = 462, Top = 118, Size = new Size(108, 31) };

            SeraphimBox.Checked = SeraphimBoss;
            HotPBox.Checked = HotPBoss;
            BlessedHammerBox.Checked = BlessedHammerBoss;

            cmdSave.Click += CmdSave_Click;
            SeraphimBox.CheckedChanged += Seraphim_Click;
            HotPBox.CheckedChanged += HotP_Click;
            BlessedHammerBox.CheckedChanged += BlessedHammer_Click;

            SettingsForm.Controls.Add(cmdSave);
            lblSeraphimBossText.BringToFront();
            lblHotPBossText.BringToFront();
            lblBlessedHammerBossText.BringToFront();

            Log.Write("Seraphim = " + SeraphimBoss);
            Log.Write("Hand of the Protector = " + HotPBoss);
            Log.Write("Blessed Hammer = " + BlessedHammerBoss);
        }

        private void CmdSave_Click(object sender, EventArgs e)
        {
            SeraphimBoss = SeraphimBox.Checked;
            HotPBoss = HotPBox.Checked;
            BlessedHammerBoss = BlessedHammerBox.Checked;
            MessageBox.Show("Settings saved", "PixelMagic", MessageBoxButtons.OK, MessageBoxIcon.Information);
            SettingsForm.Close();
        }

        private void Seraphim_Click(object sender, EventArgs e)
        {
            SeraphimBoss = SeraphimBox.Checked;
        }

        private void HotP_Click(object sender, EventArgs e)
        {
            HotPBoss = HotPBox.Checked;
        }

        private void BlessedHammer_Click(object sender, EventArgs e)
        {
            BlessedHammerBoss = BlessedHammerBox.Checked;
        }

        public override void Stop()
        {
        }

        public override void Pulse()
        {
            if (WoW.IsInCombat)
            {
                interruptwatch.Start();
            }
            // if (WoW.HasBuff("Mount")) return;
            // COOLDOWNS
            if (WoW.IsInCombat && WoW.TargetIsEnemy && WoW.IsSpellInRange("Rebuke"))
            {
                if (SeraphimBoss && !WoW.IsSpellOnCooldown("Seraphim") && WoW.PlayerSpellCharges("Shield of the Righteous") >= 2 && WoW.HealthPercent >= 80)
                {
                    Log.Write("Seraphim", Color.Red);
                    WoW.CastSpell("Seraphim");
                    return;
                }
                if (WoW.PlayerSpellCharges("Shield of the Righteous") == 3)
                {
                    Log.Write("SoTR 3 Stacks, casting SoTR.", Color.Red);
                    WoW.CastSpell("Shield of the Righteous");
                    return;
                }
                if (WoW.PlayerHasBuff("Shield of the Righteous") && WoW.PlayerBuffTimeRemaining("Shield of the Righteous") <= 1.5)
                {
                    Log.Write("SoTR remaining under 1.5seconds on target, cast SoTR.", Color.Red);
                    WoW.CastSpell("Shield of the Righteous");
                    return;
                }
                if (WoW.PlayerSpellCharges("Shield of the Righteous") >= 1 && WoW.HealthPercent <= 80 &&
                    (!WoW.PlayerHasBuff("Shield of the Righteous") || WoW.PlayerBuffTimeRemaining("Shield of the Righteous") <= 1.5))
                {
                    Log.Write("SoTR 1+ Stack and Health under 80%.", Color.Red);
                    WoW.CastSpell("Shield of the Righteous");
                    return;
                }
                if (WoW.CanCast("Eye of Tyr") && !WoW.IsSpellOnCooldown("Eye of Tyr") && WoW.HealthPercent <= 60)
                {
                    Log.Write("Health under 60%, casting Eye of Tyr.", Color.Red);
                    WoW.CastSpell("Eye of Tyr");
                    return;
                }
                if (WoW.CanCast("Ardent Defender") && !WoW.IsSpellOnCooldown("Ardent Defender") && WoW.HealthPercent <= 10)
                {
                    Log.Write("Health Critical casting Ardent Defender.", Color.Red);
                    WoW.CastSpell("Ardent Defender");
                    return;
                }
                if (WoW.CanCast("Guardian of the Ancient Kings") && !WoW.IsSpellOnCooldown("Guardian of the Ancient Kings") && WoW.HealthPercent <= 30)
                {
                    Log.Write("Health low casting GoAK.", Color.Red);
                    WoW.CastSpell("Guardian of the Ancient Kings");
                    return;
                }
                if (WoW.CanCast("Lay on Hands") && !WoW.IsSpellOnCooldown("Lay on Hands") && WoW.HealthPercent <= 20)
                {
                    Log.Write("Health low casting LoH.", Color.Red);
                    WoW.CastSpell("Lay on Hands");
                    return;
                }
                if (WoW.TargetIsCasting && interruptwatch.ElapsedMilliseconds > 1500)
                {
                    //    if (!WoW.IsSpellOnCooldown("Rebuke"))
                    //    {
                    //        WoW.CastSpellByName("Rebuke");
                    //        interruptwatch.Reset();
                    //        interruptwatch.Start();
                    //        return;
                    //    }
                    if (!WoW.IsSpellOnCooldown("Avenger's Shield") && !WoW.IsSpellOnGCD("Avenger's Shield"))
                    {
                        WoW.CastSpell("Avenger's Shield");
                        interruptwatch.Reset();
                        interruptwatch.Start();
                        return;
                    }
                }
            }
            // SINGLE
            if (combatRoutine.Type == RotationType.SingleTarget)
            {
                if (WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsSpellInRange("Rebuke")) //GCD and Melee range Check
                {
                    // Hand of the Protector under 60% health
                    if (HotPBoss && WoW.CanCast("Hand of the Protector") && !WoW.IsSpellOnCooldown("Hand of the Protector") && WoW.HealthPercent <= 60)
                    {
                        Log.Write("Health under 60%, Hand of Protector.", Color.Red);
                        WoW.CastSpell("Hand of the Protector");
                        return;
                    }
                    // Light of the Protector under 60% health
                    if (!HotPBoss && WoW.CanCast("Light of the Protector") && !WoW.IsSpellOnCooldown("Light of the Protector") && WoW.HealthPercent <= 60)
                    {
                        Log.Write("Health under 60%, Light of Protector.", Color.Red);
                        WoW.CastSpell("Light of the Protector");
                        return;
                    }
                    // Cast Hammer if max stacks
                    if (BlessedHammerBoss && WoW.CanCast("Blessed Hammer") && WoW.PlayerSpellCharges("Blessed Hammer") >= 3 && !WoW.IsSpellOnGCD("Blessed Hammer"))
                    {
                        Log.Write("Blessed Hammer max stacks, casting Blessed Hammer.", Color.Red);
                        WoW.CastSpell("Blessed Hammer");
                        return;
                    }
                    // Cast Hammer
                    if (!BlessedHammerBoss && WoW.CanCast("Hammer of the Righteous") && WoW.PlayerSpellCharges("Hammer of the Righteous") >= 1 && !WoW.IsSpellOnGCD("Hammer of the Righteous"))
                    {
                        Log.Write("Hammer of the Righteous.", Color.Red);
                        WoW.CastSpell("Hammer of the Righteous");
                        return;
                    }
                    // Judgement on Cooldown
                    if (WoW.CanCast("Judgement") && !WoW.IsSpellOnGCD("Judgement"))
                    {
                        WoW.CastSpell("Judgement");
                        return;
                    }
                    // Consecration on cooldown
                    if (WoW.CanCast("Consecration") && !WoW.IsSpellOnGCD("Consecration") && !WoW.TargetHasDebuff("Consecration"))
                    {
                        Log.Write("Consecration debuff not active, casting Consecration.", Color.Red);
                        WoW.CastSpell("Consecration");
                        return;
                    }
                    // Avenger's Shield on cooldown
                    if (WoW.CanCast("Avenger's Shield") && !WoW.IsSpellOnGCD("Avenger's Shield"))
                    {
                        WoW.CastSpell("Avenger's Shield");
                        return;
                    }
                    // Cast Blessed Hammer at 1 + stacks
                    if (BlessedHammerBoss && WoW.CanCast("Blessed Hammer") && WoW.PlayerSpellCharges("Blessed Hammer") >= 1 && !WoW.IsSpellOnGCD("Blessed Hammer") &&
                        !WoW.TargetHasDebuff("Blessed Hammer"))
                    {
                        Log.Write("Nothing to do, casting Blessed Hammer.", Color.Red);
                        WoW.CastSpell("Blessed Hammer");
                        return;
                    }
                }
            }
            // AOE/CLEAVE
            if (combatRoutine.Type == RotationType.AOE) // AoE/Cleave
            {
                if (WoW.HasTarget && WoW.TargetIsEnemy) //
                {
                    // Hand of the Protector under 60% health
                    if (HotPBoss && WoW.CanCast("Hand of the Protector") && !WoW.IsSpellOnCooldown("Hand of the Protector") && WoW.HealthPercent <= 60)
                    {
                        Log.Write("Health under 60%, Hand of Protector.", Color.Red);
                        WoW.CastSpell("Hand of the Protector");
                        return;
                    }
                    // Light of the Protector under 60% health
                    if (!HotPBoss && WoW.CanCast("Light of the Protector") && !WoW.IsSpellOnCooldown("Light of the Protector") && WoW.HealthPercent <= 60)
                    {
                        Log.Write("Health under 60%, Light of Protector.", Color.Red);
                        WoW.CastSpell("Light of the Protector");
                        return;
                    }
                    if (WoW.PlayerSpellCharges("Blessed Hammer") >= 3 && !WoW.IsSpellOnGCD("Blessed Hammer")) // Cast Hammer if max stacks
                    {
                        WoW.CastSpell("Blessed Hammer");
                        return;
                    }
                    if (WoW.CanCast("Avenger's Shield") && !WoW.IsSpellOnGCD("Avenger's Shield")) // Avenger's Shield on cooldown
                    {
                        WoW.CastSpell("Avenger's Shield");
                        return;
                    }
                    if (WoW.CanCast("Consecration") && !WoW.IsSpellOnGCD("Consecration") && !WoW.TargetHasDebuff("Consecration")) // Consecration on cooldown
                    {
                        Log.Write("Consecration debuff not active, casting Consecration.", Color.Red);
                        WoW.CastSpell("Consecration");
                        return;
                    }
                    if (WoW.CanCast("Blessed Hammer") // Cast Hammer at 1 + stacks
                        && WoW.PlayerSpellCharges("Blessed Hammer") >= 1 && !WoW.IsSpellOnGCD("Blessed Hammer"))
                    {
                        Log.Write("AoE, casting Blessed Hammer.", Color.Red);
                        WoW.CastSpell("Blessed Hammer");
                        return;
                    }
                    if (WoW.CanCast("Judgement") && !WoW.IsSpellOnGCD("Judgement")) // Judgement on Cooldown
                    {
                        WoW.CastSpell("Judgement");
                    }
                }
            }
        }
    }
}

/*
[AddonDetails.db]
AddonAuthor=Scotishdwarf
AddonName=PixelMagic
WoWVersion=Legion - 70300
[SpellBook.db]
Spell,20271,Judgement,D1
Spell,204019,Blessed Hammer,D4
Spell,53595,Hammer of the Righteous,D4
Spell,53600,Shield of the Righteous,F8
Spell,31935,Avenger's Shield,D3
Spell,31884,Avenging Wrath,D9
Spell,26573,Consecration,D2
Spell,96231,Rebuke,R
Spell,184092,Light of the Protector,F9
Spell,152262,Seraphim,D8
Spell,213652,Hand of the Protector,F9
Spell,209202,Eye of Tyr,F10
Spell,633,Lay on Hands,F
Spell,86659,Guardian of the Ancient Kings,F11
Spell,31850,Ardent Defender,F12
Aura,132403,Shield of the Righteous
Aura,204301,Blessed Hammer
Aura,204242,Consecration
*/
