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
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.txtbox_search_shortkey = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.description = new System.Windows.Forms.Label();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.history_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 113);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "current search shortkey:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(10, 12);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(343, 81);
            this.textBox1.TabIndex = 1;
            // 
            // txtbox_search_shortkey
            // 
            this.txtbox_search_shortkey.Location = new System.Drawing.Point(10, 129);
            this.txtbox_search_shortkey.Name = "txtbox_search_shortkey";
            this.txtbox_search_shortkey.ReadOnly = true;
            this.txtbox_search_shortkey.Size = new System.Drawing.Size(343, 20);
            this.txtbox_search_shortkey.TabIndex = 5;
            this.txtbox_search_shortkey.Click += new System.EventHandler(this.txtbox_search_shortkey_focused);
            this.txtbox_search_shortkey.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtbox_search_shortkey_KeyDown);
            this.txtbox_search_shortkey.Leave += new System.EventHandler(this.txtbox_search_shortkey_Leave);
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Location = new System.Drawing.Point(10, 161);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(343, 2);
            this.label2.TabIndex = 6;
            this.label2.Text = "label2";
            // 
            // description
            // 
            this.description.BackColor = System.Drawing.SystemColors.Control;
            this.description.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.description.Location = new System.Drawing.Point(10, 163);
            this.description.Name = "description";
            this.description.Size = new System.Drawing.Size(343, 31);
            this.description.TabIndex = 7;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "NinjaTranslate";
            this.notifyIcon1.Click += new System.EventHandler(this.notifyIcon1_Click);
            // 
            // history_btn
            // 
            this.history_btn.Location = new System.Drawing.Point(272, 203);
            this.history_btn.Name = "history_btn";
            this.history_btn.Size = new System.Drawing.Size(80, 23);
            this.history_btn.TabIndex = 8;
            this.history_btn.Text = "Show History";
            this.history_btn.UseVisualStyleBackColor = true;
            this.history_btn.Click += new System.EventHandler(this.History_Btn_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(369, 238);
            this.Controls.Add(this.history_btn);
            this.Controls.Add(this.description);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtbox_search_shortkey);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.Text = "NinjaTranslate Test";
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox txtbox_search_shortkey;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label description;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Button history_btn;
    }
}

