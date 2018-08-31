// winifix@gmail.com
// ReSharper disable UnusedMember.Global


using System.Drawing;
using System.Windows.Forms;
using PixelMagic.Helpers;

namespace PixelMagic.Rotation
{
    public class Elemental : CombatRoutine
    {
		public override string Name
        {
            get { return "Elemental Toomicek"; }
        }

        public override string Class
        {
            get { return "Shaman"; }
        }

        public override Form SettingsForm { get; set; }

        public override void Initialize()
        {
            Log.Write("Welcome to Elemental Shaman by Toomicek", Color.Green);
            Log.Write("Suggested build: 3112211", Color.Green);
            Log.Write("PixelMagic Elemental");
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

                    if (WoW.CanCast("Totem Mastery") && !WoW.PlayerHasBuff("Totem Mastery")) //Totem mastery at beggining
                    {
                        WoW.CastSpell("Totem Mastery");
                        return;
                    }

                    if (!WoW.IsSpellOnCooldown("Fire Elemental")) // && WoW.IsBoss) // use Fire Elemental
                    {
                        Log.Write("Boss detected, using Fire Elemental", Color.Purple);
                        WoW.CastSpell("Fire Elemental");
                        return;
                    }

                    if (WoW.CanCast("Ascendance") && !WoW.IsSpellOnCooldown("Ascendance")) // && WoW.IsBoss) //use Ascendance
                    {
                        WoW.CastSpell("Ascendance");
                        return;
                    }

                    if (WoW.CanCast("Stormkeeper") && !WoW.IsSpellOnCooldown("Stormkeeper") && !WoW.PlayerHasBuff("Ascendance")) //use stormkeeper after ascendance
                    {
                        WoW.CastSpell("Stormkeeper");
                        return;
                    }

                    if (WoW.CanCast("Lightning Bolt") && WoW.PlayerHasBuff("Stormkeeper") && !WoW.IsMoving) //Filler with stormkeeper
                    {
                        WoW.CastSpell("Lightning Bolt");
                    }

                    if (WoW.CanCast("Elemental Mastery") && WoW.IsSpellOnCooldown("Elemental Mastery")) //use Elemental Mastery on CD
                    {
                        WoW.CastSpell("Elemental Mastery");
                        return;
                    }

                    if (WoW.CanCast("Flame Shock") && !WoW.TargetHasDebuff("Flame Shock")) //Refresh Flame shock
                    {
                        WoW.CastSpell("Flame Shock");
                        return;
                    }

                    if (WoW.CanCast("Earth Shock") && WoW.Maelstrom > 99) //Earth shock on 100 maelstrom
                    {
                        WoW.CastSpell("Earth Shock");
                        return;
                    }

                    if (WoW.CanCast("Lava Burst") && WoW.TargetHasDebuff("Flame Shock") && WoW.PlayerHasBuff("Lava Surge")) //lava burst when we have lava surge
                    {
                        WoW.CastSpell("Lava Burst");
                        return;
                    }

                    if (WoW.CanCast("Lava Burst") && WoW.TargetHasDebuff("Flame Shock") && !WoW.IsMoving) //lava burst when not moving 
                    {
                        WoW.CastSpell("Lava Burst");
                        return;
                    }

                    if (WoW.CanCast("Lightning Bolt") && !WoW.IsMoving) //Filler
                    {
                        WoW.CastSpell("Lightning Bolt");
                        return;
                    }

                    if (WoW.CanCast("Astral Shift") && WoW.HealthPercent < 40 && !WoW.IsSpellOnCooldown("Astral Shift")) //ASTRAL SHIFT - DMG REDUCTION if we are below 40% of HP
                    {
                        WoW.CastSpell("Astral Shift");
                        return;
                    }
                }
            }
            if (combatRoutine.Type == RotationType.AOE) //cast chain light and earthguake, using CDs without fire elemental   
            {
                if (WoW.HasTarget && WoW.TargetIsEnemy) //First things go first
                {
                    if (WoW.TargetIsCasting && WoW.IsSpellInRange("Wind Shear")) //interupt every spell - need to add kickable spells
                    {
                        WoW.CastSpell("Wind Shear");
                        return;
                    }

                    if (WoW.CanCast("Totem Mastery") && !WoW.PlayerHasBuff("Totem Mastery")) //Totem mastery at beggining
                    {
                        WoW.CastSpell("Totem Mastery");
                        return;
                    }

                    if (WoW.CanCast("Ascendance") && !WoW.IsSpellOnCooldown("Ascendance")) // && WoW.IsBoss) //use Ascendance on boss
                    {
                        WoW.CastSpell("Ascendance");
                        return;
                    }

                    if (WoW.CanCast("Stormkeeper") && !WoW.IsSpellOnCooldown("Stormkeeper") && !WoW.PlayerHasBuff("Ascendance")) //Stormkeeper after ascendance
                    {
                        WoW.CastSpell("Stormkeeper");
                        return;
                    }

                    if (WoW.CanCast("Lava Beam") && WoW.PlayerHasBuff("Ascendance") && !WoW.IsMoving) //Filler
                    {
                        WoW.CastSpell("Lava Beam");
                    }

                    if (WoW.CanCast("Lava Burst") && WoW.TargetHasDebuff("Flame Shock") && WoW.PlayerHasBuff("Lava Surge")) //lava burst when we have lava surge
                    {
                        WoW.CastSpell("Lava Burst");
                        return;
                    }

                    if (WoW.CanCast("Earthquake") && WoW.Maelstrom > 50)
                        //Earthquake using this macro #showtooltip Earthquake /cast [@cursor] Earthquake Need to point at location where EQ is cast

                    {
                        WoW.CastSpell("Earthquake");
                        return;
                    }

                    if (WoW.CanCast("Chain Lightning") && WoW.PlayerHasBuff("Stormkeeper") && !WoW.IsMoving) //Chain with stormkeeper
                    {
                        WoW.CastSpell("Chain Lightning");
                    }

                    if (WoW.CanCast("Elemental Mastery") && !WoW.IsSpellOnCooldown("Elemental Mastery")) //use Elemental Mastery on CD
                    {
                        WoW.CastSpell("Elemental Mastery");
                        return;
                    }

                    if (WoW.CanCast("Chain Lightning") && !WoW.IsMoving) //Filler
                    {
                        WoW.CastSpell("Chain Lightning");
                    }

                    if (WoW.CanCast("Astral Shift") && WoW.HealthPercent < 40 && !WoW.IsSpellOnCooldown("Astral Shift")) //ASTRAL SHIFT - DMG REDUCTION if we are below 40% of HP
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
AddonName=PixelMagic
WoWVersion=Legion - 70300
[SpellBook.db]  
Spell,188389,Flame Shock,D3
Spell,108271,Astral Shift,D6
Spell,198067,Fire Elemental,F7
Spell,114074,Lava Beam,E
Spell,188196,Lightning Bolt,D1
Spell,51505,Lava Burst,D2
Spell,198103,Earth Elemental,F8
Spell,188443,Chain Lightning,E
Spell,16166,Elemental Mastery,D4
Spell,114050,Ascendance,D8
Spell,61882,Earthquake,F2
Spell,108281,Ancestral Guidance,D7
Spell,205495,Stormkeeper,D0
Spell,210643,Totem Mastery,F
Spell,8042,Earth Shock,Q
Spell,51490,Thunderstorm,D5
Aura,188389,Flame Shock
Aura,210659,Totem Mastery
Aura;16166,Elemental Mastery
Aura,114050,Ascendance
Aura,77762,Lava Surge
Aura,205495,Stormkeeper
*/
