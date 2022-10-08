// winifix@gmail.com
// ReSharper disable UnusedMember.Global

// Reccomended Talent Build 1121112

using System.Drawing;
using System.Windows.Forms;
using ShadowMagic.Helpers;

namespace ShadowMagic.Rotation
{
    public class ArcaneMage : CombatRoutine
    {
        public override string Name => "Arcane Mage";

        public override string Class => "Mage";

        public override Form SettingsForm { get; set; }

        public override void Initialize()
        {
            Log.Write("Welcome to Arcane Mage", Color.Green);
            Log.Write("Welcome to PixelMagic Arcane");
        }

        public override void Stop()
        {
        }

        public override void Pulse()
        {
            if (combatRoutine.Type == RotationType.SingleTarget) // Do Single Target Stuff here
            {
                if (WoW.CanCast("Arcane Familiar") && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling && !WoW.PlayerHasBuff("Arcane Familiar"))
                {
                    WoW.CastSpell("Arcane Familiar");
                    return;
                }

                if (WoW.HasTarget && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling && !WoW.PlayerHasBuff("Greater Invisibility"))

                {
                    if (WoW.CanCast("Ice Floes") && WoW.IsMoving && !WoW.PlayerHasBuff("Ice Floes"))
                    {
                        WoW.CastSpell("Ice Floes");
                        return;
                    }
                    if (WoW.CanCast("Evocation") && WoW.Mana <= 5)
                    {
                        WoW.CastSpell("Evocation");
                        return;
                    }

                    if (WoW.CanCast("Nether Tempest") && WoW.CurrentArcaneCharges == 4 && !WoW.TargetHasDebuff("Nether Tempest"))
                    {
                        WoW.CastSpell("Nether Tempest");
                        return;
                    }
                    if (WoW.CanCast("Arcane Blast") && WoW.CurrentArcaneCharges < 4)
                    {
                        WoW.CastSpell("Arcane Blast");
                        return;
                    }

                    if (WoW.CanCast("Supernova"))
                    {
                        WoW.CastSpell("Supernova");
                        return;
                    }

                    if (WoW.CanCast("Arcane Missiles") && WoW.PlayerHasBuff("Arcane Missiles") && WoW.CurrentArcaneCharges == 4)
                    {
                        WoW.CastSpell("Arcane Missiles");
                        return;
                    }

                    if (WoW.CanCast("Arcane Barrage") && !WoW.PlayerHasBuff("Arcane Missiles") && WoW.CurrentArcaneCharges == 4)
                    {
                        WoW.CastSpell("Arcane Barrage");
                        return;
                    }
                }
            }
            if (combatRoutine.Type == RotationType.AOE)
            {
                //Easiest and most DPS increasing way to dps as Arcane is to have a burn phase and a conserv phase.
                //To Simplify things, i turned AOE rotation into Burn.

                if (WoW.CanCast("Arcane Familiar") && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling && !WoW.PlayerHasBuff("Arcane Familiar"))
                {
                    WoW.CastSpell("Arcane Familiar");
                    return;
                }

                if (WoW.HasTarget && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling && !WoW.PlayerHasBuff("Greater Invisibility"))
                {
                    if (WoW.CanCast("Ice Floes") && WoW.IsMoving && !WoW.PlayerHasBuff("Ice Floes"))
                    {
                        WoW.CastSpell("Ice Floes");
                        return;
                    }
                    if (WoW.CanCast("Nether Tempest") && WoW.CurrentArcaneCharges == 4 && !WoW.TargetHasDebuff("Nether Tempest"))
                    {
                        WoW.CastSpell("Nether Tempest");
                        return;
                    }
                    if (WoW.CanCast("Rune of Power") && !WoW.PlayerHasBuff("Rune Of Power") && WoW.CurrentArcaneCharges == 4)
                    {
                        WoW.CastSpell("Rune of Power");
                        return;
                    }
                    if (WoW.CanCast("Arcane Power") && WoW.PlayerHasBuff("Rune Of Power"))
                    {
                        WoW.CastSpell("Arcane Power");
                        return;
                    }
                    if (WoW.CanCast("Mark Of Aluneth") && WoW.PlayerHasBuff("Rune of Power"))
                    {
                        WoW.CastSpell("Mark Of Aluneth");
                        return;
                    }
                    if (WoW.CanCast("Supernova"))
                    {
                        WoW.CastSpell("Supernova");
                        return;
                    }
                    if (WoW.CanCast("Arcane Blast") && !WoW.PlayerHasBuff("Arcane Missiles"))
                    {
                        WoW.CastSpell("Arcane Blast");
                        return;
                    }
                    if (WoW.CanCast("Arcane Missiles") && WoW.PlayerHasBuff("Arcane Missiles"))
                    {
                        WoW.CastSpell("Arcane Missiles");
                        return;
                    }
                }
            }
            if (combatRoutine.Type == RotationType.SingleTargetCleave)
            {
                // Do Single Target Cleave stuff here if applicable else ignore this one
            }
        }
    }
}

/*
[AddonDetails.db]
AddonAuthor=Dupe-aka-Lesion
AddonName=PixelMagic
WoWVersion=Legion - 70300
[SpellBook.db]
Spell,11426,Ice Barrier,S
Spell,205022,Arcane Familiar,D6
Spell,30451,Arcane Blast,D1
Spell,114923,Nether Tempest,E
Spell,12051,Evocation,F
Spell,5143,Arcane Missiles,D3
Spell,157980,Supernova,D7
Spell,116011,Rune of Power,Q
Spell,224968,Mark Of Aluneth,D4 
Spell,44425,Arcane Barrage,D2
Spell,12042,Arcane Power,A
Spell,157801,Slow,X
Spell,108839,Ice Floes,D8
Aura,110960,Greater Invisibility
Aura,157801,Slow
Aura,205022,Arcane Familiar
Aura,114923,Nether Tempest
Aura,79683,Arcane Missiles
Aura,116014,Rune of Power
Aura,11426,Ice Barrier
Aura,108839,Ice Floes
*/