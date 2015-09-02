using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Automation;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Windows.Input;
using System.Threading;

namespace NinjaTranslate
{
    public partial class MainWindow : Form
    {
        /// <summary>
        /// KeyboardHook from: http://www.liensberger.it/web/blog/?p=207
        /// </summary>
        private KeyboardHook hook = new KeyboardHook();
        private KeyboardHook hookInput = new KeyboardHook();
        NinjaTranslateMain nt = new NinjaTranslateMain();
        int i = 0;

        public NotifyIcon getNotifyIcon() {
            return notifyIcon1;
        }

        public MainWindow(){
            InitializeComponent();
            //minimize window
            this.ShowInTaskbar = false;
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            try {
                // register the event that is fired after the key press.
                hook.KeyPressed += new EventHandler<KeyPressedEventArgs>(hook_KeyPressed);
                // register the control + alt + N combination as hot key.
                hook.RegisterHotKey((ModifierKeys)2 | (ModifierKeys)1, Keys.N);
                // register the control + alt + B combination as hot key.
                hookInput.KeyPressed += new EventHandler<KeyPressedEventArgs>(hookInput_KeyPressed);
                hookInput.RegisterHotKey((ModifierKeys)2 | (ModifierKeys)1, Keys.B);
                txtbox_search_shortkey.Text = "Strg + Alt + N";
            }
            catch (InvalidOperationException e) {
                System.Windows.Forms.MessageBox.Show("NinjaTranslate couldn't register the necessary hotkeys. It seems like another program uses them. Try to close them :)", "NinjaTranslate found an error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        void hook_KeyPressed(object sender, KeyPressedEventArgs e){
            notifyIcon1.BalloonTipText = nt.getTranslation(true) + i++ + "\n";
            notifyIcon1.ShowBalloonTip(500);
        }


        void hookInput_KeyPressed(object sender, KeyPressedEventArgs e) {
            InputForm iF = new InputForm(this);
            iF.Show();
            iF.Activate(); // brings the form to the front and gives it focus.
        }

        private void txtbox_search_shortkey_focused(object sender, EventArgs e) {
            txtbox_search_shortkey.Text = "";
            description.Text = "Make sure that the shortkeys are not used by any other active program, or NinjaTranslate might not work properly.";
        }

        private void txtbox_search_shortkey_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e) {
            // shows the current shortkey combination
            txtbox_search_shortkey.Text = e.Modifiers.ToString() + " + " + e.KeyCode.ToString();          
            // TODO: check if current combination is valid.
        }

        // KeyUp funzt zwar, aber nicht besonders gut.
        private void txtbox_search_shortkey_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e) {
                hook.Dispose();
                this.Focus();
                hook = new KeyboardHook();
                hook.KeyPressed += new EventHandler<KeyPressedEventArgs>(hook_KeyPressed);
                hook.RegisterHotKey((ModifierKeys)2 | (ModifierKeys)1, e.KeyCode);           
        }

        private void txtbox_search_shortkey_Leave(object sender, EventArgs e) {
            description.Text = "";
        }

        private void notifyIcon1_Click(object sender, EventArgs e) {
            this.WindowState = FormWindowState.Normal;
        }

        private void Form1_SizeChanged(object sender, EventArgs e) {
            if (this.WindowState == FormWindowState.Minimized) {
                this.notifyIcon1.Visible = true;
                notifyIcon1.BalloonTipText = "NinjaTranslate has been minimized";
                notifyIcon1.ShowBalloonTip(1000);
            }
            if (this.WindowState == FormWindowState.Normal) {
                this.notifyIcon1.Visible = false;
                this.ShowInTaskbar = true;
            }
        }

        // FIXME: lousy hack, to make sure that pdf-files work 
        // still doesn't really work (from time to time) we don't find the marked text.
        private void textBox1_TextChanged(object sender, EventArgs e) {
            notifyIcon1.BalloonTipText = nt.getTranslation(true) + i++ + "\n";
            notifyIcon1.ShowBalloonTip(500);
        }

        /// <summary>
        /// Shows the history form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void History_Btn_Click(object sender, EventArgs e) {
            HistoryForm iH = new HistoryForm();
            iH.Show();
            iH.Activate();
        }
    }
}
