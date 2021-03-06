﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NinjaTranslate {
    class SystemSource : ITranslationSource{

        protected ITranslationService translationService;
        protected INotificationService notificationService;

        protected bool setClipboardData = true;
        protected int clipboardAccessTimer = 400;

        public void SetClipboardAccessTimer(int ms) {
            this.clipboardAccessTimer = ms;
        }

        public void SetClipboardData(bool boo) {
            this.setClipboardData = boo;
        }

        public void SetTranslationService(ITranslationService service) {
            this.translationService = service;
        }

        [MethodImpl(MethodImplOptions.Synchronized)] //still needed? better make some research
        public string TriggerTranslation(bool includeNotification) {
            string textFromClipboard = GetActiveWindow(this.setClipboardData);
            string translation = this.translationService.Translate(textFromClipboard);
            if (includeNotification)
                this.notificationService.Notify(translation);
            return translation;
        }

        private string GetActiveWindow(bool setClipboardData) {
            //copy data from clipboard
            object oldClipboardDataObject = Clipboard.GetData(DataFormats.UnicodeText);
            
            //add a little wait time to prevent clipboard beeing locked when we want another app to copy current selection into it
            System.Threading.Thread.Sleep(this.clipboardAccessTimer);
            SendKeys.SendWait("^(c)");
                        
            object clipboardText = Clipboard.GetData(DataFormats.UnicodeText);
            
            if (setClipboardData) {
                SaveToClipboard(oldClipboardDataObject);
            }
            if (clipboardText == null) {
                if (oldClipboardDataObject == null) {
                    return null;
                }
                return oldClipboardDataObject.ToString(); 
            }
            return clipboardText.ToString();
        }

        public void SetNotificationService(INotificationService notificationService) {
            this.notificationService = notificationService;
        }

        private void SaveToClipboard(object data) {
            try {
                Clipboard.SetData(DataFormats.UnicodeText, data);
            }
            catch (System.Runtime.InteropServices.ExternalException e) {
                Console.WriteLine("Oops! Couldn't save your stuff back to Clipboard. Let's try it again.");
                System.Threading.Thread.Sleep(500);
                SaveToClipboard(data);
            }
        }
    }
}
