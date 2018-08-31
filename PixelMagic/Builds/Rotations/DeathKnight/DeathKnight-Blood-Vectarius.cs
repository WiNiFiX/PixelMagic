// winifix@gmail.com
// ReSharper disable UnusedMember.Global


using System.Drawing;
using System.Windows.Forms;
using PixelMagic.Helpers;

namespace PixelMagic.Rotation
{
    public class DKBlood : CombatRoutine
    {
        public override string Name => "Blood DK";

        public override string Class => "Deathknight";

        public override Form SettingsForm { get; set; }

        public override void Initialize()
        {
            Log.Write("Welcome to PixelMagic Blood");
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
                    if (WoW.CanCast("Marrowrend") && WoW.PlayerBuffTimeRemaining("Bone Shield") >= 3 && WoW.CurrentRunes >= 1 && !WoW.IsSpellOnCooldown("Marrowrend"))
                    {
                        WoW.CastSpell("Marrowrend");
                        return;
                    }
                    if (WoW.CanCast("Blood Boil") && !WoW.TargetHasDebuff("Blood Plague") && !WoW.IsSpellOnCooldown("Blood Boil"))
                    {
                        WoW.CastSpell("Blood Boil");
                        return;
                    }
                    if (WoW.CanCast("Dancing Rune Weapon") && WoW.HealthPercent < 50)
                    {
                        WoW.CastSpell("Dancing Rune Weapon");
                        return;
                    }
                    if (WoW.CanCast("Death Strike") && WoW.RunicPower >= 45 && WoW.HealthPercent < 85 && !WoW.IsSpellOnCooldown("Death Strike"))
                    {
                        WoW.CastSpell("Death Strike");
                        return;
                    }
                    if (WoW.CanCast("Marrowrend") && WoW.PlayerBuffStacks("Bone Shield") >= 6 && !WoW.IsSpellOnCooldown("Marrowrend"))
                    {
                        WoW.CastSpell("Marrowrend");
                        return;
                    }
                    if (WoW.CanCast("Heart Strike") && WoW.CurrentRunes >= 3 || WoW.RunicPower <= 45 && !WoW.IsSpellOnCooldown("Heart Strike"))
                    {
                        WoW.CastSpell("Heart Strike");
                        return;
                    }
                    if (WoW.CanCast("Consumption") && !WoW.IsSpellOnCooldown("Consumption"))
                    {
                        WoW.CastSpell("Consumption");
                        return;
                    }
                    if (WoW.CanCast("Blood Boil") && !WoW.IsSpellOnCooldown("Blood Boil"))
                    {
                        WoW.CastSpell("Blood Boil");
                        return;
                    }
                }
            }
            if (combatRoutine.Type == RotationType.AOE)
            {
                if (WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsInCombat)
                {
                    if (WoW.CanCast("Anti-Magic Shell") && WoW.HealthPercent < 50 && !WoW.IsSpellOnCooldown("Anti-Magic Shell"))
                    {
                        WoW.CastSpell("Anti-Magic Shell");
                        return;
                    }
                    if (WoW.CanCast("Icebound Fortitude") && WoW.HealthPercent < 40 && !WoW.IsSpellOnCooldown("Icebound Fortitude"))
                    {
                        WoW.CastSpell("Icebound Fortitude");
                        return;
                    }
                    if (WoW.CanCast("Vampiric Blood") && WoW.HealthPercent < 30 && !WoW.IsSpellOnCooldown("Vampiric Blood"))
                    {
                        WoW.CastSpell("Vampiric Blood");
                        return;
                    }
                    if (WoW.CanCast("Marrowrend") && WoW.PlayerBuffTimeRemaining("Bone Shield") >= 3 && WoW.CurrentRunes >= 1 && !WoW.IsSpellOnCooldown("Marrowrend"))
                    {
                        WoW.CastSpell("Marrowrend");
                        return;
                    }
                    if (WoW.CanCast("Blood Boil") && !WoW.TargetHasDebuff("Blood Plague") && !WoW.IsSpellOnCooldown("Blood Boil"))
                    {
                        WoW.CastSpell("Blood Boil");
                        return;
                    }
                    if (WoW.CanCast("Dancing Rune Weapon") && WoW.HealthPercent < 50)
                    {
                        WoW.CastSpell("Dancing Rune Weapon");
                        return;
                    }
                    if (WoW.CanCast("Death Strike") && WoW.RunicPower >= 45 && WoW.HealthPercent < 85 && !WoW.IsSpellOnCooldown("Death Strike"))
                    {
                        WoW.CastSpell("Death Strike");
                        return;
                    }
                    if (WoW.CanCast("Marrowrend") && WoW.PlayerBuffStacks("Bone Shield") >= 6 && !WoW.IsSpellOnCooldown("Marrowrend"))
                    {
                        WoW.CastSpell("Marrowrend");
                        return;
                    }
                    if (WoW.CanCast("Heart Strike") && WoW.CurrentRunes >= 3 || WoW.RunicPower <= 45 && !WoW.IsSpellOnCooldown("Heart Strike"))
                    {
                        WoW.CastSpell("Heart Strike");
                        return;
                    }
                    if (WoW.CanCast("Consumption") && !WoW.IsSpellOnCooldown("Consumption"))
                    {
                        WoW.CastSpell("Consumption");
                        return;
                    }
                    if (WoW.CanCast("Blood Boil") && !WoW.IsSpellOnCooldown("Blood Boil"))
                    {
                        WoW.CastSpell("Blood Boil");
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
Spell,195182,Marrowrend,D1
Spell,50842,Blood Boil,D2
Spell,449998,Death Strike,D3
Spell,206930,Heart Strike,D4
Spell,205223,Consumption,D5
Spell,48707,Anti-Magic Shell,D6
Spell,55233,Vampiric Blood,D7
Spell,48792,Icebound Fortitude,D8
Spell,49028,Dancing Rune Weapon,D9
Aura,91289,Bone Shield
Aura,195740,Blood Plague
*/