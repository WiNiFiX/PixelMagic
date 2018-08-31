// winifix@gmail.com
// ReSharper disable UnusedMember.Global
// ReSharper disable ConvertPropertyToExpressionBody

using System;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using PixelMagic.Helpers;

namespace PixelMagic.Rotation
{
    public class HunterBMG : CombatRoutine
    {
        private readonly Stopwatch interruptwatch = new Stopwatch();
        private readonly Stopwatch multishotwatch = new Stopwatch();

        private CheckBox BarrageBox;
        private CheckBox CounterShotBox;
        private CheckBox CrowBox;
        private CheckBox DeathBox;
        private CheckBox dpscooldownsBox;
        private CheckBox ExhilBox;
        private readonly bool FourTarget = false;
        private CheckBox TurtleBox;

        public override Form SettingsForm { get; set; }

        public override string Name
        {
            get { return "HunterBeastmastery"; }
        }

        public override string Class
        {
            get { return "Hunter"; }
        }




        public static bool Barrage
        {
            get
            {
                var Barrage = ConfigFile.ReadValue("HunterBeastmastery", "Barrage").Trim();

                return Barrage != "" && Convert.ToBoolean(Barrage);
            }
            set { ConfigFile.WriteValue("HunterBeastmastery", "Barrage", value.ToString()); }
        }

        private static bool Crow
        {
            get
            {
                var Crow = ConfigFile.ReadValue("HunterBeastmastery", "Crow").Trim();

                return Crow != "" && Convert.ToBoolean(Crow);
            }
            set { ConfigFile.WriteValue("HunterBeastmastery", "Crow", value.ToString()); }
        }

        private static bool Death
        {
            get
            {
                var Death = ConfigFile.ReadValue("HunterBeastmastery", "Death").Trim();

                return Death != "" && Convert.ToBoolean(Death);
            }
            set { ConfigFile.WriteValue("HunterBeastmastery", "Death", value.ToString()); }
        }

        private static bool Exhil
        {
            get
            {
                var Exhil = ConfigFile.ReadValue("HHunterBeastmastery", "Exhil").Trim();

                return Exhil != "" && Convert.ToBoolean(Exhil);
            }
            set { ConfigFile.WriteValue("HunterBeastmastery", "Exhil", value.ToString()); }
        }

        private static bool dpscooldowns
        {
            get
            {
                var dpscooldowns = ConfigFile.ReadValue("HunterBeastmastery", "dpscooldowns").Trim();

                return dpscooldowns != "" && Convert.ToBoolean(dpscooldowns);
            }
            set { ConfigFile.WriteValue("HunterBeastmastery", "dpscooldowns", value.ToString()); }
        }

        private static bool Turtle
        {
            get
            {
                var Turtle = ConfigFile.ReadValue("HunterBeastmastery", "Turtle").Trim();

                return Turtle != "" && Convert.ToBoolean(Turtle);
            }
            set { ConfigFile.WriteValue("HunterBeastmastery", "Turtle", value.ToString()); }
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

        public override void Initialize()
        {
            Log.Write("Welcome to Hunter Beastmastery by Goat", Color.Green);
            
            SettingsForm = new Form {Text = "Settings", StartPosition = FormStartPosition.CenterScreen, Width = 480, Height = 300, ShowIcon = false};

            var picBox = new PictureBox {Left = 0, Top = 0, Width = 800, Height = 100};
            SettingsForm.Controls.Add(picBox);

            var lblBarrageText = new Label //Barrage LABEL - 114
            {Text = "Barrage", Size = new Size(81, 13), Left = 12, Top = 114};
            SettingsForm.Controls.Add(lblBarrageText); //Barrage TEXT - 114

            BarrageBox = new CheckBox {Checked = Barrage, TabIndex = 2, Size = new Size(15, 14), Left = 115, Top = 114};
            SettingsForm.Controls.Add(BarrageBox); //Barrage BOX	

            var lbldpscooldownsText = new Label //Trueshot LABEL - 129
            {Text = "dpscooldowns", Size = new Size(81, 13), Left = 12, Top = 129};
            SettingsForm.Controls.Add(lbldpscooldownsText); //Trueshot TEXT - 129

            dpscooldownsBox = new CheckBox {Checked = dpscooldowns, TabIndex = 4, Size = new Size(15, 14), Left = 115, Top = 129};
            SettingsForm.Controls.Add(dpscooldownsBox); //Trueshot BOX	

            var lblCrowText = new Label // Crow LABEL
            {Text = "Crow", Size = new Size(81, 13), Left = 12, Top = 145};
            SettingsForm.Controls.Add(lblCrowText); //Crow TEXT

            CrowBox = new CheckBox {Checked = Crow, TabIndex = 6, Size = new Size(15, 14), Left = 115, Top = 145};
            SettingsForm.Controls.Add(CrowBox); // Crow BOX

            var lblExhilText = new Label // Exhil LABEL
            {Text = "Exhil", Size = new Size(81, 13), Left = 12, Top = 161};
            SettingsForm.Controls.Add(lblExhilText); //EXHIL TEXT

            ExhilBox = new CheckBox {Checked = Exhil, TabIndex = 8, Size = new Size(15, 14), Left = 115, Top = 161};
            SettingsForm.Controls.Add(ExhilBox); //Exhil Box

            var lblTurtleText = new Label //Turtle label
            {Text = "Turtle", Size = new Size(81, 13), Left = 12, Top = 178};
            SettingsForm.Controls.Add(lblTurtleText); //turtle text

            TurtleBox = new CheckBox {Checked = Turtle, TabIndex = 10, Size = new Size(15, 14), Left = 115, Top = 178};
            SettingsForm.Controls.Add(TurtleBox); //turtle box

            var lblDeathText = new Label //Death label
            {Text = "Death", Size = new Size(81, 13), Left = 12, Top = 194};
            SettingsForm.Controls.Add(lblDeathText); //death text

            DeathBox = new CheckBox {Checked = Death, TabIndex = 12, Size = new Size(15, 14), Left = 115, Top = 194};
            SettingsForm.Controls.Add(DeathBox); //death box	

            var lblCounterShotText = new Label //CounterShot label
            {Text = "CounterShot", Size = new Size(81, 13), Left = 12, Top = 210};
            SettingsForm.Controls.Add(lblCounterShotText); //CounterShot text

            CounterShotBox = new CheckBox {Checked = CounterShot, TabIndex = 12, Size = new Size(15, 14), Left = 115, Top = 210};
            SettingsForm.Controls.Add(CounterShotBox); //CounterShot box			

            var cmdSave = new Button {Text = "Save", Width = 65, Height = 25, Left = 332, Top = 190, Size = new Size(120, 31)};

            BarrageBox.Checked = Barrage;
            CrowBox.Checked = Crow;
            DeathBox.Checked = Death;
            ExhilBox.Checked = Exhil;
            dpscooldownsBox.Checked = dpscooldowns;
            TurtleBox.Checked = Turtle;
            CounterShotBox.Checked = CounterShot;

            cmdSave.Click += CmdSave_Click;
            BarrageBox.CheckedChanged += Barrage_Click;
            CrowBox.CheckedChanged += Crow_Click;
            DeathBox.CheckedChanged += Death_Click;
            TurtleBox.CheckedChanged += Turtle_Click;
            ExhilBox.CheckedChanged += Exhil_Click;
            dpscooldownsBox.CheckedChanged += dpscooldowns_Click;
            CounterShotBox.CheckedChanged += CounterShot_Click;

            SettingsForm.Controls.Add(cmdSave);
            lblBarrageText.BringToFront();
            lblCrowText.BringToFront();
            lblExhilText.BringToFront();
            lbldpscooldownsText.BringToFront();
            lblDeathText.BringToFront();
            lblTurtleText.BringToFront();
            lblCounterShotText.BringToFront();

            Log.Write("Barrage = " + Barrage);
            Log.Write("Crow = " + Crow);
            Log.Write("Exhil = " + Exhil);
            Log.Write("Death = " + Death);
            Log.Write("Turtle = " + Turtle);
            Log.Write("dpscooldowns = " + dpscooldowns);
            Log.Write("CounterShot = " + CounterShot);
        }

        private void CmdSave_Click(object sender, EventArgs e)
        {
            Barrage = BarrageBox.Checked;
            Crow = CrowBox.Checked;
            Exhil = ExhilBox.Checked;
            Death = DeathBox.Checked;
            Turtle = TurtleBox.Checked;
            dpscooldowns = dpscooldownsBox.Checked;
            CounterShot = CounterShotBox.Checked;
            MessageBox.Show("Settings saved", "PixelMagic", MessageBoxButtons.OK, MessageBoxIcon.Information);
            SettingsForm.Close();
        }

        private void Barrage_Click(object sender, EventArgs e)
        {
            Barrage = BarrageBox.Checked;
        }

        private void Crow_Click(object sender, EventArgs e)
        {
            Crow = CrowBox.Checked;
        }

        private void Death_Click(object sender, EventArgs e)
        {
            Death = DeathBox.Checked;
        }

        private void Exhil_Click(object sender, EventArgs e)
        {
            Exhil = ExhilBox.Checked;
        }

        private void Turtle_Click(object sender, EventArgs e)
        {
            Turtle = TurtleBox.Checked;
        }

        private void dpscooldowns_Click(object sender, EventArgs e)
        {
            dpscooldowns = dpscooldownsBox.Checked;
        }

        private void CounterShot_Click(object sender, EventArgs e)
        {
            CounterShot = CounterShotBox.Checked;
        }

        public override void Stop()
        {
        }


        public override void Pulse()
        {
            if (combatRoutine.Type == RotationType.SingleTarget)  // Do Single Target Stuff here
            {
	
                    if (WoW.CanCast("Death") && WoW.HealthPercent < 40 && Death && !WoW.IsSpellOnCooldown("Death") && WoW.HealthPercent != 0)
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
											
                if (WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsInCombat)
                {	

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
					if (WoW.PetHealthPercent <= 0 && WoW.IsSpellOnCooldown("Phoenix") && WoW.CanCast("Revive Pet") && !WoW.IsMoving)
					{
						WoW.CastSpell("Revive Pet") ;
						return;
					}
					if (WoW.TargetIsCasting && CounterShot && interruptwatch.ElapsedMilliseconds == 0)
                    {
						interruptwatch.Start ();
						Log.WritePixelMagic("interruptwatch started..", Color.Black);	
						return;
					}			
                    if (WoW.CanCast("A Murder of Crows")  && !WoW.IsSpellOnCooldown("A Murder of Crows") && Crow && WoW.IsSpellInRange("A Murder of Crows") && WoW.Focus >= 30  && WoW.SpellCooldownTimeRemaining("Bestial Wrath") >= 2300)
                    {
                        WoW.CastSpell("A Murder of Crows");
                        return;
                    }
					if (WoW.CanCast("A Murder of Crows") && WoW.Focus >= 25 && WoW.IsSpellInRange("A Murder of Crows") && WoW.PlayerHasBuff("Bestial Wrath"))
                    {
                        WoW.CastSpell("A Murder of Crows");
                        return;
                    }
                    if (WoW.TargetIsCasting && CounterShot && interruptwatch.ElapsedMilliseconds > 800)
                    {
                        if (WoW.CanCast("Counter Shot") && !WoW.IsSpellOnCooldown("Counter Shot") && CounterShot && !WoW.PlayerIsChanneling && !WoW.WasLastCasted("Counter Shot"))
                        {
                            WoW.CastSpell("Counter Shot");
                            interruptwatch.Reset();
							Log.WritePixelMagic("interruptwatch reset!", Color.Black);							
                            interruptwatch.Start();
							Log.WritePixelMagic("interruptwatch started...", Color.Black);							
                            return;
                        }	
					}	
					if (WoW.CanCast("Bestial Wrath") && WoW.Focus >= 119 && WoW.IsSpellInRange("Cobra Shot") && !WoW.PlayerHasBuff("Turtle") && !WoW.IsSpellOnCooldown("Bestial Wrath"))
                    {
                        WoW.CastSpell("Bestial Wrath");
                        return;
                    }
					if (WoW.CanCast("Bestial Wrath") && WoW.Focus >= 107 && WoW.IsSpellInRange("Cobra Shot") && !WoW.PlayerHasBuff("Turtle") && !WoW.IsSpellOnCooldown("Bestial Wrath") && WoW.SpellCooldownTimeRemaining("Kill Command") <= 290)
                    {
                        WoW.CastSpell("Bestial Wrath");
                        return;
                    }
					if (WoW.CanCast("Aspect of the Wild") && !WoW.IsSpellOnCooldown("Aspect of the Wild")  && WoW.PlayerHasBuff("Bestial Wrath") && WoW.PlayerBuffTimeRemaining("Bestial Wrath") >= 1000 && (dpscooldowns || WoW.PlayerHasBuff("Bloodlust") || WoW.PlayerHasBuff("Ancient Hysteria") || WoW.PlayerHasBuff("Netherwinds") || WoW.PlayerHasBuff("Drums") || WoW.PlayerHasBuff("Heroism") || WoW.PlayerHasBuff("Time Warp") )) 
                    {
                        WoW.CastSpell("Aspect of the Wild");
						WoW.CastSpell("Blood Fury");
						return;
                    }
					if (WoW.CanCast("Kill Command") && WoW.Focus >= 100 && WoW.IsSpellInRange("Kill Command"))
                    {
                        WoW.CastSpell("Kill Command");
                        return;
                    }
                    if (WoW.CanCast("Dire Beast") && WoW.IsSpellInRange("Dire Beast") && !WoW.IsSpellOnCooldown("Dire Beast") && WoW.SpellCooldownTimeRemaining("Bestial Wrath") >= 390)
                    {
                        WoW.CastSpell("Dire Beast");
						if (WoW.CanCast("Titan's Thunder") && !WoW.IsSpellOnCooldown("Titan's Thunder"))
						{
                        WoW.CastSpell("Titan's Thunder");
                        return;
						}
                    }
                    if (WoW.CanCast("Kill Command") && WoW.Focus >= 30 && WoW.IsSpellInRange("Kill Command"))
                    {
                        WoW.CastSpell("Kill Command");
                        return;
                    }
					if (WoW.CanCast("Kill Command") && WoW.Focus >= 26 && WoW.IsSpellInRange("Kill Command") && WoW.PlayerHasBuff("Bestial Wrath"))
                    {
                        WoW.CastSpell("Kill Command");
                        return;
                    }
					if (WoW.CanCast("Cobra Shot") && (WoW.Focus >= 28) && WoW.IsSpellInRange("Cobra Shot") && WoW.PlayerHasBuff("Bestial Wrath") && (WoW.SpellCooldownTimeRemaining("Kill Command") >= 300))
                    {
                        WoW.CastSpell("Cobra Shot");
                        return;
                    }					
					if (WoW.CanCast("Cobra Shot") && (WoW.Focus >= 91) && WoW.IsSpellInRange("Cobra Shot") && (WoW.SpellCooldownTimeRemaining("Bestial Wrath") >= 100) && (WoW.SpellCooldownTimeRemaining("Kill Command") >= 150) && (WoW.SpellCooldownTimeRemaining("A Murder of Crows") >= 150))
                    {
                        WoW.CastSpell("Cobra Shot");
                        return;
                    }
					if (WoW.CanCast("Cobra Shot") && (WoW.Focus >= 105) && WoW.IsSpellInRange("Cobra Shot") && WoW.SpellCooldownTimeRemaining("Kill Command") >= 140)
                    {
                        WoW.CastSpell("Cobra Shot");
                        return;
                    }
                }
            }
            if (combatRoutine.Type == RotationType.AOE)
            {
				
                    if (WoW.CanCast("Death") && WoW.HealthPercent < 40 && Death && !WoW.IsSpellOnCooldown("Death") && WoW.HealthPercent != 0)
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
					
                if (WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsInCombat)
                {
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
					if (WoW.PetHealthPercent <= 0 && WoW.IsSpellOnCooldown("Phoenix") && WoW.CanCast("Revive Pet") && !WoW.IsMoving)
					{
						WoW.CastSpell("Revive Pet") ;
						return;
					}		
                    if (WoW.TargetIsCasting && CounterShot && interruptwatch.ElapsedMilliseconds == 0)
                    {
						interruptwatch.Start ();
						Log.WritePixelMagic("interruptwatch started..", Color.Black);	
						return;
					}															
                    if (WoW.TargetIsCasting && CounterShot && interruptwatch.ElapsedMilliseconds > 800)
                    {
                        if (WoW.CanCast("Counter Shot") && !WoW.IsSpellOnCooldown("Counter Shot") && CounterShot && !WoW.PlayerIsChanneling && !WoW.WasLastCasted("Counter Shot"))
                        {
                            WoW.CastSpell("Counter Shot");
                            interruptwatch.Reset();
							Log.WritePixelMagic("interruptwatch reset!", Color.Black);							
                            interruptwatch.Start();
							Log.WritePixelMagic("interruptwatch started...", Color.Black);							
                            return;
                        }	
					}
					if (WoW.CanCast("Multi-Shot") && (WoW.Focus >= 40) && !WoW.PetHasBuff("Beast Cleave") && WoW.IsSpellInRange("Multi-Shot"))
                    {
                        WoW.CastSpell("Multi-Shot");                        
                        return;
                    }
                    if (WoW.CanCast("Multi-Shot") && (WoW.Focus >= 34) && WoW.PlayerHasBuff("Bestial Wrath") && WoW.PetBuffTimeRemaining("Beast Cleave") <= 70 && WoW.IsSpellInRange("Multi-Shot"))
                    {
                        WoW.CastSpell("Multi-Shot");                        
                        return;
                    }
                    if (WoW.CanCast("Multi-Shot") && (WoW.Focus >= 40) && WoW.PetBuffTimeRemaining("Beast Cleave") <= 70 && WoW.IsSpellInRange("Multi-Shot") && !WoW.CanCast("Bestial Wrath"))
                    {
                        WoW.CastSpell("Multi-Shot");                        
                        return;
                    }
					if (WoW.CanCast("A Murder of Crows") && !WoW.IsSpellOnCooldown("A Murder of Crows") && Crow && WoW.IsSpellInRange("A Murder of Crows") && WoW.PetHasBuff("Beast Cleave") && WoW.PetBuffTimeRemaining("Beast Cleave") >= 1 && WoW.Focus >= 50 && !(DetectKeyPress.GetKeyState(0x5A) < 0) && !(WoW.SpellCooldownTimeRemaining("Bestial Wrath") <= 2300))
                    {
                        WoW.CastSpell("A Murder of Crows");
                        return;
                    }
                    if (WoW.CanCast("Barrage") && Barrage && WoW.IsSpellInRange("Barrage") && WoW.Focus >= 60)
                    {
                        WoW.CastSpell("Barrage");
                        return;
                    }					
					if (WoW.CanCast("Bestial Wrath") && WoW.IsSpellInRange("Cobra Shot") && !WoW.PlayerHasBuff("Turtle") && !WoW.IsSpellOnCooldown("Bestial Wrath"))
                    {
                        WoW.CastSpell("Bestial Wrath");
                        return;
                    }
					if (WoW.CanCast("Aspect of the Wild") && !WoW.IsSpellOnCooldown("Aspect of the Wild")  && WoW.PlayerHasBuff("Bestial Wrath") && WoW.PlayerBuffTimeRemaining("Bestial Wrath") >= 1000 && (dpscooldowns || WoW.PlayerHasBuff("Bloodlust") || WoW.PlayerHasBuff("Ancient Hysteria") || WoW.PlayerHasBuff("Netherwinds") || WoW.PlayerHasBuff("Drums") || WoW.PlayerHasBuff("Heroism") || WoW.PlayerHasBuff("Time Warp") )) 
                    {
                        WoW.CastSpell("Aspect of the Wild");
						WoW.CastSpell("Blood Fury");
						return;
                    }
					if (WoW.CanCast("Dire Beast") && WoW.IsSpellInRange("Dire Beast") && !WoW.IsSpellOnCooldown("Dire Beast") && !(WoW.SpellCooldownTimeRemaining("Bestial Wrath") <= 390))
                    {
                        WoW.CastSpell("Dire Beast");
						if (WoW.CanCast("Titan's Thunder") && !WoW.IsSpellOnCooldown("Titan's Thunder"))
						{
                        WoW.CastSpell("Titan's Thunder");
                        return;
						}
                    }		//Z Key Multi Shot Spam			
					if (WoW.CanCast("Multi-Shot") && (WoW.Focus >= 40) && WoW.IsSpellInRange("Multi-Shot") && (DetectKeyPress.GetKeyState(0x5A) < 0))  //Z key press
					{
                            WoW.CastSpell("Multi-Shot");
                            return;
                    }
                    if (WoW.CanCast("Kill Command") && WoW.Focus >= 55 && WoW.IsSpellInRange("Kill Command") && WoW.PetHasBuff("Beast Cleave") && WoW.PetBuffTimeRemaining("Beast Cleave") >= 130 && !(DetectKeyPress.GetKeyState(0x5A) < 0) && !FourTarget  && !(WoW.SpellCooldownTimeRemaining("Bestial Wrath") <= 200))
                    {
                        WoW.CastSpell("Kill Command");
                        return;
                    }
					if (WoW.CanCast("Kill Command") && WoW.Focus >= 30 && !FourTarget && WoW.IsSpellInRange("Kill Command") && WoW.PetHasBuff("Beast Cleave") && WoW.PetBuffTimeRemaining("Beast Cleave") >= 130 && (WoW.PlayerHasBuff("Aspect of the Wild") || WoW.PlayerHasBuff("Bloodlust") || WoW.PlayerHasBuff("Ancient Hysteria") || WoW.PlayerHasBuff("Drums") || WoW.PlayerHasBuff("Netherwinds")))
                    {
                        WoW.CastSpell("Kill Command");
                        return;
                    }
                    if (WoW.CanCast("Cobra Shot") && (WoW.Focus >= 40) && WoW.IsSpellInRange("Cobra Shot") && WoW.PetHasBuff("Beast Cleave") && WoW.PetBuffTimeRemaining("Beast Cleave") >= 130 && WoW.PlayerHasBuff("Bestial Wrath") && (WoW.SpellCooldownTimeRemaining("Kill Command") >= 300))
                    {
                        WoW.CastSpell("Cobra Shot");
                        return;
                    }
					if (WoW.CanCast("Cobra Shot") && (WoW.Focus >= 110) && WoW.IsSpellInRange("Cobra Shot") && WoW.PetHasBuff("Beast Cleave") && WoW.PetBuffTimeRemaining("Beast Cleave") >= 130 && WoW.SpellCooldownTimeRemaining("Bestial Wrath") >= 100)
                    {
                        WoW.CastSpell("Cobra Shot");
                        return;
                    }
				}
            }
            if (combatRoutine.Type == RotationType.SingleTargetCleave)
            {
                if (WoW.CanCast("Death") && WoW.HealthPercent < 40 && Death && !WoW.IsSpellOnCooldown("Death") && WoW.HealthPercent != 0)
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

                if (WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsInCombat)
                {
                    if (!WoW.HasPet && WoW.CanCast("Wolf"))
                    {
                        WoW.CastSpell("Wolf");
                        return;
                    }
                    if (WoW.PetHealthPercent <= 0 && WoW.CanCast("Phoenix"))
                    {
                        WoW.CastSpell("Phoenix");
                        return;
                    }
                    if (WoW.PetHealthPercent <= 0 && WoW.IsSpellOnCooldown("Phoenix") && WoW.CanCast("Revive Pet") && !WoW.IsMoving)
                    {
                        WoW.CastSpell("Revive Pet");
                        return;
                    }
                    if (WoW.HasTarget && WoW.TargetIsEnemy && WoW.IsInCombat && multishotwatch.ElapsedMilliseconds == 0)
                    {
                        multishotwatch.Start();
                        Log.WritePixelMagic("multishotwatch started..", Color.Black);
                        return;
                    }
                    if (WoW.TargetIsCasting && CounterShot && interruptwatch.ElapsedMilliseconds == 0)
                    {
                        interruptwatch.Start();
                        Log.WritePixelMagic("interruptwatch started..", Color.Black);
                        return;
                    }
                    if ((multishotwatch.ElapsedMilliseconds > 3000) && !WoW.IsSpellOnCooldown("Multi-Shot") && !WoW.WasLastCasted("Multi-Shot") && WoW.Focus >= 40)
                    {
                        WoW.CastSpell("Multi-Shot");
                        multishotwatch.Reset();
                        multishotwatch.Start();
                        return;
                    }
                    if (WoW.TargetIsCasting && CounterShot && interruptwatch.ElapsedMilliseconds > 800)
                    {
                        if (WoW.CanCast("Counter Shot") && !WoW.IsSpellOnCooldown("Counter Shot") && CounterShot && !WoW.PlayerIsChanneling && !WoW.WasLastCasted("Counter Shot"))
                        {
                            WoW.CastSpell("Counter Shot");
                            interruptwatch.Reset();
                            Log.WritePixelMagic("interruptwatch reset!", Color.Black);
                            interruptwatch.Start();
                            Log.WritePixelMagic("interruptwatch started...", Color.Black);
                            return;
                        }
                    }
                    if (WoW.CanCast("A Murder of Crows") && !WoW.IsSpellOnCooldown("A Murder of Crows") && Crow && WoW.IsSpellInRange("A Murder of Crows") && WoW.Focus >= 30 && !(WoW.SpellCooldownTimeRemaining("Bestial Wrath") <= 10))
                    {
                        WoW.CastSpell("A Murder of Crows");
                        return;
                    }
                    if (WoW.CanCast("Barrage") && Barrage && WoW.IsSpellInRange("Barrage") && WoW.Focus >= 60)
                    {
                        WoW.CastSpell("Barrage");
                        return;
                    }
                    if (WoW.CanCast("Bestial Wrath") && WoW.IsSpellInRange("Cobra Shot") && !WoW.PlayerHasBuff("Turtle") && !WoW.IsSpellOnCooldown("Bestial Wrath"))
                    {
                        WoW.CastSpell("Bestial Wrath");
                        return;
                    }
                    if (WoW.CanCast("Aspect of the Wild") && !WoW.IsSpellOnCooldown("Aspect of the Wild")  && WoW.PlayerHasBuff("Bestial Wrath") && WoW.PlayerBuffTimeRemaining("Bestial Wrath") >= 10 && (dpscooldowns || WoW.PlayerHasBuff("Bloodlust") || WoW.PlayerHasBuff("Ancient Hysteria") || WoW.PlayerHasBuff("Netherwinds") || WoW.PlayerHasBuff("Drums") || WoW.PlayerHasBuff("Heroism") || WoW.PlayerHasBuff("Time Warp") )) 
                    {
                        WoW.CastSpell("Aspect of the Wild");
						WoW.CastSpell("Blood Fury");
						return;
                    }
                    if (WoW.CanCast("Dire Beast") && WoW.IsSpellInRange("Dire Beast") && !WoW.IsSpellOnCooldown("Dire Beast") && !(WoW.SpellCooldownTimeRemaining("Bestial Wrath") <= 3))
                    {
                        WoW.CastSpell("Dire Beast");
                        if (WoW.CanCast("Titan's Thunder") && !WoW.IsSpellOnCooldown("Titan's Thunder"))
                        {
                            WoW.CastSpell("Titan's Thunder");
                            return;
                        }
                    }
                    if (WoW.CanCast("Kill Command") && WoW.Focus >= 30 && WoW.IsSpellInRange("Kill Command") && !(WoW.SpellCooldownTimeRemaining("Bestial Wrath") <= 1))
                    {
                        WoW.CastSpell("Kill Command");
                        return;
                    }
                    if (WoW.CanCast("Multi-Shot") && (WoW.Focus >= 40) && !WoW.PetHasBuff("Beast Cleave") && WoW.IsSpellInRange("Multi-Shot"))
                    {
                        WoW.CastSpell("Multi-Shot");                        
                        return;
                    }
                    if (WoW.CanCast("Multi-Shot") && (WoW.Focus >= 34) && WoW.PlayerHasBuff("Bestial Wrath") && WoW.PetBuffTimeRemaining("Beast Cleave") <= 1 && WoW.IsSpellInRange("Multi-Shot"))
                    {
                        WoW.CastSpell("Multi-Shot");                        
                        return;
                    }
                    if (WoW.CanCast("Multi-Shot") && (WoW.Focus >= 40) && WoW.PetBuffTimeRemaining("Beast Cleave") <= 1 && WoW.IsSpellInRange("Multi-Shot") && !WoW.CanCast("Bestial Wrath"))
                    {
                        WoW.CastSpell("Multi-Shot");                        
                        return;
                    }
                    if (WoW.CanCast("Cobra Shot") && (WoW.Focus >= 34) && WoW.IsSpellInRange("Cobra Shot") && WoW.PetHasBuff("Beast Cleave") && WoW.PetBuffTimeRemaining("Beast Cleave") >= 1 && WoW.PlayerHasBuff("Bestial Wrath") && (WoW.SpellCooldownTimeRemaining("Kill Command") >= 3))
                    {
                        WoW.CastSpell("Cobra Shot");
                        return;
                    }
					if (WoW.CanCast("Cobra Shot") && (WoW.Focus >= 110) && WoW.IsSpellInRange("Cobra Shot") && WoW.PetHasBuff("Beast Cleave") && WoW.PetBuffTimeRemaining("Beast Cleave") >= 1 && WoW.SpellCooldownTimeRemaining("Bestial Wrath") >= 1)
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
AddonName=NPCScan
WoWVersion=Legion - 70300
[SpellBook.db]
Spell,83245,Wolf,F1
Spell,120679,Dire Beast,D1
Spell,217200,Dire Frenzy,D1
Spell,193455,Cobra Shot,D3
Spell,2643,Multi-Shot,D4
Spell,34026,Kill Command,D2
Spell,19574,Bestial Wrath,F
Spell,131894,A Murder of Crows,D5
Spell,120360,Barrage,None
Spell,147362,Counter Shot,R
Spell,193530,Aspect of the Wild,F8
Spell,20572,Blood Fury,F9
Spell,207068,Titan's Thunder,E
Spell,5116,Concussive,None
Spell,109304,Exhil,None
Spell,186265,Turtle,None
Spell,5384,Death,None
Spell,133940,Silkweave Bandage,None
Spell,55709,Phoenix,F6
Spell,5512,Healthstone,F7
Spell,982,Revive Pet,V
Spell,136,Heal Pet,V
Spell,144259,Kil'jaeden's Burning Wish,Q
Aura,186265,Turtle
Aura,11196,Bandaged
Aura,234143,Temptation
Aura,2825,Bloodlust
Aura,80353,Time Warp
Aura,90355,Ancient Hysteria
Aura,160452,Netherwinds
Aura,146613,Drums
Aura,32182,Heroism
Aura,19574,Bestial Wrath
Aura,118455,Beast Cleave
Aura,193530,Aspect of the Wild
Item,144259,Kil'jaeden's Burning Wish
*/
