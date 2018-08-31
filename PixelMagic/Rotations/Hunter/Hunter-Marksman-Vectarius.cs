// winifix@gmail.com
// ReSharper disable UnusedMember.Global

using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using PixelMagic.Helpers;

namespace PixelMagic.Rotation
{
    public class MMHunterVectarius : CombatRoutine
    {
		private int AimedShotCastTime
        {
            get
            {
                if (200 / (1 + (WoW.HastePercent / 100)) > 75)
                {
                    return 200 / (1 + (WoW.HastePercent / 100));
                }
                else
                {
                    return 75;
                }
            }
        }
	// DEF cds
		private CheckBox ExhilarationBox;
		private CheckBox FeignDeathBox;
		private CheckBox AspectoftheTurtleBox;		
		
		private CheckBox CounterShotBox;		
		//dps cds
		private CheckBox KilJaedenBox;
		

		
        private static bool CounterShot
        {
            get
            {
                var CounterShot = ConfigFile.ReadValue("HunterBeastmastery", "CounterShot").Trim();

                return CounterShot != "" && Convert.ToBoolean(CounterShot);
            }
            set { ConfigFile.WriteValue("HunterBeastmastery", "CounterShot", value.ToString()); }
        }	
        private static bool Exhilaration
        {
            get
            {
                var Exhilaration = ConfigFile.ReadValue("HunterBeastmastery", "Exhilaration").Trim();

                return Exhilaration != "" && Convert.ToBoolean(Exhilaration);
            }
            set { ConfigFile.WriteValue("HunterBeastmastery", "Exhilaration", value.ToString()); }
        }	
		
        private static bool FeignDeath
        {
            get
            {
                var FeignDeath = ConfigFile.ReadValue("HunterBeastmastery", "FeignDeath").Trim();

                return FeignDeath != "" && Convert.ToBoolean(FeignDeath);
            }
            set { ConfigFile.WriteValue("HunterBeastmastery", "FeignDeath", value.ToString()); }
        }	

        private static bool AspectoftheTurtle
        {
            get
            {
                var AspectoftheTurtle = ConfigFile.ReadValue("HunterBeastmastery", "AspectoftheTurtle").Trim();

                return AspectoftheTurtle != "" && Convert.ToBoolean(AspectoftheTurtle);
            }
            set { ConfigFile.WriteValue("HunterBeastmastery", "AspectoftheTurtle", value.ToString()); }
        }				
		
        private static bool KilJaeden
        {
            get
            {
                var KilJaeden = ConfigFile.ReadValue("HunterBeastmastery", "KilJaeden").Trim();

                return KilJaeden != "" && Convert.ToBoolean(KilJaeden);
            }
            set { ConfigFile.WriteValue("HunterBeastmastery", "KilJaeden", value.ToString()); }
        }		
		

        
      public override string Name
        {
            get { return "Hunter Marksman"; }
        }

        
		 public override string Class
        {
            get { return "Hunter"; }
        }

        public override Form SettingsForm { get; set; }
		
		
        public override void Initialize()
        {
            
            SettingsForm = new Form
            {
                Text = "Marksman Hunter",
                StartPosition = FormStartPosition.CenterScreen,
                Width = 1000,
                Height = 600,
                ShowIcon = false
            };

            
			
			var lblTitle = new Label
            {
                Text =
                    "MM Hunter by Vectarius",
                Size = new Size(270, 13),
                Left = 61,
                Top = 1
	       };
			lblTitle.ForeColor = Color.Black;
			Font myFont = new Font(lblTitle.Font,FontStyle.Bold|FontStyle.Underline);
			lblTitle.Font = myFont;
            SettingsForm.Controls.Add(lblTitle);
			


						var lblTextBox3 = new Label
            {
                Text =
                    "Cooldowns",
                Size = new Size(200, 17),
                Left = 70,
                Top = 250
            };
			lblTextBox3.ForeColor = Color.Black;
			 SettingsForm.Controls.Add(lblTextBox3);			
			
			var lblKilJaedenBox = new Label
            {
                Text =
                    "Kil'Jaeden's burning wish",
                Size = new Size(270, 15),
                Left = 100,
                Top = 275
            };
			
			lblKilJaedenBox.ForeColor = Color.Black;
            SettingsForm.Controls.Add(lblKilJaedenBox);			
 
			var lblCounterShotBox = new Label
            {
                Text =
                    "Counter Shot",
                Size = new Size(270, 15),
                Left = 100,
                Top = 300
            };
			
			lblCounterShotBox.ForeColor = Color.Black;
            SettingsForm.Controls.Add(lblCounterShotBox);	

			var lblExhilarationBox = new Label
            {
                Text =
                    "Exhilaration",
                Size = new Size(270, 15),
                Left = 100,
                Top = 325
            };
			
			lblExhilarationBox.ForeColor = Color.Black;
            SettingsForm.Controls.Add(lblExhilarationBox);	

			var lblAspectoftheTurtleBox = new Label
            {
                Text =
                    "Aspect of the Turtle",
                Size = new Size(270, 15),
                Left = 100,
                Top = 375
            };
			
			lblAspectoftheTurtleBox.ForeColor = Color.Black;
            SettingsForm.Controls.Add(lblAspectoftheTurtleBox);	

			var lblFeignDeathBox = new Label
            {
                Text =
                    "Feign Death",
                Size = new Size(270, 15),
                Left = 100,
                Top = 350
            };
			
			lblFeignDeathBox.ForeColor = Color.Black;
            SettingsForm.Controls.Add(lblFeignDeathBox);	
 

			
			var cmdSave = new Button {Text = "Save", Width = 65, Height = 25, Left = 5, Top = 425, Size = new Size(120, 31)};
			
			var cmdReadme = new Button {Text = "Macros! Use Them", Width = 65, Height = 25, Left = 125, Top = 425, Size = new Size(120, 31)};
			// Checkboxes
			//dps cooldowns
            CounterShotBox = new CheckBox {Checked = CounterShot, TabIndex = 8, Size = new Size(13, 13), Left = 70, Top = 300};		
            SettingsForm.Controls.Add(CounterShotBox);
			ExhilarationBox = new CheckBox {Checked = Exhilaration, TabIndex = 8, Size = new Size(13, 13), Left = 70, Top = 325};			
            SettingsForm.Controls.Add(ExhilarationBox);
			FeignDeathBox = new CheckBox {Checked = FeignDeath, TabIndex = 8, Size = new Size(13, 13), Left = 70, Top = 350};
            SettingsForm.Controls.Add(FeignDeathBox);
			
			AspectoftheTurtleBox = new CheckBox {Checked = AspectoftheTurtle, TabIndex = 8, Size = new Size(13, 13), Left = 70, Top = 375};			
			            SettingsForm.Controls.Add(AspectoftheTurtleBox);
            KilJaedenBox = new CheckBox {Checked = KilJaeden, TabIndex = 8, Size = new Size(13, 13), Left = 70, Top = 275};
            SettingsForm.Controls.Add(KilJaedenBox);			

			//dps cooldowns
			CounterShotBox.Checked = CounterShot;	
			ExhilarationBox.Checked = Exhilaration;	
			FeignDeathBox.Checked = FeignDeath;	
			AspectoftheTurtleBox.Checked = AspectoftheTurtle;	

			KilJaedenBox.Checked = KilJaeden;

			
			//cmdSave
            KilJaedenBox.CheckedChanged += KilJaeden_Click;            
            ExhilarationBox.CheckedChanged += Exhilaration_Click; 
            CounterShotBox.CheckedChanged += CounterShot_Click;
            FeignDeathBox.CheckedChanged += FeignDeath_Click;
            AspectoftheTurtleBox.CheckedChanged += AspectoftheTurtle_Click;			
			
			cmdSave.Click += CmdSave_Click;
			cmdReadme.Click += CmdReadme_Click;
           
			
			SettingsForm.Controls.Add(cmdSave);
			SettingsForm.Controls.Add(cmdReadme);

			lblTextBox3.BringToFront();				
			lblTitle.BringToFront();
			
            KilJaedenBox.BringToFront();		
            CounterShotBox.BringToFront();	
            ExhilarationBox.BringToFront();
            FeignDeathBox.BringToFront();
            AspectoftheTurtleBox.BringToFront();			
			
			

			
			
		}
			
			private void CmdSave_Click(object sender, EventArgs e)
        {
            KilJaeden = KilJaedenBox.Checked;			
            CounterShot = CounterShotBox.Checked;	
            Exhilaration = ExhilarationBox.Checked;
            FeignDeath = FeignDeathBox.Checked;
            AspectoftheTurtle = AspectoftheTurtleBox.Checked;			
			
			

			
			
			
			
            MessageBox.Show("Settings saved.", "PixelMagic", MessageBoxButtons.OK, MessageBoxIcon.Information);
            SettingsForm.Close();
        }
		private void CmdReadme_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                " no macros <.<",
                "PixelMagic", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
			// Checkboxes
			//dpscooldown
        private void KilJaeden_Click(object sender, EventArgs e)
        {
            KilJaeden = KilJaedenBox.Checked;
        }        
		
		private void CounterShot_Click(object sender, EventArgs e)
        {
            CounterShot = CounterShotBox.Checked;
        }			
			
        private void Exhilaration_Click(object sender, EventArgs e)
        {
            Exhilaration = ExhilarationBox.Checked;
        }
        private void FeignDeath_Click(object sender, EventArgs e)
        {
            FeignDeath = FeignDeathBox.Checked;
        }
        private void AspectoftheTurtle_Click(object sender, EventArgs e)
        {
            AspectoftheTurtle = AspectoftheTurtleBox.Checked;
        }
		

	
		
		
        public override void Stop()
        {
			
			
        }

        public override void Pulse()
        {
            if (combatRoutine.Type == RotationType.SingleTarget || combatRoutine.Type == RotationType.SingleTargetCleave)  // Do Single Target Stuff here
            {
	
                   /* if (WoW.CanCast("Death") && WoW.HealthPercent < 40 && Death && !WoW.IsSpellOnCooldown("Death") && WoW.HealthPercent != 0)
                    {
                        WoW.CastSpell("Death");
                        return;
                    }
                    if (WoW.CanCast("Exhil") && WoW.HealthPercent < 30 && Exhil && !WoW.IsSpellOnCooldown("Exhil") && WoW.HealthPercent != 0)
                    {
                        WoW.CastSpell("Exhil");
                        return;
                    }	
					if (WoW.CanCast("Turtle") && WoW.HealthPercent < 20 && Turtle && !WoW.IsSpellOnCooldown("Turtle") && WoW.HealthPercent != 0)
                    {
                        WoW.CastSpell("Turtle");
                        return;
                    }
					/*if (WoW.CanCast("Ancient Healing Potion") && WoW.HealthPercent < 20 && !WoW.IsSpellOnCooldown("Ancient Healing Potion") && WoW.HealthPercent != 0)
						{
							WoW.CastSpell("Ancient Healing Potion");
							return;
						}
					/*if (WoW.CanCast("Silkweave Bandage") && WoW.HealthPercent < 40 && WoW.PlayerHasBuff("Turtle") && !WoW.IsMoving && !WoW.PlayerHasDebuff("Bandaged") && WoW.HealthPercent != 0)
						{
							WoW.CastSpell("Silkweave Bandage");
							return;
						}*/
											
				if (WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsInCombat)
                {	
					if (WoW.CanCast("Counter Shot") 
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting
						&& WoW.TargetIsCasting
						&& WoW.TargetIsCastingAndSpellIsInterruptible
						&& WoW.TargetPercentCast >=24
						&& CounterShot)
                    {
                        WoW.CastSpell("Counter Shot");
                        return;
                    }				
					if (WoW.CanCast("Volley") 
						&& !WoW.PlayerHasBuff("Volley")
						&& WoW.Talent(6) == 3)
                    {
                        WoW.CastSpell("Volley");
                        return;
                    }	
					if (KilJaeden && !WoW.ItemOnCooldown("Kil'jaeden's Burning Wish"))  
                    {
                        WoW.CastSpell("Kil'jaeden's Burning Wish");
                        return;
                    }							
					if (WoW.CanCast("Arcane Torrent") 
						
						&& !WoW.IsSpellOnCooldown ("Arcane Torrent")
						&& WoW.PlayerHasBuff("Trueshot")
						&& WoW.PlayerRace == "BloodElf"

						&& WoW.Focus <= 85)
                    {
                        WoW.CastSpell("Arcane Torrent");
                        return;
                    }
					if (WoW.CanCast("Berserking") 
						
						&& !WoW.IsSpellOnCooldown ("Berserking")
						&& WoW.PlayerHasBuff("Trueshot")
						&& WoW.PlayerRace == "Troll")
                    {
                        WoW.CastSpell("Berserking");
                        return;
                    }					
					if (WoW.CanCast("Blood Fury") 
						&& WoW.PlayerHasBuff("Trueshot")
						&& !WoW.IsSpellOnCooldown ("Blood Fury")
						&& WoW.PlayerRace == "Orc")
                    {
                        WoW.CastSpell("Blood Fury");
                        return;
                    }	
					if (WoW.CanCast("Trueshot")
						&& UseCooldowns
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting)
                    {
                        WoW.CastSpell("Trueshot");
                        return;
                    }	
                    if (WoW.CanCast("Murder of Crows")  // AMurderOfCrows if (not HasBuff(Vulnerable) 
						&& WoW.Talent(6) == 1 
						&& (WoW.Focus >= 30) 
						&& !WoW.TargetHasDebuff("Vulnerable")
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting						
						&& WoW.IsSpellInRange("Windburst"))
                    {
                        WoW.CastSpell("Murder of Crows");
                        return;
                    }					
                    if (WoW.CanCast("Murder of Crows")  // or (BuffRemainingSec(Vulnerable) < SpellCastTimeSec(AimedShot) and not HasBuff(LockAndLoad)))
						&& WoW.Talent(6) == 1 
						&& (WoW.Focus >= 30) 
						&& WoW.TargetHasDebuff("Vulnerable")
						&& WoW.TargetDebuffTimeRemaining("Vulnerable") < AimedShotCastTime
						&& !WoW.PlayerHasBuff("Lock and Load")
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting						
						&& WoW.IsSpellInRange("Windburst"))
                    {
                        WoW.CastSpell("Murder of Crows");
                        return;
                    }	
						if (WoW.CanCast("Piercing Shot")  //  PiercingShot if HasBuff(Vulnerable) and PowerToMax < 20
						&& WoW.IsSpellInRange("Windburst") 
						&& WoW.Talent(7) == 2
						&& WoW.TargetHasDebuff("Vulnerable")
						&& WoW.Focus >=100
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting)							
                    {
                        WoW.CastSpell("Piercing Shot");
                        return;
                    }					
						if (WoW.CanCast("Arcane Shot")  //  ArcaneShot if WasLastCast(MarkedShot) and HasTalent(PatientSniper)
						&& WoW.IsSpellInRange("Windburst") 
						&& WoW.Talent(4) == 3
						&& WoW.WasLastCasted("Marked Shot")
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting)							
                    {
                        WoW.CastSpell("Arcane Shot");
                        return;
                    }	
						
						if (WoW.TargetHasDebuff("Vulnerable") // AimedShot if HasBuff(LockAndLoad) and HasBuff(Vulnerable)
						&& WoW.PlayerHasBuff("Lock and Load")
						&& WoW.CanCast("AS") 
						&& WoW.IsSpellInRange("Windburst") 
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting)					
                    {
                        WoW.CastSpell("AS");
						return;
                    }					
						if (WoW.Focus >=50  // AimedShot if (WasLastCast(Windburst)
						&& WoW.WasLastCasted("Windburst")
						&& WoW.CanCast("AS") 
						&& (!WoW.IsMoving || WoW.PlayerHasBuff("Gyroscopic Stabilization")) 
						&& WoW.IsSpellInRange("Windburst") 
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting	)				
                    {
                        WoW.CastSpell("AS");
						return;
                    }					
						if (WoW.Focus >=50  /* SpellCastTimeSec(AimedShot) < BuffRemainingSec(Vulnerable)) and (not HasTalent(PiercingShot)*/
						&& WoW.CanCast("AS") 
						&& WoW.TargetHasDebuff("Vulnerable")
						&& WoW.TargetDebuffTimeRemaining("Vulnerable") > 200	
						&& WoW.Talent(7) == 3
						&& WoW.IsSpellInRange("Windburst") 
						&& (!WoW.IsMoving || WoW.PlayerHasBuff("Gyroscopic Stabilization")) 
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting		)			
                    {
                        WoW.CastSpell("AS");
						return;
                    }		
						if (WoW.Focus >=50  // CooldownSecRemaining(PiercingShot) > BuffRemainingSec(Vulnerable))

						&& WoW.CanCast("AS") 
						&& (!WoW.IsMoving || WoW.PlayerHasBuff("Gyroscopic Stabilization")) 
						&& WoW.TargetHasDebuff("Vulnerable")
						&& WoW.TargetDebuffTimeRemaining("Vulnerable") > 200	
						&& WoW.Talent(7) == 2
						&& WoW.IsSpellOnCooldown("Piercing Shot")
						&& WoW.SpellCooldownTimeRemaining("Piercing Shot") > 200
						&& WoW.IsSpellInRange("Windburst") 
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting	)				
                    {
                        WoW.CastSpell("AS");
						return;
                    }						
					if (WoW.CanCast("Windburst") // Windburst
						&& !WoW.IsMoving
						&& WoW.Focus >= 20 
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting															
						&& WoW.IsSpellInRange("Windburst"))
                    {
                        WoW.CastSpell("Windburst");
                        return;
                    }							
						if (WoW.CanCast("Arcane Shot")  // ArcaneShot if (HasBuff(MarkingTargets)
						&& WoW.IsSpellInRange("Windburst") 
						&& WoW.PlayerHasBuff("Marking Targets")
						&& !WoW.PlayerHasBuff("Hunters Mark")
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting)							
                    {
                        WoW.CastSpell("Arcane Shot");
                        return;
                    }					
					/*	if (WoW.CanCast("Arcane Shot")  //HasBuff(Trueshot)) and not HasBuff(HuntersMark)
						&& WoW.IsSpellInRange("Windburst") 
						&& WoW.Focus <=94
						&& WoW.PlayerHasBuff("Trueshot")
						&& !WoW.PlayerHasBuff("Hunters Mark")
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting)							
                    {
                        WoW.CastSpell("Arcane Shot");
                        return;
                    }	*/					
					if (WoW.CanCast("Marked Shot")   // MarkedShot if not HasTalent(PatientSniper)
						&& (WoW.Focus >= 25) 
						&& WoW.TargetHasDebuff("Hunters Mark") 
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting						
						&& WoW.IsSpellInRange("Windburst")
						&& (WoW.Talent(4) == 1 || WoW.Talent(4) == 2))
					{	
					    WoW.CastSpell("Marked Shot");
                        return;
					}					
					if (WoW.CanCast("Marked Shot")   // (BuffRemainingSec(Vulnerable) < SpellCastTimeSec(AimedShot) and (Power > SpellPowerCost(MarkedShot) + SpellPowerCost(AimedShot) 
						&& (WoW.Focus >= 75) 
						&& WoW.TargetHasDebuff("Hunters Mark") 
						&& WoW.TargetHasDebuff("Vulnerable")
						&& WoW.TargetDebuffTimeRemaining("Vulnerable") < AimedShotCastTime							
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting						
						&& WoW.IsSpellInRange("Windburst"))
					{	
					    WoW.CastSpell("Marked Shot");
                        return;
					}					
					if (WoW.CanCast("Marked Shot")   //  not HasTalent(TrueAim)
						&& (WoW.Focus >= 25) 
						&& WoW.TargetHasDebuff("Hunters Mark") 
						&& (WoW.Talent(1) == 2 || WoW.Talent(1) == 1)
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting						
						&& WoW.IsSpellInRange("Windburst"))
					{	
					    WoW.CastSpell("Marked Shot");
                        return;
					}		
						if ((!WoW.IsMoving || WoW.PlayerHasBuff("Gyroscopic Stabilization"))   // AimedShot if PowerToMax < 25
						&& WoW.Focus >= 95 
						&& WoW.CanCast("AS") && WoW.IsSpellInRange("Windburst") 
						&& (WoW.Talent(7) == 3 || WoW.Talent(7) == 1)						
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting)
                    {
                        WoW.CastSpell("AS");
						
                        return;
                    }
						if ((!WoW.IsMoving || WoW.PlayerHasBuff("Gyroscopic Stabilization"))   // AimedShot if PowerToMax < 25
						&& WoW.Focus >= 95 
						&& WoW.CanCast("AS") && WoW.IsSpellInRange("Windburst") 
						&& WoW.Talent(7) == 2		
						&& WoW.IsSpellOnCooldown("Piercing Shot")
						&& WoW.SpellCooldownTimeRemaining("Piercing Shot") > 300
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting)
                    {
                        WoW.CastSpell("AS");
						
                        return;
                    }						
						if (WoW.CanCast("Arcane Shot")  //  added: less than 50 focus
						&& WoW.IsSpellInRange("Windburst") 
						&& WoW.Focus <=49
						&& WoW.Talent(1) == 1
						&& !WoW.PlayerHasBuff("Lock and Load")
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting)							
                    {
                        WoW.CastSpell("Arcane Shot");
                        return;
                    }		
						if (WoW.CanCast("Arcane Shot")  // arcane shot
						&& WoW.IsSpellInRange("Windburst") 
						&& WoW.Focus <=99
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting
						&& WoW.Talent(7) == 2)
						
                    {
                        WoW.CastSpell("Arcane Shot");
                        return;
                    }					
						if (WoW.CanCast("Arcane Shot")  // arcane shot
						&& WoW.IsSpellInRange("Windburst") 
						&& WoW.Focus <=94
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting
						&& !WoW.TargetHasDebuff("Vulnerable"))
						
                    {
                        WoW.CastSpell("Arcane Shot");
                        return;
                    }					
					
					
					
					
					
					
					
					
					
					
					
					
				
			} 
			}
            if (combatRoutine.Type == RotationType.AOE)
            {
				if (WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsInCombat && WoW.PlayerHasBuff("Trueshot"))
				
				{
					if (WoW.CanCast("Multi-Shot") 
						&& WoW.Focus >=40
						&& WoW.IsSpellInRange("Windburst") 
						&& !WoW.TargetHasDebuff("Hunters Mark")
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting)					
						
                    {
                        WoW.CastSpell("Multi-Shot");
                        return;
                    }			
					
					if (WoW.CanCast("Marked Shot") 
						&& (WoW.Focus >= 25) 
						&& WoW.TargetHasDebuff("Hunters Mark") 
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting						
						&& WoW.IsSpellInRange("Windburst")
						&& (WoW.TargetHasDebuff("Vulnerable")||!WoW.TargetHasDebuff("Vulnerable")))
					{	
					    WoW.CastSpell("Marked Shot");
                        return;
					}	
					
				}
				if (WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsInCombat && !WoW.PlayerHasBuff("Trueshot"))
                {	
					if (WoW.CanCast("Counter Shot") 
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting
						&& WoW.TargetIsCasting
						&& WoW.TargetIsCastingAndSpellIsInterruptible
						&& WoW.TargetPercentCast >=24
						&& CounterShot)
                    {
                        WoW.CastSpell("Counter Shot");
                        return;
                    }				
					if (WoW.CanCast("Volley") 
						&& !WoW.PlayerHasBuff("Volley")
						&& WoW.Talent(6) == 3)
                    {
                        WoW.CastSpell("Volley");
                        return;
                    }		
					if (KilJaeden && !WoW.ItemOnCooldown("Kil'jaeden's Burning Wish"))  
                    {
                        WoW.CastSpell("Kil'jaeden's Burning Wish");
                        return;
                    }						
					if (WoW.CanCast("Arcane Torrent") 
						
						&& !WoW.IsSpellOnCooldown ("Arcane Torrent")
						&& WoW.PlayerHasBuff("Trueshot")
						&& WoW.PlayerRace == "BloodElf"
						&& WoW.Focus <= 85)
                    {
                        WoW.CastSpell("Arcane Torrent");
                        return;
                    }
					if (WoW.CanCast("Berserking") 
						
						&& !WoW.IsSpellOnCooldown ("Berserking")
						&& WoW.PlayerHasBuff("Trueshot")
						&& WoW.PlayerRace == "Troll")
                    {
                        WoW.CastSpell("Berserking");
                        return;
                    }					
					if (WoW.CanCast("Blood Fury") 
						&& WoW.PlayerHasBuff("Trueshot")
						&& !WoW.IsSpellOnCooldown ("Blood Fury")
						&& WoW.PlayerRace == "Orc")
                    {
                        WoW.CastSpell("Blood Fury");
                        return;
                    }	
					if (WoW.CanCast("Trueshot")
						&& UseCooldowns
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting)
                    {
                        WoW.CastSpell("Trueshot");
                        return;
                    }	
					if (WoW.CanCast("Piercing Shot") 
						&& WoW.TargetHasDebuff("Vulnerable") 
						&& WoW.Focus >= 100 
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting			
						&& WoW.Talent(7) == 2
						&& WoW.IsSpellInRange("Windburst"))
                    {
                        WoW.CastSpell("Piercing Shot");
                        return;
                    }		
					
                    if (WoW.CanCast("Murder of Crows") 
						&& WoW.Talent(6) == 1 
						&& (WoW.Focus >= 30) 
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting						
						&& WoW.IsSpellInRange("Windburst"))
                    {
                        WoW.CastSpell("Murder of Crows");
                        return;
                    }
					
					if (WoW.CanCast("Marked Shot") 
						&& (WoW.Focus >= 25) 
						&& WoW.TargetHasDebuff("Hunters Mark") 
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting						
						&& WoW.IsSpellInRange("Windburst")
						&& WoW.TargetHasDebuff("Vulnerable")						
						&& (WoW.TargetDebuffTimeRemaining("Vulnerable") <= 100))
					{	
					    WoW.CastSpell("Marked Shot");
                        return;
					}	
					if (WoW.CanCast("Marked Shot") 
						&& (WoW.Focus >= 25) 
						&& WoW.TargetHasDebuff("Hunters Mark") 
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting						
						&& WoW.IsSpellInRange("Windburst")
						&& !WoW.TargetHasDebuff("Vulnerable"))						

					{	
					    WoW.CastSpell("Marked Shot");
                        return;
					}						
/* 					if (WoW.CanCast("Windburst") 
						&& !WoW.IsMoving
						&& WoW.Focus >= 20 
						&& WoW.TargetHasDebuff("Vulnerable") 
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting						
						&& (WoW.TargetDebuffTimeRemaining("Vulnerable") <= 1)						
						&& WoW.IsSpellInRange("Windburst"))
                    {
                        WoW.CastSpell("Windburst");
                        return;
                    }
					if (WoW.CanCast("Windburst") 
						&& !WoW.IsMoving
						&& WoW.Focus >= 20 
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting						
						&& !WoW.TargetHasDebuff("Vulnerable") 										
						&& WoW.IsSpellInRange("Windburst"))
                    {
                        WoW.CastSpell("Windburst");
                        return;
                    }	
					*/
					
														
                    if ((!WoW.IsMoving || WoW.PlayerHasBuff("Gyroscopic Stabilization"))   // with piercing shot
						&& WoW.Focus >= 50 
						&& WoW.TargetHasDebuff("Vulnerable") 
						&& WoW.CanCast("AS") 
						&& (WoW.TargetDebuffTimeRemaining("Vulnerable") > AimedShotCastTime) 
						&& WoW.IsSpellInRange("Windburst") 
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting
						&& WoW.IsSpellOnCooldown("Piercing Shot")
						&& WoW.Talent(7) == 2
						&& (WoW.SpellCooldownTimeRemaining ("Piercing Shot") >300 || WoW.Focus >100))						
                    {
                        WoW.CastSpell("AS");
						
						                    if ((!WoW.IsMoving || WoW.PlayerHasBuff("Gyroscopic Stabilization"))   // with piercing shot
						&& WoW.Focus >= 50 
						&& WoW.TargetHasDebuff("Vulnerable") 
						&& WoW.CanCast("AS") 
						&& (WoW.TargetDebuffTimeRemaining("Vulnerable") > AimedShotCastTime) 
						&& WoW.IsSpellInRange("Windburst") 
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting
						&& WoW.IsSpellOnCooldown("Piercing Shot")
						&& WoW.Talent(7) == 2
						&& (WoW.SpellCooldownTimeRemaining ("Piercing Shot") >300 || WoW.Focus >100))						
                    {
                        WoW.CastSpell("AS");
						
						                    if ((!WoW.IsMoving || WoW.PlayerHasBuff("Gyroscopic Stabilization"))   // with piercing shot
						&& WoW.Focus >= 50 
						&& WoW.TargetHasDebuff("Vulnerable") 
						&& WoW.CanCast("AS") 
						&& (WoW.TargetDebuffTimeRemaining("Vulnerable") > AimedShotCastTime) 
						&& WoW.IsSpellInRange("Windburst") 
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting
						&& WoW.IsSpellOnCooldown("Piercing Shot")
						&& WoW.Talent(7) == 2
						&& (WoW.SpellCooldownTimeRemaining ("Piercing Shot") >300 || WoW.Focus >100))						
                    {
                        WoW.CastSpell("AS");
						return;
                    }	
					}
					}
                    if ((!WoW.IsMoving || WoW.PlayerHasBuff("Gyroscopic Stabilization"))  
						&& WoW.Focus >= 95 
						&& !WoW.TargetHasDebuff("Vulnerable") 
						&& WoW.CanCast("AS") && WoW.IsSpellInRange("Windburst") 
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting
						&& WoW.IsSpellOnCooldown("Piercing Shot")
						&& WoW.Talent(7) == 2
						&& (WoW.SpellCooldownTimeRemaining ("Piercing Shot") >300 || WoW.Focus >100))
                    {
                        WoW.CastSpell("AS");
						
						                    if ((!WoW.IsMoving || WoW.PlayerHasBuff("Gyroscopic Stabilization"))  
						&& WoW.Focus >= 95 
						&& !WoW.TargetHasDebuff("Vulnerable") 
						&& WoW.CanCast("AS") && WoW.IsSpellInRange("Windburst") 
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting
						&& WoW.IsSpellOnCooldown("Piercing Shot")
						&& WoW.Talent(7) == 2
						&& (WoW.SpellCooldownTimeRemaining ("Piercing Shot") >300 || WoW.Focus >100))
                    {
                        WoW.CastSpell("AS");
						
						                    if ((!WoW.IsMoving || WoW.PlayerHasBuff("Gyroscopic Stabilization"))  
						&& WoW.Focus >= 95 
						&& !WoW.TargetHasDebuff("Vulnerable") 
						&& WoW.CanCast("AS") && WoW.IsSpellInRange("Windburst") 
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting
						&& WoW.IsSpellOnCooldown("Piercing Shot")
						&& WoW.Talent(7) == 2
						&& (WoW.SpellCooldownTimeRemaining ("Piercing Shot") >300 || WoW.Focus >100))
                    {
                        WoW.CastSpell("AS");
						
                        return;
                    }
					}
					}
                    if ((!WoW.IsMoving || WoW.PlayerHasBuff("Gyroscopic Stabilization"))   // without piercing shot
						&& WoW.Focus >= 50 
						&& WoW.TargetHasDebuff("Vulnerable") 
						&& WoW.CanCast("AS") 
						&& (WoW.TargetDebuffTimeRemaining("Vulnerable") > AimedShotCastTime) 
						&& WoW.IsSpellInRange("Windburst") 
						&& WoW.Talent(7) == 3
						&& !WoW.PlayerIsChanneling					
						&& !WoW.PlayerIsCasting)
					
                    {
                        WoW.CastSpell("AS");
						
						                    if ((!WoW.IsMoving || WoW.PlayerHasBuff("Gyroscopic Stabilization"))   // without piercing shot
						&& WoW.Focus >= 50 
						&& WoW.TargetHasDebuff("Vulnerable") 
						&& WoW.CanCast("AS") 
						&& (WoW.TargetDebuffTimeRemaining("Vulnerable") > AimedShotCastTime) 
						&& WoW.IsSpellInRange("Windburst") 
						&& WoW.Talent(7) == 3
						&& !WoW.PlayerIsChanneling					
						&& !WoW.PlayerIsCasting)
					
                    {
                        WoW.CastSpell("AS");
						
						                    if ((!WoW.IsMoving || WoW.PlayerHasBuff("Gyroscopic Stabilization"))   // without piercing shot
						&& WoW.Focus >= 50 
						&& WoW.TargetHasDebuff("Vulnerable") 
						&& WoW.CanCast("AS") 
						&& (WoW.TargetDebuffTimeRemaining("Vulnerable") > AimedShotCastTime) 
						&& WoW.IsSpellInRange("Windburst") 
						&& WoW.Talent(7) == 3
						&& !WoW.PlayerIsChanneling					
						&& !WoW.PlayerIsCasting)
					
                    {
                        WoW.CastSpell("AS");
						
                        return;
                    }	
					}
					}
                    				
                    if (WoW.PlayerHasBuff ("Lock and Load") 
						&& WoW.CanCast("AS") 
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting		
						&& WoW.TargetHasDebuff("Vulnerable")
						&& WoW.IsSpellInRange("Windburst")) 
                    {
                        WoW.CastSpell("AS");
                        return;
                    }	
						if (WoW.CanCast("Multi-Shot") 
						&& WoW.IsSpellInRange("Windburst") 
						&& WoW.Focus <=94
						&& WoW.TargetHasDebuff("Vulnerable")
						&& WoW.TargetDebuffTimeRemaining("Vulnerable") < AimedShotCastTime						
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting)						
						
                    {
                        WoW.CastSpell("Multi-Shot");
                        return;
                    }					
						if (WoW.CanCast("Multi-Shot") 
						&& WoW.IsSpellInRange("Windburst") 
						&& WoW.PlayerHasBuff("Marking Targets")
						&& !WoW.TargetHasDebuff("Hunters Mark")
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting)						
						
                    {
                        WoW.CastSpell("Multi-Shot");
                        return;
                    }
					if (WoW.CanCast("Multi-Shot") 
						&& WoW.IsSpellInRange("Windburst") 
						&& !WoW.PlayerHasBuff("Marking Targets")
						&& !WoW.TargetHasDebuff("Vulnerable")
						&& !WoW.TargetHasDebuff("Hunters Mark")
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting)						
						
                    {
                        WoW.CastSpell("Multi-Shot");
                        return;
                    }
					if (WoW.CanCast("Multi-Shot") 
						&& WoW.IsSpellInRange("Windburst") 
						&& WoW.Focus <=99
						&& WoW.Talent(7) == 2
						&& !WoW.IsSpellOnCooldown("Piercing Shot")
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting)						
						
                    {
                        WoW.CastSpell("Multi-Shot");
                        return;
                    }	
						if (WoW.CanCast("Multi-Shot") 
						&& WoW.IsSpellInRange("Windburst") 
						&& WoW.Focus <=99
						&& WoW.Talent(7) == 2
						&& WoW.IsSpellOnCooldown("Piercing Shot")
						&& WoW.SpellCooldownTimeRemaining ("Piercing Shot") <3						
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting)						
						
                    {
                        WoW.CastSpell("Multi-Shot");
                        return;
                    }						
						if (WoW.CanCast("Multi-Shot") 
						&& WoW.IsSpellInRange("Windburst") 
						&& WoW.TargetHasDebuff("Vulnerable")
						&& WoW.Focus <=49
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting)						
						
                    {
                        WoW.CastSpell("Multi-Shot");
                        return;
                    }						
                   /* if (WoW.CanCast("Arcane Shot") 
						&& WoW.IsSpellInRange("Windburst") 
						&& WoW.Focus <=94
						&& !WoW.TargetHasDebuff("Vulnerable")
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting)						
						
                    {
                        WoW.CastSpell("Arcane Shot");
                        return;
                    }						
                    if (WoW.CanCast("Arcane Shot") 
						&& WoW.IsSpellInRange("Windburst") 
						&& WoW.Focus <=94
						&& WoW.TargetHasDebuff("Vulnerable")
						&& (WoW.TargetDebuffTimeRemaining("Vulnerable") <= 1.9 ||	WoW.TargetDebuffTimeRemaining("Vulnerable") >= 5)						
						&& WoW.IsSpellOnCooldown("Windburst")
						&& !WoW.PlayerIsChanneling
						&& !WoW.PlayerIsCasting)						
						
                    {
                        WoW.CastSpell("Arcane Shot");
                        return;
                    }
*/					
				}
			}

		}
	}

}
	

/*
[AddonDetails.db]
AddonAuthor=Vectarius
AddonName=myspellpriority
WoWVersion=Legion - 70300
[SpellBook.db]
Spell,194386,Volley,D3
Spell,204147,Windburst,D2
Spell,120360,Barrage,D3
Spell,131894,Murder of Crows,D9
Spell,19434,AS,D4
Spell,185358,Arcane Shot,D5
Spell,185901,Marked Shot,D6
Spell,186387,Bursting Shot,D7
Spell,147362,Counter Shot,D8
Spell,198670,Piercing Shot,D1
Spell,2643,Multi-Shot,D0
Spell,109304,Exhil,V
Spell,193526,Trueshot,C
Spell,186265,Turtle,G
Spell,5384,Death,F
Spell,144259,Kil'jaeden's Burning Wish,F4
Spell,20572,Blood Fury,F3
Spell,80483,Arcane Torrent,F3
Spell,26297,Berserking,F3
Aura,194386,Volley
Aura,223138,Marking Targets
Aura,185365,Hunters Mark
Aura,194594,Lock and Load
Aura,187131,Vulnerable
Aura,193526,Trueshot
Aura,2825,Bloodlust
Aura,235712,Gyroscopic Stabilization
Item,5512,Healthstone
Item,127834,Ancient Healing Potion
Item,133940,Silkweave Bandage
Item,144259,Kil'jaeden's Burning Wish
*/
