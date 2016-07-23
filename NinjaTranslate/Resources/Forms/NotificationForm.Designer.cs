namespace NinjaTranslate.Resources.Forms {
    partial class NotificationForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NotificationForm));
            this.notificationContent = new System.Windows.Forms.TextBox();
            this.notificationHeader = new System.Windows.Forms.Label();
            this.expandLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // notificationContent
            // 
            this.notificationContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.notificationContent.BackColor = System.Drawing.Color.Black;
            this.notificationContent.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.notificationContent.Enabled = false;
            this.notificationContent.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.notificationContent.ForeColor = System.Drawing.Color.Gainsboro;
            this.notificationContent.Location = new System.Drawing.Point(10, 37);
            this.notificationContent.Margin = new System.Windows.Forms.Padding(12, 11, 12, 11);
            this.notificationContent.Multiline = true;
            this.notificationContent.Name = "notificationContent";
            this.notificationContent.ReadOnly = true;
            this.notificationContent.Size = new System.Drawing.Size(541, 305);
            this.notificationContent.TabIndex = 2;
            this.notificationContent.TabStop = false;
            this.notificationContent.Text = "Lorem Ipsum dolor est emir";
            // 
            // notificationHeader
            // 
            this.notificationHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.notificationHeader.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.notificationHeader.Location = new System.Drawing.Point(26, 8);
            this.notificationHeader.Margin = new System.Windows.Forms.Padding(10, 10, 3, 0);
            this.notificationHeader.Name = "notificationHeader";
            this.notificationHeader.Size = new System.Drawing.Size(352, 25);
            this.notificationHeader.TabIndex = 1;
            this.notificationHeader.Text = "label1";
            // 
            // expandLabel
            // 
            this.expandLabel.AutoSize = true;
            this.expandLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.expandLabel.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.expandLabel.Location = new System.Drawing.Point(8, 10);
            this.expandLabel.Name = "expandLabel";
            this.expandLabel.Size = new System.Drawing.Size(20, 18);
            this.expandLabel.TabIndex = 2;
            this.expandLabel.Text = "▲";
            this.expandLabel.Click += new System.EventHandler(this.expandLabel_Click);
            // 
            // NotificationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(562, 343);
            this.ControlBox = false;
            this.Controls.Add(this.expandLabel);
            this.Controls.Add(this.notificationHeader);
            this.Controls.Add(this.notificationContent);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NotificationForm";
            this.Opacity = 0.75D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "NotificationForm";
            this.Deactivate += new System.EventHandler(this.NotificationForm_Deactivate);
            this.Leave += new System.EventHandler(this.NotificationForm_Leave);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox notificationContent;
        private System.Windows.Forms.Label notificationHeader;
        private System.Windows.Forms.Label expandLabel;
    }
}