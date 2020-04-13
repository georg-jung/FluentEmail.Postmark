# FluentEmail.Postmark

[![NuGet version (FluentEmail.Postmark)](https://img.shields.io/nuget/v/FluentEmail.Postmark.svg?style=flat)](https://www.nuget.org/packages/FluentEmail.Postmark/)
[![Build Status](https://dev.azure.com/georg-jung/FluentEmail.Postmark/_apis/build/status/georg-jung.FluentEmail.Postmark?branchName=master)](https://dev.azure.com/georg-jung/FluentEmail.Postmark/_build/latest?definitionId=9&branchName=master)

This library enables you to use [Postmark](https://postmarkapp.com/) as a sender for [FluentEmail](https://github.com/lukencode/FluentEmail/).

## Getting Started

Install from NuGet

    PM> Install-Package FluentEmail.Postmark

Basic Usage

```csharp
Email.DefaultSender = new PostmarkSender("POSTMARK_API_TEST");

var response = await Email
    .From("john@email.com", "Postmark Sender Support")
    .To("bob@email.com")
    .Subject("hows it going bob")
    .Body("yo dawg, sup?")
    .SendAsync();
```

For more examples see the examples of FluentEmail itself or check the [example project](src/FluentEmail.Postmark.Example).
