using System.Collections.Generic;
using MailSender.Library.Entities.Base;

namespace MailSender.Infrastructure.Services.Interfaces
{
    public interface ISaveReport<in T> where T : BaseEntity
    {
        void SaveReport(IEnumerable<T> items);
    }
}
