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
    class NinjaTranslate {
        static DictionaryReader dr = new DictionaryReader();

        [STAThread]
        static void Main() {
            dr.readRawDictionary();
            Application.Run(new MainWindow());   
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
        public string getTranslation() {
            string textFromClipboard = GetActiveWindow();
            if (textFromClipboard == null)
                return null;
            return DictionaryReader.translate(textFromClipboard.ToLowerInvariant().Trim());  
        }

        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        // TODO: restore data in Clipboard.
        private string GetActiveWindow() {
            object oldClipboardDataObject = Clipboard.GetData(DataFormats.UnicodeText);
            const int nChars = 256;
            IntPtr handle;
            StringBuilder Buff = new StringBuilder(nChars);
            handle = GetForegroundWindow();
            //if (GetWindowText(handle, Buff, nChars) > 0)
            //{
            //  string windowTitle = Buff.ToString();
            //  System.Threading.Thread.Sleep(400);
            //}
            System.Threading.Thread.Sleep(400);
            SetForegroundWindow(handle);
            SendKeys.SendWait("^(c)");
            object restorePoint = Clipboard.GetData(DataFormats.UnicodeText);
            if (restorePoint == null) {
                if (oldClipboardDataObject == null){
                    return null;
                }
                return oldClipboardDataObject.ToString();
            }          
            return restorePoint.ToString();
        }
    }
}
