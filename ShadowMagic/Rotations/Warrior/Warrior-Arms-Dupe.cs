// winifix@gmail.com
// ReSharper disable UnusedMember.Global


using System.Drawing;
using System.Windows.Forms;
using ShadowMagic.Helpers;

namespace ShadowMagic.Rotation
{
    public class ArmsWarrior : CombatRoutine
    {
        public override string Name => "Arms Warrior";

        public override string Class => "Warrior";


        public override Form SettingsForm { get; set; }

        public override void Initialize()
        {
            Log.Write("Welcome to PixelMagic Arms");
            Log.Write("Welcome to Arms Warrior", Color.Green);
            Log.Write("Suggested build: 3122111", Color.Green);
            Log.Write("Written based on this guide : http://tinyurl.com/jjowqs7 ", Color.Green);
        }

        public override void Stop()
        {
        }

        public override void Pulse()
        {
            if (combatRoutine.Type == RotationType.SingleTarget) // Do Single Target Stuff here
            {
                if (!WoW.PlayerHasBuff("Battle Cry"))
                {
                    if (WoW.TargetHealthPercent > 20 && WoW.IsInCombat && WoW.TargetIsEnemy && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling)
                    {
                        if (WoW.CanCast("Focused Rage") && WoW.WasLastCasted("Charge"))
                        {
                            WoW.CastSpell("Focused Rage");
                        }
                        if (WoW.CanCast("Focused Rage") && WoW.WasLastCasted("Colossus Smash"))
                        {
                            WoW.CastSpell("Focused Rage");
                        }
                        if (WoW.CanCast("Focused Rage") && WoW.Rage >= 105)
                        {
                            WoW.CastSpell("Focused Rage");
                        }
                        if (WoW.CanCast("Colossus Smash") && !WoW.IsSpellOnCooldown("Colossus Smash") && !WoW.PlayerHasBuff("Shattered Defenses"))
                        {
                            WoW.CastSpell("Colossus Smash");
                            return;
                        }
                        if (WoW.CanCast("Mortal Strike") && !WoW.IsSpellOnCooldown("Mortal Strike"))
                        {
                            WoW.CastSpell("Mortal Strike");
                            return;
                        }

                        if (WoW.CanCast("Slam") && WoW.Rage >= 32 && WoW.IsSpellOnCooldown("Colossus Smash") && WoW.IsSpellOnCooldown("Mortal Strike"))
                        {
                            WoW.CastSpell("Slam");
                            return;
                        }
                        if (WoW.CanCast("Warbreaker") && !WoW.IsSpellOnCooldown("Battle Cry") && !WoW.TargetHasDebuff("Colossus Smash"))
                        {
                            WoW.CastSpell("Warbreaker");
                            return;
                        }
                    }
                    if (WoW.TargetHealthPercent < 20 && WoW.IsInCombat && WoW.TargetIsEnemy && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling)

                    {
                        if (WoW.CanCast("Focused Rage") && WoW.WasLastCasted("Charge"))
                        {
                            WoW.CastSpell("Focused Rage");
                        }
                        if (WoW.CanCast("Focused Rage") && WoW.Rage > 105)
                        {
                            WoW.CastSpell("Focused Rage");
                        }
                        if (WoW.CanCast("Mortal Strike") && WoW.PlayerHasBuff("Focused Rage") && WoW.PlayerBuffStacks("Focused Rage") == 3)
                        {
                            WoW.CastSpell("Mortal Strike");
                            return;
                        }
                        if (WoW.CanCast("Colossus Smash") && !WoW.IsSpellOnCooldown("Colossus Smash") && !WoW.PlayerHasBuff("Shattered Defenses"))
                        {
                            WoW.CastSpell("Colossus Smash");
                            return;
                        }
                        if (WoW.CanCast("Execute") && WoW.Rage == 18 && WoW.PlayerHasBuff("Precise Strikes"))
                        {
                            WoW.CastSpell("Execute");
                            return;
                        }
                        if (WoW.CanCast("Execute") && WoW.Rage == 38 && !WoW.PlayerHasBuff("Precise Strikes"))
                        {
                            WoW.CastSpell("Execute");
                            return;
                        }
                        if (WoW.CanCast("Slam") && WoW.Rage > 32 && WoW.IsSpellOnCooldown("Colossus Smash") && WoW.IsSpellOnCooldown("Mortal Strike"))
                        {
                            WoW.CastSpell("Slam");
                            return;
                        }
                        if (WoW.CanCast("Warbreaker") && !WoW.IsSpellOnCooldown("Battle Cry") && !WoW.TargetHasDebuff("Colossus Smash"))
                        {
                            WoW.CastSpell("Warbreaker");
                            return;
                        }
                    }
                }
                if (WoW.PlayerHasBuff("Battle Cry"))
                {
                    if (WoW.TargetHealthPercent > 20 && WoW.IsInCombat && WoW.TargetIsEnemy && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling)

                    {
                        if (WoW.CanCast("Focused Rage") && WoW.PlayerBuffStacks("Focused Rage") < 3)
                        {
                            WoW.CastSpell("Focused Rage");
                        }

                        if (WoW.CanCast("Colossus Smash") && !WoW.IsSpellOnCooldown("Colossus Smash") && !WoW.PlayerHasBuff("Shattered Defenses"))
                        {
                            WoW.CastSpell("Colossus Smash");
                            return;
                        }
                        if (WoW.CanCast("Mortal Strike") && WoW.PlayerHasBuff("Shattered Defenses") && !WoW.IsSpellOnCooldown("Mortal Strike") && WoW.PlayerBuffStacks("Focused Rage") == 3)
                        {
                            WoW.CastSpell("Mortal Strike");
                            return;
                        }
                        if (WoW.CanCast("Slam") && WoW.IsSpellOnCooldown("Colossus Smash") && WoW.IsSpellOnCooldown("Mortal Strike"))
                        {
                            WoW.CastSpell("Slam");
                            return;
                        }
                    }
                    if (WoW.TargetHealthPercent < 20 && WoW.IsInCombat && WoW.TargetIsEnemy && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling)

                    {
                        if (WoW.CanCast("Focused Rage") && WoW.PlayerBuffStacks("Focused Rage") < 3)
                        {
                            WoW.CastSpell("Focused Rage");
                        }

                        if (WoW.CanCast("Colossus Smash") && !WoW.TargetHasDebuff("Colossus Smash") && !WoW.IsSpellOnCooldown("Colossus Smash"))
                        {
                            WoW.CastSpell("Colossus Smash");
                            return;
                        }
                        if (WoW.CanCast("Mortal Strike") && !WoW.IsSpellOnCooldown("Mortal Strike") && (WoW.PlayerBuffStacks("Focused Rage") == 3))
                        {
                            WoW.CastSpell("Mortal Strike");
                            return;
                        }
                        if (WoW.CanCast("Execute"))
                        {
                            WoW.CastSpell("Execute");
                            return;
                        }
                    }
                }
            }


            if (combatRoutine.Type == RotationType.AOE)

                if (combatRoutine.Type == RotationType.SingleTargetCleave)
                {
                    // Do Single Target Cleave stuff here if applicable else ignore this one
                }
        }
    }
}

/*
[AddonDetails.db]
AddonAuthor=Dupe
AddonName=PixelMagic
WoWVersion=Legion - 70300
[SpellBook.db]
Spell,12294,Mortal Strike,D1
Spell,167105,Colossus Smash,D2
Spell,1464,Slam,D3
Spell,207982,Focused Rage,D4
Spell,1719,Battle Cry,F2
Spell,107574,Avatar,F4
Spell,163201,Execute,D5
Spell,100,Charge,E
Spell,209577,Warbreaker,D6
Aura,208086,Colossus Smash
Aura,209574,Shattered Defenses
Aura,1719,Battle Cry
Aura,207982,Focused Rage
Aura,209492,Precise Strikes
*/