using System;
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
        private bool isExtended = false;

        private int numberOfLines = 0;
        private int heightCompleteTranslationText = 0;

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
        public void ShowNotification(String text, int width, int height) {
            notificationHeader.Text = "NinjaTranslate"; // TODO change into a more useful header text.
            notificationContent.Text = text;
            this.Height = height;
            this.numberOfLines = SendMessage(notificationContent.Handle.ToInt32(), EM_GETLINECOUNT, 0, 0);
            this.heightCompleteTranslationText = (notificationContent.Font.Height) * numberOfLines + 10;

            if (this.heightCompleteTranslationText > this.Height)
                expandLabel.Text = "▲";
            else
                expandLabel.Text = "▼";

            var screen = Screen.FromPoint(Location);
            this.Location = new Point(screen.WorkingArea.Right - width, screen.WorkingArea.Bottom - this.Height);
            this.Width = width;
            this.notificationContent.Width = width;
            this.Show();
            this.Activate();
        }

        private void NotificationForm_Leave(object sender, EventArgs e) {
            this.Hide();
        }
        private void NotificationForm_Deactivate(object sender, EventArgs e) {
            this.Hide();
        }

        /// <summary>
        /// expands the notificationForm in height, until the whole translation content fits in the forms textbox.
        /// </summary>
        public void expand() {
            var screen = Screen.FromPoint(Location);
            // expand animation
            this.expandLabel.Text = "▼";
            //maximum allowed siz
            Size s = SystemInformation.MaxWindowTrackSize;
            while (this.Height < heightCompleteTranslationText && this.Height < s.Height) {
                this.Height += 10;
                this.Location = new Point(screen.WorkingArea.Right - this.Width, screen.WorkingArea.Bottom - this.Height); 
                this.Update(); // repaint the form 
                Thread.Sleep(20);
            }
        }

        // The little "▲" Symbol on the upper left corner.
        private void expandLabel_Click(object sender, EventArgs e) {
            expand();
        }

        //makes sure that the NotificationForm loses focus as soon, as the user presses any button.
        private void NotificationForm_KeyPress(object sender, KeyPressEventArgs e) {
            var numberOfLines = SendMessage(notificationContent.Handle.ToInt32(), EM_GETLINECOUNT, 0, 0);
            var heightCompleteTranslationText = (notificationContent.Font.Height) * numberOfLines + 10;
            if (e.KeyChar == (char)Keys.Enter && this.Height < heightCompleteTranslationText && !this.isExtended) {
                this.isExtended = true;
                this.expand();
            }
            else {
                this.isExtended = false;
                this.Hide();
            }
        }
    }
}
