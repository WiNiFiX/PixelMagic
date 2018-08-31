//Changelog
// v2.0 fmflex rotation
// v2.1 fixed for latest pm, removed kick settings, removed double reload
// v2.2 added hotkey for Sindragosa's Fury
// v2.3 added support for Legendary Ring
// v2.4 Auto Talent detection added
// V2.5 Bugfixes
// V2.6 added Levelcheck, so ppl can use rota for leveling

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using PixelMagic.Helpers;
using System.Threading;

namespace PixelMagic.Rotation
{
    public class DKFrostMGSmartie : CombatRoutine
    {

        private bool AddonEdited = false;

        private bool haveCoF = true;
        private bool haveHRW = true;

        private static readonly Stopwatch coolDownStopWatch = new Stopwatch();
        private int currentRunes;
        private bool hasBreath;
        private bool useNextHRWCharge = false;
		private bool legyringtest = true;

        private bool isMelee;
        private int runicPower;
        private bool useChainofIce = false;

        public override string Name
        {
            get { return "Frost DK"; }
        }

        public override string Class
        {
            get { return "Deathknight"; }
        }

        public override Form SettingsForm { get; set; }
        public SettingsFormDFF SettingsFormDFF { get; set; }

        public static int cooldownKey
        {
            get
            {
                var cooldownKey = ConfigFile.ReadValue("DKFrost", "cooldownKey").Trim();
                if (cooldownKey != "")
                {
                    return Convert.ToInt32(cooldownKey);
                }

                return -1;
            }
            set { ConfigFile.WriteValue("DkFrost", "cooldownKey", value.ToString()); }
        }

        public static int cooldownModifier
        {
            get
            {
                var cooldownModifier = ConfigFile.ReadValue("DKFrost", "cooldownModifier").Trim();
                if (cooldownModifier != "")
                {
                    return Convert.ToInt32(cooldownModifier);
                }

                return -1;
            }
            set { ConfigFile.WriteValue("DkFrost", "cooldownModifier", value.ToString()); }
        }

        public static string cooldownHotKeyString
        {
            get
            {
                var cooldownHotKeyString = ConfigFile.ReadValue("DkFrost", "cooldownHotKeyString").Trim();

                if (cooldownHotKeyString != "")
                {
                    return cooldownHotKeyString;
                }

                return "Click to Set";
            }
            set { ConfigFile.WriteValue("DkFrost", "cooldownHotKeyString", value); }
        }

        public static bool isCheckHotkeysFrostIceboundFortitude
        {
            get
            {
                var isCheckHotkeysFrostIceboundFortitude = ConfigFile.ReadValue("DkFrost", "isCheckHotkeysFrostIceboundFortitude").Trim();

                if (isCheckHotkeysFrostIceboundFortitude != "")
                {
                    return Convert.ToBoolean(isCheckHotkeysFrostIceboundFortitude);
                }

                return true;
            }
            set { ConfigFile.WriteValue("DkFrost", "isCheckHotkeysFrostIceboundFortitude", value.ToString()); }
        }

        public static bool isCheckHotkeyslegyring
        {
            get
            {
                var isCheckHotkeyslegyring = ConfigFile.ReadValue("DkFrost", "isCheckHotkeyslegyring").Trim();

                if (isCheckHotkeyslegyring != "")
                {
                    return Convert.ToBoolean(isCheckHotkeyslegyring);
                }

                return true;
            }
            set { ConfigFile.WriteValue("DkFrost", "isCheckHotkeyslegyring", value.ToString()); }
        }

        public static int FrostIceboundHPPercent
        {
            get
            {
                var FrostIceboundHPPercent = ConfigFile.ReadValue("DKFrost", "FrostIceboundHPPercent").Trim();
                if (FrostIceboundHPPercent != "")
                {
                    return Convert.ToInt32(FrostIceboundHPPercent);
                }

                return -1;
            }
            set { ConfigFile.WriteValue("DkFrost", "FrostIceboundHPPercent", value.ToString()); }
        }

        public static bool isCheckHotkeysFrostAntiMagicShield
        {
            get
            {
                var isCheckHotkeysFrostAntiMagicShield = ConfigFile.ReadValue("DkFrost", "isCheckHotkeysFrostAntiMagicShield").Trim();

                if (isCheckHotkeysFrostAntiMagicShield != "")
                {
                    return Convert.ToBoolean(isCheckHotkeysFrostAntiMagicShield);
                }

                return true;
            }
            set { ConfigFile.WriteValue("DkFrost", "isCheckHotkeysFrostAntiMagicShield", value.ToString()); }
        }

        public static int FrostAMSHPPercent
        {
            get
            {
                var FrostAMSHPPercent = ConfigFile.ReadValue("DKFrost", "FrostAMSHPPercent").Trim();
                if (FrostAMSHPPercent != "")
                {
                    return Convert.ToInt32(FrostAMSHPPercent);
                }

                return -1;
            }
            set { ConfigFile.WriteValue("DkFrost", "FrostAMSHPPercent", value.ToString()); }
        }

        public static bool isCheckHotkeysFrostOffensiveErW
        {
            get
            {
                var isCheckHotkeysFrostOffensiveErW = ConfigFile.ReadValue("DkFrost", "isCheckHotkeysFrostOffensiveErW").Trim();

                if (isCheckHotkeysFrostOffensiveErW != "")
                {
                    return Convert.ToBoolean(isCheckHotkeysFrostOffensiveErW);
                }

                return true;
            }
            set { ConfigFile.WriteValue("DkFrost", "isCheckHotkeysFrostOffensiveErW", value.ToString()); }
        }

        public static bool isCheckHotkeysFrostOffensivePillarofFrost
        {
            get
            {
                var isCheckHotkeysFrostOffensivePillarofFrost = ConfigFile.ReadValue("DkFrost", "isCheckHotkeysFrostOffensivePillarofFrost").Trim();

                if (isCheckHotkeysFrostOffensivePillarofFrost != "")
                {
                    return Convert.ToBoolean(isCheckHotkeysFrostOffensivePillarofFrost);
                }

                return true;
            }
            set { ConfigFile.WriteValue("DkFrost", "isCheckHotkeysFrostOffensivePillarofFrost", value.ToString()); }
        }

        public override void Initialize()
        {
            Log.Write("Welcome to the Frost DK v. 2.6", Color.Green);
	        Log.Write("All Talents supported and auto detected", Color.Green);		
			Log.Write("Hold down Z key (Y for US) for Sindragosa's Fury", Color.Red);
            SettingsFormDFF = new SettingsFormDFF();
            SettingsForm = SettingsFormDFF;

            SettingsFormDFF.btnHotkeysFrostOffensiveCooldowns.Text = cooldownHotKeyString;

            SettingsFormDFF.checkHotkeysFrostOffensiveErW.Checked = isCheckHotkeysFrostOffensiveErW;
            SettingsFormDFF.checkHotkeysFrostOffensivePillarofFrost.Checked = isCheckHotkeysFrostOffensivePillarofFrost;
            SettingsFormDFF.checkHotkeysFrostAntiMagicShield.Checked = isCheckHotkeysFrostAntiMagicShield;
            SettingsFormDFF.checkHotkeysFrostIceboundFortitude.Checked = isCheckHotkeysFrostIceboundFortitude;
            SettingsFormDFF.checkHotkeysFrostIFPercent.Text = FrostIceboundHPPercent.ToString();
            SettingsFormDFF.checkHotkeysFrostAMSPercent.Text = FrostAMSHPPercent.ToString();

			try
			{
			}
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            SettingsFormDFF.checkHotkeysFrostIceboundFortitude.CheckedChanged += isCheckHotkeysFrostIceboundFortitude_Click;
            SettingsFormDFF.checkHotkeysFrostIFPercent.TextChanged += isCheckHotkeysFrostIFPercent_Click;
            SettingsFormDFF.checkHotkeysFrostAntiMagicShield.CheckedChanged += isCheckHotkeysFrostAntiMagicShield_Click;
            SettingsFormDFF.checkHotkeysFrostAMSPercent.TextChanged += isCheckHotkeysFrostAMSPercent_Click;
            SettingsFormDFF.checkHotkeysFrostOffensivePillarofFrost.CheckedChanged += isCheckHotkeysFrostOffensivePillarofFrost_Click;
            SettingsFormDFF.checkHotkeysFrostOffensiveErW.CheckedChanged += isCheckHotkeysFrostOffensiveErW_Click;
            SettingsFormDFF.btnHotkeysFrostOffensiveCooldowns.KeyDown += KeyDown;
        }

        private void KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Menu || e.KeyCode == Keys.ControlKey || e.KeyCode == Keys.ShiftKey)
                return;
            SettingsFormDFF.btnHotkeysFrostOffensiveCooldowns.Text = "Hotkey : ";
            if (e.Shift)
            {
                cooldownModifier = (int)Keys.ShiftKey;
                SettingsFormDFF.btnHotkeysFrostOffensiveCooldowns.Text += Keys.Shift + " + ";
            }
            else if (e.Alt)
            {
                cooldownModifier = (int)Keys.Menu;
                SettingsFormDFF.btnHotkeysFrostOffensiveCooldowns.Text += Keys.Alt + " + ";
            }
            else if (e.Control)
            {
                cooldownModifier = (int)Keys.ControlKey;
                SettingsFormDFF.btnHotkeysFrostOffensiveCooldowns.Text += Keys.Control + " + ";
            }
            else cooldownModifier = -1;
            cooldownKey = (int)e.KeyCode;
            SettingsFormDFF.btnHotkeysFrostOffensiveCooldowns.Text += e.KeyCode;
            cooldownHotKeyString = SettingsFormDFF.btnHotkeysFrostOffensiveCooldowns.Text;
            SettingsFormDFF.checkHotkeysFrostIFPercentLabel.Focus();
        }

        private void isCheckHotkeysFrostIceboundFortitude_Click(object sender, EventArgs e)
        {
            isCheckHotkeysFrostIceboundFortitude = SettingsFormDFF.checkHotkeysFrostIceboundFortitude.Checked;
        }
		

        private void isCheckHotkeysFrostIFPercent_Click(object sender, EventArgs e)
        {
            int userVal;
            if (int.TryParse(SettingsFormDFF.checkHotkeysFrostIFPercent.Text, out userVal) && userVal >= 0 && userVal <= 100)
            {
                FrostIceboundHPPercent = userVal;
            }
            else
            {
                SettingsFormDFF.checkHotkeysFrostIFPercent.Text = "";
                Log.Write("Enter a number between 0 and 100 in the text box", Color.DarkRed);
            }
        }

        private void isCheckHotkeysFrostAntiMagicShield_Click(object sender, EventArgs e)
        {
            isCheckHotkeysFrostAntiMagicShield = SettingsFormDFF.checkHotkeysFrostAntiMagicShield.Checked;
        }

        private void isCheckHotkeysFrostAMSPercent_Click(object sender, EventArgs e)
        {
            int userVal;
            if (int.TryParse(SettingsFormDFF.checkHotkeysFrostAMSPercent.Text, out userVal) && userVal >= 0 && userVal <= 100)
            {
                FrostAMSHPPercent = userVal;
            }
            else
            {
                SettingsFormDFF.checkHotkeysFrostAMSPercent.Text = "";
                Log.Write("Enter a number between 0 and 100 in the text box", Color.DarkRed);
            }
        }

        private void isCheckHotkeysFrostOffensivePillarofFrost_Click(object sender, EventArgs e)
        {
            isCheckHotkeysFrostOffensivePillarofFrost = SettingsFormDFF.checkHotkeysFrostOffensivePillarofFrost.Checked;
        }

        private void isCheckHotkeysFrostOffensiveErW_Click(object sender, EventArgs e)
        {
            isCheckHotkeysFrostOffensiveErW = SettingsFormDFF.checkHotkeysFrostOffensiveErW.Checked;
        }


        public override void Stop()
        {
        }


        public static bool CanCastInRange(string spell)
        {
            return WoW.CanCast(spell, false, false, true, false, false);
        }

        public static bool CanCastNoRange(string spell)
        {
            return !WoW.IsSpellOnCooldown(spell);
        }

        public override void Pulse()
        {
            if (!coolDownStopWatch.IsRunning || coolDownStopWatch.ElapsedMilliseconds > 60000)
                coolDownStopWatch.Restart();
            if (DetectKeyPress.GetKeyState(cooldownKey) < 0 && (cooldownModifier == -1 || cooldownModifier != -1 && DetectKeyPress.GetKeyState(cooldownModifier) < 0))
            {
                if (coolDownStopWatch.ElapsedMilliseconds > 1000)
                {
                    combatRoutine.UseCooldowns = !combatRoutine.UseCooldowns;
                    coolDownStopWatch.Restart();
                }
            }
            if (WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsInCombat && WoW.TargetIsVisible)
            {
                isMelee = WoW.CanCast("Obliterate", false, false, true, false, false);
                currentRunes = WoW.CurrentRunes;
                runicPower = WoW.RunicPower;
                if (WoW.TargetIsCasting && WoW.TargetIsCastingAndSpellIsInterruptible && WoW.TargetPercentCast >= 40 && WoW.Level >= 62 && WoW.CanCast("Mind Freeze", false, true, true, false, false) &&
                    isCastingListedSpell())
                {
                    WoW.CastSpell("Mind Freeze");
                }
                if (WoW.CanCast("Sindragosa's Fury") && (DetectKeyPress.GetKeyState(0x5A) < 0) && WoW.Level >= 110)
                {																
                    WoW.CastSpell("Sindragosa's Fury");
                    return;
                }
                if (CanCastNoRange("Anti-Magic Shell") && WoW.HealthPercent <= FrostAMSHPPercent && !WoW.IsSpellOnCooldown("Anti-Magic Shell") && isCheckHotkeysFrostIceboundFortitude && WoW.Level >= 57)
                {
                    WoW.CastSpell("Anti-Magic Shell");
                }
                if (CanCastNoRange("Icebound Fortitude") && WoW.HealthPercent < FrostIceboundHPPercent && !WoW.IsSpellOnCooldown("Icebound Fortitude") && isCheckHotkeysFrostAntiMagicShield && WoW.Level >= 65)
                {
                    WoW.CastSpell("Icebound Fortitude");
                }
                if (useChainofIce && CanCastInRange("ChainofIce") && !isMelee && !WoW.TargetHasDebuff("ChainofIce") && currentRunes >= 1 && WoW.Level >= 63)
                {
                    WoW.CastSpell("ChainofIce");
                    return;
                }
                if (WoW.Talent(7) == 2)
                {
                    BreathRotation();
                }
                else
                    MGRotation();
            }
        }

        public void BreathRotation()
        {
            hasBreath = WoW.PlayerHasBuff("Breath");
            if (combatRoutine.UseCooldowns && isCheckHotkeysFrostOffensivePillarofFrost && !WoW.IsSpellOnCooldown("PillarofFrost") && hasBreath && WoW.Level >= 57)
            {
                WoW.CastSpell("PillarofFrost");
            }
            if (combatRoutine.UseCooldowns && isCheckHotkeysFrostOffensivePillarofFrost && !WoW.IsSpellOnCooldown("PillarofFrost") && !hasBreath && WoW.SpellCooldownTimeRemaining("Breath") >= 5900 && WoW.Level >= 57)
            {
                WoW.CastSpell("PillarofFrost");
            }
            if ((haveCoF || useNextHRWCharge) && isCheckHotkeyslegyring && haveHRW && runicPower <= 30 && isCheckHotkeysFrostOffensiveErW && combatRoutine.UseCooldowns && !WoW.PlayerHasBuff("HEmpower Rune") && !WoW.IsSpellOnCooldown("HEmpower Rune") && hasBreath && legyringtest == true && WoW.Level >= 58)
            {
                useNextHRWCharge = false;
                WoW.CastSpell("HEmpower Rune");
				legyringtest = false;
            }
            if ((haveCoF || useNextHRWCharge) && !isCheckHotkeyslegyring && haveHRW && runicPower <= 30 && isCheckHotkeysFrostOffensiveErW && combatRoutine.UseCooldowns && !WoW.PlayerHasBuff("HEmpower Rune") && !WoW.IsSpellOnCooldown("HEmpower Rune") && hasBreath && WoW.Level >= 58)
            {
                useNextHRWCharge = false;
                WoW.CastSpell("HEmpower Rune");
            }
			if (isCheckHotkeyslegyring && !WoW.PlayerHasBuff("Breath"))
			{
				legyringtest = true;
			}
            if ((haveCoF || useNextHRWCharge) && !haveHRW && runicPower <= 50 && currentRunes <=1 && isCheckHotkeysFrostOffensiveErW && combatRoutine.UseCooldowns && !WoW.IsSpellOnCooldown("HEmpower Rune") && hasBreath && WoW.Level >= 57)
            {
                WoW.CastSpell("Empower Rune");
            }
            if (combatRoutine.UseCooldowns && isMelee && currentRunes >= 2 && runicPower >= 70 && CanCastNoRange("Breath") && WoW.Level >= 100)
            {
                WoW.CastSpell("Breath");
                useNextHRWCharge = true;
                return;
            }
            if (combatRoutine.UseCooldowns && isMelee && runicPower >= 70 && CanCastNoRange("Breath"))
            {
                return;
            }
            if (!WoW.TargetHasDebuff("Frost Fever") && currentRunes >= 1 && !hasBreath && CanCastInRange("Howling Blast") && !WoW.IsSpellOnCooldown("Howling Blast") && WoW.Level >= 55)
            {
                WoW.CastSpell("Howling Blast");
                return;
            }
            if (isMelee && currentRunes >= 1 && WoW.Level >= 57 && ((runicPower >= 48 && hasBreath) || !hasBreath) && (!combatRoutine.UseCooldowns || (combatRoutine.UseCooldowns && WoW.SpellCooldownTimeRemaining("Breath") >= 1500)) && CanCastNoRange("Remorseless Winter"))
            {
                WoW.CastSpell("Remorseless Winter");
                return;
            }
            if (((runicPower >= 46 && hasBreath) || !hasBreath) && CanCastInRange("Howling Blast") && WoW.PlayerHasBuff("Rime") && WoW.Level >= 55)
            {
                WoW.CastSpell("Howling Blast");
                return;
            }
            if (WoW.Talent(6) != 1 && isMelee && currentRunes >= 2 && !hasBreath && WoW.PlayerHasBuff("Gathering Storm") && WoW.Level >= 55)
            {
                WoW.CastSpell("Obliterate");
                return;
            }
            if (runicPower >= 70 && !hasBreath && CanCastInRange("Frost Strike"))
            {
                WoW.CastSpell("Frost Strike");
                return;
            }
            if (WoW.Talent(6) == 1 && CanCastInRange("Frost Strike") && currentRunes >= 1 && WoW.PlayerHasBuff("Killing Machine") && !hasBreath && WoW.Level >= 90)
            {
                WoW.CastSpell("Frostscythe");
                return;
            }

            if (isMelee && currentRunes >= 2 && WoW.Level >= 55 && (!hasBreath || (hasBreath && (runicPower <= 70 || hasBreath && currentRunes > 3))))
            {
                WoW.CastSpell("Obliterate");
                return;
            }
            if (runicPower >= 25 && CanCastInRange("Frost Strike") && !hasBreath && (!combatRoutine.UseCooldowns || (combatRoutine.UseCooldowns && WoW.SpellCooldownTimeRemaining("Breath") >= 1500)))
            {
                WoW.CastSpell("Frost Strike");
                return;
            }
            if (WoW.Talent(2) == 3 && currentRunes <= 4 && runicPower <= 70 && WoW.Level >= 57 && CanCastNoRange("Horn") && !WoW.PlayerHasBuff("HEmpower Rune") && (hasBreath || (!hasBreath && WoW.SpellCooldownTimeRemaining("Breath") >= 1500)))
            {
                WoW.CastSpell("Horn");
            }
            if (isMelee && WoW.HealthPercent <= 40 && WoW.PlayerHasBuff("Free DeathStrike") && !hasBreath && WoW.Level >= 55)
            {
                WoW.CastSpell("Death Strike");
                return;
            }
        }
        public void MGRotation()
        {
            if (isCheckHotkeysFrostOffensivePillarofFrost && isMelee && combatRoutine.UseCooldowns && !WoW.IsSpellOnCooldown("PillarofFrost") && WoW.Level >= 57)
            {
                WoW.CastSpell("PillarofFrost");
            }
            if (combatRoutine.UseCooldowns && isCheckHotkeysFrostOffensiveErW && isMelee && currentRunes == 0 && WoW.PlayerHasBuff("PillarofFrost") && !WoW.IsSpellOnCooldown("Empower Rune") && WoW.Level >= 57)
            {
                WoW.CastSpell("Empower Rune");
            }
            if (combatRoutine.Type == RotationType.SingleTarget || combatRoutine.Type == RotationType.SingleTargetCleave) // Do Single Target Stuff here
            {
                if (CanCastInRange("Frost Strike") && (!WoW.PlayerHasBuff("Icy Talons") || WoW.PlayerBuffTimeRemaining("Icy Talons") <= 200) && runicPower >= 25 &&
                    !(combatRoutine.UseCooldowns && CanCastNoRange("Obliteration") && WoW.Talent(7) == 1) &&
                    (WoW.Talent(7) != 1 || (WoW.Talent(7) == 1 && !WoW.PlayerHasBuff("Obliteration"))))
                {
                    Log.Write("Hasbuff " + WoW.PlayerHasBuff("Icy Talons") + " Remaining " + WoW.PlayerBuffTimeRemaining("Icy Talons"));
                    WoW.CastSpell("Frost Strike");
                    return;
                }
                if (isMelee && WoW.HealthPercent <= 40 && WoW.Level >= 55 && WoW.PlayerHasBuff("Free DeathStrike") && (WoW.Talent(7) != 1 || (WoW.Talent(7) == 1 && !WoW.PlayerHasBuff("Obliteration"))))
                {
                    WoW.CastSpell("Death Strike");
                    return;
                }
                if (CanCastInRange("Howling Blast") && !WoW.IsSpellOnCooldown("Howling Blast") && WoW.Level >= 55 && !WoW.TargetHasDebuff("Frost Fever") && currentRunes >= 1 &&
                    (WoW.Talent(7) != 1 || (WoW.Talent(7) == 1 && !WoW.PlayerHasBuff("Obliteration"))))
                {
                    WoW.CastSpell("Howling Blast");
                    return;
                }
                if (WoW.Talent(6) == 1 && runicPower >= 80 && CanCastInRange("Frost Strike"))
                {
                    WoW.CastSpell("Frost Strike");
                    return;
                }
                if (CanCastInRange("Howling Blast") && WoW.PlayerHasBuff("Rime") && WoW.Level >= 55 && (WoW.Talent(7) != 1 || (WoW.Talent(7) == 1 && !WoW.PlayerHasBuff("Obliteration"))))
                {
                    WoW.CastSpell("Howling Blast");
                    return;
                }

                if (combatRoutine.UseCooldowns && isMelee && currentRunes >= 2 && runicPower >= 25 && WoW.Talent(7) == 1 && CanCastNoRange("Obliteration") && WoW.Level >= 100)
                {
                    WoW.CastSpell("Obliteration");
                    return;
                }
                if (WoW.Talent(7) == 1 && runicPower >= 25 && CanCastInRange("Frost Strike") && WoW.PlayerHasBuff("Obliteration") && !WoW.PlayerHasBuff("Killing Machine"))
                {
                    WoW.CastSpell("Frost Strike");
                    return;
                }

                if (WoW.Talent(7) == 1 && isMelee && currentRunes >= 1 && WoW.PlayerHasBuff("Killing Machine") && WoW.PlayerHasBuff("Obliteration") && WoW.Level >= 55)
                {
                    WoW.CastSpell("Obliterate");
                    return;
                }
                if (WoW.Talent(6) == 1 && isMelee && currentRunes >= 1 && WoW.PlayerHasBuff("Killing Machine") && WoW.Level >= 90)
                {
                    WoW.CastSpell("Frostscythe");
                    return;
                }

                if (isMelee && currentRunes >= 2 && WoW.Level >= 55)
                {
                    WoW.CastSpell("Obliterate");
                    return;
                }
                if (WoW.Talent(7) == 3 && isMelee && currentRunes >= 1 && CanCastNoRange("Glacial Advance") && WoW.Level >= 100)
                {
                    WoW.CastSpell("Glacial Advance");
                    return;
                }
                if (WoW.Talent(7) == 1 && runicPower >= 40 && CanCastInRange("Frost Strike") && !(combatRoutine.UseCooldowns && CanCastNoRange("Obliteration")) && WoW.Talent(7) == 1 &&
                    !WoW.PlayerHasBuff("Obliteration"))
                {
                    WoW.CastSpell("Frost Strike");
                    return;
                }
                if (isMelee && currentRunes >= 1 && CanCastNoRange("Remorseless Winter") && WoW.Level >= 57 && (WoW.Talent(7) != 1 || (WoW.Talent(7) == 1 && !WoW.PlayerHasBuff("Obliteration"))))
                {
                    WoW.CastSpell("Remorseless Winter");
                    return;
                }
            }
            if (combatRoutine.Type == RotationType.AOE)
            {
                if (CanCastInRange("Frost Strike") && (!WoW.PlayerHasBuff("Icy Talons") || WoW.PlayerBuffTimeRemaining("Icy Talons") <= 200) && runicPower >= 25)
                {
                    WoW.CastSpell("Frost Strike");
                    return;
                }
                if (isMelee && WoW.HealthPercent <= 40 && WoW.PlayerHasBuff("Free DeathStrike") && WoW.Level >= 55)
                {
                    WoW.CastSpell("Death Strike");
                    return;
                }
                if (CanCastInRange("Howling Blast") && !WoW.IsSpellOnCooldown("Howling Blast") && !WoW.TargetHasDebuff("Frost Fever") && currentRunes >= 1 && WoW.Level >= 55)
                {
                    WoW.CastSpell("Howling Blast");
                    return;
                }
                if (CanCastInRange("Howling Blast") && WoW.PlayerHasBuff("Rime") && WoW.Level >= 55)
                {
                    WoW.CastSpell("Howling Blast");
                    return;
                }
                if (WoW.Talent(6) != 1 && isMelee && currentRunes >= 1 && CanCastNoRange("Remorseless Winter") && WoW.Level >= 57)
                {
                    WoW.CastSpell("Remorseless Winter");
                    return;
                }
                if (runicPower >= 80 && CanCastInRange("Frost Strike"))
                {
                    WoW.CastSpell("Frost Strike");
                    return;
                }
                if (WoW.Talent(6) != 1 && isMelee && currentRunes >= 2 && WoW.Level >= 55)
                {
                    WoW.CastSpell("Obliterate");
                    return;
                }
                if (WoW.Talent(6) == 1 && currentRunes >= 1 && isMelee && WoW.PlayerHasBuff("Killing Machine") && WoW.Level >= 90)
                {
                    WoW.CastSpell("Frostscythe");
                    return;
                }
                if (WoW.Talent(7) == 3 && isMelee && currentRunes >= 1 && CanCastNoRange("Glacial Advance") && WoW.Level >= 100)
                {
                    WoW.CastSpell("Glacial Advance");
                    return;
                }
                if (isMelee && currentRunes >= 1 && CanCastNoRange("Remorseless Winter") && WoW.Level >= 57)
                {
                    WoW.CastSpell("Remorseless Winter");
                    return;
                }
                if (WoW.Talent(6) == 1 && isMelee && currentRunes >= 1 && WoW.Level >= 90)
                {
                    WoW.CastSpell("Frostscythe");
                    return;
                }

                if (runicPower >= 25 && CanCastInRange("Frost Strike") && WoW.PlayerBuffStacks("Icy Talons") < 3)
                {
                    WoW.CastSpell("Frost Strike");
                    return;
                }
				
				if (WoW.CanCast("Sindragosa's Fury") && (DetectKeyPress.GetKeyState(0x5A) < 0) && WoW.Level >= 110)
                {																
                    WoW.CastSpell("Sindragosa's Fury");
                    return;
                }
            }
        }

        public bool isCastingListedSpell()
        {
            foreach (string spellid in SettingsFormDFF.spellList.Items)
            {
                var spellidint = int.Parse(spellid);
                if (WoW.TargetCastingSpellID == spellidint)
                {
                    return true;
                }
            }
            return false;
        }
    }

    public class SettingsFormDFFSm : Form
    {
        public Button btnaddspell;
        public Button btnHotkeysFrostOffensiveCooldowns;
        public Button btnremovespell;
        public TextBox checkHotkeysFrostAMSPercent;
        private readonly Label checkHotkeysFrostAMSPercentLabel;
        public CheckBox checkHotkeysFrostAntiMagicShield;
        public CheckBox checkHotkeysFrostIceboundFortitude;
        public CheckBox checkHotkeyslegyring;
        public TextBox checkHotkeysFrostIFPercent;
        public Label checkHotkeysFrostIFPercentLabel;
        public CheckBox checkHotkeysFrostOffensiveErW;
        public CheckBox checkHotkeysFrostOffensivePillarofFrost;


        /// <summary>
        ///     Required designer variable.
        /// </summary>
        private IContainer components = null;

        private readonly GroupBox groupBox12;
        private readonly GroupBox groupBox13;
        private readonly GroupBox groupBox22;
        private readonly Label spellIdLabel;
        public ListBox spellList;
        public TextBox spellText;
        private readonly TabControl tabControl3;
        private readonly TabPage tabPage5;


        #region Windows Form Designer generated code

        /// <summary>
        ///     Required method for Designer support - do not modify
        ///     the contents of this method with the code editor.
        /// </summary>
        public SettingsFormDFFSm()
        {
            this.spellList = new System.Windows.Forms.ListBox();
            this.spellText = new System.Windows.Forms.TextBox();
            this.spellIdLabel = new System.Windows.Forms.Label();
            this.btnaddspell = new System.Windows.Forms.Button();
            this.btnremovespell = new System.Windows.Forms.Button();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.groupBox22 = new System.Windows.Forms.GroupBox();
            this.checkHotkeysFrostIceboundFortitude = new System.Windows.Forms.CheckBox();
            this.checkHotkeyslegyring = new System.Windows.Forms.CheckBox();
            this.checkHotkeysFrostAntiMagicShield = new System.Windows.Forms.CheckBox();
            this.checkHotkeysFrostIFPercent = new System.Windows.Forms.TextBox();
            this.checkHotkeysFrostIFPercentLabel = new System.Windows.Forms.Label();
            this.checkHotkeysFrostAMSPercent = new System.Windows.Forms.TextBox();
            this.checkHotkeysFrostAMSPercentLabel = new System.Windows.Forms.Label();
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.checkHotkeysFrostOffensiveErW = new System.Windows.Forms.CheckBox();
            this.checkHotkeysFrostOffensivePillarofFrost = new System.Windows.Forms.CheckBox();
            this.btnHotkeysFrostOffensiveCooldowns = new System.Windows.Forms.Button();
            this.tabControl3 = new System.Windows.Forms.TabControl();
            this.tabPage5.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.groupBox22.SuspendLayout();
            this.groupBox13.SuspendLayout();
            this.tabControl3.SuspendLayout();
            /*
            this.spellIdLabel = new System.Windows.Forms.Label();
            this.btnaddspell = new System.Windows.Forms.Button();
            this.btnremovespell
            */
            // 
            // btnaddspell
            // 
            this.btnaddspell.Location = new System.Drawing.Point(110, 50);
            this.btnaddspell.Name = "btnaddspell";
            this.btnaddspell.Size = new System.Drawing.Size(28, 48);
            this.btnaddspell.TabIndex = 1;
            this.btnaddspell.Text = "+";
            this.btnaddspell.UseVisualStyleBackColor = true;

            // 
            // btnremovespell
            // 
            this.btnremovespell.Location = new System.Drawing.Point(110, 100);
            this.btnremovespell.Name = "btnremovespell";
            this.btnremovespell.Size = new System.Drawing.Size(28, 48);
            this.btnremovespell.TabIndex = 1;
            this.btnremovespell.Text = "-";
            this.btnremovespell.UseVisualStyleBackColor = true;
            // 
            // spellIdLabel
            // 
            this.spellIdLabel.AutoSize = true;
            this.spellIdLabel.Location = new System.Drawing.Point(28, 28);
            this.spellIdLabel.Name = "spellIdLabel";
            this.spellIdLabel.Size = new System.Drawing.Size(28, 28);
            this.spellIdLabel.TabIndex = 9;
            this.spellIdLabel.Text = "Spell ID:";
            // 
            // spellText
            // 
            this.spellText.AutoSize = true;
            this.spellText.Location = new System.Drawing.Point(28, 50);
            this.spellText.Name = "spellText";
            this.spellText.Size = new System.Drawing.Size(80, 28);
            this.spellText.TabIndex = 9;
            this.spellText.Text = "";

            // 
            // spellList
            // 
            this.spellList.AutoSize = false;
            this.spellList.Location = new System.Drawing.Point(28, 75);
            this.spellList.Name = "spellList";
            this.spellList.Size = new System.Drawing.Size(80, 290);
            this.spellList.TabIndex = 9;

            this.spellList.Text = "spellList";
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
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.checkHotkeysFrostIceboundFortitude);
            this.groupBox12.Controls.Add(this.checkHotkeysFrostIFPercent);
            this.groupBox12.Controls.Add(this.checkHotkeysFrostIFPercentLabel);
            this.groupBox12.Controls.Add(this.checkHotkeysFrostAntiMagicShield);
            this.groupBox12.Controls.Add(this.checkHotkeysFrostAMSPercent);
            this.groupBox12.Controls.Add(this.checkHotkeysFrostAMSPercentLabel);

            this.groupBox12.Location = new System.Drawing.Point(8, 100);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(561, 80);
            this.groupBox12.TabIndex = 2;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "Defensive Cooldowns";
            // 
            // checkHotkeysFrostIceboundFortitude
            // 
            this.checkHotkeysFrostIceboundFortitude.AutoSize = true;
            this.checkHotkeysFrostIceboundFortitude.Location = new System.Drawing.Point(151, 28);
            this.checkHotkeysFrostIceboundFortitude.Name = "checkHotkeysFrostIceboundFortitude";
            this.checkHotkeysFrostIceboundFortitude.Size = new System.Drawing.Size(100, 28);
            this.checkHotkeysFrostIceboundFortitude.TabIndex = 9;
            this.checkHotkeysFrostIceboundFortitude.Text = "Icebound Fortitude";
            this.checkHotkeysFrostIceboundFortitude.UseVisualStyleBackColor = true;
            // 
            // checkHotkeysFrostIFPercent
            // 
            this.checkHotkeysFrostIFPercent.AutoSize = true;
            this.checkHotkeysFrostIFPercent.Location = new System.Drawing.Point(300, 28);
            this.checkHotkeysFrostIFPercent.Name = "checkHotkeysFrostIFPercent";
            this.checkHotkeysFrostIFPercent.Size = new System.Drawing.Size(20, 28);
            this.checkHotkeysFrostIFPercent.TabIndex = 9;
            this.checkHotkeysFrostIFPercent.Text = "50";
            // 
            // checkHotkeysFrostIFPercent
            // 
            this.checkHotkeysFrostIFPercentLabel.AutoSize = true;
            this.checkHotkeysFrostIFPercentLabel.Location = new System.Drawing.Point(321, 30);
            this.checkHotkeysFrostIFPercentLabel.Name = "checkHotkeysFrostIFPercentLabel";
            this.checkHotkeysFrostIFPercentLabel.Size = new System.Drawing.Size(20, 28);
            this.checkHotkeysFrostIFPercentLabel.TabIndex = 9;
            this.checkHotkeysFrostIFPercentLabel.Text = "% HP";

            // 
            // checkHotkeysFrostAntiMagicShield
            // 
            this.checkHotkeysFrostAntiMagicShield.AutoSize = true;
            this.checkHotkeysFrostAntiMagicShield.Location = new System.Drawing.Point(151, 50);
            this.checkHotkeysFrostAntiMagicShield.Name = "checkHotkeysFrostAntiMagicShield";
            this.checkHotkeysFrostAntiMagicShield.Size = new System.Drawing.Size(104, 17);
            this.checkHotkeysFrostAntiMagicShield.TabIndex = 8;
            this.checkHotkeysFrostAntiMagicShield.Text = "Anti-Magic Shield";
            this.checkHotkeysFrostAntiMagicShield.UseVisualStyleBackColor = true;
            // 
            // checkHotkeysFrostAMSPercent
            // 
            this.checkHotkeysFrostAMSPercent.AutoSize = true;
            this.checkHotkeysFrostAMSPercent.Location = new System.Drawing.Point(300, 50);
            this.checkHotkeysFrostAMSPercent.Name = "checkHotkeysFrostIFPercent";
            this.checkHotkeysFrostAMSPercent.Size = new System.Drawing.Size(20, 28);
            this.checkHotkeysFrostAMSPercent.TabIndex = 9;
            this.checkHotkeysFrostAMSPercent.Text = "50";
            // 
            // checkHotkeysFrostAMSPercentLabel
            // 
            this.checkHotkeysFrostAMSPercentLabel.AutoSize = true;
            this.checkHotkeysFrostAMSPercentLabel.Location = new System.Drawing.Point(321, 52);
            this.checkHotkeysFrostAMSPercentLabel.Name = "checkHotkeysFrostAMSPercentLabel";
            this.checkHotkeysFrostAMSPercentLabel.Size = new System.Drawing.Size(20, 28);
            this.checkHotkeysFrostAMSPercentLabel.TabIndex = 9;
            this.checkHotkeysFrostAMSPercentLabel.Text = "% HP";
            // 
            // groupBox13
            // 
            this.groupBox13.Controls.Add(this.checkHotkeysFrostOffensiveErW);
            this.groupBox13.Controls.Add(this.checkHotkeysFrostOffensivePillarofFrost);
            this.groupBox13.Controls.Add(this.btnHotkeysFrostOffensiveCooldowns);
            this.groupBox13.Controls.Add(this.checkHotkeyslegyring);
            this.groupBox13.Location = new System.Drawing.Point(8, 8);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Size = new System.Drawing.Size(556, 90);
            this.groupBox13.TabIndex = 3;
            this.groupBox13.TabStop = false;
            this.groupBox13.Text = "Offensive Cooldowns";

            // 
            // checkHotkeysFrostOffensiveErW
            // 
            this.checkHotkeysFrostOffensiveErW.AutoSize = true;
            this.checkHotkeysFrostOffensiveErW.Location = new System.Drawing.Point(151, 60);
            this.checkHotkeysFrostOffensiveErW.Name = "checkHotkeysFrostOffensiveErW";
            this.checkHotkeysFrostOffensiveErW.Size = new System.Drawing.Size(48, 17);
            this.checkHotkeysFrostOffensiveErW.TabIndex = 3;
            this.checkHotkeysFrostOffensiveErW.Text = "ErW";
            this.checkHotkeysFrostOffensiveErW.UseVisualStyleBackColor = true;

            // 
            // checkHotkeysFrostOffensivePillarofFrost
            // 
            this.checkHotkeysFrostOffensivePillarofFrost.AutoSize = true;
            this.checkHotkeysFrostOffensivePillarofFrost.Location = new System.Drawing.Point(151, 32);
            this.checkHotkeysFrostOffensivePillarofFrost.Name = "checkHotkeysFrostOffensivePillarofFrost";
            this.checkHotkeysFrostOffensivePillarofFrost.Size = new System.Drawing.Size(99, 17);
            this.checkHotkeysFrostOffensivePillarofFrost.TabIndex = 2;
            this.checkHotkeysFrostOffensivePillarofFrost.Text = "Pillar of Frost";
            this.checkHotkeysFrostOffensivePillarofFrost.UseVisualStyleBackColor = true;

            // 
            // checkHotkeyslegyring
            // 
            this.checkHotkeyslegyring.AutoSize = true;
            this.checkHotkeyslegyring.Location = new System.Drawing.Point(300, 32);
            this.checkHotkeyslegyring.Name = "checkHotkeyslegyring";
            this.checkHotkeyslegyring.Size = new System.Drawing.Size(99, 17);
            this.checkHotkeyslegyring.TabIndex = 4;
            this.checkHotkeyslegyring.Text = "Legendary Ring";
            this.checkHotkeyslegyring.UseVisualStyleBackColor = true;
			
            // 
            // btnHotkeysFrostOffensiveCooldowns
            // 
            this.btnHotkeysFrostOffensiveCooldowns.Location = new System.Drawing.Point(18, 28);
            this.btnHotkeysFrostOffensiveCooldowns.Name = "btnHotkeysFrostOffensiveCooldowns";
            this.btnHotkeysFrostOffensiveCooldowns.Size = new System.Drawing.Size(113, 23);
            this.btnHotkeysFrostOffensiveCooldowns.TabIndex = 1;
            this.btnHotkeysFrostOffensiveCooldowns.Text = "Click to Set";
            this.btnHotkeysFrostOffensiveCooldowns.UseVisualStyleBackColor = true;
            // 
            // tabControl3
            // 
            this.tabControl3.Controls.Add(this.tabPage5);
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
            this.Text = "SettingsForm";
            this.tabPage5.ResumeLayout(false);
            this.groupBox12.ResumeLayout(false);
            this.groupBox12.PerformLayout();
            this.groupBox22.ResumeLayout(false);
            this.groupBox22.PerformLayout();
            this.groupBox13.ResumeLayout(false);
            this.groupBox13.PerformLayout();
            this.tabControl3.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion
    }
}

/*
[AddonDetails.db]
AddonAuthor=FmFlex
AddonName=smartie
WoWVersion=Legion - 70300
[SpellBook.db]
Spell,49143,Frost Strike,D4
Spell,207230,Frostscythe,NumPad8
Spell,49184,Howling Blast,D1
Spell,49020,Obliterate,D3
Spell,196770,Remorseless Winter,D7
Spell,194913,Glacial Advance,NumPad7
Spell,49998,Death Strike,NumPad1
Spell,48707,Anti-Magic Shell,NumPad2
Spell,48792,Icebound Fortitude,NumPad3
Spell,51271,PillarofFrost,D6
Spell,45524,ChainofIce,NumPad5
Spell,47568,Empower Rune,D9
Spell,207127,HEmpower Rune,D9
Spell,207256,Obliteration,OemOpenBrackets
Spell,47528,Mind Freeze,NumPad4
Spell,152279,Breath,D5
Spell,57330,Horn,D0
Spell,190778,Sindragosa's Fury,D8
Aura,51124,Killing Machine
Aura,194879,Icy Talons
Aura,55095,Frost Fever
Aura,59057,Rime
Aura,101568,Free DeathStrike
Aura,51271,PillarofFrost
Aura,45524,ChainofIce
Aura,207256,Obliteration
Aura,152279,Breath
Aura,207127,HEmpower Rune
Aura,211805,Gathering Storm
*/
