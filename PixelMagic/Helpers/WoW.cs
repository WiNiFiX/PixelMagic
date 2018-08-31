//////////////////////////////////////////////////
//                                              //
//   See License.txt for Licensing information  //
//                                              //
//////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Speech.Synthesis;
using System.Threading;
using PixelMagic.GUI;

// ReSharper disable UnusedMember.Local
// ReSharper disable PossibleNullReferenceException
// ReSharper disable once CheckNamespace

namespace PixelMagic.Helpers
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
    public static partial class WoW
    {
        [Flags]
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public enum Keys
        {
            A = 0x41,
            Add = 0x6b,
            Alt = 0x40000,
            Apps = 0x5d,
            Attn = 0xf6,
            B = 0x42,
            Back = 8,
            BrowserBack = 0xa6,
            BrowserFavorites = 0xab,
            BrowserForward = 0xa7,
            BrowserHome = 0xac,
            BrowserRefresh = 0xa8,
            BrowserSearch = 170,
            BrowserStop = 0xa9,
            C = 0x43,
            Cancel = 3,
            Capital = 20,
            CapsLock = 20,
            Clear = 12,
            Control = 0x20000,
            ControlKey = 0x11,
            Crsel = 0xf7,
            D = 0x44,
            D0 = 0x30,
            D1 = 0x31,
            D2 = 50,
            D3 = 0x33,
            D4 = 0x34,
            D5 = 0x35,
            D6 = 0x36,
            D7 = 0x37,
            D8 = 0x38,
            D9 = 0x39,
            Decimal = 110,
            Delete = 0x2e,
            Divide = 0x6f,
            Down = 40,
            E = 0x45,
            End = 0x23,
            Enter = 13,
            EraseEof = 0xf9,
            Escape = 0x1b,
            Execute = 0x2b,
            Exsel = 0xf8,
            F = 70,
            F1 = 0x70,
            F10 = 0x79,
            F11 = 0x7a,
            F12 = 0x7b,
            F13 = 0x7c,
            F14 = 0x7d,
            F15 = 0x7e,
            F16 = 0x7f,
            F17 = 0x80,
            F18 = 0x81,
            F19 = 130,
            F2 = 0x71,
            F20 = 0x83,
            F21 = 0x84,
            F22 = 0x85,
            F23 = 0x86,
            F24 = 0x87,
            F3 = 0x72,
            F4 = 0x73,
            F5 = 0x74,
            F6 = 0x75,
            F7 = 0x76,
            F8 = 0x77,
            F9 = 120,
            FinalMode = 0x18,
            G = 0x47,
            H = 0x48,
            HanguelMode = 0x15,
            HangulMode = 0x15,
            HanjaMode = 0x19,
            Help = 0x2f,
            Home = 0x24,
            I = 0x49,
            ImeAccept = 30,
            ImeAceept = 30,
            ImeConvert = 0x1c,
            ImeModeChange = 0x1f,
            ImeNonconvert = 0x1d,
            Insert = 0x2d,
            J = 0x4a,
            JunjaMode = 0x17,
            K = 0x4b,
            KanaMode = 0x15,
            KanjiMode = 0x19,
            KeyCode = 0xffff,
            L = 0x4c,
            LaunchApplication1 = 0xb6,
            LaunchApplication2 = 0xb7,
            LaunchMail = 180,
            LButton = 1,
            LControlKey = 0xa2,
            Left = 0x25,
            LineFeed = 10,
            LMenu = 0xa4,
            LShiftKey = 160,
            LWin = 0x5b,
            M = 0x4d,
            MButton = 4,
            MediaNextTrack = 0xb0,
            MediaPlayPause = 0xb3,
            MediaPreviousTrack = 0xb1,
            MediaStop = 0xb2,
            Menu = 0x12,
            Modifiers = -65536,
            Multiply = 0x6a,
            N = 0x4e,
            Next = 0x22,
            NoName = 0xfc,
            None = 0,
            NumLock = 0x90,
            NumPad0 = 0x60,
            NumPad1 = 0x61,
            NumPad2 = 0x62,
            NumPad3 = 0x63,
            NumPad4 = 100,
            NumPad5 = 0x65,
            NumPad6 = 0x66,
            NumPad7 = 0x67,
            NumPad8 = 0x68,
            NumPad9 = 0x69,
            O = 0x4f,
            Oem1 = 0xba,
            Oem102 = 0xe2,
            Oem2 = 0xbf,
            Oem3 = 0xc0,
            Oem4 = 0xdb,
            Oem5 = 220,
            Oem6 = 0xdd,
            Oem7 = 0xde,
            Oem8 = 0xdf,
            OemBackslash = 0xe2,
            OemClear = 0xfe,
            OemCloseBrackets = 0xdd,
            Oemcomma = 0xbc,
            OemMinus = 0xbd,
            OemOpenBrackets = 0xdb,
            OemPeriod = 190,
            OemPipe = 220,
            Oemplus = 0xbb,
            OemQuestion = 0xbf,
            OemQuotes = 0xde,
            OemSemicolon = 0xba,
            Oemtilde = 0xc0,
            P = 80,
            Pa1 = 0xfd,
            Packet = 0xe7,
            PageDown = 0x22,
            PageUp = 0x21,
            Pause = 0x13,
            Play = 250,
            Print = 0x2a,
            PrintScreen = 0x2c,
            Prior = 0x21,
            ProcessKey = 0xe5,
            Q = 0x51,
            R = 0x52,
            RButton = 2,
            RControlKey = 0xa3,
            Return = 13,
            Right = 0x27,
            RMenu = 0xa5,
            RShiftKey = 0xa1,
            RWin = 0x5c,
            S = 0x53,
            Scroll = 0x91,
            Select = 0x29,
            SelectMedia = 0xb5,
            Separator = 0x6c,
            Shift = 0x10000,
            ShiftKey = 0x10,
            Sleep = 0x5f,
            Snapshot = 0x2c,
            Space = 0x20,
            Subtract = 0x6d,
            T = 0x54,
            Tab = 9,
            U = 0x55,
            Up = 0x26,
            V = 0x56,
            VolumeDown = 0xae,
            VolumeMute = 0xad,
            VolumeUp = 0xaf,
            W = 0x57,
            X = 0x58,
            XButton1 = 5,
            XButton2 = 6,
            Y = 0x59,
            Z = 90,
            Zoom = 0xfb
        }

        internal static Process Process;
        private static Random random;
        private static readonly object thisLock = new object();
        private static readonly Bitmap screenPixel = new Bitmap(1, 1);
        private static DataTable dtColorHelper;
        private static SpeechSynthesizer synthesizer;
        private static readonly IDictionary<string, int> damageModifierHash = new Dictionary<string, int>();

        private static string Version => Process.MainModule.FileVersionInfo.FileVersion;
        public static string InstallPath => Path.GetDirectoryName(Process?.MainModule.FileName);
        public static string AddonPath => InstallPath + "\\Interface\\AddOns";
        public static string Config => new StreamReader(Path.Combine(InstallPath, "WTF\\Config.wtf")).ReadToEnd();
        public static string LastSpell { private set; get; } = "";
        
        public static bool IsInCombat
        {
            get
            {
                var c = GetBlockColor(4, 1);
                return c.R == Color.Red.R && c.G == Color.Red.G && c.B == Color.Red.B;
            }
        }

        /// <summary>
        /// Gets a value indicating whether [player is channeling].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [player is channeling]; otherwise, <c>false</c>.
        /// </value>
        public static bool PlayerIsChanneling
        {
            get
            {
                var blockColor = GetBlockColor(8, 1);
                return blockColor.R == 0 && blockColor.G == 255 && blockColor.B == 0;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has target.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has target; otherwise, <c>false</c>.
        /// </value>
        public static bool HasTarget
        {
            get
            {
                if (HealthPercent == 0) return false;
                if (TargetHealthPercent == 0) return false;
                if (HasBossTarget) return true;

                var c = GetBlockColor(7, 1);
                return c.R == Color.Red.R && c.G == Color.Red.G && c.B == Color.Red.B;
            }
        }

        /// <summary>
        /// Gets a value indicating whether [player is casting].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [player is casting]; otherwise, <c>false</c>.
        /// </value>
        public static bool PlayerIsCasting
        {
            get
            {
                var c = GetBlockColor(8, 1);
                return c.R == Color.Red.R && c.G == Color.Red.G && c.B == Color.Red.B;
            }
        }

        /// <summary>
        /// Gets a value indicating whether [target is casting].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [target is casting]; otherwise, <c>false</c>.
        /// </value>
        public static bool TargetIsCasting => TargetCastingSpellID != 0;

        /// <summary>
        /// Gets a value indicating whether [target is casting and spell is interruptible].
        /// </summary>
        /// <value>
        /// <c>true</c> if [target is casting and spell is interruptible]; otherwise, <c>false</c>.
        /// </value>
        public static bool TargetIsCastingAndSpellIsInterruptible
        {
            get
            {
                var pixelColor = GetBlockColor(9, 1);
                return Convert.ToInt32(Math.Round(Convert.ToSingle(pixelColor.R) / 255)) == 1;
            }
        }

        /// <summary>
        /// Gets the target percent cast.
        /// </summary>
        /// <value>
        /// The target percent cast.
        /// </value>
        public static int TargetPercentCast
        {
            get
            {
                var pixelColor = GetBlockColor(9, 1);
                var ret = Convert.ToInt32(Math.Round(Convert.ToSingle(pixelColor.G) * 100 / 255));
                return 100 - ret;
            }
        }

        /// <summary>
        /// Gets a value indicating whether [target is visible].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [target is visible]; otherwise, <c>false</c>.
        /// </value>
        public static bool TargetIsVisible
        {
            get
            {
                var c = GetBlockColor(11, 1);
                return c.R == Color.Red.R && c.G == Color.Red.G && c.B == Color.Red.B;
            }
        }

        /// <summary>
        /// Gets a value indicating whether [target is friend].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [target is friend]; otherwise, <c>false</c>.
        /// </value>
        public static bool TargetIsFriend
        {
            get
            {
                var c = GetBlockColor(6, 1);
                return c.R == 0 && c.G == 255 && c.B == 0;
            }
        }

        /// <summary>
        /// Gets the current chi.
        /// </summary>
        /// <value>
        /// The current chi.
        /// </value>
        public static int CurrentChi => UnitPower;

        /// <summary>
        /// Gets the current arcane charges.
        /// </summary>
        /// <value>
        /// The current arcane charges.
        /// </value>
        public static int CurrentArcaneCharges => UnitPower;

        /// <summary>
        /// Gets the current runes.
        /// </summary>
        /// <value>
        /// The current runes.
        /// </value>
        public static int CurrentRunes => UnitPower;

        /// <summary>
        /// Gets the current combo points.
        /// </summary>
        /// <value>
        /// The current combo points.
        /// </value>
        public static int CurrentComboPoints => UnitPower;

        /// <summary>
        /// Gets the current soul shards.
        /// </summary>
        /// <value>
        /// The current soul shards.
        /// </value>
        public static int CurrentSoulShards => UnitPower;

        /// <summary>
        /// Gets the current holy power.
        /// </summary>
        /// <value>
        /// The current holy power.
        /// </value>
        public static int CurrentHolyPower => UnitPower;

        /// <summary>
        /// Gets the unit power.
        /// </summary>
        /// <value>
        /// The unit power.
        /// </value>
        public static int UnitPower
        {
            get
            {
                var c = GetBlockColor(5, 1);
                try
                {
                    var unitPower = dtColorHelper.Select($"[Rounded] = '{c.G}'").FirstOrDefault()?["Value"].ToString();
                    // ReSharper disable once AssignNullToNotNullAttribute
                    return unitPower != null ? int.Parse(unitPower) : 100;
                }
                catch (Exception ex)
                {
                    Log.Write($"[UnitPower] Red = {c.R}");
                    Log.Write(ex.Message, Color.Red);
                    return 100;
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether [target is enemy].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [target is enemy]; otherwise, <c>false</c>.
        /// </value>
        public static bool TargetIsEnemy => !TargetIsFriend;

        /// <summary>
        /// Gets the health percent.
        /// </summary>
        /// <value>
        /// The health percent.
        /// </value>
        public static int HealthPercent
        {
            get
            {
                var c = GetBlockColor(1, 1);
                try
                {
                    var health = dtColorHelper.Select($"[Rounded] = '{c.R}'").FirstOrDefault()?["Value"].ToString();

                    // ReSharper disable once AssignNullToNotNullAttribute
                    return health != null ? int.Parse(health) : 100;
                }
                catch (Exception ex)
                {
                    Log.Write($"[Health] Red = {c.R}");
                    Log.Write(ex.Message, Color.Red);
                    return 100;
                }
            }
        }


        /// <summary>
        /// Counts Number of Hostile NPC Nameplaes
        /// </summary>
        /// <returns>Returns Count</returns>
        public static int CountEnemyNPCsInRange
        {
            get
            {
                var c = GetBlockColor(1, 23);
                try
                {
                    if (c.R != 255 || c.B == 0)
                        return 0;
                        var power = dtColorHelper.Select($"[Rounded] = '{c.G}'").FirstOrDefault()?["Value"].ToString();
                        return power != null ? int.Parse(power) : 0;
                    
                }
                catch (Exception ex)
                {
                    Log.Write("Error in Npc Count  Green = " + c.G);

                    Log.Write(ex.Message, Color.Red);
                }
                return 1;
            }
        }
        /// <summary>
        /// NpC NamePlates on/off
        /// </summary>
        /// <returns>True or False</returns>
        /// 
        public static bool Nameplates
        {
            get
            {
                var c = GetBlockColor(1, 23);
                try
                {
                    if (c.R != 255)
                        return false;
                    if (c.B == 255)
                        return true;
                    else
                        return false;
                 
                }
                catch (Exception ex)
                {
                    Log.Write("Error in NamePlateON  Blue = " + c.B);

                    Log.Write(ex.Message, Color.Red);
                }
                return false;
            }
        }
        /// <summary>
        /// Setbonus for Legion
        /// </summary>
        /// <param name="tier">What tier set bonus: 19-20 </param>
        /// <returns>#num of piece of set worn</returns>
        public static int SetBonus(int tier)
        {
            var c = WoW.GetBlockColor(3, 24);
            if (c.B != 0)
                return 0;
            try
            {
                if (tier > 20 || tier < 19)
                    return 0;
                
                c = WoW.GetBlockColor(4, 24);
                var slot = c.R;
                switch (tier)
                {
                    case 19:
                        slot = c.G;
                        break;
                    case 20:
                        slot = c.B;
                        break;
                    default:
                        return 0;
                }
                var power = dtColorHelper.Select($"[Rounded] = '{slot}'").FirstOrDefault()?["Value"].ToString();
                return power != null ? int.Parse(power) : 0;
            }
            catch (Exception ex)
            {
                Log.Write("Error in Setbonus Red  = " + c.R);
                Log.Write(ex.Message, Color.Red);
            }
            return 0;
        }
        /// <summary>
        /// legendary piece worn
        /// </summary>
        /// <param name="num">What legendary 1 or 2 </param>
        /// <returns>EquipmentSlot legendar worn</returns>
        public static int Legendary(int num)
        {
            var c = WoW.GetBlockColor(3, 24);
            if (c.B != 0)
                return 0;
            try
            {
                c = WoW.GetBlockColor(5, 24);
                var slot = c.R;
                switch (num)
                {
                    case 1:
                        slot = c.R;
                        break;    
                    case 2:
                        slot = c.G;
                        break;
                    default:
                        return 0;
                }
                var power = dtColorHelper.Select($"[Rounded] = '{slot}'").FirstOrDefault()?["Value"].ToString();
                return power != null ? int.Parse(power) : 0;
            }
            catch (Exception ex)
            {
                Log.Write("Error in legendary Red  = " + c.R);

                Log.Write(ex.Message, Color.Red);
            }
            return 0;
        }

        /// <summary>
        /// Gets the pet health percent.
        /// </summary>
        /// <value>
        /// The pet health percent.
        /// </value>
        public static int PetHealthPercent
        {
            get
            {
                if (!HasPet) return 0;

                var c = GetBlockColor(13, 1);
                try
                {
                    var health = dtColorHelper.Select($"[Rounded] = '{c.R}'").FirstOrDefault()?["Value"].ToString();

                    // ReSharper disable once AssignNullToNotNullAttribute
                    return health != null ? int.Parse(health) : 100;
                }
                catch (Exception ex)
                {
                    Log.Write($"[PetHealth] Red = {c.R}");
                    Log.Write(ex.Message, Color.Red);
                    return 100;
                }
            }
        }

        /// <summary>
        /// Gets the last spell casted identifier.
        /// </summary>
        /// <value>
        /// The last spell casted identifier.
        /// </value>
        public static int LastSpellCastedID //returns the ID of the spell
        {
            get
            {
                var c1 = GetBlockColor(5, 7);
                var c2 = GetBlockColor(6, 7);
                try
                {
                    var red1 = (int) (float.Parse((string) dtColorHelper.Select($"[Rounded] = '{c1.R}'").FirstOrDefault()?["Value"])/10);
                    var green1 = (int) (float.Parse((string) dtColorHelper.Select($"[Rounded] = '{c1.G}'").FirstOrDefault()?["Value"])/10);
                    var blue1 = (int) (float.Parse((string) dtColorHelper.Select($"[Rounded] = '{c1.B}'").FirstOrDefault()?["Value"])/10);

                    var red2 = (int) (float.Parse((string) dtColorHelper.Select($"[Rounded] = '{c2.R}'").FirstOrDefault()?["Value"])/10);
                    var green2 = (int) (float.Parse((string) dtColorHelper.Select($"[Rounded] = '{c2.G}'").FirstOrDefault()?["Value"])/10);
                    var blue2 = (int) (float.Parse((string) dtColorHelper.Select($"[Rounded] = '{c2.B}'").FirstOrDefault()?["Value"])/10);

                    var strRed1 = "";
                    var strGreen1 = "";
                    var strBlue1 = "";
                    var strRed2 = "";
                    var strGreen2 = "";
                    var strBlue2 = "";

                    if (red1 != 10)
                    {
                        strRed1 = red1.ToString();
                    }
                    if (green1 != 10)
                    {
                        strGreen1 = green1.ToString();
                    }
                    if (blue1 != 10)
                    {
                        strBlue1 = blue1.ToString();
                    }
                    if (red2 != 10)
                    {
                        strRed2 = red2.ToString();
                    }
                    if (green2 != 10)
                    {
                        strGreen2 = green2.ToString();
                    }
                    if (blue2 != 10)
                    {
                        strBlue2 = blue2.ToString();
                    }

                    var spellText = strRed1 + strGreen1 + strBlue1 + strRed2 + strGreen2 + strBlue2;
                    //Log.Write("Last casted ID: " + spellText);
                    return RemoveTrailingZerosFromSpell(spellText);
                }
                catch (Exception ex)
                {
                    Log.Write($" Red1 = {c1.R} Green1 = {c1.G} Blue1 = {c1.B} Red2 = {c2.R} Green2 = {c2.G} Blue2 = {c2.B}");
                    Log.Write(ex.Message, Color.Red);
                    return 0;
                }
            }
        }
        
        private static int RemoveTrailingZerosFromSpell(string spellId)
        {
            if (int.Parse(spellId) == 0)
                return 0;

            while(spellId.EndsWith("0"))
            {
                spellId = spellId.Substring(0, spellId.Length - 1);
            }
            return int.Parse(spellId);
        }

        /// <summary>
        /// Gets the target casting spell identifier.
        /// </summary>
        /// <value>
        /// The target casting spell identifier.
        /// </value>
        public static int TargetCastingSpellID //returns the ID of the spell
        {
            get
            {
                if (!HasTarget) return 0;
                var c1 = GetBlockColor(7, 7);
                var c2 = GetBlockColor(8, 7);
                try
                {
                    var red1 = (int) (float.Parse((string) dtColorHelper.Select($"[Rounded] = '{c1.R}'").FirstOrDefault()?["Value"])/10);
                    var green1 = (int) (float.Parse((string) dtColorHelper.Select($"[Rounded] = '{c1.G}'").FirstOrDefault()?["Value"])/10);
                    var blue1 = (int) (float.Parse((string) dtColorHelper.Select($"[Rounded] = '{c1.B}'").FirstOrDefault()?["Value"])/10);

                    var red2 = (int) (float.Parse((string) dtColorHelper.Select($"[Rounded] = '{c2.R}'").FirstOrDefault()?["Value"])/10);
                    var green2 = (int) (float.Parse((string) dtColorHelper.Select($"[Rounded] = '{c2.G}'").FirstOrDefault()?["Value"])/10);
                    var blue2 = (int) (float.Parse((string) dtColorHelper.Select($"[Rounded] = '{c2.B}'").FirstOrDefault()?["Value"])/10);

                    var strRed1 = "";
                    var strGreen1 = "";
                    var strBlue1 = "";
                    var strRed2 = "";
                    var strGreen2 = "";
                    var strBlue2 = "";

                    if (red1 != 10)
                    {
                        strRed1 = red1.ToString();
                    }
                    if (green1 != 10)
                    {
                        strGreen1 = green1.ToString();
                    }
                    if (blue1 != 10)
                    {
                        strBlue1 = blue1.ToString();
                    }
                    if (red2 != 10)
                    {
                        strRed2 = red2.ToString();
                    }
                    if (green2 != 10)
                    {
                        strGreen2 = green2.ToString();
                    }
                    if (blue2 != 10)
                    {
                        strBlue2 = blue2.ToString();
                    }

                    var spellText = strRed1 + strGreen1 + strBlue1 + strRed2 + strGreen2 + strBlue2;
                    //Log.Write("Target casting spell ID: " + spellText);
                    return RemoveTrailingZerosFromSpell(spellText);
                }
                catch (Exception ex)
                {
                    Log.Write($" Red1 = {c1.R} Green1 = {c1.G} Blue1 = {c1.B} Red2 = {c2.R} Green2 = {c2.G} Blue2 = {c2.B}");
                    Log.Write(ex.Message, Color.Red);
                    return 0;
                }
            }
        }

        /// <summary>
        /// Gets the arena1 casting spell identifier.
        /// </summary>
        /// <value>
        /// The arena1 casting spell identifier.
        /// </value>
        public static int Arena1CastingSpellID //returns the ID of the spell
        {
            get
            {
                var c1 = GetBlockColor(9, 7);
                var c2 = GetBlockColor(10, 7);
                try
                {
                    var red1 = (int) (float.Parse((string) dtColorHelper.Select($"[Rounded] = '{c1.R}'").FirstOrDefault()?["Value"])/10);
                    var green1 = (int) (float.Parse((string) dtColorHelper.Select($"[Rounded] = '{c1.G}'").FirstOrDefault()?["Value"])/10);
                    var blue1 = (int) (float.Parse((string) dtColorHelper.Select($"[Rounded] = '{c1.B}'").FirstOrDefault()?["Value"])/10);

                    var red2 = (int) (float.Parse((string) dtColorHelper.Select($"[Rounded] = '{c2.R}'").FirstOrDefault()?["Value"])/10);
                    var green2 = (int) (float.Parse((string) dtColorHelper.Select($"[Rounded] = '{c2.G}'").FirstOrDefault()?["Value"])/10);
                    var blue2 = (int) (float.Parse((string) dtColorHelper.Select($"[Rounded] = '{c2.B}'").FirstOrDefault()?["Value"])/10);

                    var strRed1 = "";
                    var strGreen1 = "";
                    var strBlue1 = "";
                    var strRed2 = "";
                    var strGreen2 = "";
                    var strBlue2 = "";

                    if (red1 != 10)
                    {
                        strRed1 = red1.ToString();
                    }
                    if (green1 != 10)
                    {
                        strGreen1 = green1.ToString();
                    }
                    if (blue1 != 10)
                    {
                        strBlue1 = blue1.ToString();
                    }
                    if (red2 != 10)
                    {
                        strRed2 = red2.ToString();
                    }
                    if (green2 != 10)
                    {
                        strGreen2 = green2.ToString();
                    }
                    if (blue2 != 10)
                    {
                        strBlue2 = blue2.ToString();
                    }

                    var spellText = strRed1 + strGreen1 + strBlue1 + strRed2 + strGreen2 + strBlue2;
                    //Log.Write("Arena 1 casting spell ID: " + spellText);
                    return RemoveTrailingZerosFromSpell(spellText);
                }
                catch (Exception ex)
                {
                    Log.Write($" Red1 = {c1.R} Green1 = {c1.G} Blue1 = {c1.B} Red2 = {c2.R} Green2 = {c2.G} Blue2 = {c2.B}");
                    Log.Write(ex.Message, Color.Red);
                    return 0;
                }
            }
        }

        /// <summary>
        /// Gets the arena2 casting spell identifier.
        /// </summary>
        /// <value>
        /// The arena2 casting spell identifier.
        /// </value>
        public static int Arena2CastingSpellID //returns the ID of the spell
        {
            get
            {
                var c1 = GetBlockColor(11, 7);
                var c2 = GetBlockColor(12, 7);
                try
                {
                    var red1 = (int) (float.Parse((string) dtColorHelper.Select($"[Rounded] = '{c1.R}'").FirstOrDefault()?["Value"])/10);
                    var green1 = (int) (float.Parse((string) dtColorHelper.Select($"[Rounded] = '{c1.G}'").FirstOrDefault()?["Value"])/10);
                    var blue1 = (int) (float.Parse((string) dtColorHelper.Select($"[Rounded] = '{c1.B}'").FirstOrDefault()?["Value"])/10);

                    var red2 = (int) (float.Parse((string) dtColorHelper.Select($"[Rounded] = '{c2.R}'").FirstOrDefault()?["Value"])/10);
                    var green2 = (int) (float.Parse((string) dtColorHelper.Select($"[Rounded] = '{c2.G}'").FirstOrDefault()?["Value"])/10);
                    var blue2 = (int) (float.Parse((string) dtColorHelper.Select($"[Rounded] = '{c2.B}'").FirstOrDefault()?["Value"])/10);

                    var strRed1 = "";
                    var strGreen1 = "";
                    var strBlue1 = "";
                    var strRed2 = "";
                    var strGreen2 = "";
                    var strBlue2 = "";

                    if (red1 != 10)
                    {
                        strRed1 = red1.ToString();
                    }
                    if (green1 != 10)
                    {
                        strGreen1 = green1.ToString();
                    }
                    if (blue1 != 10)
                    {
                        strBlue1 = blue1.ToString();
                    }
                    if (red2 != 10)
                    {
                        strRed2 = red2.ToString();
                    }
                    if (green2 != 10)
                    {
                        strGreen2 = green2.ToString();
                    }
                    if (blue2 != 10)
                    {
                        strBlue2 = blue2.ToString();
                    }

                    var spellText = strRed1 + strGreen1 + strBlue1 + strRed2 + strGreen2 + strBlue2;
                    //Log.Write("Arena 2 casting spell ID: " + spellText);
                    return RemoveTrailingZerosFromSpell(spellText);
                }
                catch (Exception ex)
                {
                    Log.Write($" Red1 = {c1.R} Green1 = {c1.G} Blue1 = {c1.B} Red2 = {c2.R} Green2 = {c2.G} Blue2 = {c2.B}");
                    Log.Write(ex.Message, Color.Red);
                    return 0;
                }
            }
        }

        /// <summary>
        /// Gets the arena3 casting spell identifier.
        /// </summary>
        /// <value>
        /// The arena3 casting spell identifier.
        /// </value>
        public static int Arena3CastingSpellID //returns the ID of the spell
        {
            get
            {
                var c1 = GetBlockColor(13, 7);
                var c2 = GetBlockColor(14, 7);
                try
                {
                    var red1 = (int) (float.Parse((string) dtColorHelper.Select($"[Rounded] = '{c1.R}'").FirstOrDefault()?["Value"])/10);
                    var green1 = (int) (float.Parse((string) dtColorHelper.Select($"[Rounded] = '{c1.G}'").FirstOrDefault()?["Value"])/10);
                    var blue1 = (int) (float.Parse((string) dtColorHelper.Select($"[Rounded] = '{c1.B}'").FirstOrDefault()?["Value"])/10);

                    var red2 = (int) (float.Parse((string) dtColorHelper.Select($"[Rounded] = '{c2.R}'").FirstOrDefault()?["Value"])/10);
                    var green2 = (int) (float.Parse((string) dtColorHelper.Select($"[Rounded] = '{c2.G}'").FirstOrDefault()?["Value"])/10);
                    var blue2 = (int) (float.Parse((string) dtColorHelper.Select($"[Rounded] = '{c2.B}'").FirstOrDefault()?["Value"])/10);

                    var strRed1 = "";
                    var strGreen1 = "";
                    var strBlue1 = "";
                    var strRed2 = "";
                    var strGreen2 = "";
                    var strBlue2 = "";

                    if (red1 != 10)
                    {
                        strRed1 = red1.ToString();
                    }
                    if (green1 != 10)
                    {
                        strGreen1 = green1.ToString();
                    }
                    if (blue1 != 10)
                    {
                        strBlue1 = blue1.ToString();
                    }
                    if (red2 != 10)
                    {
                        strRed2 = red2.ToString();
                    }
                    if (green2 != 10)
                    {
                        strGreen2 = green2.ToString();
                    }
                    if (blue2 != 10)
                    {
                        strBlue2 = blue2.ToString();
                    }

                    var spellText = strRed1 + strGreen1 + strBlue1 + strRed2 + strGreen2 + strBlue2;
                    //Log.Write("Arena 3 casting spell ID: " + spellText);
                    return RemoveTrailingZerosFromSpell(spellText);
                }
                catch (Exception ex)
                {
                    Log.Write($" Red1 = {c1.R} Green1 = {c1.G} Blue1 = {c1.B} Red2 = {c2.R} Green2 = {c2.G} Blue2 = {c2.B}");
                    Log.Write(ex.Message, Color.Red);
                    return 0;
                }
            }
        }

        private static int GetNumberNameplates => 1;

        private static bool IsLeftShiftDown => false;

        /// <summary>
        /// Gets a value indicating whether this instance has pet.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has pet; otherwise, <c>false</c>.
        /// </value>
        public static bool HasPet
        {
            get
            {
                var c = GetBlockColor(12, 1);
                return c.R == Color.Red.R && c.G == Color.Red.G && c.B == Color.Red.B;
            }
        }

        /// <summary>
        /// Gets the target health percent.
        /// </summary>
        /// <value>
        /// The target health percent.
        /// </value>
        public static int TargetHealthPercent
        {
            get
            {
                var c = GetBlockColor(3, 1);
                try
                {
                    var health = dtColorHelper.Select($"[Rounded] = '{c.R}'").FirstOrDefault()?["Value"].ToString();

                    // ReSharper disable once AssignNullToNotNullAttribute
                    return health != null ? int.Parse(health) : 50;
                }
                catch (Exception ex)
                {
                    Log.Write($"[Target Health] Red = {c.R} Green = {c.G}");
                    Log.Write(ex.Message, Color.Red);
                    return 100;
                }
            }
        }

        /// <summary>
        /// Gets the level.
        /// </summary>
        /// <value>
        /// The level.
        /// </value>
        public static int Level
        {
            get
            {
                var c = GetBlockColor(1, 1);

                try
                {
                    Log.WriteDirectlyToLogFile($"Green = {c.G} Blue = {c.B}");
                    if (c.B == 255 && c.B == 255)
                        return 0;

                    if (c.B != 0)
                    {
                        var lvl = dtColorHelper.Select($"[Rounded] = '{c.B}'").FirstOrDefault()?["Value"].ToString();
                        if (lvl != null)
                            return int.Parse(lvl) + 100;
                    }
                    else
                    {
                        var lvl = dtColorHelper.Select($"[Rounded] = '{c.G}'").FirstOrDefault()?["Value"].ToString();
                        if (lvl != null)
                            return int.Parse(lvl);
                    }
                }
                catch (Exception ex)
                {
                    Log.Write($"Green = {c.G} Blue = {c.B}");
                    Log.Write(ex.Message, Color.Red);
                }
                return 0;
            }
        }

        /// <summary>
        /// Gets the power.
        /// </summary>
        /// <value>
        /// The power.
        /// </value>
        public static int Power
        {
            get
            {
                var c = GetBlockColor(2, 1);

                try
                {
                    Log.WriteDirectlyToLogFile($"Red = {c.R} Green = {c.G}");
                    if (c.G == 255 && c.R == 255)
                        return 0;

                    if (c.G != 0)
                    {
                        var power = dtColorHelper.Select($"[Rounded] = '{c.G}'").FirstOrDefault()?["Value"].ToString();
                        if (power != null)
                            return int.Parse(power) + 100;
                    }
                    else
                    {
                        var power = dtColorHelper.Select($"[Rounded] = '{c.R}'").FirstOrDefault()?["Value"].ToString();
                        if (power != null)
                            return int.Parse(power);
                    }
                }
                catch (Exception ex)
                {
                    Log.Write($"Red = {c.R} Green = {c.G}");
                    Log.Write(ex.Message, Color.Red);
                }
                return 0;
            }
        }

        /// <summary>
        /// Gets the focus.
        /// </summary>
        /// <value>
        /// The focus.
        /// </value>
        public static int Focus => Power;
        /// <summary>
        /// Gets the mana.
        /// </summary>
        /// <value>
        /// The mana.
        /// </value>
        public static int Mana => Power;
        /// <summary>
        /// Gets the energy.
        /// </summary>
        /// <value>
        /// The energy.
        /// </value>
        public static int Energy => Power;
        /// <summary>
        /// Gets the rage.
        /// </summary>
        /// <value>
        /// The rage.
        /// </value>
        public static int Rage => Power;
        /// <summary>
        /// Gets the pain.
        /// </summary>
        /// <value>
        /// The pain.
        /// </value>
        public static int Pain => Power;
        /// <summary>
        /// Gets the fury.
        /// </summary>
        /// <value>
        /// The fury.
        /// </value>
        public static int Fury => Power;
        /// <summary>
        /// Gets the runic power.
        /// </summary>
        /// <value>
        /// The runic power.
        /// </value>
        public static int RunicPower => Power;
        /// <summary>
        /// Gets the current astral power.
        /// </summary>
        /// <value>
        /// The current astral power.
        /// </value>
        public static int CurrentAstralPower => Power;
        /// <summary>
        /// Gets the maelstrom.
        /// </summary>
        /// <value>
        /// The maelstrom.
        /// </value>
        public static int Maelstrom => Power;
        /// <summary>
        /// Gets the insanity.
        /// </summary>
        /// <value>
        /// The insanity.
        /// </value>
        public static int Insanity => Power;

        /// <summary>
        /// Gets the haste percent.
        /// </summary>
        /// <value>
        /// The haste percent.
        /// </value>
        public static int HastePercent
        {
            get
            {
                var c = GetBlockColor(10, 1);

                try
                {
                    if (c.R == 0 || c.R == 255)
                    {
                        Log.WriteDirectlyToLogFile($"Red = {c.R} Green = {c.G}Blue = {c.B}");
                        var power1 = dtColorHelper.Select($"[Rounded] = '{c.R}'").FirstOrDefault()?["Value"].ToString();
                        var power2 = dtColorHelper.Select($"[Rounded] = '{c.G}'").FirstOrDefault()?["Value"].ToString();
                        var power3 = dtColorHelper.Select($"[Rounded] = '{c.B}'").FirstOrDefault()?["Value"].ToString();
                        if (int.Parse(power1) == 100)
                            return int.Parse(power2) * 100 + int.Parse(power3);
                        else
                            return -(int.Parse(power2) * 100 + int.Parse(power3));
                    }
                }
                catch (Exception ex)
                {
                    Log.Write($"Error in haste Red = {c.R} Green = {c.G} Blue = {c.B}");
                    Log.Write(ex.Message, Color.Red);
                }
                return 0;
            }
        }

        /// <summary>
        /// Gets a value indicating whether [wow window has focus].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [wow window has focus]; otherwise, <c>false</c>.
        /// </value>
        /// <exception cref="Exception">World of warcraft is not detected / running, please login before attempting to restart the bot</exception>
        public static bool WowWindowHasFocus
        {
            get
            {
                var activatedHandle = GetForegroundWindow();
                if (activatedHandle == IntPtr.Zero)
                {
                    return false; // No window is currently activated
                }

                int activeProcId;
                GetWindowThreadProcessId(activatedHandle, out activeProcId);

                if (Process == null)
                {
                    throw new Exception("World of warcraft is not detected / running, please login before attempting to restart the bot");
                }

                return activeProcId == Process.Id;
            }
        }

        /// <summary>
        /// Gets a value indicating whether [automatic atacking].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [automatic atacking]; otherwise, <c>false</c>.
        /// </value>
        public static bool AutoAtacking
        {
            get
            {
                var c = GetBlockColor(2, 7);
                return c.R == Color.Red.R && c.G == Color.Red.G && c.B == Color.Red.B;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is moving.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is moving; otherwise, <c>false</c>.
        /// </value>

        public static bool IsMoving
        {
            get
            {
                var c = WoW.GetBlockColor(1, 7);
                return (c.R == Color.Red.R) && (c.B == Color.Blue.B);
            }
        }
        public static bool IsMounted
        {
            get
            {
                var c = WoW.GetBlockColor(1, 7);
                return (c.G == Color.Green.G && (c.B == Color.Blue.B));
            }
        }
        /// <summary>
        /// Gets a value indicating whether [target is player].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [target is player]; otherwise, <c>false</c>.
        /// </value>
        public static bool TargetIsPlayer
        {
            get
            {
                var c = GetBlockColor(3, 7);
                return c.R == Color.Red.R && c.G == Color.Red.G && c.B == Color.Red.B;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="WoW"/> is flag.
        /// </summary>
        /// <value>
        ///   <c>true</c> if flag; otherwise, <c>false</c>.
        /// </value>
        public static bool IsOutdoors
        {
            get
            {
                var c = GetBlockColor(4, 7);
                return c.R == Color.Red.R && c.G == Color.Red.G && c.B == Color.Red.B;
            }
        }
        public static bool HasBossTarget
        {
            get
            {
                var c = GetBlockColor(7, 1);
                return c.R == Color.Blue.R && c.G == Color.Blue.G && c.B == Color.Blue.B;
            }
        }
        
        public static bool IsBoss => HasBossTarget;

        /// <summary>
        /// Gets the wild imps count.
        /// </summary>
        /// <value>
        /// The wild imps count.
        /// </value>
        public static int WildImpsCount
        {
            get
            {
                var c = GetBlockColor(14, 1);

                try
                {
                    var Red = dtColorHelper.Select($"[Rounded] = '{c.R}'").FirstOrDefault()["Value"].ToString();

                    return int.Parse(Red);
                }
                catch (Exception ex)
                {
                    Log.Write("Error: Wild Imps " + ex.Message, Color.Red);
                }

                return 0;
            }
        }

        /// <summary>
        /// Gets the dreadstalkers count.
        /// </summary>
        /// <value>
        /// The dreadstalkers count.
        /// </value>
        public static int DreadstalkersCount
        {
            get
            {
                var c = GetBlockColor(14, 1);

                try
                {
                    var Blue = dtColorHelper.Select($"[Rounded] = '{c.B}'").FirstOrDefault()["Value"].ToString();

                    return int.Parse(Blue);
                }
                catch (Exception ex)
                {
                    Log.Write("Error: Dreadstalkers " + ex.Message, Color.Red);
                }
                return 0;
            }
        }

        private static void TargetPartyMember(int id)
        {
            if (id <= 0)
                throw new Exception("Party members go from 1 - 5");
            id = id - 1;
            Mouse.LeftClick(958, 3 + 20*id);
        }

        public static void TargetNearestEnemy()
        {
            KeyPressRelease(Keys.Tab);
        }

        /// <summary>
        /// Wases the last casted.
        /// </summary>
        /// <param name="spellBookSpellName">Name of the spell book spell.</param>
        /// <returns></returns>
        public static bool WasLastCasted(string spellBookSpellName)
        {
            var spell = SpellBook.Spells.FirstOrDefault(s => s.SpellName == spellBookSpellName);
            var spellId = spell.SpellId;

            return spellId == LastSpellCastedID;
        }

        /// <summary>
        /// Speaks the specified words.
        /// </summary>
        /// <param name="words">The words.</param>
        public static void Speak(string words)
        {
            synthesizer.SpeakAsync(words);
        }

        /// <summary>
        /// Initializes the specified wow process.
        /// </summary>
        /// <param name="wowProcess">The wow process.</param>
        public static void Initialize(Process wowProcess)
        {
            random = new Random();
            synthesizer = new SpeechSynthesizer();
            synthesizer.Volume = 100; // 0...100
            synthesizer.Rate = 2; // -10...10
            synthesizer.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Adult);
            
            Process = wowProcess;

            Log.Write("Successfully connected to WoW with process ID: " + Process.Id, Color.Green);

            var is64 = Process.ProcessName.Contains("64");

            Log.Write($"WoW Version: {Version} (x{(is64 ? "64" : "86")})", Color.Gray);

            var wowRectangle = new Rectangle();
            GetWindowRect(Process.MainWindowHandle, ref wowRectangle);
            Log.Write($"WoW Screen Resolution: {wowRectangle.Width}x{wowRectangle.Height}", Color.Gray);

            if (ConfigFile.ReadValue("PixelMagic", "AddonName") == "")
            {
                Log.Write("This is the first time you have run the program, please specify a name you would like the PixelMagic addon to use");
                Log.Write("this can be anything you like (letters only no numbers)");

                while (ConfigFile.ReadValue("PixelMagic", "AddonName") == "")
                {
                    var f = new frmSelectAddonName();
                    f.ShowDialog();
                }
            }

            Log.Write($"Addon Name set to: [{ConfigFile.ReadValue("PixelMagic", "AddonName")}]", Color.Blue);

            dtColorHelper = new DataTable();
            dtColorHelper.Columns.Add("Percent");
            dtColorHelper.Columns.Add("Unrounded");
            dtColorHelper.Columns.Add("Rounded");
            dtColorHelper.Columns.Add("Value");

            for (var i = 0; i <= 99; i++)
            {
                var drNew = dtColorHelper.NewRow();
                drNew["Percent"] = i < 10 ? "0.0" + i : "0." + i;
                drNew["Unrounded"] = double.Parse(drNew["Percent"].ToString())*255;
                drNew["Rounded"] = Math.Round(double.Parse(drNew["Percent"].ToString())*255, 0);
                drNew["Value"] = i;
                dtColorHelper.Rows.Add(drNew);
            }
            {
                var drNew = dtColorHelper.NewRow();
                drNew["Percent"] = "1.00";
                drNew["Unrounded"] = "255";
                drNew["Rounded"] = "255";
                drNew["Value"] = 100;
                dtColorHelper.Rows.Add(drNew);
            }
            {
                var drNew = dtColorHelper.NewRow();
                drNew["Percent"] = "1.00";
                drNew["Unrounded"] = "255";
                drNew["Rounded"] = "77"; // Manually added from testing this color sometimes shows up 
                drNew["Value"] = 30;
                dtColorHelper.Rows.Add(drNew);
            }
            {
                var drNew = dtColorHelper.NewRow();
                drNew["Percent"] = "1.00";
                drNew["Unrounded"] = "255";
                drNew["Rounded"] = "179"; // Manually added from testing this color sometimes shows up 
                drNew["Value"] = 70;
                dtColorHelper.Rows.Add(drNew);
            }
        }

        /// <summary>
        /// Targets the has buff.
        /// </summary>
        /// <param name="buffName">Name of the buff.</param>
        /// <returns></returns>
        public static bool TargetHasBuff(string buffName)
        {
            var aura = SpellBook.Auras.FirstOrDefault(s => s.AuraName == buffName);

            if (aura == null)
            {
                Log.Write($"[TargetHasBuff] cant find buff '{buffName}' in Spell Book");
                return false;
            }

            return TargetHasBuff(aura.InternalAuraNo);
        }

        /// <summary>
        /// Targets the has buff.
        /// </summary>
        /// <param name="auraNoInArrayOfAuras">The aura no in array of auras.</param>
        /// <returns></returns>
        private static bool TargetHasBuff(int auraNoInArrayOfAuras)
        {
            var c = GetBlockColor(auraNoInArrayOfAuras, 6);
            return c.R != 255  && c.B != 255;
        }

        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hWnd, ref Rectangle rect);

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public static void Dispose()
        {
            Log.Write("Disposing of WoW Process...");
            Process.Close();
            Process = null;
            Log.Write("Disposing of WoW Process Completed.");
        }

        private static bool IsSpellOnCooldown(int spellNoInArrayOfSpells) // This will take the spell no from the array of spells, 1, 2, 3 ..... n
        {
            var c = GetBlockColor(spellNoInArrayOfSpells, 2);
            return c.R == 0;
        }

        /// <summary>
        /// Determines whether [is spell on cooldown] [the specified spell book spell name].
        /// </summary>
        /// <param name="spellBookSpellName">Name of the spell book spell.</param>
        /// <returns>
        ///   <c>true</c> if [is spell on cooldown] [the specified spell book spell name]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsSpellOnCooldown(string spellBookSpellName)
        {
            var spell = SpellBook.Spells.FirstOrDefault(s => s.SpellName == spellBookSpellName);

            if (spell != null) return IsSpellOnCooldown(spell.InternalSpellNo);

            Log.Write($"[IsSpellOnCooldown] Unable to find spell with name '{spellBookSpellName}' in Spell Book");
            return false;
        }

        /// <summary>
        /// Spells the cooldown time remaining.
        /// </summary>
        /// <param name="spellNoInArrayOfSpells">The spell no in array of spells.</param>
        /// <returns></returns>
        public static int SpellCooldownTimeRemaining(int spellNoInArrayOfSpells)
        {
            var c = GetBlockColor(spellNoInArrayOfSpells, 2);

            try
            {
                Log.WriteDirectlyToLogFile($"Red = {c.R} Green = {c.G} Blue = {c.B}");
                if (c.R == 255)
                    return 0;
                if (c.R == 0)
                {
                    var power = dtColorHelper.Select($"[Rounded] = '{c.G}'").FirstOrDefault()?["Value"].ToString();
                    var power2 = dtColorHelper.Select($"[Rounded] = '{c.B}'").FirstOrDefault()?["Value"].ToString();
                    return power != null && power2 != null ? int.Parse(power) * 100 + int.Parse(power2) : 0;
                }
            }
            catch (Exception ex)
            {
                Log.Write($"Failure in Spell Cooldown Remaining Red = {c.R} Green = {c.G} Blue = {c.B}");
                Log.Write(ex.Message, Color.Red);
            }

            return 0;
        }
        /// <summary>
        /// Talent By Row
        /// </summary>
        /// <param name="Talent">The row a spell is on. </param>
        /// <returns></returns>
        public static int Talent(int row)
        {
            var c = WoW.GetBlockColor(3, 24);
            if (c.B != 0) return 0;
            try
            {
                switch (row)
                {
                    case 1:
                         c = GetBlockColor(1, 24);
                        return int.Parse(dtColorHelper.Select($"[Rounded] = '{c.R}'").FirstOrDefault()["Value"].ToString());
                    case 2:
                        c = GetBlockColor(1, 24);
                        return int.Parse(dtColorHelper.Select($"[Rounded] = '{c.G}'").FirstOrDefault()["Value"].ToString());
                    case 3:
                         c = GetBlockColor(1, 24);
                        return int.Parse(dtColorHelper.Select($"[Rounded] = '{c.B}'").FirstOrDefault()["Value"].ToString());
                    case 4:
                        c = GetBlockColor(2, 24);
                        return int.Parse(dtColorHelper.Select($"[Rounded] = '{c.R}'").FirstOrDefault()["Value"].ToString());
                    case 5:
                        c = GetBlockColor(2, 24);
                        return int.Parse(dtColorHelper.Select($"[Rounded] = '{c.G}'").FirstOrDefault()["Value"].ToString());
                    case 6:
                        c = GetBlockColor(2, 24);
                        return int.Parse(dtColorHelper.Select($"[Rounded] = '{c.B}'").FirstOrDefault()["Value"].ToString());
                    case 7:
                        c = GetBlockColor(3, 24);
                        return int.Parse(dtColorHelper.Select($"[Rounded] = '{c.R}'").FirstOrDefault()["Value"].ToString());
                }
            }
            catch (Exception ex)
            {
                Log.Write($"Error in Talents Red = {c.R} Green = {c.G} Blue = {c.G}");
                Log.Write(ex.Message, Color.Red);
            }

            return 0;
        }
        /// <summary>
        /// Returns Player Race
        /// </summary>
        /// <param name="PlayerRace">Returns english Race</param>
        /// <returns></returns>
        public static string PlayerRace
        {
            get
            {
                var c = GetBlockColor(3, 24);

                try
                {
                    if (c.B != 0) return "none";
                    
                    string[] Race = new string[]
                    {"None","Human","Dwarf", "NightElf", "Gnome", "Dreanei", "Pandaren", "Orc", "Undead", "Tauren", "Troll", "BloodElf", "Goblin", "Worgen" ,"none"};
                    Log.WriteDirectlyToLogFile($"Green = {c.G}");
                    var race = dtColorHelper.Select($"[Rounded] = '{c.G}'").FirstOrDefault()?["Value"].ToString();
                    if (int.Parse(race) > 13)
                        return "none";
                    return race != null || int.Parse(race) > 13 ? Race[int.Parse(race)] : "none";

                }
                catch (Exception ex)
                {
                    Log.Write($"Error in race  Green = {c.G}");
                    Log.Write(ex.Message, Color.Red);
                }
                return "none";
            }
        }
        /// <summary>
        /// Returns Player Spec
        /// </summary>
        /// <param name="PlayerSpec">Returns english Spec string</param>
        /// <returns></returns>
        public static string PlayerSpec
        {
            get
            {
                var c = GetBlockColor(4, 24);
                try
                {
                    if (c.B == 0) return "none"; 
                    string[] Spec = new string[] {"None","Blood", "Frost", "Unholy", "Havoc", "Vengeance", "Balance", "Feral", "Guardian", "Restoration", "Beast Mastery", "Marksmanship", "Survival", "Arcane", "Fire", "Frost", "Brewmaster", "Mistweaver", "Windwalker", "Holy", "Protection", "Retribution", "Discipline", "HolyPriest", "Shadow", "Assassination", "Outlaw", "Subtlety", "Elemental", "Enhancement", "RestorationShaman", "Affliction", "Arms", "Fury", "Protection","Demonology","Destruction","none"};
                    var spec = dtColorHelper.Select($"[Rounded] = '{c.R}'").FirstOrDefault()?["Value"].ToString();
                    return spec != null && int.Parse(spec) < 37 ? Spec[int.Parse(spec)] : "none";
                }
                catch (Exception ex)
                {
                    Log.Write("Error in Spec  Green = " + c.R);

                    Log.Write(ex.Message, Color.Red);
                }
                return "none";
            }
        }
        /// <summary>
        /// Spells the cooldown time remaining.
        /// </summary>
        /// <param name="spellBookSpellName">Name of the spell book spell.</param>
        /// <returns></returns>
        public static int SpellCooldownTimeRemaining(string spellBookSpellName)
        {
            var spell = SpellBook.Spells.FirstOrDefault(s => s.SpellName == spellBookSpellName);

            if (spell != null) return SpellCooldownTimeRemaining(spell.InternalSpellNo);

            Log.Write($"[SpellCooldownTimeRemaining] Unable to find spell with name '{spellBookSpellName}' in Spell Book");
            return 0;
        }

        private static bool IsSpellOnGCD(int spellNoInArrayOfSpells)
        {
            var blockColor = GetBlockColor(spellNoInArrayOfSpells, 2);
            return blockColor.R == Color.Red.R && blockColor.G == Color.Red.G && blockColor.B == Color.Red.B;
        }

        /// <summary>
        /// Determines whether [is spell on GCD] [the specified spell book spell name].
        /// </summary>
        /// <param name="spellBookSpellName">Name of the spell book spell.</param>
        /// <returns>
        ///   <c>true</c> if [is spell on GCD] [the specified spell book spell name]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsSpellOnGCD(string spellBookSpellName)
        {
            var spell = SpellBook.Spells.FirstOrDefault(s => s.SpellName == spellBookSpellName);
            var flag = spell == null;
            bool result;
            if (flag)
            {
                Log.Write($"[IsSpellOnCooldown] Unable to find spell with name '{spellBookSpellName}' in Spell Book");
                result = false;
            }
            else
            {
                result = IsSpellOnGCD(spell.InternalSpellNo);
            }
            return result;
        }

        private static bool IsSpellInRange(int spellNoInArrayOfSpells) // This will take the spell no from the array of spells, 1, 2, 3 ..... n
        {
            var c = GetBlockColor(spellNoInArrayOfSpells, 3);
            return c.R == Color.Red.R && c.G == Color.Red.G && c.B == Color.Red.B;
        }

        /// <summary>
        /// Determines whether [is spell in range] [the specified spell book spell name].
        /// </summary>
        /// <param name="spellBookSpellName">Name of the spell book spell.</param>
        /// <returns>
        ///   <c>true</c> if [is spell in range] [the specified spell book spell name]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsSpellInRange(string spellBookSpellName)
        {
            var spell = SpellBook.Spells.FirstOrDefault(s => s.SpellName == spellBookSpellName);

            if (spell != null) return IsSpellInRange(spell.InternalSpellNo);

            Log.Write($"[IsSpellInRange] Unable to find spell with name '{spellBookSpellName}' in Spell Book");
            return false;
        }

        private static bool CanCast(int spellNoInArrayOfSpells, bool checkIfPlayerIsCasting = true, bool checkIfSpellIsOnCooldown = true, bool checkIfSpellIsInRange = true,
            bool checkSpellCharges = true, bool checkIfTargetIsVisible = true)
        {
            if (checkIfPlayerIsCasting)
                if (PlayerIsCasting)
                    return false;

            if (checkIfSpellIsOnCooldown)
                if (IsSpellOnCooldown(spellNoInArrayOfSpells))
                    return false;

            if (checkIfSpellIsInRange)
                if (IsSpellInRange(spellNoInArrayOfSpells) == false)
                    return false;

            if (checkSpellCharges)
                if (PlayerSpellCharges(spellNoInArrayOfSpells) <= 0)
                    return false;

            if (checkIfTargetIsVisible)
                if (TargetIsVisible == false)
                    return false;

            return true;
        }

        /// <summary>
        /// Determines whether this instance can cast the specified spell book spell name.
        /// </summary>
        /// <param name="spellBookSpellName">Name of the spell book spell.</param>
        /// <param name="checkIfPlayerIsCasting">if set to <c>true</c> [check if player is casting].</param>
        /// <param name="checkIfSpellIsOnCooldown">if set to <c>true</c> [check if spell is on cooldown].</param>
        /// <param name="checkIfSpellIsInRange">if set to <c>true</c> [check if spell is in range].</param>
        /// <param name="checkSpellCharges">if set to <c>true</c> [check spell charges].</param>
        /// <param name="checkIfTargetIsVisible">if set to <c>true</c> [check if target is visible].</param>
        /// <returns>
        ///   <c>true</c> if this instance can cast the specified spell book spell name; otherwise, <c>false</c>.
        /// </returns>
        public static bool CanCast(string spellBookSpellName, bool checkIfPlayerIsCasting = true, bool checkIfSpellIsOnCooldown = true, bool checkIfSpellIsInRange = false,
            bool checkSpellCharges = false, bool checkIfTargetIsVisible = true)
        {
            var spell = SpellBook.Spells.FirstOrDefault(s => s.SpellName == spellBookSpellName);

            if (spell == null)
            {
                Log.Write($"[CanCast] Unable to find spell with name '{spellBookSpellName}' in Spell Book");
                return false;
            }

            var ret = CanCast(spell.InternalSpellNo, checkIfPlayerIsCasting, checkIfSpellIsOnCooldown, checkIfSpellIsInRange, checkSpellCharges, checkIfTargetIsVisible);

            return ret;
        }

        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        private static void SendKey(Keys key, string spellId, int milliseconds = 50, string spellName = null)
        {
            if (spellName == null)
            {
                Log.Write("Sending keypress: " + key, Color.Gray);
            }
            else
            {
                string myKeyMapping = ConfigFile.ReadValue("Keybinds-" + Path.GetFileName(frmMain.combatRoutine.FileName).Replace(" ", "").ToLower(), spellId);
                if (myKeyMapping.Trim() == "")
                    Log.Write("Casting spell: " + spellName + ", power = " + Power + ", unit power = " + UnitPower + ", Key = " + key, Color.Black);
                else
                {
                    key = ParseEnum<Keys>(myKeyMapping);
                    Log.Write("Casting spell: " + spellName + ", power = " + Power + ", unit power = " + UnitPower + ", Key = " + key, Color.Black);
                }
            }

            if (milliseconds < 50)
                milliseconds = 50;

            milliseconds = milliseconds + random.Next(50);

            KeyDown(key);
            Thread.Sleep(milliseconds);
            KeyUp(key);
        }

        /// <summary>
        /// Casts the spell on me.
        /// </summary>
        /// <param name="spellName">Name of the spell.</param>
        public static void CastSpellOnMe(string spellName)
        {
            CastSpell(spellName);

            Mouse.RightRelease();
            Thread.Sleep(50);
            Mouse.LeftClick(960, 540);
            Thread.Sleep(50);
            Mouse.RightDown();
        }

        /// <summary>
        /// Sends the key at location.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public static void SendKeyAtLocation(Keys key, int x, int y)
        {
            Log.Write($"Sending keypress {key} at location: x = {x}, y = {y}", Color.Gray);

            KeyDown(key);
            Thread.Sleep(50);
            KeyUp(key);

            Mouse.RightRelease();
            Thread.Sleep(50);
            Mouse.LeftClick(x, y);
            Thread.Sleep(50);
            Mouse.RightDown();
        }

        /// <summary>
        /// Sends the macro.
        /// </summary>
        /// <param name="macro">The macro.</param>
        internal static void SendMacro(string macro)
        {
            Log.Write("Sending macro: " + macro, Color.Gray);

            KeyPressRelease(Keys.Enter);
            Thread.Sleep(100);
            Write(macro);
            Thread.Sleep(100);
            KeyPressRelease(Keys.Enter);
        }

        public static int PlayerBuffStacks(int auraNoInArrayOfAuras)
        {
            var c = GetBlockColor(auraNoInArrayOfAuras, 8);

            try
            {
                if (c.R == 255)
                    return 0;
                // ReSharper disable once PossibleNullReferenceException
                var stacks = dtColorHelper.Select($"[Rounded] = '{c.R}'").FirstOrDefault()["Value"].ToString();
                return int.Parse(stacks);
            }
            catch (Exception ex)
            {
                Log.Write("Failed to find buff stacks for color G = " + c.R, Color.Red);
                Log.Write("Error: " + ex.Message, Color.Red);
            }

            return 0;
        }

        public static int PlayerBuffStacks(string auraName)
        {
            var aura = SpellBook.Auras.FirstOrDefault(s => s.AuraName == auraName);

            if (aura == null)
            {
                Log.Write($"[PlayerBuffStacks] Unable to find buff with name '{auraName}' in Spell Book");
                return -1;
            }

            return PlayerBuffStacks(aura.InternalAuraNo);
        }

        /// <summary>
        /// Pets the buff stacks.
        /// </summary>
        /// <param name="auraNoInArrayOfAuras">The aura no in array of auras.</param>
        /// <returns></returns>
        public static int PetBuffStacks(int auraNoInArrayOfAuras)
        {
            var c = GetBlockColor(auraNoInArrayOfAuras, 10);

            try
            {
                if (c.R == 255)
                    return 0;
                // ReSharper disable once PossibleNullReferenceException
                var stacks = dtColorHelper.Select($"[Rounded] = '{c.R}'").FirstOrDefault()["Value"].ToString();
                return int.Parse(stacks);
            }
            catch (Exception ex)
            {
                Log.Write("Failed to find buff stacks for color G = " + c.R, Color.Red);
                Log.Write("Error: " + ex.Message, Color.Red);
            }

            return 0;
        }

        public static void Reload()
        {
            SendMacro("/reload");
        }

        /// <summary>
        /// Pets the buff stacks.
        /// </summary>
        /// <param name="auraName">Name of the aura.</param>
        /// <returns></returns>
        public static int PetBuffStacks(string auraName)
        {
            var aura = SpellBook.Auras.FirstOrDefault(s => s.AuraName == auraName);

            if (aura == null)
            {
                Log.Write($"[PlayerBuffStacks] Unable to find buff with name '{auraName}' in Spell Book");
                return -1;
            }

            return PetBuffStacks(aura.InternalAuraNo);
        }

        public static int TargetDebuffTimeRemaining(int auraNoInArrayOfAuras)
        {
            var c = GetBlockColor(auraNoInArrayOfAuras, 4);

            try
            {
                if (c.R == 255)
                    return 0;
                // ReSharper disable once PossibleNullReferenceException
                var power = dtColorHelper.Select($"[Rounded] = '{c.G}'").FirstOrDefault()?["Value"].ToString();
                var power2 = dtColorHelper.Select($"[Rounded] = '{c.B}'").FirstOrDefault()?["Value"].ToString();
                return power != null && power2 != null ? int.Parse(power) * 100 + int.Parse(power2) : 0;

            }
            catch (Exception ex)
            {
                Log.Write("Failed to find Debuff Time Remaining for color G = " + c.G, Color.Red);
                Log.Write("Error: " + ex.Message, Color.Red);
            }

            return 0;
        }

        /// <summary>
        /// Targets the debuff time remaining.
        /// </summary>
        /// <param name="debuffName">Name of the debuff.</param>
        /// <returns></returns>
        public static int TargetDebuffTimeRemaining(string debuffName)
        {
            var aura = SpellBook.Auras.FirstOrDefault(s => s.AuraName == debuffName);

            if (aura == null)
            {
                Log.Write($"[TargetDebuffTimeRemaining] Unable to find debuff with name '{debuffName}' in Spell Book");
                return -1;
            }

            return TargetDebuffTimeRemaining(aura.InternalAuraNo);
        }

        public static int PlayerBuffTimeRemaining(int auraNoInArrayOfAuras)
        {
            var c = GetBlockColor(auraNoInArrayOfAuras, 8);
            if (c.R == 255 || c.B == 255)
                return 0;
            try
            {
                // ReSharper disable once PossibleNullReferenceException
                var power = dtColorHelper.Select($"[Rounded] = '{c.G}'").FirstOrDefault()?["Value"].ToString();
                var power2 = dtColorHelper.Select($"[Rounded] = '{c.B}'").FirstOrDefault()?["Value"].ToString();
                return power != null && power2 != null ? int.Parse(power) * 100 + int.Parse(power2) : 0;
            }
            catch (Exception ex)
            {
                Log.Write("Failed to find buff Remaining for color R = " + c.R + "G = " + c.G +" B = " +c.B, Color.Red);
                Log.Write("Error: " + ex.Message, Color.Red);
            }

            return 0;
        }

        /// <summary>
        /// Players the buff time remaining.
        /// </summary>
        /// <param name="buffName">Name of the buff.</param>
        /// <returns></returns>
        public static int PlayerBuffTimeRemaining(string buffName)
        {
            var aura = SpellBook.Auras.FirstOrDefault(s => s.AuraName == buffName);

            if (aura == null)
            {
                Log.Write($"[PlayerBuffTimeRemaining] Unable to find buff with name '{buffName}' in Spell Book");
                return -1;
            }

            return PlayerBuffTimeRemaining(aura.InternalAuraNo);
        }

        public static int PetBuffTimeRemaining(int auraNoInArrayOfAuras)
        {
            var c = GetBlockColor(auraNoInArrayOfAuras, 10);

            try
            {
                if (c.R == 255 || c.B == 255)
                    return 0;
                var power = dtColorHelper.Select($"[Rounded] = '{c.G}'").FirstOrDefault()?["Value"].ToString();
                var power2 = dtColorHelper.Select($"[Rounded] = '{c.B}'").FirstOrDefault()?["Value"].ToString();
                return power != null && power2 != null ? int.Parse(power) * 100 + int.Parse(power2) : 0;
                // ReSharper disable once PossibleNullReferenceException
            }
            catch (Exception ex)
            {
                Log.Write("Failed to find pet buff remaining for color G = " + c.G, Color.Red);
                Log.Write("Error: " + ex.Message, Color.Red);
            }

            return 0;
        }

        /// <summary>
        /// Pets the buff time remaining.
        /// </summary>
        /// <param name="buffName">Name of the buff.</param>
        /// <returns></returns>
        public static int PetBuffTimeRemaining(string buffName)
        {
            var aura = SpellBook.Auras.FirstOrDefault(s => s.AuraName == buffName);

            if (aura == null)
            {
                Log.Write($"[GetPetBuffTimeRemaining] Unable to find buff with name '{buffName}' in Spell Book");
                return -1;
            }

            return PetBuffTimeRemaining(aura.InternalAuraNo);
        }


        public static int TargetDebuffStacks(int auraNoInArrayOfAuras)
        {
            var c = GetBlockColor(auraNoInArrayOfAuras, 4);

            try
            {
                Log.WriteDirectlyToLogFile($"Red = {c.R}");
                if (c.R == 255)
                    return 0;

                // ReSharper disable once PossibleNullReferenceException
                var stacks = dtColorHelper.Select($"[Rounded] = '{c.R}'").FirstOrDefault()["Value"].ToString();

                return int.Parse(stacks);
            }
            catch (Exception ex)
            {
                Log.Write("Failed to find debuff stacks for color R = " + c.R, Color.Red);
                Log.Write("Error: " + ex.Message, Color.Red);
            }

            return 0;
        }

        /// <summary>
        /// Targets the debuff stacks.
        /// </summary>
        /// <param name="debuffName">Name of the debuff.</param>
        /// <returns></returns>
        public static int TargetDebuffStacks(string debuffName)
        {
            var aura = SpellBook.Auras.FirstOrDefault(s => s.AuraName == debuffName);

            if (aura == null)
            {
                Log.Write($"[TargetDebuffStacks] Unable to find debuff with name '{debuffName}' in Spell Book");
                return -1;
            }

            return TargetDebuffStacks(aura.InternalAuraNo);
        }

        public static int PlayerSpellCharges(int spellNoInArrayOfSpells)
        {
            var c = GetBlockColor(spellNoInArrayOfSpells, 5);

            try
            {
                // ReSharper disable once PossibleNullReferenceException
                var stacks = dtColorHelper.Select($"[Rounded] = '{c.G}'").FirstOrDefault()["Value"].ToString();

                return int.Parse(stacks);
            }
            catch (Exception ex)
            {
                Log.Write("Failed to find spell charge stacks for color G = " + c.G, Color.Red);
                Log.Write("Error: " + ex.Message, Color.Red);
            }

            return 0;
        }

        /// <summary>
        /// Players the spell charges.
        /// </summary>
        /// <param name="spellName">Name of the spell.</param>
        /// <returns></returns>
        public static int PlayerSpellCharges(string spellName)
        {
            var spell = SpellBook.Spells.FirstOrDefault(s => s.SpellName == spellName);

            if (spell == null)
            {
                Log.Write($"[PlayerSpellCharges] Unable to find spell with name '{spellName}' in Spell Book");
                return -1;
            }

            return PlayerSpellCharges(spell.InternalSpellNo);
        }

        /// <summary>
        /// Items the count.
        /// </summary>
        /// <param name="itemName">Name of the item.</param>
        /// <returns></returns>
        public static int ItemCount(string itemName)
        {
            var item = SpellBook.Items.FirstOrDefault(s => s.ItemName == itemName);

            if (item == null)
            {
                Log.Write($"[GetItemCount] Unable to find Item with name '{itemName}' in Item Book");
                return -1;
            }

            return ItemCount(item.InternalItemNo);
        }

        public static int ItemCount(int itemNoInArrayOfItems)
        {
            var c = GetBlockColor(itemNoInArrayOfItems, 9);

            try
            {
                // ReSharper disable once PossibleNullReferenceException
                var count = dtColorHelper.Select($"[Rounded] = '{c.B}'").FirstOrDefault()["Value"].ToString();

                return int.Parse(count);
            }
            catch (Exception ex)
            {
                Log.Write("Failed to find Item count for color B = " + c.B, Color.Red);
                Log.Write("Error: " + ex.Message, Color.Red);
            }

            return 0;
        }

        /// <summary>
        /// Items the on cooldown.
        /// </summary>
        /// <param name="itemName">Name of the item.</param>
        /// <returns></returns>
        public static bool ItemOnCooldown(string itemName)
        {
            var item = SpellBook.Items.FirstOrDefault(s => s.ItemName == itemName);

            if (item == null)
            {
                Log.Write($"[GetItemCooldown] Unable to find Item with name '{itemName}' in Item Book");
                return false;
            }

            return ItemOnCooldown(item.InternalItemNo);
        }

        public static bool ItemOnCooldown(int itemNoInArrayOfItems)
        {
            var c = GetBlockColor(itemNoInArrayOfItems, 9);

            try
            {
                return c.R == Color.Red.R;
            }
            catch (Exception ex)
            {
                Log.Write("Failed to find Item Cooldown for color R = " + c.B, Color.Red);
                Log.Write("Error: " + ex.Message, Color.Red);
            }

            return false;
        }

        public static bool PlayerHasBuff(int auraNoInArrayOfAuras)
        {
            var c = GetBlockColor(auraNoInArrayOfAuras, 8);
            return c.R != 255  && c.B != 255;
        }

        /// <summary>
        /// Players the has buff.
        /// </summary>
        /// <param name="buffName">Name of the buff.</param>
        /// <returns></returns>
        public static bool PlayerHasBuff(string buffName)
        {
            var aura = SpellBook.Auras.FirstOrDefault(s => s.AuraName == buffName);

            if (aura == null)
            {
                Log.Write($"[HasAura] Unable to find aura with name '{buffName}' in Spell Book");
                return false;
            }

            return PlayerHasBuff(aura.InternalAuraNo);
        }

        public static bool PlayerHasDebuff(int auraNoInArrayOfAuras)
        {
            var c = GetBlockColor(auraNoInArrayOfAuras, 11);
            return c.R != 255 && c.B != 255;
        }

        public static bool IsSpellOverlayed(int spellNoInArrayOfSpells)
        {
            var c = GetBlockColor(spellNoInArrayOfSpells, 12);
            //Log.Write("R = " + c.R + " G = " + c.G + " B = " + c.B);
            return c.R == 255 && c.G == 0 && c.B == 0;
        }

        /// <summary>
        /// Determines whether [is spell overlayed] [the specified spell name].
        /// </summary>
        /// <param name="spellName">Name of the spell.</param>
        /// <returns>
        ///   <c>true</c> if [is spell overlayed] [the specified spell name]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsSpellOverlayed(string spellName)
        {
            var spell = SpellBook.Spells.FirstOrDefault(s => s.SpellName == spellName);

            if (spell == null)
            {
                Log.Write($"[IsSpellOverlayed] Unable to find spell with name '{spellName}' in Spell Book");
                return false;
            }

            return IsSpellOverlayed(spell.InternalSpellNo);
        }

        /// <summary>
        /// Players the has debuff.
        /// </summary>
        /// <param name="debuffName">Name of the debuff.</param>
        /// <returns></returns>
        public static bool PlayerHasDebuff(string debuffName)
        {
            var aura = SpellBook.Auras.FirstOrDefault(s => s.AuraName == debuffName);

            if (aura == null)
            {
                Log.Write($"[PlayerHasDebuff] Unable to find debuff with name '{debuffName}' in Spell Book");
                return false;
            }

            return PlayerHasDebuff(aura.InternalAuraNo);
        }

        public static bool PetHasBuff(int auraNoInArrayOfAuras)
        {
            var c = GetBlockColor(auraNoInArrayOfAuras, 10);
            return c.R != 255 &&  c.B != 255;
        }

        /// <summary>
        /// Pets the has buff.
        /// </summary>
        /// <param name="buffName">Name of the buff.</param>
        /// <returns></returns>
        public static bool PetHasBuff(string buffName)
        {
            var aura = SpellBook.Auras.FirstOrDefault(s => s.AuraName == buffName);

            if (aura == null)
            {
                Log.Write($"[PetHasBuff] Unable to find aura with name '{buffName}' in Spell Book");
                return false;
            }

            return PetHasBuff(aura.InternalAuraNo);
        }

        /// <summary>
        /// Targets the has debuff.
        /// </summary>
        /// <param name="debuffName">Name of the debuff.</param>
        /// <returns></returns>
        public static bool TargetHasDebuff(string debuffName)
        {
            var aura = SpellBook.Auras.FirstOrDefault(s => s.AuraName == debuffName);

            if (aura == null)
            {
                Log.Write($"[TargetHasDebuff] Unable to find debuff with name '{debuffName}' in Spell Book");
                return false;
            }

            return TargetHasDebuff(aura.InternalAuraNo);
        }

        public static bool TargetHasDebuff(int auraNoInArrayOfAuras)
        {
            var c = GetBlockColor(auraNoInArrayOfAuras, 4);
            return c.R != 255 && c.B != 255;
        }

        /// <summary>
        /// Casts the spell.
        /// </summary>
        /// <param name="spellBookSpellName">Name of the spell book spell.</param>
        public static void CastSpell(string spellBookSpellName)
        {
            var spell = SpellBook.Spells.FirstOrDefault(s => s.SpellName == spellBookSpellName);

            if (spell == null)
            {
                Log.Write($"[CastSpellByName] Unable to find spell with name '{spellBookSpellName}' in Spell Book");
                return;
            }

            LastSpell = spellBookSpellName;
            SendKey(spell.Key, spell.SpellId.ToString(), 50, spellBookSpellName);
        }

        [DllImport("gdi32.dll")]
        private static extern int BitBlt(IntPtr srchDC, int srcX, int srcY, int srcW, int srcH, IntPtr desthDC, int destX, int destY, int op);

        // This is apparently one of the fastest ways to read single pixel color
        // http://stackoverflow.com/questions/17130138/fastest-way-to-get-screen-pixel-color-in-c-sharp

        /// <summary>
        /// Gets the color of the block.
        /// </summary>
        /// <param name="column">The column.</param>
        /// <param name="row">The row.</param>
        /// <returns></returns>
        /// <exception cref="Exception">x and or y must be >= 1</exception>
        public static Color GetBlockColor(int column, int row)
        {
            if (Process == null)
                return Color.Black;

            if (column <= 0 || row <= 0)
                throw new Exception("x and or y must be >= 1");

            column = column - 1;
            row = row - 1;

            lock (thisLock) // We lock the bitmap "screenPixel" here to avoid it from being accessed by multiple threads at the same time and crashing
            {
                try
                {
                    using (var gdest = Graphics.FromImage(screenPixel))
                    {
                        using (var gsrc = Graphics.FromHwnd(Process.MainWindowHandle))
                        {
                            var hSrcDC = gsrc.GetHdc();
                            var hDC = gdest.GetHdc();
                            BitBlt(hDC, 0, 0, 1, 1, hSrcDC, column, row, (int) CopyPixelOperation.SourceCopy);
                            gdest.ReleaseHdc();
                            gsrc.ReleaseHdc();
                        }
                    }
                    var temp = screenPixel.GetPixel(0, 0);

                    return temp;
                }
                catch (Exception ex)
                {
                    Log.Write("Failed to find pixel color from screen, this is usually due to wow closing while", Color.Red);
                    Log.Write("attempting to find the pixel color", Color.Red);
                    Log.Write("Error Details: " + ex.Message, Color.Red);

                    return Color.Orange; // Orange cause nothing currently uses it
                }
            }
        }

        /// <summary>
        /// Gets the color of the pixel.
        /// </summary>
        /// <param name="column">The column.</param>
        /// <param name="row">The row.</param>
        /// <returns></returns>
        public static Color GetPixelColor(int column, int row)
        {
            if (Process == null)
                return Color.Black;

            lock (thisLock) // We lock the bitmap "screenPixel" here to avoid it from being accessed by multiple threads at the same time and crashing
            {
                try
                {
                    using (var gdest = Graphics.FromImage(screenPixel))
                    {
                        using (var gsrc = Graphics.FromHwnd(Process.MainWindowHandle))
                        {
                            var hSrcDC = gsrc.GetHdc();
                            var hDC = gdest.GetHdc();
                            BitBlt(hDC, 0, 0, 1, 1, hSrcDC, column, row, (int) CopyPixelOperation.SourceCopy);
                            gdest.ReleaseHdc();
                            gsrc.ReleaseHdc();
                        }
                    }
                    var temp = screenPixel.GetPixel(0, 0);

                    return temp;
                }
                catch (Exception ex)
                {
                    Log.Write("Failed to find pixel color from screen, this is usually due to wow closing while", Color.Red);
                    Log.Write("attempting to find the pixel color", Color.Red);
                    Log.Write("Error Details: " + ex.Message, Color.Red);

                    return Color.Orange; // Orange cause nothing currently uses it
                }
            }
        }
        
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowThreadProcessId(IntPtr handle, out int processId);

        public static MemoryStream FromBytes(byte[] buffer)
        {
            return new MemoryStream(buffer);
        }

        #region Keyboard Input

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool PostMessage(IntPtr hWnd, uint msg, UIntPtr wParam, UIntPtr lParam);

        /// <summary>
        /// Keys down.
        /// </summary>
        /// <param name="Key">The key.</param>
        public static void KeyDown(Keys Key)
        {
            SendMessage(Process.MainWindowHandle, 0x100, (int) Key, 0);
        }

        public static void KeyUp(Keys Key)
        {
            SendMessage(Process.MainWindowHandle, 0x101, (int) Key, 0);
        }

        /// <summary>
        /// Keys the press release.
        /// </summary>
        /// <param name="key">The key.</param>
        public static void KeyPressRelease(Keys key)
        {
            KeyDown(key);
            Thread.Sleep(50);
            KeyUp(key);
        }

        private static void Write(string text, params object[] args)
        {
            foreach (var character in string.Format(text, args))
            {
                PostMessage(Process.MainWindowHandle, 0x0102, new UIntPtr(character), UIntPtr.Zero);
            }
        }

        #endregion
    }
}