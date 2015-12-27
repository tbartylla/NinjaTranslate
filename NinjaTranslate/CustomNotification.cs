using NinjaTranslate.Resources.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NinjaTranslate.Resources {
    class CustomNotification : INotificationService{

        private NotificationForm form;
        int duration = 500;

        public void SetForm(Form form) {
            this.form = (NotificationForm) form;
        }

        public void SetNotificationDuration(int ms) {
            this.duration = ms;
        }

        public void Notify(String message) {
            if (this.form != null)
                this.form.ShowNotification(message, duration);
            else 
                throw new NotImplementedException();
        }
    }
}
