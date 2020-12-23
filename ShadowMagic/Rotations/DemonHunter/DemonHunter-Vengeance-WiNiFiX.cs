// winifix@gmail.com
// ReSharper disable UnusedMember.Global

using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using ShadowMagic.Helpers;

namespace ShadowMagic.Rotation
{
    public class DemonHunterVeng : CombatRoutine
    {
        private readonly Stopwatch interruptwatch = new Stopwatch();
		
        public override string Name 
		{
			get
			{
				return "PixelMagic DemonHunter";
			}
		}

        public override string Class 
		{
			get
			{
				return "DemonHunter";
			}
		}

		public override Form SettingsForm { get; set; }

        public override void Initialize()
        {
            Log.DrawHorizontalLine();            
            Log.WritePixelMagic("Welcome to PixelMagic Demon Hunter", Color.Black);
            Log.Write("Spec: " + WoW.PlayerSpec);
        }

        public override void Stop()
        {
        }

        public override void Pulse()
        {	
            if (WoW.IsInCombat && interruptwatch.ElapsedMilliseconds == 0)
            {
				Log.Write("Starting interrupt timer", Color.Blue);
                interruptwatch.Start();
            }

            if (UseCooldowns)
            {
            }

            if (combatRoutine.Type != RotationType.SingleTarget && combatRoutine.Type != RotationType.AOE) return;

			if (WoW.IsInCombat && (!WoW.TargetIsEnemy || WoW.TargetHealthPercent == 0))
			{
				WoW.TargetNearestEnemy();
			}
			
            if (!WoW.HasTarget || !WoW.TargetIsEnemy) return;

            if (WoW.HealthPercent < 30 && !WoW.IsSpellOnCooldown("Metamorphasis"))
            {
                Log.Write("Metamorphasis");
                Log.Write("Health low < 70% using CDs...", Color.Red);
                WoW.CastSpell("Metamorphasis"); // Off the GCD no return needed
            }

            if (WoW.PlayerHasBuff("Metamorphasis") && WoW.CanCast("Sever"))
            {
                WoW.CastSpell("Sever");
                return;
            }

            if (WoW.PlayerHasBuff("Metamorphasis") && WoW.PlayerHasBuff("Soul Fragments") && (WoW.PlayerBuffStacks("Soul Fragments") >= 5) && WoW.Pain >= 50)
            {
                WoW.CastSpell("Soul Cleave");
                return;
            }

            if (!WoW.IsSpellInRange("Soul Carver") && !WoW.IsSpellOnCooldown("Throw Glaive") && WoW.IsSpellInRange("Throw Glaive") && WoW.CanCast("Throw Glaive"))
            {
                WoW.CastSpell("Throw Glaive");
                return;
            }

            if (!WoW.IsSpellInRange("Soul Carver")) // If we are out of melee range return
                return;

            if (WoW.TargetIsCastingAndSpellIsInterruptible && interruptwatch.ElapsedMilliseconds > 1200 && WoW.TargetPercentCast > 70)
            {
                if (!WoW.IsSpellOnCooldown("Sigil of Silence"))
                {
                    Log.Write("Interrupting spell");
                    WoW.CastSpell("Sigil of Silence");
                    interruptwatch.Reset();
                    interruptwatch.Start();
                    return;
                }

                if (!WoW.IsSpellOnCooldown("Arcane Torrent"))
                {
                    Log.Write("Interrupting spell");
                    WoW.CastSpell("Arcane Torrent");
                    interruptwatch.Reset();
                    interruptwatch.Start();
                    return;
                }
				
				if (!WoW.IsSpellOnCooldown("Consume Magic"))
                {
                    Log.Write("Interrupting spell");
                    WoW.CastSpell("Consume Magic");
                    interruptwatch.Reset();
                    interruptwatch.Start();
                    return;
                }
            }

            if (!WoW.TargetHasDebuff("Fiery Demise") && !WoW.IsSpellOnCooldown("Fiery Brand"))
            {
                WoW.CastSpell("Fiery Brand");
            }

            if (WoW.CanCast("Demon Spikes") && !WoW.PlayerHasBuff("Demon Spikes") && WoW.Pain > 20 && !WoW.PlayerHasBuff("Magnum Opus"))
            {
                WoW.CastSpell("Demon Spikes");
            }

            if (WoW.CanCast("Soul Carver"))
            {
                WoW.CastSpell("Soul Carver");
            }

            if (WoW.CanCast("Fel Devastation") && WoW.Pain >= 30)
            {
                WoW.CastSpell("Fel Devastation");
            }

            if (WoW.CanCast("Soul Cleave") && WoW.Pain >= 50)
            {
                WoW.CastSpell("Soul Cleave");
                return;
            }

            if (WoW.CanCast("Immolation Aura"))
            {
                WoW.CastSpell("Immolation Aura");
                return;
            }

            if (WoW.CanCast("Sigil of Flame") && !WoW.TargetHasDebuff("Sigil of Flame"))
            {                
                WoW.CastSpell("Sigil of Flame"); // NB must have "Concentrated Sigil's" talent
                return;
            }

            if (WoW.CanCast("Shear")) // Pain Generator
            {
                WoW.CastSpell("Shear");
            }
        }
    }
}

/*
[AddonDetails.db]
AddonAuthor=WiNiFiX
AddonName=PixelMagic
WoWVersion=Legion - 70200
[SpellBook.db]
Spell,203782,Shear,T
Spell,235964,Sever,T
Spell,228477,Soul Cleave,Y
Spell,207407,Soul Carver,H
Spell,212084,Fel Devastation,W
Spell,178740,Immolation Aura,D5
Spell,204513,Sigil of Flame,D6
Spell,204157,Throw Glaive,D7
Spell,207682,Sigil of Silence,D8
Spell,202719,Arcane Torrent,D0
Spell,187827,Metamorphasis,D9
Spell,204021,Fiery Brand,F
Spell,203720,Demon Spikes,S
Spell,183752,Consume Magic,B
Aura,203819,Demon Spikes
Aura,212818,Fiery Demise
Aura,41252,Mount
Aura,207472,Magnum Opus
Aura,187827,Metamorphasis
Aura,204598,Sigil of Flame
Aura,203981,Soul Fragments
Item,80610,Mana
*/