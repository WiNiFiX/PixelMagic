// ReSharper disable UnusedMember.Global
// ReSharper disable UseStringInterpolation
// ReSharper disable CheckNamespace

using System.Drawing;
using System.Windows.Forms;
using ShadowMagic.Helpers;

namespace ShadowMagic.Rotation
{
    public class BrewMaster : CombatRoutine
    {
        private static bool _ironSkinFired;

		public override string Name 
		{
			get		
			{
				return "BrewMaster Rotation";
			}
		}
		
		public override string Class 
		{
			get		
			{
				return "Monk";
			}
		}

        public override Form SettingsForm { get; set; }

        public override void Initialize()
        {            
            Log.Write("Welcome to Monk BM rotation", Color.Green);
        }

        public override void Stop()
        {
        }


        public override void Pulse()
        {
            if (combatRoutine.Type == RotationType.SingleTarget)
            {
                //Heal if not in combat
                if (!WoW.IsInCombat && WoW.HealthPercent <= 95 & WoW.Energy >= 30 && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling)
                {
                    //WoW.SendMacro("/cast [@player] Effuse");
                    return;
                }

                //Sanity Checks
                if (WoW.PlayerIsChanneling) return;
                if (!WoW.HasTarget) return;
                if (!WoW.TargetIsEnemy) return;
                if (WoW.PlayerIsCasting) return;
                if (!WoW.IsInCombat) return;
            }


            //Cooldown saves
            if (WoW.HealthPercent <= 85 && WoW.PlayerSpellCharges("Healing Elixir") > 0 && !WoW.IsSpellOnCooldown("Healing Elixir"))
            {
                WoW.CastSpell("Healing Elixir");
                return;
            }

            //Interrupts or Damage negation
            if (WoW.TargetIsCasting && WoW.CanCast("Spear Hand Strike") && !WoW.IsSpellOnCooldown("Spear Hand Strike") && WoW.IsSpellInRange("Spear Hand Strike"))
            {
                WoW.CastSpell("Spear Hand Strike");
                return;
            }

            //Leg Sweep to open, or mitigate damage
            if (WoW.CanCast("Leg Sweep") && !WoW.IsSpellOnCooldown("Leg Sweep") && WoW.IsSpellInRange("Tiger Palm"))
            {
                WoW.CastSpell("Leg Sweep");
                return;
            }

            //Look for Expel Harm Charges.. if 3 and health low then hit
            if (WoW.PlayerSpellCharges("Expel Harm") >= 3 && WoW.HealthPercent <= 75 && WoW.Energy >= 15)
            {
                WoW.CastSpell("Expel Harm");
                return;
            }

            //If Target is almost dead, and we have Expel Harm charges use up -- we don't want to leave it 
            if (WoW.TargetHealthPercent <= 10 && WoW.PlayerSpellCharges("Expel Harm") != 100 && WoW.CanCast("Expel Harm") && !WoW.IsSpellOnCooldown("Expel Harm") && WoW.Energy >= 15)
            {
                Log.Write(string.Format("Expel Harm Count {0}", WoW.PlayerSpellCharges("Expel Harm")));
                WoW.CastSpell("Expel Harm");
                return;
            }


            //Maintain Eye of the Tiger
            if (!WoW.PlayerHasBuff("Eye of the Tiger") && !WoW.IsSpellOnCooldown("Tiger Palm") && WoW.IsSpellInRange("Tiger Palm"))
            {
                WoW.CastSpell("Tiger Palm");
                return;
            }


            //Breath of fire if Vulnerable
            if (WoW.TargetHasDebuff("Keg Smash") && !WoW.TargetHasDebuff("Breath of Fire") && WoW.CanCast("Breath of Fire") && !WoW.IsSpellOnCooldown("Breath of Fire"))
            {
                WoW.CastSpell("Breath of Fire");
                return;
            }

            if (!WoW.TargetHasDebuff("Keg Smash") && WoW.CanCast("Keg Smash") && !WoW.IsSpellOnCooldown("Keg Smash") && WoW.Energy >= 40)
            {
                WoW.CastSpell("Keg Smash");
                return;
            }

            //Energy dump if High
            if (WoW.Energy >= 65 && WoW.CanCast("Tiger Palm") && !WoW.IsSpellOnCooldown("Tiger Palm") && WoW.IsSpellInRange("Tiger Palm"))
            {
                WoW.CastSpell("Tiger Palm");
                return;
            }

            //if Ironskin count = 3 and in Melee range (or we cast too early as we approach)

            if (WoW.PlayerSpellCharges("Ironskin Brew") >= 2 && !WoW.PlayerHasBuff("Ironskin Brew") && WoW.IsSpellInRange("Tiger Palm"))
            {
                _ironSkinFired = true;
                WoW.CastSpell("Ironskin Brew");
                return;
            }
            if (_ironSkinFired && !WoW.PlayerHasBuff("Ironskin Brew"))
            {
                _ironSkinFired = false;
                if (WoW.PlayerSpellCharges("Purifying Brew") >= 1)
                {
                    WoW.CastSpell("Purifying Brew");
                    return;
                }
            }

            //if We Can Cast Exploding Keg and in Melee range then do so
            if (WoW.CanCast("Exploding Keg") && !WoW.IsSpellOnCooldown("Exploding Keg") && WoW.IsSpellInRange("Tiger Palm"))
            {
                //WoW.SendMacro("/cast [@player] Exploding Keg");
                return;
            }

            //TODO NEED Fucking ability to detect staggers so we can use our spare Purifying Brew here.. We are not 
            //Optimized until this behavior exists.  We need to use it, then pop Fortifying Brew


            if (WoW.CanCast("Blackout Strike") && !WoW.IsSpellOnCooldown("Blackout Strike") && WoW.IsSpellInRange("Tiger Palm"))
            {
                WoW.CastSpell("Blackout Strike");
            }

            if (WoW.CanCast("Tiger Palm") && !WoW.IsSpellOnCooldown("Tiger Palm") && WoW.IsSpellInRange("Tiger Palm") && WoW.TargetHasDebuff("Keg Smash"))
            {
                WoW.CastSpell("Tiger Palm");
            }
        }
    }
}

/*
[AddonDetails.db]
AddonAuthor=BrewDrinkCode
AddonName=PixelMagic
WoWVersion=Legion - 70300
[SpellBook.db]
Spell,121253,Keg Smash,D1
Spell,115181,Breath of Fire,D2
Spell,100780,Tiger Palm,D3
Spell,205523,Blackout Strike,D4
Spell,115308,Ironskin Brew,D5
Spell,119582,Purifying Brew,D6
Spell,115399,Black Ox Brew,D7
Spell,122281,Healing Elixir,D8
Spell,116705,Spear Hand Strike,D9
Spell,119381,Leg Sweep,D0
Spell,115072,Expel Harm,F1
Spell,214326,Exploding Keg,F2
Aura,121253,Keg Smash
Aura,115181,Breath of Fire
Aura,196736,Blackout Combo
Aura,115308,Ironskin Brew
Aura,124275,Light Stagger
Aura,124274,Moderate Stagger
Aura,124273,Heavy Stagger
Aura,196607,Eye of the Tiger
*/