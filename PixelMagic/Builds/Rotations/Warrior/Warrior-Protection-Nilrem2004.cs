// winifix@gmail.com
// ReSharper disable UnusedMember.Global
// ReSharper disable ConvertPropertyToExpressionBody

using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using PixelMagic.Helpers;

namespace PixelMagic.Rotation
{
    public class ProtNilremLeg : CombatRoutine
    {
        public Stopwatch CombatWatch = new Stopwatch();

        public override string Name
        {
            get { return "Prot Warrior"; }
        }

        public override string Class
        {
            get { return "Warrior"; }
        }

        public override Form SettingsForm { get; set; }

        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(int vKey);

        public override void Initialize()
        {
            Log.Write("Welcome to Protection Warrior", Color.Green);
            Log.Write("Suggested build: 1222312", Color.Green);
        }

        public override void Stop()
        {
        }

        public override void Pulse()
        {
            if (WoW.PlayerHasBuff("Mount")) return;

            if (WoW.IsInCombat && WoW.HealthPercent < 35 && WoW.CanCast("Last Stand") && !WoW.IsSpellOnCooldown("Last Stand"))
            {
                WoW.CastSpell("Last Stand");
                return;
            }
            if (WoW.IsInCombat && WoW.HealthPercent < 20 && WoW.CanCast("Shield Wall") && !WoW.IsSpellOnCooldown("Shield Wall"))
            {
                WoW.CastSpell("Shield Wall");
                return;
            }
            
            if (combatRoutine.Type == RotationType.SingleTarget) // Do Single Target Stuff here
            {
                if (CombatWatch.IsRunning && !WoW.IsInCombat)
                {
                    CombatWatch.Reset();
                }
                if (!CombatWatch.IsRunning && WoW.IsInCombat)
                {
                    CombatWatch.Start();
                }

                if (WoW.HasTarget && WoW.TargetIsEnemy && !WoW.PlayerIsChanneling)
                {
                    if (!WoW.TargetHasDebuff("ShockWavestun") && WoW.IsInCombat)

                    {
                        if (WoW.CanCast("Shield Block") && WoW.Rage >= 10 && !WoW.PlayerIsChanneling && WoW.HealthPercent < 100 &&
                            (WoW.PlayerSpellCharges("Shield Block") == 2 ||
                             (WoW.PlayerSpellCharges("Shield Block") >= 1 && WoW.HealthPercent <= 90 && WoW.PlayerBuffTimeRemaining("ShieldBlockAura") <= 2)))
                        {
                            WoW.CastSpell("Shield Block");
                            return;
                        }

                        if (WoW.IsSpellInRange("Shield Slam") && WoW.CanCast("Thunder Clap") && !WoW.IsSpellOnCooldown("Thunder Clap") && CombatWatch.ElapsedMilliseconds > 1000 &&
                            CombatWatch.ElapsedMilliseconds < 5000)
                        {
                            WoW.CastSpell("Thunder Clap");
                            return;
                        }

                        /* ------------------ IGNORE PAIN MANAGEMENT----------------------*/

                        if (WoW.CanCast("Ignore Pain") && WoW.PlayerHasBuff("Vengeance: Ignore Pain") && WoW.PlayerHasBuff("Ultimatum") && WoW.Rage >= 18)
                        {
                            WoW.CastSpell("Ignore Pain");
                            return;
                        }

                        if (WoW.CanCast("Ignore Pain") && WoW.Rage > 35 && WoW.PlayerHasBuff("Vengeance: Ignore Pain"))
                        {
                            WoW.CastSpell("Ignore Pain");
                            return;
                        }

                        if (WoW.CanCast("Ignore Pain") && WoW.Rage < 30 && WoW.Rage >= 20 && WoW.HealthPercent < 100 &&
                            (!WoW.PlayerHasBuff("Ignore Pain") || WoW.PlayerBuffTimeRemaining("Ignore Pain") <= 2) && !WoW.PlayerHasBuff("Ultimatum") &&
                            !WoW.PlayerHasBuff("Vengeance: Ignore Pain") && !WoW.PlayerHasBuff("Vengeance: Focused Rage"))
                        {
                            WoW.CastSpell("Ignore Pain");
                            return;
                        }

                        /* ------------------ END IGNORE PAIN MANAGEMENT-------------------*/


                        /* ------------------ FOCUSED RAGE MANAGEMENT----------------------*/

                        if (WoW.CanCast("Focused Rage") && (!WoW.PlayerHasBuff("Ignore Pain") || WoW.PlayerBuffTimeRemaining("Ignore Pain") <= 2) &&
                            WoW.PlayerHasBuff("Vengeance: Focused Rage") && WoW.Rage >= 20)
                        {
                            WoW.CastSpell("Focused Rage");
                        }
                        if (WoW.CanCast("Focused Rage") && WoW.PlayerHasBuff("Ultimatum") && (!WoW.PlayerHasBuff("Ignore Pain") || WoW.PlayerBuffTimeRemaining("Ignore Pain") <= 2) &&
                            WoW.PlayerHasBuff("Vengeance: Focused Rage"))
                        {
                            WoW.CastSpell("Focused Rage");
                        }
                        if (WoW.CanCast("Focused Rage") && WoW.PlayerHasBuff("Ultimatum") && !WoW.PlayerHasBuff("Vengeance: Ignore Pain") && !WoW.PlayerHasBuff("Vengeance: Focused Rage"))
                        {
                            WoW.CastSpell("Focused Rage");
                        }
                        if (WoW.CanCast("Focused Rage") && WoW.Rage >= 30 && !WoW.PlayerHasBuff("Vengeance: Focused Rage") && !WoW.PlayerHasBuff("Vengeance: Ignore Pain"))
                        {
                            WoW.CastSpell("Focused Rage");
                        }
                        if (WoW.CanCast("Focused Rage") && WoW.Rage < 10 && WoW.PlayerHasBuff("Ultimatum") && WoW.PlayerHasBuff("Vengeance: Ignore Pain") &&
                            !WoW.IsSpellOnCooldown("Shield Slam"))
                        {
                            WoW.CastSpell("Focused Rage");
                        }
                        if (WoW.CanCast("Focused Rage") && WoW.Rage >= 120)
                        {
                            WoW.CastSpell("Focused Rage");
                        }

                        /* ------------------ END FOCUSED RAGE MANAGEMENT-------------------*/

                        if (WoW.TargetIsCasting && WoW.CanCast("SpellReflect") && !WoW.IsSpellOnCooldown("SpellReflect"))
                        {
                            WoW.CastSpell("SpellReflect");
                        }
                        if (WoW.IsSpellInRange("Shield Slam") && WoW.CanCast("Shield Slam") && !WoW.IsSpellOnCooldown("Shield Slam"))
                        {
                            WoW.CastSpell("Shield Slam");
                            return;
                        }
                        if (WoW.CanCast("Revenge") && !WoW.IsSpellOnCooldown("Revenge") && WoW.IsSpellInRange("Shield Slam"))
                        {
                            WoW.CastSpell("Revenge");
                            return;
                        }
                        if (WoW.CanCast("Victory Rush") && !WoW.IsSpellOnCooldown("Victory Rush") && WoW.IsSpellInRange("Shield Slam") && WoW.HealthPercent < 90 &&
                            WoW.PlayerHasBuff("VictoryRush"))
                        {
                            WoW.CastSpell("Victory Rush");
                            return;
                        }
                        if (WoW.IsSpellInRange("Devastate") && WoW.CanCast("Devastate"))
                        {
                            WoW.CastSpell("Devastate");
                            return;
                        }

                        /* if (WoW.IsSpellInRange("Shield Slam")&& WoW.CanCast("Thunder Clap")&& !WoW.IsSpellOnCooldown("Thunder Clap"))
							{
								WoW.CastSpell("Thunder Clap");
								return;
							}  */
                    }
                    if (WoW.CanCast("Neltharion's Fury") && WoW.TargetHasDebuff("ShockWavestun"))
                    {
                        WoW.CastSpell("Neltharion's Fury");
                        return;
                    }
                }
            }
            if (combatRoutine.Type == RotationType.AOE)
            {
                // Do AOE Stuff here

                if (WoW.IsSpellOverlayed("Shield Slam"))
                    Log.Write("Spell Overlayed: Shield Slam");
            }
        }
    }
}

/*
[AddonDetails.db]
AddonAuthor=WiNiFiX
AddonName=RGB
WoWVersion=Legion - 70300
[SpellBook.db]
Spell,6343,Thunder Clap,V
Spell,23922,Shield Slam,D2
Spell,6572,Revenge,D3
Spell,20243,Devastate,B
Spell,34428,Victory Rush,D5
Spell,204488,Focused Rage,D6
Spell,203526,Neltharion's Fury,X
Spell,46968,Shockwave,S
Spell,871,Shield Wall,D6
Spell,12975,Last Stand,D9
Spell,6552,Pummel,D7
Spell,2565,Shield Block,Q
Spell,190456,Ignore Pain,K
Spell,6544,HeroicLeap,U
Spell,23920,SpellReflect,D8
Aura,132168,ShockWavestun
Aura,122510,Ultimatum
Aura,202573,Vengeance: Focused Rage
Aura,202574,Vengeance: Ignore Pain
Aura,190456,Ignore Pain
Aura,132404,ShieldBlockAura
Aura,32216,VictoryRush
Aura,186305,Mount
*/
