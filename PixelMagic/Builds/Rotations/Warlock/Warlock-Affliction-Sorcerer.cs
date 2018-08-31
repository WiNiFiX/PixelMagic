// winifix@gmail.com
// ReSharper disable UnusedMember.Global
// ReSharper disable ConvertPropertyToExpressionBody

using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using PixelMagic.Helpers;

namespace PixelMagic.Rotation
{
    public class WarlockAffliction : CombatRoutine
    {
        public override string Name
        {get{return "Affliction Warlock";}}

        public override string Class
        {get{return "Warlock";}}

        public override Form SettingsForm { get; set; }

        public override void Initialize()
        {
            Log.Write("Welcome to Affliction Warlock", Color.Purple);
            Log.Write("Use Scroll Lock key to toggle ST/AOE/CLEAVE auto detection", Color.Blue);
            Log.Write("If Scroll Lock LED is ON ST/AOE/CLEAVE auto detection is ENABLED", Color.Blue);
            Log.Write("If Scroll Lock LED is OFF ST/AOE/CLEAVE auto detection is DISABLED use the manual mode to select ST/AOE/CLEAVE (Default: ALT+S, ALT+A)", Color.Blue);
        }

        public override void Stop()
        {
        }

        public override void Pulse() // Updated for Legion (tested and working for single target)
        {
            if (WoW.IsInCombat && Control.IsKeyLocked(Keys.Scroll) && !WoW.TargetIsPlayer && !WoW.IsMounted)
            {
                SelectRotation(4, 9999, 1);
            }

            if (combatRoutine.Type == RotationType.SingleTarget) // Do Single Target Stuff here
            {
                if (WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsInCombat && !WoW.PlayerIsCasting && !WoW.IsMounted)
                {                   


                    if ((!WoW.TargetHasDebuff("Agony") || WoW.TargetDebuffTimeRemaining("Agony") <= 540)
                        && (!WoW.PlayerIsChanneling || WoW.TargetDebuffTimeRemaining("Agony") <= 150)
                        && WoW.CanCast("Agony")
                        && WoW.IsSpellInRange("Agony"))
                    {
                        WoW.CastSpell("Agony");
                        return;
                    }

                    if ((WoW.CurrentSoulShards >= 3 || (WoW.CurrentSoulShards >= 2 && WoW.WasLastCasted("Unstable Affliction")))
                        && !WoW.IsMoving
                        && WoW.CanCast("Unstable Affliction")
                        && WoW.IsSpellInRange("Agony"))
                    {
                        WoW.CastSpell("Unstable Affliction");
                        Thread.Sleep(200);
                        return;
                    }

                    if ((WoW.TargetHasDebuff("Unstable Affliction1") && WoW.TargetHasDebuff("Unstable Affliction2")
                        || WoW.TargetHasDebuff("Unstable Affliction1") && WoW.TargetHasDebuff("Unstable Affliction3")
                        || WoW.TargetHasDebuff("Unstable Affliction1") && WoW.TargetHasDebuff("Unstable Affliction4")
                        || WoW.TargetHasDebuff("Unstable Affliction1") && WoW.TargetHasDebuff("Unstable Affliction5")
                        || WoW.TargetHasDebuff("Unstable Affliction2") && WoW.TargetHasDebuff("Unstable Affliction3")
                        || WoW.TargetHasDebuff("Unstable Affliction2") && WoW.TargetHasDebuff("Unstable Affliction4")
                        || WoW.TargetHasDebuff("Unstable Affliction2") && WoW.TargetHasDebuff("Unstable Affliction5")
                        || WoW.TargetHasDebuff("Unstable Affliction3") && WoW.TargetHasDebuff("Unstable Affliction4")
                        || WoW.TargetHasDebuff("Unstable Affliction3") && WoW.TargetHasDebuff("Unstable Affliction5")
                        || WoW.TargetHasDebuff("Unstable Affliction4") && WoW.TargetHasDebuff("Unstable Affliction5")
                        || WoW.PlayerBuffStacks("Reap Souls") >= 12)
                        && !WoW.PlayerIsCasting
                        && WoW.CanCast("Reap Souls")
                        && !WoW.PlayerHasBuff("Deadwind Harvester")
                        && WoW.PlayerHasBuff("Tormented Souls"))
                    {
                        WoW.CastSpell("Reap Souls");
                        return;
                    }

                    if (WoW.CanCast("Life Tap") && !WoW.PlayerIsChanneling && WoW.Talent(2) == 3 && !WoW.PlayerHasBuff("Empowered Life Tap"))
                    {
                        WoW.CastSpell("Life Tap");
                        return;
                    }

                    if ((!WoW.TargetHasDebuff("Corruption") || WoW.TargetDebuffTimeRemaining("Corruption") <= 420)
                        && (!WoW.PlayerIsChanneling || WoW.TargetDebuffTimeRemaining("Corruption") <= 150)
                        && WoW.CanCast("Corruption")
                        && WoW.IsSpellInRange("Agony"))
                    {
                        WoW.CastSpell("Corruption");
                        return;
                    }

                    if ((!WoW.TargetHasDebuff("Siphon Life") || WoW.TargetDebuffTimeRemaining("Siphon Life") <= 420)
                        && (!WoW.PlayerIsChanneling || WoW.TargetDebuffTimeRemaining("Siphon Life") <= 150)
                        && WoW.Talent(4) == 1
                        && WoW.CanCast("Siphon Life")
                        && WoW.IsSpellInRange("Agony"))
                    {
                        WoW.CastSpell("Siphon Life");
                        return;
                    }

                    if (WoW.CanCast("Felhunter") && WoW.Talent(6) == 2 && !WoW.IsSpellOnCooldown("Felhunter") && WoW.IsSpellInRange("Agony") && !WoW.PlayerIsChanneling && !WoW.PlayerIsCasting)
                    {
                        WoW.CastSpell("Felhunter");
                        return;
                    }

                    if (WoW.CanCast("Unstable Affliction") && !WoW.IsMoving && WoW.Talent(2) == 1 && !WoW.IsMoving && WoW.IsSpellInRange("Unstable Affliction") && !WoW.PlayerIsChanneling && WoW.CurrentSoulShards >= 1
                        && (!WoW.TargetHasDebuff("Unstable Affliction1") || !WoW.TargetHasDebuff("Unstable Affliction2") || !WoW.TargetHasDebuff("Unstable Affliction3") || !WoW.TargetHasDebuff("Unstable Affliction4") || !WoW.TargetHasDebuff("Unstable Affliction5")
                        || (WoW.TargetDebuffTimeRemaining("Unstable Affliction1") <= 150) || (WoW.TargetDebuffTimeRemaining("Unstable Affliction2") <= 150) || (WoW.TargetDebuffTimeRemaining("Unstable Affliction3") <= 150)
                        || (WoW.TargetDebuffTimeRemaining("Unstable Affliction4") <= 150) || (WoW.TargetDebuffTimeRemaining("Unstable Affliction5") <= 150)))
                    {
                        WoW.CastSpell("Unstable Affliction");
                        Thread.Sleep(200);
                        return;
                    }

                    if (WoW.IsInCombat && WoW.Mana < 70 && WoW.HealthPercent > 70 && WoW.CanCast("Life Tap"))
                    {
                        WoW.CastSpell("Life Tap");
                        return;
                    }

                    if (WoW.CanCast("Haunt") && WoW.Talent(1) == 1 && !WoW.IsSpellOnCooldown("Haunt") && WoW.IsSpellInRange("Agony") && !WoW.PlayerIsChanneling && !WoW.PlayerIsCasting && !WoW.IsMoving)
                    {
                        WoW.CastSpell("Haunt");
                        return;
                    }

                    if (WoW.CanCast("Drain Soul") && WoW.IsSpellInRange("Agony") && !WoW.PlayerIsChanneling && !WoW.PlayerIsCasting && !WoW.IsMoving)
                    {
                        WoW.CastSpell("Drain Soul");
                        return;
                    }
                }
            }
            if (combatRoutine.Type == RotationType.AOE)
            {
                if (WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsInCombat && !WoW.PlayerIsChanneling && !WoW.PlayerIsCasting && !WoW.IsMounted) // Do AOE stuff here
                {
                    if (WoW.CanCast("Agony") && WoW.IsSpellInRange("Agony") && WoW.TargetHasDebuff("Seed of Corruption") && (!WoW.TargetHasDebuff("Agony") || (WoW.TargetDebuffTimeRemaining("Agony") <= 540)))
                    {
                        WoW.CastSpell("Agony");
                        return;
                    }

                    if (WoW.CanCast("Corruption") && WoW.IsSpellInRange("Agony") && WoW.TargetHasDebuff("Seed of Corruption") && (!WoW.TargetHasDebuff("Corruption") || (WoW.TargetDebuffTimeRemaining("Corruption") <= 420)))
                    {
                        WoW.CastSpell("Corruption");
                        return;
                    }

                    if (WoW.CanCast("Seed of Corruption") && WoW.IsSpellInRange("Agony") && !WoW.TargetHasDebuff("Seed of Corruption") && !WoW.IsMoving && WoW.CurrentSoulShards >= 1)
                    {
                        WoW.CastSpell("Seed of Corruption");
                        return;
                    }
                }
            }
            if (combatRoutine.Type == RotationType.SingleTargetCleave)
            {
                // Do Single Target Cleave stuff here if applicable else ignore this one
            }
        }



        #region Functions

        private float GCD
        {
            get
            {
                if (Convert.ToSingle(150f / (1 + (WoW.HastePercent / 100f))) > 75f)
                {
                    return Convert.ToSingle(150f / (1 + (WoW.HastePercent / 100f)));
                }
                else
                {
                    return 75f;
                }
            }
        }

        private void interruptcast()
        {
            Random random = new Random();
            int randomNumber = random.Next(60, 80);
            if (WoW.TargetPercentCast > randomNumber && WoW.TargetIsCastingAndSpellIsInterruptible)
            {
                if (WoW.PlayerRace == "BloodElf" && WoW.CanCast("Arcane Torrent", true, true, false, false, true) && !WoW.IsSpellOnCooldown("Wind Shear") && WoW.TargetIsCastingAndSpellIsInterruptible) //interupt every spell, not a boss.
                {
                    WoW.CastSpell("Arcane Torrent");
                    return;
                }
                if (WoW.PlayerRace == "Pandaren" && WoW.CanCast("Quaking palm", true, true, true, false, true)) //interupt every spell, not a boss.
                {
                    WoW.CastSpell("Quaking palm");
                    return;
                }
            }
        }

        private void DBMPrePull()
        {
            if (dbmOn && dbmTimer <= 18 && dbmTimer > 0 && WoW.HasTarget)
            {
                if (!WoW.ItemOnCooldown("Prolonged Power"))
                {
                    WoW.CastSpell("Prolonged Power");
                    return;
                }
            }
        }

        private void Defensive()
        {
            if (WoW.PlayerRace == "Dreanei" && WoW.HealthPercent < 80 && !WoW.IsSpellOnCooldown("Gift Naaru"))
            {
                WoW.CastSpell("Gift Naaru");
            }
        }

        private void Stuns()
        {
            if (!WoW.PlayerIsCasting)
            {
                if (WoW.PlayerRace == "Tauren​" && !WoW.IsMoving && WoW.CanCast("War Stomp") && !WoW.IsSpellOnCooldown("War Stomp"))
                {
                    WoW.CastSpell("War Stomp");
                    return;
                }
            }
        }

        private void DPSRacial()
        {
            if (!WoW.PlayerIsCasting)
            {
                if (WoW.PlayerRace == "Troll" && WoW.CanCast("Berserking") && !WoW.IsSpellOnCooldown("Berserking"))
                {
                    WoW.CastSpell("Berserking");
                    return;
                }

                if (WoW.PlayerRace == "Orc" && WoW.CanCast("Blood Fury") && !WoW.IsSpellOnCooldown("Blood Fury"))
                {
                    WoW.CastSpell("Blood Fury");
                    return;
                }
            }

        }

        private void UsePotion()
        {
            if (!WoW.ItemOnCooldown("Prolonged Power"))
            {
                WoW.CastSpell("Prolonged Power");
                return;
            }
        }

        private static bool lastNamePlate = true;
        public void SelectRotation(int aoe, int cleave, int single)
        {
            int count = WoW.CountEnemyNPCsInRange;
            if (!lastNamePlate)
            {
                combatRoutine.ChangeType(RotationType.SingleTarget);
                lastNamePlate = true;
            }
            lastNamePlate = WoW.Nameplates;
            if (count >= aoe)
                combatRoutine.ChangeType(RotationType.AOE);
            if (count == cleave)
                combatRoutine.ChangeType(RotationType.SingleTargetCleave);
            if (count <= single)
                combatRoutine.ChangeType(RotationType.SingleTarget);

        }

        /*  private void ChangeTarget()
          {
              await Task.Run(() =>
              {
                  if (PlayerSpec == "Enhancement" && !TargetInfo.Melee)
                      WoW.KeyPressRelease(WoW.Keys.Tab);
                  if (PlayerSpec == "Elemental " && !TargetInfo.Range)
                      WoW.KeyPressRelease(WoW.Keys.Tab);
              });
          }*/

        private static bool setBonus2Pc
        {
            get
            {
                var control = WoW.GetBlockColor(9, 24);
                if (Convert.ToInt32(Math.Round(Convert.ToSingle(control.G))) == 0 && Convert.ToInt32(Math.Round(Convert.ToSingle(control.R) * 100 / 255)) >= 2)
                {

                    return true;
                }
                else
                    return false;
            }
        }

        private static bool setBonus4Pc
        {
            get
            {
                var control = WoW.GetBlockColor(9, 24);
                //Log.Write("Bonus location: " + Convert.ToInt32(Math.Round(Convert.ToSingle(control.R) * 100 / 255)));
                if (Convert.ToInt32(Math.Round(Convert.ToSingle(control.G))) == 0 && Convert.ToInt32(Math.Round(Convert.ToSingle(control.R) * 100 / 255)) >= 4)
                {

                    return true;
                }
                else
                    return false;
            }
        }

        private static string PlayerSpec
        {
            get
            {
                var c = WoW.GetBlockColor(10, 24);
                try
                {
                    if (c.B == 0) return "none";
                    string[] Spec = new string[] { "None", "Blood", "Frost", "Unholy", "Havoc", "Vengeance", "Balance", "Feral", "Guardian", "Restoration", "Beast Mastery", "Marksmanship", "Survival", "Arcane", "Fire", "Frost", "Brewmaster", "Mistweaver", "Windwalker", "Holy", "Protection", "Retribution", "Discipline", "HolyPriest", "Shadow", "Assassination", "Outlaw", "Subtlety", "Elemental", "Enhancement", "RestorationShaman", "Affliction", "Arms", "Fury", "Protection", "Demonology", "Destruction", "none" };
                    var race = Convert.ToInt32(Math.Round(Convert.ToSingle(c.G))) * 100 / 255;
                    var spec = Spec[race];

                    return spec;
                }
                catch (Exception ex)
                {
                    Log.Write("Error in Spec  Green = " + c.G);

                    Log.Write(ex.Message, Color.Red);
                }
                return "none";
            }
        }

        private static int npcCount
        {
            get
            {
                var c = WoW.GetBlockColor(11, 23);
                try
                {
                    return Convert.ToInt32(Math.Round(Convert.ToSingle(c.G) * 100 / 255));
                }
                catch (Exception ex)
                {
                    Log.Write("Error in NamePlate  Green = " + c.G);

                    Log.Write(ex.Message, Color.Red);
                }
                return 1;
            }
        }

        private static bool Nameplates
        {
            get
            {
                var c = WoW.GetBlockColor(11, 23);
                try
                {
                    if (Convert.ToInt32(Math.Round(Convert.ToSingle(c.B) / 255)) == 1)
                        return true;
                    else
                        return false;

                }
                catch (Exception ex)
                {
                    Log.Write("Error in NamePlate  Green = " + c.G);

                    Log.Write(ex.Message, Color.Red);
                }
                return false;
            }
        }

        private static int RaidSize
        {
            get
            {
                var c = WoW.GetBlockColor(11, 23);
                try
                {
                    int players = 0;
                    if (Convert.ToInt32(Math.Round(Convert.ToSingle(c.R)) * 100 / 255) > 0)
                        players = (Convert.ToInt32(Math.Round(Convert.ToSingle(c.R)) * 100 / 255));
                    if (Convert.ToInt32(Math.Round(Convert.ToSingle(c.R)) * 100 / 255) == 100)
                        players = 1;
                    if (players > 30)
                        players = 30;
                    if (players <= 5)
                        players = players - 1;
                    return players;
                }
                catch (Exception ex)
                {
                    Log.Write("Error in Players  Green = " + c.G);

                    Log.Write(ex.Message, Color.Red);
                }
                return 1;
            }
        }

        private static bool dbmOn
        {
            get
            {
                Color pixelColor = Color.FromArgb(0);
                var c = WoW.GetBlockColor(6, 24);
                try
                {
                    if (Convert.ToInt32(Math.Round(Convert.ToSingle(pixelColor.R) / 255)) == 1)
                        return true;
                    else
                        return false;

                }
                catch (Exception ex)
                {
                    Log.Write("Error in DBMON  Green = " + c.G);

                    Log.Write(ex.Message, Color.Red);
                }
                return false;
            }
        }

        private static int dbmTimer
        {
            get
            {
                Color pixelColor = Color.FromArgb(0);
                var c = WoW.GetBlockColor(6, 24);
                try
                {
                    if (Convert.ToInt32(Math.Round(Convert.ToSingle(pixelColor.R) / 255)) == 1)
                        return Convert.ToInt32(Math.Round(Convert.ToSingle(pixelColor.G) * 100 / 255));
                    else
                        return 0;
                }
                catch (Exception ex)
                {
                    Log.Write("Error in Dbm Timer Green = " + c.G);

                    Log.Write(ex.Message, Color.Red);
                }
                return 0;
            }
        }

        #endregion

    }
}


/*
[AddonDetails.db]
AddonAuthor=Sorcerer
AddonName=PixelMagic
WoWVersion=Legion - 70300
[SpellBook.db]
Spell,980,Agony,NumPad1
Spell,63106,Siphon Life,NumPad2
Spell,172,Corruption,NumPad3
Spell,30108,Unstable Affliction,NumPad4
Spell,216698,Reap Souls,NumPad5
Spell,1454,Life Tap,NumPad7
Spell,48181,Haunt,NumPad8
Spell,198590,Drain Soul,Add
Spell,27243,Seed of Corruption,NumPad0
Spell,111897,Felhunter,NumPad9
Aura,980,Agony
Aura,27243,Seed of Corruption
Aura,146739,Corruption
Aura,63106,Siphon Life
Aura,233490,Unstable Affliction1
Aura,233496,Unstable Affliction2
Aura,233497,Unstable Affliction3
Aura,233498,Unstable Affliction4
Aura,233499,Unstable Affliction5
Aura,216708,Deadwind Harvester
Aura,216695,Tormented Souls
Aura,235156,Empowered Life Tap
Aura,127271,Mount
*/
