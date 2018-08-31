/**
 * Shadow rotation written by Miestro.
 * 
 * TODO:
 *  Allow for toggle of cooldown usage.
 *  
 * 
 * Published November 27th, 2016
 * Updated Febuary 18th, 2016
 */

using System.Diagnostics;
using System.Drawing;
using System.Threading;
using PixelMagic.Helpers;
using System.Windows.Forms;

/**
 * Shadow priest rotation.
*/

namespace PixelMagic.Rotation
{
    internal class MiestroShadow : CombatRoutine
    {
        //General constants
        private const int HEALTH_PERCENT_FOR_SWD = 20;
        private const int PANIC_INSANITY_VALUE = 45;
        private const int INTERRUPT_DELAY = 650;

        //Spell Constants
        private const string SHADOW_PAIN = "Shadow Word: Pain";
        private const string VAMPIRIC_TOUCH = "Vampiric Touch";
        private const string VOID_TORRENT = "Void Torrent";
        private const string MIND_FLAY = "Mind Flay";
        private const string MIND_SEAR = "Mind Sear";
        private const string MIND_BLAST = "Mind Blast";
        private const string SHADOW_MEND = "Shadow Mend";
        private const string POWER_WORD_SHIELD = "Power Word: Shield";
        private const string VOID_BOLT = "Void Bolt";
        private const string VOID_ERUPTION = "Void Eruption";
        private const string SURRENDER_MADNESS = "Surrender to Madness";
        private const string SHADOW_DEATH = "Shadow Word: Death";
        private const string SHADOWFORM = "Shadowform";
        private const string SILENCE = "Silence";
        private const string SHADOW_FIEND = "Shadowfiend";

        //Aura Constants
        private const string VOIDFORM_AURA = "Voidform";
        private const string SHADOWFORM_AURA = "Shadowform";
        private const string POWER_INFUSION = "Power Infusion";

        /// <summary>
        ///     Private variable for timing interrupt delay.
        /// </summary>
        private readonly Stopwatch timer = new Stopwatch();

        /// <summary>
        ///     Whether or not to use cooldowns automatically in the rotation.
        /// </summary>
        private bool useCooldowns = true;

        /**
         * Member Variables
         */

        //Name of the rotation.
        public override string Name => "Shadow Rotation by Miestro";

        //Name of the class.
        public override string Class => "Priest";

        //Settings form, Right side.
        public override Form SettingsForm { get; set; }

        //Initialize, print some details so the user can prepare thier ingame character.
        public override void Initialize()
        {
            Log.Write("Welcome to Miestro's Shadow rotation", Color.Orange);
            Log.Write("Please make sure your specialization is as follows: http://us.battle.net/wow/en/tool/talent-calculator#Xba!0100000", Color.Orange);
            Log.Write("Note: legendaries are not supported. If you need one supported or something fixed, please make note of it in the discord.", Color.Orange);
            //Log.Write("To enable/disable the use of cooldowns, press Alt+f", Color.Green);
            SettingsForm = new Form();
            SettingsForm.KeyUp+=keyUpEvent;
        }

        public override void Stop()
        {
            //Do nothing
        }

        private void keyUpEvent(object sender, KeyEventArgs evt) {
            if (evt.KeyCode==Keys.F) {
                this.useCooldowns=!this.useCooldowns;
                Log.Write(useCooldowns ? "We are now using cooldowns" : "We are no longer using cooldowns", Color.Red);
            }
        }

        public override void Pulse()
        {
            if (WoW.HealthPercent <= 1)
            {
                //Dead
                return;
            }

            //Heal yourself, Can't do damage if you're dead.
            if (WoW.HealthPercent <= 15)
            {
                if (isPlayerBusy(true, false) && !WoW.PlayerHasBuff(POWER_WORD_SHIELD))
                {
                    castWithRangeCheck(POWER_WORD_SHIELD);
                }
                castWithRangeCheck(SHADOW_MEND);
            }
            //Shield if health is dropping.
            if (WoW.HealthPercent <= 45 && !WoW.PlayerHasBuff(POWER_WORD_SHIELD))
            {
                castWithRangeCheck(POWER_WORD_SHIELD, true);
            }

            //Always have shadowform.
            if (!(WoW.PlayerHasBuff(SHADOWFORM_AURA) || WoW.PlayerHasBuff(VOIDFORM_AURA)))
            {
                castWithRangeCheck(SHADOWFORM);
            }

            if (WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsInCombat && WoW.TargetIsVisible)
            {
                if (!WoW.PlayerHasBuff(VOIDFORM_AURA))
                {
                    //Just so happens that the spell and debuff name are the same, this is not ALWAYS the case.
                    maintainDebuff(VAMPIRIC_TOUCH, VAMPIRIC_TOUCH, 5);
                    maintainDebuff(SHADOW_PAIN, SHADOW_PAIN, 2);
                } 
                else 
                {
                    maintainDebuff(VAMPIRIC_TOUCH, VAMPIRIC_TOUCH, WoW.SpellCooldownTimeRemaining(VOID_BOLT));
                    maintainDebuff(SHADOW_PAIN, SHADOW_PAIN, WoW.SpellCooldownTimeRemaining(VOID_BOLT));
                }

                switch (combatRoutine.Type)
                {
                    //Single target
                    case RotationType.SingleTarget:
                        doRotation();
                        break;

                    //Against 2 or more
                    case RotationType.AOE:
                    case RotationType.SingleTargetCleave:
                        doRotation(false);
                        break;
                }
            }

            //Interrupt after a delay.
            if (WoW.TargetIsCasting && WoW.TargetIsEnemy)
            {
                if (timer.ElapsedMilliseconds >= INTERRUPT_DELAY)
                {
                    castWithRangeCheck(SILENCE);
                }
                else
                {
                    timer.Reset();
                    timer.Start();
                }
            }
        }

        /// <summary>
        ///     Do the rotation.
        /// </summary>
        private void doRotation(bool isSingleTarget = true)
        {
            bool ignoreMovement = WoW.PlayerHasBuff(SURRENDER_MADNESS);

            if (WoW.Insanity >= 70 || WoW.PlayerHasBuff(VOIDFORM_AURA))
            {
                //Expend insanity in voidform.
                if (WoW.HasTarget && !WoW.PlayerHasBuff(VOIDFORM_AURA))
                {
                    castWithRangeCheck(VOID_ERUPTION);
                }
                else
                {
                    //If we can, cast it.
                    castWithRangeCheck(VOID_BOLT);

                    //Cast it.
                    if(calculateInsanityDrain() > (WoW.Insanity-35)) { 
                        castWithRangeCheck(VOID_TORRENT, ignoreMovement);
                    }

                    //If the boss health is at or below our set threshold SW:D
                    if (WoW.TargetHealthPercent <= HEALTH_PERCENT_FOR_SWD)
                    {
                        if (WoW.PlayerSpellCharges(SHADOW_DEATH) == 2 && WoW.Insanity <= 70)
                        {
                            //If we have 2 charges, always cast
                            castWithRangeCheck(SHADOW_DEATH);
                        }
                        else if (WoW.PlayerSpellCharges(SHADOW_DEATH) == 1)
                        {
                            //If we have 1 charge, only cast if at high insanity and mindblast is off CD or extremely low insanity
                            if ((WoW.Insanity > PANIC_INSANITY_VALUE && !(WoW.IsSpellOnCooldown(MIND_BLAST) || WoW.IsSpellOnCooldown(VOID_BOLT))) || WoW.Insanity <= calculateInsanityDrain())
                            {
                                castWithRangeCheck(SHADOW_DEATH);
                            }
                        }
                    }

                    if(WoW.Insanity <= 40 && !WoW.PlayerHasBuff(POWER_INFUSION)) {
                        castWithRangeCheck(POWER_INFUSION, false, false);
                    }

                    //Cast shadowfiend if we have more than 15 stacks of voidform aura.
                    if(WoW.PlayerBuffStacks(VOIDFORM_AURA) >= 15) {
                        castWithRangeCheck(SHADOW_FIEND);
                    }

                    //If we can, cast it.
                    castWithRangeCheck(MIND_BLAST);

                    if (!isPlayerBusy(ignoreChanneling: false))
                    {
                        //Always fill with mind flay on single target.
                        castWithRangeCheck(MIND_FLAY);
                    }
                }
            }
            else
            {
                //Build up insanity
                if (WoW.HasTarget && WoW.TargetIsEnemy && WoW.TargetIsVisible)
                {
                    //If we can, cast mind blast.
                    if (castWithRangeCheck(MIND_BLAST))
                    {
                        return;
                    }

                    //If we don't have anything else to do, cast Mind flay.
                    if (!isPlayerBusy(ignoreChanneling: false))
                    {
                        castWithRangeCheck(MIND_FLAY);
                    }
                }
            }
        }

        /// <summary>
        ///     Calculate the amount of insanity currently drained per second
        /// </summary>
        /// <returns>The amount of insanity drained per second</returns>
        private float calculateInsanityDrain()
        {
            return 9 + (WoW.PlayerBuffStacks(VOIDFORM_AURA) - 1/2);
        }

        /// <summary>
        ///     Get whether we can cast spells based on what the player is currently doing.
        /// </summary>
        /// <param name="ignoreMovement">Can we ignore movement</param>
        /// <param name="ignoreChanneling"></param>
        /// <returns>True if we can not currently cast another spell.</returns>
        private bool isPlayerBusy(bool ignoreMovement = false, bool ignoreChanneling = true)
        {
            var canCast = WoW.PlayerIsCasting || (WoW.PlayerIsChanneling && !(ignoreChanneling && !WoW.WasLastCasted(VOID_TORRENT))) || (WoW.IsMoving && ignoreMovement);
            return canCast;
        }

        /// <summary>
        ///     Cast a spell by name. Will check range, cooldown, and visibility. After the spell is cast, the thread will sleep
        ///     for GCD.
        /// </summary>
        /// <param name="spellName">The name of the spell in the spell databse.</param>
        /// <param name="ignoreMovement">Can we cast while moving.</param>
        /// <param name="ignoreChanneling"></param>
        /// <returns>True if the spell was cast, false if it was not.</returns>
        private bool castWithRangeCheck(string spellName, bool ignoreMovement = false, bool ignoreChanneling = true)
        {
            //Can't do range check.
            if (!isPlayerBusy(ignoreMovement, ignoreChanneling) && WoW.CanCast(spellName))
            {
                WoW.CastSpell(spellName);
                if (WoW.IsSpellOnGCD(spellName))
                {
                    Thread.Sleep(WoW.SpellCooldownTimeRemaining(spellName));
                }
                return true;
            }
            return false;
        }

        /// <summary>
        ///     Maintain a debuff if it is not currently on the target or if it's about to expire.
        /// </summary>
        /// <param name="debuffName">The name of the debuff we are maintaining.</param>
        /// <param name="spellName">The name of the spell that applies the debuff.</param>
        /// <param name="minTimeToExpire">The minimum amount of time to allow on the debuff before renewing.</param>
        /// <returns>True if the debuff was renewed, otherwise fasle.</returns>
        private void maintainDebuff(string debuffName, string spellName, float minTimeToExpire)
        {
            if (!WoW.TargetHasDebuff(debuffName) || (WoW.TargetDebuffTimeRemaining(debuffName) < minTimeToExpire))
            {
                castWithRangeCheck(spellName);
            }
        }
    }
}

/*
[AddonDetails.db]
AddonAuthor=Miestro
AddonName=PixelMagic
WoWVersion=Legion - 70300
[SpellBook.db]
Spell,589,Shadow Word: Pain,Q
Spell,34914,Vampiric Touch,E
Spell,205065,Void Torrent,G
Spell,15407,Mind Flay,D1
Spell,8092,Mind Blast,D2
Spell,186263,Shadow Mend,D3
Spell,17,Power Word: Shield,D4
Spell,205448,Void Bolt,D5
Spell,228260,Void Eruption,D5
Spell,193223,Surrender to Madness,F
Spell,32379,Shadow Word: Death,D6
Spell,34433,Shadowfiend,D7
Spell,232698,Shadowform,D0
Spell,15487,Silence,R
Spell,47585,Dispersion,T
Spell,10060,Power Infusion,D9
Aura,232698,Shadowform
Aura,34914,Vampiric Touch
Aura,589,Shadow Word: Pain
Aura,197937,Lingering Insanity
Aura,194249,Voidform
Aura,193223,Surrender to Madness
Aura,17,Power Word: Shield
Aura,10060,Power Infusion
*/
