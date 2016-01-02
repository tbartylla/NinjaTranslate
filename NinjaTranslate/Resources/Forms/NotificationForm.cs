﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NinjaTranslate.Resources.Forms {
    public partial class NotificationForm : Form {

        private const int EM_GETLINECOUNT = 0xba;
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

        [DllImport("user32", EntryPoint = "SendMessageA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern int SendMessage(int hwnd, int wMsg, int wParam, int lParam);

        public NotificationForm() {
            InitializeComponent();        
            var screen = Screen.FromPoint(Location);
            this.Location = new Point(screen.WorkingArea.Right - this.Width, screen.WorkingArea.Bottom - this.Height);
            Visible = false;
            notificationContent.ReadOnly = true;
            notificationHeader.Text = "NinjaTranslate"; // TODO change into a more useful header text.
            notificationContent.Text = "idk";
        }

        /// <summary>
        /// shows the NotificationForm 
        /// </summary>
        /// <param name="text">content of the form</param>
        /// <param name="ms">duration of how long it will be visible shown on the users screen</param>
        public void ShowNotification(String text, int ms) {
            notificationHeader.Text = "NinjaTranslate"; // TODO change into a more useful header text.
            notificationContent.Text = text;
            if (this.Visible)
                expand();
            this.Show();
            this.Activate();
            CloseFormAfterX(ms);
        }

        /// <summary>
        /// closes the form after a specific time ms
        /// </summary>
        /// <param name="ms">duration of how long the form will be visible shown on the users screen before getting hid</param>
        private void CloseFormAfterX(int ms) {
            timer.Interval = ms;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e) {          
            this.Hide();
        }

        private void NotificationForm_Leave(object sender, EventArgs e) {
            Height = 143; // Height it has, when not expanded.
            this.expandLabel.Text = "▲";
            this.Visible = false;
            this.Hide();
        }

        /// <summary>
        /// expands the notificationForm in height, until the whole translation content fits in the forms textbox.
        /// </summary>
        public void expand() {
            var numberOfLines = SendMessage(notificationContent.Handle.ToInt32(), EM_GETLINECOUNT, 0, 0);
            var heightCompleteTranslationText = (notificationContent.Font.Height) * numberOfLines + 10;
            var screen = Screen.FromPoint(Location);
            // expand animation
            this.expandLabel.Text = "▼";
            while (Height < heightCompleteTranslationText) {
                Height += 10;
                this.Location = new Point(screen.WorkingArea.Right - this.Width, screen.WorkingArea.Bottom - this.Height); 
                this.Update(); // repaint the form 
                Thread.Sleep(20);
            }
        }

        // The little "▲" Symbol on the upper left corner.
        private void expandLabel_Click(object sender, EventArgs e) {
            expand();
        }
    }
}
