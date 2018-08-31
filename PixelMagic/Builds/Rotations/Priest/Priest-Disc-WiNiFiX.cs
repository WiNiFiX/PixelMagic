// winifix@gmail.com
// ReSharper disable UnusedMember.Global


using System.Drawing;
using System.Windows.Forms;
using PixelMagic.Helpers;

namespace PixelMagic.Rotation
{
    public class Disc : CombatRoutine
    {
        public override string Name => "Disc Priest";

        public override string Class => "Priest";

        public override Form SettingsForm { get; set; }

        public override void Initialize()
        {
            Log.Write("Welcome to WiNiFiX Disc Priest", Color.Green);
        }

        public override void Stop()
        {
        }

        public override void Pulse() // Updated for Legion (tested and working for single target)
        {
            if (combatRoutine.Type == RotationType.SingleTarget) // Do Single Target Stuff here
            {
                Log.Write(WoW.PlayerBuffTimeRemaining("PWS").ToString());

                if ((!WoW.PlayerHasBuff("PWS") || WoW.PlayerBuffTimeRemaining("PWS") < 4) && WoW.CanCast("PWS"))
                {
                    WoW.CastSpell("PWS");
                }
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
Spell,17,PWS,S
Aura,17,PWS
*/