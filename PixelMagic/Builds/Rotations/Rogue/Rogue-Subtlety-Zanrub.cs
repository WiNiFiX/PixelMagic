// winifix@gmail.com
// ReSharper disable UnusedMember.Global


using System.Drawing;
using System.Windows.Forms;
using PixelMagic.Helpers;

namespace PixelMagic.Rotation
{
    public class Rogue : CombatRoutine
    {
        public override string Name => "Rogue-Subtlety";

        public override string Class => "Rogue";

        public override Form SettingsForm { get; set; }

        public override void Initialize()
        {
            Log.Write("Welcome to Rogue-Subtlety", Color.Green);
            Log.Write("Suggested build: http://www.wowhead.com/talent-calc/rogue/subtlety/cTIz");
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
                    if (WoW.PlayerHasBuff("Stealth") || WoW.PlayerHasBuff("Subterfuge") || WoW.PlayerHasBuff("Shadow Dance"))
                    {
                        if (WoW.CanCast("Symbols of Death") && WoW.Energy >= 35 && (!WoW.PlayerHasBuff("Symbols of Death") || WoW.PlayerBuffTimeRemaining("Symbols of Death") <= 10))
                        {
                            WoW.CastSpell("Symbols of Death");
                            return;
                        }

                        /*	if (WoW.CanCast("Shadow Blades") && WoW.Cooldown && WoW.HasBuff("Symbols of Death"))
                            {

                                WoW.CastSpellByName("Shadow Blades");
                                return;
                            } */

                        /*	if(WoW.CanCast("Goremaw's Bite") && WoW.CurrentComboPoints <= 3 && WoW.IsSpellInRange("Goremaw's Bite"))
                            {

                                WoW.CastSpellByName("Goremaw's Bite");
                                return;
                            } */

                        if (WoW.CanCast("Nightblade") && WoW.CurrentComboPoints >= 5 && WoW.Energy >= 25 &&
                            (!WoW.TargetHasDebuff("Nightblade") || WoW.TargetDebuffTimeRemaining("Nightblade") <= 4) && WoW.IsSpellInRange("Nightblade"))
                        {
                            WoW.CastSpell("Nightblade");
                            return;
                        }

                        if (WoW.CanCast("Eviscerate") && WoW.CurrentComboPoints >= 5 && WoW.Energy >= 35 && WoW.TargetHasDebuff("Nightblade") && WoW.IsSpellInRange("Eviscerate"))

                        {
                            WoW.CastSpell("Eviscerate");
                            return;
                        }

                        if (WoW.CanCast("Shadowstrike") && WoW.CurrentComboPoints < 6 && WoW.Energy >= 40 && WoW.IsSpellInRange("Shadowstrike"))
                        {
                            WoW.CastSpell("Shadowstrike");
                            return;
                        }
                    }


                    if (WoW.CanCast("Shadow Blades") && WoW.HasTarget && WoW.PlayerHasBuff("Symbols of Death") && WoW.IsSpellInRange("Eviscerate"))
                    {
                        WoW.CastSpell("Shadow Blades");
                        return;
                    }

                    if (WoW.CanCast("Shadow Dance") && (!WoW.PlayerHasBuff("Stealth") || !WoW.PlayerHasBuff("Shadow Dance") || !WoW.PlayerHasBuff("Subterfuge")) && WoW.Energy >= 55 &&
                        (WoW.PlayerSpellCharges("Shadow Dance") == 3 && WoW.CurrentComboPoints <= 3 || WoW.PlayerSpellCharges("Shadow Dance") == 2 && WoW.CurrentComboPoints <= 1) &&
                        WoW.IsSpellInRange("Eviscerate"))
                    {
                        WoW.CastSpell("Shadow Dance");
                        return;
                    }

                    if (WoW.CanCast("Goremaw's Bite") && WoW.CurrentComboPoints <= 2 && WoW.Energy <= 50 && WoW.IsSpellInRange("Goremaw's Bite") && WoW.IsSpellInRange("Eviscerate"))
                    {
                        WoW.CastSpell("Goremaw's Bite");
                        return;
                    }

                    if (WoW.CanCast("Nightblade") && WoW.CurrentComboPoints >= 5 && WoW.Energy >= 25 && (!WoW.TargetHasDebuff("Nightblade") || WoW.TargetDebuffTimeRemaining("Nightblade") <= 4) &&
                        WoW.IsSpellInRange("Eviscerate"))
                    {
                        WoW.CastSpell("Nightblade");
                        return;
                    }

                    if (WoW.CanCast("Eviscerate") && WoW.CurrentComboPoints >= 5 && WoW.Energy >= 35 && WoW.TargetHasDebuff("Nightblade") && WoW.IsSpellInRange("Eviscerate"))
                    {
                        WoW.CastSpell("Eviscerate");
                        return;
                    }

                    if (WoW.CanCast("Backstab") && WoW.CurrentComboPoints < 6 && WoW.Energy >= 55 && WoW.IsSpellInRange("Eviscerate"))
                    {
                        WoW.CastSpell("Backstab");
                        return;
                    }
                }
            }

            if (combatRoutine.Type == RotationType.AOE || combatRoutine.Type == RotationType.SingleTargetCleave) // Do AoE Target Stuff here
            {
                if ((WoW.HasTarget || UseCooldowns) && WoW.TargetIsEnemy && WoW.IsInCombat)
                {
                    if (WoW.PlayerHasBuff("Stealth") || WoW.PlayerHasBuff("Subterfuge") || WoW.PlayerHasBuff("Shadow Dance"))
                    {
                        if (WoW.CanCast("Symbols of Death") && WoW.Energy >= 35 && (!WoW.PlayerHasBuff("Symbols of Death") || WoW.PlayerBuffTimeRemaining("Symbols of Death") <= 10))
                        {
                            WoW.CastSpell("Symbols of Death");
                            return;
                        }

                        /*	if (WoW.CanCast("Shadow Blades") && WoW.Cooldown && WoW.HasBuff("Symbols of Death"))
                            {

                                WoW.CastSpellByName("Shadow Blades");
                                return;
                            } */

                        /*	if(WoW.CanCast("Goremaw's Bite") && WoW.CurrentComboPoints <= 3 && WoW.IsSpellInRange("Goremaw's Bite"))
                            {

                                WoW.CastSpellByName("Goremaw's Bite");
                                return;
                            } */

                        if (WoW.CanCast("Nightblade") && WoW.CurrentComboPoints >= 5 && WoW.Energy >= 25 &&
                            (!WoW.TargetHasDebuff("Nightblade") || WoW.TargetDebuffTimeRemaining("Nightblade") <= 4) && WoW.IsSpellInRange("Nightblade"))
                        {
                            WoW.CastSpell("Nightblade");
                            return;
                        }

                        if (WoW.CanCast("Eviscerate") && WoW.CurrentComboPoints >= 5 && WoW.Energy >= 35 && WoW.TargetHasDebuff("Nightblade") && WoW.IsSpellInRange("Eviscerate"))

                        {
                            WoW.CastSpell("Eviscerate");
                            return;
                        }

                        if (WoW.CanCast("Shadowstrike") && WoW.CurrentComboPoints < 6 && WoW.Energy >= 40 && WoW.IsSpellInRange("Shadowstrike"))
                        {
                            WoW.CastSpell("Shadowstrike");
                            return;
                        }

                        if (WoW.CanCast("Shuriken Storm") && WoW.CurrentComboPoints < 6 && WoW.Energy >= 35 && WoW.IsSpellInRange("Eviscerate"))
                        {
                            WoW.CastSpell("Shuriken Storm");
                            return;
                        }
                    }


                    if (WoW.CanCast("Shadow Blades") && WoW.HasTarget && WoW.PlayerHasBuff("Symbols of Death") && WoW.IsSpellInRange("Eviscerate"))
                    {
                        WoW.CastSpell("Shadow Blades");
                        return;
                    }

                    if (WoW.CanCast("Shadow Dance") && (!WoW.PlayerHasBuff("Stealth") || !WoW.PlayerHasBuff("Shadow Dance") || !WoW.PlayerHasBuff("Subterfuge")) &&
                        (WoW.PlayerSpellCharges("Shadow Dance") == 3 && WoW.CurrentComboPoints <= 3 || WoW.PlayerSpellCharges("Shadow Dance") == 2 && WoW.CurrentComboPoints <= 1) &&
                        WoW.IsSpellInRange("Eviscerate"))
                    {
                        WoW.CastSpell("Shadow Dance");
                        return;
                    }

                    if (WoW.CanCast("Goremaw's Bite") && WoW.CurrentComboPoints <= 2 && WoW.Energy <= 50 && WoW.IsSpellInRange("Goremaw's Bite") && WoW.IsSpellInRange("Eviscerate"))
                    {
                        WoW.CastSpell("Goremaw's Bite");
                        return;
                    }

                    if (WoW.CanCast("Nightblade") && WoW.CurrentComboPoints >= 5 && WoW.Energy >= 25 && (!WoW.TargetHasDebuff("Nightblade") || WoW.TargetDebuffTimeRemaining("Nightblade") <= 4) &&
                        WoW.IsSpellInRange("Eviscerate"))
                    {
                        WoW.CastSpell("Nightblade");
                        return;
                    }

                    if (WoW.CanCast("Eviscerate") && WoW.CurrentComboPoints >= 5 && WoW.Energy >= 35 && WoW.TargetHasDebuff("Nightblade") && WoW.IsSpellInRange("Eviscerate"))
                    {
                        WoW.CastSpell("Eviscerate");
                        return;
                    }

                    if (WoW.CanCast("Shuriken Storm") && WoW.CurrentComboPoints < 6 && WoW.Energy >= 35 && WoW.IsSpellInRange("Eviscerate"))
                    {
                        WoW.CastSpell("Shuriken Storm");
                    }
                }
            }
        }
    }
}

/*
[AddonDetails.db]
AddonAuthor=Zanrub
AddonName=PixelMagic
WoWVersion=Legion - 70300
[SpellBook.db]
Spell,185438,Shadowstrike,D2
Spell,196819,Eviscerate,D3
Spell,212283,Symbols of Death,D1
Spell,195452,Nightblade,D4
Spell,137619,Marked for Death,D8
Spell,209782,Goremaw's Bite,F10
Spell,53,Backstab,D2
Spell,185313,Shadow Dance,F9
Spell,121471,Shadow Blades,F11
Spell,1856,Vanish,F12
Spell,197835,Shuriken Storm,F7
Aura,222018,Nightblade
Aura,121473,Shadow Blade
Aura,212283,Symbols of Death
Aura,1784,Stealth
Aura,108208,Subterfuge
Aura,185422,Shadow Dance
*/
