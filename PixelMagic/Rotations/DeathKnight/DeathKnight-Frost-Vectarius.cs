// winifix@gmail.com
// ReSharper disable UnusedMember.Global


using System.Drawing;
using System.Windows.Forms;
using PixelMagic.Helpers;

namespace PixelMagic.Rotation
{
    public class DKFrost : CombatRoutine
    {
        public override string Name => "Frost DK";

        public override string Class => "Deathknight";

        public override Form SettingsForm { get; set; }

        public override void Initialize()
        {
            Log.Write("Welcome to PixelMagic Frost");
            Log.Write("Welcome to the Frost DK Rotation by Vectarius", Color.Green);
        }

        public override void Stop()
        {
        }

        public override void Pulse() // Updated for Legion (tested and working for single target)
        {
            if (combatRoutine.Type == RotationType.SingleTarget) // Do Single Target Stuff here
            {
                if (WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsInCombat)
                {
                    if (WoW.CanCast("Frost Strike") && WoW.PlayerHasBuff("Icy Talons") && (WoW.PlayerBuffTimeRemaining("Icy Talons") >= 2) && WoW.RunicPower >= 20)
                    {
                        WoW.CastSpell("Frost Strike");
                        return;
                    }
                    if (WoW.CanCast("Frost Strike") && WoW.RunicPower >= 80)
                    {
                        WoW.CastSpell("Frost Strike");
                        return;
                    }
                    if (WoW.CanCast("Howling Blast") && !WoW.TargetHasDebuff("Frost Fever") && (WoW.CurrentRunes >= 1))
                    {
                        WoW.CastSpell("Howling Blast");
                        return;
                    }
                    if (WoW.CanCast("Howling Blast") && WoW.TargetHasDebuff("Frost Fever") && WoW.PlayerBuffTimeRemaining("Frost Fever") >= 5 && (WoW.CurrentRunes >= 1))
                    {
                        WoW.CastSpell("Howling Blast");
                        return;
                    }
                    if (WoW.CanCast("Howling Blast") && WoW.PlayerHasBuff("Rime") && !WoW.PlayerHasBuff("Obliteration") && (WoW.CurrentRunes >= 1))
                    {
                        WoW.CastSpell("Howling Blast");
                        return;
                    }
                    if (WoW.CanCast("Obliteration") && (WoW.RunicPower >= 25) && (WoW.CurrentRunes >= 2))
                    {
                        WoW.CastSpell("Obliteration");
                        return;
                    }
                    if (WoW.CanCast("Frost Strike") && WoW.PlayerHasBuff("Obliteration") && (WoW.RunicPower >= 25))
                    {
                        WoW.CastSpell("Frost Strike");
                        return;
                    }
                    if (WoW.CanCast("Obliterate") && WoW.PlayerHasBuff("Killing Machine") && (WoW.CurrentRunes >= 2))
                    {
                        WoW.CastSpell("Obliterate");
                        return;
                    }
                    if (WoW.CanCast("Remorseless Winter") && (WoW.CurrentRunes >= 1))
                    {
                        WoW.CastSpell("Remorseless Winter");
                        return;
                    }
                    if (WoW.CanCast("Obliterate") && (WoW.CurrentRunes >= 2))
                    {
                        WoW.CastSpell("Obliterate");
                        return;
                    }
                    if (WoW.CanCast("Frost Strike") && (WoW.RunicPower >= 40))
                    {
                        WoW.CastSpell("Frost Strike");
                        return;
                    }
                }
            }
            if (combatRoutine.Type == RotationType.AOE)
            {
                if (WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsInCombat)
                {
                    if (WoW.CanCast("Howling Blast") && !WoW.TargetHasDebuff("Frost Fever") && (WoW.CurrentRunes >= 1))
                    {
                        WoW.CastSpell("Howling Blast");
                        return;
                    }
                    if (WoW.CanCast("Howling Blast") && WoW.TargetHasDebuff("Frost Fever") && WoW.PlayerBuffTimeRemaining("Frost Fever") >= 5 && (WoW.CurrentRunes >= 1))
                    {
                        WoW.CastSpell("Howling Blast");
                        return;
                    }
                    if (WoW.CanCast("Remorseless Winter") && (WoW.CurrentRunes >= 1))
                    {
                        WoW.CastSpell("Remorseless Winter");
                        return;
                    }
                    if (WoW.CanCast("Obliterate") && (WoW.CurrentRunes >= 2))
                    {
                        WoW.CastSpell("Obliterate");
                        return;
                    }
                    if (WoW.CanCast("Frost Strike") && !WoW.PlayerHasBuff("Icy Talons") && WoW.RunicPower >= 20)
                    {
                        WoW.CastSpell("Frost Strike");
                        return;
                    }
                    if (WoW.CanCast("Frost Strike") && WoW.PlayerHasBuff("Icy Talons") && (WoW.PlayerBuffTimeRemaining("Icy Talons") >= 2) && WoW.RunicPower >= 20)
                    {
                        WoW.CastSpell("Frost Strike");
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
AddonAuthor=Vectarius
AddonName=PixelMagic
WoWVersion=Legion - 70300
[SpellBook.db]
Spell,49143,Frost Strike,D1
Spell,207256,Obliteration,D2
Spell,49184,Howling Blast,D3
Spell,49020,Obliterate,D4
Spell,196770,Remorseless Winter,D5
Aura,94879,Icy Talons
Aura,55095,Frost Fever
Aura,59057,Rime
Aura,51128,Killing Machine
Aura,207256,Obliteration
*/