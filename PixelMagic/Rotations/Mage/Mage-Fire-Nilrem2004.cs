// winifix@gmail.com
// ReSharper disable UnusedMember.Global
// ReSharper disable ConvertPropertyToExpressionBody

using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using PixelMagic.Helpers;

namespace PixelMagic.Rotation
{
    public class FireMageNRX : CombatRoutine
    {
        public static bool Opener;
        public static bool UseLB;
        public static bool autoCD;
        public static bool autointerrupt;
        public static bool ForcePyro;
        public Stopwatch autoCDwatch = new Stopwatch();
        public Stopwatch CombatWatch = new Stopwatch();
        public Stopwatch flamewatch = new Stopwatch();
        public Stopwatch interruptwatch = new Stopwatch();
        public Stopwatch LBwatch = new Stopwatch();
        public Stopwatch meteorwatch = new Stopwatch();
        public Stopwatch openertimer = new Stopwatch();
        public Stopwatch openerwatch = new Stopwatch();

        public Stopwatch togglewatch = new Stopwatch();

        public override string Name
        {
            get { return "FireMage"; }
        }

        public override string Class
        {
            get { return "Mage"; }
        }

        public override Form SettingsForm { get; set; }

        public override void Initialize()
        {
            Log.Write("Fire Mage", Color.Red);

            Log.Write("Welcome to Nilrem2004's FIRE MAGE ROUTINE", Color.Red);
            Log.Write("Suggested build 1: 2112113 - single target/AoE mixed with Living Bomb and Meteor", Color.Red);
            Log.Write("Suggested build 2: 1112123 - single target build with Meteor", Color.Red);
            Log.Write("Talent tiers 1,2 and 5 do not affect rotation so you can change them as you wish", Color.Red);
        }


        public override void Stop()
        {
            Log.Write("Stopping.....", Color.Red);
        }


        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(int vKey);


        public override void Pulse()
        {
            /* Log.Write("Starting.....", Color.Red); */
            /* Place to check target's (boss) buffs/debuffs in order to stop casting */

            /* if (WoW.IsInCombat && WoW.HasTarget && WoW.TargetIsEnemy && WoW.TargetPlayerHasBuff("Focusing")) return; */
            if (togglewatch.ElapsedMilliseconds == 0)
            {
                togglewatch.Start();
                return;
            }

            if (DetectKeyPress.GetKeyState(DetectKeyPress.VK_RCONTROL) < 0)
            {
                if (togglewatch.ElapsedMilliseconds > 1000)
                {
                    combatRoutine.UseCooldowns = !combatRoutine.UseCooldowns;
                    Log.Write("Cooldowns " + (combatRoutine.UseCooldowns ? "On" : "Off"));
                    togglewatch.Restart();
                    if (!UseCooldowns && Opener)
                    {
                        Opener = !Opener;
                        Log.Write("Opener " + (Opener ? "Enabled" : "Disabled"));
                        Log.WritePixelMagic("Disabling Opener because you disabled Cooldowns.", Color.Red);
                    }
                    if (!UseCooldowns && autoCD)
                    {
                        autoCD = !autoCD;
                        Log.Write("Auto Burst " + (Opener ? "Enabled" : "Disabled"));
                        Log.WritePixelMagic("Disabling Automatic burst because you disabled Cooldowns.", Color.Red);
                    }
                }
            }
            if (openerwatch.ElapsedMilliseconds == 0)
            {
                openerwatch.Start();
                Log.Write("To activate/deactivate cooldowns press RIGHT CONTROL button", Color.Black);
                Log.Write("To activate/deactivate auto Combustion/Mirror Image press LEFT ALT button (cooldowns must be ENABLED)", Color.Black);
                Log.Write("it will use it when conditions are met and disable after that so you have to enable manually again", Color.Black);
                Log.Write("To activate/deactivate Opener press F1 (make sure it's not binded in WoW keybinds first!)", Color.Black);
                Log.Write("To activate/deactivate using of Living Bomb press F2 (make sure it's not binded in WoW keybinds first!)", Color.Black);
                Log.Write("To activate/deactivate automatic interrupting F3 (it will interrupt above 80 percent of cast,)", Color.Black);
                Log.Write("To cast Meteor use MOUSE button 3, keep it pressed and cursor where you want to cast Meteor untill it's done.", Color.Black);
                Log.Write("To cast instant Flamestrike use Tilde button (left from number 1), keep mouse cursor where you want to cast Flamestrike.", Color.Black);
                Log.Write("OPENER is used on bosses, it requires Fire Blast(3 charges),Phoenix(3 charges),Mirror Image & Combustion off CD", Color.Black);
                Log.Write("When you enable OPENER, your task is to prepot and precast first spell, routine will do the rest including Mi/Combustion", Color.Black);
                Log.Write("You must first ENABLE COOLDOWNS to be able to use OPENER. When OPENER finishes it's work it will automatically DISABLE", Color.Black);
                Log.Write("itself so you must enable OPENER on next pull. This is to prevent nabness and unwanted routine behaviour.", Color.Black);
                Log.Write("For Flamestrike and Meteor you have to create macros /cast [@cursor] spell_name (Flamestrike or Meteor).", Color.Black);

                return;
            }

            if (DetectKeyPress.GetKeyState(DetectKeyPress.VK_F1) < 0)
            {
                if (openerwatch.ElapsedMilliseconds > 1000)
                {
                    if (!Opener)
                    {
                        if (UseCooldowns && WoW.PlayerSpellCharges("Phoenix") == 3 && WoW.PlayerSpellCharges("Fire Blast") == 3 && !WoW.IsSpellOnCooldown("Combustion") &&
                            !WoW.IsSpellOnCooldown("Mirror Image"))
                        {
                            Opener = !Opener;
                            Log.Write("Opener " + (Opener ? "Enabled" : "Disabled"));
                            Log.Write("Opener " + (Opener ? "Enabled" : "Disabled"), Color.Red);
                            openerwatch.Restart();
                        }
                        else
                        {
                            Log.Write("Error");
                            Log.Write("ERROR: You don't have all necessary CD's/CHARGES to enable OPENER or you didn't ENABLE COOLDOWNS first!", Color.Red);
                            openerwatch.Restart();
                        }
                    }
                    else
                    {
                        Opener = !Opener;
                        Log.Write("Opener " + (Opener ? "Enabled" : "Disabled"));
                        Log.Write("Opener " + (Opener ? "Enabled" : "Disabled"), Color.Red);
                        openerwatch.Restart();
                    }
                }
            }

            if (LBwatch.ElapsedMilliseconds == 0)
            {
                LBwatch.Start();
                return;
            }


            if (DetectKeyPress.GetKeyState(DetectKeyPress.VK_F2) < 0)
            {
                if (LBwatch.ElapsedMilliseconds > 1000)
                {
                    UseLB = !UseLB;
                    Log.Write("Bomb " + (UseLB ? "On" : "Off"));
                    Log.Write("Living Bomb " + (UseLB ? "ON" : "OFF"), Color.Red);
                    LBwatch.Restart();
                }
            }

            if (interruptwatch.ElapsedMilliseconds == 0)
            {
                interruptwatch.Start();
                return;
            }


            if (DetectKeyPress.GetKeyState(DetectKeyPress.VK_F3) < 0)
            {
                if (interruptwatch.ElapsedMilliseconds > 1000)
                {
                    autointerrupt = !autointerrupt;
                    Log.Write("Interrupt " + (autointerrupt ? "On" : "Off"));
                    Log.Write("Auto interrupt " + (autointerrupt ? "ON" : "OFF"), Color.Red);
                    interruptwatch.Restart();
                }
            }


            if (autoCDwatch.ElapsedMilliseconds == 0)
            {
                autoCDwatch.Start();
                return;
            }


            if (WoW.PlayerHasBuff("Mount") || WoW.PlayerHasBuff("Invisibility") || WoW.PlayerHasBuff("InvisiStart")) return;

            /* Log.Write ("Combustion: " + WoW.SpellCooldownTimeRemaining("Combustion")); */

            if (flamewatch.ElapsedMilliseconds == 0)
            {
                flamewatch.Start();
                return;
            }

            if (DetectKeyPress.GetKeyState(DetectKeyPress.VK_OEM_3) < 0 && WoW.PlayerHasBuff("HotStreak") && WoW.IsInCombat && !WoW.IsSpellOnCooldown("Flamestrike") &&
                flamewatch.ElapsedMilliseconds > 1200)
            {
                WoW.CastSpell("Flamestrike");
                flamewatch.Reset();
                Log.Write("Flamestrike CASTED", Color.Black);
                return;
            }

            if (meteorwatch.ElapsedMilliseconds == 0)
            {
                meteorwatch.Start();
                return;
            }

            if (DetectKeyPress.GetKeyState(DetectKeyPress.VK_XBUTTON1) < 0 && WoW.IsInCombat && !WoW.IsSpellOnCooldown("Meteor") && WoW.CanCast("Meteor") &&
                meteorwatch.ElapsedMilliseconds > 1000)
            {
                WoW.CastSpell("Meteor");
                meteorwatch.Reset();
                Log.Write("Meteor CASTED", Color.Black);
                return;
            }

            if (combatRoutine.Type == RotationType.SingleTarget)
            {
                if (CombatWatch.IsRunning && !WoW.IsInCombat)
                {
                    CombatWatch.Reset();
                }
                if (!CombatWatch.IsRunning && WoW.IsInCombat && UseCooldowns && Opener && WoW.HasTarget && WoW.TargetIsEnemy)
                {
                    CombatWatch.Start();
                    Log.Write("Entering Combat, Starting Opener.", Color.Red);
                    Log.Write("Aaalllriiiight! Who ordered up an extra large can of whoop-ass?", Color.Red);
                }

                if (CombatWatch.IsRunning && CombatWatch.ElapsedMilliseconds < 4500)
                {
                    if (WoW.IsInCombat && WoW.HasTarget && WoW.TargetIsEnemy && WoW.PlayerSpellCharges("Phoenix") == 3 && WoW.CanCast("Phoenix") && !WoW.IsSpellOnCooldown("Phoenix") &&
                        !WoW.LastSpell.Equals("Phoenix") && !WoW.LastSpell.Equals("Fire Blast") && WoW.IsSpellInRange("Fireball"))
                    {
                        WoW.CastSpell("Phoenix");
                        return;
                    }
                    if (WoW.IsInCombat && !WoW.IsSpellOnCooldown("Combustion") && WoW.HasTarget && WoW.TargetIsEnemy && WoW.PlayerSpellCharges("Phoenix") == 2 && WoW.CanCast("Combustion") &&
                        WoW.CanCast("Mirror Image"))
                    {
                        Thread.Sleep(100);
                        WoW.CastSpell("Mirror Image");
                        Thread.Sleep(700);
                        WoW.CastSpell("Combustion");
                        Opener = !Opener;
                        Log.Write("Opener Finished" + (Opener ? "Enabled" : "Disabled"));
                        Log.Write("Opener Finished and disabled", Color.Red);
                        CombatWatch.Reset();
                        return;
                    }

                    Log.Write("Executing opener sequence");
                }

                if (CombatWatch.IsRunning && CombatWatch.ElapsedMilliseconds > 6500 && Opener)
                {
                    Opener = !Opener;
                    Log.Write("Opener Finished" + (Opener ? "Enabled" : "Disabled"));
                    Log.Write("Opener Finished and disabled", Color.Red);
                    CombatWatch.Reset();
                }

                if (CombatWatch.IsRunning && CombatWatch.ElapsedMilliseconds < 4300 && Opener)
                {
                    return;
                }

                if (autointerrupt && WoW.IsInCombat && !WoW.IsSpellOnCooldown("Counterspell") && WoW.HasTarget && WoW.TargetIsEnemy && !WoW.PlayerHasBuff("Combustion Aura") &&
                    WoW.TargetIsCasting && WoW.TargetIsCastingAndSpellIsInterruptible && WoW.TargetPercentCast >= 80)
                {
                    WoW.CastSpell("Counterspell");
                    return;
                }

                if (WoW.IsInCombat && WoW.CanCast("Blazing Barrier") && !WoW.PlayerHasBuff("Blazing Barrier Aura") && !WoW.LastSpell.Equals("Blazing Barrier") && WoW.HealthPercent < 70 &&
                    !WoW.PlayerHasBuff("Combustion Aura"))
                {
                    WoW.CastSpell("Blazing Barrier");
                    return;
                }

                if (WoW.IsInCombat && WoW.CanCast("Ice Block") && !WoW.PlayerHasBuff("Ice Block") && WoW.HealthPercent < 20 && !WoW.IsSpellOnCooldown("Ice Block"))
                {
                    WoW.CastSpell("Ice Block");
                    return;
                }

                if (autoCD && WoW.IsInCombat && !WoW.IsSpellOnCooldown("Combustion") && !WoW.IsSpellOnCooldown("Mirror Image") && WoW.HasTarget && WoW.TargetIsEnemy &&
                    WoW.CanCast("Combustion") && WoW.CanCast("Mirror Image") && (WoW.PlayerHasBuff("HotStreak") || WoW.PlayerHasBuff("HeatingUp")))
                {
                    Thread.Sleep(100);
                    WoW.CastSpell("Mirror Image");
                    Thread.Sleep(700);
                    WoW.CastSpell("Combustion");
                    autoCD = !autoCD;
                    Log.Write("Burst Finished");
                    Log.Write("Burst done, disabling burst", Color.Red);
                    return;
                }

                // Log.WritePixelMagic("Force Pyro "+ (ForcePyro ? "TRUE" : "FALSE"), Color.Red);

                // COMBUSTION PHASE //

                if (WoW.PlayerHasBuff("Combustion Aura")) /* What to do if we are in COMBUSTION BURST PHASE  */
                {
                    if (ForcePyro)
                    {
                        ForcePyro = !ForcePyro;
                    }
                    if (WoW.IsInCombat && WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsSpellInRange("Fireball") && WoW.PlayerHasBuff("HotStreak") && WoW.CanCast("Pyroblast"))
                    {
                        WoW.CastSpell("Pyroblast");
                        Thread.Sleep(100);
                        if (WoW.PlayerSpellCharges("Fire Blast") >= 1) WoW.CastSpell("Fire Blast");
                        return;
                    }
                    if (WoW.IsInCombat && WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsSpellInRange("Fireball") && WoW.PlayerSpellCharges("Fire Blast") >= 1 && WoW.PlayerHasBuff("HeatingUp") &&
                        !WoW.LastSpell.Equals("Fire Blast") && !WoW.PlayerHasBuff("HotStreak"))
                    {
                        WoW.CastSpell("Fire Blast");
                        return;
                    }
                    if (WoW.IsInCombat && WoW.HasTarget && WoW.TargetIsEnemy && WoW.PlayerSpellCharges("Fire Blast") == 0 && WoW.CanCast("Phoenix") && WoW.PlayerSpellCharges("Phoenix") >= 1 &&
                        !WoW.IsSpellOnCooldown("Phoenix") && !WoW.PlayerHasBuff("HotStreak") && !WoW.LastSpell.Equals("Phoenix") && WoW.IsSpellInRange("Fireball"))
                    {
                        WoW.CastSpell("Phoenix");
                        return;
                    }
                    if (WoW.IsInCombat && WoW.HasTarget && WoW.TargetIsEnemy && WoW.PlayerSpellCharges("Phoenix") >= 1 && !WoW.IsSpellOnCooldown("Phoenix") && !WoW.PlayerHasBuff("HotStreak") &&
                        !WoW.PlayerHasBuff("HeatingUp") && WoW.CanCast("Phoenix") && !WoW.LastSpell.Equals("Phoenix") && !WoW.LastSpell.Equals("Fire Blast") &&
                        WoW.LastSpell.Equals("Combustion") && WoW.IsSpellInRange("Fireball"))
                    {
                        WoW.CastSpell("Phoenix");
                        return;
                    }
                    if (WoW.IsInCombat && WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsSpellInRange("Fireball") && WoW.CanCast("Scorch") && !WoW.PlayerHasBuff("HotStreak") &&
                        WoW.PlayerSpellCharges("Phoenix") == 0 && !WoW.LastSpell.Equals("Phoenix") && WoW.PlayerSpellCharges("Fire Blast") == 0)
                    {
                        WoW.CastSpell("Scorch");
                        return;
                    }
                    return;
                }

                // END COMBUSTION PHASE //

                // MOVING PHASE //

                if (WoW.IsMoving && !WoW.PlayerHasBuff("Combustion Aura") && !WoW.LastSpell.Equals("Combustion") && !WoW.WasLastCasted("Combustion") && !Opener)
                    /* What to do if we are MOVING */
                {
                    if (ForcePyro)
                    {
                        ForcePyro = !ForcePyro;
                    }
                    if (WoW.IsInCombat && WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsSpellInRange("Fireball") && WoW.PlayerHasBuff("HotStreak") && WoW.CanCast("Pyroblast"))
                    {
                        WoW.CastSpell("Pyroblast");
                        return;
                    }
                    /* if (WoW.IsInCombat && WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsSpellInRange("Fireball") && WoW.CanCast("Mirror Image") 
						 && !WoW.IsSpellOnCooldown("Mirror Image"))
					{ 
						WoW.CastSpell("Mirror Image");
						return;
					}   */
                    if (WoW.IsInCombat && WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsSpellInRange("Fireball") && WoW.PlayerSpellCharges("Fire Blast") >= 1 && WoW.PlayerHasBuff("HeatingUp") &&
                        !WoW.LastSpell.Equals("Fire Blast") && !WoW.PlayerHasBuff("HotStreak") && WoW.IsSpellOnCooldown("Combustion") &&
                        ((WoW.SpellCooldownTimeRemaining("Combustion") > 24 && WoW.PlayerSpellCharges("Fire Blast") >= 1) ||
                         (WoW.SpellCooldownTimeRemaining("Combustion") > 15 && WoW.PlayerSpellCharges("Fire Blast") >= 2) ||
                         (WoW.SpellCooldownTimeRemaining("Combustion") > 8 && WoW.PlayerSpellCharges("Fire Blast") > 2)))
                    {
                        WoW.CastSpell("Fire Blast");
                        return;
                    }
                    if (!UseCooldowns && WoW.IsInCombat && WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsSpellInRange("Fireball") && WoW.PlayerSpellCharges("Fire Blast") >= 1 &&
                        WoW.PlayerHasBuff("HeatingUp") && !WoW.LastSpell.Equals("Fire Blast") && !WoW.PlayerHasBuff("HotStreak"))
                    {
                        WoW.CastSpell("Fire Blast");
                        return;
                    }
                    if (WoW.IsInCombat && WoW.HasTarget && WoW.TargetIsEnemy && WoW.PlayerSpellCharges("Fire Blast") == 0 && WoW.CanCast("Phoenix") && !WoW.PlayerHasBuff("HotStreak") &&
                        WoW.IsSpellOnCooldown("Fire Blast") && !WoW.LastSpell.Equals("Phoenix") && WoW.IsSpellInRange("Fireball") && !WoW.LastSpell.Equals("Fire Blast") &&
                        ((WoW.SpellCooldownTimeRemaining("Combustion") > 20 && WoW.PlayerSpellCharges("Phoenix") > 2) ||
                         (WoW.SpellCooldownTimeRemaining("Combustion") > 30 && WoW.PlayerSpellCharges("Phoenix") > 1) ||
                         (WoW.SpellCooldownTimeRemaining("Combustion") > 65 && WoW.PlayerSpellCharges("Phoenix") == 1)))
                    {
                        WoW.CastSpell("Phoenix");
                        return;
                    }
                    if (!UseCooldowns && WoW.IsInCombat && WoW.HasTarget && WoW.TargetIsEnemy && WoW.PlayerSpellCharges("Fire Blast") == 0 && WoW.CanCast("Phoenix") &&
                        !WoW.PlayerHasBuff("HotStreak") && WoW.IsSpellOnCooldown("Fire Blast") && !WoW.LastSpell.Equals("Fire Blast") && !WoW.LastSpell.Equals("Phoenix") &&
                        WoW.IsSpellInRange("Fireball") && WoW.PlayerSpellCharges("Phoenix") > 1)
                    {
                        WoW.CastSpell("Phoenix");
                        return;
                    }
                    if (UseLB && WoW.IsInCombat && WoW.HasTarget && WoW.TargetIsEnemy && WoW.CanCast("Living Bomb") && !WoW.IsSpellOnCooldown("Living Bomb") && !WoW.PlayerHasBuff("HotStreak") &&
                        WoW.IsSpellInRange("Fireball"))
                    {
                        WoW.CastSpell("Living Bomb");
                        return;
                    }
                    if (WoW.IsInCombat && WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsSpellInRange("Scorch") && WoW.CanCast("Scorch") && !WoW.PlayerHasBuff("HotStreak"))
                    {
                        WoW.CastSpell("Scorch");
                        return;
                    }
                    return;
                }

                // END MOVING PHASE //

                // SINGLE TARGET STAND STILL PHASE //

                if (!WoW.IsMoving && !WoW.PlayerHasBuff("Combustion Aura") && !WoW.LastSpell.Equals("Combustion") && !WoW.WasLastCasted("Combustion") && !Opener)
                    /* What to do if we are NOT MOVING - NON BURST PHASE */
                {
                    /* double dur = WoW.GetDebuffTimeRemaining("Shadowflame");
					Log.Write(System.Convert.ToString(dur), Color.Red); */
                    if (ForcePyro)
                    {
                        ForcePyro = !ForcePyro;
                    }
                    if (WoW.IsInCombat && WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsSpellInRange("Fireball") && WoW.PlayerHasBuff("HotStreak") && WoW.CanCast("Pyroblast") &&
                        !WoW.WasLastCasted("Pyroblast"))
                    {
                        WoW.CastSpell("Pyroblast");
                        return;
                    }

                    // Legendary Bracers support
                    if (WoW.IsInCombat && WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsSpellInRange("Fireball") && WoW.CanCast("Pyroblast") && !WoW.WasLastCasted("Pyroblast") &&
                        !WoW.PlayerIsCasting && !WoW.PlayerHasBuff("Combustion Aura") && WoW.PlayerHasBuff("Legendary Bracers") && WoW.PlayerBuffTimeRemaining("Legendary Bracers") > 4 &&
                        !WoW.PlayerHasBuff("Hot Streak!"))
                    {
                        WoW.CastSpell("Pyroblast");
                        return;
                    }
                    // END Legendary Bracers support

                    /* if (WoW.IsInCombat && WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsSpellInRange("Fireball") && WoW.CanCast("Mirror Image") 
						 && !WoW.IsSpellOnCooldown("Mirror Image"))
					{ 
						WoW.CastSpell("Mirror Image");
						return;
					}   */
                    /* if (WoW.IsInCombat && WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsSpellInRange("Fireball") && WoW.CanCast("Cinderstorm") 
						&& WoW.TargetHasDebuff("Ignite") && !WoW.IsSpellOnCooldown("Cinderstorm"))
					{ 
						WoW.CastSpell("Cinderstorm");
						return;
					}   */
                    if (WoW.IsInCombat && WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsSpellInRange("Fireball") && WoW.PlayerSpellCharges("Fire Blast") >= 1 && WoW.PlayerHasBuff("HeatingUp") &&
                        !WoW.LastSpell.Equals("Fire Blast") && !WoW.PlayerHasBuff("HotStreak") && WoW.IsSpellOnCooldown("Combustion") &&
                        ((WoW.SpellCooldownTimeRemaining("Combustion") > 24 && WoW.PlayerSpellCharges("Fire Blast") >= 1) ||
                         (WoW.SpellCooldownTimeRemaining("Combustion") > 15 && WoW.PlayerSpellCharges("Fire Blast") >= 2) ||
                         (WoW.SpellCooldownTimeRemaining("Combustion") > 8 && WoW.PlayerSpellCharges("Fire Blast") > 2)))
                    {
                        WoW.CastSpell("Fire Blast");
                        return;
                    }
                    if (!UseCooldowns && WoW.IsInCombat && WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsSpellInRange("Fireball") && WoW.PlayerSpellCharges("Fire Blast") >= 1 &&
                        WoW.PlayerHasBuff("HeatingUp") && !WoW.LastSpell.Equals("Fire Blast") && !WoW.PlayerHasBuff("HotStreak"))
                    {
                        WoW.CastSpell("Fire Blast");
                        return;
                    }
                    if (WoW.IsInCombat && WoW.HasTarget && WoW.TargetIsEnemy && WoW.PlayerSpellCharges("Fire Blast") == 0 && WoW.CanCast("Phoenix") && !WoW.PlayerHasBuff("HotStreak") &&
                        WoW.IsSpellOnCooldown("Fire Blast") && !WoW.LastSpell.Equals("Phoenix") && WoW.IsSpellInRange("Fireball") && !WoW.LastSpell.Equals("Fire Blast") &&
                        ((WoW.SpellCooldownTimeRemaining("Combustion") > 20 && WoW.PlayerSpellCharges("Phoenix") > 2) ||
                         (WoW.SpellCooldownTimeRemaining("Combustion") > 30 && WoW.PlayerSpellCharges("Phoenix") > 1) ||
                         (WoW.SpellCooldownTimeRemaining("Combustion") > 65 && WoW.PlayerSpellCharges("Phoenix") == 1)))
                    {
                        WoW.CastSpell("Phoenix");
                        return;
                    }
                    if (!UseCooldowns && WoW.IsInCombat && WoW.HasTarget && WoW.TargetIsEnemy && WoW.PlayerSpellCharges("Fire Blast") == 0 && WoW.CanCast("Phoenix") &&
                        !WoW.PlayerHasBuff("HotStreak") && WoW.IsSpellOnCooldown("Fire Blast") && !WoW.LastSpell.Equals("Fire Blast") && !WoW.LastSpell.Equals("Phoenix") &&
                        WoW.IsSpellInRange("Fireball") && WoW.PlayerSpellCharges("Phoenix") > 1)
                    {
                        WoW.CastSpell("Phoenix");
                        return;
                    }
                    if (UseLB && WoW.IsInCombat && WoW.HasTarget && WoW.TargetIsEnemy && WoW.CanCast("Living Bomb") && !WoW.IsSpellOnCooldown("Living Bomb") && !WoW.PlayerHasBuff("HotStreak") &&
                        WoW.IsSpellInRange("Fireball"))
                    {
                        WoW.CastSpell("Living Bomb");
                        return;
                    }
                    if (WoW.IsInCombat && WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsSpellInRange("Fireball") && WoW.CanCast("Fireball"))
                    {
                        WoW.CastSpell("Fireball");
                        return;
                    }
                }

                return;
            }
            if ((combatRoutine.Type == RotationType.AOE) || (combatRoutine.Type == RotationType.SingleTargetCleave))
            {
                if (WoW.IsMoving) /* AOE WHEN MOVING */
                {
                }

                if (!WoW.IsMoving) /* AOE WHEN NOT MOVING */
                {
                }
            }
        }
    }
}

/*
[AddonDetails.db]
AddonAuthor=Nilrem2004
AddonName=AuctionLite
WoWVersion=Legion - 70000
[SpellBook.db]
Spell,108853,Fire Blast,D1
Spell,194466,Phoenix,D2
Spell,133,Fireball,D3
Spell,11366,Pyroblast,D4
Spell,116011,RoP,D5
Spell,153561,Meteor,D6
Spell,44457,Living Bomb,D8
Spell,2120,Flamestrike,D9
Spell,2948,Scorch,F5
Spell,2139,Counterspell,F6
Spell,55342,Mirror Image,F7
Spell,235313,Blazing Barrier,F8
Spell,190319,Combustion,F9
Spell,45438,Ice Block,F10
Aura,235313,Blazing Barrier Aura
Aura,190319,Combustion Aura
Aura,48107,HeatingUp
Aura,48108,HotStreak
Aura,12654,Ignite
Aura,45438,Ice Block
Aura,209455,Legendary Bracers
Aura,32612,Invisibility
Aura,66,InvisiStart
Aura,186305,Mount
Item,5512,Healthstone
*/
