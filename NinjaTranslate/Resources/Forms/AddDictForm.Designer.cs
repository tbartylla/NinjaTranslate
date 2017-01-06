namespace NinjaTranslate.Resources.Forms {
    partial class AddDictForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.btn_browse_dict = new System.Windows.Forms.Button();
            this.pathTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.keyTextBox = new System.Windows.Forms.TextBox();
            this.addButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_browse_dict
            // 
            this.btn_browse_dict.Location = new System.Drawing.Point(437, 28);
            this.btn_browse_dict.Margin = new System.Windows.Forms.Padding(4);
            this.btn_browse_dict.Name = "btn_browse_dict";
            this.btn_browse_dict.Size = new System.Drawing.Size(96, 27);
            this.btn_browse_dict.TabIndex = 21;
            this.btn_browse_dict.Text = "Browse";
            this.btn_browse_dict.UseVisualStyleBackColor = true;
            this.btn_browse_dict.Click += new System.EventHandler(this.btn_browse_dict_Click);
            // 
            // pathTextBox
            // 
            this.pathTextBox.Enabled = false;
            this.pathTextBox.Location = new System.Drawing.Point(175, 30);
            this.pathTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.pathTextBox.Name = "pathTextBox";
            this.pathTextBox.ReadOnly = true;
            this.pathTextBox.Size = new System.Drawing.Size(256, 22);
            this.pathTextBox.TabIndex = 20;
            this.pathTextBox.Text = "Path to Dictionary";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 33);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 17);
            this.label4.TabIndex = 22;
            this.label4.Text = "Dictionary file:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 63);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 17);
            this.label1.TabIndex = 24;
            this.label1.Text = "Name";
            // 
            // keyTextBox
            // 
            this.keyTextBox.Location = new System.Drawing.Point(175, 60);
            this.keyTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.keyTextBox.Name = "keyTextBox";
            this.keyTextBox.Size = new System.Drawing.Size(256, 22);
            this.keyTextBox.TabIndex = 23;
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(175, 102);
            this.addButton.Margin = new System.Windows.Forms.Padding(4);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(96, 27);
            this.addButton.TabIndex = 25;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // AddDictForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 147);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.keyTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btn_browse_dict);
            this.Controls.Add(this.pathTextBox);
            this.Name = "AddDictForm";
            this.Text = "Add Dictionary";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_browse_dict;
        private System.Windows.Forms.TextBox pathTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox keyTextBox;
        private System.Windows.Forms.Button addButton;
    }
}