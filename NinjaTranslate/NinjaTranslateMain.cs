using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text;

using ntutil;

namespace NinjaTranslate
{
    class NinjaTranslateMain {

        [STAThread]
        static void Main() {
            //Initiate update process
            NinjaTranslateMain.Update(Config.GetValue("version"), Config.GetValue("updateStep"));

            //TODO Change following with filter structure to be more flexible
            Normalizer normalizer = new Normalizer();
            
            //load windows
            SplashForm sf = new SplashForm();
            MainWindow mainWindow = new MainWindow();

            //load data
            DictLoader dc = new DictLoader();
            dc.SetSplashForm(sf);
            dc.SetFilter(normalizer); //TODO this has to be replaced when filters are implemented
            PatriciaTrie translationTree = dc.LoadDictData(Config.GetValue("path"), Config.GetValue("serializedPath"));

            //initiate translation center
            TranslationCenter translationCenter = new TranslationCenter();
            translationCenter.SetFilter(normalizer);
            translationCenter.SetTranslationTree(translationTree);

            //initiate notification service
            BubbleNotification notification = new BubbleNotification();
            notification.SetNotificationDuration(Int32.Parse(Config.GetValue("notificationDuration")));
            notification.SetForm(mainWindow);

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
            try {
                // register the event that is fired after the key press.
                markerHook.KeyPressed += new EventHandler<KeyPressedEventArgs>(delegate(object sender, KeyPressedEventArgs e) {
                    systemSource.TriggerTranslation(true);
                });
                // register the control + alt + N combination as hot key to translate current selection.
                markerHook.RegisterHotKey((ModifierKeys)2 | (ModifierKeys)1, Keys.N);
                // register the control + alt + B combination as hot key to open up input formular.
                inputHook.KeyPressed += new EventHandler<KeyPressedEventArgs>(delegate(object sender, KeyPressedEventArgs e) {
                    inputSource.TriggerTranslation(true);    
                });
                inputHook.RegisterHotKey((ModifierKeys)2 | (ModifierKeys)1, Keys.B);
            }
            catch (InvalidOperationException e) {
                System.Windows.Forms.MessageBox.Show("NinjaTranslate couldn't register the necessary hotkeys. It seems like another program uses them. Try to close them :)", "NinjaTranslate found an error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //check if construction went well or if window is marked as disposed
            if (!mainWindow.IsDisposed)
                Application.Run(mainWindow);
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
