using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NinjaTranslate {

    public static class Config {
        private static String filePath = "config.ini";
        private static Dictionary<String, String> confValues = new Dictionary<String, String>();
        private static bool valuesLoaded = false;

        private static bool Load() {
            if (!File.Exists(Config.filePath))
                return false;
            
            String[] lines = File.ReadAllLines(@Config.filePath);
            foreach (String line in lines) {
                if (line.StartsWith("//") || line.Trim().Equals(""))
                    continue;
                
                String key = line.Substring(0, line.IndexOf("="));
                String val = line.Substring(line.IndexOf("=") + 1);

                Config.confValues.Add(key, val);
            }

            Config.valuesLoaded = true;

            return true;
        }

        public static string GetValue(String key) {
            if (!Config.valuesLoaded)
                if (!Config.Load())
                    return "";

            if (Config.confValues.ContainsKey(key))
                return Config.confValues[key];
            return "";
        }

        //TODO sollte auch was machen
        public static void SetValue(String key, String value) {

        }
    }
}
