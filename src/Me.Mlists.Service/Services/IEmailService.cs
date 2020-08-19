using System.Threading.Tasks;

namespace Me.Mlists.Service.Services
{
    public interface IEmailService
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage);
    }
}