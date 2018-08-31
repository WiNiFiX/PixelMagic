// muddern@safe-mail.net
// If you want me to consider rebuilding the rotation or adding stuff, you can find me in the official Discord
// ReSharper disable UnusedMember.Global

// Special thanks to Dova for helping me out many times

using System.Drawing;
using System.Windows.Forms;
using PixelMagic.Helpers;

namespace PixelMagic.Rotation
{
    public class DestructionWarlock : CombatRoutine
    {
        public override string Name => "DestructionWarlock";

        public override string Class => "Warlock";

        public override Form SettingsForm { get; set; }

        public override void Initialize()
        {
            Log.Write("Destruction Warlock beta by: luck.exe");
            Log.Write("Talents are specced the following: 2,1,1,1,3,2,3");
            Log.Write("Welcome to PixelMagic Destruction");
        }

        public override void Stop()
        {
            Log.Write("Stopping.....", Color.Red);
        }

        public override void Pulse()
        {
            if (combatRoutine.Type == RotationType.SingleTarget) /* Singel Target Rotation*/
            {
                if (WoW.HasTarget && WoW.TargetIsEnemy && WoW.HasTarget && WoW.IsInCombat && WoW.HealthPercent <= 20)
                    /* Defensive Cooldowns */
                {
                    if (WoW.IsMoving && WoW.CanCast("UnendingResolve"))
                    {
                        WoW.CastSpell("UnendingResolve");
                        return;
                    }

                    if (!WoW.IsMoving && WoW.CanCast("UnendingResolve"))
                    {
                        WoW.CastSpell("UnendingResolve");
                        return;
                    }

                    if (!WoW.IsMoving && WoW.CanCast("DrainLife"))
                    {
                        WoW.CastSpell("DrainLife");
                        return;
                    }
                }

                if (WoW.IsInCombat && Control.ModifierKeys == Keys.Shift && !WoW.PlayerIsCasting)
                    /* Havoc on mouseover target, create macro to use: #showtooltip /cast [target=mouseover,harm,exists,nodead] Havoc; Havoc */
                {
                    WoW.CastSpell("Havoc");
                    return;
                }

                if (WoW.HasTarget && WoW.TargetIsEnemy && !WoW.IsMoving && WoW.IsInCombat) /* What to do when we are NOT moving */
                {
                    if (!WoW.PlayerIsCasting && !WoW.PlayerIsChanneling && !WoW.TargetHasDebuff("AuraImmolate"))
                    {
                        if (WoW.HasTarget && WoW.CanCast("DimRift") && WoW.PlayerSpellCharges("DimRift") == 3)
                        {
                            WoW.CastSpell("DimRift");
                            return;
                        }

                        if (!WoW.WasLastCasted("Immolate") && WoW.CanCast("Immolate"))
                        {
                            WoW.CastSpell("Immolate");
                            return;
                        }
                    }

                    if (!WoW.PlayerIsCasting && !WoW.PlayerIsChanneling && WoW.TargetHasDebuff("AuraImmolate"))
                    {
                        if (WoW.HasTarget && WoW.CanCast("DimRift") && WoW.PlayerSpellCharges("DimRift") == 3)
                        {
                            WoW.CastSpell("DimRift");
                            return;
                        }

                        if (WoW.TargetDebuffTimeRemaining("AuraImmolate") >= 10 && WoW.CanCast("Conflagrate"))
                        {
                            WoW.CastSpell("Conflagrate");
                            return;
                        }

                        if (WoW.TargetDebuffTimeRemaining("AuraImmolate") <= 4.2 && !WoW.WasLastCasted("Immolate") && WoW.CanCast("Immolate"))
                        {
                            WoW.CastSpell("Immolate");
                            return;
                        }

                        if (WoW.TargetDebuffTimeRemaining("AuraImmolate") >= 10 && WoW.PlayerSpellCharges("Conflagrate") == 1 && WoW.WasLastCasted("Conflagrate") && WoW.CanCast("Conflagrate"))
                        {
                            WoW.CastSpell("Conflagrate");
                            return;
                        }

                        if (WoW.PlayerHasBuff("AuraConflagrateBuff") && WoW.TargetHasDebuff("AuraChaosBolt") && WoW.CanCast("Conflagrate") && WoW.CurrentSoulShards <= 4 &&
                            WoW.CanCast("Conflagrate"))
                        {
                            WoW.CastSpell("Conflagrate");
                            return;
                        }

                        if (WoW.CanCast("Conflagrate") && WoW.PlayerSpellCharges("Conflagrate") == 2 && !WoW.WasLastCasted("Immolate") && WoW.CurrentSoulShards <= 4)
                        {
                            WoW.CastSpell("Conflagrate");
                            return;
                        }

                        if (WoW.HasTarget && WoW.CanCast("ServiceImp") && WoW.CurrentSoulShards >= 1)
                        {
                            WoW.CastSpell("ServiceImp");
                            return;
                        }

                        /* Summon Infernal on the target but its an AOE ground Spell. I'm not able to code that: */
                        /* Summon DoomGuard if LordOfFlames debuff is up*/

                        if (WoW.HasTarget && WoW.CanCast("DoomGuard"))
                            /*Since I can't do the Infernal check, I just summon DoomGuard*/
                        {
                            WoW.CastSpell("DoomGuard");
                            return;
                        }

                        if (WoW.CanCast("ChaosBolt") /* && !WoW.WasLastCasted("ChaosBolt")  */&& WoW.CurrentSoulShards > 3)
                        {
                            WoW.CastSpell("ChaosBolt");
                            return;
                        }

                        if (WoW.HasTarget && WoW.CanCast("DimRift") && WoW.PlayerSpellCharges("DimRift") <= 2)
                        {
                            WoW.CastSpell("DimRift");
                            return;
                        }

                        if (WoW.CanCast("ChaosBolt") && WoW.CurrentSoulShards >= 2)
                        {
                            WoW.CastSpell("ChaosBolt");
                            return;
                        }

                        if (WoW.CanCast("Incinerate") && WoW.WasLastCasted("ChaosBolt")
                            /*&& WoW.HasDebuff("AuraChaosBolt")*/)
                        {
                            WoW.CastSpell("Incinerate");
                            return;
                        }

                        if (WoW.CanCast("Incinerate") && WoW.CurrentSoulShards <= 1)
                        {
                            WoW.CastSpell("Incinerate");
                            return;
                        }

                        if (WoW.CanCast("Incinerate") && WoW.TargetHasDebuff("AuraChaosBolt") && WoW.TargetDebuffTimeRemaining("AuraChaosBolt") >= 2 && WoW.CurrentSoulShards <= 3)
                        {
                            WoW.CastSpell("Incinerate");
                            return;
                        }
                    }
                }

                if (WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsMoving && WoW.IsInCombat)
                    /* What to do when we are moving */
                {
                    if (!WoW.PlayerIsCasting && !WoW.PlayerIsChanneling && WoW.HasTarget && WoW.CanCast("DimRift"))
                    {
                        WoW.CastSpell("DimRift");
                        return;
                    }

                    if (!WoW.PlayerIsCasting && !WoW.PlayerIsChanneling && WoW.CanCast("Conflagrate"))
                    {
                        WoW.CastSpell("Conflagrate");
                    }
                }
            }
        }
    }
}

/*
[AddonDetails.db]
AddonAuthor=Nilrem
AddonName=PixelMagic
WoWVersion=Legion - 70300
[SpellBook.db]
Spell,29722,Incinerate,D1
Spell,348,Immolate,D3
Spell,17962,Conflagrate,D4
Spell,116858,ChaosBolt,D2
Spell,18540,DoomGuard,F1
Spell,196586,DimRift,F2
Spell,111859,ServiceImp,F3
Spell,1122,Infernal,F4
Spell,104773,UnendingResolve,F5
Spell,108416,DarkPact,F6
Spell,80240,Havoc,F5
Aura,157736,AuraImmolate
Aura,196414,AuraChaosBolt
Aura,196546,AuraConflagrateBuff
*/