using System.Threading.Tasks;

namespace MailSender.Library.Services.Interfaces
{
    public interface IPlugin
    {
        Task InitializeAsync();
        Task StartAsync();
        Task StopAsync();
    }
}