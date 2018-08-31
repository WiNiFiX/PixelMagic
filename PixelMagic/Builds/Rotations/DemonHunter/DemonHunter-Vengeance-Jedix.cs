// winifix@gmail.com
// ReSharper disable UnusedMember.Global
// Todo: right range of Throw Glaive spell 204157


using System;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using PixelMagic.Helpers;

namespace PixelMagic.Rotation
{
    public class DemonHunterVengJE : CombatRoutine
    {
	private NumericUpDown nudMetaPercentValue;
		
	private CheckBox BloodelfBox;
		
        public override string Name { get { return "PixelMagic Vengeance by Jedix"; } }

        public override string Class { get { return "Demon Hunter"; } }

        public override Form SettingsForm { get; set; }
		
	public static bool Bloodelf
        {
            get
            {
                var Bloodelf = ConfigFile.ReadValue("DemonHunter", "Bloodelf").Trim();

                return Bloodelf != "" && Convert.ToBoolean(Bloodelf);
            }
            set { ConfigFile.WriteValue("DemonHunter", "Bloodelf", value.ToString()); }
        }

        public override void Initialize()
        {
			if (ConfigFile.ReadValue("DemonHunter", "Metamorphosis Usage Percent") == "")
            {
                ConfigFile.WriteValue("Demonhunter", "Metamorphosis Usage Percent", "20");
            }
            Log.DrawHorizontalLine();
            //Log.Write("Welcome to PixelMagic Vengeance by Jedix");
            Log.WritePixelMagic("Welcome to PixelMagic Vengeance by Jedix", Color.Green);
            Log.Write("Suggested build: 3332333", Color.Green);
            Log.Write("IMPORTANT!", Color.Red);
            Log.Write("You can use any talent instead of Fracture, just set bot to aoe - he will never use a Fracture", Color.Black);
            Log.Write("Also need to do a macro in wow (or take a talent for non-target sigils) to make Sigil of Flame(Silience) work, macro:",Color.Black);
			Log.Write("#showtooltip",Color.Black);
			Log.Write("/cast [mod:shift,target=player][nomod,target=player][mod:ctrl,target=player] Sigil of Flame",Color.Black);
			Log.Write("/cast [mod:alt] Sigil of Flame",Color.Black);
            Log.Write("Put it in position of Sigil of Flame(Silience), when u need to cast it ranged just hold ALT and click", Color.Black);
			
			SettingsForm = new Form {Text = "Settings", StartPosition = FormStartPosition.CenterScreen, Width = 800, Height = 490, ShowIcon = false};
            var lblMetaPercent = new Label {Text = "Metamorphosis Health %", Left = 12, Top = 150};
            SettingsForm.Controls.Add(lblMetaPercent);
			
			var BloodelfText = new Label 
            {Text = "Im a blood elf", Size = new Size(81, 13), Left = 12, Top = 129};
            SettingsForm.Controls.Add(BloodelfText); 
			
			BloodelfBox = new CheckBox {Checked = Bloodelf, TabIndex = 4, Size = new Size(15, 14), Left = 115, Top = 129};
            SettingsForm.Controls.Add(BloodelfBox);
			
			BloodelfBox.Checked = Bloodelf;

            nudMetaPercentValue = new NumericUpDown {Minimum = 0, Maximum = 100, Value = ConfigFile.ReadValue<decimal>("Demonhunter", "Metamorphosis Usage Percent"), Left = 108, Top = 145};
            SettingsForm.Controls.Add(nudMetaPercentValue);	

            var cmdSave = new Button {Text = "Save", Width = 65, Height = 25, Left = 662, Top = 408, Size = new Size(108, 31)};
            cmdSave.Click += CmdSave_Click;
			BloodelfBox.CheckedChanged += Bloodelf_Click;

            SettingsForm.Controls.Add(cmdSave);
            nudMetaPercentValue.BringToFront();
			BloodelfText.BringToFront();
        }
		
	private void CmdSave_Click(object sender, EventArgs e)
        {
            ConfigFile.WriteValue("Demonhunter", "Metamorphosis Usage Percent", nudMetaPercentValue.Value.ToString());
        }
		
	private void Bloodelf_Click(object sender, EventArgs e)
        {
            Bloodelf = BloodelfBox.Checked;
        }

        public override void Stop()
        {
        }

        public override void Pulse()
        {
            if (combatRoutine.Type == RotationType.SingleTarget) // Do Single Target Stuff here
            {
                if (WoW.HasTarget && WoW.TargetIsEnemy && !WoW.PlayerIsChanneling && WoW.IsInCombat) //Pull need to do by yourself - to prevent a mess
                {
                    if (WoW.CanCast("Metamorphosis") && WoW.HealthPercent <= ConfigFile.ReadValue<int>("Demonhunter", "Metamorphosis Usage Percent"))
                    {
                        //Log.Write("Metamorphosis");
                        Log.Write("Health low < set % using CDs...", Color.Red);
                        WoW.CastSpell("Metamorphosis"); // Off the GCD no return needed
                    }
						
					if (WoW.TargetIsCastingAndSpellIsInterruptible) //interupt every spell with all we got
                    {
						if (WoW.CanCast("Consume Magic") && WoW.TargetPercentCast >= 60 && WoW.IsSpellInRange("Consume Magic"))
						{	
							WoW.CastSpell("Consume Magic");
							return;
						}
						
						if (Bloodelf && WoW.CanCast("Arcane Torrent") && WoW.IsSpellOnCooldown("Consume Magic") && WoW.TargetPercentCast >= 60 && WoW.IsSpellInRange("Soul Carver"))
						{	
							WoW.CastSpell("Arcane Torrent");
							return;
						}
						
						if (WoW.CanCast("Sigil of Silence") && WoW.IsSpellOnCooldown("Consume Magic") && (WoW.IsSpellOnCooldown("Arcane Torrent") || !Bloodelf) && WoW.IsSpellInRange("Soul Carver"))
						{	
							WoW.CastSpell("Sigil of Silence");
							return;
						}
                        
					}

                    if (WoW.CanCast("Fiery Brand") && WoW.IsSpellInRange("Fiery Brand") && !WoW.TargetHasDebuff("Fiery Demise"))
                    {
                        WoW.CastSpell("Fiery Brand");
                    }

                    if (WoW.CanCast("Soul Carver") && WoW.IsSpellInRange("Soul Carver") && WoW.SpellCooldownTimeRemaining("Fiery Brand") >= 10)
                    {
                        WoW.CastSpell("Soul Carver");
                        return;
                    }

                    if (WoW.CanCast("Spirit Bomb") && WoW.IsSpellInRange("Spirit Bomb") && WoW.PlayerHasBuff("Soul Fragments") && !WoW.TargetHasDebuff("Frailty"))
                    {
                        WoW.CastSpell("Spirit Bomb");
                        return;
                    }

                    if (WoW.CanCast("Sigil of Flame") && WoW.IsSpellInRange("Soul Carver") && WoW.TargetHasDebuff("Fiery Demise") && WoW.TargetDebuffTimeRemaining("Fiery Demise") >= 7)
                    {
                        WoW.CastSpell("Sigil of Flame");
                        return;
                    }

                    if (WoW.CanCast("Fel Eruption") && WoW.IsSpellInRange("Fel Eruption") && WoW.Pain >= 10 && WoW.TargetHasDebuff("Fiery Demise"))
                    {
                        WoW.CastSpell("Fel Eruption");
                        return;
                    }

                    if (WoW.CanCast("Soul Barrier") && WoW.Pain >= 10 && WoW.PlayerHasBuff("Soul Fragments") && WoW.PlayerBuffStacks("Soul Fragments") >= 4)
                    {
                        WoW.CastSpell("Soul Barrier");
                        return;
                    }

                    if (WoW.CanCast("Demon Spikes") && !WoW.PlayerHasBuff("Demon Spikes") && (WoW.Pain >= 92 || WoW.HealthPercent < 90 && WoW.Pain >= 20)) // to not waste cd and pain
                    {
                        WoW.CastSpell("Demon Spikes");
                    }
					
					if (WoW.IsSpellInRange("Soul Carver"))
					{
							if (WoW.CanCast("Soul Cleave") && ((WoW.Pain >= 30 && WoW.HealthPercent < 25) || (WoW.Pain >= 80 && WoW.HealthPercent < 50 && !WoW.CanCast("Demon Spikes"))
								|| (WoW.Pain >= 60 && WoW.HealthPercent < 50 && WoW.PlayerHasBuff("Demon Spikes"))))
								{
									WoW.CastSpell("Soul Cleave");
									return;
								}
								
							if (WoW.CanCast("Fracture") && ((WoW.Pain >= 20 && WoW.HealthPercent >= 50 && WoW.PlayerHasBuff("Demon Spikes"))
								|| (WoW.Pain >= 80 && WoW.HealthPercent >= 50 && !WoW.CanCast("Demon Spikes"))))
								{
									WoW.CastSpell("Fracture");
									return;
								}

						if (WoW.CanCast("Immolation Aura"))
						{
							WoW.CastSpell("Immolation Aura");
							return;
						}

						if (WoW.CanCast("Sigil of Flame"))
						{
							WoW.CastSpell("Sigil of Flame");
							return;
						}
					}

                    if (WoW.CanCast("Fel Eruption") && WoW.IsSpellInRange("Fel Eruption") && WoW.Pain >= 10)
                    {
                        WoW.CastSpell("Fel Eruption");
                        return;
                    }

                    if (WoW.CanCast("Sever") && WoW.IsSpellInRange("Soul Carver") && WoW.PlayerHasBuff("Metamorphosis")) // Pain Generator
                    {
                        WoW.CastSpell("Sever");
                        return;
                    }

                    if (WoW.CanCast("Shear") && WoW.IsSpellInRange("Soul Carver") && !WoW.PlayerHasBuff("Metamorphosis")) // Pain Generator
                    {
                        WoW.CastSpell("Shear");
                        return;
                    }
					
					if (WoW.CanCast("Throw Glaive") && !WoW.IsSpellInRange("Soul Carver") && WoW.IsSpellInRange("Throw Glaive")) //Need to implement range 30y for Throw Glaive spell 204157
                    {
                        WoW.CastSpell("Throw Glaive");
                        return;
                    }
                }
            }
            if (combatRoutine.Type == RotationType.AOE || combatRoutine.Type == RotationType.SingleTargetCleave)
            {
				if (WoW.HasTarget && WoW.TargetIsEnemy && !WoW.PlayerIsChanneling && WoW.IsInCombat) //Pull need to do by yourself - to prevent a mess
                {
                    if (WoW.CanCast("Metamorphosis") && WoW.HealthPercent <= ConfigFile.ReadValue<int>("Demonhunter", "Metamorphosis Usage Percent"))
                    {
                        //Log.Write("Metamorphosis");
                        Log.Write("Health low < set % using CDs...", Color.Red);
                        WoW.CastSpell("Metamorphosis"); // Off the GCD no return needed
                    }
						
					if (WoW.TargetIsCastingAndSpellIsInterruptible) //interupt every spell with all we got
                    {
						if (WoW.CanCast("Consume Magic") && WoW.TargetPercentCast >= 60 && WoW.IsSpellInRange("Consume Magic"))
						{	
							WoW.CastSpell("Consume Magic");
							return;
						}
						
						if (Bloodelf && WoW.CanCast("Arcane Torrent") && WoW.IsSpellOnCooldown("Consume Magic") && WoW.TargetPercentCast >= 60 && WoW.IsSpellInRange("Soul Carver"))
						{	
							WoW.CastSpell("Arcane Torrent");
							return;
						}
						
						if (WoW.CanCast("Sigil of Silence") && WoW.IsSpellOnCooldown("Consume Magic") && (WoW.IsSpellOnCooldown("Arcane Torrent") || !Bloodelf) && WoW.IsSpellInRange("Soul Carver"))
						{	
							WoW.CastSpell("Sigil of Silence");
							return;
						}
                        
					}

                    if (WoW.CanCast("Fiery Brand") && WoW.IsSpellInRange("Fiery Brand") && !WoW.TargetHasDebuff("Fiery Demise"))
                    {
                        WoW.CastSpell("Fiery Brand");
                    }

                    if (WoW.CanCast("Soul Carver") && WoW.IsSpellInRange("Soul Carver") && WoW.SpellCooldownTimeRemaining("Fiery Brand") >= 10)
                    {
                        WoW.CastSpell("Soul Carver");
                        return;
                    }

                    if (WoW.CanCast("Spirit Bomb") && WoW.IsSpellInRange("Spirit Bomb") && WoW.PlayerHasBuff("Soul Fragments") && !WoW.TargetHasDebuff("Frailty"))
                    {
                        WoW.CastSpell("Spirit Bomb");
                        return;
                    }

                    if (WoW.CanCast("Sigil of Flame") && WoW.IsSpellInRange("Soul Carver") && WoW.TargetHasDebuff("Fiery Demise") && WoW.TargetDebuffTimeRemaining("Fiery Demise") >= 7)
                    {
                        WoW.CastSpell("Sigil of Flame");
                        return;
                    }

                    if (WoW.CanCast("Fel Eruption") && WoW.IsSpellInRange("Fel Eruption") && WoW.Pain >= 10 && WoW.TargetHasDebuff("Fiery Demise"))
                    {
                        WoW.CastSpell("Fel Eruption");
                        return;
                    }

                    if (WoW.CanCast("Soul Barrier") && WoW.Pain >= 10 && WoW.PlayerHasBuff("Soul Fragments") && WoW.PlayerBuffStacks("Soul Fragments") >= 4)
                    {
                        WoW.CastSpell("Soul Barrier");
                        return;
                    }

                    if (WoW.CanCast("Demon Spikes") && !WoW.PlayerHasBuff("Demon Spikes") && (WoW.Pain >= 92 || WoW.HealthPercent < 90 && WoW.Pain >= 20)) // to not waste cd and pain
                    {
                        WoW.CastSpell("Demon Spikes");
                    }
					
					if (WoW.IsSpellInRange("Soul Carver"))
					{
						if (WoW.CanCast("Soul Cleave") && ((WoW.Pain >= 30 && WoW.HealthPercent < 25) || (WoW.Pain >= 80 && !WoW.CanCast("Demon Spikes"))
							|| (WoW.Pain >= 60 && WoW.PlayerHasBuff("Demon Spikes"))))
						{
							WoW.CastSpell("Soul Cleave");
							return;
						}

						if (WoW.CanCast("Immolation Aura"))
						{
							WoW.CastSpell("Immolation Aura");
							return;
						}

						if (WoW.CanCast("Sigil of Flame"))
						{
							WoW.CastSpell("Sigil of Flame");
							return;
						}
					}

                    if (WoW.CanCast("Fel Eruption") && WoW.IsSpellInRange("Fel Eruption") && WoW.Pain >= 10)
                    {
                        WoW.CastSpell("Fel Eruption");
                        return;
                    }

                    if (WoW.CanCast("Sever") && WoW.IsSpellInRange("Soul Carver") && WoW.PlayerHasBuff("Metamorphosis")) // Pain Generator
                    {
                        WoW.CastSpell("Sever");
                        return;
                    }

                    if (WoW.CanCast("Shear") && WoW.IsSpellInRange("Soul Carver") && !WoW.PlayerHasBuff("Metamorphosis")) // Pain Generator
                    {
                        WoW.CastSpell("Shear");
                        return;
                    }
					
					if (WoW.CanCast("Throw Glaive") && !WoW.IsSpellInRange("Soul Carver") && WoW.IsSpellInRange("Throw Glaive")) //Need to implement range 30y for Throw Glaive spell 204157
                    {
                        WoW.CastSpell("Throw Glaive");
                        return;
                    }
				}
            }
        }
    }
}

/*
[AddonDetails.db]
AddonAuthor=Jedix
AddonName=Pawn
WoWVersion=Legion - 70300
[SpellBook.db]
Spell,203782,Shear,G
Spell,235964,Sever,G
Spell,228477,Soul Cleave,E
Spell,207407,Soul Carver,NumPad5
Spell,178740,Immolation Aura,D1
Spell,204596,Sigil of Flame,NumPad4
Spell,204157,Throw Glaive,D3
Spell,207682,Sigil of Silence,NumPad1
Spell,202719,Arcane Torrent,D8
Spell,187827,Metamorphosis,NumPad2
Spell,204021,Fiery Brand,NumPad3
Spell,218679,Spirit Bomb,NumPad6
Spell,183752,Consume Magic,NumPad9
Spell,203720,Demon Spikes,Q
Spell,189110,Infernal Strike,MButton
Spell,209795,Fracture,D2
Spell,227225,Soul Barrier,D6
Spell,211881,Fel Eruption,D5
Aura,203819,Demon Spikes
Aura,212818,Fiery Demise
Aura,187827,Metamorphosis
Aura,224509,Frailty
Aura,203981,Soul Fragments
*/
