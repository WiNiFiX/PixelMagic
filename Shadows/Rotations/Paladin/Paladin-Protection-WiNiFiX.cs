// winifix@gmail.com
// ReSharper disable UnusedMember.Global


using System;
using System.Drawing;
using System.Windows.Forms;
using ShadowMagic.Helpers;

namespace ShadowMagic.Rotation
{
    public class PaladinProtWinifix : CombatRoutine
    {
        public override string Name
        {
            get
            {
                return "PixelMagic Protection";
            }
        }
        public override string Class
        {
            get
            {
                return "Paladin";
            }
        }

        public override Form SettingsForm
        {
            get { throw new NotImplementedException(); }

            set { throw new NotImplementedException(); }
        }


        public override void Initialize()
        {
            Log.DrawHorizontalLine();
            Log.WritePixelMagic("Welcome to PixelMagic Protection", Color.Black);
            Log.Write("Supported Talents: 2,2,1,2,3,2,1");
        }

        public override void Stop()
        {
        }

        public override void Pulse()
        {
            if (WoW.HealthPercent == 0 || WoW.IsMounted) return;
            if (!WoW.IsInCombat && WoW.CanCast("Mount") && WoW.IsOutdoors)
            {
                WoW.CastSpell("Mount");
            }

            if (WoW.TargetHealthPercent == 0) return;

            if (!WoW.TargetIsEnemy &&
                WoW.HealthPercent < 100 &&
                WoW.CanCast("FlashHeal") &&
                WoW.Mana > 25)
            {
                WoW.CastSpell("FlashHeal");
                return;
            }

            if (!WoW.TargetIsEnemy) return;

            if (WoW.TargetIsCastingAndSpellIsInterruptible &&
                WoW.TargetPercentCast > 60 &&
                WoW.CanCast("Rebuke"))
            {
                WoW.CastSpell("Rebuke");
                return;
            }

            if (WoW.CanCast("ArdentDefender") && WoW.HealthPercent < 15)
            {
                Log.Write("Health < 15% using CD: [Ardent Defender]", Color.Red);
                WoW.CastSpell("ArdentDefender");
                return;
            }

            if (WoW.HealthPercent < 20 && !WoW.PlayerHasBuff("ArdentDefender"))
            {
                if (WoW.CanCast("LayOnHands") &&
                    !WoW.PlayerHasDebuff("Forbearance"))
                {
                    Log.Write("Health < 20% using CD: [Lay On Hands]", Color.Red);
                    WoW.CastSpell("LayOnHands");
                    return;
                }

                if (WoW.CanCast("DivineShield") &&
                    !WoW.PlayerHasDebuff("Forbearance") &&
                    WoW.CanCast("HandOfReckoning"))
                {
                    Log.Write("Health < 20% using CD: [Taunt & Divine Shield]", Color.Red);
                    WoW.CastSpell("HandOfReckoning");
                    WoW.CastSpell("DivineShield");
                    return;
                }
            }

            if (WoW.HealthPercent < 50)
            {
                if (WoW.CanCast("GuardianOfAncientKings") &&
                    !WoW.PlayerHasBuff("ArdentDefender"))
                {
                    Log.Write("Health < 50% using CD: [Guardian Of Ancient Kings]", Color.Red);
                    WoW.CastSpell("GuardianOfAncientKings");
                    return;
                }
            }

            if (!WoW.HasTarget) return;

            if (WoW.HasBossTarget &&
                WoW.CanCast("AvengingWrath"))
            {
                WoW.CastSpell("AvengingWrath"); // Off the GCD no return needed.
            }

            if (WoW.CanCast("AvengersShield"))
            {
                WoW.CastSpell("AvengersShield");
                return;
            }

            if (WoW.HealthPercent < 100)
            {
                if (WoW.CanCast("EyeOfTyr"))
                {
                    WoW.CastSpell("EyeOfTyr");
                    return;
                }
            }

            if (WoW.CanCast("Judgment"))
            {
                WoW.CastSpell("Judgment");
                return;
            }

            if (WoW.CanCast("Consecration"))
            {
                WoW.CastSpell("Consecration");
                return;
            }

            if (WoW.CanCast("LightOfTheProtector") &&
                WoW.PlayerHasBuff("Consecration") &&
                WoW.HealthPercent < 70)
            {
                WoW.CastSpell("LightOfTheProtector");
                return;
            }

            if (WoW.CanCast("BlessedHammer") &&
                WoW.CountEnemyNPCsInRange > 1)
            {
                WoW.CastSpell("BlessedHammer");
                return;
            }

            if (WoW.CanCast("BastionOfLight") &&
                WoW.PlayerSpellCharges("ShieldOfTheRighteous") == 0 &&
                !WoW.PlayerHasBuff("ShieldOfTheRighteous"))
            {
                WoW.CastSpell("BastionOfLight");
                return;
            }

            if (WoW.CanCast("ShieldOfTheRighteous") &&
                WoW.PlayerHasBuff("Consecration") &&
                WoW.PlayerSpellCharges("ShieldOfTheRighteous") > 0 &&
                !WoW.PlayerHasBuff("ShieldOfTheRighteous"))

            {
                WoW.CastSpell("ShieldOfTheRighteous");
                return;
            }

            if (WoW.CanCast("BlessedHammer"))
            {
                WoW.CastSpell("BlessedHammer");
                return;
            }
        }
    }
}

/*
[AddonDetails.db]
AddonAuthor=WiNiFiX
AddonName=PixelMagic
WoWVersion=Legion - 70200
[SpellBook.db]
Spell,20271,Judgment,U
Spell,26573,Consecration,V
Spell,31935,AvengersShield,T
Spell,204019,BlessedHammer,B
Spell,53600,ShieldOfTheRighteous,Y
Spell,31884,AvengingWrath,A
Spell,204035,BastionOfLight,H
Spell,184092,LightOfTheProtector,F
Spell,633,LayOnHands,D9
Spell,19750,FlashHeal,OemMinus
Spell,31850,ArdentDefender,W
Spell,86659,GuardianOfAncientKings,D8
Spell,209202,EyeOfTyr,D1
Spell,642,DivineShield,D0
Spell,62124,HandOfReckoning,E
Spell,96231,Rebuke,S
Spell,34767,Mount,D4
Aura,188370,Consecration
Aura,132403,ShieldOfTheRighteous
Aura,25771,Forbearance
Aura,31850,ArdentDefender
*/
