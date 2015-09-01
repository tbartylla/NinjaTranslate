using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Automation;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Runtime.CompilerServices;
using System.Threading;

namespace NinjaTranslate
{
    class NinjaTranslateMain {
        static DictionaryReader dr = new DictionaryReader();

        [STAThread]
        static void Main() {
            dr.readRawDictionary();
            MainWindow mW = new MainWindow();
            //check if construction went well or if window is marked as disposed
            if (!mW.IsDisposed)
                Application.Run(mW);
        }   

        public void getSelectedText() {
            var element = AutomationElement.FocusedElement;
            var selectedText = "";
            if (element != null) {
                object pattern = null;
                if (element.TryGetCurrentPattern(TextPattern.Pattern, out pattern)) {
                    var tp = (TextPattern)pattern;
                    var sb = new StringBuilder();

                    foreach (var r in tp.GetSelection()) {
                        sb.AppendLine(r.GetText(-1));
                    }

                    selectedText = sb.ToString();
                }
            }
            MessageBox.Show(selectedText);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public string getTranslation(bool setClipboardData) {
            string textFromClipboard = GetActiveWindow(setClipboardData);
            if (textFromClipboard == null)
                return null;
            return DictionaryReader.translate(textFromClipboard);  
        }

        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        private void SaveToClipboard() {
            try {
                Clipboard.SetData(DataFormats.UnicodeText, savedClipboardDataObject);
            } catch (System.Runtime.InteropServices.ExternalException e) {
                Console.WriteLine("Oops! Couldn't save your stuff back to Clipboard. Let's try it again.");
                System.Threading.Thread.Sleep(200);
                SaveToClipboard();
            }           
        }

        object savedClipboardDataObject;

        // TODO: FIXME. at this point i'm not sure anymore, if this works. 
        // seems like this method always has problems whenever a thread exits 
        private string GetActiveWindow(bool setClipboardData) {
            object oldClipboardDataObject = Clipboard.GetData(DataFormats.UnicodeText);
            if (setClipboardData && savedClipboardDataObject != null)
                if (oldClipboardDataObject.ToString() != savedClipboardDataObject.ToString())
                    Console.WriteLine("Something is wrong. old: " + oldClipboardDataObject.ToString() + " saved: " + savedClipboardDataObject.ToString()); // Jackpot! 
            const int nChars = 256;
            IntPtr handle;
            StringBuilder Buff = new StringBuilder(nChars);
            handle = GetForegroundWindow();
            SetForegroundWindow(handle);
            SendKeys.SendWait("^(c)");
            System.Threading.Thread.Sleep(400);
            object restorePoint = Clipboard.GetData(DataFormats.UnicodeText);
            // setClipboardData necessary to invoke SetData only at every second call of GetActiveWindow; 
            // otherwise the pdf hack would be screwed. Delete this once we find a better solution.
            if (setClipboardData) {
                SaveToClipboard();
            } else {
                savedClipboardDataObject = oldClipboardDataObject;
            }
            if (restorePoint == null) {
                if (oldClipboardDataObject == null){
                    return null;
                }
                return oldClipboardDataObject.ToString(); // TODO: change something here, so we dont return the stuff that belongs to the clipboard.
            }          
            return restorePoint.ToString();
        }
    }
}
