using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using MailSender.Library.Data;
using MailSender.Library.Entities;
using Prism.Commands;
using Prism.Mvvm;

namespace MailSender.ViewModels
{
    public class DistributionGroupVM : BindableBase
    {
        public List<Server> Servers => DevData.Servers;
        public List<Sender> Senders => DevData.Senders;
        public List<Recipient> Recipients => DevData.Recipients;

        public void SwitchToScheduler(TabControl tc)
        {
            tc.TabIndex = 1;
        }
    }
}
