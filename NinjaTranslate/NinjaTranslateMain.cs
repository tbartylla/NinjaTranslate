using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text;

using ntutil;
using NinjaTranslate.Resources;
using NinjaTranslate.Resources.Forms;

namespace NinjaTranslate
{
    class NinjaTranslateMain {

        public static TranslationCenter translationCenter;
        public static Dictionary<String, String> rawFiles;
        public static String currentFileKey;
        public static String quickChangeKey;
        public static Dictionary<String, PatriciaTrie> treesInMemory;
        public static int maxTreesInMemory;

        [STAThread]
        static void Main() {
            //Initiate update process
            NinjaTranslateMain.Update(Config.GetValue("version"), Config.GetValue("updateStep"));

            //load windows
            MainWindow mainWindow = new MainWindow();
            NotificationForm notificationForm = new NotificationForm();

            //load config values
            NinjaTranslateMain.currentFileKey = Config.GetValue("currentKey");
            NinjaTranslateMain.rawFiles = Config.GetMultiValue("path");
            NinjaTranslateMain.quickChangeKey = Config.GetValue("quickchangeKey");
            NinjaTranslateMain.treesInMemory = new Dictionary<String, PatriciaTrie>();
            NinjaTranslateMain.maxTreesInMemory = Int32.Parse(Config.GetValue("maxTreesInMemory"));
            if (NinjaTranslateMain.maxTreesInMemory < 1)
                NinjaTranslateMain.maxTreesInMemory = 1;


            //load tree
            PatriciaTrie translationTree = LoadTranslationTree(currentFileKey);

            //TODO normalizer is used for input and  parsing -> surely no problem when we finally add filters
            Normalizer normalizer = new Normalizer();

            //initiate translation center
            translationCenter = new TranslationCenter();
            translationCenter.SetFilter(normalizer);
            translationCenter.SetTranslationTree(translationTree);

            //initiate notification service
            CustomNotification notification = new CustomNotification();
            notification.setHeight(Int32.Parse(Config.GetValue("windowHeight")));
            notification.setWidth(Int32.Parse(Config.GetValue("windowWidth")));
            notification.SetNotificationDuration(Int32.Parse(Config.GetValue("notificationDuration")));
            notification.SetForm(notificationForm);

            //initiate sources to be translated from
            SystemSource systemSource = new SystemSource();
            int clipboardAccessTimer = 500;
            int.TryParse(Config.GetValue("clipboardAccessTimer"), out clipboardAccessTimer);
            systemSource.SetClipboardAccessTimer(clipboardAccessTimer);
            systemSource.SetTranslationService(translationCenter);
            systemSource.SetNotificationService(notification);

            InputForm inputSource = new InputForm();
            inputSource.SetTranslationService(translationCenter);
            inputSource.SetNotificationService(notification);

            //register hooks
            KeyboardHook markerHook = new KeyboardHook();
            KeyboardHook inputHook = new KeyboardHook();
            KeyboardHook dictionaryHook = new KeyboardHook();
            try {
                // register the control + alt + N combination as hot key to translate current selection.
                markerHook.KeyPressed += new EventHandler<KeyPressedEventArgs>(delegate(object sender, KeyPressedEventArgs e) {
                    systemSource.TriggerTranslation(true);
                });
                markerHook.RegisterHotKey((ModifierKeys)2 | (ModifierKeys)1, Keys.N);
                // register the control + alt + B combination as hot key to open up input formular.
                inputHook.KeyPressed += new EventHandler<KeyPressedEventArgs>(delegate(object sender, KeyPressedEventArgs e) {
                    inputSource.TriggerTranslation(true);    
                });
                inputHook.RegisterHotKey((ModifierKeys)2 | (ModifierKeys)1, Keys.B);
                //register the control + alt + V combination to trigger a switch of the chosen dictionary
                dictionaryHook.KeyPressed += new EventHandler<KeyPressedEventArgs>(delegate(object sender, KeyPressedEventArgs e) {
                    NinjaTranslateMain.ChangeDictionary(notification);
                });
                dictionaryHook.RegisterHotKey((ModifierKeys)2 | (ModifierKeys)1, Keys.V);
            }
            catch (InvalidOperationException e) {
                System.Windows.Forms.MessageBox.Show("NinjaTranslate couldn't register the necessary hotkeys. It seems like another program uses them. Try to close them :)", "NinjaTranslate found an error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //check if construction went well or if window is marked as indisposed
            if (!mainWindow.IsDisposed)
                Application.Run(mainWindow);

            GC.KeepAlive(markerHook);
            GC.KeepAlive(dictionaryHook);
            GC.KeepAlive(inputHook);
        }

        static void ChangeDictionary(INotificationService notification) {
            PatriciaTrie translationTrie = NinjaTranslateMain.LoadTranslationTree(NinjaTranslateMain.quickChangeKey);
            NinjaTranslateMain.translationCenter.SetTranslationTree(translationTrie);

            String temp = NinjaTranslateMain.currentFileKey;
            NinjaTranslateMain.currentFileKey = NinjaTranslateMain.quickChangeKey;
            NinjaTranslateMain.quickChangeKey = temp;

            if (notification != null)
                notification.Notify("New Dictionary: " + NinjaTranslateMain.currentFileKey);
        }

        static PatriciaTrie LoadTranslationTree(String currentKey) {
            //check if tree is already stored in memory
            if (NinjaTranslateMain.treesInMemory.ContainsKey(currentKey))
                return NinjaTranslateMain.treesInMemory[currentKey];
            
            //load windows
            SplashForm sf = new SplashForm();

            //TODO Change following with filter structure to be more flexible
            Normalizer normalizer = new Normalizer();
            
            //load data
            DictLoader dc = new DictLoader();
            dc.SetSplashForm(sf);
            dc.SetFilter(normalizer); //TODO this has to be replaced when filters are implemented

            PatriciaTrie translationTree = dc.LoadDictData(NinjaTranslateMain.rawFiles[currentKey], Config.GetValue("serializedPath") + currentKey + ".tree");

            //if we are allowed to store this tree in memory, even when we load another one -> store it
            if (NinjaTranslateMain.treesInMemory.Count < NinjaTranslateMain.maxTreesInMemory)
                NinjaTranslateMain.treesInMemory.Add(currentKey, translationTree);

            return translationTree;
        }

        static void Update(String version, String mode) {
            switch (mode) {
                case "update":
                    System.Diagnostics.Process.Start("Updater.exe", "update " + version);
                    break;
                case "copy":
                    System.Diagnostics.Process.Start("Updater.exe", "copy");
                    System.Environment.Exit(5);
                    break;
            }
        }
    }
}
