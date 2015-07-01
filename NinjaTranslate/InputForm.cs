using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NinjaTranslate {
    public partial class InputForm : Form {
        MainWindow mw;
        public InputForm(object sender) {
            InitializeComponent();
            mw = (MainWindow)sender;
        }

        private void InputForm_Leave(object sender, EventArgs e) {
            this.Close();
        }

        private void InputForm_Deactivate(object sender, EventArgs e) {
            this.Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == (char)13) { // Enter key
                mw.getNotifyIcon().BalloonTipText = DictionaryReader.translate(this.textBox1.Text.ToString());
                mw.getNotifyIcon().ShowBalloonTip(500);
                this.Close();
            }
        }
    }
}
