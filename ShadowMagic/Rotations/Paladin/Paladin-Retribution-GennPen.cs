// ReSharper disable RedundantUsingDirective

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Timers;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Windows.Forms;
using ShadowMagic.Helpers;
using ShadowMagic.GUI;

// ReSharper disable CheckNamespace
// ReSharper disable NotAccessedField.Local
// ReSharper disable ArrangeThisQualifier

namespace ShadowMagic.Rotation
{
    public class RetributionPaladinGP : CombatRoutine
    {
        public static int PulseTick;
        private bool BloodLustUp;
        private int Gcd;

        private int HolyPower;
        private bool InMeleeRange;
        public override string Name => "Retribution Paladin";
        public override string Class => "Paladin";
        private int BattleTime => 9999;
        private bool CanCastFinisher3hp => HolyPower >= (TheFiresOfJustice.PlayerBuff ? 2 : 3) || DivinePurpose.PlayerBuff;

        private int TargetsDivineStorm
        {
            get
            {
                switch (combatRoutine.Type)
                {
                    case RotationType.AOE:
                        return 5;
                    case RotationType.SingleTargetCleave:
                        return 2;
                    case RotationType.SingleTarget:
                        return 1;
                    default:
                        return 1;
                }
            }
        }

        //////////// TALENTS (tested on 1112112) ////////////////////
        //15
        private bool TalentFinalVerdict => true;
        private bool TalentExecutionSentence => false; // bugged, low dps
        private bool TalentConsecration => false;
        //30
        private bool TalentTheFiresOfJustice => true;
        private bool TalentZeal => false;
        private bool TalentGreaterJudgment => false;
        //45
        private bool TalentFistOfJustice => true;
        private bool TalentRepentance => false;
        private bool TalentBlindingLight => false;
        //60
        private bool TalentVirtuesBlade => false;
        private bool TalentBladeOfWrath => true;
        private bool TalentDivineHammer => false;
        //75
        private bool TalentJusticarsVengeance => true;
        private bool TalentEyeForAnEye => false;
        private bool TalentWordOfGlory => false;
        //90
        private bool TalentDivineIntervention => true;
        private bool TalentCavalier => false;
        private bool TalentJudgmentOfLight => false;
        //100
        private bool TalentDivinePurpose => false; // bugged
        private bool TalentCrusade => true;
        private bool TalentHolyWrath => false;
        ///////////////////////////////////////////////////////

        private ClassSpell Zeal { get; } = new ClassSpell("Zeal");
        private ClassSpell JusticarsVengeance { get; } = new ClassSpell("Justicars Vengeance");
        private ClassSpell ShieldOfVengeance { get; } = new ClassSpell("Shield of Vengeance");
        private ClassSpell HammerOfJustice { get; } = new ClassSpell("Hammer of Justice");
        private ClassSpell ExecutionSentence { get; } = new ClassSpell("Execution Sentence");
        private ClassSpell LayOnHands { get; } = new ClassSpell("Lay on Hands");
        private ClassSpell WakeOfAshes { get; } = new ClassSpell("Wake of Ashes"); // SpellCooldownRemaining
        private ClassSpell DivineStorm { get; } = new ClassSpell("Divine Storm");
        private ClassSpell BladeOfJustice { get; } = new ClassSpell("Blade of Justice"); // SpellCooldownRemaining
        private ClassSpell DivineHammer { get; } = new ClassSpell("Divine Hammer"); // SpellCooldownRemaining
        private ClassSpell CrusaderStrike { get; } = new ClassSpell("Crusader Strike"); // SpellCooldownRemaining
        private ClassSpell TemplarVerdict { get; } = new ClassSpell("Templar Verdict");
        private ClassSpell Judgement { get; } = new ClassSpell("Judgement"); // SpellCooldownRemaining
        private ClassSpell JudgementDebuff { get; } = new ClassSpell("Judgement Debuff"); // TargetDebuffRemaining
        private ClassSpell Crusade { get; } = new ClassSpell("Crusade"); // SpellCooldownRemaining
        private ClassSpell AvengingWrath { get; } = new ClassSpell("Avenging Wrath");
        private ClassSpell FlashOfLight { get; } = new ClassSpell("Flash of Light");
        private ClassSpell HolyWrath { get; } = new ClassSpell("Holy Wrath");
        private ClassSpell Consecration { get; } = new ClassSpell("Consecration");
        private ClassSpell DivinePurpose { get; } = new ClassSpell("Divine Purpose"); // PlayerBuffRemaining
        private ClassSpell TheFiresOfJustice { get; } = new ClassSpell("The Fires of Justice");
        private ClassSpell Forbearance { get; } = new ClassSpell("Forbearance");
        private ClassSpell WhisperOfTheNathrezim { get; } = new ClassSpell("Whisper of the Nathrezim"); // PlayerBuffRemaining

        public override Form SettingsForm { get; set; }

        public override void Stop()
        {
        }

        private int CooldownWithHaste(int cooldown) => (int) (cooldown/(1 + (double) WoW.HastePercent/100));

        public override void Pulse()
        {
            new Thread(PulseUpdateSpells).Start();

            PulseTick = Environment.TickCount;
            Gcd = CooldownWithHaste(1500);
            HolyPower = WoW.CurrentHolyPower;
            InMeleeRange = WoW.CanCast("Templar Verdict", false, false, true, false, true);
            BloodLustUp = WoW.PlayerHasBuff("Bloodlust") || WoW.PlayerHasBuff("Time Warp") || WoW.PlayerHasBuff("Netherwinds") || WoW.PlayerHasBuff("Drums of War");


            //Log.Write("11 " + BladeOfJustice.SpellCanCast);

            if (WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsInCombat && !WoW.PlayerIsChanneling && !WoW.PlayerIsCasting && !WoW.PlayerHasBuff("Mount"))
            {
                if (false)
                {
                    //System.Diagnostics.Stopwatch sw = new Stopwatch();
                    //sw.Start();

                    //PulseRotattion();

                    //sw.Stop();
                    //Log.Write("## " + sw.ElapsedMilliseconds);
                }
                PulseRotattion();
            }
        }

        private void PulseUpdateSpells()
        {
            WakeOfAshes.PulseSpellCooldownRemaining();
            BladeOfJustice.PulseSpellCooldownRemaining();
            DivineHammer.PulseSpellCooldownRemaining();
            CrusaderStrike.PulseSpellCooldownRemaining();
            Judgement.PulseSpellCooldownRemaining();
            Crusade.PulseSpellCooldownRemaining();
            Zeal.PulseSpellCooldownRemaining();

            JudgementDebuff.PulseTargetDebuffRemaining();

            DivinePurpose.PulsePlayerBuffRemaining();
            WhisperOfTheNathrezim.PulsePlayerBuffRemaining();
        }

        private void PulseRotattion()
        {
            //actions=auto_attack
            //actions+=/rebuke
            //actions+=/potion,name=old_war,if=(buff.bloodlust.react|buff.avenging_wrath.up|buff.crusade.up&buff.crusade.remains<25|target.time_to_die<=40)
            //actions+=/use_item,name=faulty_countermeasure,if=(buff.avenging_wrath.up|buff.crusade.up)
            //actions+=/blood_fury
            //actions+=/berserking
            //actions+=/arcane_torrent,if=holy_power<5&(buff.crusade.up|buff.avenging_wrath.up|time<2)
            //actions+=/judgment,if=time<2
            //actions+=/blade_of_justice,if=time<2&(equipped.137048|race.blood_elf)
            //actions+=/divine_hammer,if=time<2&(equipped.137048|race.blood_elf)
            //actions+=/wake_of_ashes,if=holy_power<=1&time<2
            //actions+=/holy_wrath
            //actions+=/avenging_wrath
            //actions+=/shield_of_vengeance
            //actions+=/crusade,if=holy_power>=5&!equipped.137048|((equipped.137048|race.blood_elf)&time<2|time>2&holy_power>=4)

            //actions+=/execution_sentence,if=spell_targets.divine_storm<=3&(cooldown.judgment.remains<gcd*4.5|debuff.judgment.remains>gcd*4.67)&(!talent.crusade.enabled|cooldown.crusade.remains>gcd*2)
            if (TalentExecutionSentence && CanCastFinisher3hp)
            {
                if (TargetsDivineStorm <= 3 && (Judgement.SpellCooldownRemaining < Gcd*4.5 || JudgementDebuff.TargetDebuffRemaining > Gcd*4.67) &&
                    (!TalentCrusade || Crusade.SpellCooldownRemaining > Gcd*2))
                {
                    if (ExecutionSentence.SpellCanCast && InMeleeRange)
                    {
                        ExecutionSentence.Cast();
                        return;
                    }
                }
            }


            //actions+=/divine_storm,if=debuff.judgment.up&spell_targets.divine_storm>=2&buff.divine_purpose.up&buff.divine_purpose.remains<gcd*2
            if (CanCastFinisher3hp)
            {
                if (JudgementDebuff.TargetDebuff && TargetsDivineStorm >= 2 && DivinePurpose.PlayerBuff && DivinePurpose.PlayerBuffRemaining < Gcd*2)
                {
                    if (DivineStorm.SpellCanCastBlind && InMeleeRange)
                    {
                        DivineStorm.Cast();
                        return;
                    }
                }
            }

            //actions+=/divine_storm,if=debuff.judgment.up&spell_targets.divine_storm>=2&holy_power>=5&buff.divine_purpose.react
            if (CanCastFinisher3hp)
            {
                if (JudgementDebuff.TargetDebuff && TargetsDivineStorm >= 2 && HolyPower >= 5 && DivinePurpose.PlayerBuff)
                {
                    if (DivineStorm.SpellCanCastBlind && InMeleeRange)
                    {
                        DivineStorm.Cast();
                        return;
                    }
                }
            }

            //actions+=/divine_storm,if=debuff.judgment.up&spell_targets.divine_storm>=2&holy_power>=3&buff.crusade.up&(buff.crusade.stack<15|buff.bloodlust.up)
            if (CanCastFinisher3hp)
            {
                if (JudgementDebuff.TargetDebuff && TargetsDivineStorm >= 2 && HolyPower >= 3 && Crusade.PlayerBuff && (Crusade.PlayerBuffStacks < 15 || BloodLustUp))
                {
                    if (DivineStorm.SpellCanCastBlind && InMeleeRange)
                    {
                        DivineStorm.Cast();
                        return;
                    }
                }
            }

            //actions+=/divine_storm,if=debuff.judgment.up&spell_targets.divine_storm>=2&holy_power>=5&(!talent.crusade.enabled|cooldown.crusade.remains>gcd*3)
            if (CanCastFinisher3hp)
            {
                if (JudgementDebuff.TargetDebuff && TargetsDivineStorm >= 2 && HolyPower >= 5 && (!TalentCrusade || Crusade.PlayerBuffRemaining > Gcd*3))
                {
                    if (DivineStorm.SpellCanCastBlind && InMeleeRange)
                    {
                        DivineStorm.Cast();
                        return;
                    }
                }
            }

            //actions+=/templars_verdict,if=debuff.judgment.up&buff.divine_purpose.up&buff.divine_purpose.remains<gcd*2
            if (CanCastFinisher3hp)
            {
                if (JudgementDebuff.TargetDebuff && DivinePurpose.PlayerBuff && DivinePurpose.PlayerBuffRemaining < Gcd*2)
                {
                    if (TemplarVerdict.SpellCanCast && InMeleeRange)
                    {
                        TemplarVerdict.Cast();
                        return;
                    }
                }
            }

            //actions+=/templars_verdict,if=debuff.judgment.up&holy_power>=5&buff.divine_purpose.react
            if (CanCastFinisher3hp)
            {
                if (JudgementDebuff.TargetDebuff && HolyPower >= 5 && DivinePurpose.PlayerBuff)
                {
                    if (TemplarVerdict.SpellCanCast && InMeleeRange)
                    {
                        TemplarVerdict.Cast();
                        return;
                    }
                }
            }

            //actions+=/templars_verdict,if=debuff.judgment.up&holy_power>=3&buff.crusade.up&(buff.crusade.stack<15|buff.bloodlust.up)
            if (CanCastFinisher3hp)
            {
                if (JudgementDebuff.TargetDebuff && HolyPower >= 3 && Crusade.PlayerBuff && (Crusade.PlayerBuffStacks < 15 || BloodLustUp))
                {
                    if (TemplarVerdict.SpellCanCast && InMeleeRange)
                    {
                        TemplarVerdict.Cast();
                        return;
                    }
                }
            }

            //actions+=/templars_verdict,if=debuff.judgment.up&holy_power>=5&(!talent.crusade.enabled|cooldown.crusade.remains>gcd*3)
            if (CanCastFinisher3hp)
            {
                if (JudgementDebuff.TargetDebuff && HolyPower >= 5 && (!TalentCrusade || Crusade.SpellCooldownRemaining > Gcd*3))
                {
                    if (TemplarVerdict.SpellCanCast && InMeleeRange)
                    {
                        TemplarVerdict.Cast();
                        return;
                    }
                }
            }

            //actions+=/divine_storm,if=debuff.judgment.up&holy_power>=3&spell_targets.divine_storm>=2&(cooldown.wake_of_ashes.remains<gcd*2&artifact.wake_of_ashes.enabled|buff.whisper_of_the_nathrezim.up&buff.whisper_of_the_nathrezim.remains<gcd)&(!talent.crusade.enabled|cooldown.crusade.remains>gcd*4)
            if (CanCastFinisher3hp)
            {
                if (JudgementDebuff.TargetDebuff && HolyPower >= 3 && TargetsDivineStorm >= 2 &&
                    (WakeOfAshes.SpellCooldownRemaining < Gcd*2 /*&& true*/|| WhisperOfTheNathrezim.PlayerBuff && WhisperOfTheNathrezim.PlayerBuffRemaining < Gcd) &&
                    (!TalentCrusade || Crusade.SpellCooldownRemaining > Gcd*4))
                {
                    if (DivineStorm.SpellCanCastBlind && InMeleeRange)
                    {
                        DivineStorm.Cast();
                        return;
                    }
                }
            }

            //actions+=/templars_verdict,if=debuff.judgment.up&holy_power>=3&(cooldown.wake_of_ashes.remains<gcd*2&artifact.wake_of_ashes.enabled|buff.whisper_of_the_nathrezim.up&buff.whisper_of_the_nathrezim.remains<gcd)&(!talent.crusade.enabled|cooldown.crusade.remains>gcd*4)
            if (CanCastFinisher3hp)
            {
                if (JudgementDebuff.TargetDebuff && HolyPower >= 3 &&
                    (WakeOfAshes.SpellCooldownRemaining < Gcd*2 /*&& true*/|| WhisperOfTheNathrezim.PlayerBuff && WhisperOfTheNathrezim.PlayerBuffRemaining < Gcd) &&
                    (!TalentCrusade || Crusade.SpellCooldownRemaining > Gcd*4))
                {
                    if (TemplarVerdict.SpellCanCast && InMeleeRange)
                    {
                        TemplarVerdict.Cast();
                        return;
                    }
                }
            }

            //actions+=/wake_of_ashes,if=holy_power=0|holy_power=1&(cooldown.blade_of_justice.remains>gcd|cooldown.divine_hammer.remains>gcd)|holy_power=2&(cooldown.zeal.charges_fractional<=0.65|cooldown.crusader_strike.charges_fractional<=0.65)
            if (HolyPower <= 1 && (BladeOfJustice.SpellCooldownRemaining > Gcd || DivineHammer.SpellCooldownRemaining > Gcd) ||
                HolyPower == 2 && (Zeal.SpellChargesFractional <= 0.65 || CrusaderStrike.SpellChargesFractional <= 0.65))
            {
                if (WakeOfAshes.SpellCanCastBlind && InMeleeRange)
                {
                    WakeOfAshes.Cast();
                    return;
                }
            }

            //actions+=/blade_of_justice,if=holy_power<=3&buff.whisper_of_the_nathrezim.up&buff.whisper_of_the_nathrezim.remains>gcd&buff.whisper_of_the_nathrezim.remains<gcd*3&debuff.judgment.up&debuff.judgment.remains>gcd*2
            if (!TalentDivineHammer)
            {
                if (HolyPower <= 3 && WhisperOfTheNathrezim.PlayerBuff && WhisperOfTheNathrezim.PlayerBuffRemaining > Gcd && WhisperOfTheNathrezim.PlayerBuffRemaining < Gcd*3 &&
                    JudgementDebuff.TargetDebuff && JudgementDebuff.TargetDebuffRemaining > Gcd*2)
                {
                    if (BladeOfJustice.SpellCanCast)
                    {
                        BladeOfJustice.Cast();
                        return;
                    }
                }
            }

            //actions+=/divine_hammer,if=holy_power<=3&buff.whisper_of_the_nathrezim.up&buff.whisper_of_the_nathrezim.remains>gcd&buff.whisper_of_the_nathrezim.remains<gcd*3&debuff.judgment.up&debuff.judgment.remains>gcd*2
            if (TalentDivineHammer)
            {
                if (HolyPower <= 3 && WhisperOfTheNathrezim.PlayerBuff && WhisperOfTheNathrezim.PlayerBuffRemaining > Gcd && WhisperOfTheNathrezim.PlayerBuffRemaining < Gcd*3 &&
                    JudgementDebuff.TargetDebuff && JudgementDebuff.TargetDebuffRemaining > Gcd*2)
                {
                    if (DivineHammer.SpellCanCastBlind && InMeleeRange)
                    {
                        DivineHammer.Cast();
                        return;
                    }
                }
            }

            //actions+=/blade_of_justice,if=talent.blade_of_wrath.enabled&holy_power<=3
            if (!TalentDivineHammer)
            {
                if (TalentBladeOfWrath && HolyPower <= 3)
                {
                    if (BladeOfJustice.SpellCanCast)
                    {
                        BladeOfJustice.Cast();
                        return;
                    }
                }
            }

            //actions+=/zeal,if=charges=2&holy_power<=4
            if (TalentZeal)
            {
                if (Zeal.SpellCharges == 2 && HolyPower <= 4)
                {
                    if (Zeal.SpellCanCastBlind && InMeleeRange)
                    {
                        Zeal.Cast();
                        return;
                    }
                }
            }

            //actions+=/crusader_strike,if=charges=2&holy_power<=4
            if (!TalentZeal)
            {
                if (CrusaderStrike.SpellCharges == 2 && HolyPower <= 4)
                {
                    if (CrusaderStrike.SpellCanCastBlind && InMeleeRange)
                    {
                        CrusaderStrike.Cast();
                        return;
                    }
                }
            }

            //actions+=/blade_of_justice,if=holy_power<=2|(holy_power<=3&(cooldown.zeal.charges_fractional<=1.34|cooldown.crusader_strike.charges_fractional<=1.34))
            if (!TalentDivineHammer)
            {
                if (HolyPower <= 2 || (HolyPower <= 3 && (Zeal.SpellChargesFractional <= 1.34 || CrusaderStrike.SpellChargesFractional <= 1.34)))
                {
                    if (BladeOfJustice.SpellCanCast)
                    {
                        BladeOfJustice.Cast();
                        return;
                    }
                }
            }

            //actions+=/divine_hammer,if=holy_power<=2|(holy_power<=3&(cooldown.zeal.charges_fractional<=1.34|cooldown.crusader_strike.charges_fractional<=1.34))
            if (TalentDivineHammer)
            {
                if (HolyPower <= 2 || (HolyPower <= 3 && (Zeal.SpellChargesFractional <= 1.34 || CrusaderStrike.SpellChargesFractional <= 1.34)))
                {
                    if (DivineHammer.SpellCanCastBlind && InMeleeRange)
                    {
                        DivineHammer.Cast();
                        return;
                    }
                }
            }

            //actions+=/judgment,if=holy_power>=3|((cooldown.zeal.charges_fractional<=1.67|cooldown.crusader_strike.charges_fractional<=1.67)&(cooldown.divine_hammer.remains>gcd|cooldown.blade_of_justice.remains>gcd))|(talent.greater_judgment.enabled&target.health.pct>50)
            if (HolyPower >= 3 ||
                ((Zeal.SpellChargesFractional <= 1.67 || CrusaderStrike.SpellChargesFractional <= 1.67) &&
                 (DivineHammer.SpellCooldownRemaining > Gcd || BladeOfJustice.SpellCooldownRemaining > Gcd)) || (TalentGreaterJudgment && WoW.TargetHealthPercent > 50))
            {
                if (Judgement.SpellCanCast && InMeleeRange)
                {
                    Judgement.Cast();
                    return;
                }
            }

            //actions+=/consecration
            if (TalentConsecration)
            {
                if (true)
                {
                    if (Consecration.SpellCanCastBlind && InMeleeRange)
                    {
                        Consecration.Cast();
                        return;
                    }
                }
            }

            //actions+=/divine_storm,if=debuff.judgment.up&spell_targets.divine_storm>=2&buff.divine_purpose.react
            if (CanCastFinisher3hp)
            {
                if (JudgementDebuff.TargetDebuff && TargetsDivineStorm >= 2 && DivinePurpose.PlayerBuff)
                {
                    if (DivineStorm.SpellCanCastBlind && InMeleeRange)
                    {
                        DivineStorm.Cast();
                        return;
                    }
                }
            }

            //actions+=/divine_storm,if=debuff.judgment.up&spell_targets.divine_storm>=2&buff.the_fires_of_justice.react&(!talent.crusade.enabled|cooldown.crusade.remains>gcd*3)
            if (CanCastFinisher3hp)
            {
                if (JudgementDebuff.TargetDebuff && TargetsDivineStorm >= 2 && TheFiresOfJustice.PlayerBuff && (!TalentCrusade || Crusade.SpellCooldownRemaining > Gcd*3))
                {
                    if (DivineStorm.SpellCanCastBlind && InMeleeRange)
                    {
                        DivineStorm.Cast();
                        return;
                    }
                }
            }

            //actions+=/divine_storm,if=debuff.judgment.up&spell_targets.divine_storm>=2&(holy_power>=4|((cooldown.zeal.charges_fractional<=1.34|cooldown.crusader_strike.charges_fractional<=1.34)&(cooldown.divine_hammer.remains>gcd|cooldown.blade_of_justice.remains>gcd)))&(!talent.crusade.enabled|cooldown.crusade.remains>gcd*4)
            if (CanCastFinisher3hp)
            {
                if (JudgementDebuff.TargetDebuff && TargetsDivineStorm >= 2 &&
                    (HolyPower >= 4 ||
                     ((Zeal.SpellChargesFractional <= 1.34 || CrusaderStrike.SpellChargesFractional <= 1.34) &&
                      (DivineHammer.SpellCooldownRemaining > Gcd || BladeOfJustice.SpellCooldownRemaining > Gcd))) && (!TalentCrusade || Crusade.SpellCooldownRemaining > Gcd*4))
                {
                    if (DivineStorm.SpellCanCastBlind && InMeleeRange)
                    {
                        DivineStorm.Cast();
                        return;
                    }
                }
            }

            //actions+=/templars_verdict,if=debuff.judgment.up&buff.divine_purpose.react
            if (CanCastFinisher3hp)
            {
                if (JudgementDebuff.TargetDebuff && DivinePurpose.PlayerBuff)
                {
                    if (TemplarVerdict.SpellCanCast && InMeleeRange)
                    {
                        TemplarVerdict.Cast();
                        return;
                    }
                }
            }

            //actions+=/templars_verdict,if=debuff.judgment.up&buff.the_fires_of_justice.react&(!talent.crusade.enabled|cooldown.crusade.remains>gcd*3)
            if (CanCastFinisher3hp)
            {
                if (JudgementDebuff.TargetDebuff && TheFiresOfJustice.PlayerBuff && (!TalentCrusade || Crusade.SpellCooldownRemaining > Gcd*3))
                {
                    if (TemplarVerdict.SpellCanCast && InMeleeRange)
                    {
                        TemplarVerdict.Cast();
                        return;
                    }
                }
            }

            //actions+=/templars_verdict,if=debuff.judgment.up&(holy_power>=4|((cooldown.zeal.charges_fractional<=1.34|cooldown.crusader_strike.charges_fractional<=1.34)&(cooldown.divine_hammer.remains>gcd|cooldown.blade_of_justice.remains>gcd)))&(!talent.crusade.enabled|cooldown.crusade.remains>gcd*4)
            if (CanCastFinisher3hp)
            {
                if (JudgementDebuff.TargetDebuff &&
                    (HolyPower >= 4 ||
                     ((Zeal.SpellChargesFractional <= 1.34 || CrusaderStrike.SpellChargesFractional <= 1.34) &&
                      (DivineHammer.SpellCooldownRemaining > Gcd || BladeOfJustice.SpellCooldownRemaining > Gcd))) && (!TalentCrusade || Crusade.SpellCooldownRemaining > Gcd*4))
                {
                    if (TemplarVerdict.SpellCanCast && InMeleeRange)
                    {
                        TemplarVerdict.Cast();
                        return;
                    }
                }
            }

            //actions+=/zeal,if=holy_power<=4
            if (TalentZeal)
            {
                if (HolyPower <= 4)
                {
                    if (Zeal.SpellCanCastBlind && InMeleeRange)
                    {
                        Zeal.Cast();
                        return;
                    }
                }
            }


            //actions+=/crusader_strike,if=holy_power<=4
            if (!TalentZeal)
            {
                if (HolyPower <= 4)
                {
                    if (CrusaderStrike.SpellCanCast && InMeleeRange)
                    {
                        CrusaderStrike.Cast();
                        return;
                    }
                }
            }


            //actions+=/divine_storm,if=debuff.judgment.up&holy_power>=3&spell_targets.divine_storm>=2&(!talent.crusade.enabled|cooldown.crusade.remains>gcd*5)
            if (CanCastFinisher3hp)
            {
                if (JudgementDebuff.TargetDebuff && HolyPower >= 3 && TargetsDivineStorm >= 2 && (!TalentCrusade || Crusade.SpellCooldownRemaining > Gcd*5))
                {
                    if (DivineStorm.SpellCanCastBlind && InMeleeRange)
                    {
                        DivineStorm.Cast();
                        return;
                    }
                }
            }

            //actions+=/templars_verdict,if=debuff.judgment.up&holy_power>=3&(!talent.crusade.enabled|cooldown.crusade.remains>gcd*5)
            if (CanCastFinisher3hp)
            {
                if (JudgementDebuff.TargetDebuff && HolyPower >= 3 && (!TalentCrusade || Crusade.SpellCooldownRemaining > Gcd*5))
                {
                    if (TemplarVerdict.SpellCanCast && InMeleeRange)
                    {
                        TemplarVerdict.Cast();
                    }
                }
            }
        }

        public override void Initialize()
        {
            Log.DrawHorizontalLine();
            Log.Write("##################################", Color.Red);
            Log.Write("# Retribution Paladin (alpha) (by GennPen) #", Color.Red);
            Log.Write("##################################", Color.Red);
            Log.DrawHorizontalLine();
        }
    }

    public class ClassSpell
    {
        private readonly string _name;

        private readonly ClassCachedVariable<bool> _cachePlayerBuff = new ClassCachedVariable<bool>();

        private readonly ClassCachedVariable<int> _cachePlayerBuffRemaining = new ClassCachedVariable<int>();

        private readonly ClassCachedVariable<int> _cachePlayerBuffStacks = new ClassCachedVariable<int>();

        private readonly ClassCachedVariable<bool> _cachePlayerDebuff = new ClassCachedVariable<bool>();

        private readonly ClassCachedVariable<bool> _cacheSpellCanCast = new ClassCachedVariable<bool>();

        private readonly ClassCachedVariable<bool> _cacheSpellCanCastBlind = new ClassCachedVariable<bool>();

        private ClassCachedVariable<int> _cacheSpellCharges = new ClassCachedVariable<int>();

        private readonly ClassCachedVariable<float> _cacheSpellChargesFractional = new ClassCachedVariable<float>();

        private readonly ClassCachedVariable<int> _cacheSpellCooldownRemaining = new ClassCachedVariable<int>();

        private readonly ClassCachedVariable<bool> _cacheSpellInRange = new ClassCachedVariable<bool>();

        private readonly ClassCachedVariable<bool> _cacheTargetDebuff = new ClassCachedVariable<bool>();

        private readonly ClassCachedVariable<int> _cacheTargetDebuffRemaining = new ClassCachedVariable<int>();

        private readonly ClassCachedVariable<int> _cacheTargetDebuffStacks = new ClassCachedVariable<int>();
        private int _endPlayerBuffRemaining;
        private int _endSpellCooldownRemaining;
        private int _endTargetDebuffRemaining;

        private int _lastPlayerBuffRemaining;


        private int _lastSpellCooldownRemaining;

        private int _lastTargetDebuffRemaining;

        public ClassSpell(string name)
        {
            _name = name;
        }

        public bool SpellCanCastBlind
        {
            get
            {
                if (_cacheSpellCanCastBlind.TimeStamp != RetributionPaladinGP.PulseTick)
                {
                    _cacheSpellCanCastBlind.TimeStamp = RetributionPaladinGP.PulseTick;
                    _cacheSpellCanCastBlind.Variable = WoW.CanCast(this._name);
                }
                return _cacheSpellCanCastBlind.Variable;
            }
        }

        public bool SpellCanCast
        {
            get
            {
                if (_cacheSpellCanCast.TimeStamp != RetributionPaladinGP.PulseTick)
                {
                    _cacheSpellCanCast.TimeStamp = RetributionPaladinGP.PulseTick;
                    _cacheSpellCanCast.Variable = WoW.CanCast(this._name, true, true, true, true, true);
                }
                return _cacheSpellCanCast.Variable;
            }
        }

        public bool SpellInRange
        {
            get
            {
                if (_cacheSpellInRange.TimeStamp != RetributionPaladinGP.PulseTick)
                {
                    _cacheSpellInRange.TimeStamp = RetributionPaladinGP.PulseTick;
                    _cacheSpellInRange.Variable = WoW.IsSpellInRange(this._name);
                }
                return _cacheSpellInRange.Variable;
            }
        }

        public int SpellCharges => WoW.PlayerSpellCharges(this._name);

        public float SpellChargesFractional
        {
            get
            {
                if (_cacheSpellChargesFractional.TimeStamp != RetributionPaladinGP.PulseTick)
                {
                    _cacheSpellChargesFractional.TimeStamp = RetributionPaladinGP.PulseTick;
                    _cacheSpellChargesFractional.Variable = WoW.PlayerSpellCharges(this._name);
                }
                return _cacheSpellChargesFractional.Variable;
            }
        }

        public int SpellCooldownRemaining
        {
            get
            {
                if (this._name == "Crusade") return 100000; // temp disable Crusade burst

                if (_cacheSpellCooldownRemaining.TimeStamp != RetributionPaladinGP.PulseTick)
                {
                    _cacheSpellCooldownRemaining.TimeStamp = RetributionPaladinGP.PulseTick;

                    if (_endSpellCooldownRemaining < Environment.TickCount)
                    {
                        _cacheSpellCooldownRemaining.Variable = WoW.SpellCooldownTimeRemaining(this._name)*1000;
                    }
                    else
                    {
                        _cacheSpellCooldownRemaining.Variable = _endSpellCooldownRemaining - Environment.TickCount;
                    }
                }

                return _cacheSpellCooldownRemaining.Variable;
            }
        }

        public bool TargetDebuff
        {
            get
            {
                if (this._name == "Judgement Debuff" && !WoW.TargetHasDebuff("Judgement Debuff") && WoW.SpellCooldownTimeRemaining("Judgement") >= 5) return true;

                if (_cacheTargetDebuff.TimeStamp != RetributionPaladinGP.PulseTick)
                {
                    _cacheTargetDebuff.TimeStamp = RetributionPaladinGP.PulseTick;
                    _cacheTargetDebuff.Variable = WoW.TargetHasDebuff(this._name);
                }
                return _cacheTargetDebuff.Variable;
            }
        }

        public int TargetDebuffRemaining
        {
            get
            {
                if (_cacheTargetDebuffRemaining.TimeStamp != RetributionPaladinGP.PulseTick)
                {
                    _cacheTargetDebuffRemaining.TimeStamp = RetributionPaladinGP.PulseTick;

                    if (_endTargetDebuffRemaining < Environment.TickCount)
                    {
                        _cacheTargetDebuffRemaining.Variable = WoW.TargetDebuffTimeRemaining(this._name)*1000;
                    }
                    else
                    {
                        _cacheTargetDebuffRemaining.Variable = _endTargetDebuffRemaining - Environment.TickCount;
                    }
                }
                return _cacheTargetDebuffRemaining.Variable;
            }
        }

        public int TargetDebuffStacks
        {
            get
            {
                if (_cacheTargetDebuffStacks.TimeStamp != RetributionPaladinGP.PulseTick)
                {
                    _cacheTargetDebuffStacks.TimeStamp = RetributionPaladinGP.PulseTick;
                    _cacheTargetDebuffStacks.Variable = WoW.TargetDebuffStacks(this._name);
                }
                return _cacheTargetDebuffStacks.Variable;
            }
        }

        public bool PlayerDebuff
        {
            get
            {
                if (_cachePlayerDebuff.TimeStamp != RetributionPaladinGP.PulseTick)
                {
                    _cachePlayerDebuff.TimeStamp = RetributionPaladinGP.PulseTick;
                    _cachePlayerDebuff.Variable = WoW.PlayerHasDebuff(this._name);
                }
                return _cachePlayerDebuff.Variable;
            }
        }

        public bool PlayerBuff
        {
            get
            {
                if (_cachePlayerBuff.TimeStamp != RetributionPaladinGP.PulseTick)
                {
                    _cachePlayerBuff.TimeStamp = RetributionPaladinGP.PulseTick;
                    _cachePlayerBuff.Variable = WoW.PlayerHasBuff(this._name);
                }
                return _cachePlayerBuff.Variable;
            }
        }

        public int PlayerBuffStacks
        {
            get
            {
                if (_cachePlayerBuffStacks.TimeStamp != RetributionPaladinGP.PulseTick)
                {
                    _cachePlayerBuffStacks.TimeStamp = RetributionPaladinGP.PulseTick;
                    _cachePlayerBuffStacks.Variable = WoW.PlayerBuffStacks(this._name);
                }
                return _cachePlayerBuffStacks.Variable;
            }
        }

        public int PlayerBuffRemaining
        {
            get
            {
                if (_cachePlayerBuffRemaining.TimeStamp != RetributionPaladinGP.PulseTick)
                {
                    _cachePlayerBuffRemaining.TimeStamp = RetributionPaladinGP.PulseTick;

                    if (_endPlayerBuffRemaining < Environment.TickCount)
                    {
                        _cachePlayerBuffRemaining.Variable = WoW.PlayerBuffTimeRemaining(this._name)*1000;
                    }
                    else
                    {
                        _cachePlayerBuffRemaining.Variable = _endPlayerBuffRemaining - Environment.TickCount;
                    }
                }

                return _cachePlayerBuffRemaining.Variable;
            }
        }

        public void Cast()
        {
            WoW.CastSpell(this._name);
        }

        public void PulseSpellCooldownRemaining()
        {
            var remaining = WoW.SpellCooldownTimeRemaining(this._name);

            if (_lastSpellCooldownRemaining - remaining == 1)
            {
                _endSpellCooldownRemaining = Environment.TickCount + 1000*_lastSpellCooldownRemaining;
            }
            _lastSpellCooldownRemaining = remaining;
        }

        public void PulseTargetDebuffRemaining()
        {
            var remaining = WoW.TargetDebuffTimeRemaining(this._name);

            if (_lastTargetDebuffRemaining - remaining == 1)
            {
                _endTargetDebuffRemaining = Environment.TickCount + 1000*_lastTargetDebuffRemaining;
            }
            _lastTargetDebuffRemaining = remaining;
        }

        public void PulsePlayerBuffRemaining()
        {
            var remaining = WoW.PlayerBuffTimeRemaining(this._name);

            if (_lastPlayerBuffRemaining - remaining == 1)
            {
                _endPlayerBuffRemaining = Environment.TickCount + 1000*_lastPlayerBuffRemaining;
            }
            _lastPlayerBuffRemaining = remaining;
        }
    }

    public class ClassCachedVariable<Type>
    {
        public int TimeStamp;
        public Type Variable;
    }
}

/*
[AddonDetails.db]
AddonAuthor=GennPen
AddonName=NotePM
WoWVersion=Legion - 70300
[SpellBook.db]
Spell,217020,Zeal,D1
Spell,215661,Justicars Vengeance,D5
Spell,184662,Shield of Vengeance,F5
Spell,853,Hammer of Justice,F
Spell,213757,Execution Sentence,D8
Spell,633,Lay on Hands,F12
Spell,205273,Wake of Ashes,D6
Spell,53385,Divine Storm,D7
Spell,184575,Blade of Justice,D2
Spell,198034,Divine Hammer,D2
Spell,35395,Crusader Strike,D1
Spell,85256,Templar Verdict,D4
Spell,20271,Judgement,D3
Spell,224668,Crusade,F1
Spell,31884,Avenging Wrath,F1
Spell,19750,Flash of Light,E
Spell,210220,Holy Wrath,D9
Spell,205228,Consecration,D0
Aura,197277,Judgement Debuff
Aura,223819,Divine Purpose
Aura,209785,The Fires of Justice
Aura,2825,Bloodlust
Aura,80353,Time Warp
Aura,160452,Netherwinds
Aura,230935,Drums of War
Aura,127271,Mount
Aura,25771,Forbearance
Aura,207635,Whisper of the Nathrezim
Aura,231985,Crusade
*/