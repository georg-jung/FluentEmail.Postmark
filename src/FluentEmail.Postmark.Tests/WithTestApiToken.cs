using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using FluentEmail.Core;
using Xunit;

namespace FluentEmail.Postmark.Tests
{
    public class WithTestApiToken
    {
        [Fact]
        public async Task SimpleMailFromCode()
        {
            Email.DefaultSender = new PostmarkSender("POSTMARK_API_TEST");

            var response = await Email
                .From("john@email.com", "Postmark Sender Support")
                .To("bob@email.com")
                .Subject("hows it going bob")
                .Body("yo dawg, sup?")
                .SendAsync()
                .ConfigureAwait(false);
        }

        [Fact]
        public async Task TooManyRecipients()
        {
            Email.DefaultSender = new PostmarkSender("POSTMARK_API_TEST");

            var mail = Email
                .From("john@email.com", "Postmark Sender Support")
                .Subject("hows it going bob")
                .Body("yo dawg, sup?");

            var recipAdrs = new List<string>();
            for (var i = 0; i < 60; i++)
                recipAdrs.Add($"bob{i}@email.com");

            var recips = recipAdrs.Select(s => new FluentEmail.Core.Models.Address(s)).ToList();
            mail.To(recips);

            Func<Task> act = async () => { await mail.SendAsync().ConfigureAwait(false); };
            await act.Should().ThrowAsync<ArgumentException>().ConfigureAwait(false);
        }
    }
}
