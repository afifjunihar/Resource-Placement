using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Library.Email
{
    public interface IEmailSender
    {
      Task SendEmailAsync(Message message);
    }
}
