using System;
using System.Drawing;
using Microsoft.Win32;

namespace PixelMagic.Helpers
{
    public static class GameDVR
    {
        public static bool IsAppCapturedEnabled
        {
            get
            {
                try
                {
                    const string appCaptureEnabledKey = "HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\GameDVR";
                    if ((int)Registry.GetValue(appCaptureEnabledKey, "AppCaptureEnabled", -1) == 1)
                    {
                        return true;
                    }
                }
                catch(Exception ex)
                {
                    Log.Write("Failed to find GameDVR-AppCaptureEnabled registry key", Color.Gray);
                    Log.Write("Reason: " + ex.Message, Color.Gray);
                }
                return false;
            }
        }

        public static bool IsGameDVREnabled
        {
            get
            {
                try
                {
                    const string appCaptureEnabledKey = "HKEY_CURRENT_USER\\System\\GameConfigStore";
                    if ((int)Registry.GetValue(appCaptureEnabledKey, "GameDVR_Enabled", -1) == 1)
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Log.Write("Failed to find GameDVR-GameConfigStore registry key", Color.Gray);
                    Log.Write("Reason: " + ex.Message, Color.Gray);
                }
                return false;
            }
        }

        public static void SetGameDVREnabled(int value)
        {
            const string gameDVREnabledKey = "HKEY_CURRENT_USER\\System\\GameConfigStore";
            Registry.SetValue(gameDVREnabledKey, "GameDVR_Enabled", value);
        }

        public static void SetAppCapturedEnabled(int value)
        {
            const string appCaptureEnabledKey =
                "HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\GameDVR";
            var appCaptureEnabled = (int) Registry.GetValue(appCaptureEnabledKey, "AppCaptureEnabled", value);
        }
    }
}