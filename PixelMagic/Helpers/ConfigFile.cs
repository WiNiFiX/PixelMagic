//////////////////////////////////////////////////
//                                              //
//   See License.txt for Licensing information  //
//                                              //
//////////////////////////////////////////////////

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using PixelMagic.GUI;

namespace PixelMagic.Helpers
{
    public static class ConfigFile
    {
        private static string path;

        public static string LastRotation => ReadValue("PixelMagic", "LastProfile").Trim();

        public static bool PlayErrorSounds
        {
            get
            {
                var playErrorsounds = ReadValue("PixelMagic", "PlayErrorSounds").Trim();

                if (playErrorsounds != "")
                {
                    return Convert.ToBoolean(playErrorsounds);
                }

                return true;
            }
            set { WriteValue("PixelMagic", "PlayErrorSounds", value.ToString()); }
        }

        public static bool DisableOverlay
        {
            get
            {
                var disableOverlay = ReadValue("PixelMagic", "DisableOverlay").Trim();

                if (disableOverlay != "")
                {
                    return Convert.ToBoolean(disableOverlay);
                }

                return true;
            }
            set
            {
                WriteValue("PixelMagic", "DisableOverlay", value.ToString());

                if (value)
                    Overlay.HideOverlay();
                else
                    Overlay.ShowOverlay();
            }
        }

        public static decimal Pulse
        {
            get
            {
                var pulse = ReadValue("PixelMagic", "Pulse").Trim();

                if (pulse != "")
                {
                    return Convert.ToDecimal(pulse);
                }

                return 100;
            }
            set { WriteValue("PixelMagic", "Pulse", value.ToString()); }
        }

        public static decimal Latency
        {
            get
            {
                var latency = ReadValue("PixelMagic", "Latency").Trim();

                if (latency != "")
                {
                    return Convert.ToDecimal(latency);
                }

                return 0;
            }
            set
            {
                Log.Write($"Latency Updated to: {value}, Please restart PixelMagic for changes to take effect", Color.Red);
                WriteValue("PixelMagic", "Latency", value.ToString());
            }
        }

        public static bool LicenseAccepted
        {
            get
            {
                var licenseAccepted = ReadValue("PixelMagic", "License Accepted").Trim();

                if (licenseAccepted != "")
                {
                    return Convert.ToBoolean(licenseAccepted);
                }

                return false;
            }
            set
            {
                WriteValue("PixelMagic", "License Accepted", value.ToString());
            }
        }

        public static string GitHubName
        {
            get
            {
                 return ReadValue("PixelMagic", "GitHubName").Trim();
            }
            set
            {
                WriteValue("PixelMagic", "GitHubName", value);
            }
        }

        public static string GitHubPassword
        {
            get
            {
                return ReadValue("PixelMagic", "GitHubPassword").Trim();
            }
            set
            {
                WriteValue("PixelMagic", "GitHubPassword", value);
            }
        }

        public static string LatencyForAddon => (int.Parse(Latency.ToString()) * 2 / 1000.0).ToString("#0.000");

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        public static void Initialize()
        {
            path = Application.StartupPath + "\\config.ini";
        }

        public static void WriteValue(string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, path);
        }

        public static string ReadValue(string section, string key)
        {
            var temp = new StringBuilder(255);
            GetPrivateProfileString(section, key, "", temp, 255, path);
            return temp.ToString().Trim();
        }

        public static T ReadValue<T>(string section, string key)
        {
            var temp = new StringBuilder(255);
            GetPrivateProfileString(section, key, "", temp, 255, path);

            if ((temp.ToString() == "") && (typeof(T) == typeof(int)))
            {
                temp.Append("0");
            }
            if ((temp.ToString() == "") && (typeof(T) == typeof(short)))
            {
                temp.Append("0");
            }
            return (T) Convert.ChangeType(temp.ToString(), typeof(T));
        }
    }
}