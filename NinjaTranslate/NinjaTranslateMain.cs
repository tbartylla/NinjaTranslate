﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text;


namespace NinjaTranslate
{
    class NinjaTranslateMain {
        private DictLoader dr = new DictLoader();

        [STAThread]
        static void Main() {
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
            InputForm inputSource = new InputForm();
            systemSource.SetTranslationService(translationCenter);
            systemSource.SetNotificationService(notification);
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
                //TODO Toni wants to comment sth here
                inputHook.RegisterHotKey((ModifierKeys)2 | (ModifierKeys)1, Keys.B);
            }
            catch (InvalidOperationException e) {
                System.Windows.Forms.MessageBox.Show("NinjaTranslate couldn't register the necessary hotkeys. It seems like another program uses them. Try to close them :)", "NinjaTranslate found an error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //check if construction went well or if window is marked as disposed
            if (!mainWindow.IsDisposed)
                Application.Run(mainWindow);
        }   

    }
}