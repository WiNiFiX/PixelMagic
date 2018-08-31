// winifix@gmail.com
// ReSharper disable UnusedMember.Global


using System.Drawing;
using System.Windows.Forms;
using PixelMagic.Helpers;

namespace PixelMagic.Rotation
{
    public class AssassinationV : CombatRoutine
    {
        public override string Name => "Rogue-Assassination";

        public override string Class => "Rogue";

        public override Form SettingsForm { get; set; }

        public override void Initialize()
        {
            Log.Write("Welcome to Rogue-Assassination", Color.Green);
            Log.Write("Suggested build: 2112111");
        }

        public override void Stop()
        {
        }

        public override void Pulse()
        {
            if (combatRoutine.Type == RotationType.SingleTarget) // Do Single Target Stuff here
            {
                if (WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsInCombat)
                {
                    if (WoW.CanCast("Rupture") && WoW.CurrentComboPoints >= 5 && WoW.Energy >= 25 &&
                        (!WoW.TargetHasDebuff("Rupture") || WoW.TargetHasDebuff("Rupture") && WoW.TargetDebuffTimeRemaining("Rupture") < 5))
                    {
                        WoW.CastSpell("Rupture");
                        return;
                    }
                    if (WoW.CanCast("Vendetta") && !WoW.IsSpellOnCooldown("Vendetta") && WoW.Energy <= 50)
                    {
                        WoW.CastSpell("Vendetta");
                        return;
                    }
                    if (WoW.CanCast("Vanish") && WoW.CurrentComboPoints >= 6 && WoW.Energy >= 25 &&
                        (!WoW.TargetHasDebuff("Rupture") || WoW.TargetHasDebuff("Rupture") && WoW.TargetDebuffTimeRemaining("Rupture") < 5))
                    {
                        WoW.CastSpell("Vanish");
                        return;
                    }
                    if (WoW.CanCast("Rupture") && WoW.CurrentComboPoints >= 6 && WoW.Energy >= 25 && WoW.PlayerHasBuff("Stealth") &&
                        (WoW.TargetHasDebuff("Rupture") && WoW.TargetDebuffTimeRemaining("Rupture") < 5 || !WoW.TargetHasDebuff("Rupture")))
                    {
                        WoW.CastSpell("Rupture");
                        return;
                    }
                    if (WoW.CanCast("Garrote") && WoW.Energy >= 45 && (!WoW.TargetHasDebuff("Garrote") || WoW.TargetHasDebuff("Garrote") && WoW.TargetDebuffTimeRemaining("Garrote") < 3))
                    {
                        WoW.CastSpell("Garrote");
                        return;
                    }
                    if (WoW.CanCast("Kingsbane") && !WoW.IsSpellOnCooldown("Kingsbane") && (WoW.SpellCooldownTimeRemaining("Vendetta") >= 10 || WoW.TargetHasDebuff("Vendetta")))
                    {
                        WoW.CastSpell("Kingsbane");
                        return;
                    }
                    if (WoW.CanCast("Envenom") && WoW.Energy >= 35 && !WoW.PlayerHasBuff("Envenom") && WoW.CurrentComboPoints >= 3 && WoW.TargetHasDebuff("Rupture") &&
                        WoW.TargetDebuffTimeRemaining("Rupture") >= 10 && (!WoW.PlayerHasBuff("Elaborate Planning") || WoW.PlayerBuffTimeRemaining("Elaborate Planning") <= 2.0))
                    {
                        WoW.CastSpell("Envenom");
                        return;
                    }
                    if (WoW.CanCast("Mutilate") && WoW.Energy >= 55 && WoW.CurrentComboPoints <= 5 && WoW.PlayerBuffTimeRemaining("Elaborate Planning") >= 2.1)
                    {
                        WoW.CastSpell("Mutilate");
                        return;
                    }
                }
            }

            if (combatRoutine.Type == RotationType.AOE || combatRoutine.Type == RotationType.SingleTargetCleave) // Do AoE Target Stuff here
            {
                if (WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsInCombat)
                {
                    if (WoW.CanCast("Rupture") && WoW.CurrentComboPoints >= 5 && WoW.Energy >= 25 &&
                        (!WoW.TargetHasDebuff("Rupture") || WoW.TargetHasDebuff("Rupture") && WoW.TargetDebuffTimeRemaining("Rupture") < 5))
                    {
                        WoW.CastSpell("Rupture");
                        return;
                    }
                    if (WoW.CanCast("Vendetta") && !WoW.IsSpellOnCooldown("Vendetta") && WoW.Energy <= 50)
                    {
                        WoW.CastSpell("Vendetta");
                        return;
                    }
                    if (WoW.CanCast("Vanish") && WoW.CurrentComboPoints >= 6 && WoW.Energy >= 25 &&
                        (!WoW.TargetHasDebuff("Rupture") || WoW.TargetHasDebuff("Rupture") && WoW.TargetDebuffTimeRemaining("Rupture") < 5))
                    {
                        WoW.CastSpell("Vanish");
                        return;
                    }
                    if (WoW.CanCast("Rupture") && WoW.CurrentComboPoints >= 6 && WoW.Energy >= 25 && WoW.PlayerHasBuff("Stealth") &&
                        (WoW.TargetHasDebuff("Rupture") && WoW.TargetDebuffTimeRemaining("Rupture") < 5 || !WoW.TargetHasDebuff("Rupture")))
                    {
                        WoW.CastSpell("Rupture");
                        return;
                    }
                }
                if (WoW.CanCast("Garrote") && WoW.Energy >= 45 && (!WoW.TargetHasDebuff("Garrote") || WoW.TargetHasDebuff("Garrote") && WoW.TargetDebuffTimeRemaining("Garrote") < 3))
                {
                    WoW.CastSpell("Garrote");
                    return;
                }
                if (WoW.CanCast("Kingsbane") && !WoW.IsSpellOnCooldown("Kingsbane") && (WoW.SpellCooldownTimeRemaining("Vendetta") >= 10 || WoW.TargetHasDebuff("Vendetta")))
                {
                    WoW.CastSpell("Kingsbane");
                    return;
                }
                if (WoW.CanCast("Envenom") && WoW.Energy >= 35 && !WoW.PlayerHasBuff("Envenom") && WoW.CurrentComboPoints >= 3 && WoW.TargetHasDebuff("Rupture") &&
                    WoW.TargetDebuffTimeRemaining("Rupture") >= 10 && (!WoW.PlayerHasBuff("Elaborate Planning") || (WoW.PlayerBuffTimeRemaining("Elaborate Planning") <= 2.0)))
                {
                    WoW.CastSpell("Envenom");
                    return;
                }
                if (WoW.CanCast("Fan of Knives") && WoW.Energy >= 35 && !WoW.TargetHasDebuff("Agonizing Poison"))
                {
                    WoW.CastSpell("Fan of Knives");
                    return;
                }
                if (WoW.CanCast("Fan of Knives") && WoW.Energy >= 35 && WoW.TargetHasDebuff("Agonizing Poison") && WoW.TargetDebuffStacks("Agonizing Poison") <= 4)
                {
                    WoW.CastSpell("Fan of Knives");
                    return;
                }
                if (WoW.CanCast("Fan of Knives") && WoW.Energy >= 35 && WoW.TargetHasDebuff("Agonizing Poison") && WoW.TargetDebuffTimeRemaining("Agonizing Poison") < 2)
                {
                    WoW.CastSpell("Fan of Knives");
                    return;
                }
                if (WoW.CanCast("Mutilate") && WoW.Energy >= 55 && WoW.CurrentComboPoints <= 5)
                {
                    WoW.CastSpell("Mutilate");
                }
            }
        }
    }
}

/*
[AddonDetails.db]
AddonAuthor=Vectarius
AddonName=PMx
WoWVersion=Legion - 70300
[SpellBook.db]
Spell,1943,Rupture,D2
Spell,79140,Vendetta,D6
Spell,1856,Vanish,V
Spell,1329,Mutilate,D1
Spell,703,Garrote,R
Spell,192759,Kingsbane,D3
Spell,32645,Envenom,Q
Spell,51723,Fan Of Knives,D5
Aura,1943,Rupture
Aura,1784,Stealth
Aura,703,Garrote
Aura,32645,Envenom
Aura,200802,Agonizing Poison
Aura,193641,Elaborate Planning
Aura,79140,Vendetta
*/
