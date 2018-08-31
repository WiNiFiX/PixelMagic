// winifix@gmail.com
// ReSharper disable UnusedMember.Global


using System.Drawing;
using System.Windows.Forms;
using PixelMagic.Helpers;

namespace PixelMagic.Rotation
{
    public class EnhancementZZ : CombatRoutine
    {
		public override string Name 
		{
			get
			{				
				return "Enhancement Toomicek";		
			}
		}

        public override string Class 
		{
			get
			{				
				return "Shaman";		
			}
		}

        public override Form SettingsForm { get; set; }

        public override void Initialize()
        {
            Log.Write("Welcome to Enhancement Shaman by Toomicek", Color.Green);
            Log.Write("Suggested build: 1213112", Color.Green);            
        }

        public override void Stop()
        {
        }

        public override void Pulse()
        {
            if (combatRoutine.Type == RotationType.SingleTarget) // Do Single Target Stuff here
            {
                if (WoW.HasTarget && WoW.TargetIsEnemy) //First things go first
                {
                    if (WoW.TargetIsCasting && WoW.IsSpellInRange("Wind Shear")) //interupt every spell - need to add kickable spells
                    {
                        WoW.CastSpell("Wind Shear");
                        return;
                    }

                    if (WoW.CanCast("Boulderfist") && !WoW.PlayerHasBuff("Landslide") && WoW.IsSpellInRange("Flametongue")) //REFRESH LANDSLIDE
                    {
                        Log.Write("Reseting Landslide", Color.Red);
                        WoW.CastSpell("Boulderfist");
                        return;
                    }

                    if (WoW.CanCast("Hailstorm") && !WoW.PlayerHasBuff("Frostbrand") && WoW.IsSpellInRange("Flametongue") && WoW.Maelstrom >= 20) //REFRESH FROSTBRAND
                    {
                        Log.Write("Reseting Frostbrand buff", Color.Red);
                        WoW.CastSpell("Hailstorm");
                        return;
                    }

                    if (WoW.CanCast("Flametongue") && !WoW.PlayerHasBuff("Flametongue") && WoW.IsSpellInRange("Flametongue") && !WoW.IsSpellOnCooldown("Flametongue")) //REFRESH FLAMETONGUE
                    {
                        Log.Write("Reseting Flametongue buff", Color.Red);
                        WoW.CastSpell("Flametongue");
                        return;
                    }

                    if (!WoW.IsSpellOnCooldown("Feral Spirit") && WoW.CanCast("Feral Spirit") && UseCooldowns) //feral spirit on boss - normally cast manually
                    {
                        Log.Write("Using Feral Spirit", Color.Red);
                        WoW.CastSpell("Feral Spirit");
                        return;
                    }

                    if (WoW.CanCast("Doom Winds") && !WoW.IsSpellOnCooldown("Doom Winds") && WoW.IsSpellInRange("Stormstrike"))
                        //use doom winds on CD - need implementation to Cooldowns also with Feral Spirit
                    {
                        WoW.CastSpell("Doom Winds");
                        return;
                    }

                    if (WoW.CanCast("Stormstrike") && WoW.IsSpellInRange("Stormstrike") && (WoW.Maelstrom >= 40 || WoW.PlayerHasBuff("Stormbringer") && WoW.Maelstrom >= 20))
                    {
                        WoW.CastSpell("Stormstrike");
                        return;
                    }

                    if (WoW.CanCast("Boulderfist") && WoW.IsSpellInRange("Flametongue") && WoW.PlayerSpellCharges("Boulderfist") > 1 && WoW.SpellCooldownTimeRemaining("Boulderfist") <= 1.2 ||
                        WoW.PlayerSpellCharges("Boulderfist") == 2)
                    {
                        WoW.CastSpell("Boulderfist"); //boulderfist it to not waste a charge
                        return;
                    }

                    if (WoW.CanCast("Hailstorm") && (WoW.PlayerBuffTimeRemaining("Frostbrand") <= 4.2) && WoW.IsSpellInRange("Flametongue") && WoW.Maelstrom >= 20)
                        //REFRESH FROSTBRAND PANCEMIC
                    {
                        WoW.CastSpell("Hailstorm");
                        return;
                    }

                    if (WoW.CanCast("Flametongue") && (WoW.PlayerBuffTimeRemaining("Flametongue") <= 4.2) && WoW.IsSpellInRange("Flametongue") && !WoW.IsSpellOnCooldown("Flametongue"))
                        //REFRESH FLAMETONGUE PANDEMIC
                    {
                        WoW.CastSpell("Flametongue");
                        return;
                    }

                    if (WoW.CanCast("Crash Lightning") && WoW.IsSpellInRange("Stormstrike") && WoW.Maelstrom > 80) //Crash lightning on range of Stormstrike
                    {
                        WoW.CastSpell("Crash Lightning");
                        return;
                    }

                    if (WoW.CanCast("Lava Lash") && WoW.Maelstrom > 90) //maelstrom dump
                    {
                        Log.Write("Maelstorm spender - Lava Lash", Color.Blue);
                        WoW.CastSpell("Lava Lash");
                        return;
                    }

                    if (WoW.CanCast("Boulderfist") && WoW.IsSpellInRange("Flametongue")) //filler if we are in range 10y
                    {
                        Log.Write("Nothing to do - Boulderfist", Color.Blue);
                        WoW.CastSpell("Boulderfist");
                        return;
                    }

                    if (WoW.CanCast("Flametongue") && WoW.IsSpellInRange("Flametongue") && !WoW.IsSpellOnCooldown("Flametongue")) //filler if we are in range 10y
                    {
                        Log.Write("Nothing to do - Flametongue", Color.Blue);
                        WoW.CastSpell("Flametongue");
                        return;
                    }

                    if (WoW.CanCast("Feral Lunge") && !WoW.IsSpellInRange("Flametongue") && WoW.IsSpellInRange("Feral Lunge"))
                        //out of range of flametongue 10y and in range of feral lunge 8-25y 
                    {
                        WoW.CastSpell("Feral Lunge");
                        return;
                    }

                    if (WoW.CanCast("Lightning Bolt") && !WoW.IsSpellInRange("Flametongue")) //out of range cast LB if we are 10y away and cannot jump by Feral lounge
                    {
                        WoW.CastSpell("Lightning Bolt");
                        return;
                    }

                    if (WoW.CanCast("Astral Shift") && WoW.HealthPercent < 60 && !WoW.IsSpellOnCooldown("Astral Shift")) //ASTRAL SHIFT - DMG REDUCTION if we are below 60% of HP
                    {
                        WoW.CastSpell("Astral Shift");
                        return;
                    }
                }
            }
            if (combatRoutine.Type == RotationType.SingleTargetCleave)
            {
                if (WoW.HasTarget && WoW.TargetIsEnemy) //First things go first
                {
                    if (WoW.TargetIsCasting && WoW.IsSpellInRange("Wind Shear")) //interupt every spell - need to add kickable spells
                    {
                        WoW.CastSpell("Wind Shear");
                        return;
                    }

                    if (WoW.CanCast("Boulderfist") && !WoW.PlayerHasBuff("Landslide") && WoW.IsSpellInRange("Flametongue")) //REFRESH LANDSLIDE
                    {
                        Log.Write("Reseting Landslide", Color.Red);
                        WoW.CastSpell("Boulderfist");
                        return;
                    }

                    if (WoW.CanCast("Hailstorm") && !WoW.PlayerHasBuff("Frostbrand") && WoW.IsSpellInRange("Flametongue") && WoW.Maelstrom >= 20) //REFRESH FROSTBRAND
                    {
                        Log.Write("Reseting Frostbrand buff", Color.Red);
                        WoW.CastSpell("Hailstorm");
                        return;
                    }

                    if (WoW.CanCast("Flametongue") && !WoW.PlayerHasBuff("Flametongue") && WoW.IsSpellInRange("Flametongue") && !WoW.IsSpellOnCooldown("Flametongue")) //REFRESH FLAMETONGUE
                    {
                        Log.Write("Reseting Flametongue buff", Color.Red);
                        WoW.CastSpell("Flametongue");
                        return;
                    }

                    if (!WoW.IsSpellOnCooldown("Feral Spirit") && WoW.CanCast("Feral Spirit") && UseCooldowns) //feral spirit on boss - normally cast manually
                    {
                        Log.Write("Using Feral Spirit", Color.Red);
                        WoW.CastSpell("Feral Spirit");
                        return;
                    }

                    if (WoW.CanCast("Doom Winds") && !WoW.IsSpellOnCooldown("Doom Winds") && WoW.IsSpellInRange("Stormstrike"))
                        //use doom winds on CD - need implementation to Cooldowns also with Feral Spirit
                    {
                        WoW.CastSpell("Doom Winds");
                        return;
                    }

                    if (WoW.CanCast("Stormstrike") && WoW.IsSpellInRange("Stormstrike") && (WoW.Maelstrom >= 40 || WoW.PlayerHasBuff("Stormbringer") && WoW.Maelstrom >= 20))
                    {
                        WoW.CastSpell("Stormstrike");
                        return;
                    }

                    if (WoW.CanCast("Crash Lightning") && WoW.IsSpellInRange("Stormstrike") && WoW.Maelstrom >= 20) //Crash lightning to do a splash damage
                    {
                        WoW.CastSpell("Crash Lightning");
                        return;
                    }

                    if (WoW.CanCast("Boulderfist") && WoW.IsSpellInRange("Flametongue") && WoW.PlayerSpellCharges("Boulderfist") > 1 && WoW.SpellCooldownTimeRemaining("Boulderfist") <= 1.2 ||
                        WoW.PlayerSpellCharges("Boulderfist") == 2)
                    {
                        WoW.CastSpell("Boulderfist"); //boulderfist it to not waste a charge
                        return;
                    }

                    if (WoW.CanCast("Hailstorm") && (WoW.PlayerBuffTimeRemaining("Frostbrand") <= 4.2) && WoW.IsSpellInRange("Flametongue") && WoW.Maelstrom >= 20)
                        //REFRESH FROSTBRAND PANCEMIC
                    {
                        WoW.CastSpell("Hailstorm");
                        return;
                    }

                    if (WoW.CanCast("Flametongue") && (WoW.PlayerBuffTimeRemaining("Flametongue") <= 4.2) && WoW.IsSpellInRange("Flametongue") && !WoW.IsSpellOnCooldown("Flametongue"))
                    {
                        WoW.CastSpell("Flametongue");
                        return;
                    }

                    if (WoW.CanCast("Crash Lightning") && WoW.IsSpellInRange("Stormstrike") && WoW.Maelstrom > 80) //Crash lightning on range of Stormstrike
                    {
                        WoW.CastSpell("Crash Lightning");
                        return;
                    }

                    if (WoW.CanCast("Lava Lash") && WoW.Maelstrom > 90) //maelstrom dump
                    {
                        Log.Write("Maelstorm spender - Lava Lash", Color.Blue);
                        WoW.CastSpell("Lava Lash");
                        return;
                    }

                    if (WoW.CanCast("Boulderfist") && WoW.IsSpellInRange("Flametongue")) //filler if we are in range 10y
                    {
                        Log.Write("Nothing to do - Boulderfist", Color.Blue);
                        WoW.CastSpell("Boulderfist");
                        return;
                    }

                    if (WoW.CanCast("Flametongue") && WoW.IsSpellInRange("Flametongue") && !WoW.IsSpellOnCooldown("Flametongue")) //filler if we are in range 10y
                    {
                        Log.Write("Nothing to do - Flametongue", Color.Blue);
                        WoW.CastSpell("Flametongue");
                        return;
                    }

                    if (WoW.CanCast("Feral Lunge") && !WoW.IsSpellInRange("Flametongue") && WoW.IsSpellInRange("Feral Lunge"))
                        //out of range of flametongue 10y and in range of feral lunge 8-25y 
                    {
                        WoW.CastSpell("Feral Lunge");
                        return;
                    }

                    if (WoW.CanCast("Lightning Bolt") && !WoW.IsSpellInRange("Flametongue")) //out of range cast LB if we are 10y away and cannot jump by Feral lounge
                    {
                        WoW.CastSpell("Lightning Bolt");
                        return;
                    }

                    if (WoW.CanCast("Astral Shift") && WoW.HealthPercent < 60 && !WoW.IsSpellOnCooldown("Astral Shift")) //ASTRAL SHIFT - DMG REDUCTION if we are below 60% of HP
                    {
                        WoW.CastSpell("Astral Shift");
                        return;
                    }
                }
            }
            if (combatRoutine.Type == RotationType.AOE)
            {
                if (WoW.HasTarget && WoW.TargetIsEnemy) //First things go first
                {
                    if (WoW.TargetIsCasting && WoW.IsSpellInRange("Wind Shear")) //interupt every spell - need to add kickable spells
                    {
                        WoW.CastSpell("Wind Shear");
                        return;
                    }

                    if (WoW.CanCast("Boulderfist") && !WoW.PlayerHasBuff("Landslide") && WoW.IsSpellInRange("Flametongue")) //REFRESH LANDSLIDE
                    {
                        Log.Write("Reseting Landslide", Color.Red);
                        WoW.CastSpell("Boulderfist");
                        return;
                    }

                    if (WoW.CanCast("Hailstorm") && !WoW.PlayerHasBuff("Frostbrand") && WoW.IsSpellInRange("Flametongue") && WoW.Maelstrom >= 20) //REFRESH FROSTBRAND
                    {
                        Log.Write("Reseting Frostbrand buff", Color.Red);
                        WoW.CastSpell("Hailstorm");
                        return;
                    }

                    if (WoW.CanCast("Flametongue") && !WoW.PlayerHasBuff("Flametongue") && WoW.IsSpellInRange("Flametongue") && !WoW.IsSpellOnCooldown("Flametongue")) //REFRESH FLAMETONGUE
                    {
                        Log.Write("Reseting Flametongue buff", Color.Red);
                        WoW.CastSpell("Flametongue");
                        return;
                    }

                    if (!WoW.IsSpellOnCooldown("Feral Spirit") && WoW.CanCast("Feral Spirit") && UseCooldowns) //feral spirit on boss - normally cast manually
                    {
                        Log.Write("Using Feral Spirit", Color.Red);
                        WoW.CastSpell("Feral Spirit");
                        return;
                    }

                    if (WoW.CanCast("Doom Winds") && !WoW.IsSpellOnCooldown("Doom Winds") && WoW.IsSpellInRange("Stormstrike"))
                        //use doom winds on CD - need implementation to Cooldowns also with Feral Spirit
                    {
                        WoW.CastSpell("Doom Winds");
                        return;
                    }

                    if (WoW.CanCast("Crash Lightning") && WoW.IsSpellInRange("Stormstrike") && WoW.Maelstrom >= 20) //Crash lightning to do a more splash damage
                    {
                        WoW.CastSpell("Crash Lightning");
                        return;
                    }

                    if (WoW.CanCast("Stormstrike") && WoW.IsSpellInRange("Stormstrike") && (WoW.Maelstrom >= 40 || WoW.PlayerHasBuff("Stormbringer") && WoW.Maelstrom >= 20))
                    {
                        WoW.CastSpell("Stormstrike");
                        return;
                    }

                    if (WoW.CanCast("Boulderfist") && WoW.IsSpellInRange("Flametongue") && WoW.PlayerSpellCharges("Boulderfist") > 1 && WoW.SpellCooldownTimeRemaining("Boulderfist") <= 1.2 ||
                        WoW.PlayerSpellCharges("Boulderfist") == 2)
                    {
                        WoW.CastSpell("Boulderfist"); //boulderfist it to not waste a charge
                        return;
                    }

                    if (WoW.CanCast("Hailstorm") && (WoW.PlayerBuffTimeRemaining("Frostbrand") <= 4.2) && WoW.IsSpellInRange("Flametongue") && WoW.Maelstrom >= 20)
                        //REFRESH FROSTBRAND PANCEMIC
                    {
                        WoW.CastSpell("Hailstorm");
                        return;
                    }

                    if (WoW.CanCast("Flametongue") && (WoW.PlayerBuffTimeRemaining("Flametongue") <= 4.2) && WoW.IsSpellInRange("Flametongue") && !WoW.IsSpellOnCooldown("Flametongue"))
                    {
                        WoW.CastSpell("Flametongue");
                        return;
                    }

                    if (WoW.CanCast("Crash Lightning") && WoW.IsSpellInRange("Stormstrike") && WoW.Maelstrom > 80) //Crash lightning on range of Stormstrike
                    {
                        WoW.CastSpell("Crash Lightning");
                        return;
                    }

                    if (WoW.CanCast("Lava Lash") && WoW.Maelstrom > 90) //maelstrom dump
                    {
                        Log.Write("Maelstorm spender - Lava Lash", Color.Blue);
                        WoW.CastSpell("Lava Lash");
                        return;
                    }

                    if (WoW.CanCast("Boulderfist") && WoW.IsSpellInRange("Flametongue")) //filler if we are in range 10y
                    {
                        Log.Write("Nothing to do - Boulderfist", Color.Blue);
                        WoW.CastSpell("Boulderfist");
                        return;
                    }

                    if (WoW.CanCast("Flametongue") && WoW.IsSpellInRange("Flametongue") && !WoW.IsSpellOnCooldown("Flametongue")) //filler if we are in range 10y
                    {
                        Log.Write("Nothing to do - Flametongue", Color.Blue);
                        WoW.CastSpell("Flametongue");
                        return;
                    }

                    if (WoW.CanCast("Feral Lunge") && !WoW.IsSpellInRange("Flametongue") && WoW.IsSpellInRange("Feral Lunge"))
                        //out of range of flametongue 10y and in range of feral lunge 8-25y 
                    {
                        WoW.CastSpell("Feral Lunge");
                        return;
                    }

                    if (WoW.CanCast("Lightning Bolt") && !WoW.IsSpellInRange("Flametongue")) //out of range cast LB if we are 10y away and cannot jump by Feral lounge
                    {
                        WoW.CastSpell("Lightning Bolt");
                        return;
                    }

                    if (WoW.CanCast("Astral Shift") && WoW.HealthPercent < 60 && !WoW.IsSpellOnCooldown("Astral Shift")) //ASTRAL SHIFT - DMG REDUCTION if we are below 60% of HP
                    {
                        WoW.CastSpell("Astral Shift");
                    }
                }
            }
        }
    }
}

/*
[AddonDetails.db]
AddonAuthor=Toomicek
AddonName=RGB
WoWVersion=Legion - 70300
[SpellBook.db]
Spell,57994,Wind Shear,NumPad9
Spell,196884,Feral Lunge,NumPad6
Spell,51533,Feral Spirit,NumPad2
Spell,196834,Hailstorm,D2
Spell,204945,Doom Winds,NumPad3
Spell,187874,Crash Lightning,D3
Spell,193796,Flametongue,D1
Spell,108271,Astral Shift,D5
Spell,201897,Boulderfist,G
Spell,60103,Lava Lash,E
Spell,17364,Stormstrike,Q
Spell,187837,Lightning Bolt,NumPad5
Spell,188070,Healing Surge,NumPad4
Aura,194084,Flametongue
Aura,196834,Frostbrand
Aura,187878,Crashing Storm
Aura,218825,Boulderfist
Aura,202004,Landslide
Aura,201846,Stormbringer
Aura,204945,Doom Winds
*/
