//////////////////////////////////////////////////
//                                              //
//   See License.txt for Licensing information  //
//                                              //
//////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

// ReSharper disable AssignNullToNotNullAttribute
// ReSharper disable MemberCanBePrivate.Global

namespace PixelMagic.Helpers
{
    public static class SpellBook
    {
        private static string FullRotationFilePath = "";

        public static List<Spell> Spells;
        public static List<Aura> Auras;
        public static List<Item> Items;
        
        public static DataTable dtSpells;
        public static DataTable dtAuras;
        public static DataTable dtItems;

        public static bool Initialize(string fullRotationFilePath, bool reloadUI = true)
        {
            FullRotationFilePath = fullRotationFilePath;

            Spells = new List<Spell>();
            Auras = new List<Aura>();
            Items = new List<Item>();

            dtSpells = new DataTable();            
            dtSpells.Columns.Add("Spell Id");
            dtSpells.Columns.Add("Spell Name");
            dtSpells.Columns.Add("Key Bind");
            dtSpells.Columns.Add("InternalNo"); // This stores the spell no in the array of spells that will be used on the addon
            dtSpells.Columns.Add("My Keybind"); // This stores the spell no in the array of spells that will be used on the addon

            dtAuras = new DataTable();
            dtAuras.Columns.Add("Aura Id");
            dtAuras.Columns.Add("Aura Name");
            dtAuras.Columns.Add("InternalNo"); // This stores the aura no in the array of auras that will be used on the addon

            dtItems = new DataTable();
            dtItems.Columns.Add("Item Id");
            dtItems.Columns.Add("Item Name");
            dtItems.Columns.Add("InternalNo"); // This stores the item no in the array of items that will be used on the addon

            return Load(reloadUI);
        }

        public static void AddSpell(NumericUpDown spellId, TextBox spellName, Keys key)
        {
            AddSpell(int.Parse(spellId.Value.ToString()), spellName.Text, key.ToString());
        }

        public static void UpdateSpellsFromDataTable()
        {
            foreach (DataRow drSpell in dtSpells.Rows)
            {
                int spellId = int.Parse(drSpell[0].ToString());
                string keyBind = drSpell[2].ToString();
                UpdateSpellKeybind(spellId, keyBind);
            }
        }

        public static void UpdateSpellKeybind(int spellId, string keyBind)
        {
            try
            {
                Spell spell = Spells.FirstOrDefault(s => s.SpellId == spellId);
                spell.KeyBind = keyBind;
            }
            catch(Exception ex)
            {
                Log.Write("Failed to update key binding for spell id " + spellId, Color.Red);
                Log.Write("Error " + ex.Message, Color.Red);
            }
        }

        private static void RenumberSpells()
        {
            var i = 1;

            foreach(DataRow dr in dtSpells.Rows)
            {
                dr["InternalNo"] = i;
                i++;
            }
        }

        private static void RenumberAuras()
        {
            var i = 1;

            foreach (DataRow dr in dtAuras.Rows)
            {
                dr["InternalNo"] = i;
                i++;
            }
        }

        private static void RenumberItems()
        {
            var i = 1;

            foreach (DataRow dr in dtItems.Rows)
            {
                dr["InternalNo"] = i;
                i++;
            }
        }

        public static void AddSpell(int spellId, string spellName, string keyBind)
        {
            keyBind = keyBind.Replace("\r", "").Replace("\n", "");

            if (dtSpells != null && dtSpells.Select($"[Spell Id] = '{spellId}'").Length == 0)
            {
                try
                {
                    dtSpells.Rows.Add(spellId, spellName, keyBind, 0);
                    RenumberSpells();

                    var newSpellId = int.Parse(dtSpells.Select($"[Spell Id] = '{spellId}'")[0]["InternalNo"].ToString());

                    Spells.Add(new Spell(spellId, spellName, keyBind, newSpellId));
                }
                catch (Exception ex)
                {
                    Log.Write(ex.Message, Color.Red);
                }
            }
            else
            {
                MessageBox.Show("The current spell already exists, you may not add it twice", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void AddAura(NumericUpDown auraId, TextBox auraName)
        {
            AddAura(int.Parse(auraId.Value.ToString()), auraName.Text);
        }

        public static void AddAura(int auraId, string auraName)
        {
            if (dtAuras != null && dtAuras.Select($"[Aura Id] = '{auraId}'").Length == 0)
            {
                dtAuras.Rows.Add(auraId, auraName);
                RenumberAuras();

                var newAuraId = int.Parse(dtAuras.Select($"[Aura Id] = '{auraId}'")[0]["InternalNo"].ToString());

                Auras.Add(new Aura(auraId, auraName, newAuraId));
            }
            else
            {
                MessageBox.Show("The current aura already exists, you may not add it twice", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void AddItem(NumericUpDown ItemId, TextBox ItemName)
        {
            AddItem(int.Parse(ItemId.Value.ToString()), ItemName.Text);
        }

        public static void AddItem(int itemId, string itemName)
        {
            if (dtItems != null && dtItems.Select($"[Item Id] = '{itemId}'").Length == 0)
            {
                dtItems.Rows.Add(itemId, itemName);
                RenumberItems();

                var newItemId = int.Parse(dtItems.Select($"[Item Id] = '{itemId}'")[0]["InternalNo"].ToString());

                Items.Add(new Item(itemId, itemName, newItemId));
            }
            else
            {
                MessageBox.Show("The current Item already exists, you may not add it twice", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public static void RemoveSpell(NumericUpDown spellId)
        {
            RemoveSpell(int.Parse(spellId.Value.ToString()));
        }

        public static void RemoveSpell(int spellId)
        {
            if (dtSpells.Select($"[Spell Id] = '{spellId}'").Length == 1)
            {
                dtSpells.Rows.Remove(dtSpells.Select($"[Spell Id] = '{spellId}'").FirstOrDefault());
                Spells.Remove(Spells.FirstOrDefault(s => s.SpellId == spellId));

                RenumberSpells();
            }
            else
            {
                MessageBox.Show("The current spell does not exist in the spellbook yet, so it can't be removed", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void RemoveAura(NumericUpDown auraId)
        {
            RemoveAura(int.Parse(auraId.Value.ToString()));
        }

        public static void RemoveAura(int auraId)
        {
            if (dtAuras.Select($"[Aura Id] = '{auraId}'").Length == 1)
            {
                dtAuras.Rows.Remove(dtAuras.Select($"[Aura Id] = '{auraId}'").FirstOrDefault());
                Auras.Remove(Auras.FirstOrDefault(a => a.AuraId == auraId));
            }
            else
            {
                MessageBox.Show("The current aura does not exist in the spellbook yet, so it can't be removed", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void RemoveItem(NumericUpDown itemId)
        {
            RemoveItem(int.Parse(itemId.Value.ToString()));
        }

        public static void RemoveItem(int itemId)
        {
            if (dtItems.Select($"[Item Id] = '{itemId}'").Length == 1)
            {
                dtItems.Rows.Remove(dtItems.Select($"[Item Id] = '{itemId}'").FirstOrDefault());
                Items.Remove(Items.FirstOrDefault(a => a.ItemId == itemId));
            }
            else
            {
                MessageBox.Show("The current Item does not exist in the spellbook yet, so it can't be removed", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static bool Load(bool reloadUI = true)
        {
            using (var sr = new StreamReader(FullRotationFilePath))
            {
                var fileContents = sr.ReadToEnd();

                //var encrypted = (FullRotationFilePath.EndsWith(".enc"));

                //if (encrypted)
                //{ 
                    //fileContents = Encryption.Decrypt(fileContents);
                //}
                
                RotationFileContents = fileContents;

                var addonLines = false;
                var readLines = false;
                
                foreach (var line in fileContents.Split('\n'))
                {                
                    if (line.Contains("AddonDetails.db"))
                    {
                        addonLines = true;
                    }

                    if (line.Contains("SpellBook.db"))
                    {
                        readLines = true;
                    }

                    if (addonLines)
                    {
                        if (line.Contains("AddonLines.db"))
                            continue;

                        var split = line.Split('=');

                        if (split[0] == "AddonAuthor")
                        {
                            AddonAuthor = split[1];
                        }
                        
                        if (split[0] == "WoWVersion")
                        {
                            InterfaceVersion = split[1];
                        }
                    }

                    if (readLines)
                    {
                        if (line.Contains("SpellBook.db"))
                            continue;

                        var split = line.Split(',');

                        if (split[0] == "Spell")
                        {
                            AddSpell(int.Parse(split[1]), split[2], split[3]);
                        }

                        if (split[0] == "Aura")
                        {
                            AddAura(int.Parse(split[1]), split[2]);
                        }

                        if (split[0] == "Item")
                        {
                            AddItem(int.Parse(split[1]), split[2]);
                        }
                    }
                }

                sr.Close();

                if (addonLines && readLines)  // If the word "AddonDetails.db" and "SpellBook.db" exists in the rotation.cs file
                {
                    RenumberSpells();
                    Log.Write($"Found {Spells.Count} spells defined", Color.Gray);
                    Log.Write($"Found {Auras.Count} auras defined", Color.Gray);
                    Log.Write("SpellBook loaded.");

                    if (!File.Exists(AddonPath + "\\" + AddonName + "\\" + AddonName + ".toc"))
                    {
                        return GenerateLUAFile(reloadUI);
                    }
                }
                else
                {                    
                    Log.Write("Failed to load addon details or spellbook from rotation file, please ensure that it is not missing.", Color.Red);
                    Log.Write("you can see the file: " + Application.StartupPath + "\\Rotations\\Warrior\\Warrior.cs for reference", Color.Red);

                    return false;
                }
            }

            return false;
        }

        public static string AddonAuthor;
        public static string InterfaceVersion;
        public static int NumericInterfaceVersion => int.Parse(InterfaceVersion.Contains("-") ? InterfaceVersion.Split('-')[1].Trim() : InterfaceVersion);
        public static string AddonName => ConfigFile.ReadValue("PixelMagic", "AddonName");

        public static string RotationFileContents = "";

        public static void Save(TextBox author, string interfaceVersion)
        {
            AddonAuthor = author.Text.Replace("\n", "").Replace("\r", "");
            InterfaceVersion = interfaceVersion;
            
            try
            {
                var fullRotationText = "";

                var encrypted = FullRotationFilePath.EndsWith(".enc");

                using (var sr = new StreamReader(FullRotationFilePath))
                {
                    var readLines = true;
                    var fileContents = sr.ReadToEnd();
                    
                    //if (encrypted)
                    //{
                    //    fileContents = Encryption.Decrypt(fileContents);
                    //}

                    foreach (var line in fileContents.Split('\n'))
                    {
                        if (line.Contains("AddonDetails.db"))
                        {
                            readLines = false;
                        }

                        if (readLines)
                        {
                            if (line.StartsWith("/*"))
                            {
                                fullRotationText += line.Replace("\r", "").Replace("\n", "");
                            }
                            else
                            {
                                fullRotationText += line.Replace("\r", "").Replace("\n", "") + Environment.NewLine;
                            }
                        }
                    }

                    sr.Close();
                }

                var updatedRotationText = fullRotationText + Environment.NewLine;
                updatedRotationText += "[AddonDetails.db]" + Environment.NewLine;
                updatedRotationText += $"AddonAuthor={AddonAuthor}" + Environment.NewLine;
                updatedRotationText += $"AddonName={AddonName}" + Environment.NewLine;
                updatedRotationText += $"WoWVersion={InterfaceVersion}" + Environment.NewLine;

                updatedRotationText += "[SpellBook.db]" + Environment.NewLine;

                UpdateSpellsFromDataTable();

                updatedRotationText = Spells.Aggregate(updatedRotationText, (current, spell) => current + ($"Spell,{spell.SpellId},{spell.SpellName},{spell.KeyBind}" + Environment.NewLine));
                updatedRotationText = Auras.Aggregate(updatedRotationText, (current, aura) => current + ($"Aura,{aura.AuraId},{aura.AuraName}" + Environment.NewLine));
                updatedRotationText = Items.Aggregate(updatedRotationText, (current, item) => current + ($"Item,{item.ItemId},{item.ItemName}" + Environment.NewLine));

                updatedRotationText += "*/";

                using (var sw = new StreamWriter(FullRotationFilePath, false))
                {
                    //if (encrypted)
                    //{
                    //    updatedRotationText = Encryption.Encrypt(updatedRotationText);
                    //    sw.WriteLine(updatedRotationText);
                    //}
                    //else
                    {
                        sw.WriteLine(updatedRotationText);
                    }                    

                    sw.Close();
                }

                GenerateLUAFile();

                MessageBox.Show("Spell Book Saved.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static string AddonPath => $"{WoW.AddonPath}\\{AddonName}";

        private static string LibStubPath => $"{WoW.AddonPath}\\{AddonName}\\Boss\\LibStub";

        private static string LibBossPath => $"{WoW.AddonPath}\\{AddonName}\\Boss";

        private static string LibRangeStubPath => $"{WoW.AddonPath}\\{AddonName}\\Range\\LibStub";

        private static string LibRangePath => $"{WoW.AddonPath}\\{AddonName}\\Range";

        private static void CustomLua()
        {
            Log.Write("Starting Addon Edit, for Custom Lua...");

            try
            {
                var CustomLua = File.ReadAllText(FullRotationFilePath.Replace(".cs", ".lua"));
                if (CustomLua.Trim() == "")
                {
                    Log.Write("Custom lua is blank, not using it...", Color.Gray);
                    return;
                }

                try
                {
                    var addonlua = File.ReadAllText($"{AddonPath}\\{AddonName}.lua");

                    addonlua = addonlua.Replace("local lastCombat = nil" + Environment.NewLine + "local alphaColor = 1", "local lastCombat = nil" + Environment.NewLine + "local alphaColor = 1" + Environment.NewLine + CustomLua);
                    addonlua = addonlua.Replace("InitializeOne()" + Environment.NewLine + "            InitializeTwo()",
                                                "InitializeOne()" + Environment.NewLine + "            InitializeTwo()" + Environment.NewLine + "            InitializeThree()");

                    File.WriteAllText($"{AddonPath}\\{AddonName}.lua", addonlua);
                    Log.Write("Addon Editing in progress", Color.Green);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "PixelMagic", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch 
            {
                Log.Write("Custom Lua file not supplied, using default Lua", Color.Gray);
            }            
        }

        public static bool GenerateLUAFile(bool reloadUI = true)
        {
            try
            {
                if (!Directory.Exists(AddonPath))
                    Directory.CreateDirectory(AddonPath);

                if (!Directory.Exists(LibBossPath))
                    Directory.CreateDirectory(LibBossPath);

                if (!Directory.Exists(LibStubPath))
                    Directory.CreateDirectory(LibStubPath);

                if (!Directory.Exists(LibRangePath))
                    Directory.CreateDirectory(LibRangePath);

                if (!Directory.Exists(LibRangeStubPath))
                    Directory.CreateDirectory(LibRangeStubPath);

                Log.Write($"Creating Addon from SpellBook, AddonName will be [{AddonName}]...");

                Log.Write($"Creating file: [{AddonName}.toc]", Color.Gray);

                using (var sr = new StreamWriter($"{AddonPath}\\{AddonName}.toc"))
                {
                    //  ## Author: WiNiFiX
                    //  ## Interface: 60200
                    //  ## Title: DoIt
                    //  ## Version: 1.0.0
                    //  ## SavedVariablesPerCharacter: DoItOptions
                    //  DoItBase.lua

                    sr.WriteLine($"## Author: {AddonAuthor.Replace("\r", "").Replace("\n", "")}");
                    
                    sr.WriteLine($"## Interface: {NumericInterfaceVersion}");
                    sr.WriteLine($"## Title: {AddonName.Replace("\r", "").Replace("\n", "")}");
                    sr.WriteLine($"## Version: {Application.ProductVersion}");
                    sr.WriteLine($"## SavedVariablesPerCharacter: {AddonName.Replace("\r", "").Replace("\n", "")}_settings");
                    sr.WriteLine($"{AddonName.Replace("\r", "").Replace("\n", "")}.lua");
                    sr.WriteLine(@"#@no-lib-strip@");
                    sr.WriteLine("BossLib.xml");
                    sr.WriteLine("RangeLib.xml");
                    sr.WriteLine(@"#@end-no-lib-strip@");
                    sr.Close();
                }

 ///////////////////////////////////////////////////////////////////////////////////////////////// BOSS LIB /////////////////////////////////////////
 
                Log.Write("Creating file: [BossLib.xml]", Color.Gray);

                using (var sr = new StreamWriter($"{AddonPath}\\BossLib.xml"))
                {
                    sr.WriteLine(@"<Ui xmlns=""http://www.blizzard.com/wow/ui/"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xsi:schemaLocation=""http://www.blizzard.com/wow/ui/ ..\FrameXML\UI.xsd"">");
                    sr.WriteLine(@"<Script file=""Boss\LibStub\LibStub.lua""/>");
                    sr.WriteLine(@"<Include file=""Boss\lib.xml""/>");
                    sr.WriteLine(@"</Ui>");
                    sr.Close();
                }

                Log.Write("Creating file: [Boss\\LibBossIDs-1.0.toc]", Color.Gray);

                using (var sr = new StreamWriter($"{LibBossPath}\\LibBossIDs-1.0.toc"))
                {
                    sr.WriteLine(InterfaceVersion.Contains("-")? $"## Interface: {InterfaceVersion.Split('-')[1].Trim()}" : $"## Interface: {InterfaceVersion}");
                    sr.WriteLine("## LoadOnDemand: 1");
                    sr.WriteLine("## Title: Lib: BossIDs-1.0");
                    sr.WriteLine("## A library to provide mobIDs for bosses.");
                    sr.WriteLine("## Author: Elsia");
                    sr.WriteLine("## X-Category: Library");
                    sr.WriteLine("## X-License: Public Domain");
                    sr.WriteLine("## X-Curse-Packaged-Version: r97-release");
                    sr.WriteLine("## X-Curse-Project-Name: LibBossIDs-1.0");
                    sr.WriteLine("## X-Curse-Project-ID: libbossids-1-0");
                    sr.WriteLine("## X-Curse-Repository-ID: wow/libbossids-1-0/mainline");
                    sr.WriteLine("LibStub\\LibStub.lua");
                    sr.WriteLine("lib.xml");
                    sr.Close();
                }

                Log.Write("Creating file: [Boss\\lib.xml]", Color.Gray);

                using (var sr = new StreamWriter($"{AddonPath}\\Boss\\lib.xml"))
                {
                    sr.WriteLine(@"<Ui xmlns=""http://www.blizzard.com/wow/ui/"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xsi:schemaLocation=""http://www.blizzard.com/wow/ui/ ..\FrameXML\UI.xsd"">");
                    sr.WriteLine(@"<Script file=""LibBossIDs-1.0.lua"" />");
                    sr.WriteLine("</Ui>");
                    sr.Close();
                }

                Log.Write("Creating file: [Boss\\LibBossIDs-1.0.lua]", Color.Gray);

                using (var sr = new StreamWriter($"{AddonPath}\\Boss\\LibBossIDs-1.0.lua"))
                {
                    var luaContents1 = AddonLibBoss.LuaContents;
                    sr.WriteLine(luaContents1);
                    sr.Close();
                }

                Log.Write("Creating file: [Boss\\LibStub\\LibStub.lua]", Color.Gray);

                using (var sr = new StreamWriter($"{AddonPath}\\Boss\\LibStub\\LibStub.lua"))
                {
                    var luaContents2 = AddonLibStub.LuaContents;
                    sr.WriteLine(luaContents2);
                    sr.Close();
                }

 ///////////////////////////////////////////////////////////////////////////////////////////////// RANGE LIB /////////////////////////////////////////

                Log.Write("Creating file: [RangeLib.xml]", Color.Gray);

                using (var sr = new StreamWriter($"{AddonPath}\\RangeLib.xml"))
                {
                    sr.WriteLine(@"<Ui xmlns=""http://www.blizzard.com/wow/ui/"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xsi:schemaLocation=""http://www.blizzard.com/wow/ui/ ..\FrameXML\UI.xsd"">");
                    sr.WriteLine(@"<Script file=""Range\LibStub\LibStub.lua""/>");
                    sr.WriteLine(@"<Include file=""Range\lib.xml""/>");
                    sr.WriteLine(@"</Ui>");
                    sr.Close();
                }

                Log.Write("Creating file: [Range\\LibSpellRange - 1.0.toc]", Color.Gray);

                using (var sr = new StreamWriter($"{LibRangePath}\\LibSpellRange-1.0.toc"))
                {
                    sr.WriteLine(InterfaceVersion.Contains("-") ? $"## Interface: {InterfaceVersion.Split('-')[1].Trim()}" : $"## Interface: {InterfaceVersion}");
                    sr.WriteLine("## LoadOnDemand: 1");
                    sr.WriteLine("## Title: Lib: SpellRange-1.0");
                    sr.WriteLine("## Notes: Provides enhanced spell range checking functionality");
                    sr.WriteLine("## Author: Cybeloras of Aerie Peak");
                    sr.WriteLine("## X-Category: Library");
                    sr.WriteLine("## X-License: Public Domain");
                    sr.WriteLine("## X-Curse-Packaged-Version: 1.0.011");
                    sr.WriteLine("## X-Curse-Project-Name: LibSpellRange-1.0");
                    sr.WriteLine("## X-Curse-Project-ID: libspellrange-1-0");
                    sr.WriteLine("## X-Curse-Repository-ID: wow/libspellrange-1-0/mainline");
                    sr.WriteLine("LibStub\\LibStub.lua");
                    sr.WriteLine("lib.xml");
                    sr.Close();
                }

                Log.Write("Creating file: [Range\\lib.xml]", Color.Gray);

                using (var sr = new StreamWriter($"{AddonPath}\\Range\\lib.xml"))
                {
                    sr.WriteLine(@"<Ui xmlns=""http://www.blizzard.com/wow/ui/"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xsi:schemaLocation=""http://www.blizzard.com/wow/ui/ ..\FrameXML\UI.xsd"">");
                    sr.WriteLine(@"<Script file=""LibSpellRange-1.0.lua"" />");
                    sr.WriteLine("</Ui>");
                    sr.Close();
                }

                Log.Write("Creating file: [Range\\LibSpellRange-1.0.lua]", Color.Gray);

                using (var sr = new StreamWriter($"{AddonPath}\\Range\\LibSpellRange-1.0.lua"))
                {
                    var luaContents = AddonLibRange.LuaContents;
                    sr.WriteLine(luaContents);
                    sr.Close();
                }

                Log.Write("Creating file: [Range\\LibStub\\LibStub.lua]", Color.Gray);

                using (var sr = new StreamWriter($"{AddonPath}\\Range\\LibStub\\LibStub.lua"))
                {
                    var luaContents = AddonLibStubRange.LuaContents;
                    sr.WriteLine(luaContents);
                    sr.Close();
                }

                Log.Write($"Creating file: [{AddonName}.lua]", Color.Gray);
                
                using (var sr = new StreamWriter($"{AddonPath}\\{AddonName}.lua"))
                {
                    //local cooldowns = { --These should be spellIDs for the spell you want to track for cooldowns
                    //    56641,    -- Steadyshot
                    //    3044,     -- Arcane Shot
                    //    34026     -- Kill Command
                    //}

                    var cooldowns = "local cooldowns = { --These should be spellIDs for the spell you want to track for cooldowns" + Environment.NewLine;

                    foreach (var spell in Spells)
                    {
                        if (spell.InternalSpellNo == Spells.Count)  // We are adding the last spell, dont include the comma
                        {
                            cooldowns += $"    {spell.SpellId} \t -- {spell.SpellName}" + Environment.NewLine;
                        }
                        else
                        {
                            cooldowns += $"    {spell.SpellId},\t -- {spell.SpellName}" + Environment.NewLine;
                        }
                    }

                    cooldowns += "}" + Environment.NewLine;

                    sr.Write(cooldowns);

                    var auras = "local buffs = { --These should be auraIDs for the spell you want to track " + Environment.NewLine;

                    foreach (var aura in Auras)
                    {
                        if (aura.InternalAuraNo == Auras.Count)  // We are adding the last aura, dont include the comma
                        {
                            auras += $"    {aura.AuraId} \t -- {aura.AuraName}" + Environment.NewLine;
                        }
                        else
                        {
                            auras += $"    {aura.AuraId},\t -- {aura.AuraName}" + Environment.NewLine;
                        }
                    }

                    auras += "}" + Environment.NewLine;

                    sr.Write(auras);

                    var debuffs = "local debuffs = { --These should be auraIDs for the spell you want to track " + Environment.NewLine;

                    foreach (var aura in Auras)
                    {
                        if (aura.InternalAuraNo == Auras.Count)  // We are adding the last aura, dont include the comma
                        {
                            debuffs += $"    {aura.AuraId} \t -- {aura.AuraName}" + Environment.NewLine;
                        }
                        else
                        {
                            debuffs += $"    {aura.AuraId},\t -- {aura.AuraName}" + Environment.NewLine;
                        }
                    }

                    debuffs += "}" + Environment.NewLine;

                    sr.Write(debuffs);

                    var items = "local items = { --These should be itemIDs for the items you want to track " + Environment.NewLine;

                    foreach (var item in Items)
                    {
                        if (item.ItemId == Items.Count)  // We are adding the last item, dont include the comma
                        {
                            items += $"    {item.ItemId} \t -- {item.ItemName}" + Environment.NewLine;
                        }
                        else
                        {
                            items += $"    {item.ItemId},\t -- {item.ItemName}" + Environment.NewLine;
                        }
                    }

                    items += "}" + Environment.NewLine;

                    sr.Write(items);

                    var luaContents = Addon.LuaContents;
                    luaContents = luaContents.Replace("[PixelMagic]", AddonName);
                    sr.WriteLine(luaContents);
                    sr.Close();
                }
                
                Log.Write("Addon file generated.", Color.Green);

                CustomLua();

                Log.Write($"Make sure that the addon: [{AddonName}] is enabled in your list of WoW Addons or the rotation bot will fail to work", Color.Black);

                if (reloadUI)
                {
                    WoW.SendMacro("/reload");
                }

                return true;
            }
            catch(Exception ex)
            {
                Log.Write("Failed to generate addon file:", Color.Red);
                Log.Write(ex.Message, Color.Red);

                return false;
            }
        }
    }
}