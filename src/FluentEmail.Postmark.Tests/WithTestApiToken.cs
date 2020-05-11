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
        public void SimpleMailFromCodeSync()
        {
            Email.DefaultSender = new PostmarkSender("POSTMARK_API_TEST");

            var response = Email
                .From("john@email.com", "Postmark Sender Support")
                .To("bob@email.com")
                .Subject("hows it going bob")
                .Body("yo dawg, sup?")
                .Send();

            response.Successful.Should().BeTrue();
        }

        [Fact]
        public async Task SimpleMailFromCode()
        {
            Email.DefaultSender = new PostmarkSender("POSTMARK_API_TEST");

            var response = await Email
                .From("john@email.com")
                .To("bob@email.com")
                .Subject("hows it going bob")
                .Body("yo dawg, sup?")
                .SendAsync()
                .ConfigureAwait(false);

            response.Successful.Should().BeTrue();
            response.MessageId.Should().NotBeNullOrEmpty();
            response.ErrorMessages.Should().BeEmpty();
        }

        [Fact]
        public async Task SimpleMailReplyTo()
        {
            Email.DefaultSender = new PostmarkSender("POSTMARK_API_TEST");

            var response = await Email
                .From("john@email.com")
                .To("bob@email.com")
                .ReplyTo("john+hashcode@email.com")
                .Subject("hows it going bob")
                .Body("yo dawg, sup?")
                .SendAsync()
                .ConfigureAwait(false);

            response.Successful.Should().BeTrue();
            response.MessageId.Should().NotBeNullOrEmpty();
            response.ErrorMessages.Should().BeEmpty();
        }

        [Fact]
        public async Task SimpleMailWithNameFromCode()
        {
            Email.DefaultSender = new PostmarkSender("POSTMARK_API_TEST");

            var response = await Email
                .From("john@email.com", "Postmark Sender Support")
                .To("bob@email.com")
                .Subject("hows it going bob")
                .Body("yo dawg, sup?")
                .SendAsync()
                .ConfigureAwait(false);

            response.Successful.Should().BeTrue();
            response.MessageId.Should().NotBeNullOrEmpty();
            response.ErrorMessages.Should().BeEmpty();
        }

        [Fact]
        public async Task SimpleHtmlMailFromCode()
        {
            Email.DefaultSender = new PostmarkSender("POSTMARK_API_TEST");

            var response = await Email
                .From("john@email.com", "Postmark Sender Support")
                .To("bob@email.com")
                .Subject("hows it going bob")
                .Body("<html><body><h1>Test</h1></body></html>", true)
                .SendAsync()
                .ConfigureAwait(false);

            response.Successful.Should().BeTrue();
        }

        [Fact]
        public async Task SimpleMailWithAttachmentFromCode()
        {
            Email.DefaultSender = new PostmarkSender("POSTMARK_API_TEST");

            var response = await Email
                .From("john@email.com", "Postmark Sender Support")
                .To("bob@email.com")
                .Subject("hows it going bob")
                .Body("yo dawg, sup?")
                .Attach(new Core.Models.Attachment()
                {
                    Filename = "test.txt",
                    Data = new System.IO.MemoryStream(new byte[] { 0, 1, 2, 3, 4, 5, 6, 7 }),
                    ContentType = "application/octet-stream"
                })
                .SendAsync()
                .ConfigureAwait(false);

            response.Successful.Should().BeTrue();
            response.MessageId.Should().NotBeNullOrEmpty();
            response.ErrorMessages.Should().BeEmpty();
        }

        [Fact]
        public async Task SimpleHtmlMailWithAlternFromCode()
        {
            Email.DefaultSender = new PostmarkSender("POSTMARK_API_TEST");

            var response = await Email
                .From("john@email.com", "Postmark Sender Support")
                .To("bob@email.com")
                .Subject("hows it going bob")
                .Body("<html><body><h1>Test</h1></body></html>", true)
                .PlaintextAlternativeBody("Test")
                .SendAsync()
                .ConfigureAwait(false);

            response.Successful.Should().BeTrue();
        }

        [Fact]
        public async Task SimpleMailFromCodeWithOpts()
        {
            var opts = new PostmarkSenderOptions("POSTMARK_API_TEST");
            opts.TrackOpens = true;
            opts.TrackLinks = PostmarkDotNet.LinkTrackingOptions.HtmlAndText;
            opts.Tag = "unittest";
            opts.Metadata = new Dictionary<string, string>() { { "key", "example" } };
            Email.DefaultSender = new PostmarkSender(opts);

            var response = await Email
                .From("john@email.com", "Postmark Sender Support")
                .To("bob@email.com")
                .Subject("hows it going bob")
                .Body("yo dawg, sup?")
                .SendAsync()
                .ConfigureAwait(false);

            response.Successful.Should().BeTrue();
        }

        [Fact]
        public async Task SimpleMailFromCodeWithLowPrio()
        {
            Email.DefaultSender = new PostmarkSender("POSTMARK_API_TEST");

            var response = await Email
                .From("john@email.com", "Postmark Sender Support")
                .To("bob@email.com")
                .Subject("hows it going bob")
                .Body("yo dawg, sup?")
                .LowPriority()
                .SendAsync()
                .ConfigureAwait(false);

            response.Successful.Should().BeTrue();
        }

        [Fact]
        public async Task SimpleMailFromCodeWithHighPrio()
        {
            Email.DefaultSender = new PostmarkSender("POSTMARK_API_TEST");

            var response = await Email
                .From("john@email.com", "Postmark Sender Support")
                .To("bob@email.com")
                .Subject("hows it going bob")
                .Body("yo dawg, sup?")
                .HighPriority()
                .SendAsync()
                .ConfigureAwait(false);

            response.Successful.Should().BeTrue();
        }

        [Fact]
        public async Task SimpleMailFromCodeWithHeaders()
        {
            Email.DefaultSender = new PostmarkSender("POSTMARK_API_TEST");

            var response = await Email
                .From("john@email.com", "Postmark Sender Support")
                .To("bob@email.com")
                .Subject("hows it going bob")
                .Body("yo dawg, sup?")
                .Header("X-Random-Useless-Header", "SomeValue")
                .Header("X-Another-Random-Useless-Header", "AnotherValue")
                .SendAsync()
                .ConfigureAwait(false);

            response.Successful.Should().BeTrue();
        }

        [Fact]
        public void SenderNullServerToken()
        {
            Func<PostmarkSender> fn = () => new PostmarkSender((string)null!);
            fn.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void OptionsNullServerToken()
        {
            Func<PostmarkSenderOptions> fn = () => new PostmarkSenderOptions(null!);
            fn.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void NullOptions()
        {
            Func<PostmarkSender> fn = () => new PostmarkSender((PostmarkSenderOptions)null!);
            fn.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void SendNull()
        {
            var sender = new PostmarkSender("POSTMARK_API_TEST");
            Func<Task> fn = async () => await sender.SendAsync(null!).ConfigureAwait(false);
            fn.Should().Throw<ArgumentNullException>();
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
