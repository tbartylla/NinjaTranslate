using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjaTranslate {
    class BubbleNotification : INotificationService {

        private MainWindow form;
        int duration = 500;

        public void SetForm(MainWindow form) {
            this.form = form;
        }

        public void SetNotificationDuration(int ms) {
            this.duration = ms;
        }

        public void Notify(String message) {
            if (this.form != null)
                this.form.ShowNotification(message, duration);
        }
    }
}
