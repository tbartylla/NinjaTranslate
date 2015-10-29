namespace NinjaTranslate
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.txtbox_search_shortkey = new System.Windows.Forms.TextBox();
            this.description = new System.Windows.Forms.Label();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenu1 = new System.Windows.Forms.ContextMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.btn_history = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.numeric_clipboardAccess = new System.Windows.Forms.NumericUpDown();
            this.numeric_notification = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_add_dict = new System.Windows.Forms.Button();
            this.btn_browse_tree = new System.Windows.Forms.Button();
            this.textBox_path_to_tree = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btn_browse_dict = new System.Windows.Forms.Button();
            this.textBox_path_to_dict = new System.Windows.Forms.TextBox();
            this.comboBox_dict = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btn_restore_shortkeys = new System.Windows.Forms.Button();
            this.txtbox_expand_shortkey = new System.Windows.Forms.TextBox();
            this.txtbox_openinput_shortkey = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btn_save = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_clipboardAccess)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_notification)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtbox_search_shortkey
            // 
            this.txtbox_search_shortkey.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtbox_search_shortkey.Location = new System.Drawing.Point(210, 22);
            this.txtbox_search_shortkey.Name = "txtbox_search_shortkey";
            this.txtbox_search_shortkey.ReadOnly = true;
            this.txtbox_search_shortkey.Size = new System.Drawing.Size(129, 20);
            this.txtbox_search_shortkey.TabIndex = 5;
            this.txtbox_search_shortkey.Click += new System.EventHandler(this.txtbox_search_shortkey_focused);
            this.txtbox_search_shortkey.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtbox_search_shortkey_KeyDown);
            this.txtbox_search_shortkey.Leave += new System.EventHandler(this.txtbox_search_shortkey_Leave);
            // 
            // description
            // 
            this.description.BackColor = System.Drawing.SystemColors.Control;
            this.description.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.description.Location = new System.Drawing.Point(16, 442);
            this.description.Name = "description";
            this.description.Size = new System.Drawing.Size(343, 31);
            this.description.TabIndex = 7;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon1.ContextMenu = this.contextMenu1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "NinjaTranslate";
            this.notifyIcon1.Click += new System.EventHandler(this.notifyIcon1_Click);
            // 
            // contextMenu1
            // 
            this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.menuItem2,
            this.menuItem3});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.Text = "Settings";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 1;
            this.menuItem2.Text = "Donate";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 2;
            this.menuItem3.Text = "Exit";
            this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
            // 
            // btn_history
            // 
            this.btn_history.Location = new System.Drawing.Point(9, 488);
            this.btn_history.Name = "btn_history";
            this.btn_history.Size = new System.Drawing.Size(80, 23);
            this.btn_history.TabIndex = 8;
            this.btn_history.Text = "Show History";
            this.btn_history.UseVisualStyleBackColor = true;
            this.btn_history.Click += new System.EventHandler(this.Btn_history_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Notification Duration:";
            this.toolTip1.SetToolTip(this.label3, "Defines the duration of how long the translation \r\nwill be shown on the screen af" +
        "ter an user interaction");
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.numeric_clipboardAccess);
            this.groupBox1.Controls.Add(this.numeric_notification);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(10, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(349, 81);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Config";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(319, 51);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(20, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "ms";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(319, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(20, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "ms";
            // 
            // numeric_clipboardAccess
            // 
            this.numeric_clipboardAccess.Location = new System.Drawing.Point(210, 49);
            this.numeric_clipboardAccess.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numeric_clipboardAccess.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numeric_clipboardAccess.Name = "numeric_clipboardAccess";
            this.numeric_clipboardAccess.Size = new System.Drawing.Size(103, 20);
            this.numeric_clipboardAccess.TabIndex = 14;
            this.numeric_clipboardAccess.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numeric_clipboardAccess.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numeric_clipboardAccess.ValueChanged += new System.EventHandler(this.numeric_clipboardAccess_ValueChanged);
            // 
            // numeric_notification
            // 
            this.numeric_notification.Location = new System.Drawing.Point(210, 25);
            this.numeric_notification.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numeric_notification.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numeric_notification.Name = "numeric_notification";
            this.numeric_notification.Size = new System.Drawing.Size(103, 20);
            this.numeric_notification.TabIndex = 13;
            this.numeric_notification.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numeric_notification.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numeric_notification.ValueChanged += new System.EventHandler(this.numeric_notification_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(121, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Clipboard Access Timer:";
            this.toolTip1.SetToolTip(this.label4, "Defines the time of how long NinjaTranslate waits until\r\nit takes the data from t" +
        "he clipboard. Increase the timer if\r\nNinjaTranslate ist having troubles translat" +
        "ing the selected\r\nword.");
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 54);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(94, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "Path to Dictionary:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_add_dict);
            this.groupBox2.Controls.Add(this.btn_browse_tree);
            this.groupBox2.Controls.Add(this.textBox_path_to_tree);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.btn_browse_dict);
            this.groupBox2.Controls.Add(this.textBox_path_to_dict);
            this.groupBox2.Controls.Add(this.comboBox_dict);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(10, 97);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(349, 184);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Dictionaries";
            // 
            // btn_add_dict
            // 
            this.btn_add_dict.Location = new System.Drawing.Point(8, 145);
            this.btn_add_dict.Name = "btn_add_dict";
            this.btn_add_dict.Size = new System.Drawing.Size(103, 22);
            this.btn_add_dict.TabIndex = 23;
            this.btn_add_dict.Text = "Add Dictionary";
            this.btn_add_dict.UseVisualStyleBackColor = true;
            this.btn_add_dict.Click += new System.EventHandler(this.Btn_add_dict_Click);
            // 
            // btn_browse_tree
            // 
            this.btn_browse_tree.Enabled = false;
            this.btn_browse_tree.Location = new System.Drawing.Point(268, 108);
            this.btn_browse_tree.Name = "btn_browse_tree";
            this.btn_browse_tree.Size = new System.Drawing.Size(72, 22);
            this.btn_browse_tree.TabIndex = 22;
            this.btn_browse_tree.Text = "Browse";
            this.btn_browse_tree.UseVisualStyleBackColor = true;
            this.btn_browse_tree.Click += new System.EventHandler(this.Btn_browse_tree_Click);
            // 
            // textBox_path_to_tree
            // 
            this.textBox_path_to_tree.Location = new System.Drawing.Point(9, 109);
            this.textBox_path_to_tree.Name = "textBox_path_to_tree";
            this.textBox_path_to_tree.ReadOnly = true;
            this.textBox_path_to_tree.Size = new System.Drawing.Size(252, 20);
            this.textBox_path_to_tree.TabIndex = 21;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 93);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(107, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "Path to Patricia Tree:";
            // 
            // btn_browse_dict
            // 
            this.btn_browse_dict.Enabled = false;
            this.btn_browse_dict.Location = new System.Drawing.Point(268, 69);
            this.btn_browse_dict.Name = "btn_browse_dict";
            this.btn_browse_dict.Size = new System.Drawing.Size(72, 22);
            this.btn_browse_dict.TabIndex = 19;
            this.btn_browse_dict.Text = "Browse";
            this.btn_browse_dict.UseVisualStyleBackColor = true;
            this.btn_browse_dict.Click += new System.EventHandler(this.Btn_browse_dict_Click);
            // 
            // textBox_path_to_dict
            // 
            this.textBox_path_to_dict.Location = new System.Drawing.Point(9, 70);
            this.textBox_path_to_dict.Name = "textBox_path_to_dict";
            this.textBox_path_to_dict.ReadOnly = true;
            this.textBox_path_to_dict.Size = new System.Drawing.Size(252, 20);
            this.textBox_path_to_dict.TabIndex = 18;
            // 
            // comboBox_dict
            // 
            this.comboBox_dict.FormattingEnabled = true;
            this.comboBox_dict.Items.AddRange(new object[] {
            "Beispiel Dict"});
            this.comboBox_dict.Location = new System.Drawing.Point(210, 22);
            this.comboBox_dict.Name = "comboBox_dict";
            this.comboBox_dict.Size = new System.Drawing.Size(129, 21);
            this.comboBox_dict.TabIndex = 1;
            this.comboBox_dict.Text = "select Dictionary";
            this.comboBox_dict.SelectedIndexChanged += new System.EventHandler(this.ComboBox_dict_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Dictionary:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btn_restore_shortkeys);
            this.groupBox3.Controls.Add(this.txtbox_expand_shortkey);
            this.groupBox3.Controls.Add(this.txtbox_openinput_shortkey);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.txtbox_search_shortkey);
            this.groupBox3.Location = new System.Drawing.Point(10, 287);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(349, 147);
            this.groupBox3.TabIndex = 19;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Shortkeys";
            // 
            // btn_restore_shortkeys
            // 
            this.btn_restore_shortkeys.Location = new System.Drawing.Point(9, 107);
            this.btn_restore_shortkeys.Name = "btn_restore_shortkeys";
            this.btn_restore_shortkeys.Size = new System.Drawing.Size(103, 22);
            this.btn_restore_shortkeys.TabIndex = 24;
            this.btn_restore_shortkeys.Text = "Restore Default";
            this.btn_restore_shortkeys.UseVisualStyleBackColor = true;
            this.btn_restore_shortkeys.Click += new System.EventHandler(this.Btn_restore_shortkeys_Click);
            // 
            // txtbox_expand_shortkey
            // 
            this.txtbox_expand_shortkey.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtbox_expand_shortkey.Location = new System.Drawing.Point(210, 74);
            this.txtbox_expand_shortkey.Name = "txtbox_expand_shortkey";
            this.txtbox_expand_shortkey.ReadOnly = true;
            this.txtbox_expand_shortkey.Size = new System.Drawing.Size(129, 20);
            this.txtbox_expand_shortkey.TabIndex = 22;
            // 
            // txtbox_openinput_shortkey
            // 
            this.txtbox_openinput_shortkey.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtbox_openinput_shortkey.Location = new System.Drawing.Point(210, 48);
            this.txtbox_openinput_shortkey.Name = "txtbox_openinput_shortkey";
            this.txtbox_openinput_shortkey.ReadOnly = true;
            this.txtbox_openinput_shortkey.Size = new System.Drawing.Size(129, 20);
            this.txtbox_openinput_shortkey.TabIndex = 21;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 77);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(107, 13);
            this.label11.TabIndex = 20;
            this.label11.Text = "Expand Output Form:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 51);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(89, 13);
            this.label10.TabIndex = 19;
            this.label10.Text = "Open Input Form:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 25);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(123, 13);
            this.label9.TabIndex = 18;
            this.label9.Text = "Translate Selected Text:";
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(279, 488);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(80, 23);
            this.btn_save.TabIndex = 20;
            this.btn_save.Text = "Save";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.Btn_save_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(193, 488);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(80, 23);
            this.btn_cancel.TabIndex = 21;
            this.btn_cancel.Text = "Cancel";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.Btn_cancel_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ShowAlways = true;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(369, 520);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_history);
            this.Controls.Add(this.description);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.Text = "NinjaTranslate - Settings";
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_clipboardAccess)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_notification)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }


        #endregion

        private System.Windows.Forms.ContextMenu contextMenu1;
        private System.Windows.Forms.TextBox txtbox_search_shortkey;
        private System.Windows.Forms.Label description;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Button btn_history;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numeric_clipboardAccess;
        private System.Windows.Forms.NumericUpDown numeric_notification;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_add_dict;
        private System.Windows.Forms.Button btn_browse_tree;
        private System.Windows.Forms.TextBox textBox_path_to_tree;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btn_browse_dict;
        private System.Windows.Forms.TextBox textBox_path_to_dict;
        private System.Windows.Forms.ComboBox comboBox_dict;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btn_restore_shortkeys;
        private System.Windows.Forms.TextBox txtbox_expand_shortkey;
        private System.Windows.Forms.TextBox txtbox_openinput_shortkey;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItem3;
    }
}

