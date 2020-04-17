using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentEmail.Core;

namespace FluentEmail.Postmark.HostingExample.Services
{
    internal class FluentMailer : IMailer
    {
        private readonly IFluentEmailFactory emailFactory;

        public FluentMailer(IFluentEmailFactory emailFactory)
        {
            this.emailFactory = emailFactory;
        }

        public async Task SendExample(string to)
        {
            var mail = emailFactory.Create();
            mail.To(to);
            mail.Subject("Example from FluentEmail.Postmark");
            mail.UsingTemplate("This is an example for you @Model.Email!", new { Email = to });
            await mail.SendAsync().ConfigureAwait(false);
        }
    }
}
