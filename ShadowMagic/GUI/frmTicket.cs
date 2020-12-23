﻿//////////////////////////////////////////////////
//                                              //
//   See License.txt for Licensing information  //
//                                              //
//////////////////////////////////////////////////

using System;
using System.Drawing;
using System.Windows.Forms;
using ShadowMagic.Helpers;
using System.Diagnostics;

namespace ShadowMagic.GUI
{
    public partial class frmTicket : Form
    {
        private string text;
        private string logText;

        public frmTicket(string text, string logText)
        {
            this.text = text;
            this.logText = logText;
            InitializeComponent();
        }
        
        private void frmTicket_Load(object sender, EventArgs e)
        {
            Ticket.Initialize(richTextBox1, this);

            foreach(string line in text.Split('\n'))
            {
                Ticket.WriteNoTime(line.Replace("\r", "").Replace("\n", ""));                
            }
                        
            Ticket.WriteNoTime("[B]Rotation File Contents[/B]");
            Ticket.WriteNoTime(SpellBook.RotationFileContents, Color.Gray);
            Ticket.WriteNoTime(" ");
            Ticket.WriteNoTime("[B]Log File Contents[/B]");
            Ticket.WriteNoTime(logText);
        }

        private void cmdOpenWebsite_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.ownedcore.com/forums/world-of-warcraft/world-of-warcraft-bots-programs/wow-bots-questions-requests/542750-pixel-based-bot.html");
        }
    }
}

