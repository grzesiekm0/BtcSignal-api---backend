using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Btcsignal.Core.Inerfaces.Services
{
    public interface IMailService
    {

        Task SendEmailAsync(string toEmail, string subject, string content);
    }
}

