using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjaTranslate {
    public interface ITranslationSource {
        void SetTranslationService(ITranslationService service);

        void SetNotificationService(INotificationService service);

        string TriggerTranslation(bool includeNotification);
    }
}
