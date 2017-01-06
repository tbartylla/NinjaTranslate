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
using NinjaTranslate.Resources.Forms;

namespace NinjaTranslate
{
    public partial class MainWindow : Form {
        
        // HistoryForm iH = new HistoryForm();
        NotificationForm notificationForm = new NotificationForm();
        Dictionary<string, string> dictionaries;
        string currentKeyConfig = "";
        string quickChangeKeyConfig = "";

        public MainWindow() {
            InitializeComponent();

            //sets the numeric textfields to the values saved in the config.ini
            this.numeric_clipboardAccess.Value = new decimal(new int[] {
                Int32.Parse(Config.GetValue("clipboardAccessTimer")),0,0,0});
            this.numeric_window_width.Value = new decimal(new int[] {
                Int32.Parse(Config.GetValue("windowWidth")),0,0,0});
            this.numeric_window_height.Value = new decimal(new int[] {
                Int32.Parse(Config.GetValue("windowHeight")),0,0,0});

            this.dictionaries = Config.GetMultiValue("path");
            this.comboBox_dict.Items.Add("please select");
            this.comboBox_dict.SelectedIndex = 0;
            foreach (KeyValuePair<string, string> dict in dictionaries) {
                this.comboBox_dict.Items.Add(dict.Key);
            }

            if (Int32.Parse(Config.GetValue("maxTreesInMemory")) > 1)
                this.quickChangeInMemory.Checked = true;

            this.quickChangeKeyConfig = Config.GetValue("quickchangeKey");
            this.currentKeyConfig = Config.GetValue("currentKey");

            MinimizeForm();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
            MinimizeForm();
            e.Cancel = true;
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
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void Form1_SizeChanged(object sender, EventArgs e) {
            if (this.WindowState == FormWindowState.Minimized) {
                this.notifyIcon1.Visible = true;
                //notifyIcon1.BalloonTipText = "NinjaTranslate has been minimized";
                //notifyIcon1.ShowBalloonTip(1000);
                this.ShowInTaskbar = false;
                this.Visible = false;
            }
            if (this.WindowState == FormWindowState.Normal) {
                this.notifyIcon1.Visible = false;
                this.ShowInTaskbar = true;
            }
        }

        // History
        private void Btn_history_Click(object sender, EventArgs e) {
            // TODO show History Form
            notificationForm.Show(); 
            // iH.Show();
        }

        // Dictionary Location
        private void Btn_browse_dict_Click(object sender, EventArgs e) {
            // Displays an OpenFileDialog
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Text Files|*.txt";
            openFileDialog1.Title = "Select a Dictionary";

            // Show the Dialog.
            // If the user clicked OK in the dialog then paste the path to its textbox.
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                textBox_path_to_dict.Text = openFileDialog1.FileName;
            }
        }

        // Add Dictionary
        private void Btn_add_dict_Click(object sender, EventArgs e) {
            AddDictForm adf = new AddDictForm(this);
            adf.ShowDialog();
        }

        // Restore Default Shortkeys
        private void Btn_restore_shortkeys_Click(object sender, EventArgs e) {
            
        }

        // Cancel
        private void Btn_cancel_Click(object sender, EventArgs e) {
            MinimizeForm();
        }

        // Save
        private void Btn_save_Click(object sender, EventArgs e) {
            Config.SetSingleValue("windowWidth", this.numeric_window_width.Value.ToString());
            Config.SetSingleValue("windowHeight", this.numeric_window_height.Value.ToString());
            Config.SetSingleValue("clipboardAccessTimer", this.numeric_clipboardAccess.Value.ToString());
            Config.SetMultiValue("path", this.dictionaries);
            Config.SetSingleValue("currentKey", this.currentKeyConfig);
            Config.SetSingleValue("quickchangeKey", this.quickChangeKeyConfig);

            if (this.quickChangeInMemory.Checked)
                Config.SetSingleValue("maxTreesInMemory", "2");
            else
                Config.SetSingleValue("maxTreesInMemory", "1");
            Config.Save();
            MinimizeForm();
        }

        // Select Dictionary
        private void ComboBox_dict_SelectedIndexChanged(object sender, EventArgs e) {
            if (this.comboBox_dict.SelectedIndex > 0) {
                this.keyTextBox.Text = this.comboBox_dict.SelectedItem.ToString();
                this.textBox_path_to_dict.Text = this.dictionaries[this.comboBox_dict.SelectedItem.ToString()];

                this.deleteButton.Enabled = true;
                this.btn_browse_dict.Enabled = true;

                this.currentKey.Enabled = true;
                if (this.currentKeyConfig == this.comboBox_dict.SelectedItem.ToString())
                    this.currentKey.Checked = true;
                else
                    this.currentKey.Checked = false;

                this.quickChangeKey.Enabled = true;
                if (this.quickChangeKeyConfig == this.comboBox_dict.SelectedItem.ToString())
                    this.quickChangeKey.Checked = true;
                else
                    this.quickChangeKey.Checked = false;

            } 
            else {
                this.keyTextBox.Enabled = false;
                this.textBox_path_to_dict.Enabled = false;
                this.deleteButton.Enabled = false;
                this.btn_browse_dict.Enabled = false;
                this.currentKey.Enabled = false;
                this.quickChangeKey.Enabled = false;
            }
        }

        // Settings
        private void menuItem1_Click(object sender, EventArgs e) {
            notifyIcon1_Click(sender, e);
        }

        // Exit NinjaTranslate
        private void menuItem3_Click(object sender, EventArgs e) {
            System.Windows.Forms.Application.Exit();
        }

        private void currentKey_CheckedChanged(object sender, EventArgs e) {
            if (this.currentKey.Checked)
                this.currentKeyConfig = this.comboBox_dict.SelectedItem.ToString();
        }

        private void quickChangeKey_CheckedChanged(object sender, EventArgs e) {
            if (this.quickChangeKey.Checked)
                this.quickChangeKeyConfig = this.comboBox_dict.SelectedItem.ToString();
        }

        private void deleteButton_Click(object sender, EventArgs e) {
            this.dictionaries[this.comboBox_dict.SelectedItem.ToString()] = null;
            this.comboBox_dict.Items.Remove(this.comboBox_dict.SelectedItem);
            //TODO created tree should be deleted when this happens
        }

        private void infoButton_Click(object sender, EventArgs e) {
            System.Windows.MessageBox.Show("NinjaTranslate offers you to handle multiple dictionaries at once. All you need is a file containing words, followed by a tab and the translation(s). You can specify this file here using the Add-button. \n \n You can also select which dictionary should be loaded when NinjaTranslate is started, and which dictionary should be accessible by using the quick-change button.", "NinjaTranslate");
        }

        public bool addDictionary(string key, string path) {
            if (this.dictionaries.ContainsKey(key))
                return false;
            this.dictionaries.Add(key, path);
            this.comboBox_dict.SelectedIndex = this.comboBox_dict.Items.Add(key);
            return true;
        }
    }
}
