using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ntutil
{
    public static class Config {

        public class ConfigStore {
            public String singleVal = "";
            public Dictionary<String, String> multiValues = new Dictionary<String, String>();
            public Boolean isMulti = false;
        }

        private static String filePath = "config.ini";
        private static Dictionary<String, ConfigStore> confValues = new Dictionary<String, ConfigStore>();
        private static bool valuesLoaded = false;

        private static bool Load() {
            if (!File.Exists(Config.filePath))
                return false;

            String[] lines = File.ReadAllLines(@Config.filePath);
            foreach (String line in lines) {
                ConfigStore val = new ConfigStore();
                String trimmedLine = line.Trim();
                if (trimmedLine.StartsWith("//") || trimmedLine.Trim().Equals(""))
                    continue;

                String key;
                //assume multiValue
                if (trimmedLine.Contains(":")) {
                    int splitPos = trimmedLine.IndexOf(":");
                    key = trimmedLine.Substring(0, splitPos);
                    trimmedLine = trimmedLine.Substring(splitPos + 1);
                    val = parseMultiValue(trimmedLine, val);
                }
                else {
                    int splitPos = trimmedLine.IndexOf("=");
                    key = trimmedLine.Substring(0, trimmedLine.IndexOf("="));
                    trimmedLine = trimmedLine.Substring(splitPos + 1);
                    val.singleVal = trimmedLine;
                }

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
                return Config.confValues[key].singleVal;
            return "";
        }

        public static Dictionary<String, String> GetMultiValue(String key) {
            if (!Config.valuesLoaded)
                if (!Config.Load())
                    return new Dictionary<string,string>();

            if (Config.confValues.ContainsKey(key))
                return Config.confValues[key].multiValues;
            return new Dictionary<string,string>();
        }
        
        public static void addMultiValue(String key, String valKey, String val) {
            if (!Config.valuesLoaded)
                Config.Load();

            if (Config.confValues.ContainsKey(key))
                Config.confValues[key].multiValues.Add(valKey, val);
        }

        public static void SetSingleValue(String key, String value) {
            //First check that all old values have been loaded, they would be lost otherwise when saving
            if (!Config.valuesLoaded)
                Config.Load();

            Config.confValues[key].singleVal = value;
        }

        public static void SetMultiValue(String key, Dictionary<string, string> value) {
            //First check that all old values have been loaded, they would be lost otherwise when saving
            if (!Config.valuesLoaded)
                Config.Load();

            Config.confValues[key].multiValues = value;
        }

        /**
         * TODO noch nicht getestet
         * */
        public static void Save() {
            String[] lines = new String[Config.confValues.Count];
            
            int i = 0;
            foreach(KeyValuePair<string, ConfigStore> pair in Config.confValues) {
                if (pair.Value.isMulti) {
                    lines[i] = pair.Key + ":";
                    foreach (KeyValuePair<String, String> entry in pair.Value.multiValues) {
                        lines[i] += entry.Key + "=" + entry.Value + ",";
                    }
                }
                else {
                    lines[i] = pair.Key + "=" + pair.Value.singleVal;
                }
                i++;
            }

            //write back (will delete comments at the moment)
            File.WriteAllLines(Config.filePath, lines, System.Text.Encoding.UTF8);
        }

        public static ConfigStore parseMultiValue(String line, ConfigStore obj) {
            //at this point line should only contain (multiple) values
            line = line.Trim();
            while (line.Trim().Length != 0) {
                int splitPosition = line.IndexOf("=");
                String key = line.Substring(0, splitPosition);
                line = line.Substring(splitPosition);
                //remove "="
                line = line.Substring(1);
                
                String val;
                int cutEnding;
                if (line.Contains(",")) {
                    cutEnding = line.IndexOf(",");
                    val = line.Substring(0, cutEnding);
                    //remove "," as well
                    line = line.Substring(cutEnding + 1);
                }
                else {
                    cutEnding = line.Length;
                    val = line.Substring(0, cutEnding);
                    line = line.Substring(cutEnding);
                }
                
                obj.multiValues.Add(key, val);
            }

            obj.isMulti = true;

            return obj;
        }
    }
}
