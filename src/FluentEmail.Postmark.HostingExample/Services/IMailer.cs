using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluentEmail.Postmark.HostingExample.Services
{
    internal interface IMailer
    {
        Task SendExample(string to);
    }
}
