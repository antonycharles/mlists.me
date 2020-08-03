using System.Threading.Tasks;

namespace me.mlists.service.Services
{
    public interface IEmailService
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage);
    }
}