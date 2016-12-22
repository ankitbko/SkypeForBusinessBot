using Microsoft.Lync.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyncBot.Core
{
    public class PresenceService
    {
        private Self _self;

        public PresenceService(Self self)
        {
            _self = self;
        }

        public void SetPresenceBusy()
        {
            _self.BeginPublishContactInformation(Busy, null, null);
        }

        public void SetPresenceFree()
        {
            _self.BeginPublishContactInformation(Free, null, null);
        }

        public void SetPresenceFreeIdle()
        {
            _self.BeginPublishContactInformation(FreeIdle, null, null);
        }

        private Dictionary<PublishableContactInformationType, object> Busy =
                new Dictionary<PublishableContactInformationType, object>() { { PublishableContactInformationType.Availability, ContactAvailability.Busy } };
        private Dictionary<PublishableContactInformationType, object> Free =
                new Dictionary<PublishableContactInformationType, object>() { { PublishableContactInformationType.Availability, ContactAvailability.Free } };
        private Dictionary<PublishableContactInformationType, object> FreeIdle =
                new Dictionary<PublishableContactInformationType, object>() { { PublishableContactInformationType.Availability, ContactAvailability.FreeIdle } };
    }
}
