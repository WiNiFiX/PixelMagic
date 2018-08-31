// ReSharper disable UnusedMember.Global

/* 	Mixo's Destruction Rotation v1.1

	This rotation is based on Simulationcraft, IcyVeins, and... myself! Been playing warlock since Vanilla :D


	Recommended Talents:
		-These are talents that are proven to be efficient,
			but feel free to try any supported talents.

				Single Target		|	Cleave (2+ Targets)   |	  AoE	(5+ Targets)	|
				--------------------|-------------------------|-------------------------
		Tier 1:	Backdraft 			| 	Backdraft			  |	  Backdraft			    |
		Tier 2:	Reverse Entropy		|	Reverse Entropy		  |	  Reverse Entropy		|
		Tier 3:	Shadowfury*			|	Shadowfury*			  |   Shadowfury*		 	|
		Tier 4:	Eradication			|	Fire & Brimstone	  |	  Fire & Brimstone	  	|
		Tier 5:	Burning Rush*		|	Burning Rush*		  |	  Burning Rush*		    |
		Tier 6:	Grimoire Of Service	|	Grimoire Of Sacrifice |	  Grimoire Of Sacrifice |
		Tier 7:	Soul Conduit		|	Wreck Havoc			  |	  Wreck Havoc			|
				-------------------- ------------------------- -------------------------
					*(My suggestions. It won't affect rotation
						if you pick different talents in these
							tiers.)


	Macros I use with this rotation:
	(feel free try out your own)

	- Havoc (Mouseover)
	#showtooltip Havoc
	/cast [@mouseover, exists, nodead][] Havoc

	- Infernal (On Cursor)
	#showtooltip
	/cast [@cursor] Summon Infernal

	- Rain Of Fire (On Player)
	#showtooltip
	/cast [@player] Rain of Fire


	To Do (in order of priority):
		- Automatic interrupts for PvP.
		- Support for all Talents.
		- Support for all Honor Talents.
		- Casting Havoc automatically switches routine to cleave.
		- Some kind of opener rotation.
		- Might remove Dimensional Rift recharge-tracking.
			The benefit of having this is small.
		- Perfecting Roaring Blaze.
			Roaring Blaze seems only better than Backdraft while below 31 artifact traits and is a pain to optimize so this might not happen.
		- Force Havoc, w/ macro. Not really needed tho.
		- Probably completly rework this rotation when 7.1.5 arrives. If Destruction Warlocks aren't completly removed that is... :/


	Change Log:

	v1.1 - The Happy New Year Release
		- Grimoire Of Supremacy support.
		- Added toggle for Automatic Imp Cauterize Master.
		- Added toggle for only using cooldowns on bosses.
		- Tweeked Immolate refreshing.
		- Use all Dimensional Rift charges available during Bloodlust.

	v1.0 - The X-Mas Release
		- Added utility cooldowns, like Healthstones and Cauterize Master.
		- Added Dimensional Rift casting at 3 stacks in Cleave-rotation.

	v0.5 (beta)
		- Saving 2+ Dimensional Rifts for bosses.
		- Cleaned up Spell Book.
		- Added Macro suggestions in the Readme.

	v0.4 (beta)
		- Implemented mechanisms to prevent more Immolate casts then necessary when using Roaring Blaze.
		- Added support for Lord Of Flames trait.

	v0.3 (beta)
		- Added AoE-rotation (Essentially just maximizing Rain Of Fire).
		- Updated text and settings window.

	v0.2 (alpha)
		- Added support for Roaring Blaze talent.
		- Added support for Mana Tap talent.
		- Added support for Grimoire Of Service talent.
		- Added support for Dimension Ripper artifact trait.
		- Added support for Norgannon's Foresight legendary.
		- Added support for Odr, Shawl of the Ymirjar legendary.
		- Added support for movement and optimized rotation to maintain a high dps while moving.
		- Added Cleave-rotation.

	v0.1 (alpha)
		- Rotation creation started.
		- Rotation built around Backdraft-build.

*/

using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using PixelMagic.Helpers;

namespace PixelMagic.Rotation
{
    public class DestructionWarlockMixo : CombatRoutine
    {
        private static bool blazed;
        private static bool SecondImmolateCasted;

        private static readonly float ConflagCD = Convert.ToSingle(12f/(1 + WoW.HastePercent/100f)*1000f);

        private static readonly float ImmolateCastMS = Convert.ToSingle(1.5f/(1 + WoW.HastePercent/100f)*1000f);

        //Boss bool (easier for testing)

        private static bool boss;

        private static bool IsBLUp;
        private CheckBox BossToggleBox;

        private readonly Stopwatch CombatTime = new Stopwatch();
        private readonly Stopwatch ConflagTime = new Stopwatch();
        private CheckBox DimensionRipperBox;
        private readonly Stopwatch DimRiftTime = new Stopwatch();
        private CheckBox GrimoireOfServiceBox;
        private CheckBox GrimoireOfSupremacyBox;
        private CheckBox LordOfFlamesBox;
        private CheckBox ManaTapBox;

        private CheckBox OdrBox;

        private readonly string readme = "Mixo's Destruction Rotation v1.1" + Environment.NewLine + "" + Environment.NewLine +
                                         "This rotation is based on Simulationcraft, IcyVeins, and... myself! I've been playing warlock since Vanilla :D" + Environment.NewLine + "" +
                                         Environment.NewLine + "Information:" + Environment.NewLine +
                                         "- This rotation uses all three routines, Single-Target (1-target), Cleave (2+ targets), and AoE (5+ targets). Get familiar with switching between them." +
                                         Environment.NewLine +
                                         "- You have to apply Havoc manually. This is easiest done with a [@mouseover] macro. Other macros worth considering is [@cursor] Infernal and [@player] Rain of Fire." +
                                         Environment.NewLine +
                                         "- Healthstones (if you have any) and Cauterize Master (if you have a pet and you have enabled Automatic Imp Cauterize in the settings) will be used automatically. Unending Resolve will not." +
                                         Environment.NewLine +
                                         "- If you enable it in the settings, offensive cooldowns will only be automatically used at bosses. Otherwise they will be used on any target. BE AWARE: Bosses in Karazhan and ToV are not supported atm!" +
                                         Environment.NewLine + "" + Environment.NewLine + "Recommended Talents:" + Environment.NewLine +
                                         "-These are talents that are proven to be efficient, but feel free to try any supported talents." + Environment.NewLine + "" + Environment.NewLine +
                                         "	Single Target	Cleave (2+ Targets)		AoE (5+ Targets)	" + Environment.NewLine + "" + Environment.NewLine +
                                         "Tier 1:	Backdraft 		| Backdraft		| Backdraft" + Environment.NewLine + "Tier 2:	Reverse Entropy	| Reverse Entropy		| Reverse Entropy" +
                                         Environment.NewLine + "Tier 3:	Shadowfury*	| Shadowfury*		| Shadowfury*" + Environment.NewLine +
                                         "Tier 4:	Eradication	| Fire & Brimstone		| Fire & Brimstone" + Environment.NewLine + "Tier 5:	Burning Rush*	| Burning Rush*		| Burning Rush*" +
                                         Environment.NewLine + "Tier 6:	Grimoire Of Service	| Grimoire Of Sacrifice	| Grimoire Of Sacrifice" + Environment.NewLine +
                                         "Tier 7:	Soul Conduit	| Wreck Havoc		| Wreck Havoc" + Environment.NewLine + "" + Environment.NewLine +
                                         "	*(My suggestions. It won't affect rotation if you pick different talents in these tiers)" + Environment.NewLine + "" + Environment.NewLine + "" +
                                         Environment.NewLine + "Not Supported talents:" + Environment.NewLine + "- Shadowburn" + Environment.NewLine + "- Cataclysm" + Environment.NewLine +
                                         "- Soul Harvest" + Environment.NewLine + "- Channel Demonfire" + Environment.NewLine + "" + Environment.NewLine + "" + Environment.NewLine +
                                         "To Do (in order of priority):" + Environment.NewLine + "- Automatic interrupts for PvP." + Environment.NewLine + "- Support for all Talents." +
                                         Environment.NewLine + "- Support for all Honor Talents." + Environment.NewLine + "- Casting Havoc automatically switches routine to cleave." +
                                         Environment.NewLine + "- Some kind of opener rotation." + Environment.NewLine + "- Might remove Dimensional Rift recharge-tracking." +
                                         Environment.NewLine + "	The benefit of having this is small." + Environment.NewLine + "- Perfect Roaring Blaze." + Environment.NewLine +
                                         "	Roaring Blaze seems only better than Backdraft while below 31 artifact traits and is a pain to optimize so this might not happen." +
                                         Environment.NewLine + "- Force Havoc, w/ macro. Not really needed tho." + Environment.NewLine +
                                         "- Probably completly rework this rotation when 7.1.5 arrives. If Destruction Warlocks aren't completly removed that is... :/" + Environment.NewLine +
                                         "" + Environment.NewLine + "" + Environment.NewLine + "Change Log:" + Environment.NewLine + "" + Environment.NewLine +
                                         "v1.1 - The Happy New Year Release" + Environment.NewLine + "- Grimoire Of Supremacy support." + Environment.NewLine +
                                         "- Added toggle for Automatic Imp Cauterize Master." + Environment.NewLine + "- Added toggle for only using cooldowns on bosses." + Environment.NewLine +
                                         "- Tweeked Immolate refreshing." + Environment.NewLine + "- Use all Dimensional Rift charges available during Bloodlust." + Environment.NewLine + "";

        private CheckBox RoaringBlazeBox;
        private CheckBox SelfhealingBox;

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

        private static float ChaosBoltCast
        {
            get
            {
                if (WoW.PlayerHasBuff("Backdraft"))
                {
                    return Convert.ToSingle(2.5f/(1 + WoW.HastePercent/100f)*0.7f);
                }
                return Convert.ToSingle(2.5f/(1 + WoW.HastePercent/100f));
            }
        }

        private static bool Odr
        {
            get
            {
                var odr = ConfigFile.ReadValue("Destruction", "Odr").Trim();
                return odr != "" && Convert.ToBoolean(odr);
            }
            set { ConfigFile.WriteValue("Destruction", "Odr", value.ToString()); }
        }

        private static bool Selfhealing
        {
            get
            {
                var selfhealing = ConfigFile.ReadValue("Destruction", "Selfhealing").Trim();
                return selfhealing != "" && Convert.ToBoolean(selfhealing);
            }
            set { ConfigFile.WriteValue("Destruction", "Selfhealing", value.ToString()); }
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

        private static bool RoaringBlaze
        {
            get
            {
                var roaringBlaze = ConfigFile.ReadValue("Destruction", "RoaringBlaze").Trim();
                return roaringBlaze != "" && Convert.ToBoolean(roaringBlaze);
            }
            set { ConfigFile.WriteValue("Destruction", "RoaringBlaze", value.ToString()); }
        }

        private static bool ManaTap
        {
            get
            {
                var manaTap = ConfigFile.ReadValue("Destruction", "ManaTap").Trim();
                return manaTap != "" && Convert.ToBoolean(manaTap);
            }
            set { ConfigFile.WriteValue("Destruction", "ManaTap", value.ToString()); }
        }

        private static bool GrimoireOfSupremacy
        {
            get
            {
                var grimoireOfSupremacy = ConfigFile.ReadValue("Destruction", "GrimoireOfSupremacy").Trim();
                return grimoireOfSupremacy != "" && Convert.ToBoolean(grimoireOfSupremacy);
            }
            set { ConfigFile.WriteValue("Destruction", "GrimoireOfSupremacy", value.ToString()); }
        }

        private static bool GrimoireOfService
        {
            get
            {
                var grimoireOfService = ConfigFile.ReadValue("Destruction", "GrimoireOfService").Trim();
                return grimoireOfService != "" && Convert.ToBoolean(grimoireOfService);
            }
            set { ConfigFile.WriteValue("Destruction", "GrimoireOfService", value.ToString()); }
        }

        private static bool LordOfFlames
        {
            get
            {
                var lordOfFlames = ConfigFile.ReadValue("Destruction", "LordOfFlames").Trim();
                return lordOfFlames != "" && Convert.ToBoolean(lordOfFlames);
            }
            set { ConfigFile.WriteValue("Destruction", "LordOfFlames", value.ToString()); }
        }

        private static bool DimensionRipper
        {
            get
            {
                var dimensionRipper = ConfigFile.ReadValue("Destruction", "DimensionRipper").Trim();
                return dimensionRipper != "" && Convert.ToBoolean(dimensionRipper);
            }
            set { ConfigFile.WriteValue("Destruction", "DimensionRipper", value.ToString()); }
        }

        public override Form SettingsForm { get; set; }

        public override string Name => "Destruction Warlock";

        public override string Class => "Warlock";

        public override void Initialize()
        {
            Log.Write("-----", Color.DarkViolet);
            Log.Write("WELCOME TO MIXO's DESTRO ROTATION!", Color.DarkViolet);
            Log.Write("- Version: 1.1 -", Color.DarkViolet);
            Log.Write("", Color.DarkViolet);
            Log.Write("Don't forget to change the Rotation settings based on your preferences.", Color.DarkViolet);
            Log.Write("", Color.DarkViolet);
            Log.Write("And please take a look at the Read Me to further understand this rotation ", Color.DarkViolet);
            Log.Write(" and the choices behind it.", Color.DarkViolet);
            Log.Write("", Color.DarkViolet);
            Log.Write("Feedback is appreciated in the Warlock section on PixelMagic's Discord.", Color.DarkViolet);
            Log.Write("Tag it with @[EU] Mixo to notify me. :)", Color.DarkViolet);
            Log.Write("-----", Color.DarkViolet);
            Log.Write("Mix Oh! Destruction?!");

            SettingsForm = new Form {Text = "Mixo's Destro Warlock Rotation - Settings", StartPosition = FormStartPosition.CenterScreen, Width = 480, Height = 318, ShowIcon = false};

            var lblLegendaryText = new Label {Text = "Legendary", Size = new Size(115, 13), Left = 15, Top = 15};
            lblLegendaryText.ForeColor = Color.DarkOrange;
            SettingsForm.Controls.Add(lblLegendaryText);
            var lblSettingsText = new Label {Text = "Settings", Size = new Size(115, 13), Left = 15, Top = 87};
            lblSettingsText.ForeColor = Color.DarkGreen;
            SettingsForm.Controls.Add(lblSettingsText);
            var lblCooldownzText = new Label {Text = "Talents/Traits", Size = new Size(115, 13), Left = 260, Top = 15};
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
            lblTextBox.ForeColor = Color.Green;
            SettingsForm.Controls.Add(lblTextBox);

            var cmdSave = new Button {Text = "Save", Width = 65, Height = 25, Left = 332, Top = 243, Size = new Size(120, 31)};
            var cmdReadme = new Button {Text = "Read Me", Width = 65, Height = 25, Left = 332, Top = 213, Size = new Size(120, 31)};

            var lblOdrText = new Label {Text = "Odr, Shawl of Ymirjar", Size = new Size(120, 13), Left = 15, Top = 39};
            SettingsForm.Controls.Add(lblOdrText);
            OdrBox = new CheckBox {Checked = Odr, TabIndex = 6, Size = new Size(15, 14), Left = 145, Top = 39};
            SettingsForm.Controls.Add(OdrBox);

            var lblSelfhealingText = new Label {Text = "Auto Imp Cauterize", Size = new Size(120, 13), Left = 15, Top = 111};
            SettingsForm.Controls.Add(lblSelfhealingText);
            SelfhealingBox = new CheckBox {Checked = Selfhealing, TabIndex = 6, Size = new Size(15, 14), Left = 145, Top = 111};
            SettingsForm.Controls.Add(SelfhealingBox);

            var lblBossToggleText = new Label {Text = "Only Auto CDs on Boss", Size = new Size(120, 13), Left = 15, Top = 128};
            SettingsForm.Controls.Add(lblBossToggleText);
            BossToggleBox = new CheckBox {Checked = BossToggle, TabIndex = 6, Size = new Size(15, 14), Left = 145, Top = 128};
            SettingsForm.Controls.Add(BossToggleBox);

            var lblRoaringBlazeText = new Label {Text = "Roaring Blaze", Size = new Size(120, 13), Left = 260, Top = 39};
            SettingsForm.Controls.Add(lblRoaringBlazeText);
            RoaringBlazeBox = new CheckBox {Checked = RoaringBlaze, TabIndex = 22, Size = new Size(15, 14), Left = 390, Top = 39};
            SettingsForm.Controls.Add(RoaringBlazeBox);

            var lblManaTapText = new Label {Text = "Mana Tap", Size = new Size(120, 13), Left = 260, Top = 56};
            SettingsForm.Controls.Add(lblManaTapText);
            ManaTapBox = new CheckBox {Checked = ManaTap, TabIndex = 22, Size = new Size(15, 14), Left = 390, Top = 56};
            SettingsForm.Controls.Add(ManaTapBox);

            var lblGrimoireOfSupremacyText = new Label {Text = "Grimoire of Supremacy", Size = new Size(120, 13), Left = 260, Top = 73};
            SettingsForm.Controls.Add(lblGrimoireOfSupremacyText);
            GrimoireOfSupremacyBox = new CheckBox {Checked = GrimoireOfSupremacy, TabIndex = 22, Size = new Size(15, 14), Left = 390, Top = 73};
            SettingsForm.Controls.Add(GrimoireOfSupremacyBox);

            var lblGrimoireOfServiceText = new Label {Text = "Grimoire of Service", Size = new Size(120, 13), Left = 260, Top = 90};
            SettingsForm.Controls.Add(lblGrimoireOfServiceText);
            GrimoireOfServiceBox = new CheckBox {Checked = GrimoireOfService, TabIndex = 22, Size = new Size(15, 14), Left = 390, Top = 90};
            SettingsForm.Controls.Add(GrimoireOfServiceBox);

            var lblLordOfFlamesText = new Label {Text = "Lord Of Flames", Size = new Size(120, 13), Left = 260, Top = 107};
            SettingsForm.Controls.Add(lblLordOfFlamesText);
            LordOfFlamesBox = new CheckBox {Checked = LordOfFlames, TabIndex = 22, Size = new Size(15, 14), Left = 390, Top = 107};
            SettingsForm.Controls.Add(LordOfFlamesBox);

            var lblDimensionRipperText = new Label {Text = "Dimension Ripper", Size = new Size(120, 13), Left = 260, Top = 124};
            SettingsForm.Controls.Add(lblDimensionRipperText);
            DimensionRipperBox = new CheckBox {Checked = DimensionRipper, TabIndex = 22, Size = new Size(15, 14), Left = 390, Top = 124};
            SettingsForm.Controls.Add(DimensionRipperBox);

            OdrBox.Checked = Odr;
            SelfhealingBox.Checked = Selfhealing;
            BossToggleBox.Checked = BossToggle;
            RoaringBlazeBox.Checked = RoaringBlaze;
            ManaTapBox.Checked = ManaTap;
            GrimoireOfSupremacyBox.Checked = GrimoireOfSupremacy;
            GrimoireOfServiceBox.Checked = GrimoireOfService;
            LordOfFlamesBox.Checked = LordOfFlames;
            DimensionRipperBox.Checked = DimensionRipper;


            cmdSave.Click += CmdSave_Click;
            cmdReadme.Click += CmdReadme_Click;
            OdrBox.CheckedChanged += Odr_Click;
            SelfhealingBox.CheckedChanged += Selfhealing_Click;
            BossToggleBox.CheckedChanged += BossToggle_Click;
            RoaringBlazeBox.CheckedChanged += RoaringBlaze_Click;
            ManaTapBox.CheckedChanged += ManaTap_Click;
            GrimoireOfSupremacyBox.CheckedChanged += GrimoireOfSupremacy_Click;
            GrimoireOfServiceBox.CheckedChanged += GrimoireOfService_Click;
            LordOfFlamesBox.CheckedChanged += LordOfFlames_Click;
            DimensionRipperBox.CheckedChanged += DimensionRipper_Click;


            SettingsForm.Controls.Add(cmdSave);
            SettingsForm.Controls.Add(cmdReadme);
            lblOdrText.BringToFront();
            lblBossToggleText.BringToFront();
            lblSelfhealingText.BringToFront();
            lblRoaringBlazeText.BringToFront();
            lblManaTapText.BringToFront();
            lblGrimoireOfSupremacyText.BringToFront();
            lblGrimoireOfServiceText.BringToFront();
            lblLordOfFlamesText.BringToFront();
            lblDimensionRipperText.BringToFront();
        }

        private void CmdReadme_Click(object sender, EventArgs e)
        {
            MessageBox.Show(readme, "PixelMagic", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CmdSave_Click(object sender, EventArgs e)
        {
            Odr = OdrBox.Checked;
            Selfhealing = SelfhealingBox.Checked;
            BossToggle = BossToggleBox.Checked;
            RoaringBlaze = RoaringBlazeBox.Checked;
            ManaTap = ManaTapBox.Checked;
            GrimoireOfSupremacy = GrimoireOfSupremacyBox.Checked;
            GrimoireOfService = GrimoireOfServiceBox.Checked;
            LordOfFlames = LordOfFlamesBox.Checked;
            DimensionRipper = DimensionRipperBox.Checked;
            MessageBox.Show("Settings saved.", "PixelMagic", MessageBoxButtons.OK, MessageBoxIcon.Information);
            SettingsForm.Close();
        }

        // Checkboxes

        private void Odr_Click(object sender, EventArgs e)
        {
            Odr = OdrBox.Checked;
        }

        private void Selfhealing_Click(object sender, EventArgs e)
        {
            Selfhealing = SelfhealingBox.Checked;
        }

        private void BossToggle_Click(object sender, EventArgs e)
        {
            BossToggle = BossToggleBox.Checked;
        }

        private void RoaringBlaze_Click(object sender, EventArgs e)
        {
            RoaringBlaze = RoaringBlazeBox.Checked;
        }

        private void ManaTap_Click(object sender, EventArgs e)
        {
            ManaTap = ManaTapBox.Checked;
        }

        private void GrimoireOfSupremacy_Click(object sender, EventArgs e)
        {
            GrimoireOfSupremacy = GrimoireOfSupremacyBox.Checked;
        }

        private void GrimoireOfService_Click(object sender, EventArgs e)
        {
            GrimoireOfService = GrimoireOfServiceBox.Checked;
        }

        private void LordOfFlames_Click(object sender, EventArgs e)
        {
            LordOfFlames = LordOfFlamesBox.Checked;
        }

        private void DimensionRipper_Click(object sender, EventArgs e)
        {
            DimensionRipper = DimensionRipperBox.Checked;
        }

        public override void Stop()
        {
        }

        private static bool IsTargetBoss()
        {
            if (BossToggle)
            {
                if (!WoW.HasTarget && WoW.IsInCombat)
                {
                    boss = false;
                    return boss;
                }
                if (WoW.HasTarget && WoW.IsInCombat)
                {
                    boss = true;
                    return boss;
                }
                return boss;
            }
            boss = true;
            return boss;
        }

        private static bool isBLUp()
        {
            if (WoW.PlayerHasBuff("Bloodlust") || WoW.PlayerHasBuff("TimeWarp") || WoW.PlayerHasBuff("Heroism"))
            {
                IsBLUp = true;
                return IsBLUp;
            }
            IsBLUp = false;
            return IsBLUp;
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

            //Dimensional Rift Recharge-Time (EXPERIMENTAL)
            if (DimRiftTime.IsRunning && WoW.PlayerSpellCharges("DimensionalRift") == 3)
            {
                DimRiftTime.Reset();
            }
            if (DimRiftTime.ElapsedMilliseconds >= 45000)
            {
                DimRiftTime.Restart();
            }
            if (!DimRiftTime.IsRunning && WoW.WasLastCasted("DimensionalRift") && WoW.PlayerSpellCharges("DimensionalRift") == 2)
            {
                DimRiftTime.Start();
            }

            //Conflagrate Recharge-Time (EXPERIMENTAL)
            if (ConflagTime.IsRunning && WoW.PlayerSpellCharges("Conflagrate") == 2)
            {
                ConflagTime.Reset();
            }
            if (ConflagTime.ElapsedMilliseconds >= ConflagCD)
            {
                ConflagTime.Restart();
            }
            if (!ConflagTime.IsRunning && WoW.WasLastCasted("Conflagrate") && WoW.PlayerSpellCharges("Conflagrate") == 1)
            {
                ConflagTime.Start();
            }

            IsTargetBoss();
            isBLUp();

            // Single Target Rotation
            if (combatRoutine.Type == RotationType.SingleTarget)
            {
                if (WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsInCombat && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling && WoW.ItemCount("Healthstone") >= 1 &&
                    !WoW.ItemOnCooldown("Healthstone") && WoW.HealthPercent < 30)
                {
                    WoW.CastSpell("HealthstoneKeybind");
                    return;
                }

                if (WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsInCombat && WoW.CanCast("CauterizeMaster") && WoW.HealthPercent < 100 && WoW.HasPet && !GrimoireOfSupremacy && Selfhealing)
                {
                    WoW.CastSpell("CauterizeMaster");
                    return;
                }

                //Log.Write("Time: "+ChaosBoltCast, Color.Red);
                // Normal Rotation (if Moving)
                if (WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsInCombat && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling && WoW.IsMoving && !WoW.PlayerHasBuff("Norgannon"))
                {
                    //Dimensional Rift (if 3 charges)
                    if (WoW.CanCast("DimensionalRift") && WoW.IsSpellInRange("Immolate") && WoW.PlayerSpellCharges("DimensionalRift") == 3)
                    {
                        WoW.CastSpell("DimensionalRift");
                        return;
                    }

                    //Conflagrate (Roaring Blaze not applied)
                    if (WoW.CanCast("Conflagrate") && RoaringBlaze && WoW.TargetDebuffTimeRemaining("Immolate") > 18 &&
                        (WoW.PlayerSpellCharges("Conflagrate") == 2 || (WoW.PlayerSpellCharges("Conflagrate") == 1 && ConflagTime.ElapsedMilliseconds > ConflagCD - gcd*1000)) &&
                        SecondImmolateCasted && WoW.IsSpellInRange("Immolate"))
                    {
                        WoW.CastSpell("Conflagrate");
                        blazed = true;
                        return;
                    }

                    //Conflagrate (Roaring Blaze applied)
                    if (WoW.CanCast("Conflagrate") && RoaringBlaze && blazed && SecondImmolateCasted && WoW.TargetDebuffTimeRemaining("Immolate") > 5 && WoW.CurrentSoulShards < 5 &&
                        WoW.IsSpellInRange("Immolate"))
                    {
                        WoW.CastSpell("Conflagrate");
                        return;
                    }

                    //Conflagrate (Conflagration Of Chaos is about to expire)
                    if (WoW.CanCast("Conflagrate") && !RoaringBlaze && !WoW.PlayerHasBuff("Backdraft") && WoW.PlayerBuffTimeRemaining("ConflagrationOfChaos") <= ChaosBoltCast &&
                        WoW.IsSpellInRange("Immolate"))
                    {
                        WoW.CastSpell("Conflagrate");
                        return;
                    }

                    //Conflagrate (w/ special conditions)
                    if (WoW.CanCast("Conflagrate") && !RoaringBlaze && !WoW.PlayerHasBuff("Backdraft") &&
                        ((WoW.PlayerSpellCharges("Conflagrate") == 1 && WoW.SpellCooldownTimeRemaining("Conflagrate") < ChaosBoltCast) || WoW.PlayerSpellCharges("Conflagrate") == 2) &&
                        WoW.CurrentSoulShards < 5 && WoW.IsSpellInRange("Immolate"))
                    {
                        WoW.CastSpell("Conflagrate");
                        return;
                    }

                    // Service Pet
                    if (WoW.CanCast("ServiceImp") && GrimoireOfService && WoW.CurrentSoulShards >= 1 && boss && WoW.IsSpellInRange("Immolate"))
                    {
                        WoW.CastSpell("ServiceImp");
                        return;
                    }

                    // Doomguard
                    if (WoW.CanCast("Doomguard") && WoW.PlayerHasDebuff("LordOfFlames") && WoW.CurrentSoulShards >= 1 && boss && WoW.IsSpellInRange("Immolate") && !GrimoireOfSupremacy)
                    {
                        WoW.CastSpell("Doomguard");
                        return;
                    }

                    //Havoc (if Odr legendary)
                    if (WoW.CanCast("Havoc") && Odr && (!WoW.TargetHasDebuff("Havoc") || WoW.TargetDebuffTimeRemaining("Havoc") < gcd) && boss)
                    {
                        WoW.CastSpell("Havoc");
                        return;
                    }

                    //Dimensional Rift (all while moving to sustain dps)
                    if (WoW.CanCast("DimensionalRift") && WoW.IsSpellInRange("Immolate") && boss)
                    {
                        WoW.CastSpell("DimensionalRift");
                        return;
                    }

                    //Mana Tap
                    if (WoW.CanCast("ManaTap") && ManaTap &&
                        (!WoW.PlayerHasBuff("ManaTap") || (WoW.PlayerBuffTimeRemaining("ManaTap") < 6 && (WoW.Mana < 20 || WoW.PlayerBuffTimeRemaining("ManaTap") < ChaosBoltCast))))
                    {
                        WoW.CastSpell("ManaTap");
                        return;
                    }

                    //Conflagrate
                    if (WoW.CanCast("Conflagrate") && !RoaringBlaze && !WoW.PlayerHasBuff("Backdraft") && WoW.IsSpellInRange("Immolate"))
                    {
                        WoW.CastSpell("Conflagrate");
                        return;
                    }

                    //Life Tap
                    if (WoW.CanCast("LifeTap") && ManaTap && WoW.Mana <= 10)
                    {
                        WoW.CastSpell("LifeTap");
                        return;
                    }

                    //Life Tap
                    if (WoW.CanCast("LifeTap") && WoW.Mana <= 70)
                    {
                        WoW.CastSpell("LifeTap");
                        return;
                    }
                }

                // Normal Rotation (if NOT Moving [or Norgannon])
                if (WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsInCombat && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling && (!WoW.IsMoving || WoW.PlayerHasBuff("Norgannon")))
                {
                    //Dimensional Rift (if 3 charges)
                    if (WoW.CanCast("DimensionalRift") && WoW.IsSpellInRange("Immolate") && WoW.PlayerSpellCharges("DimensionalRift") == 3)
                    {
                        WoW.CastSpell("DimensionalRift");
                        return;
                    }

                    //Immolate
                    if (WoW.CanCast("Immolate") && (!WoW.TargetHasDebuff("Immolate") || WoW.TargetDebuffTimeRemaining("Immolate") <= 3) && WoW.LastSpell != "Immolate" &&
                        WoW.IsSpellInRange("Immolate"))
                    {
                        WoW.CastSpell("Immolate");
                        blazed = false;
                        SecondImmolateCasted = false;
                        return;
                    }

                    //Immolate (Duration maxed with Roaring Blaze)
                    if (WoW.CanCast("Immolate") && RoaringBlaze && !blazed && WoW.TargetDebuffTimeRemaining("Immolate") <= 18 &&
                        (WoW.PlayerSpellCharges("Conflagrate") == 2 || (WoW.PlayerSpellCharges("Conflagrate") >= 1 && ConflagTime.ElapsedMilliseconds > ConflagCD - (ImmolateCastMS + gcd*1000))) &&
                        !SecondImmolateCasted && WoW.IsSpellInRange("Immolate"))
                    {
                        WoW.CastSpell("Immolate");
                        SecondImmolateCasted = true;
                        return;
                    }

                    //Conflagrate (Roaring Blaze not applied)
                    if (WoW.CanCast("Conflagrate") && RoaringBlaze && WoW.TargetDebuffTimeRemaining("Immolate") > 18 &&
                        (WoW.PlayerSpellCharges("Conflagrate") == 2 || (WoW.PlayerSpellCharges("Conflagrate") == 1 && ConflagTime.ElapsedMilliseconds > ConflagCD - gcd*1000)) &&
                        SecondImmolateCasted && WoW.IsSpellInRange("Immolate"))
                    {
                        WoW.CastSpell("Conflagrate");
                        blazed = true;
                        return;
                    }

                    //Conflagrate (Roaring Blaze applied)
                    if (WoW.CanCast("Conflagrate") && RoaringBlaze && blazed && SecondImmolateCasted && WoW.TargetDebuffTimeRemaining("Immolate") > 5 && WoW.CurrentSoulShards < 5 &&
                        WoW.IsSpellInRange("Immolate"))
                    {
                        WoW.CastSpell("Conflagrate");
                        return;
                    }

                    //Conflagrate (Conflagration Of Chaos is about to expire)
                    if (WoW.CanCast("Conflagrate") && !RoaringBlaze && !WoW.PlayerHasBuff("Backdraft") && WoW.PlayerBuffTimeRemaining("ConflagrationOfChaos") <= ChaosBoltCast &&
                        WoW.IsSpellInRange("Immolate"))
                    {
                        WoW.CastSpell("Conflagrate");
                        return;
                    }

                    //Conflagrate (w/ special conditions)
                    if (WoW.CanCast("Conflagrate") && !RoaringBlaze && !WoW.PlayerHasBuff("Backdraft") &&
                        ((WoW.PlayerSpellCharges("Conflagrate") == 1 && WoW.SpellCooldownTimeRemaining("Conflagrate") < ChaosBoltCast) || WoW.PlayerSpellCharges("Conflagrate") == 2) &&
                        WoW.CurrentSoulShards < 5 && WoW.IsSpellInRange("Immolate"))
                    {
                        WoW.CastSpell("Conflagrate");
                        return;
                    }

                    // Service Pet
                    if (WoW.CanCast("ServiceImp") && GrimoireOfService && WoW.CurrentSoulShards >= 1 && boss && WoW.IsSpellInRange("Immolate"))
                    {
                        WoW.CastSpell("ServiceImp");
                        return;
                    }

                    // Infernal (make a @cursor macro)
                    if (WoW.CanCast("Infernal") && LordOfFlames && !WoW.PlayerHasDebuff("LordOfFlames") && WoW.CurrentSoulShards >= 1 && boss && WoW.IsSpellInRange("Immolate") &&
                        !GrimoireOfSupremacy)
                    {
                        WoW.CastSpell("Infernal");
                        return;
                    }

                    // Doomguard
                    if (WoW.CanCast("Doomguard") && (WoW.PlayerHasDebuff("LordOfFlames") || !LordOfFlames) && WoW.CurrentSoulShards >= 1 && boss && WoW.IsSpellInRange("Immolate") &&
                        !GrimoireOfSupremacy)
                    {
                        WoW.CastSpell("Doomguard");
                        return;
                    }

                    //Chaos Bolt (if about to cap Soul Shards or has Backdraft)
                    if (WoW.CanCast("ChaosBolt") && WoW.CurrentSoulShards >= 2 && (WoW.CurrentSoulShards > 3 || WoW.PlayerHasBuff("Backdraft")) && WoW.IsSpellInRange("Immolate"))
                    {
                        WoW.CastSpell("ChaosBolt");
                        return;
                    }

                    //Incinerate (if Backdraft)
                    if (WoW.CanCast("Incinerate") && WoW.PlayerHasBuff("Backdraft") && WoW.IsSpellInRange("Immolate"))
                    {
                        WoW.CastSpell("Incinerate");
                        return;
                    }

                    //Havoc (if Odr legendary)
                    if (WoW.CanCast("Havoc") && Odr && (!WoW.TargetHasDebuff("Havoc") || WoW.TargetDebuffTimeRemaining("Havoc") < gcd) && boss)
                    {
                        WoW.CastSpell("Havoc");
                        return;
                    }

                    //Dimensional Rift (to get to designated charges)
                    if (WoW.CanCast("DimensionalRift") && ((WoW.PlayerSpellCharges("DimensionalRift") > 1 && DimensionRipper) || WoW.TargetHealthPercent < 30 || IsBLUp) && boss &&
                        WoW.IsSpellInRange("Immolate"))
                    {
                        WoW.CastSpell("DimensionalRift");
                        return;
                    }

                    //Dimensional Rift (only to avoid capping charges, we want to save charges for when moving. But casting everything when boss is low hp or during BL.)
                    if (WoW.CanCast("DimensionalRift") && DimRiftTime.ElapsedMilliseconds > 40000 &&
                        ((WoW.PlayerSpellCharges("DimensionalRift") == 2 && !DimensionRipper) || (WoW.PlayerSpellCharges("DimensionalRift") == 1 && DimensionRipper)) &&
                        WoW.IsSpellInRange("Immolate") && boss)
                    {
                        WoW.CastSpell("DimensionalRift");
                        return;
                    }

                    //Mana Tap
                    if (WoW.CanCast("ManaTap") && ManaTap &&
                        (!WoW.PlayerHasBuff("ManaTap") || (WoW.PlayerBuffTimeRemaining("ManaTap") < 6 && (WoW.Mana < 20 || WoW.PlayerBuffTimeRemaining("ManaTap") < ChaosBoltCast))))
                    {
                        WoW.CastSpell("ManaTap");
                        return;
                    }

                    //Chaos Bolt
                    if (WoW.CanCast("ChaosBolt") && WoW.CurrentSoulShards >= 2 && WoW.IsSpellInRange("Immolate"))
                    {
                        WoW.CastSpell("ChaosBolt");
                        return;
                    }

                    //Conflagrate
                    if (WoW.CanCast("Conflagrate") && !RoaringBlaze && !WoW.PlayerHasBuff("Backdraft") && WoW.IsSpellInRange("Immolate"))
                    {
                        WoW.CastSpell("Conflagrate");
                        return;
                    }

                    //Immolate (maintain)
                    if (WoW.CanCast("Immolate") && !RoaringBlaze && WoW.TargetDebuffTimeRemaining("Immolate") <= 8 && WoW.IsSpellInRange("Immolate"))
                    {
                        WoW.CastSpell("Immolate");
                        return;
                    }

                    //Life Tap
                    if (WoW.CanCast("LifeTap") && ManaTap && WoW.Mana <= 10)
                    {
                        WoW.CastSpell("LifeTap");
                        return;
                    }

                    //Incinerate
                    if (WoW.CanCast("Incinerate") && WoW.IsSpellInRange("Conflagrate"))
                    {
                        WoW.CastSpell("Incinerate");
                        return;
                    }

                    //Life Tap
                    if (WoW.CanCast("LifeTap") && WoW.Mana <= 70)
                    {
                        WoW.CastSpell("LifeTap");
                        return;
                    }
                }
            }

            // Cleave Rotation
            if (combatRoutine.Type == RotationType.SingleTargetCleave)
            {
                if (WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsInCombat && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling && WoW.ItemCount("Healthstone") >= 1 &&
                    !WoW.ItemOnCooldown("Healthstone") && WoW.HealthPercent < 30)
                {
                    WoW.CastSpell("HealthstoneKeybind");
                    return;
                }

                if (WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsInCombat && WoW.CanCast("CauterizeMaster") && WoW.HealthPercent < 100 && WoW.HasPet && !GrimoireOfSupremacy && Selfhealing)
                {
                    WoW.CastSpell("CauterizeMaster");
                    return;
                }

                // Cleave Rotation (if Moving)
                if (WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsInCombat && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling && WoW.IsMoving && !WoW.PlayerHasBuff("Norgannon"))
                {
                    //Dimensional Rift (if 3 charges)
                    if (WoW.CanCast("DimensionalRift") && WoW.IsSpellInRange("Immolate") && WoW.PlayerSpellCharges("DimensionalRift") == 3)
                    {
                        WoW.CastSpell("DimensionalRift");
                        return;
                    }

                    //Conflagrate (Roaring Blaze not applied)
                    if (WoW.CanCast("Conflagrate") && RoaringBlaze && WoW.TargetDebuffTimeRemaining("Immolate") > 18 &&
                        (WoW.PlayerSpellCharges("Conflagrate") == 2 || (WoW.PlayerSpellCharges("Conflagrate") == 1 && ConflagTime.ElapsedMilliseconds > ConflagCD - gcd*1000)) &&
                        SecondImmolateCasted && WoW.IsSpellInRange("Immolate"))
                    {
                        WoW.CastSpell("Conflagrate");
                        blazed = true;
                        return;
                    }

                    //Conflagrate (Roaring Blaze applied)
                    if (WoW.CanCast("Conflagrate") && RoaringBlaze && blazed && SecondImmolateCasted && WoW.TargetDebuffTimeRemaining("Immolate") > 5 && WoW.CurrentSoulShards < 5 &&
                        WoW.IsSpellInRange("Immolate"))
                    {
                        WoW.CastSpell("Conflagrate");
                        return;
                    }

                    //Conflagrate (Conflagration Of Chaos is about to expire)
                    if (WoW.CanCast("Conflagrate") && !RoaringBlaze && !WoW.PlayerHasBuff("Backdraft") && WoW.PlayerBuffTimeRemaining("ConflagrationOfChaos") <= ChaosBoltCast &&
                        WoW.IsSpellInRange("Immolate"))
                    {
                        WoW.CastSpell("Conflagrate");
                        return;
                    }

                    //Conflagrate (w/ special conditions)
                    if (WoW.CanCast("Conflagrate") && !RoaringBlaze && !WoW.PlayerHasBuff("Backdraft") &&
                        ((WoW.PlayerSpellCharges("Conflagrate") == 1 && WoW.SpellCooldownTimeRemaining("Conflagrate") < ChaosBoltCast) || WoW.PlayerSpellCharges("Conflagrate") == 2) &&
                        WoW.CurrentSoulShards < 5 && WoW.IsSpellInRange("Immolate"))
                    {
                        WoW.CastSpell("Conflagrate");
                        return;
                    }

                    // Service Pet
                    if (WoW.CanCast("ServiceImp") && GrimoireOfService && WoW.CurrentSoulShards >= 1 && boss && WoW.IsSpellInRange("Immolate"))
                    {
                        WoW.CastSpell("ServiceImp");
                        return;
                    }

                    // Doomguard
                    if (WoW.CanCast("Doomguard") && WoW.PlayerHasDebuff("LordOfFlames") && WoW.CurrentSoulShards >= 1 && boss && WoW.IsSpellInRange("Immolate"))
                    {
                        WoW.CastSpell("Doomguard");
                        return;
                    }

                    //Dimensional Rift
                    if (WoW.CanCast("DimensionalRift") && WoW.IsSpellInRange("Immolate") && boss)
                    {
                        WoW.CastSpell("DimensionalRift");
                        return;
                    }

                    //Mana Tap
                    if (WoW.CanCast("ManaTap") && ManaTap &&
                        (!WoW.PlayerHasBuff("ManaTap") || (WoW.PlayerBuffTimeRemaining("ManaTap") < 6 && (WoW.Mana < 20 || WoW.PlayerBuffTimeRemaining("ManaTap") < ChaosBoltCast))))
                    {
                        WoW.CastSpell("ManaTap");
                        return;
                    }

                    //Conflagrate
                    if (WoW.CanCast("Conflagrate") && !RoaringBlaze && !WoW.PlayerHasBuff("Backdraft") && WoW.IsSpellInRange("Immolate"))
                    {
                        WoW.CastSpell("Conflagrate");
                        return;
                    }

                    //Life Tap
                    if (WoW.CanCast("LifeTap") && ManaTap && WoW.Mana <= 10)
                    {
                        WoW.CastSpell("LifeTap");
                        return;
                    }

                    //Life Tap
                    if (WoW.CanCast("LifeTap") && WoW.Mana <= 70)
                    {
                        WoW.CastSpell("LifeTap");
                        return;
                    }
                }

                // Cleave Rotation (if NOT Moving [or Norgannon])
                if (WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsInCombat && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling && (!WoW.IsMoving || WoW.PlayerHasBuff("Norgannon")))
                {
                    //Dimensional Rift (if 3 charges)
                    if (WoW.CanCast("DimensionalRift") && WoW.IsSpellInRange("Immolate") && WoW.PlayerSpellCharges("DimensionalRift") == 3)
                    {
                        WoW.CastSpell("DimensionalRift");
                        return;
                    }

                    //Immolate
                    if (WoW.CanCast("Immolate") && (!WoW.TargetHasDebuff("Immolate") || WoW.TargetDebuffTimeRemaining("Immolate") <= 3) && WoW.LastSpell != "Immolate" &&
                        WoW.IsSpellInRange("Immolate"))
                    {
                        WoW.CastSpell("Immolate");
                        blazed = false;
                        SecondImmolateCasted = false;
                        return;
                    }

                    //Immolate (Duration maxed with Roaring Blaze)
                    if (WoW.CanCast("Immolate") && RoaringBlaze && !blazed && WoW.TargetDebuffTimeRemaining("Immolate") <= 18 &&
                        (WoW.PlayerSpellCharges("Conflagrate") == 2 || (WoW.PlayerSpellCharges("Conflagrate") >= 1 && ConflagTime.ElapsedMilliseconds > ConflagCD - (ImmolateCastMS + gcd*1000))) &&
                        !SecondImmolateCasted && WoW.IsSpellInRange("Immolate"))
                    {
                        WoW.CastSpell("Immolate");
                        SecondImmolateCasted = true;
                        return;
                    }

                    //Conflagrate (Roaring Blaze not applied)
                    if (WoW.CanCast("Conflagrate") && RoaringBlaze && WoW.TargetDebuffTimeRemaining("Immolate") > 18 &&
                        (WoW.PlayerSpellCharges("Conflagrate") == 2 || (WoW.PlayerSpellCharges("Conflagrate") == 1 && ConflagTime.ElapsedMilliseconds > ConflagCD - gcd*1000)) &&
                        SecondImmolateCasted && WoW.IsSpellInRange("Immolate"))
                    {
                        WoW.CastSpell("Conflagrate");
                        blazed = true;
                        return;
                    }

                    //Conflagrate (Roaring Blaze applied)
                    if (WoW.CanCast("Conflagrate") && RoaringBlaze && blazed && SecondImmolateCasted && WoW.TargetDebuffTimeRemaining("Immolate") > 5 && WoW.CurrentSoulShards < 5 &&
                        WoW.IsSpellInRange("Immolate"))
                    {
                        WoW.CastSpell("Conflagrate");
                        return;
                    }

                    //Conflagrate (Conflagration Of Chaos is about to expire)
                    if (WoW.CanCast("Conflagrate") && !RoaringBlaze && !WoW.PlayerHasBuff("Backdraft") && WoW.PlayerBuffTimeRemaining("ConflagrationOfChaos") <= ChaosBoltCast &&
                        WoW.IsSpellInRange("Immolate"))
                    {
                        WoW.CastSpell("Conflagrate");
                        return;
                    }

                    //Conflagrate (w/ special conditions)
                    if (WoW.CanCast("Conflagrate") && !RoaringBlaze && !WoW.PlayerHasBuff("Backdraft") &&
                        ((WoW.PlayerSpellCharges("Conflagrate") == 1 && WoW.SpellCooldownTimeRemaining("Conflagrate") < ChaosBoltCast) || WoW.PlayerSpellCharges("Conflagrate") == 2) &&
                        WoW.CurrentSoulShards < 5 && WoW.IsSpellInRange("Immolate"))
                    {
                        WoW.CastSpell("Conflagrate");
                        return;
                    }

                    // Service Pet
                    if (WoW.CanCast("ServiceImp") && GrimoireOfService && WoW.CurrentSoulShards >= 1 && boss && WoW.IsSpellInRange("Immolate"))
                    {
                        WoW.CastSpell("ServiceImp");
                        return;
                    }

                    // Infernal (make a @cursor macro)
                    if (WoW.CanCast("Infernal") && LordOfFlames && !WoW.PlayerHasDebuff("LordOfFlames") && WoW.CurrentSoulShards >= 1 && boss && WoW.IsSpellInRange("Immolate"))
                    {
                        WoW.CastSpell("Infernal");
                        return;
                    }

                    // Doomguard
                    if (WoW.CanCast("Doomguard") && (WoW.PlayerHasDebuff("LordOfFlames") || !LordOfFlames) && WoW.CurrentSoulShards >= 1 && boss && WoW.IsSpellInRange("Immolate"))
                    {
                        WoW.CastSpell("Doomguard");
                        return;
                    }

                    //Chaos Bolt (if about to cap Soul Shards or has Backdraft)
                    if (WoW.CanCast("ChaosBolt") && WoW.CurrentSoulShards >= 2 && (WoW.CurrentSoulShards > 3 || WoW.PlayerHasBuff("Backdraft")) && WoW.IsSpellInRange("Immolate"))
                    {
                        WoW.CastSpell("ChaosBolt");
                        return;
                    }

                    //Incinerate (if Backdraft)
                    if (WoW.CanCast("Incinerate") && WoW.PlayerHasBuff("Backdraft") && WoW.IsSpellInRange("Immolate"))
                    {
                        WoW.CastSpell("Incinerate");
                        return;
                    }

                    //Dimensional Rift (if no shards and Conflagrate is on cd)
                    if (WoW.CanCast("DimensionalRift") && WoW.CurrentSoulShards == 0 && !WoW.CanCast("Conflagrate") && WoW.SpellCooldownTimeRemaining("Conflagrate") > gcd && boss &&
                        WoW.IsSpellInRange("Immolate"))
                    {
                        WoW.CastSpell("DimensionalRift");
                        return;
                    }

                    //Mana Tap
                    if (WoW.CanCast("ManaTap") && ManaTap &&
                        (!WoW.PlayerHasBuff("ManaTap") || (WoW.PlayerBuffTimeRemaining("ManaTap") < 6 && (WoW.Mana < 20 || WoW.PlayerBuffTimeRemaining("ManaTap") < ChaosBoltCast))))
                    {
                        WoW.CastSpell("ManaTap");
                        return;
                    }

                    //Chaos Bolt
                    if (WoW.CanCast("ChaosBolt") && WoW.CurrentSoulShards >= 2 && WoW.IsSpellInRange("Immolate"))
                    {
                        WoW.CastSpell("ChaosBolt");
                        return;
                    }

                    //Conflagrate
                    if (WoW.CanCast("Conflagrate") && !RoaringBlaze && !WoW.PlayerHasBuff("Backdraft") && WoW.IsSpellInRange("Immolate"))
                    {
                        WoW.CastSpell("Conflagrate");
                        return;
                    }

                    //Immolate (maintain)
                    if (WoW.CanCast("Immolate") && !RoaringBlaze && WoW.TargetDebuffTimeRemaining("Immolate") <= 8 && WoW.IsSpellInRange("Immolate"))
                    {
                        WoW.CastSpell("Immolate");
                        return;
                    }

                    //Life Tap
                    if (WoW.CanCast("LifeTap") && ManaTap && WoW.Mana <= 10)
                    {
                        WoW.CastSpell("LifeTap");
                        return;
                    }

                    //Incinerate
                    if (WoW.CanCast("Incinerate") && WoW.IsSpellInRange("Conflagrate"))
                    {
                        WoW.CastSpell("Incinerate");
                        return;
                    }

                    //Life Tap
                    if (WoW.CanCast("LifeTap") && WoW.Mana <= 70)
                    {
                        WoW.CastSpell("LifeTap");
                        return;
                    }
                }
            }

            // AoE Rotation
            if (combatRoutine.Type == RotationType.AOE)
            {
                if (WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsInCombat && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling && WoW.ItemCount("Healthstone") >= 1 &&
                    !WoW.ItemOnCooldown("Healthstone") && WoW.HealthPercent < 30)
                {
                    WoW.CastSpell("HealthstoneKeybind");
                    return;
                }

                if (WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsInCombat && WoW.CanCast("CauterizeMaster") && WoW.HealthPercent < 100 && WoW.HasPet && !GrimoireOfSupremacy && Selfhealing)
                {
                    WoW.CastSpell("CauterizeMaster");
                    return;
                }

                //AoE Rotation (if Moving)
                if (WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsInCombat && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling && WoW.IsMoving && !WoW.PlayerHasBuff("Norgannon"))
                {
                    //Rain Of Fire
                    if (WoW.CanCast("RainOfFire") && WoW.CurrentSoulShards > 4)
                    {
                        WoW.CastSpell("RainOfFire");
                        return;
                    }

                    //Conflagrate (Conflagration Of Chaos is about to expire)
                    if (WoW.CanCast("Conflagrate") && !WoW.PlayerHasBuff("Backdraft") && WoW.PlayerBuffTimeRemaining("ConflagrationOfChaos") <= ChaosBoltCast && WoW.IsSpellInRange("Immolate"))
                    {
                        WoW.CastSpell("Conflagrate");
                        return;
                    }

                    //Conflagrate (w/ special conditions)
                    if (WoW.CanCast("Conflagrate") && !WoW.PlayerHasBuff("Backdraft") &&
                        ((WoW.PlayerSpellCharges("Conflagrate") == 1 && WoW.SpellCooldownTimeRemaining("Conflagrate") < ChaosBoltCast) || WoW.PlayerSpellCharges("Conflagrate") == 2) &&
                        WoW.CurrentSoulShards < 5 && WoW.IsSpellInRange("Immolate"))
                    {
                        WoW.CastSpell("Conflagrate");
                        return;
                    }

                    //Rain Of Fire
                    if (WoW.CanCast("RainOfFire") && WoW.CurrentSoulShards >= 3)
                    {
                        WoW.CastSpell("RainOfFire");
                        return;
                    }

                    //Dimensional Rift
                    if (WoW.CanCast("DimensionalRift") && boss && WoW.IsSpellInRange("Immolate"))
                    {
                        WoW.CastSpell("DimensionalRift");
                        return;
                    }

                    //Conflagrate
                    if (WoW.CanCast("Conflagrate") && !WoW.PlayerHasBuff("Backdraft") && WoW.IsSpellInRange("Immolate"))
                    {
                        WoW.CastSpell("Conflagrate");
                        return;
                    }

                    //Mana Tap
                    if (WoW.CanCast("ManaTap") && ManaTap &&
                        (!WoW.PlayerHasBuff("ManaTap") || (WoW.PlayerBuffTimeRemaining("ManaTap") < 6 && (WoW.Mana < 20 || WoW.PlayerBuffTimeRemaining("ManaTap") < ChaosBoltCast))))
                    {
                        WoW.CastSpell("ManaTap");
                        return;
                    }

                    //Conflagrate
                    if (WoW.CanCast("Conflagrate") && !WoW.PlayerHasBuff("Backdraft") && WoW.IsSpellInRange("Immolate"))
                    {
                        WoW.CastSpell("Conflagrate");
                        return;
                    }

                    //Life Tap
                    if (WoW.CanCast("LifeTap") && ManaTap && WoW.Mana <= 10)
                    {
                        WoW.CastSpell("LifeTap");
                        return;
                    }


                    //Life Tap
                    if (WoW.CanCast("LifeTap") && WoW.Mana <= 70)
                    {
                        WoW.CastSpell("LifeTap");
                        return;
                    }
                }

                // Normal Rotation (if NOT Moving [or Norgannon])
                if (WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsInCombat && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling && (!WoW.IsMoving || WoW.PlayerHasBuff("Norgannon")))
                {
                    //Rain Of Fire
                    if (WoW.CanCast("RainOfFire") && WoW.CurrentSoulShards > 4)
                    {
                        WoW.CastSpell("RainOfFire");
                        return;
                    }

                    //Immolate
                    if (WoW.CanCast("Immolate") && (!WoW.TargetHasDebuff("Immolate") || WoW.TargetDebuffTimeRemaining("Immolate") <= 3) && WoW.IsSpellInRange("Immolate"))
                    {
                        WoW.CastSpell("Immolate");
                        blazed = false;
                        return;
                    }

                    //Conflagrate (Conflagration Of Chaos is about to expire)
                    if (WoW.CanCast("Conflagrate") && !WoW.PlayerHasBuff("Backdraft") && WoW.PlayerBuffTimeRemaining("ConflagrationOfChaos") <= ChaosBoltCast && WoW.IsSpellInRange("Immolate"))
                    {
                        WoW.CastSpell("Conflagrate");
                        return;
                    }

                    //Conflagrate (w/ special conditions)
                    if (WoW.CanCast("Conflagrate") && !WoW.PlayerHasBuff("Backdraft") &&
                        ((WoW.PlayerSpellCharges("Conflagrate") == 1 && WoW.SpellCooldownTimeRemaining("Conflagrate") < ChaosBoltCast) || WoW.PlayerSpellCharges("Conflagrate") == 2) &&
                        WoW.CurrentSoulShards < 5 && WoW.IsSpellInRange("Immolate"))
                    {
                        WoW.CastSpell("Conflagrate");
                        return;
                    }

                    //Rain Of Fire
                    if (WoW.CanCast("RainOfFire") && WoW.CurrentSoulShards >= 3)
                    {
                        WoW.CastSpell("RainOfFire");
                        return;
                    }

                    //Dimensional Rift (if no shards and Conflagrate is on cd)
                    if (WoW.CanCast("DimensionalRift") && WoW.CurrentSoulShards == 0 && !WoW.CanCast("Conflagrate") && WoW.SpellCooldownTimeRemaining("Conflagrate") > gcd && boss &&
                        WoW.IsSpellInRange("Immolate"))
                    {
                        WoW.CastSpell("DimensionalRift");
                        return;
                    }

                    //Conflagrate
                    if (WoW.CanCast("Conflagrate") && !WoW.PlayerHasBuff("Backdraft") && WoW.IsSpellInRange("Immolate"))
                    {
                        WoW.CastSpell("Conflagrate");
                        return;
                    }

                    //Mana Tap
                    if (WoW.CanCast("ManaTap") && ManaTap &&
                        (!WoW.PlayerHasBuff("ManaTap") || (WoW.PlayerBuffTimeRemaining("ManaTap") < 6 && (WoW.Mana < 20 || WoW.PlayerBuffTimeRemaining("ManaTap") < ChaosBoltCast))))
                    {
                        WoW.CastSpell("ManaTap");
                        return;
                    }

                    //Conflagrate
                    if (WoW.CanCast("Conflagrate") && !WoW.PlayerHasBuff("Backdraft") && WoW.IsSpellInRange("Immolate"))
                    {
                        WoW.CastSpell("Conflagrate");
                        return;
                    }

                    //Immolate (maintain)
                    if (WoW.CanCast("Immolate") && WoW.TargetDebuffTimeRemaining("Immolate") <= 8 && WoW.IsSpellInRange("Immolate"))
                    {
                        WoW.CastSpell("Immolate");
                        return;
                    }

                    //Life Tap
                    if (WoW.CanCast("LifeTap") && ManaTap && WoW.Mana <= 10)
                    {
                        WoW.CastSpell("LifeTap");
                        return;
                    }

                    //Incinerate
                    if (WoW.CanCast("Incinerate") && WoW.IsSpellInRange("Conflagrate"))
                    {
                        WoW.CastSpell("Incinerate");
                        return;
                    }

                    //Life Tap
                    if (WoW.CanCast("LifeTap") && WoW.Mana <= 70)
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
AddonName=PixelMagic
WoWVersion=Legion - 70300
[SpellBook.db]
Spell,29722,Incinerate,F6
Spell,1454,LifeTap,R
Spell,348,Immolate,F4
Spell,116858,ChaosBolt,F1
Spell,111859,ServiceImp,F
Spell,196586,DimensionalRift,F3
Spell,18540,Doomguard,T
Spell,1122,Infernal,G
Spell,17962,Conflagrate,F5
Spell,196104,ManaTap,Z
Spell,5740,RainOfFire,Q
Spell,80140,Havoc,F2
Spell,119905,CauterizeMaster,E
Spell,0,HealthstoneKeybind,F10
Aura,348,Immolate
Aura,117828,Backdraft
Aura,196546,ConflagrationOfChaos
Aura,196104,ManaTap
Aura,111400,BurningRush
Aura,226802,LordOfFlames
Aura,80240,Havoc
Aura,208215,Norgannon
Aura,2825,Bloodlust
Aura,80353,TimeWarp
Aura,32182,Heroism
Item,5512,Healthstone
*/