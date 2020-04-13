using System;
using System.Threading.Tasks;
using FluentEmail.Core;

namespace FluentEmail.Postmark.Example
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Email.DefaultSender = new PostmarkSender("POSTMARK_API_TEST");

            var response = await Email
                .From("john@email.com", "Postmark Sender Support")
                .To("bob@email.com")
                .Subject("hows it going bob")
                .Body("yo dawg, sup?")
                .SendAsync();

            var m = new ExampleModel()
            {
                ProductName = "Postmark Sender",
                Headline = "Nice Example!",
                FirstParagraph = "This is the cool text we want to send to our users."
            };
            response = await Email
                .From("john@email.com", "noreply")
                .To("bob@email.com", "bob")
                .Subject("hows it going bob")
                .UsingTemplateFromEmbedded("FluentEmail.Postmark.Example.Example.cshtml", m, typeof(Program).Assembly)
                .SendAsync();
        }
    }
}
