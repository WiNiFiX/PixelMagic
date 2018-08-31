// winifix@gmail.com
// ReSharper disable UnusedMember.Global
// ReSharper disable ConvertPropertyToExpressionBody

using System.Drawing;
using System.Windows.Forms;
using PixelMagic.Helpers;

namespace PixelMagic.Rotation
{
    public class FrostMage : CombatRoutine
    {
        public override string Name
        {
            get { return "Frost Mage"; }
        }

        public override string Class
        {
            get { return "Mage"; }
        }

        public override Form SettingsForm { get; set; }


        public override void Initialize()
        {
            Log.Write("Welcome to Haste/Crit Frost Mage", Color.Green);
            Log.Write("Suggested build: 3132121", Color.Green);
        }

        public override void Stop()
        {
        }

        public override void Pulse()
        {
            if (combatRoutine.Type == RotationType.SingleTarget) // Do Single Target Stuff here
            {
                if (WoW.CanCast("Summon Water Elemental") && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling && !WoW.HasPet)
                {
                    WoW.CastSpell("Summon Water Elemental");
                    return;
                }

                if (WoW.HasTarget && WoW.IsInCombat && WoW.TargetIsEnemy && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling && !WoW.PlayerHasBuff("Invisibility"))

                {
                    //Movement Control
                    if (WoW.CanCast("Ice Floes") && WoW.IsMoving && !WoW.PlayerHasBuff("Ice Floes"))
                    {
                        WoW.CastSpell("Ice Floes");
                    }
                    //Insta-Cast Flurry on Proc and ice lance for shatter. 
                    if (WoW.CanCast("Flurry") && WoW.PlayerHasBuff("Brain Freeze"))
                    {
                        WoW.CastSpell("Flurry");
                    }
                    if (WoW.CanCast("Ice Lance") && WoW.WasLastCasted("Flurry") && WoW.PlayerHasBuff("Icy Veins"))
                    {
                        WoW.CastSpell("Ice Lance");
                    }
                    //RoP Control
                    if (WoW.CanCast("Rune of Power") && !WoW.PlayerHasBuff("Rune of Power") && WoW.SpellCooldownTimeRemaining("Icy Veins") >= 120)
                    {
                        WoW.CastSpell("Rune of Power");
                        return;
                    }
                    //Ray of Frost control
                    if (WoW.CanCast("Ray of Frost") && WoW.PlayerHasBuff("Rune of Power") && WoW.PlayerSpellCharges("Rune of Power") == 0 && WoW.SpellCooldownTimeRemaining("Icy Veins") >= 60)
                    {
                        WoW.CastSpell("Ray of Frost");
                    }

                    // FoF Creation
                    if (WoW.PlayerBuffStacks("Chain Reaction") >= 1 && WoW.SpellCooldownTimeRemaining("Icy Veins") >= 30 && !WoW.WasLastCasted("Flurry"))
                    {
                        if (WoW.CanCast("Frozen Orb") && !WoW.IsSpellOnCooldown("Frozen Orb") && WoW.SpellCooldownTimeRemaining("Icy Veins") >= 60)
                        {
                            WoW.CastSpell("Frozen Orb");
                        }
                        if (WoW.CanCast("Frozen Touch") && WoW.PlayerBuffStacks("Fingers of Frost") <= 1 && WoW.IsSpellOnCooldown("Frozen Orb") && !WoW.WasLastCasted("Frozen Orb"))
                        {
                            WoW.CastSpell("Frozen Touch");
                        }

                        if (WoW.CanCast("Ebonbolt") && !WoW.PlayerHasBuff("Fingers of Frost") && !WoW.PlayerHasBuff("Brain Freeze") && WoW.SpellCooldownTimeRemaining("Icy Veins") >= 45)
                        {
                            WoW.CastSpell("Ebonbolt");
                            return;
                        }
                        //Waterjet on cooldown within parameters		
                        if (WoW.CanCast("Water Jet") && WoW.PlayerBuffStacks("Fingers of Frost") <= 1 && !WoW.WasLastCasted("Frozen Touch") && !WoW.WasLastCasted("Frozen Orb") &&
                            WoW.IsSpellOnCooldown("Frozen Orb") && WoW.IsSpellOnCooldown("Frozen Touch"))
                        {
                            WoW.CastSpell("Water Jet");
                        }
                        if (WoW.CanCast("Frostbolt") && WoW.TargetHasDebuff("Water Jet"))
                        {
                            WoW.CastSpell("Frostbolt");
                            return;
                        }
                    }
                    //Ice Lance Control
                    if (WoW.CanCast("Ice Lance") && WoW.WasLastCasted("Frostbolt") && WoW.PlayerHasBuff("Chain Reaction") && WoW.PlayerBuffStacks("Chain Reaction") >= 1 &&
                        WoW.PlayerHasBuff("Fingers of Frost") && WoW.PlayerBuffStacks("Fingers of Frost") >= 2)
                    {
                        WoW.CastSpell("Ice Lance");
                    }
                    if (WoW.CanCast("Ice Lance") && WoW.PlayerBuffStacks("Fingers of Frost") == 3)
                    {
                        WoW.CastSpell("Ice Lance");
                    }

                    if (WoW.CanCast("Ice Lance") && WoW.PlayerBuffStacks("Fingers of Frost") >= 1 && WoW.WasLastCasted("Ice Lance"))
                    {
                        WoW.CastSpell("Ice Lance");
                    }
                    if (WoW.CanCast("Ice Lance") && !WoW.PlayerHasBuff("Brain Freeze") && WoW.WasLastCasted("Flurry"))
                    {
                        WoW.CastSpell("Ice Lance");
                    }
                    if (WoW.CanCast("Ice Lance") && WoW.PlayerBuffStacks("Chain Reaction") >= 1 && WoW.PlayerHasBuff("Fingers of Frost") && WoW.PlayerBuffStacks("Fingers of Frost") >= 1)
                    {
                        WoW.CastSpell("Ice Lance");
                        return;
                    }
                    //Frostbolt Control
                    if (!WoW.PlayerHasBuff("Brain Freeze") && !WoW.WasLastCasted("Flurry"))
                    {
                        if (WoW.CanCast("Frostbolt") && !WoW.PlayerHasBuff("Fingers of Frost") && !WoW.WasLastCasted("Frostbolt"))
                        {
                            WoW.CastSpell("Frostbolt");
                            return;
                        }

                        if (WoW.CanCast("Frostbolt") && WoW.PlayerBuffStacks("Chain Reaction") <= 2 && !WoW.WasLastCasted("Frostbolt"))
                        {
                            WoW.CastSpell("Frostbolt");
                            return;
                        }
                        if (WoW.CanCast("Frostbolt") && WoW.PlayerHasBuff("Chain Reaction") && WoW.PlayerBuffStacks("Chain Reaction") == 3 && WoW.PlayerBuffTimeRemaining("Chain Reaction") <= 2)
                        {
                            WoW.CastSpell("Frostbolt");
                            return;
                        }
                    }

                    //Frost Bomb - i dont use
                    //if	((WoW.CanCast("Frost Bomb")&&WoW.PlayerHasBuff("Fingers of Frost")&&WoW.PlayerBuffStacks("Fingers of Frost") == 2)&&!WoW.TargetHasDebuff("Frost Bomb")&&!WoW.WasLastCasted("Ice Lance")&&!WoW.WasLastCasted("Rune of Power"))
                    //		{
                    //		WoW.CastSpell("Frost Bomb");
                    //		return;
                    //		}
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
AddonName=badgggggggggerui
WoWVersion=Legion - 70300
[SpellBook.db]
Spell,116,Frostbolt,D1
Spell,84714,Frozen Orb,D6
Spell,30455,Ice Lance,D2
Spell,44614,Flurry,Z
Spell,190356,Blizzard,D5
Spell,214634,Ebonbolt,K
Spell,12472,Icy Veins,X
Spell,116011,Rune of Power,D3
Spell,31687,Summon Water Elemental,None
Spell,11426,Ice Barrier,S
Spell,205030,Frozen Touch,None
Spell,112948,Frost Bomb,None
Spell,108839,Ice Floes,None
Spell,135029,Water Jet,None
Spell,45438,Ice Block,D8
Spell,205021,Ray of Frost,None
Aura,195418,Chain Reaction
Aura,45438,Ice Block
Aura,112948,Frost Bomb
Aura,135029,Water Jet
Aura,44544,Fingers of Frost
Aura,190446,Brain Freeze
Aura,76613,Mastery: Icicles
Aura,66,Invisibility
Aura,116014,Rune of Power
Aura,11426,Ice Barrier
Aura,108839,Ice Floes
*/