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

using ntutil;

namespace NinjaTranslate
{
    public partial class MainWindow : Form {
        public MainWindow() {
            InitializeComponent();

            //sets the numeric textfields to the values saved in the config.ini
            this.numeric_notification.Value = new decimal(new int[] {
                Int32.Parse(Config.GetValue("notificationDuration")),0,0,0});
            this.numeric_clipboardAccess.Value = new decimal(new int[] {
                Int32.Parse(Config.GetValue("clipboardAccessTimer")),0,0,0});

            //minimize window when starting
            MinimizeForm();
        }

        private void MinimizeForm() {
            this.ShowInTaskbar = false;
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Visible = false;
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
                this.ShowInTaskbar = false;
                this.Visible = false;
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
        private void Btn_history_Click(object sender, EventArgs e) {
            HistoryForm iH = new HistoryForm();
            iH.Show();
            iH.Activate();
        }

        private void Btn_browse_dict_Click(object sender, EventArgs e) {
            // Displays an OpenFileDialog
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Text Files|*.txt";
            openFileDialog1.Title = "Select a Dictionary";

            // Show the Dialog.
            // If the user clicked OK in the dialog then paste the path to its textbox.
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                textBox_path_to_dict.Text = openFileDialog1.FileName;
                Config.SetValue("path", textBox_path_to_dict.Text);
            }
        }

        private void Btn_browse_tree_Click(object sender, EventArgs e) {
            // Displays an OpenFileDialog
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Tree Files|*.tree";
            openFileDialog1.Title = "Select a Patricia Tree";

            // Show the Dialog.
            // If the user clicked OK in the dialog then paste the path to its textbox.
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                textBox_path_to_tree.Text = openFileDialog1.FileName;
                Config.SetValue("path_tree", textBox_path_to_tree.Text);
            }
        }

        private void Btn_add_dict_Click(object sender, EventArgs e) {
            // TODO change stuff here 
            Btn_browse_dict_Click(sender, e);
        }

        private void Btn_restore_shortkeys_Click(object sender, EventArgs e) {
            //TODO Restore Default Shortkeys
        }

        private void Btn_cancel_Click(object sender, EventArgs e) {
            // TODO minimize form
        }

        private void Btn_save_Click(object sender, EventArgs e) {
            Config.Save();
            // TODO minimize form
        }

        private void ComboBox_dict_SelectedIndexChanged(object sender, EventArgs e) {
            textBox_path_to_dict.ReadOnly = false;
            textBox_path_to_tree.ReadOnly = false;
            btn_browse_dict.Enabled = true;
            btn_browse_tree.Enabled = true;
        }

        private void numeric_notification_ValueChanged(object sender, EventArgs e) {
            Config.SetValue("notificationDuration", this.numeric_notification.Value.ToString());
        }

        private void numeric_clipboardAccess_ValueChanged(object sender, EventArgs e) {
            Config.SetValue("clipboardAccessTimer", this.numeric_clipboardAccess.Value.ToString());
        }

        private void menuItem1_Click(object sender, EventArgs e) {
            // TODO Settings
        }

        private void menuItem2_Click(object sender, EventArgs e) {
            // TODO Donate
        }

        private void menuItem3_Click(object sender, EventArgs e) {
            // TODO Exit
        }
    }
}
