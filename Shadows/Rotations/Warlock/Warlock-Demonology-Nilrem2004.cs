// winifix@gmail.com
// ReSharper disable UnusedMember.Global


using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using ShadowMagic.Helpers;

namespace ShadowMagic.Rotation
{
    public class DemonologyWarlock : CombatRoutine
    {
        public static bool lastpetstatus;
        public static int impnumber;
        public static int dreadnumber;
        public Stopwatch CombatWatch = new Stopwatch();

        public override string Name 
		{
			get
			{
				return "Demo Warlock";
			}
		}

        public override string Class 
		{
			get
			{
				return "Warlock";
			}
		}

        public override Form SettingsForm { get; set; }

        public override void Initialize()
        {
            Log.Write("Demonology Warlock", Color.Red);
            Log.Write("Suggested build: 2211232 - single target", Color.Red);
            Log.Write("Suggested build: 2311232 - AOE", Color.Red);
            Log.Write("Welcome to PixelMagic Demonology");
        }

        public override void Stop()
        {
            Log.Write("Stopping.....", Color.Red);
        }

        public static bool PetBuffStatus()
        {
            if (!WoW.IsInCombat)
            {
                lastpetstatus = false;
                return lastpetstatus;
            }
            if (WoW.WasLastCasted("DemonicEmpowerment") || WoW.WasLastCasted("Implosion"))
            {
                lastpetstatus = false;
                return lastpetstatus;
            }
            if (lastpetstatus && (!WoW.WasLastCasted("DemonicEmpowerment") || !WoW.WasLastCasted("Implosion")))
            {
                return lastpetstatus;
            }
            if (WoW.WasLastCasted("Dreadstalkers") || WoW.WasLastCasted("HoG"))
            {
                lastpetstatus = true;
                return lastpetstatus;
            }
            return lastpetstatus;
        }

        /* public static List<DateTime> imps = new List<DateTime>();
		
		public static bool ImpCounting()
		{
			if (!WoW.IsInCombat && imps.Any())
			{
				imps.Clear(); // to clear list when out of combat
				return true;
			}
			
			if (WoW.WasLastCasted("HoG"))
			{
				imps.Add(DateTime.UtcNow); // to add new entry
				return true;
			}
			return true;
		}
		
		public static int CountImpPacks
		{
			get
			{
				if (imps.Any())	
				{
					return imps.Count(i => i.AddSeconds(10) > DateTime.UtcNow);
				}
				return 0;
			}
		} */

        /* imps.Count(i => i.AddSeconds(10) > DateTime.UtcNow); */

        public override void Pulse()
        {
            /* Log.Write("Starting.....", Color.Red); */
            /* Place to check target's (boss) buffs/debuffs in order to stop casting */

            /* if (WoW.IsInCombat && WoW.HasTarget && WoW.TargetIsEnemy && WoW.TargetHasBuff("Focusing")) return; */

            if (WoW.PlayerHasBuff("Mount")) return;

            if (combatRoutine.Type == RotationType.SingleTarget)
            {
                if (CombatWatch.IsRunning && !WoW.IsInCombat)
                {
                    CombatWatch.Reset();
                }
                if (!CombatWatch.IsRunning && WoW.IsInCombat)
                {
                    CombatWatch.Start();
                }
                /* if (CombatWatch.IsRunning && CombatWatch.ElapsedMilliseconds > 5000)
					{
					Log.Write ("CombatWatch > 5 seconds");
					} */


                /* if (WoW.IsInCombat && WoW.HealthPercent<30 && WoW.CanCast("Healthstone") && WoW.PlayerSpellCharges("Healthstone") >= 1 && !WoW.IsSpellOnCooldown("Healthstone"))
					{
						WoW.CastSpellByName("Healthstone");
						return;
					} */

                PetBuffStatus();
                /* Log.Write ("Pets need buff: " + lastpetstatus); */

                /* impnumber = WoW.GetWildImpsCount();
				dreadnumber = WoW.GetDreadstalkersCount(); */
                /* Log.Write ("Wild imps: " + WoW.GetWildImpsCount); 
				Log.Write ("Dreadretards: " + WoW.GetDreadstalkersCount); */


                if (WoW.IsInCombat && WoW.HasTarget && WoW.TargetIsEnemy && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling && WoW.CanCast("Doomguard") && WoW.IsSpellInRange("Doomguard") &&
                    (WoW.PlayerHasBuff("Bloodlust") || WoW.PlayerHasBuff("TimeWarp")) && WoW.CurrentSoulShards >= 1 && !WoW.IsSpellOnCooldown("Doomguard"))
                {
                    WoW.CastSpell("Doomguard");
                    return;
                }

                if (WoW.IsMoving) /* What to do if we are MOVING */
                {
                    if (WoW.IsInCombat && WoW.HasTarget && WoW.TargetIsEnemy && WoW.PlayerSpellCharges("Shadowflame") == 1 && WoW.TargetHasDebuff("Shadowflame") &&
                        WoW.TargetDebuffTimeRemaining("Shadowflame") < 3 && WoW.CanCast("Shadowflame") && WoW.IsSpellInRange("Shadowflame"))
                    {
                        WoW.CastSpell("Shadowflame");
                        return;
                    }

                    if (WoW.IsInCombat && WoW.HasTarget && WoW.TargetIsEnemy && WoW.HasPet && WoW.CanCast("Wrathstorm") && CombatWatch.ElapsedMilliseconds > 2000)
                    {
                        WoW.CastSpell("Wrathstorm");
                    }

                    if (!WoW.IsInCombat && WoW.Mana < 80 && WoW.HealthPercent > 75 && WoW.CanCast("Life Tap"))
                    {
                        WoW.CastSpell("Life Tap");
                        return;
                    }

                    if (WoW.IsInCombat && WoW.Mana < 30 && WoW.HealthPercent > 65 && WoW.CanCast("Life Tap"))
                    {
                        WoW.CastSpell("Life Tap");
                        return;
                    }

                    if (WoW.IsInCombat && WoW.HasTarget && WoW.CanCast("Shadowflame") && WoW.TargetIsEnemy && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling &&
                        WoW.PlayerSpellCharges("Shadowflame") == 2 && WoW.IsSpellInRange("Shadowflame"))
                    {
                        WoW.CastSpell("Shadowflame");
                        return;
                    }

                    if (WoW.IsInCombat && WoW.CanCast("Demonwrath") && WoW.HasPet && WoW.HasTarget && WoW.TargetIsEnemy && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling)
                    {
                        WoW.CastSpell("Demonwrath");
                        return;
                    }
                }

                if (!WoW.IsMoving) /* What to do if we are NOT MOVING */
                {
                    /* double dur = WoW.TargetDebuffTimeRemaining("Shadowflame");
					Log.Write(System.Convert.ToString(dur), Color.Red); */
                    if (WoW.IsInCombat && WoW.HasTarget && WoW.TargetIsEnemy && WoW.PlayerSpellCharges("Shadowflame") == 1 && WoW.TargetHasDebuff("Shadowflame") &&
                        WoW.TargetDebuffTimeRemaining("Shadowflame") < 3 && WoW.IsSpellInRange("Shadowflame") && WoW.CanCast("Shadowflame"))
                    {
                        WoW.CastSpell("Shadowflame");
                        return;
                    }

                    if (WoW.IsInCombat && WoW.HasTarget && WoW.TargetIsEnemy && WoW.HasPet && WoW.CanCast("Wrathstorm") && CombatWatch.ElapsedMilliseconds > 2000)
                    {
                        WoW.CastSpell("Wrathstorm");
                    }

                    if (WoW.IsInCombat && WoW.HasTarget && WoW.TargetIsEnemy && !WoW.PlayerIsCasting && !WoW.IsSpellOnCooldown("Dreadstalkers") && WoW.CurrentSoulShards >= 2 &&
                        WoW.IsSpellInRange("Dreadstalkers") && WoW.CanCast("Dreadstalkers"))
                    {
                        WoW.CastSpell("Dreadstalkers");
                        return;
                    }

                    if (WoW.IsInCombat && WoW.HasTarget && WoW.TargetIsEnemy && !WoW.PlayerIsCasting && WoW.CurrentSoulShards >= 4 && WoW.IsSpellInRange("HoG") && WoW.CanCast("HoG"))
                    {
                        WoW.CastSpell("HoG");
                        return;
                    }

                    if (WoW.IsInCombat && WoW.HasTarget && WoW.TargetIsEnemy && !WoW.PlayerIsCasting && WoW.CurrentSoulShards >= 3 && WoW.IsSpellInRange("HoG") &&
                        WoW.WasLastCasted("Dreadstalkers") && WoW.CanCast("HoG"))
                    {
                        WoW.CastSpell("HoG");
                        return;
                    }

                    if (WoW.IsInCombat && !WoW.PlayerIsCasting && lastpetstatus && !WoW.WasLastCasted("DemonicEmpowerment") && WoW.CanCast("DemonicEmpowerment"))
                    {
                        WoW.CastSpell("DemonicEmpowerment");
                        return;
                    }

                    if (WoW.IsInCombat && WoW.HasTarget && WoW.TargetIsEnemy && !WoW.PlayerIsCasting && !lastpetstatus && !WoW.IsSpellOnCooldown("TK") && WoW.CanCast("TK") &&
                        WoW.WildImpsCount >= 1 && WoW.DreadstalkersCount >= 1)
                    {
                        WoW.CastSpell("TK");
                        return;
                    }

                    if (WoW.IsInCombat && WoW.HasTarget && WoW.TargetIsEnemy && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling && WoW.PlayerSpellCharges("Shadowflame") == 2 &&
                        WoW.IsSpellInRange("Shadowflame") && WoW.CanCast("Shadowflame"))
                    {
                        WoW.CastSpell("Shadowflame");
                        return;
                    }

                    if (WoW.IsInCombat && WoW.Mana < 30 && WoW.HealthPercent > 50 && WoW.CanCast("Life Tap"))
                    {
                        WoW.CastSpell("Life Tap");
                        return;
                    }

                    if (WoW.CanCast("Demonbolt") && WoW.HasTarget && WoW.TargetIsEnemy && !WoW.PlayerIsCasting && WoW.IsInCombat && WoW.IsSpellInRange("Shadowflame"))
                    {
                        WoW.CastSpell("Demonbolt");
                        return;
                    }


                    /* shadowflame,if=debuff.shadowflame.stack>0&remains<action.shadow_bolt.cast_time+travel_time   - DONE
					call_dreadstalkers,if=!talent.summon_darkglare.enabled&(spell_targets.implosion<3|!talent.implosion.enabled) - DONE
					hand_of_guldan,if=soul_shard>=4&!talent.summon_darkglare.enabled - DONE
					hand_of_guldan,if=soul_shard>=3&prev_gcd.call_dreadstalkers - DONE
					demonic_empowerment,if=wild_imp_no_de>3|prev_gcd.hand_of_guldan - DONE
					demonic_empowerment,if=dreadstalker_no_de>0|darkglare_no_de>0|doomguard_no_de>0|infernal_no_de>0|service_no_de>0 - DONE
					felguard:felstorm - DONE
					shadowflame,if=charges=2 - DONE
					thalkiels_consumption,if=(dreadstalker_remaining_duration>execute_time|talent.implosion.enabled&spell_targets.implosion>=3)&wild_imp_count>3&wild_imp_remaining_duration>execute_time - DONE
					life_tap,if=mana.pct<=30 - DONE
					demonbolt - DONE
					life_tap - NOT NEEDED CAN BE CAST OUT OF COMBAT MANUALLY + WE HAVE IT ON MOVING */
                }
            }
            if ((combatRoutine.Type == RotationType.AOE) || (combatRoutine.Type == RotationType.SingleTargetCleave))
            {
                if (WoW.IsMoving) /* AOE WHEN MOVING */
                {
                    if (WoW.HasTarget && WoW.TargetIsEnemy && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling && WoW.IsInCombat && WoW.WasLastCasted("HoG") && WoW.CanCast("Implosion"))
                    {
                        WoW.CastSpell("Implosion");
                        return;
                    }

                    if (WoW.IsInCombat && WoW.HasTarget && WoW.TargetIsEnemy && WoW.PlayerSpellCharges("Shadowflame") == 1 && WoW.TargetHasDebuff("Shadowflame") &&
                        WoW.TargetDebuffTimeRemaining("Shadowflame") < 3 && WoW.CanCast("Shadowflame") && WoW.IsSpellInRange("Shadowflame"))
                    {
                        WoW.CastSpell("Shadowflame");
                        return;
                    }


                    if (WoW.IsInCombat && WoW.HasTarget && WoW.TargetIsEnemy && WoW.HasPet && WoW.CanCast("Wrathstorm") && CombatWatch.ElapsedMilliseconds > 2000)
                    {
                        WoW.CastSpell("Wrathstorm");
                    }

                    if (WoW.IsInCombat && WoW.HasTarget && WoW.CanCast("Shadowflame") && WoW.TargetIsEnemy && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling &&
                        WoW.PlayerSpellCharges("Shadowflame") == 2 && WoW.IsSpellInRange("Shadowflame"))
                    {
                        WoW.CastSpell("Shadowflame");
                        return;
                    }

                    if (!WoW.IsInCombat && WoW.Mana < 80 && WoW.HealthPercent > 75 && WoW.CanCast("Life Tap"))
                    {
                        WoW.CastSpell("Life Tap");
                        return;
                    }

                    if (WoW.IsInCombat && WoW.Mana < 30 && WoW.HealthPercent > 50 && WoW.CanCast("Life Tap"))
                    {
                        WoW.CastSpell("Life Tap");
                        return;
                    }

                    if (WoW.IsInCombat && WoW.CanCast("Demonwrath") && WoW.HasTarget && WoW.HasPet && WoW.TargetIsEnemy && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling)
                    {
                        WoW.CastSpell("Demonwrath");
                        return;
                    }
                }

                if (!WoW.IsMoving) /* AOE WHEN NOT MOVING */
                {
                    if (WoW.HasTarget && WoW.TargetIsEnemy && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling && WoW.IsInCombat && WoW.WasLastCasted("HoG") && WoW.CanCast("Implosion"))
                    {
                        WoW.CastSpell("Implosion");
                        return;
                    }

                    if (WoW.IsInCombat && WoW.HasTarget && WoW.TargetIsEnemy && WoW.PlayerSpellCharges("Shadowflame") == 1 && WoW.TargetHasDebuff("Shadowflame") &&
                        WoW.TargetDebuffTimeRemaining("Shadowflame") < 3 && WoW.CanCast("Shadowflame") && WoW.IsSpellInRange("Shadowflame"))
                    {
                        WoW.CastSpell("Shadowflame");
                        return;
                    }

                    if (WoW.IsInCombat && WoW.HasTarget && WoW.TargetIsEnemy && WoW.HasPet && WoW.CanCast("Wrathstorm") && CombatWatch.ElapsedMilliseconds > 2000)
                    {
                        WoW.CastSpell("Wrathstorm");
                    }

                    if (WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsInCombat && WoW.CurrentSoulShards >= 3 && WoW.IsSpellInRange("HoG") && WoW.CanCast("HoG"))
                    {
                        WoW.CastSpell("HoG");
                        return;
                    }

                    if (WoW.IsInCombat && WoW.HasTarget && WoW.CanCast("Shadowflame") && WoW.TargetIsEnemy && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling &&
                        WoW.PlayerSpellCharges("Shadowflame") == 2 && WoW.IsSpellInRange("Shadowflame"))
                    {
                        WoW.CastSpell("Shadowflame");
                        return;
                    }

                    if (!WoW.IsInCombat && WoW.Mana < 80 && WoW.HealthPercent > 75 && WoW.CanCast("Life Tap"))
                    {
                        WoW.CastSpell("Life Tap");
                        return;
                    }

                    if (WoW.IsInCombat && WoW.Mana < 30 && WoW.HealthPercent > 65 && WoW.CanCast("Life Tap"))
                    {
                        WoW.CastSpell("Life Tap");
                        return;
                    }

                    if (WoW.IsInCombat && WoW.CanCast("Demonwrath") && WoW.HasTarget && WoW.TargetIsEnemy && WoW.HasPet && !WoW.PlayerIsCasting && !WoW.PlayerIsChanneling)
                    {
                        WoW.CastSpell("Demonwrath");
                    }
                }
            }
        }
    }
}

/*
[AddonDetails.db]
AddonAuthor=Nilrem-aka-Dova
AddonName=RGB
WoWVersion=Legion - 70300
[SpellBook.db]
Spell,157695,Demonbolt,D2
Spell,193440,Demonwrath,D8
Spell,1454,Life Tap,Oemtilde
Spell,205181,Shadowflame,D6
Spell,603,Doom,D1
Spell,105174,HoG,D3
Spell,104316,Dreadstalkers,D4
Spell,193396,DemonicEmpowerment,D5
Spell,196277,Implosion,F9
Spell,119914,Wrathstorm,F8
Spell,211714,TK,T
Spell,5512,Healthstone,F10
Spell,18540,Doomguard,F11
Aura,2825,Bloodlust
Aura,80353,TimeWarp
Aura,205181,Shadowflame
Aura,186305,Mount
*/
