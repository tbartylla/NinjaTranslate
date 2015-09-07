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
        public MainWindow(){
            InitializeComponent();
            //minimize window when starting
            this.ShowInTaskbar = false;
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            
        }

        public void ShowNotification(String text, int ms) {
            notifyIcon1.BalloonTipText = text + "\n";
            notifyIcon1.ShowBalloonTip(ms);
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
