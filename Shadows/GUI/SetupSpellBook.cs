//////////////////////////////////////////////////
//                                              //
//   See License.txt for Licensing information  //
//                                              //
//////////////////////////////////////////////////

// Icon Backlink: http://www.aha-soft.com/

using System;
using System.Windows.Forms;
using ShadowMagic.Helpers;
using System.Globalization;
using System.Linq;
using System.Data;

// ReSharper disable once CheckNamespace
#pragma warning disable 168

namespace ShadowMagic.GUI
{
    public partial class SetupSpellBook : Form
    {
        private System.Windows.Forms.Keys key;
        public SetupSpellBook()
        {
            InitializeComponent();
        }

        private string AddonInterfaceVersion => cmbWowVersion.Text;

        private void SetupSpellBook_Load(object sender, EventArgs e)
        {
            txtKeyCode.KeyDown += TxtKeyCode_KeyDown;
            dgSpells.PreviewKeyDown += DgSpells_PreviewKeyDown;
            dgSpells.CellClick += DgSpells_CellClick;

            dgSpells.AllowUserToAddRows = false;

            var ti = new CultureInfo("en-ZA", false).TextInfo;
            txtAddonAuthor.Text = ti.ToTitleCase(Environment.UserName);

            var machineName = new string(Environment.MachineName.Where(char.IsLetter).ToArray()).ToLower();
            txtAddonName.Text = ti.ToTitleCase(machineName);

            txtAddonAuthor.Text = SpellBook.AddonAuthor;
            txtAddonName.Text = ConfigFile.ReadValue("PixelMagic", "AddonName");
            
            try
            {
                foreach (var item in cmbWowVersion.Items)
                {
                    if (item.ToString().Contains(SpellBook.NumericInterfaceVersion.ToString()))
                        cmbWowVersion.SelectedItem = item;
                }
            }
            catch(Exception ex)
            {
                // Do nothing - ignore
            }

            BindingSource source = new BindingSource();
            foreach(DataRow dr in SpellBook.dtSpells.Rows)
            {
                dr[4] = ConfigFile.ReadValue("Keybinds-" + System.IO.Path.GetFileName(frmMain.combatRoutine.FileName).Replace(" ", "").ToLower(), dr[0].ToString());                
            }

            source.DataSource = SpellBook.dtSpells;
            dgSpells.DataSource = source;

            dgSpells.DataError += DgSpells_DataError;
            dgAuras.DataSource = SpellBook.dtAuras;
            dgItems.DataSource = SpellBook.dtItems;
        }

        private static int currentRow = 0;
        private static int currentColum = 0;

        private void DgSpells_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            currentRow = e.RowIndex;
            currentColum = e.ColumnIndex;
        }

        private void DgSpells_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (currentColum == 4)
            {
                dgSpells[currentColum, currentRow].Value = e.KeyCode.ToString();
            }
        }

        private void TxtKeyCode_KeyDown(object sender, KeyEventArgs e)
        {
            key = e.KeyCode;
        }

        private void DgSpells_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // Ignore this is issue with enums see: stackoverflow.com/questions/654829/datagridviewcomboboxcell-binding-value-is-not-valid
        }

        private void cmdAddSpell_Click(object sender, EventArgs e)
        {
            var ret = Web.GetString("http://www.wowhead.com/spell=" + nudSpellId.Value + "&power");
            var split1 = ret.Split(':')[1];
            var split2 = split1.Substring(0, split1.IndexOf(','));
            var spellName = split2.Replace("'", "").Trim();
            txtSpellName.Text = spellName;
            
            SpellBook.AddSpell(nudSpellId, txtSpellName, key);
        }

        private void cmdRemoveSpell_Click(object sender, EventArgs e)
        {
            SpellBook.RemoveSpell(nudSpellId);
        }

        private void cmdAddAura_Click(object sender, EventArgs e)
        {
            SpellBook.AddAura(nudAuraId, txtAuraName);
        }

        private void cmdRemoveAura_Click(object sender, EventArgs e)
        {
            SpellBook.RemoveAura(nudAuraId);
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (cmbWowVersion.Text == "")
            {
                MessageBox.Show("Please select a valid wow version", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            dgSpells.CommitEdit(DataGridViewDataErrorContexts.Commit);
            // Save Custom Keybinds to spells
            foreach (DataGridViewRow row in dgSpells.Rows)
            {
                string spellId = row.Cells[0].Value.ToString();
                string keyBind = row.Cells[4].Value.ToString();
                ConfigFile.WriteValue("Keybinds-" + System.IO.Path.GetFileName(frmMain.combatRoutine.FileName).Replace(" ", "").ToLower(), spellId, keyBind);
            }

            SpellBook.Save(txtAddonAuthor, AddonInterfaceVersion);            
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void cmdAddItem_Click(object sender, EventArgs e)
        {
            SpellBook.AddItem(nudItemId, txtItemName);
        }

        private void cmdRemoveItem_Click(object sender, EventArgs e)
        {
            SpellBook.RemoveItem(nudItemId);
        }
    }
}