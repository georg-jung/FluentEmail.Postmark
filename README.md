# FluentEmail.Postmark

This library enables you to use [Postmark](https://postmarkapp.com/) as a sender for [FluentEmail](https://github.com/lukencode/FluentEmail/).

## Getting Started

Install from NuGet

    PM> Install-Package FluentEmail.Postmark

Basic Usage

    Email.DefaultSender = new PostmarkSender("POSTMARK_API_TEST");

    var response = await Email
        .From("john@email.com", "Postmark Sender Support")
        .To("bob@email.com")
        .Subject("hows it going bob")
        .Body("yo dawg, sup?")
        .SendAsync();

For more examples see the examples of FluentEmail itself or check the [example project](src/FluentEmail.Postmark.Example).
