// winifix@gmail.com
// ReSharper disable UnusedMember.Global


using System.Drawing;
using System.Windows.Forms;
using PixelMagic.Helpers;

namespace PixelMagic.Rotation
{
    public class DKUnholy : CombatRoutine
    {
        public override string Name => "Unholy DK";

        public override string Class => "Deathknight";

        public override Form SettingsForm { get; set; }

        public override void Initialize()
        {
            Log.Write("Welcome to PixelMagic Unholy");
            Log.Write("Welcome to PixelMagic Unholy", Color.Green);
        }

        public override void Stop()
        {
        }

        public override void Pulse() // Updated for Legion (tested and working for single target)
        {
            if (combatRoutine.Type == RotationType.SingleTarget) // Do Single Target Stuff here
            {
                Log.Write("Runes: " + WoW.CurrentRunes);
                Log.Write("Health: " + WoW.HealthPercent);

                if (WoW.HasTarget && WoW.TargetIsEnemy)
                {
                    if (!WoW.TargetHasDebuff("Virulent Plague") && WoW.CurrentRunes >= 1 && WoW.CanCast("Outbreak", true, false, true))
                    {
                        WoW.CastSpell("Outbreak");
                    }
                    if (WoW.CanCast("Dark Transformation", true, true, true))
                    {
                        WoW.CastSpell("Dark Transformation");
                    }
                    if ((WoW.CanCast("Death Coil") && (WoW.RunicPower >= 80)) || (WoW.PlayerHasBuff("Sudden Doom") && WoW.IsSpellOnCooldown("Dark Arbiter")))
                    {
                        WoW.CastSpell("Death Coil");
                    }
                    if (WoW.CanCast("Festering Strike", true, true, true) && WoW.TargetDebuffStacks("Festering Wound") <= 4)
                    {
                        WoW.CastSpell("Festering Strike");
                    }
                    if (WoW.CanCast("Clawing Shadows") && WoW.CurrentRunes >= 3)
                    {
                        WoW.CastSpell("Clawing Shadows");
                    }
                }
            }
            if (combatRoutine.Type == RotationType.AOE)
            {
                // Do AOE stuff here
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
AddonAuthor=WiNiFiX
AddonName=PixelMagic
WoWVersion=Legion - 70300
[SpellBook.db]
Spell,85948,Festering Strike,D1
Spell,77575,Outbreak,D2
Spell,207311,Clawing Shadows,D3
Spell,47541,Death Coil,D4
Spell,194918,Blighted Rune Weapon,D5
Spell,63560,Dark Transformation,D6
Spell,207349,Dark Arbiter,Q
Aura,81340,Sudden Doom
Aura,194310,Festering Wound
Aura,191587,Virulent Plague
*/