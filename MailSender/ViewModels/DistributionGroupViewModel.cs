using System.Collections.Generic;
using MailSender.Library.Data;
using MailSender.Library.Entities;
using Prism.Mvvm;

namespace MailSender.ViewModels
{
    public class DistributionGroupViewModel : BindableBase
    {
        public List<Server> Servers => DevData.Servers;
        public List<Sender> Senders => DevData.Senders;
        public List<Recipient> Recipients => DevData.Recipients;

        /*
        public void SwitchToScheduler(TabItem ti)
        {
            var tc = ti.Parent as TabControl;
            tc.TabIndex = 1;
        }
        */
    }
}