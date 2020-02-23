using System.Collections.Generic;
using MailSender.Library.Entities.Base;

namespace MailSender.Infrastructure.Services.Interfaces
{
    public interface IEntityExportToExcel<in T> where T : BaseEntity
    {
        void ExportToExcel(IEnumerable<T> items);
    }
}
