// winifix@gmail.com
// ReSharper disable UnusedMember.Global
// ReSharper disable ConvertPropertyToExpressionBody

// Changelog :
// Version 2.0 beta 5
// - Patch 7.1.5 broke Moonfire, fixed with workaround
// Version 2.0 beta 4
// - Basic PVP using BrutalSlash, Incarnation (if checked) and Renewal (if checked)
// Version 2.0 beta 3
// - Improved more on Rip usage.
// Version 2.0 beta 2
// - Improvements on Rip usage while cooldowns are running. Workaround to bleed snapshotting.
// Version 2.0 beta 1
// - Complete rebuild of rotation
// - Bleed strenght tracking not yet implemented.
// - PvP Missing

using ShadowMagic.Helpers;
using System.Drawing;
using System.Threading;
using System;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Windows.Forms;

namespace ShadowMagic.Rotation
{

    public class Feral_Scotishdwarf : CombatRoutine
    {
        private static readonly Stopwatch pullwatch = new Stopwatch();
        private static readonly Stopwatch ripunbuffed = new Stopwatch();
        private static readonly Stopwatch ripbuffed = new Stopwatch();
        private static readonly Stopwatch superrip = new Stopwatch();
        private CheckBox BloodtalonsBox;
        private CheckBox ElunesGuidanceBox;
        private CheckBox SabertoothBox;
        private CheckBox AiluroPouncersBox;
        private CheckBox SavageRoarBox;
        private CheckBox JaggedWoundsBox;
        private CheckBox NightElfBox;
        private CheckBox BrutalSlashBox;
        private CheckBox LunarInspirationBox;
        private CheckBox IncarnationBox;
        private CheckBox RenewalBox;
        private CheckBox KBWBox;

        public override string Name
        {
            get
            {
                return "Feral Rotation";
            }
        }

        public override string Class
        {
            get
            {
                return "Druid";
            }
        }

        private static bool Bloodtalons
        {
            get
            {
                var bloodtalons = ConfigFile.ReadValue("Feral_Scotishdwarf", "Bloodtalons").Trim();

                return bloodtalons != "" && Convert.ToBoolean(bloodtalons);
            }
            set { ConfigFile.WriteValue("Feral_Scotishdwarf", "Bloodtalons", value.ToString()); }
        }
        private static bool ElunesGuidance
        {
            get
            {
                var elunesGuidance = ConfigFile.ReadValue("Feral_Scotishdwarf", "ElunesGuidance").Trim();

                return elunesGuidance != "" && Convert.ToBoolean(elunesGuidance);
            }
            set { ConfigFile.WriteValue("Feral_Scotishdwarf", "ElunesGuidance", value.ToString()); }
        }
        private static bool Sabertooth
        {
            get
            {
                var sabertooth = ConfigFile.ReadValue("Feral_Scotishdwarf", "Sabertooth").Trim();

                return sabertooth != "" && Convert.ToBoolean(sabertooth);
            }
            set { ConfigFile.WriteValue("Feral_Scotishdwarf", "Sabertooth", value.ToString()); }
        }
        private static bool AiluroPouncers
        {
            get
            {
                var ailuroPouncers = ConfigFile.ReadValue("Feral_Scotishdwarf", "AiluroPouncers").Trim();

                return ailuroPouncers != "" && Convert.ToBoolean(ailuroPouncers);
            }
            set { ConfigFile.WriteValue("Feral_Scotishdwarf", "AiluroPouncers", value.ToString()); }
        }
        private static bool SavageRoar
        {
            get
            {
                var savageRoar = ConfigFile.ReadValue("Feral_Scotishdwarf", "SavageRoar").Trim();

                return savageRoar != "" && Convert.ToBoolean(savageRoar);
            }
            set { ConfigFile.WriteValue("Feral_Scotishdwarf", "SavageRoar", value.ToString()); }
        }
        private static bool JaggedWounds
        {
            get
            {
                var jaggedWounds = ConfigFile.ReadValue("Feral_Scotishdwarf", "JaggedWounds").Trim();

                return jaggedWounds != "" && Convert.ToBoolean(jaggedWounds);
            }
            set { ConfigFile.WriteValue("Feral_Scotishdwarf", "JaggedWounds", value.ToString()); }
        }
        private static bool NightElf
        {
            get
            {
                var nightElf = ConfigFile.ReadValue("Feral_Scotishdwarf", "NightElf").Trim();

                return nightElf != "" && Convert.ToBoolean(nightElf);
            }
            set { ConfigFile.WriteValue("Feral_Scotishdwarf", "NightElf", value.ToString()); }
        }
        private static bool BrutalSlash
        {
            get
            {
                var brutalSlash = ConfigFile.ReadValue("Feral_Scotishdwarf", "BrutalSlash").Trim();

                return brutalSlash != "" && Convert.ToBoolean(brutalSlash);
            }
            set { ConfigFile.WriteValue("Feral_Scotishdwarf", "BrutalSlash", value.ToString()); }
        }
        private static bool LunarInspiration
        {
            get
            {
                var lunarInspiration = ConfigFile.ReadValue("Feral_Scotishdwarf", "LunarInspiration").Trim();

                return lunarInspiration != "" && Convert.ToBoolean(lunarInspiration);
            }
            set { ConfigFile.WriteValue("Feral_Scotishdwarf", "LunarInspiration", value.ToString()); }
        }
        private static bool Incarnation
        {
            get
            {
                var incarnation = ConfigFile.ReadValue("Feral_Scotishdwarf", "Incarnation").Trim();

                return incarnation != "" && Convert.ToBoolean(incarnation);
            }
            set { ConfigFile.WriteValue("Feral_Scotishdwarf", "Incarnation", value.ToString()); }
        }
        private static bool Renewal
        {
            get
            {
                var renewal = ConfigFile.ReadValue("Feral_Scotishdwarf", "Renewal").Trim();

                return renewal != "" && Convert.ToBoolean(renewal);
            }
            set { ConfigFile.WriteValue("Feral_Scotishdwarf", "Renewal", value.ToString()); }
        }
        private static bool KBW
        {
            get
            {
                var kBW = ConfigFile.ReadValue("BalanceDruid", "KBW").Trim();

                return kBW != "" && Convert.ToBoolean(kBW);
            }
            set { ConfigFile.WriteValue("BalanceDruid", "KBW", value.ToString()); }
        }
        protected string Linkify(string SearchText)
        {
            Regex regx = new Regex(@"\b(((\S+)?)(@|mailto\:|(news|(ht|f)tp(s?))\://)\S+)\b", RegexOptions.IgnoreCase);
            SearchText = SearchText.Replace("&nbsp;", " ");
            MatchCollection matches = regx.Matches(SearchText);

            foreach (Match match in matches)
            {
                if (match.Value.StartsWith("http"))
                { // if it starts with anything else then dont linkify -- may already be linked!
                    SearchText = SearchText.Replace(match.Value, "<a href='" + match.Value + "'>" + match.Value + "</a>");
                }
            }

            return SearchText;
        }

        public override void Initialize()
        {
            MessageBox.Show("Welcome to Feral Druid by Scotishdwarf 2.0b5.\n\n- Suggested build : 3,2,2,1,3,2,2\n\n- Complete rebuild of rotation\n- Bleed strenght tracked trough workaround for Rip, still need code for Rake.\n- Basic pvp using Brutal Slash, Incarnation and Renewal\n\nPlease configure rotation trough Rotation Settings.\n\nThis is beta build, bugs, much doge.");
            Log.Write("Welcome to Feral Druid by Scotishdwarf", Color.Purple);
            Log.Write("Suggested build : http://eu.battle.net/wow/en/tool/talent-calculator#UZa!2110211", Color.Purple);

            SettingsForm = new Form { Text = "Settings", StartPosition = FormStartPosition.CenterScreen, Width = 500, Height = 250, ShowIcon = false };

            // TALENTS
            var lblTalentsForm = new Label { Text = "Talents", Size = new Size(150, 13), Left = 12, Top = 14 };
            lblTalentsForm.ForeColor = System.Drawing.Color.Red;
            SettingsForm.Controls.Add(lblTalentsForm);

            // Legendaries 
            var lblLegendaryForm = new Label { Text = "Legendaries", Size = new Size(150, 13), Left = 292, Top = 14 };
            lblLegendaryForm.ForeColor = System.Drawing.Color.Red;
            SettingsForm.Controls.Add(lblLegendaryForm);

            // Misc
            var lblMiscForm = new Label { Text = "Misc", Size = new Size(150, 13), Left = 292, Top = 59 };
            lblMiscForm.ForeColor = System.Drawing.Color.Red;
            SettingsForm.Controls.Add(lblMiscForm);

            // LUNAR INSPIRATION
            var lblLunarInspirationText = new Label { Text = "L15: Lunar Inspiration", Size = new Size(100, 13), Left = 12, Top = 29 };
            SettingsForm.Controls.Add(lblLunarInspirationText);

            LunarInspirationBox = new CheckBox { Checked = LunarInspiration, TabIndex = 6, Size = new Size(15, 14), Left = 140, Top = 29 };
            SettingsForm.Controls.Add(LunarInspirationBox);

            // Renewal
            var lblRenewalText = new Label { Text = "L30 : Renewal", Size = new Size(80, 13), Left = 12, Top = 44 };
            SettingsForm.Controls.Add(lblRenewalText);

            RenewalBox = new CheckBox { Checked = Renewal, TabIndex = 4, Size = new Size(15, 14), Left = 140, Top = 44 };
            SettingsForm.Controls.Add(RenewalBox);

            // INCARNATION
            var lblIncarnationText = new Label { Text = "L75 : Incarnation", Size = new Size(100, 13), Left = 12, Top = 59 };
            SettingsForm.Controls.Add(lblIncarnationText);

            IncarnationBox = new CheckBox { Checked = Incarnation, TabIndex = 4, Size = new Size(15, 14), Left = 140, Top = 59 };
            SettingsForm.Controls.Add(IncarnationBox);

            // SAVAGE ROAR
            var lblSavageRoarText = new Label { Text = "L75 : Savage Roar", Size = new Size(100, 13), Left = 12, Top = 74 };
            SettingsForm.Controls.Add(lblSavageRoarText);

            SavageRoarBox = new CheckBox { Checked = SavageRoar, TabIndex = 6, Size = new Size(15, 14), Left = 140, Top = 74 };
            SettingsForm.Controls.Add(SavageRoarBox);
            
            // SABERTOOTH
            var lblSabertoothText = new Label { Text = "L90 : Sabertooth", Size = new Size(100, 13), Left = 12, Top = 89 };
            SettingsForm.Controls.Add(lblSabertoothText);

            SabertoothBox = new CheckBox { Checked = Sabertooth, TabIndex = 6, Size = new Size(15, 14), Left = 140, Top = 89 };
            SettingsForm.Controls.Add(SabertoothBox);

            // JAGGED WOUNDS
            var lblJaggedWoundsText = new Label { Text = "L90 : Jagged Wounds", Size = new Size(100, 13), Left = 12, Top = 104 };
            SettingsForm.Controls.Add(lblJaggedWoundsText);

            JaggedWoundsBox = new CheckBox { Checked = JaggedWounds, TabIndex = 6, Size = new Size(15, 14), Left = 140, Top = 104 };
            SettingsForm.Controls.Add(JaggedWoundsBox);

            // ELUNES GUIDANCE
            var lblElunesGuidanceText = new Label { Text = "L90 : Elune's Guidance", Size = new Size(100, 13), Left = 12, Top = 119 };
            SettingsForm.Controls.Add(lblElunesGuidanceText);

            ElunesGuidanceBox = new CheckBox { Checked = ElunesGuidance, TabIndex = 4, Size = new Size(15, 14), Left = 140, Top = 119 };
            SettingsForm.Controls.Add(ElunesGuidanceBox);

            // BLOODTALONS
            var lblBloodtalonsText = new Label { Text = "L100 : Bloodtalons", Size = new Size(100, 13), Left = 12, Top = 134 };
            SettingsForm.Controls.Add(lblBloodtalonsText);

            BloodtalonsBox = new CheckBox { Checked = Bloodtalons, TabIndex = 2, Size = new Size(15, 14), Left = 140, Top = 134 };
            SettingsForm.Controls.Add(BloodtalonsBox);

            // BRUTAL SLASH
            var lblBrutalSlashText = new Label { Text = "L100 : Brutal Slash", Size = new Size(100, 13), Left = 12, Top = 149 };
            SettingsForm.Controls.Add(lblBrutalSlashText);

            BrutalSlashBox = new CheckBox { Checked = BrutalSlash, TabIndex = 6, Size = new Size(15, 14), Left = 140, Top = 149 };
            SettingsForm.Controls.Add(BrutalSlashBox);

            // AILURO POUNCERS
            var lblAiluroPouncersText = new Label { Text = "Ailuro Pouncers", Size = new Size(100, 13), Left = 292, Top = 29 };
            SettingsForm.Controls.Add(lblAiluroPouncersText);

            AiluroPouncersBox = new CheckBox { Checked = AiluroPouncers, TabIndex = 6, Size = new Size(15, 14), Left = 420, Top = 29 };
            SettingsForm.Controls.Add(AiluroPouncersBox);

            // Kil'jaeden's Burning Wish
            var lblKBWText = new Label { Text = "Item : Kil'jaeden's Burning Wish", Size = new Size(200, 13), Left = 292, Top = 44 };
            SettingsForm.Controls.Add(lblKBWText);

            KBWBox = new CheckBox { Checked = KBW, TabIndex = 6, Size = new Size(15, 14), Left = 420, Top = 44 };
            SettingsForm.Controls.Add(KBWBox);

            // NIGHT ELF
            var lblNightElfText = new Label { Text = "Race : Night Elf", Size = new Size(100, 13), Left = 292, Top = 74 };
            SettingsForm.Controls.Add(lblNightElfText);

            NightElfBox = new CheckBox { Checked = NightElf, TabIndex = 6, Size = new Size(15, 14), Left = 420, Top = 74 };
            SettingsForm.Controls.Add(NightElfBox);

            var cmdSave = new Button { Text = "Save", Width = 65, Height = 25, Left = 350, Top = 164, Size = new Size(108, 31) };

            BloodtalonsBox.Checked = Bloodtalons;
            ElunesGuidanceBox.Checked = ElunesGuidance;
            SabertoothBox.Checked = Sabertooth;
            AiluroPouncersBox.Checked = AiluroPouncers;
            SavageRoarBox.Checked = SavageRoar;
            JaggedWoundsBox.Checked = JaggedWounds;
            NightElfBox.Checked = NightElf;
            BrutalSlashBox.Checked = BrutalSlash;
            LunarInspirationBox.Checked = LunarInspiration;
            IncarnationBox.Checked = Incarnation;
            RenewalBox.Checked = Renewal;

            cmdSave.Click += CmdSave_Click;
            BloodtalonsBox.CheckedChanged += Bloodtalons_Click;
            ElunesGuidanceBox.CheckedChanged += ElunesGuidance_Click;
            SabertoothBox.CheckedChanged += Sabertooth_Click;
            AiluroPouncersBox.CheckedChanged += AiluroPouncers_Click;
            SavageRoarBox.CheckedChanged += SavageRoar_Click;
            JaggedWoundsBox.CheckedChanged += JaggedWounds_Click;
            NightElfBox.CheckedChanged += NightElf_Click;
            BrutalSlashBox.CheckedChanged += BrutalSlash_Click;
            LunarInspirationBox.CheckedChanged += LunarInspiration_Click;
            IncarnationBox.CheckedChanged += Incarnation_Click;
            RenewalBox.CheckedChanged += Renewal_Click;

            SettingsForm.Controls.Add(cmdSave);
            lblBloodtalonsText.BringToFront();
            lblElunesGuidanceText.BringToFront();
            lblSabertoothText.BringToFront();
            lblAiluroPouncersText.BringToFront();
            lblSavageRoarText.BringToFront();
            lblJaggedWoundsText.BringToFront();
            lblNightElfText.BringToFront();
            lblBrutalSlashText.BringToFront();
            lblLunarInspirationText.BringToFront();
            lblIncarnationText.BringToFront();
            lblRenewalText.BringToFront();

            Log.Write("Bloodtalons = " + Bloodtalons);
            Log.Write("Elunes Guidance = " +ElunesGuidance);
            Log.Write("Sabertooth = " + Sabertooth);
            Log.Write("Ailuro Pouncers = " + AiluroPouncers);
            Log.Write("Savage Roar = " + SavageRoar);
            Log.Write("Jagged Wounds = " + JaggedWounds);
            Log.Write("Race : Night Elf = " + NightElf);
            Log.Write("Brutal Slash = " + BrutalSlash);
            Log.Write("Lunar Inspiration = " + LunarInspiration);
            Log.Write("Incarnation = " + Incarnation);
            Log.Write("Renewal = " + Renewal);
        }

        private void CmdSave_Click(object sender, EventArgs e)
        {
            Bloodtalons = BloodtalonsBox.Checked;
            ElunesGuidance = ElunesGuidanceBox.Checked;
            Sabertooth = SabertoothBox.Checked;
            AiluroPouncers = AiluroPouncersBox.Checked;
            SavageRoar = SavageRoarBox.Checked;
            JaggedWounds = JaggedWoundsBox.Checked;
            NightElf = NightElfBox.Checked;
            BrutalSlash = BrutalSlashBox.Checked;
            LunarInspiration = LunarInspirationBox.Checked;
            Incarnation = IncarnationBox.Checked;
            Renewal = RenewalBox.Checked;

            MessageBox.Show("Settings saved", "PixelMagic", MessageBoxButtons.OK, MessageBoxIcon.Information);
            SettingsForm.Close();
        }

        private void Bloodtalons_Click(object sender, EventArgs e)
        {
            Bloodtalons = BloodtalonsBox.Checked;
            BrutalSlash = BrutalSlashBox.Checked = false;
        }
        private void ElunesGuidance_Click(object sender, EventArgs e)
        {
            ElunesGuidance = ElunesGuidanceBox.Checked;
        }
        private void Sabertooth_Click(object sender, EventArgs e)
        {
            Sabertooth = SabertoothBox.Checked;
        }
        private void AiluroPouncers_Click(object sender, EventArgs e)
        {
            AiluroPouncers = AiluroPouncersBox.Checked;
        }
        private void SavageRoar_Click(object sender, EventArgs e)
        {
            SavageRoar = SavageRoarBox.Checked;
            Incarnation = IncarnationBox.Checked = false;
        }
        private void JaggedWounds_Click(object sender, EventArgs e)
        {
            JaggedWounds = JaggedWoundsBox.Checked;
        }
        private void NightElf_Click(object sender, EventArgs e)
        {
            NightElf = NightElfBox.Checked;
        }
        private void BrutalSlash_Click(object sender, EventArgs e)
        {
            BrutalSlash = BrutalSlashBox.Checked;
            Bloodtalons = BloodtalonsBox.Checked = false;
        }
        private void LunarInspiration_Click(object sender, EventArgs e)
        {
            LunarInspiration = LunarInspirationBox.Checked;
        }
        private void Incarnation_Click(object sender, EventArgs e)
        {
            Incarnation = IncarnationBox.Checked;
            SavageRoar = SavageRoarBox.Checked = false;
        }
        private void Renewal_Click(object sender, EventArgs e)
        {
            Renewal = RenewalBox.Checked;
        }

        public override void Stop()
        {
        }

        public override void Pulse()
        {
            if (combatRoutine.Type == RotationType.SingleTarget)
            {
                // Stopwatch START
                if (WoW.IsInCombat && !pullwatch.IsRunning)
                {
                    pullwatch.Start();
                    Log.Write("Entering Combat, Starting opener timer.", Color.Red);
                }
                // Stopwatch stop
                if (!WoW.IsInCombat && pullwatch.ElapsedMilliseconds >= 1000)
                {
                    pullwatch.Reset();
                    ripunbuffed.Reset();
                    ripbuffed.Reset();
                    superrip.Reset();
                    Log.Write("Leaving Combat, Resetting opener timer.", Color.Red);
                }

                //PVP, Basic PVP with Brutal Slash, Incarnation and Renewal (if enabled)
                if (WoW.TargetIsPlayer && WoW.TargetIsEnemy && WoW.IsInCombat)
                {
                    if (WoW.CanCast("Regrowth") && WoW.PlayerHasBuff("Regrowth") && WoW.HealthPercent <= 80 && WoW.PlayerHasBuff("PredatorySwiftness"))
                    {
                        WoW.CastSpell("Regrowth");
                        return;
                    }
                    if (Renewal && WoW.CanCast("Renwal") && WoW.HealthPercent <= 70)
                    {
                        WoW.CastSpell("Renewal");
                        return;
                    }
                    if ((WoW.IsSpellInRange("FerociousBite") && WoW.CanCast("FerociousBite") && WoW.CurrentComboPoints >= 5 && WoW.TargetHasDebuff("Rip")) && WoW.TargetDebuffTimeRemaining("Rip") >= 500 && (WoW.Energy >= 50 || WoW.PlayerHasBuff("Incarnation") || WoW.PlayerHasBuff("Berserk")))
                    {
                        WoW.CastSpell("FerociousBite");
                        return;
                    }

                    if ((WoW.IsSpellInRange("Rake") && WoW.CanCast("Rake") && (!WoW.TargetHasDebuff("Rake") || WoW.TargetDebuffTimeRemaining("Rake") <= 300)) && (WoW.Energy >= 35 || WoW.PlayerHasBuff("Incarnation") || WoW.PlayerHasBuff("Berserk")))
                    {
                        WoW.CastSpell("Rake");
                        return;
                    }
                    if (WoW.IsSpellInRange("Ashamane") && WoW.CanCast("Ashamane") && !WoW.IsSpellOnCooldown("Ashamane") && WoW.CurrentComboPoints <= 2)
                    {
                        WoW.CastSpell("Ashamane");
                        return;
                    }

                    if ((WoW.IsSpellInRange("Rip") && WoW.CanCast("Rip") && WoW.CurrentComboPoints >= 5 && (!WoW.TargetHasDebuff("Rip") || WoW.TargetDebuffTimeRemaining("Rip") <= 500)) && (WoW.Energy >= 30 || WoW.PlayerHasBuff("Incarnation") || WoW.PlayerHasBuff("Berserk")))
                    {
                        WoW.CastSpell("Rip");
                        return;
                    }

                    if ((WoW.IsSpellInRange("Shred") && WoW.CanCast("Shred") && WoW.CurrentComboPoints < 5 && WoW.TargetHasDebuff("Rake") && (WoW.IsSpellOnCooldown("BrutalSlash") || !BrutalSlash) && (WoW.Energy >= 40 || WoW.PlayerHasBuff("Incarnation") || WoW.PlayerHasBuff("Berserk"))))
                    {
                        WoW.CastSpell("Shred");
                        return;
                    }
                    if (WoW.CanCast("TigersFury") && WoW.Energy <= 20 && !WoW.IsSpellOnCooldown("TigersFury"))
                    {
                        WoW.CastSpell("TigersFury");
                        return;
                    }
                    if (BrutalSlash && WoW.IsSpellInRange("Shred") && WoW.CanCast("BrutalSlash") && !WoW.IsSpellOnCooldown("BrutalSlash") && (WoW.Energy >= 20 || WoW.PlayerHasBuff("Incarnation") || WoW.PlayerHasBuff("Berserk")))
                    {
                        WoW.CastSpell("BrutalSlash");
                        return;
                    }
                }

                // OPENER
                if (WoW.IsInCombat && WoW.TargetIsEnemy && WoW.IsBoss && pullwatch.ElapsedMilliseconds < 10000 && UseCooldowns)
                {
                    if (Bloodtalons && WoW.CanCast("Regrowth"))
                    {
                        if (WoW.CurrentComboPoints >= 2 && !WoW.PlayerHasBuff("Savage Roar") && WoW.PlayerHasBuff("PredatorySwiftness"))
                        {
                            WoW.CastSpell("Regrowth");
                            return;
                        }
                        if (WoW.CurrentComboPoints >= 5 && WoW.PlayerHasBuff("PredatorySwiftness"))
                        {
                            WoW.CastSpell("Regrowth");
                            return;
                        }
                        if (WoW.PlayerBuffTimeRemaining("PredatorySwiftness") < 150 && WoW.PlayerHasBuff("PredatorySwiftness"))
                        {
                            WoW.CastSpell("Regrowth");
                            return;
                        }
                        if (WoW.CurrentComboPoints == 2 && !WoW.PlayerHasBuff("Bloodtalons") && WoW.SpellCooldownTimeRemaining("Ashamane") <= 100 && WoW.PlayerHasBuff("PredatorySwiftness"))
                        {
                            WoW.CastSpell("Regrowth");
                            return;
                        }
                        // If Elunes Guidance talent enabled
                        if (ElunesGuidance)
                        {
                            if (WoW.SpellCooldownTimeRemaining("ElunesGuidance") <= 100 && WoW.CurrentComboPoints == 0)
                            {
                                WoW.CastSpell("Regrowth");
                                return;
                            }
                            if (WoW.PlayerHasBuff("ElunesGuidance") && WoW.CurrentComboPoints >= 4)
                            {
                                WoW.CastSpell("Regrowth");
                                return;
                            }
                        }
                    }
                    if (SavageRoar && WoW.CanCast("SavageRoar") && WoW.CurrentComboPoints == 5 && !WoW.PlayerHasBuff("SavageRoar"))
                    {
                        WoW.CastSpell("SavageRoar");
                        return;
                    }
                    if (SavageRoar && WoW.CanCast("SavageRoar") && !WoW.PlayerHasBuff("SavageRoar") && WoW.CurrentComboPoints >= 2)
                    {
                        WoW.CastSpell("SavageRoar");
                        return;
                    }
                    if (WoW.CanCast("TigersFury") && WoW.Energy <= 30)
                    {
                        WoW.CastSpell("TigersFury");
                        return;
                    }
                    if (WoW.CanCast("TigersFury")
                        && WoW.PlayerHasBuff("SavageRoar"))
                    {
                        WoW.CastSpell("TigersFury");
                        return;
                    }
                    if (!Incarnation && WoW.CanCast("Berserk")
                        && WoW.PlayerHasBuff("SavageRoar"))
                    {
                        WoW.CastSpell("Berserk");
                        return;
                    }
                    if (Incarnation && WoW.CanCast("Incarnation")
                        && WoW.PlayerHasBuff("SavageRoar"))
                    {
                        WoW.CastSpell("Incarnation");
                        return;
                    }
                    if (WoW.CanCast("Rake") && !WoW.TargetHasDebuff("Rake"))
                    {
                        WoW.CastSpell("Rake");
                        return;
                    }
                    if (LunarInspiration && !WoW.TargetHasDebuff("Moonfire") && WoW.IsSpellInRange("SkullBash") && WoW.Energy >= 30 && WoW.CurrentComboPoints < 5)
                    {
                        WoW.CastSpell("Moonfire");
                        return;
                    }
                    {
                        WoW.CastSpell("Moonfire");
                        return;
                    }
                    if (WoW.CanCast("Ashamane") && WoW.TargetHasDebuff("Rip"))
                    {
                        WoW.CastSpell("Ashamane");
                        return;
                    }
                    if (WoW.CanCast("Rip") && WoW.PlayerHasBuff("SavageRoar") && WoW.CurrentComboPoints == 5)
                    {
                        WoW.CastSpell("Rip");
                        return;
                    }
                    if (WoW.CanCast("Shred") && WoW.CurrentComboPoints < 5)
                    {
                        WoW.CastSpell("Shred");
                        return;
                    }
                }

                // OPEN COMBAT WITH SHADOWMELD RAKE
                if (WoW.TargetIsEnemy && WoW.IsInCombat && (WoW.PlayerHasBuff("Prowl") || WoW.PlayerHasBuff("Shadowmeld")))
                {
                    if (WoW.IsSpellInRange("Rake") && WoW.CanCast("Rake"))
                    {
                        WoW.CastSpell("Rake");
                        return;
                    }
                }

                // COOLDOWN ROTATION
                if (WoW.IsInCombat && WoW.TargetIsEnemy && UseCooldowns)
                {
                    if (!Incarnation && WoW.CanCast("Berserk") && WoW.PlayerHasBuff("TigersFury") || WoW.SpellCooldownTimeRemaining("TigersFury") < 200)
                    {
                        WoW.CastSpell("Berserk");
                        return;
                    }
                    if (Incarnation && WoW.CanCast("Incarnation") && WoW.PlayerHasBuff("TigersFury") || WoW.SpellCooldownTimeRemaining("TigersFury") < 200)
                    {
                        WoW.CastSpell("Incarnation");
                        return;
                    }
                }
       
                // PRIMARY ROTATION
                if (WoW.IsInCombat && WoW.TargetIsEnemy && !WoW.PlayerHasBuff("Prowl") && WoW.PlayerHasBuff("Cat Form"))
                {
                    // Tigers Fury on cooldown if under 30 Energy
                    if (WoW.CanCast("TigersFury") && WoW.Energy <= 30)
                    {
                        WoW.CastSpell("TigersFury");
                        return;
                    }
                    // Keep Rip from falling off during execute range
                    if (WoW.IsSpellInRange("Rake") && WoW.CanCast("FerociousBite") && WoW.TargetHasDebuff("Rip") && WoW.TargetHasDebuff("Rip") && WoW.PlayerHasBuff("SavageRoar") && WoW.TargetDebuffTimeRemaining("Rip") < 3 && WoW.TargetHealthPercent <= 25 && WoW.Energy >= 25)
                    {
                        WoW.CastSpell("FerociousBite");
                        return;
                    }
                    // Regrowth logic with Bloodtalons talent enabled
                    if (Bloodtalons && WoW.CanCast("Regrowth")) 
                    {
                        if (WoW.CurrentComboPoints >= 5 && WoW.PlayerHasBuff("PredatorySwiftness"))
                        {
                            WoW.CastSpell("Regrowth");
                            return;
                        }
                        if (WoW.PlayerBuffTimeRemaining("PredatorySwiftness") < 1500 && WoW.PlayerHasBuff("PredatorySwiftness"))
                        {
                            WoW.CastSpell("Regrowth");
                            return;
                        }
                        if (WoW.CurrentComboPoints == 2 && !WoW.PlayerHasBuff("Bloodtalons") && WoW.SpellCooldownTimeRemaining("Ashamane") <= 100 && WoW.PlayerHasBuff("PredatorySwiftness"))
                        {
                            WoW.CastSpell("Regrowth");
                            return;
                        }
                        // If Elunes Guidance talent enabled
                        if (ElunesGuidance)
                        {
                            if (WoW.SpellCooldownTimeRemaining("ElunesGuidance") <= 100 && WoW.CurrentComboPoints == 0)
                            {
                                WoW.CastSpell("Regrowth");
                                return;
                            }
                            if (WoW.PlayerHasBuff("ElunesGuidance") && WoW.CurrentComboPoints >= 4)
                            {
                                WoW.CastSpell("Regrowth");
                                return;
                            }
                        }
                    }
                    // Sabertooth Opener
                    //if (Sabertooth)
                    //{

                    //}
           
                    // LEGENDARY LOGIC
                    if (AiluroPouncers)
                    {
                        if (Bloodtalons && WoW.CanCast("Regrowth") && WoW.PlayerBuffStacks("PredatorySwiftness") > 1 && !WoW.PlayerHasBuff("Bloodtalons") && WoW.PlayerHasBuff("PredatorySwiftness"))
                        {
                            WoW.CastSpell("Regrowth");
                            return;
                        }
                    }
                    // FINISHER LOGIC
                    // FINISHER LOGIC
                    // If have Savage Roar and Rip under 3 seconds remaining and target above 25% hp, cast BUFFED rip.
                    //if (WoW.IsSpellInRange("Rip") && WoW.CanCast("Rip") && WoW.Energy >= 30 && WoW.TargetHasDebuff("Rip") && WoW.TargetDebuffTimeRemaining("Rip") < 2 && WoW.PlayerHasBuff("SavageRoar") && WoW.PlayerBuffTimeRemaining("SavageRoar") < 8 && WoW.TargetHealthPercent > 25 && WoW.CurrentComboPoints == 5 && WoW.PlayerHasBuff("TigersFury"))
                    //{
                    //    WoW.CastSpell("Rip");
                    //    ripbuffed.Reset();
                    //    ripbuffed.Start();
                    //    ripunbuffed.Reset();
                    //    Log.Write("Override Savage Roar refresh when Rip under 3seconds remaining.", Color.Red);
                    //    return;
                    //}
                    ////
                    //if (WoW.IsSpellInRange("Rip") && WoW.CanCast("Rip") && WoW.Energy >= 30 && WoW.TargetHasDebuff("Rip") && WoW.TargetDebuffTimeRemaining("Rip") < 2 && WoW.PlayerHasBuff("SavageRoar") && WoW.PlayerBuffTimeRemaining("SavageRoar") < 8 && WoW.TargetHealthPercent > 25 && WoW.CurrentComboPoints == 5 && WoW.PlayerHasBuff("TigersFury"))
                    //{
                    //    WoW.CastSpell("Rip");
                    //    ripbuffed.Reset();
                    //    ripbuffed.Start();
                    //    ripunbuffed.Reset();
                    //    Log.Write("Override Savage Roar refresh when Rip under 3seconds remaining.", Color.Red);
                    //    return;
                    //}
                    // Savage Roar if player dont have buff Savage Roar and is at 5 combo points.
                    if (SavageRoar && WoW.CanCast("SavageRoar") && WoW.Energy >= 40 && !WoW.PlayerHasBuff("SavageRoar") && WoW.CurrentComboPoints == 5)
                    {
                        WoW.CastSpell("SavageRoar");
                        return;
                    }
                    // Savage Roar if player have buff Savage Roar and is under 2 seconds remaining and is at 5 combo points.
                    if (SavageRoar && WoW.CanCast("SavageRoar") && WoW.Energy >= 40 && WoW.PlayerBuffTimeRemaining("SavageRoar") <= 200 && WoW.PlayerHasBuff("SavageRoar") && WoW.CurrentComboPoints == 5)
                    {
                        WoW.CastSpell("SavageRoar");
                        return;
                    }
                    // Ferocious Bite if HP under 25% and Rip Remaining under 3 seconds. (will extend Rip)
                    if (WoW.IsSpellInRange("FerociousBite") && WoW.CanCast("FerociousBite") && WoW.CurrentComboPoints > 1 && WoW.TargetDebuffTimeRemaining("Rip") < 300 && WoW.TargetHealthPercent < 25 && WoW.TargetHasDebuff("Rip"))
                    {
                        WoW.CastSpell("FerociousBite");
                        return;
                    }
                    // If TigersFury running.
                    if (WoW.IsSpellInRange("FerociousBite") && WoW.CanCast("FerociousBite") && WoW.CurrentComboPoints == 5 && ripbuffed.IsRunning && WoW.TargetHasBuff("Rip") && WoW.PlayerHasBuff("SavageRoar") && WoW.PlayerBuffTimeRemaining("SavageRoar") <= 1000 && WoW.TargetDebuffTimeRemaining("Rip") > 7)
                    {
                        WoW.CastSpell("FerociousBite");
                        Log.Write("O mighty super bite.");
                    }
                    // Refresh Rip if not on target and tigersfury or berserk
                    if (WoW.IsSpellInRange("Rip") && WoW.CanCast("Rip") && WoW.Energy >= 30 && !WoW.TargetHasDebuff("Rip") && WoW.CurrentComboPoints == 5 && WoW.PlayerHasBuff("TigersFury"))
                    {
                        WoW.CastSpell("Rip");
                        ripbuffed.Start();
                        ripunbuffed.Reset();
                        Log.Write("Buffed Rip. Berserk or Tigers Fury buff.", Color.Red);
                        return;
                    }
                    // Refresh Rip for stronger Rip
                    if (WoW.IsSpellInRange("Rip") && WoW.CanCast("Rip") && WoW.Energy >= 30 && ripunbuffed.IsRunning && WoW.PlayerHasBuff("TigersFury") && WoW.CurrentComboPoints == 5)
                    {
                        WoW.CastSpell("Rip");
                        ripbuffed.Start();
                        ripunbuffed.Reset();
                        Log.Write("Buffed Rip. Berserk or Tigers Fury buff.", Color.Red);
                        return;
                    }
                    // Refresh Rip at 8 seconds if have Tigers fury or Berserk buff.
                    if (WoW.IsSpellInRange("Rip") && WoW.CanCast("Rip") && WoW.Energy >= 30 && !Sabertooth && WoW.TargetDebuffTimeRemaining("Rip") < 800 && WoW.TargetHealthPercent > 25 && WoW.CurrentComboPoints == 5 && WoW.PlayerHasBuff("TigersFury"))
                    {
                        WoW.CastSpell("Rip");
                        ripbuffed.Start();
                        ripunbuffed.Reset();
                        Log.Write("Buffed Rip. Berserk or Tigers Fury buff.", Color.Red);
                        return;
                    }
                    // Refresh Rip at 8 seconds if no buff and no sabertooth talent and non-buffed rip is running.
                    if (WoW.IsSpellInRange("Rip") && WoW.CanCast("Rip") && WoW.Energy >= 30 && !Sabertooth && WoW.TargetDebuffTimeRemaining("Rip") < 800 && WoW.TargetHealthPercent > 25 && WoW.CurrentComboPoints == 5 && !ripbuffed.IsRunning)
                    {
                        WoW.CastSpell("Rip");
                        ripunbuffed.Start();
                        ripbuffed.Reset();
                        Log.Write("Unbuffed Rip. Under 8 secs remaining on Unbuffed Rip.", Color.Red);
                        return;
                    }
                    // Refresh Rip if not on target.
                    if (WoW.IsSpellInRange("Rip") && WoW.CanCast("Rip") && WoW.Energy >= 30 && !WoW.TargetHasDebuff("Rip") && WoW.CurrentComboPoints == 5)
                    {
                        WoW.CastSpell("Rip");
                        ripunbuffed.Start();
                        ripbuffed.Reset();
                        Log.Write("Unbuffed Rip. No Rip on target.", Color.Red);
                        return;
                    }
                    // Refresh Rip if under 2 seconds remaining.
                    if (WoW.IsSpellInRange("Rip") && WoW.CanCast("Rip") && WoW.Energy >= 30 && WoW.TargetDebuffTimeRemaining("Rip") < 200 && WoW.CurrentComboPoints == 5)
                    {
                        WoW.CastSpell("Rip");
                        ripunbuffed.Start();
                        ripbuffed.Reset();
                        Log.Write("Unbuffed Rip. No Rip under 2secs remaining.", Color.Red);
                        return;
                    }
                    // Refresh Savage roar at under 7.2 secons remaining if no JaggedWounds talent
                    if (SavageRoar && WoW.CanCast("SavageRoar") && WoW.Energy >= 40 && !JaggedWounds && WoW.PlayerBuffTimeRemaining("SavageRoar") < 720 && WoW.CurrentComboPoints == 5)
                    {
                        WoW.CastSpell("SavageRoar");
                        return;
                    }
                    // Refresh Savage Roar early with jagged wounds talent
                    if (SavageRoar && WoW.CanCast("SavageRoar") && WoW.Energy >= 40 && JaggedWounds && WoW.PlayerBuffTimeRemaining("SavageRoar") < 1050 && WoW.CurrentComboPoints == 5)
                    {
                        WoW.CastSpell("SavageRoar");
                        return;
                    }
                    // FB cast without JaggedWounds talent
                    if (WoW.IsSpellInRange("FerociousBite") && WoW.CanCast("FerociousBite") && WoW.Energy >= 25 && WoW.CurrentComboPoints == 5 && !JaggedWounds && WoW.TargetHasDebuff("Rip") && WoW.PlayerHasBuff("SavageRoar") && WoW.PlayerBuffTimeRemaining("SavageRoar") > 920 && WoW.TargetDebuffTimeRemaining("Rip") > 1000)
                    {
                        WoW.CastSpell("FerociousBite");
                        return;
                    }
                    // FB cast with JaggedWounds talent
                    if (WoW.IsSpellInRange("FerociousBite") && WoW.CanCast("FerociousBite") && WoW.Energy >= 25 && WoW.CurrentComboPoints == 5 && JaggedWounds && WoW.TargetHasDebuff("Rip") && WoW.PlayerHasBuff("SavageRoar") && WoW.PlayerBuffTimeRemaining("SavageRoar") > 1250 && WoW.TargetDebuffTimeRemaining("Rip") > 1000)
                    {
                        WoW.CastSpell("FerociousBite");
                        return;
                    }
                    // GENERATOR LOGIC
                    // GENERATOR LOGIC
                    if (WoW.IsSpellInRange("Ashamane") && WoW.CanCast("Ashamane") && WoW.CurrentComboPoints <= 2 && !ElunesGuidance)
                    {
                        WoW.CastSpell("Ashamane");
                        return;
                    }
                    if (ElunesGuidance && WoW.CanCast("ElunesGuidance") && WoW.CurrentComboPoints == 0 && WoW.Energy >= 30)
                    {
                        
                        WoW.CastSpell("ElunesGuidance");
                        return;
                    }
                    if (NightElf && WoW.CanCast("Shadowmeld") && WoW.CurrentComboPoints < 5 && WoW.Energy >= 35 && WoW.PlayerHasBuff("TigersFury"))
                    {
                        WoW.CastSpell("Shadowmeld");
                        return;
                    }
                    if (WoW.IsSpellInRange("Rake") && WoW.CanCast("Rake") && WoW.Energy >= 35)
                    {
                        // TODO : Code in bleed multiplier
                        if (WoW.CurrentComboPoints < 5 && Bloodtalons && WoW.PlayerHasBuff("Bloodtalons") && WoW.TargetDebuffTimeRemaining("Rake") <= 500)
                        {
                            WoW.CastSpell("Rake");
                            return;
                        }
                        if (WoW.CurrentComboPoints < 5 && !WoW.TargetHasDebuff("Rake"))
                        {
                            WoW.CastSpell("Rake");
                            return;
                        }
                        if (WoW.CurrentComboPoints < 5 && WoW.TargetDebuffTimeRemaining("Rake") <= 300)
                        {
                            WoW.CastSpell("Rake");
                            return;
                        }
                    }
                    if (LunarInspiration && !WoW.TargetHasDebuff("Moonfire") && WoW.IsSpellInRange("SkullBash") && WoW.Energy >= 30 && WoW.CurrentComboPoints < 5)
                    {
                        WoW.CastSpell("Moonfire");
                        return;
                    }
                    //if (LunarInspiration && WoW.IsSpellInRange("Moonfire") && WoW.CanCast("Moonfire") && WoW.Energy >= 30)
                    //{
                    //    if (WoW.CurrentComboPoints < 5 && WoW.TargetDebuffTimeRemaining("Moonfire") <= 4.2)
                    //    {
                    //        WoW.CastSpell("Moonfire");
                    //        return;
                    //    }
                    //    if (WoW.CurrentComboPoints < 5 && !WoW.TargetHasDebuff("Moonfire"))
                    //    {
                    //        WoW.CastSpell("Moonfire");
                    //        return;
                    //    }
                    //}
                    if (BrutalSlash && WoW.IsSpellInRange("Rake") && WoW.CanCast("BrutalSlash") && WoW.PlayerSpellCharges("BrutalSlash") == 3 && WoW.Energy >= 20)
                    {
                        WoW.CastSpell("BrutalSlash");
                        return;
                    }
                    if (WoW.IsSpellInRange("Shred") && WoW.CanCast("Shred") && WoW.CurrentComboPoints < 5 && WoW.Energy >= 40)
                    {
                        WoW.CastSpell("Shred");
                        return;
                    }                
                }
            }

            //  AOE ROTATION
            if (combatRoutine.Type == RotationType.AOE)
            {
                // In combat, target is enemy, target is not player, i dont have prowl and i have buff catform
                if (WoW.IsInCombat && WoW.TargetIsEnemy && !WoW.TargetIsPlayer && !WoW.PlayerHasBuff("Prowl") && WoW.PlayerHasBuff("Cat Form"))
                {
                    if (WoW.IsSpellInRange("Rake") && WoW.CanCast("Rake") && !WoW.TargetHasDebuff("Rake") && WoW.Energy >= 35)
                    {
                        WoW.CastSpell("Rake");
                        return;
                    }
                    if (WoW.IsSpellInRange("Rake") && WoW.CanCast("Thrash") && !WoW.TargetHasDebuff("Thrash") && WoW.Energy >= 50)
                    {
                        WoW.CastSpell("Thrash");
                        return;
                    }
                    if (WoW.IsSpellInRange("Rake") && WoW.CanCast("Swipe") && WoW.TargetHasDebuff("Thrash") && WoW.CurrentComboPoints < 5 && WoW.Energy >= 45)
                    {
                        WoW.CastSpell("Swipe");
                        return;
                    }
                    if (SavageRoar && WoW.CanCast("Savage Roar") && WoW.CurrentComboPoints == 5 && !WoW.PlayerHasBuff("Savage Roar") && WoW.Energy >= 40)
                    {
                        WoW.CastSpell("SavageRoar");
                        return;
                    }
                    if (WoW.IsSpellInRange("Rip") && WoW.CanCast("Rip") && WoW.CurrentComboPoints == 5 && !WoW.TargetHasDebuff("Rip") && WoW.Energy >= 30)
                    {
                        WoW.CastSpell("Rip");
                        return;
                    }
                }
            }
        }

        public override Form SettingsForm { get; set; }
    }
}

/*
[AddonDetails.db]
AddonAuthor=Dani
AddonName=HideOrderHallBar
WoWVersion=Legion - 70000
[SpellBook.db]
Spell,768,Cat Form,D7
Spell,1079,Rip,F8
Spell,1822,Rake,D1
Spell,5215,Prowl,Q
Spell,5217,TigersFury,F10
Spell,5221,Shred,D2
Spell,8936,Regrowth,R
Spell,22568,FerociousBite,D8
Spell,52610,SavageRoar,D4
Spell,58984,Shadowmeld,F12
Spell,106830,Thrash,D3
Spell,106785,Swipe,F9
Spell,155625,Moonfire,G
Spell,202028,BrutalSlash,F9
Spell,210722,Ashamane,Z
Spell,106951,Berserk,D9
Spell,102543,Incarnation,D9
Spell,202060,ElunesGuidance,Z
Spell,106839,SkullBash,E
Aura,5215,Prowl
Aura,768,Cat Form
Aura,1079,Rip
Aura,5217,TigersFury
Aura,52610,SavageRoar
Aura,58984,Shadowmeld
Aura,69369,PredatorySwiftness
Aura,106830,Thrash
Aura,106951,Berserk
Aura,145152,Bloodtalons
Aura,155580,LunarinspirationTalent
Aura,155672,BloodtalonsTalent
Aura,155722,Rake
Aura,202031,Sabertooth
Aura,202032,JaggedwoundsTalent
Aura,155625,Moonfire
*/
