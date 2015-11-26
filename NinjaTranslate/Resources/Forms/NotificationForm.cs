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
        [DllImport("user32", EntryPoint = "SendMessageA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern int SendMessage(int hwnd, int wMsg, int wParam, int lParam);

        public NotificationForm() {
            InitializeComponent();        
            var screen = Screen.FromPoint(Location);
            this.Location = new Point(screen.WorkingArea.Right - this.Width, screen.WorkingArea.Bottom - this.Height);
            Visible = false;
            notificationContent.ReadOnly = true;
            notificationHeader.Text = "NinjaTranslate - Lorem Ipsum";
            notificationContent.Text = "Test Lorem Ipsum dolor est im John Willemse suggested, I " +
                "ended up creating the functionality myself. I added a Panel in the form" +
                "that I just set visible or invisible Test Lorem Ipsum dolor est im John Willemse suggested, I " +
                "ended up creating the functionality myself. I added a Panel in the form" +
                "Test Lorem Ipsum dolor est im John Willemse suggested, I " +
                "ended up creating the functionality myself. I added a Panel in the form" +
                "Test Lorem Ipsum dolor est im John Willemse suggested, I " +
                "ended up creating the functionality myself. I added a Panel in the form" +
                "Test Lorem Ipsum dolor est im John Willemse suggested, I " +
                "ended up creating the functionality myself. I added a Panel in the form" +
                "Test Lorem Ipsum dolor est im John Willemse suggested, I " +
                                "Test Lorem Ipsum dolor est im John Willemse suggested, I " +
                "ended up creating the functionality myself. I added a Panel in the form" +
                "Test Lorem Ipsum dolor est im John Willemse suggested, I " +
                "ended up creating the functionality myself. I added a Panel in the form" +
                "Test Lorem Ipsum dolor est im John Willemse suggested, I " +
                "ended up creating the functionality myself. I added a Panel in the form" +
                "Test Lorem Ipsum dolor est im John Willemse suggested, I " +
                                "Test Lorem Ipsum dolor est im John Willemse suggested, I " +
                "ended up creating the functionality myself. I added a Panel in the form" +
                "Test Lorem Ipsum dolor est im John Willemse suggested, I " +
                "ended up creating the functionality myself. I added a Panel in the form" +
                "Test Lorem Ipsum dolor est im John Willemse suggested, I " +
                "ended up creating the functionality myself. I added a Panel in the form" +
                "Test Lorem Ipsum dolor est im John Willemse suggested, I " +
                "ended up creating the functionality myself. I added a Panel in the form";
        }



        /// <summary>
        /// expands the notificationForm in height, until the whole translation content fits in the forms textbox.
        /// </summary>
        public void expand() {
            var numberOfLines = SendMessage(notificationContent.Handle.ToInt32(), EM_GETLINECOUNT, 0, 0);
            var heightCompleteTranslationText = (notificationContent.Font.Height) * numberOfLines + 10;
            var screen = Screen.FromPoint(Location);
            // expand animation
            while (Height < heightCompleteTranslationText) {
                Height += 10;
                this.Location = new Point(screen.WorkingArea.Right - this.Width, screen.WorkingArea.Bottom - this.Height);
                this.expandLabel.Text = "▼";
                this.Update(); // repaint the form 
                Thread.Sleep(20);
            }
        }

        private void expandLabel_Click(object sender, EventArgs e) {
            expand();
        }
    }
}
