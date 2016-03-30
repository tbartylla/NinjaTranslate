using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using ntutil;

namespace Updater {
    class NinjaTranslateUpdater {

        protected static String updateUrl = "http://web-teq.de/NinjaTranslate/updateList";
        protected static String updateLocation = "http://web-teq.de/NinjaTranslate";
        protected static String updateCheckUrl = "http://web-teq.de/NinjaTranslate/version";
        protected static String updateDir = "updates";
        protected static String targetDir = "";
        protected static String restartExe = "NinjaTranslate.exe";

        static int Main(string[] args) {
            if (!NinjaTranslateUpdater.PreUpdate())
                return -1;

            int version = 0;
            switch (args[0]) {
                case "update":
                    int.TryParse(args[1], out version);
                    if (NinjaTranslateUpdater.CheckForUpdates(version)) { 
                        bool success = NinjaTranslateUpdater.LoadUpdate();
                        if (success) {
                            System.Console.WriteLine("Update gefunden und geladen");
                            Config.SetSingleValue("updateStep", "copy");
                            Config.Save();
                            return 1;
                        }

                        return 0;
                    }
                    System.Console.WriteLine("Kein Update gefunden");
                    return 0;

                case "copy":
                    //wait some time, so the main program can be closed
                    System.Threading.Thread.Sleep(500);
                    version = NinjaTranslateUpdater.CopyUpdateFiles();
                    NinjaTranslateUpdater.RestartProgram();
                    Config.SetSingleValue("version", version.ToString());
                    Config.SetSingleValue("updateStep", "update");
                    Config.Save();
                    break;
            }

            return -1;
        }

        static bool PreUpdate() {
            //check folder
            if (!Directory.Exists(NinjaTranslateUpdater.updateDir))
                Directory.CreateDirectory(NinjaTranslateUpdater.updateDir);

            return true;
        }

        static bool CheckForUpdates(int version) {
            if (version == 0)
                return false;

            try {
                WebClient wc = new WebClient();
                int newestVersion = 0;
                byte[] data; 
                data = wc.DownloadData(NinjaTranslateUpdater.updateCheckUrl);
                String versionText = Encoding.UTF8.GetString(data);
                int.TryParse(versionText, out newestVersion);

                if (newestVersion > version)
                    return true;
                else 
                    return false;
            }
            catch (Exception e) {
                return false;
            }
        }

        static bool LoadUpdate() {
            try {
                WebClient wc = new WebClient();
                String fileList = wc.DownloadString(NinjaTranslateUpdater.updateUrl);
                String[] files = fileList.Split(("\n").ToCharArray());

                foreach(String file in files) {
                    int lastPositionOfBackslash = file.LastIndexOf("/");
                    String fileName;
                    if (lastPositionOfBackslash != -1)
                        fileName = file.Substring(lastPositionOfBackslash, file.Length - lastPositionOfBackslash);
                    else
                        fileName = file;
                    wc.DownloadFile(NinjaTranslateUpdater.updateLocation + "/" + file, Directory.GetCurrentDirectory() + "\\" + NinjaTranslateUpdater.updateDir + "\\" + fileName);
                }

                return true;
            }
            catch (Exception e) {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>version of new files</returns>
        static int CopyUpdateFiles() {
            int version = 0;
            //copy files from upload directory to actual program
            foreach (String file in Directory.GetFiles(NinjaTranslateUpdater.updateDir)) {
                String fileName = Path.GetFileName(file);

                if (fileName.Equals("version")) {
                    String versionText = Encoding.UTF8.GetString(File.ReadAllBytes(file));
                    int.TryParse(versionText, out version);
                    continue;
                }

                String destFile = Path.Combine(NinjaTranslateUpdater.targetDir, fileName);
                System.IO.File.Copy(file, destFile, true);
            }

            return version;
        }

        static void RestartProgram() {
            System.Diagnostics.Process.Start(NinjaTranslateUpdater.restartExe);
        }
    }
}
