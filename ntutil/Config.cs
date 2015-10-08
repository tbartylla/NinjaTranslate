using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ntutil
{
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

        public static void SetValue(String key, String value) {
            //First check that all old values have been loaded, they would be lost otherwise when saving
            if (!Config.valuesLoaded)
                Config.Load();

            Config.confValues[key] = value;
        }

        public static void Save() {
            String[] lines = new String[Config.confValues.Count];
            
            int i = 0;
            foreach(KeyValuePair<string, string> pair in Config.confValues) {
                lines[i] = pair.Key + "=" + pair.Value;
                i++;
            }

            //write back (will delete comments at the moment)
            File.WriteAllLines(Config.filePath, lines, System.Text.Encoding.UTF8);
        }
    }
}
