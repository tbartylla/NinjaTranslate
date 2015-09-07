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
    public partial class InputForm : Form, ITranslationSource {

        ITranslationService translationService;
        INotificationService notificationService;

        bool showNotification = false;

        public InputForm() {
            InitializeComponent();
        }

        private void InputForm_Leave(object sender, EventArgs e) {
            this.Hide();
        }

        private void InputForm_Deactivate(object sender, EventArgs e) {
            this.Hide();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == (char)13) { // Enter key
                string translation = this.translationService.Translate(this.textBox1.Text.ToString());
                if (this.showNotification && this.notificationService != null)
                    this.notificationService.Notify(translation);
                this.textBox1.Text = "type to translate";
                this.Hide();
            }
        }

        public void SetTranslationService(ITranslationService service) {
            this.translationService = service;
        }

        public void SetNotificationService(INotificationService notificationService) {
            this.notificationService = notificationService;
        }

        public string TriggerTranslation(bool includeNotification) {
            this.showNotification = includeNotification;
            this.Show();
            this.Activate();

            return "Translation will be asynchronous shown in notification";
        }
    }
}
