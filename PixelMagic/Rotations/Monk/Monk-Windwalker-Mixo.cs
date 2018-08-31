// ReSharper disable UnusedMember.Global

/* 	Mixo's WindWalker Rotation v2.5
	This rotation is based on the writings of Walkingthewind.com, a prime WW theorycrafting site.

	Information and guidelines:
		-	"Stop Casting"-spell refers to the keybind to stop casting. Needed for countering the Tiger Palm issue.
		-	In AOE mode, cycle your targets to apply Mark of the Crane debuff to everything.
		-	Serenity/SEF will have to be activated manually in AOE mode, if you want to use it.

	Feedback is appreciated in the Monk section on PixelMagic's Discord. Tag it with @[EU]Mixo :)

	Talent Tree:
		Tier 1:	Chi Wave
		Tier 2:	W/E
		Tier 3:	Energizing Elixir
		Tier 4: W/E
		Tier 5:	Healing Elixir
		Tier 6: Hit Combo
		Tier 7:	Whirling Dragon Punch / Serenity

    Known issues:
		- Not in-line with perfect manual play, mostly because of lack of intelligence.
		- Freezes in AoE, when it should be able to cast Tiger Palm.

	To Do:
		- Improve intelligence.
        - Maybe a toggle to save SotW & EE before bosses?
		- Change priority on SCK depending on it's damage modifier.
        - Add Flying Serpents Kick usage.
        - Utility cooldowns support, mainly for Mythic+.
        - Take a look at Energizing Elixir usage. Simc version currently, change to Walkingthewind.

	Change Log:
	v2.5
		- Rebuilt from the ground.
		- Openers added.
		- More in-line with Simc rotation.
		- Hit Combo-dropping issues fixed (Thank you Hemradinger).
		- Provided a fix for the Tiger Palm parried issue.
	
*/

using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using PixelMagic.Helpers;

namespace PixelMagic.Rotation
{
    public class WindwalkerMonk : CombatRoutine
    {
        private static int OpenerOrder;
        private static bool WasStrikeOfTheWindlordCasted;
        private static bool IsOpenerDone;

        private static readonly float gcd = 1.5f;

        private static readonly float EnergyMax = WoW.Energy;

        private readonly Stopwatch CombatTime = new Stopwatch();

        private readonly string readme = "Mixo's WindWalker Rotation v2.5" + Environment.NewLine + "" + Environment.NewLine + "Not added yet. Reference the .cs file for a Readme." +
                                         Environment.NewLine + "";

        private CheckBox GaleBurstBox;

        //Settings
        private CheckBox KatsuoBox;

        //Talents
        private CheckBox SerenityBox;

        private static string HitCombo { set; get; } 

        private static bool Katsuo
        {
            get
            {
                var katsuo = ConfigFile.ReadValue("Windwalker", "Katsuo").Trim();
                return katsuo != "" && Convert.ToBoolean(katsuo);
            }
            set { ConfigFile.WriteValue("Windwalker", "Katsuo", value.ToString()); }
        }

        private static bool Serenity
        {
            get
            {
                var serenity = ConfigFile.ReadValue("Windwalker", "Serenity").Trim();
                return serenity != "" && Convert.ToBoolean(serenity);
            }
            set { ConfigFile.WriteValue("Windwalker", "Serenity", value.ToString()); }
        }

        private static bool GaleBurst
        {
            get
            {
                var galeBurst = ConfigFile.ReadValue("Windwalker", "GaleBurst").Trim();
                return galeBurst != "" && Convert.ToBoolean(galeBurst);
            }
            set { ConfigFile.WriteValue("Windwalker", "GaleBurst", value.ToString()); }
        }


        public override Form SettingsForm { get; set; }

        public override string Name 
		{
			get
			{
				return "Windwalker Monk";
			}
		}

        public override string Class 
		{
			get		
			{
				return "Monk";
			}
		}

        public override void Initialize()
        {
            Log.DrawHorizontalLine();
            Log.Write("WELCOME TO MIXO's WINDWALKER ROTATION!", Color.SpringGreen);
            Log.Write("- Version: v2.5 -", Color.SpringGreen);
            Log.Write("", Color.SpringGreen);
            Log.Write("Don't forget to change the Rotation settings based on your preferences.", Color.SpringGreen);
            Log.Write("", Color.SpringGreen);
            Log.Write("Feedback is appreciated in the Monk section on PixelMagic's Discord.", Color.SpringGreen);
            Log.Write("Tag it with @[EU] Mixo to notify me. :)", Color.SpringGreen);
            Log.DrawHorizontalLine();            

            SettingsForm = new Form {Text = "Mixo's Windwalker Monk Rotation - Settings", StartPosition = FormStartPosition.CenterScreen, Width = 480, Height = 318, ShowIcon = false};

            var lblDefensivesText = new Label {Text = "Legendary", Size = new Size(115, 13), Left = 15, Top = 15};
            lblDefensivesText.ForeColor = Color.DarkOrange;
            SettingsForm.Controls.Add(lblDefensivesText);
            var lblCooldownzText = new Label {Text = "Talents / Traits", Size = new Size(115, 13), Left = 260, Top = 15};
            lblCooldownzText.ForeColor = Color.SpringGreen;
            SettingsForm.Controls.Add(lblCooldownzText);

            var lblTextBox = new Label
            {
                Text =
                    "Please select your settings for the current specialization. " + Environment.NewLine +
                    "If a Talent, Trait, or Legendary is checked and it's not used in-game, the rotation will fail. " + Environment.NewLine +
                    "Please report any issues on #Monk PM Discord Channel, use @[EU] Mixo tag. ",
                Size = new Size(270, 220),
                Left = 15,
                Top = 195
            };
            lblTextBox.ForeColor = Color.DarkGreen;
            SettingsForm.Controls.Add(lblTextBox);

            var cmdSave = new Button {Text = "Save", Width = 65, Height = 25, Left = 332, Top = 243, Size = new Size(120, 31)};
            var cmdReadme = new Button {Text = "Read Me", Width = 65, Height = 25, Left = 332, Top = 213, Size = new Size(120, 31)};

            var lblKatsuoText = new Label {Text = "Katsuo's Eclipse", Size = new Size(120, 13), Left = 15, Top = 39};
            SettingsForm.Controls.Add(lblKatsuoText);
            KatsuoBox = new CheckBox {Checked = Katsuo, TabIndex = 6, Size = new Size(15, 14), Left = 145, Top = 39};
            SettingsForm.Controls.Add(KatsuoBox);

            var lblSerenityText = new Label {Text = "Serenity", Size = new Size(120, 13), Left = 260, Top = 39};
            SettingsForm.Controls.Add(lblSerenityText);
            SerenityBox = new CheckBox {Checked = Serenity, TabIndex = 22, Size = new Size(15, 14), Left = 390, Top = 39};
            SettingsForm.Controls.Add(SerenityBox);

            var lblGaleBurstText = new Label {Text = "Gale Burst", Size = new Size(120, 13), Left = 260, Top = 56};
            SettingsForm.Controls.Add(lblGaleBurstText);
            GaleBurstBox = new CheckBox {Checked = GaleBurst, TabIndex = 22, Size = new Size(15, 14), Left = 390, Top = 56};
            SettingsForm.Controls.Add(GaleBurstBox);

            KatsuoBox.Checked = Katsuo;
            SerenityBox.Checked = Serenity;
            GaleBurstBox.Checked = GaleBurst;

            cmdSave.Click += CmdSave_Click;
            cmdReadme.Click += CmdReadme_Click;
            KatsuoBox.CheckedChanged += Katsuo_Click;
            SerenityBox.CheckedChanged += Serenity_Click;
            GaleBurstBox.CheckedChanged += GaleBurst_Click;

            SettingsForm.Controls.Add(cmdSave);
            SettingsForm.Controls.Add(cmdReadme);
            lblKatsuoText.BringToFront();
            lblSerenityText.BringToFront();
            lblGaleBurstText.BringToFront();
        }

        private void CmdReadme_Click(object sender, EventArgs e)
        {
            MessageBox.Show(readme, "PixelMagic", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CmdSave_Click(object sender, EventArgs e)
        {
            Katsuo = KatsuoBox.Checked;
            Serenity = SerenityBox.Checked;
            GaleBurst = GaleBurstBox.Checked;
            MessageBox.Show("Settings saved.", "PixelMagic", MessageBoxButtons.OK, MessageBoxIcon.Information);
            SettingsForm.Close();
        }

        // Checkboxes

        private void Katsuo_Click(object sender, EventArgs e)
        {
            Katsuo = KatsuoBox.Checked;
        }

        private void Serenity_Click(object sender, EventArgs e)
        {
            Serenity = SerenityBox.Checked;
        }

        private void GaleBurst_Click(object sender, EventArgs e)
        {
            GaleBurst = GaleBurstBox.Checked;
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

            if (!WoW.PlayerHasBuff("HitCombo"))
            {
                HitCombo = "";
            }

            if (!WoW.IsInCombat || (!WoW.TargetHasDebuff("TouchOfDeath") && !WoW.PlayerHasBuff("Serenity")) || (IsOpenerDone && !Serenity))
            {
                OpenerOrder = 1;
            }

            var EnergyPct = WoW.Energy/EnergyMax*100f;

            // Single Target Rotation
            if (combatRoutine.Type == RotationType.SingleTarget)
            {
                //Log.Write($"[HitCombo] '{HitCombo}'");				
                //Stop Casting CJL
                if (HitCombo == "CracklingJadeLightning" && WoW.PlayerIsChanneling)
                {
                    WoW.CastSpell("StopCasting");
                    return;
                }

                //Cooldowns
                if (WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsInCombat && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling)
                {
                    //Touch Of Death
                    if (WoW.CanCast("TouchOfDeath") && WoW.IsSpellInRange("TigerPalm") && !GaleBurst && UseCooldowns)
                    {
                        WoW.CastSpell("TouchOfDeath");
                        HitCombo = "TouchOfDeath";
                        return;
                    }

                    //Touch Of Death (Serenity talent) [Gale]
                    if (WoW.CanCast("TouchOfDeath") && WoW.IsSpellInRange("TigerPalm") && WoW.SpellCooldownTimeRemaining("Serenity") < 2 && WoW.SpellCooldownTimeRemaining("RisingSunKick") < 2 &&
                        WoW.SpellCooldownTimeRemaining("FistsOfFury") < 8 && WoW.SpellCooldownTimeRemaining("StrikeOfTheWindlord") <= 4 && Serenity && GaleBurst && WoW.CurrentChi >= 2 &&
                        UseCooldowns)
                    {
                        WoW.CastSpell("TouchOfDeath");
                        HitCombo = "TouchOfDeath";
                        return;
                    }

                    //Touch Of Death (WDP talent) [Gale]
                    if (WoW.CanCast("TouchOfDeath") && WoW.IsSpellInRange("TigerPalm") && WoW.SpellCooldownTimeRemaining("RisingSunKick") < 7 &&
                        WoW.SpellCooldownTimeRemaining("FistsOfFury") <= 4 && WoW.SpellCooldownTimeRemaining("StrikeOfTheWindlord") < 8 && !Serenity && GaleBurst && WoW.CurrentChi >= 2 &&
                        UseCooldowns)
                    {
                        WoW.CastSpell("TouchOfDeath");
                        HitCombo = "TouchOfDeath";
                        return;
                    }

                    //Serenity
                    if (WoW.CanCast("Serenity") && WoW.IsSpellInRange("TigerPalm") && (WoW.IsSpellOnCooldown("TouchOfDeath") || WoW.TargetHasDebuff("TouchOfDeath")) &&
                        WoW.SpellCooldownTimeRemaining("FistsOfFury") < 15 && WoW.SpellCooldownTimeRemaining("StrikeOfTheWindlord") < 13 && !WoW.IsSpellOnCooldown("RisingSunKick") && Serenity &&
                        UseCooldowns)
                    {
                        WoW.CastSpell("Serenity");
                        return;
                    }

                    //SEF (Opener not done)
                    if (WoW.CanCast("SEF") && WoW.IsSpellInRange("TigerPalm") && WoW.CurrentChi >= 2 && !IsOpenerDone && !WoW.PlayerHasBuff("SEF") && !Serenity && UseCooldowns)
                    {
                        WoW.CastSpell("SEF");
                        return;
                    }

                    //SEF
                    if (WoW.CanCast("SEF") && WoW.IsSpellInRange("TigerPalm") && IsOpenerDone && (WoW.IsSpellOnCooldown("TouchOfDeath") || WoW.TargetHasDebuff("TouchOfDeath")) &&
                        WoW.SpellCooldownTimeRemaining("FistsOfFury") <= 6 && WoW.SpellCooldownTimeRemaining("StrikeOfTheWindlord") <= 14 &&
                        WoW.SpellCooldownTimeRemaining("RisingSunKick") <= 6 && !WoW.PlayerHasBuff("SEF") && !Serenity &&
                        (!WoW.IsSpellOnCooldown("EnergizingElixir") || WoW.PlayerSpellCharges("SEF") == 2 || WoW.TargetHasDebuff("TouchOfDeath")) && UseCooldowns)
                    {
                        WoW.CastSpell("SEF");
                        return;
                    }

                    //Energizing Elixir
                    if (WoW.CanCast("EnergizingElixir") && WoW.IsSpellInRange("TigerPalm") && IsOpenerDone && WoW.PlayerHasBuff("SEF"))
                    {
                        WoW.CastSpell("EnergizingElixir");
                        return;
                    }

                    //Energizing Elixir
                    if (WoW.CanCast("EnergizingElixir") && WoW.IsSpellInRange("TigerPalm") && WoW.Energy < EnergyMax && (IsOpenerDone || !Serenity) && WoW.CurrentChi <= 1 &&
                        (!WoW.IsSpellOnCooldown("StrikeOfTheWindlord") || !WoW.IsSpellOnCooldown("RisingSunKick")))
                    {
                        WoW.CastSpell("EnergizingElixir");
                        return;
                    }
                }

                //Serenity Opener (With Touch of Death)
                if (WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsInCombat && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling && WoW.PlayerHasBuff("Serenity") &&
                    WoW.TargetHasDebuff("TouchOfDeath"))
                {
                    //Rising Sun Kick
                    if (WoW.CanCast("RisingSunKick") && WoW.IsSpellInRange("TigerPalm") && OpenerOrder == 1)
                    {
                        WoW.CastSpell("RisingSunKick");
                        HitCombo = "RisingSunKick";
                        OpenerOrder = 2;
                        return;
                    }

                    //Strike Of The Windlord
                    if (WoW.CanCast("StrikeOfTheWindlord") && WoW.IsSpellInRange("TigerPalm") && OpenerOrder == 2)
                    {
                        WoW.CastSpell("StrikeOfTheWindlord");
                        HitCombo = "StrikeOfTheWindlord";
                        OpenerOrder = 3;
                        return;
                    }

                    //Fists of Fury
                    if (WoW.CanCast("FistsOfFury") && WoW.IsSpellInRange("TigerPalm") && OpenerOrder == 3)
                    {
                        WoW.CastSpell("FistsOfFury");
                        HitCombo = "FistsOfFury";
                        OpenerOrder = 4;
                        return;
                    }

                    //Rising Sun Kick
                    if (WoW.CanCast("RisingSunKick") && WoW.IsSpellInRange("TigerPalm") && OpenerOrder == 4)
                    {
                        WoW.CastSpell("RisingSunKick");
                        HitCombo = "RisingSunKick";
                        OpenerOrder = 5;
                        return;
                    }

                    //Spinning Crane Kick
                    if (WoW.CanCast("SpinningCraneKick") && WoW.IsSpellInRange("TigerPalm") && OpenerOrder == 5)
                    {
                        WoW.CastSpell("SpinningCraneKick");
                        HitCombo = "SpinningCraneKick";
                        OpenerOrder = 6;
                        return;
                    }

                    //Blackout Kick
                    if (WoW.CanCast("BlackoutKick") && WoW.IsSpellInRange("TigerPalm") && HitCombo != "BlackoutKick" && OpenerOrder == 6)
                    {
                        WoW.CastSpell("BlackoutKick");
                        HitCombo = "BlackoutKick";
                        return;
                    }
                }

                //WDP Opener
                if (WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsInCombat && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling && WoW.PlayerHasBuff("SEF") && WoW.TargetHasDebuff("TouchOfDeath") &&
                    !IsOpenerDone)
                {
                    //Rising Sun Kick
                    if (WoW.CanCast("RisingSunKick") && WoW.IsSpellInRange("TigerPalm") && OpenerOrder == 1)
                    {
                        WoW.CastSpell("RisingSunKick");
                        HitCombo = "RisingSunKick";
                        OpenerOrder = 2;
                        return;
                    }

                    //Energizing Elixir
                    if (WoW.CanCast("EnergizingElixir") && WoW.IsSpellInRange("TigerPalm") && OpenerOrder == 2)
                    {
                        WoW.CastSpell("EnergizingElixir");
                        OpenerOrder = 3;
                        return;
                    }

                    //Fists of Fury
                    if (WoW.CanCast("FistsOfFury") && WoW.IsSpellInRange("TigerPalm") && OpenerOrder == 3)
                    {
                        WoW.CastSpell("FistsOfFury");
                        HitCombo = "FistsOfFury";
                        OpenerOrder = 4;
                        return;
                    }

                    //Strike Of The Windlord
                    if (WoW.CanCast("StrikeOfTheWindlord") && WoW.IsSpellInRange("TigerPalm") && OpenerOrder == 4)
                    {
                        WoW.CastSpell("StrikeOfTheWindlord");
                        HitCombo = "StrikeOfTheWindlord";
                        OpenerOrder = 5;
                        return;
                    }

                    //Tiger Palm
                    if (WoW.CanCast("TigerPalm") && WoW.IsSpellInRange("TigerPalm") && HitCombo != "TigerPalm" && OpenerOrder == 5)
                    {
                        WoW.CastSpell("TigerPalm");
                        HitCombo = "TigerPalm";
                        OpenerOrder = 6;
                        return;
                    }

                    //Whirling Dragon Punch
                    if (WoW.CanCast("WhirlingDragonPunch") && WoW.IsSpellInRange("TigerPalm") && WoW.IsSpellOnCooldown("FistsOfFury") && WoW.IsSpellOnCooldown("RisingSunKick") &&
                        OpenerOrder == 6)
                    {
                        WoW.CastSpell("WhirlingDragonPunch");
                        HitCombo = "WhirlingDragonPunch";
                        IsOpenerDone = true;
                        return;
                    }
                }

                //Serenity Rotation
                if (WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsInCombat && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling && WoW.PlayerHasBuff("Serenity") &&
                    !WoW.TargetHasDebuff("TouchOfDeath"))
                {
                    //Strike Of The Windlord
                    if (WoW.CanCast("StrikeOfTheWindlord") && WoW.IsSpellInRange("TigerPalm"))
                    {
                        WoW.CastSpell("StrikeOfTheWindlord");
                        HitCombo = "StrikeOfTheWindlord";
                        WasStrikeOfTheWindlordCasted = true;
                        return;
                    }

                    //Rising Sun Kick
                    if (WoW.CanCast("RisingSunKick") && WoW.IsSpellInRange("TigerPalm"))
                    {
                        WoW.CastSpell("RisingSunKick");
                        HitCombo = "RisingSunKick";
                        return;
                    }

                    //Fists of Fury
                    if (WoW.CanCast("FistsOfFury") && WoW.IsSpellInRange("TigerPalm") && WasStrikeOfTheWindlordCasted && WoW.PlayerBuffTimeRemaining("Serenity") <= 2)
                    {
                        WoW.CastSpell("FistsOfFury");
                        HitCombo = "FistsOfFury";
                        WasStrikeOfTheWindlordCasted = false;
                        return;
                    }

                    //Spinning Crane Kick
                    if (WoW.CanCast("SpinningCraneKick") && WoW.IsSpellInRange("TigerPalm") && HitCombo != "SpinningCraneKick" && (HitCombo == "BlackoutKick" || OpenerOrder == 5))
                    {
                        WoW.CastSpell("SpinningCraneKick");
                        HitCombo = "SpinningCraneKick";
                        return;
                    }

                    //Blackout Kick
                    if (WoW.CanCast("BlackoutKick") && WoW.IsSpellInRange("TigerPalm") && HitCombo != "BlackoutKick")
                    {
                        WoW.CastSpell("BlackoutKick");
                        HitCombo = "BlackoutKick";
                        return;
                    }
                }

                // Normal Rotation
                if (WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsInCombat && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling && !WoW.PlayerHasBuff("Serenity") &&
                    (!WoW.TargetHasDebuff("TouchOfDeath") || !Serenity))
                {
                    //Fists of Fury
                    if (WoW.CanCast("FistsOfFury") && WoW.IsSpellInRange("TigerPalm") && (WoW.CurrentChi >= 3 || (Katsuo && WoW.CurrentChi >= 1)) && !WoW.WasLastCasted("EnergizingElixir"))
                    {
                        WoW.CastSpell("FistsOfFury");
                        HitCombo = "FistsOfFury";
                        return;
                    }

                    //Strike Of The Windlord
                    if (WoW.CanCast("StrikeOfTheWindlord") && WoW.IsSpellInRange("TigerPalm") && WoW.CurrentChi >= 2)
                    {
                        WoW.CastSpell("StrikeOfTheWindlord");
                        HitCombo = "StrikeOfTheWindlord";
                        return;
                    }

                    //Tiger Palm (if <4 Chi and about to cap energy)
                    if (WoW.CanCast("TigerPalm") && WoW.IsSpellInRange("TigerPalm") && WoW.Energy >= 50 && EnergyPct > 90 && !WoW.WasLastCasted("EnergizingElixir") && WoW.CurrentChi < 4 &&
                        HitCombo != "TigerPalm")
                    {
                        WoW.CastSpell("TigerPalm");
                        HitCombo = "TigerPalm";
                        return;
                    }

                    //Rising Sun Kick
                    if (WoW.CanCast("RisingSunKick") && WoW.IsSpellInRange("TigerPalm") && WoW.CurrentChi >= 2)
                    {
                        WoW.CastSpell("RisingSunKick");
                        HitCombo = "RisingSunKick";
                        return;
                    }

                    //Whirling Dragon Punch
                    if (WoW.CanCast("WhirlingDragonPunch") && WoW.IsSpellInRange("TigerPalm") && WoW.IsSpellOnCooldown("FistsOfFury") && WoW.IsSpellOnCooldown("RisingSunKick") && !Serenity)
                    {
                        WoW.CastSpell("WhirlingDragonPunch");
                        HitCombo = "WhirlingDragonPunch";
                        return;
                    }

                    //Chi Wave
                    if (WoW.CanCast("ChiWave") && WoW.IsSpellInRange("TigerPalm") && WoW.Energy < 50 && WoW.CurrentChi < 5 && !WoW.PlayerHasBuff("SEF"))
                    {
                        WoW.CastSpell("ChiWave");
                        HitCombo = "ChiWave";
                        return;
                    }

                    //Blackout Kick
                    if (WoW.CanCast("BlackoutKick") && WoW.IsSpellInRange("TigerPalm") && (WoW.CurrentChi > 1 || WoW.PlayerHasBuff("BlackoutKick!")) && HitCombo != "BlackoutKick")
                    {
                        WoW.CastSpell("BlackoutKick");
                        HitCombo = "BlackoutKick";
                        return;
                    }

                    //Tiger Palm
                    if (WoW.CanCast("TigerPalm") && WoW.IsSpellInRange("TigerPalm") && WoW.Energy >= 50 && HitCombo != "TigerPalm")
                    {
                        WoW.CastSpell("TigerPalm");
                        HitCombo = "TigerPalm";
                        return;
                    }

                    //CracklingJadeLightning (only to counter the Tiger Palm issue)
                    if (WoW.CanCast("CracklingJadeLightning") && WoW.IsSpellInRange("TigerPalm") && WoW.Energy >= 20 && HitCombo == "TigerPalm" && WoW.CurrentChi == 0)
                    {
                        WoW.CastSpell("CracklingJadeLightning");
                        HitCombo = "CracklingJadeLightning";
                        return;
                    }
                }
            }

            // AoE Rotation
            if (combatRoutine.Type == RotationType.AOE)
            {
                if (HitCombo == "CracklingJadeLightning" && WoW.PlayerIsChanneling)
                {
                    WoW.CastSpell("StopCasting");
                    return;
                }

                //Cooldowns
                if (WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsInCombat && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling)
                {
                    //Energizing Elixir
                    if (WoW.CanCast("EnergizingElixir") && WoW.IsSpellInRange("TigerPalm") && WoW.Energy < EnergyMax && WoW.CurrentChi <= 1 &&
                        (!WoW.IsSpellOnCooldown("StrikeOfTheWindlord") || !WoW.IsSpellOnCooldown("RisingSunKick")))
                    {
                        WoW.CastSpell("EnergizingElixir");
                        return;
                    }
                }

                //Serenity Rotation
                if (WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsInCombat && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling && WoW.PlayerHasBuff("Serenity"))
                {
                    //Strike Of The Windlord
                    if (WoW.CanCast("StrikeOfTheWindlord") && WoW.IsSpellInRange("TigerPalm"))
                    {
                        WoW.CastSpell("StrikeOfTheWindlord");
                        HitCombo = "StrikeOfTheWindlord";
                        WasStrikeOfTheWindlordCasted = true;
                        return;
                    }

                    //Rising Sun Kick
                    if (WoW.CanCast("RisingSunKick") && WoW.IsSpellInRange("TigerPalm"))
                    {
                        WoW.CastSpell("RisingSunKick");
                        HitCombo = "RisingSunKick";
                        return;
                    }

                    //Fists of Fury
                    if (WoW.CanCast("FistsOfFury") && WoW.IsSpellInRange("TigerPalm") && WasStrikeOfTheWindlordCasted && WoW.PlayerBuffTimeRemaining("Serenity") <= 2)
                    {
                        WoW.CastSpell("FistsOfFury");
                        HitCombo = "FistsOfFury";
                        WasStrikeOfTheWindlordCasted = false;
                        return;
                    }

                    //Spinning Crane Kick
                    if (WoW.CanCast("SpinningCraneKick") && WoW.IsSpellInRange("TigerPalm") && HitCombo != "SpinningCraneKick" && (HitCombo == "BlackoutKick" || OpenerOrder == 5))
                    {
                        WoW.CastSpell("SpinningCraneKick");
                        HitCombo = "SpinningCraneKick";
                        return;
                    }

                    //Blackout Kick
                    if (WoW.CanCast("BlackoutKick") && WoW.IsSpellInRange("TigerPalm") && HitCombo != "BlackoutKick")
                    {
                        WoW.CastSpell("BlackoutKick");
                        HitCombo = "BlackoutKick";
                        return;
                    }
                }

                // Normal Rotation
                if (WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsInCombat && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling && !WoW.PlayerHasBuff("Serenity") &&
                    (!WoW.TargetHasDebuff("TouchOfDeath") || !Serenity))
                {
                    //Fists of Fury
                    if (WoW.CanCast("FistsOfFury") && WoW.IsSpellInRange("TigerPalm") && (WoW.CurrentChi >= 3 || (Katsuo && WoW.CurrentChi >= 1)) && !WoW.WasLastCasted("EnergizingElixir"))
                    {
                        WoW.CastSpell("FistsOfFury");
                        HitCombo = "FistsOfFury";
                        return;
                    }

                    //Whirling Dragon Punch
                    if (WoW.CanCast("WhirlingDragonPunch") && WoW.IsSpellInRange("TigerPalm") && WoW.IsSpellOnCooldown("FistsOfFury") && WoW.IsSpellOnCooldown("RisingSunKick") && !Serenity)
                    {
                        WoW.CastSpell("WhirlingDragonPunch");
                        HitCombo = "WhirlingDragonPunch";
                        return;
                    }

                    //Strike Of The Windlord
                    if (WoW.CanCast("StrikeOfTheWindlord") && WoW.IsSpellInRange("TigerPalm") && WoW.CurrentChi >= 2)
                    {
                        WoW.CastSpell("StrikeOfTheWindlord");
                        HitCombo = "StrikeOfTheWindlord";
                        return;
                    }

                    //Rising Sun Kick
                    if (WoW.CanCast("RisingSunKick") && WoW.IsSpellInRange("TigerPalm") && WoW.CurrentChi >= 2 && !Serenity && WoW.SpellCooldownTimeRemaining("WhirlingDragonPunch") < gcd*2 &&
                        WoW.IsSpellOnCooldown("FistsOfFury"))
                    {
                        WoW.CastSpell("RisingSunKick");
                        HitCombo = "RisingSunKick";
                        return;
                    }

                    //Spinning Crane Kick
                    if (WoW.CanCast("SpinningCraneKick") && WoW.IsSpellInRange("TigerPalm") && HitCombo != "SpinningCraneKick" && WoW.CurrentChi >= 3)
                    {
                        WoW.CastSpell("SpinningCraneKick");
                        HitCombo = "SpinningCraneKick";
                        return;
                    }

                    //Chi Wave
                    if (WoW.CanCast("ChiWave") && WoW.IsSpellInRange("TigerPalm") && WoW.Energy < 50 && WoW.CurrentChi < 5 && !WoW.PlayerHasBuff("SEF") && HitCombo == "TigerPalm")
                    {
                        WoW.CastSpell("ChiWave");
                        HitCombo = "ChiWave";
                        return;
                    }

                    //Blackout Kick
                    if (WoW.CanCast("BlackoutKick") && WoW.IsSpellInRange("TigerPalm") && (WoW.CurrentChi > 1 || WoW.PlayerHasBuff("BlackoutKick!")) && HitCombo != "BlackoutKick" &&
                        (HitCombo == "TigerPalm" || !WoW.TargetHasDebuff("MotC")))
                    {
                        WoW.CastSpell("BlackoutKick");
                        HitCombo = "BlackoutKick";
                        return;
                    }

                    //Tiger Palm
                    if (WoW.CanCast("TigerPalm") && WoW.IsSpellInRange("TigerPalm") && WoW.Energy >= 50 && HitCombo != "TigerPalm")
                    {
                        WoW.CastSpell("TigerPalm");
                        HitCombo = "TigerPalm";
                        return;
                    }

                    //CracklingJadeLightning (only to counter the Tiger Palm issue)
                    if (WoW.CanCast("CracklingJadeLightning") && WoW.IsSpellInRange("TigerPalm") && WoW.Energy >= 20 && HitCombo == "TigerPalm" && WoW.CurrentChi == 0)
                    {
                        WoW.CastSpell("CracklingJadeLightning");
                        HitCombo = "CracklingJadeLightning";
                    }
                }
            }
        }
    }
}

/*
[AddonDetails.db]
AddonAuthor=Mixo
AddonName=RGB
WoWVersion=Legion - 70300
[SpellBook.db]
Spell,115080,TouchOfDeath,G
Spell,113656,FistsOfFury,F3
Spell,205320,StrikeOfTheWindlord,F6
Spell,100780,TigerPalm,F1
Spell,107428,RisingSunKick,F4
Spell,115098,ChiWave,F5
Spell,100784,BlackoutKick,F2
Spell,101546,SpinningCraneKick,V
Spell,152173,Serenity,T
Spell,115288,EnergizingElixir,C
Spell,152175,WhirlingDragonPunch,F7
Spell,137639,SEF,T
Spell,221771,SEF:Fix,T
Spell,117952,CracklingJadeLightning,F11
Spell,0,StopCasting,S
Aura,152173,Serenity
Aura,116768,BlackoutKick!
Aura,137639,SEF
Aura,228287,MotC
Aura,220358,SCK
Aura,115080,TouchOfDeath
Aura,196741,HitCombo
*/
