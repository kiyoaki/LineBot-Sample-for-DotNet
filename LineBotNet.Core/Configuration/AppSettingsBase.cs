using System;
using System.Collections.Specialized;
using System.Configuration;

namespace LineBotNet.Core.Configuration
{
    public class AppSettingsBase
    {
        protected AppSettingsBase() { }

        protected static NameValueCollection AppSettings = ConfigurationManager.AppSettings;

        protected static string Setting(string key, string defaultValue = null)
        {
            return AppSettings[key] ?? defaultValue;
        }

        protected static int SettingAsInt(string key, int defaultValue = default(int))
        {
            int ret;
            return int.TryParse(AppSettings[key], out ret) ? ret : defaultValue;
        }

        protected static float SettingAsFloat(string key, float defaultValue = default(float))
        {
            float ret;
            return float.TryParse(AppSettings[key], out ret) ? ret : defaultValue;
        }

        protected static double SettingAsDouble(string key, double defaultValue = default(double))
        {
            double ret;
            return double.TryParse(AppSettings[key], out ret) ? ret : defaultValue;
        }

        protected static bool SettingAsBool(string key, bool defaultValue = default(bool))
        {
            bool ret;
            return bool.TryParse(AppSettings[key], out ret) ? ret : defaultValue;
        }

        protected static DateTime SettingAsDateTime(string key, DateTime defaultValue = default(DateTime))
        {
            DateTime ret;
            return DateTime.TryParse(AppSettings[key], out ret) ? ret : defaultValue;
        }

        protected static T SettingAsEnum<T>(string key, T defaultValue = default(T))
            where T : struct
        {
            T ret;
            return Enum.TryParse(AppSettings[key], out ret) ? ret : defaultValue;
        }
    }
}