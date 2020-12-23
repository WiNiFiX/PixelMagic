// winifix@gmail.com
// ReSharper disable UnusedMember.Global

using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using ShadowMagic.Helpers;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Threading;

namespace ShadowMagic.Rotation
{
	
	public class BMHunterVectarius : CombatRoutine
    {
		
		
		private NumericUpDown nudExhilarationPercentValue;
		private NumericUpDown nudAspectoftheTurtlePercentValue;
		private NumericUpDown nudFeignDeathPercentValue;


		
		 private float GCD
        {
            get
            {

                    return (150f / (1f + (WoW.HastePercent / 100f)));

            }
        }
private float FocusRegen
{
     get
     {
         return (10f* (1f + (WoW.HastePercent / 100f)));
     }
}	
private float FocusTimetoMax
{
     get
     {
         return ((120f - WoW.Focus) /(10f* (1f + (WoW.HastePercent / 100f)))) *100f;
     }
}	

		//Pet Control	
		private CheckBox HealPetBox;
		// Items
		private CheckBox KilJaedenBox;			


		// DEF cds
		private CheckBox ExhilarationBox;
		private CheckBox FeignDeathBox;
		private CheckBox AspectoftheTurtleBox;	

		private CheckBox CounterShotBox;		
		
		//dps cds
		private CheckBox AspectoftheWildBox;
		
		

		private static bool KilJaeden
        {
            get
            {
                var KilJaeden = ConfigFile.ReadValue("HunterBeastmastery", "KilJaeden").Trim();

                return KilJaeden != "" && Convert.ToBoolean(KilJaeden);
            }
            set { ConfigFile.WriteValue("HunterBeastmastery", "KilJaeden", value.ToString()); }
        }	
		
        private static bool HealPet
        {
            get
            {
                var HealPet = ConfigFile.ReadValue("HunterBeastmastery", "HealPet").Trim();

                return HealPet != "" && Convert.ToBoolean(HealPet);
            }
            set { ConfigFile.WriteValue("HunterBeastmastery", "HealPet", value.ToString()); }
        }
		
		

		
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
		
        private static bool AspectoftheWild
        {
            get
            {
                var AspectoftheWild = ConfigFile.ReadValue("HunterBeastmastery", "AspectoftheWild").Trim();

                return AspectoftheWild != "" && Convert.ToBoolean(AspectoftheWild);
            }
            set { ConfigFile.WriteValue("HunterBeastmastery", "AspectoftheWild", value.ToString()); }
        }		
		

		

        
      public override string Name
        {
            get { return "Hunter Beast Mastery"; }
        }

        
		 public override string Class
        {
            get { return "Hunter"; }
        }

        public override Form SettingsForm { get; set; }
		
		
        public override void Initialize()
        {
           
			if (ConfigFile.ReadValue("Hunter", "AspectoftheTurtle Percent") == "")
            {
                ConfigFile.WriteValue("Hunter", "AspectoftheTurtle Percent", "15");
            }
						if (ConfigFile.ReadValue("Hunter", "FeignDeath Percent") == "")
            {
                ConfigFile.WriteValue("Hunter", "FeignDeath Percent", "5");
            }
						if (ConfigFile.ReadValue("Hunter", "Exhilaration Percent") == "")
            {
                ConfigFile.WriteValue("Hunter", "Exhilaration Percent", "45");
            }
		   
SettingsForm = new Form {Text = "Beast Mastery Hunter", StartPosition = FormStartPosition.CenterScreen, Width = 400, Height = 500, ShowIcon = false};

            nudAspectoftheTurtlePercentValue = new NumericUpDown 
			{Minimum = 0, Maximum = 100, Value = ConfigFile.ReadValue<decimal>("Hunter", "AspectoftheTurtle Percent"), 
			Left = 215, 
			Top = 172,
			Size = new Size (40, 10)
			}; 
			SettingsForm.Controls.Add(nudAspectoftheTurtlePercentValue);
			
		

            nudExhilarationPercentValue = new NumericUpDown 
			{Minimum = 0, Maximum = 100, Value = ConfigFile.ReadValue<decimal>("Hunter", "Exhilaration Percent"), 
			Left = 215, 
			Top = 122,
			Size = new Size (40, 10)
			};
			SettingsForm.Controls.Add(nudExhilarationPercentValue);
			
			
		

            nudFeignDeathPercentValue = new NumericUpDown 
			{Minimum = 0, Maximum = 100, Value = ConfigFile.ReadValue<decimal>("Hunter", "FeignDeath Percent"), 
			Left = 215, 
			Top =147,
			Size = new Size (40, 10)
			};
			SettingsForm.Controls.Add(nudFeignDeathPercentValue);
			
			


			
			var lblTitle = new Label
            {
                Text =
                    "BM Hunter by Vectarius",
                Size = new Size(270, 14),
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
                Top = 50
            };
			lblTextBox3.ForeColor = Color.Black;
			 SettingsForm.Controls.Add(lblTextBox3);

			 
			var lblAspectoftheWildBox = new Label
            {
                Text =
                    "Aspect of the Wild",
                Size = new Size(270, 15),
                Left = 100,
                Top = 75
            };
			
			lblAspectoftheWildBox.ForeColor = Color.Black;
            SettingsForm.Controls.Add(lblAspectoftheWildBox);			
           
			var lblCounterShotBox = new Label
            {
                Text =
                    "Counter Shot",
                Size = new Size(270, 15),
                Left = 100,
                Top = 100
            };
			
			lblCounterShotBox.ForeColor = Color.Black;
            SettingsForm.Controls.Add(lblCounterShotBox);	

			var lblExhilarationBox = new Label
            {
                Text =
                    "Exhilaration @",
                Size = new Size(270, 15),
                Left = 100,
                Top = 125
            };
			
			lblExhilarationBox.ForeColor = Color.Black;
            SettingsForm.Controls.Add(lblExhilarationBox);	

			var lblAspectoftheTurtleBox = new Label
            {
                Text =
                    "Aspect of the Turtle @",
                Size = new Size(270, 15),
                Left = 100,
                Top = 175
            };
			
			lblAspectoftheTurtleBox.ForeColor = Color.Black;
            SettingsForm.Controls.Add(lblAspectoftheTurtleBox);	

			var lblFeignDeathBox = new Label
            {
                Text =
                    "Feign Death @",
                Size = new Size(270, 15),
                Left = 100,
                Top = 150
            };
			
			lblFeignDeathBox.ForeColor = Color.Black;
            SettingsForm.Controls.Add(lblFeignDeathBox);		



					
			 
			var lblTextBox5 = new Label
            {
                Text =
                    "Pet Control",
                Size = new Size(200, 17),
                Left = 70,
                Top = 225
            };
			lblTextBox5.ForeColor = Color.Black;
			 SettingsForm.Controls.Add(lblTextBox5);			 

						var lblTextBox6 = new Label
            {
                Text =
                    "Items",
                Size = new Size(200, 17),
                Left = 70,
                Top = 275
            };
			lblTextBox6.ForeColor = Color.Black;
			 SettingsForm.Controls.Add(lblTextBox6);
			 



	

			var lblHealPetBox = new Label
            {
                Text =
                    "Heal Pet",
                Size = new Size(270, 15),
                Left = 100,
                Top = 250
            };
			
			lblHealPetBox.ForeColor = Color.Black;
            SettingsForm.Controls.Add(lblHealPetBox);	

			var lblKilJaedenBox = new Label
            {
                Text =
                    "Kil'Jaeden's Burning Wish",
                Size = new Size(270, 15),
                Left = 100,
                Top = 300
            };
			
			lblKilJaedenBox.ForeColor = Color.Black;
            SettingsForm.Controls.Add(lblKilJaedenBox);			
		   

			
			
			
			
			var cmdSave = new Button {Text = "Save", Width = 65, Height = 25, Left = 5, Top = 375, Size = new Size(120, 31)};
			
			var cmdReadme = new Button {Text = "Macros! Use Them", Width = 65, Height = 25, Left = 125, Top = 375, Size = new Size(120, 31)};
			
 

//items
            KilJaedenBox = new CheckBox {Checked = KilJaeden, TabIndex = 8, Size = new Size(14, 14), Left = 70, Top = 300};		
            SettingsForm.Controls.Add(KilJaedenBox);
//pet control			
			HealPetBox = new CheckBox {Checked = HealPet, TabIndex = 8, Size = new Size(14, 14), Left = 70, Top = 250};			
            SettingsForm.Controls.Add(HealPetBox);
			
			// Checkboxes
            CounterShotBox = new CheckBox {Checked = CounterShot, TabIndex = 8, Size = new Size(14, 14), Left = 70, Top = 100};		
            SettingsForm.Controls.Add(CounterShotBox);
			ExhilarationBox = new CheckBox {Checked = Exhilaration, TabIndex = 8, Size = new Size(14, 14), Left = 70, Top = 125};			
            SettingsForm.Controls.Add(ExhilarationBox);
			FeignDeathBox = new CheckBox {Checked = FeignDeath, TabIndex = 8, Size = new Size(14, 14), Left = 70, Top = 150};
            SettingsForm.Controls.Add(FeignDeathBox);
			
			AspectoftheTurtleBox = new CheckBox {Checked = AspectoftheTurtle, TabIndex = 8, Size = new Size(14, 14), Left = 70, Top = 175};			
			            SettingsForm.Controls.Add(AspectoftheTurtleBox);		
			//dps cooldowns
            AspectoftheWildBox = new CheckBox {Checked = AspectoftheWild, TabIndex = 8, Size = new Size(14, 14), Left = 70, Top = 75};
            SettingsForm.Controls.Add(AspectoftheWildBox);			

			
			
			CounterShotBox.Checked = CounterShot;	
			ExhilarationBox.Checked = Exhilaration;	
			FeignDeathBox.Checked = FeignDeath;	
			AspectoftheTurtleBox.Checked = AspectoftheTurtle;	
			
			AspectoftheWildBox.Checked = AspectoftheWild;

			

		
			
			//cmdSave

			
            KilJaedenBox.CheckedChanged += KilJaeden_Click;    
            HealPetBox.CheckedChanged += HealPet_Click;				
			
            AspectoftheWildBox.CheckedChanged += AspectoftheWild_Click;    
            ExhilarationBox.CheckedChanged += Exhilaration_Click; 
            CounterShotBox.CheckedChanged += CounterShot_Click;
            FeignDeathBox.CheckedChanged += FeignDeath_Click;
            AspectoftheTurtleBox.CheckedChanged += AspectoftheTurtle_Click;	
			
			
			cmdSave.Click += CmdSave_Click;
			cmdReadme.Click += CmdReadme_Click;
 
			
			SettingsForm.Controls.Add(cmdSave);
			SettingsForm.Controls.Add(cmdReadme);
		
			lblTextBox5.BringToFront();		
			lblTextBox6.BringToFront();				
			lblTitle.BringToFront();

			nudExhilarationPercentValue.BringToFront();
			nudAspectoftheTurtlePercentValue.BringToFront();
			nudFeignDeathPercentValue.BringToFront();		
			
            KilJaedenBox.BringToFront();	
            HealPetBox.BringToFront();				
			
            AspectoftheWildBox.BringToFront();	
            CounterShotBox.BringToFront();	
            ExhilarationBox.BringToFront();
            FeignDeathBox.BringToFront();
            AspectoftheTurtleBox.BringToFront();				
			

			
			
		}
			
			private void CmdSave_Click(object sender, EventArgs e)
        {


            KilJaeden = KilJaedenBox.Checked;		
            HealPet = HealPetBox.Checked;				
			
            AspectoftheWild = AspectoftheWildBox.Checked;		
            CounterShot = CounterShotBox.Checked;	
            Exhilaration = ExhilarationBox.Checked;
            FeignDeath = FeignDeathBox.Checked;
            AspectoftheTurtle = AspectoftheTurtleBox.Checked;			
			
            ConfigFile.WriteValue("Hunter", "AspectoftheTurtle Percent", nudAspectoftheTurtlePercentValue.Value.ToString());
	        ConfigFile.WriteValue("Hunter", "FeignDeath Percent", nudFeignDeathPercentValue.Value.ToString());		
            ConfigFile.WriteValue("Hunter", "Exhilaration Percent", nudExhilarationPercentValue.Value.ToString());			
			
			
            MessageBox.Show("Settings saved.", "PixelMagic", MessageBoxButtons.OK, MessageBoxIcon.Information);
            SettingsForm.Close();
        }
		private void CmdReadme_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                " make sure you make macros of Kill Command and Dire Frenzy/Beast /petattack",
                "PixelMagic", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
	

		
//items
		private void KilJaeden_Click(object sender, EventArgs e)
        {
            KilJaeden = KilJaedenBox.Checked;
        }			
			
//pet control			
		private void HealPet_Click(object sender, EventArgs e)
        {
            HealPet = HealPetBox.Checked;
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
			//dpscooldown
        private void AspectoftheWild_Click(object sender, EventArgs e)
        {
            AspectoftheWild = AspectoftheWildBox.Checked;
        }			

		
		
        public override void Stop()
        {
			
			
        }

        private static bool lastNamePlate = true;
        public void SelectRotation(int aoe, int cleave, int single)
        {
            int count = WoW.CountEnemyNPCsInRange;
            if (!lastNamePlate)
            {
                combatRoutine.ChangeType(RotationType.SingleTarget);
                lastNamePlate = true;
            }
            lastNamePlate = WoW.Nameplates;
            if (count >= aoe)
                combatRoutine.ChangeType(RotationType.AOE);
            if (count == cleave)
                combatRoutine.ChangeType(RotationType.SingleTargetCleave);
            if (count <= single)
                combatRoutine.ChangeType(RotationType.SingleTarget);

        }       

        public override void Pulse()
        {
			if (WoW.IsInCombat && Control.IsKeyLocked(Keys.Scroll) && !WoW.TargetIsPlayer && !WoW.IsMounted)
			{	
			SelectRotation(4, 100, 1 );
			}
			
		if (DetectKeyPress.GetKeyState(0x6A) < 0)
            {
                UseCooldowns = !UseCooldowns;
                Thread.Sleep(150);
            }			
				if(WoW.IsInCombat && !WoW.HasTarget)
				{
				WoW.KeyPressRelease(WoW.Keys.Tab);
				return;
				}
				if (WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsInCombat)
				{
Log.Write("focus * cdremain KC >300 : " + ((FocusRegen*WoW.SpellCooldownTimeRemaining("Kill Command")) > 300));
				     if (WoW.CanCast("FeignDeath") && WoW.HealthPercent <= ConfigFile.ReadValue<int>("Hunter", "FeignDeath Percent") && FeignDeath && !WoW.IsSpellOnCooldown("FeignDeath") && WoW.HealthPercent != 0)
                    {
                        WoW.CastSpell("FeignDeath");
                        return;
                    }
                    if (WoW.CanCast("Exhilaration") && WoW.HealthPercent <= ConfigFile.ReadValue<int>("Hunter", "Exhilaration Percent") && Exhilaration && !WoW.IsSpellOnCooldown("Exhilaration") && WoW.HealthPercent != 0)
                    {
                        WoW.CastSpell("Exhilaration");
                        return;
                    }	
					if (WoW.CanCast("AspectoftheTurtle") && WoW.HealthPercent <= ConfigFile.ReadValue<int>("Hunter", "AspectoftheTurtle Percent") && AspectoftheTurtle && !WoW.IsSpellOnCooldown("AspectoftheTurtle") && WoW.HealthPercent != 0)
                    {
                        WoW.CastSpell("AspectoftheTurtle");
                        return;
                    }
					if (WoW.CanCast("Ancient Healing Potion") && WoW.HealthPercent < 20 && !WoW.IsSpellOnCooldown("Ancient Healing Potion") && WoW.HealthPercent != 0)
						{
							WoW.CastSpell("Ancient Healing Potion");
							return;
						}
					if (WoW.CanCast("Silkweave Bandage") && WoW.HealthPercent < 40 && WoW.PlayerHasBuff("Turtle") && !WoW.IsMoving && !WoW.PlayerHasDebuff("Bandaged"))
						{
							WoW.CastSpell("Silkweave Bandage");
							return;
						}	
					
                    if (!WoW.HasPet && WoW.CanCast("Wolf"))
					{
						WoW.CastSpell("Wolf") ;
						return;
					}
					if (WoW.PetHealthPercent <= 0 && WoW.CanCast("Phoenix"))
					{
						WoW.CastSpell("Phoenix") ;
						return;
					}
				
					if (WoW.PetHealthPercent <= 90 
						&& !WoW.PetHasBuff("Heal Pet")
						&& HealPet						
						&& WoW.CanCast("Revive Pet") 
						&& !WoW.IsMoving)
					{
						WoW.CastSpell("Heal Pet") ;
						return;
					}					
					if (WoW.PetHealthPercent <= 0 
						&& WoW.IsSpellOnCooldown("Phoenix") 
						&& WoW.CanCast("Revive Pet") 
						&& !WoW.IsMoving)
					{
						WoW.CastSpell("Revive Pet") ;
						return;
					}	
					                    if (WoW.TargetIsCasting)
                    {
                        if (WoW.CanCast("Counter Shot") 
							&& WoW.TargetIsCastingAndSpellIsInterruptible 
							&& WoW.TargetPercentCast >= 60 
							&& !WoW.IsSpellOnCooldown("Counter Shot") 
							&& !WoW.PlayerIsChanneling 
							&& !WoW.WasLastCasted("Counter Shot"))
                        {
                            WoW.CastSpell("Counter Shot");						
                            return;
                        }	
					}
				}
			if (combatRoutine.Type == RotationType.SingleTarget || combatRoutine.Type == RotationType.SingleTargetCleave)  
            {

			if (WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsInCombat)
                {	
			
					if (WoW.CanCast("A Murder of Crows") 
						&& WoW.Talent(6) == 1
						&& WoW.Focus >= 25
						&& WoW.IsSpellInRange("Cobra Shot")
						&& !WoW.IsSpellOnCooldown("A Murder of Crows")	)
                    {
                        WoW.CastSpell("A Murder of Crows");
                        return;
                    }
	
					if (WoW.CanCast("Volley") 
						&& !WoW.PlayerHasBuff("Volley")
						&& WoW.Talent(6) == 3)
                    {
                        WoW.CastSpell("Volley");
                        return;
                    }
					if (WoW.CanCast("Arcane Torrent") 
						
						&& !WoW.IsSpellOnCooldown ("Arcane Torrent")
						&& WoW.PlayerRace == "BloodElf"
						&& WoW.Focus <= 85)
                    {
                        WoW.CastSpell("Arcane Torrent");
                        return;
                    }
					if (WoW.CanCast("Berserking") 
						
						&& !WoW.IsSpellOnCooldown ("Berserking")
						&& WoW.PlayerRace == "Troll")
                    {
                        WoW.CastSpell("Berserking");
                        return;
                    }					
					if (WoW.CanCast("Blood Fury") 
						
						&& !WoW.IsSpellOnCooldown ("Blood Fury")
						&& WoW.PlayerRace == "Orc")
                    {
                        WoW.CastSpell("Blood Fury");
                        return;
                    }	
					if (WoW.CanCast("Chimaera Shot") 
						&& WoW.Focus <90
						&& WoW.IsSpellOnCooldown("Dire Frenzy")
						&& WoW.IsSpellOnCooldown("Kill Command")						
						&& WoW.CanCast("Chimaera Shot")
						&& WoW.Talent(2) == 3)
						{
                        WoW.CastSpell("Chimaera Shot");
                        return;
						}					
					if (WoW.CanCast("Kil'jaeden's Burning Wish") && KilJaeden && !WoW.ItemOnCooldown("Kil'jaeden's Burning Wish") && !WoW.IsSpellOnCooldown("Kil'jaeden's Burning Wish"))  
                    {
                        WoW.CastSpell("Kil'jaeden's Burning Wish");
                        return;
                    }					
//	stampede,if=buff.bloodlust.up|buff.bestial_wrath.up|cooldown.bestial_wrath.remains<=2|target.time_to_die<=14	
					if (WoW.CanCast("Stampede") && WoW.Talent(7) == 1 && WoW.IsSpellInRange("Cobra Shot") && ((WoW.PlayerHasBuff("Bestial Wrath")) || (WoW.SpellCooldownTimeRemaining("Bestial Wrath") <=2))						
						&& !WoW.PlayerHasBuff("AspectoftheTurtle")
						&& !WoW.IsSpellOnCooldown("Stampede")) 
                    {
                        WoW.CastSpell("Stampede");

                        return;
                    }				
//dire_beast,if=cooldown.bestial_wrath.remains>3	
					if (WoW.CanCast("Dire Beast") && WoW.Talent(2) != 2 && !WoW.IsSpellOnCooldown ("Dire Beast") && WoW.SpellCooldownTimeRemaining("Bestial Wrath") > 300 && WoW.IsSpellInRange("Cobra Shot"))
                    {
                        WoW.CastSpell("Dire Beast");
                        return;
                    }									
//dire_frenzy,if=(cooldown.bestial_wrath.remains>6&(!equipped.the_mantle_of_command|pet.cat.buff.dire_frenzy.remains<=gcd.max*1.2))
					if (WoW.CanCast("Dire Frenzy") && (WoW.SpellCooldownTimeRemaining("Bestial Wrath") >600 &&(WoW.Legendary(1) != 3 || WoW.PetBuffTimeRemaining("Dire Frenzy") <= 70)) && WoW.Talent(2) == 2 && WoW.IsSpellInRange("Cobra Shot"))
                    {
                        WoW.CastSpell("Dire Frenzy");
                        return;
                    }
//|(charges>=2&focus.deficit>=25+talent.dire_stable.enabled*12)|target.time_to_die<9
					if (WoW.CanCast("Dire Frenzy") && WoW.PlayerSpellCharges("Dire Frenzy") >=2 && WoW.Focus <= 95 && WoW.Talent(2) == 2 && WoW.IsSpellInRange("Cobra Shot"))
                    {
                        WoW.CastSpell("Dire Frenzy");
                        return;
                    }
					if (WoW.CanCast("Dire Frenzy") && WoW.PlayerSpellCharges("Dire Frenzy") >=2 && WoW.Focus <= 83 && WoW.Talent(2) == 2 && WoW.Talent(1) == 3 && WoW.IsSpellInRange("Cobra Shot"))
                    {
                        WoW.CastSpell("Dire Frenzy");
                        return;
                    }	
//aspect_of_the_wild,if=buff.bestial_wrath.up|target.time_to_die<12	
					if (WoW.CanCast("Aspect of the Wild")&& UseCooldowns && WoW.PlayerHasBuff("Bestial Wrath") && WoW.IsSpellInRange("Cobra Shot"))
                    {
                        WoW.CastSpell("Aspect of the Wild");
                        return;
                    }	
//titans_thunder,if=talent.dire_frenzy.enabled|cooldown.dire_beast.remains>=3|(buff.bestial_wrath.up&pet.dire_beast.active)		
					if (WoW.CanCast("Titan's Thunder") && (WoW.Talent(2) == 2 || (WoW.Talent(2) != 2 && WoW.SpellCooldownTimeRemaining("Dire Beast") > 300) || (WoW.Talent(2) != 2 && WoW.PlayerHasBuff("Bestial Wrath") && WoW.PlayerHasBuff ("Dire Beast"))) && WoW.IsSpellInRange("Cobra Shot"))
                    {
                        WoW.CastSpell("Titan's Thunder");
                        return;
                    }	
//bestial_wrath
					if (WoW.CanCast("Bestial Wrath") &&  WoW.IsSpellInRange("Cobra Shot"))
                    {
                        WoW.CastSpell("Bestial Wrath");
                        return;
                    }
//kill_command
					if (WoW.CanCast("Kill Command") &&  WoW.IsSpellInRange("Cobra Shot"))
                    {
                        WoW.CastSpell("Kill Command");
                        return;
                    }					
//cobra_shot,if=(cooldown.kill_command.remains>focus.time_to_max&cooldown.bestial_wrath.remains>focus.time_to_max)|(buff.bestial_wrath.up&focus.regen*cooldown.kill_command.remains>30)|target.time_to_die<cooldown.kill_command.remains					
					if (WoW.CanCast("Cobra Shot") && WoW.Focus > 32&& (WoW.SpellCooldownTimeRemaining("Kill Command") > FocusTimetoMax && WoW.SpellCooldownTimeRemaining("Bestial Wrath") > FocusTimetoMax) 
						
						&& WoW.IsSpellInRange("Cobra Shot"))
                    {	
                        WoW.CastSpell("Cobra Shot");
                        return;
                    }
					if (WoW.CanCast("Cobra Shot") && WoW.Focus > 32 && WoW.PlayerHasBuff("Bestial Wrath") && ((FocusRegen*WoW.SpellCooldownTimeRemaining("Kill Command")) > 300) && WoW.IsSpellInRange("Cobra Shot"))
                    {				
                        WoW.CastSpell("Cobra Shot");
                        return;
                    }
                }
            }

            if (combatRoutine.Type == RotationType.AOE)
            {
	
				if (WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsInCombat)
                {
																					
					if (WoW.CanCast("Kil'jaeden's Burning Wish") && KilJaeden && !WoW.ItemOnCooldown("Kil'jaeden's Burning Wish") && !WoW.IsSpellOnCooldown("Kil'jaeden's Burning Wish"))  
                    {
                        WoW.CastSpell("Kil'jaeden's Burning Wish");
                        return;
                    }	
					if (WoW.CanCast("Volley") 
						&& !WoW.PlayerHasBuff("Volley")
						&& WoW.Talent(6) == 3)
                    {
                        WoW.CastSpell("Volley");
                        return;
                    }
					if (WoW.CanCast("Berserking") 
						
						&& !WoW.IsSpellOnCooldown ("Berserking")
						&& WoW.PlayerRace == "Troll")
                    {
                        WoW.CastSpell("Berserking");
                        return;
                    }	
					if (WoW.CanCast("Arcane Torrent")  
						&& WoW.PlayerRace == "BloodElf"
						&& WoW.Focus <= 85)
                    {
                        WoW.CastSpell("Arcane Torrent");
                        return;
                    }	
					if (WoW.CanCast("Blood Fury") 
						
						&& !WoW.IsSpellOnCooldown ("Blood Fury")
						&& WoW.PlayerRace == "Orc")
                    {
                        WoW.CastSpell("Blood Fury");
                        return;
                    }					
                    if (WoW.CanCast("Barrage") 
						&& WoW.Talent(6) == 2 
						&& !WoW.IsSpellOnCooldown("Barrage") 
						&& WoW.IsSpellInRange("Cobra Shot") 
						&& WoW.Focus >= 60)
                    {
                        WoW.CastSpell("Barrage");
                        return;
                    }	
					if (WoW.CanCast("Chimaera Shot") 
						&& WoW.Focus <90
						&& WoW.IsSpellOnCooldown("Dire Frenzy")
						&& WoW.IsSpellOnCooldown("Kill Command")						
						&& WoW.CanCast("Chimaera Shot")
						&& WoW.Talent(2) == 3)
						{
                        WoW.CastSpell("Chimaera Shot");
                        return;
						}					
					if (WoW.CanCast("A Murder of Crows") 
						&& WoW.Talent(6) == 1
						&& WoW.Focus >= 46-FocusRegen 
						&& WoW.PetBuffTimeRemaining("Beast Cleave") > GCD
						&& WoW.IsSpellInRange("Cobra Shot")
						&& !WoW.IsSpellOnCooldown("A Murder of Crows")	)
                    {
                        WoW.CastSpell("A Murder of Crows");
                        return;
                    }
					if (WoW.CanCast("Multi-Shot") 
						&& WoW.Focus >= 40
						&& !WoW.PetHasBuff("Beast Cleave") 
						&& WoW.IsSpellInRange("Multi-Shot"))
                    {
                        WoW.CastSpell("Multi-Shot");                        
                        return;
                    }
                    if (WoW.CanCast("Multi-Shot") 
						&& WoW.Focus >= 40 
						&& WoW.PetHasBuff("Beast Cleave") 
						&& WoW.PetBuffTimeRemaining("Beast Cleave") <= 70
						&& WoW.IsSpellInRange("Multi-Shot"))
                    {
                        WoW.CastSpell("Multi-Shot");                        
                        return;
                    }					
//	stampede,if=buff.bloodlust.up|buff.bestial_wrath.up|cooldown.bestial_wrath.remains<=2|target.time_to_die<=14	
					if (WoW.CanCast("Stampede") && WoW.Talent(7) == 1 && WoW.IsSpellInRange("Cobra Shot") && ((WoW.PlayerHasBuff("Bestial Wrath")) || (WoW.SpellCooldownTimeRemaining("Bestial Wrath") <=2))						
						&& !WoW.PlayerHasBuff("AspectoftheTurtle")
						&& !WoW.IsSpellOnCooldown("Stampede")) 
                    {
                        WoW.CastSpell("Stampede");

                        return;
                    }				
//dire_beast,if=cooldown.bestial_wrath.remains>3	
					if (WoW.CanCast("Dire Beast") && WoW.Talent(2) != 2 && !WoW.IsSpellOnCooldown ("Dire Beast") && WoW.SpellCooldownTimeRemaining("Bestial Wrath") > 300 && WoW.IsSpellInRange("Cobra Shot"))
                    {
                        WoW.CastSpell("Dire Beast");
                        return;
                    }									
//dire_frenzy,if=(cooldown.bestial_wrath.remains>6&(!equipped.the_mantle_of_command|pet.cat.buff.dire_frenzy.remains<=gcd.max*1.2))
					if (WoW.CanCast("Dire Frenzy") && (WoW.SpellCooldownTimeRemaining("Bestial Wrath") >600 &&(WoW.Legendary(1) != 3 || WoW.PetBuffTimeRemaining("Dire Frenzy") <= 70)) && WoW.Talent(2) == 2 && WoW.IsSpellInRange("Cobra Shot"))
                    {
                        WoW.CastSpell("Dire Frenzy");
                        return;
                    }
//|(charges>=2&focus.deficit>=25+talent.dire_stable.enabled*12)|target.time_to_die<9
					if (WoW.CanCast("Dire Frenzy") && WoW.PlayerSpellCharges("Dire Frenzy") >=2 && WoW.Focus <= 95 && WoW.Talent(2) == 2 && WoW.IsSpellInRange("Cobra Shot"))
                    {
                        WoW.CastSpell("Dire Frenzy");
                        return;
                    }
					if (WoW.CanCast("Dire Frenzy") && WoW.PlayerSpellCharges("Dire Frenzy") >=2 && WoW.Focus <= 83 && WoW.Talent(2) == 2 && WoW.Talent(1) == 3 && WoW.IsSpellInRange("Cobra Shot"))
                    {
                        WoW.CastSpell("Dire Frenzy");
                        return;
                    }	
//aspect_of_the_wild,if=buff.bestial_wrath.up|target.time_to_die<12	
					if (WoW.CanCast("Aspect of the Wild") && UseCooldowns && WoW.PetHasBuff("Beast Cleave") && WoW.PetBuffTimeRemaining("Beast Cleave") > GCD && WoW.PlayerHasBuff("Bestial Wrath") && WoW.IsSpellInRange("Cobra Shot"))
                    {
                        WoW.CastSpell("Aspect of the Wild");
                        return;
                    }	
//titans_thunder,if=talent.dire_frenzy.enabled|cooldown.dire_beast.remains>=3|(buff.bestial_wrath.up&pet.dire_beast.active)		
					if (WoW.CanCast("Titan's Thunder") && WoW.PetHasBuff("Beast Cleave") && WoW.PetBuffTimeRemaining("Beast Cleave") > GCD && (WoW.Talent(2) == 2 || (WoW.Talent(2) != 2 && WoW.SpellCooldownTimeRemaining("Dire Beast") > 300) || (WoW.Talent(2) != 2 && WoW.PlayerHasBuff("Bestial Wrath") && WoW.PlayerHasBuff ("Dire Beast"))) && WoW.IsSpellInRange("Cobra Shot"))
                    {
                        WoW.CastSpell("Titan's Thunder");
                        return;
                    }	
//bestial_wrath
					if (WoW.CanCast("Bestial Wrath") && WoW.PetHasBuff("Beast Cleave") && WoW.PetBuffTimeRemaining("Beast Cleave") > GCD  &&  WoW.IsSpellInRange("Cobra Shot"))
                    {
                        WoW.CastSpell("Bestial Wrath");
                        return;
                    }
//kill_command
					if (WoW.CanCast("Kill Command") && WoW.Focus >= 70-FocusRegen && WoW.PetHasBuff("Beast Cleave") && WoW.PetBuffTimeRemaining("Beast Cleave") > GCD &&  WoW.IsSpellInRange("Cobra Shot"))
                    {
                        WoW.CastSpell("Kill Command");
                        return;
                    }					
//cobra_shot,if=(cooldown.kill_command.remains>focus.time_to_max&cooldown.bestial_wrath.remains>focus.time_to_max)|(buff.bestial_wrath.up&focus.regen*cooldown.kill_command.remains>30)|target.time_to_die<cooldown.kill_command.remains					
					if (WoW.CanCast("Cobra Shot") && WoW.PetHasBuff("Beast Cleave") && WoW.PetBuffTimeRemaining("Beast Cleave") > GCD && WoW.Focus > 72-FocusRegen && (WoW.SpellCooldownTimeRemaining("Kill Command") > FocusTimetoMax && WoW.SpellCooldownTimeRemaining("Bestial Wrath") > FocusTimetoMax) 
						
						&& WoW.IsSpellInRange("Cobra Shot"))
                    {	
                        WoW.CastSpell("Cobra Shot");
                        return;
                    }
					if (WoW.CanCast("Cobra Shot") && WoW.PetHasBuff("Beast Cleave") && WoW.PetBuffTimeRemaining("Beast Cleave") > GCD && WoW.Focus > 72-FocusRegen && WoW.PlayerHasBuff("Bestial Wrath") && ((FocusRegen*WoW.SpellCooldownTimeRemaining("Kill Command")) > 300) && WoW.IsSpellInRange("Cobra Shot"))
                    {				
                        WoW.CastSpell("Cobra Shot");
                        return;
                    }

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
Spell,83245,Wolf,F1
Spell,120679,Dire Beast,D1
Spell,217200,Dire Frenzy,D1
Spell,193455,Cobra Shot,D3
Spell,2643,Multi-Shot,D4
Spell,34026,Kill Command,D2
Spell,19574,Bestial Wrath,D8
Spell,131894,A Murder of Crows,D5
Spell,120360,Barrage,D6
Spell,147362,Counter Shot,D0
Spell,193530,Aspect of the Wild,D9
Spell,20572,Blood Fury,F9
Spell,207068,Titan's Thunder,D7
Spell,5116,Concussive,None
Spell,109304,Exhilaration,V
Spell,186265,AspectoftheTurtle,G
Spell,5384,FeignDeath,F2
Spell,127834,Ancient Healing Potion,F5
Spell,143940,Silkweave Bandage,None
Spell,55709,Phoenix,F6
Spell,5512,Healthstone,F7
Spell,982,Revive Pet,X
Spell,136,Heal Pet,X
Spell,142117,Potion Power,F10
Spell,144259,Kil'jaeden's Burning Wish,F4
Spell,194386,Volley,F
Spell,80483,Arcane Torrent,F3
Spell,53209,Chimaera Shot,F8
Spell,26297,Berserking,F9
Spell,201430,Stampede,C
Aura,217200,Dire Frenzy
Aura,186265,AspectoftheTurtle
Aura,136,Heal Pet
Aura,11196,Bandaged
Aura,234143,Temptation
Aura,2825,Bloodlust
Aura,80353,Time Warp
Aura,90355,Ancient Hysteria
Aura,160452,Netherwinds
Aura,146613,Drums
Aura,32182,Heroism
Aura,229206,Potion Power
Aura,19574,Bestial Wrath
Aura,118455,Beast Cleave
Aura,193530,Aspect of the Wild
Aura,194386,Volley
Item,144259,Kil'jaeden's Burning Wish

*/