// winifix@gmail.com
// ReSharper disable UnusedMember.Global


using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using ShadowMagic.Helpers;

namespace ShadowMagic.Rotation
{
    public class DKUnholySuitz : CombatRoutine
    {
        private static readonly Stopwatch stopwatch = new Stopwatch();

        public override string Name => "Unholy Suitz";

        public override string Class => "Deathknight";

        public override Form SettingsForm { get; set; }


        public override void Initialize()
        {
            Log.Write("Welcome to Unholy Suitz", Color.Green);
            Log.Write("Suggested build: http://www.wowhead.com/talent-calc/death-knight/unholy/cJWc");
        }

        public override void Stop()
        {
            Log.Write("Welcome to Unholy Suitz", Color.Green);
        }


        private static void Generic()
        {
            if (WoW.CanCast("SoulReaper") && WoW.CanCast("Apocalipse") && WoW.CurrentRunes >= 1 && WoW.TargetDebuffStacks("Festering Wound") >= 7)
            {
                WoW.CastSpell("SoulReaper");
                Thread.Sleep(800);
                WoW.CastSpell("Apocalipse");
                Thread.Sleep(200);
                WoW.CastSpell("Apocalipse");
                Thread.Sleep(200);
                WoW.CastSpell("Apocalipse");
                Thread.Sleep(100);
                WoW.CastSpell("Apocalipse");
                stopwatch.Reset();
                stopwatch.Start();
                return;
            }

            if (WoW.CanCast("SoulReaper") && !WoW.CanCast("Apocalipse") && WoW.CurrentRunes >= 1 && WoW.TargetDebuffStacks("Festering Wound") >= 3 && stopwatch.ElapsedMilliseconds < 50000)
            {
                WoW.CastSpell("SoulReaper");
                return;
            }

            if ((WoW.CanCast("SoulReaper") && WoW.CanCast("Apocalipse") || WoW.CanCast("SoulReaper")) && WoW.CurrentRunes >= 1 && WoW.TargetDebuffStacks("Festering Wound") <= 7)
            {
                WoW.CastSpell("Festering Strike");
                return;
            }

            if (WoW.CanCast("Death Coil") && (WoW.RunicPower > 85 || WoW.PlayerHasBuff("Sudden Doom")))
            {
                WoW.CastSpell("Death Coil");
                return;
            }

            if (WoW.CanCast("Festering Strike") && WoW.CurrentRunes >= 2 && (!WoW.TargetHasDebuff("Festering Wound") || WoW.TargetDebuffStacks("Festering Wound") <= 4))
            {
                WoW.CastSpell("Festering Strike");
                return;
            }

            if (WoW.CanCast("Scourge Strike") && WoW.CurrentRunes >= 1 && WoW.TargetHasDebuff("Festering Wound"))
            {
                WoW.CastSpell("Scourge Strike");
                return;
            }

            if (WoW.CanCast("Death Coil") && WoW.RunicPower > 35)
            {
                WoW.CastSpell("Death Coil");
            }
        }

        public override void Pulse() // Updated for Legion (tested and working for single target)
        {
            if (combatRoutine.Type == RotationType.SingleTarget) // Do Single Target Stuff here
            {
                if (WoW.HasTarget && !WoW.PlayerIsChanneling && WoW.TargetIsEnemy && WoW.IsInCombat)
                {
                    if (DetectKeyPress.GetKeyState(DetectKeyPress.VK_XBUTTON1) < 0)
                    {
                        if (!WoW.CanCast("DnD")) return;

                        WoW.CastSpell("DnD");

                        Mouse.RightRelease();
                        Thread.Sleep(50);
                        Mouse.LeftClick(Cursor.Position.X, Cursor.Position.Y);
                        Thread.Sleep(50);
                        Mouse.RightDown();
                        Thread.Sleep(50);
                        Mouse.RightRelease();
                        return;
                    }

                    if (WoW.CanCast("Outbreak") && WoW.CurrentRunes >= 1 && !WoW.TargetHasDebuff("Virulent Plague"))
                    {
                        WoW.CastSpell("Outbreak");
                        return;
                    }

                    if (WoW.CanCast("Dark Transformation") && WoW.TargetHasDebuff("Virulent Plague"))
                    {
                        WoW.CastSpell("Dark Transformation");
                        return;
                    }

                    Generic();
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
AddonAuthor=Suitz
AddonName=PixelMagic
WoWVersion=Legion - 70000
[SpellBook.db]
Spell,85948,Festering Strike,D3
Spell,77575,Outbreak,Q
Spell,55090,Scourge Strike,D2
Spell,47541,Death Coil,D1
Spell,63560,Dark Transformation,D4
Spell,220143,Apocalipse,R
Spell,130736,SoulReaper,G
Spell,207349,Dark Arbiter,F
Spell,46584,Raise Dead,D0
Spell,212512,EP,U
Spell,43265,DnD,Y
Aura,81340,Sudden Doom
Aura,194310,Festering Wound
Aura,191587,Virulent Plague
Aura,191748,GDW
Aura,130736,SR
*/