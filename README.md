# FluentEmail.Postmark

[![NuGet version (FluentEmail.Postmark)](https://img.shields.io/nuget/v/FluentEmail.Postmark.svg?style=flat)](https://www.nuget.org/packages/FluentEmail.Postmark/)
[![Build Status](https://dev.azure.com/georg-jung/FluentEmail.Postmark/_apis/build/status/georg-jung.FluentEmail.Postmark?branchName=master)](https://dev.azure.com/georg-jung/FluentEmail.Postmark/_build/latest?definitionId=9&branchName=master)

This library enables you to use [Postmark](https://postmarkapp.com/) as a sender for [FluentEmail](https://github.com/lukencode/FluentEmail/).

## Getting Started

Install from NuGet

    PM> Install-Package FluentEmail.Postmark

## Basic Usage

```csharp
Email.DefaultSender = new PostmarkSender("POSTMARK_API_TEST");

var response = await Email
    .From("john@email.com", "Postmark Sender Support")
    .To("bob@email.com")
    .Subject("hows it going bob")
    .Body("yo dawg, sup?")
    .SendAsync();
```

For more examples see the examples of FluentEmail itself or check the [basic example project](src/FluentEmail.Postmark.Example).

## Real world usage

* in ASP.NET Core 3.1
* with DI
* with Razor templates

The FluentEmail package itself is slightly outdated, so the following steps are required to get up and running with a modern 3.x project. Hopefully the situation will [change soon](https://github.com/lukencode/FluentEmail/pull/186), so that this is more straight forward.

A complete project that's set up like the following steps describe can be found under [FluentEmail.Postmark.HostingExample](src/FluentEmail.Postmark.HostingExample).

1. Add this feed to your nuget.config to get an up-to-date version of FluentEmail.Razor:
    * https://pkgs.dev.azure.com/georg-jung/FluentEmail.Postmark/_packaging/FluentEmail.Razor/nuget/v3/index.json
    * see the [nuget.config](nuget.config) file from this repo for an example how this works
    * this basically let's you use nuget.org as normal but adds this one package, as this feed is built just for the sole purpose of providing this one package
    * alternatively you could add the following myget feed of the creator of the up-to-date version instead but this can lead to issues with restoring packages in locked mode
      * https://www.myget.org/F/fluentemail/api/v3/index.json
2. Install dependencies

    ```powershell
    PM> Install-Package FluentEmail.Razor -Version 2.8.0 # 2.8 is only available in this projects vsts feed/by manual download/other custom feeds
    PM> Install-Package RazorLight -Version 2.0.0-beta7  # install manually to get the most up to date version
    PM> Install-Package Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation -Version 3.1.3 # runtime exceptions will be thrown if this is not installed
    PM> Install-Package FluentEmail.Postmark # just get the most recent version from nuget
    ```

3. Add the service in `Startup.cs`

    ```csharp
    public void ConfigureServices(IServiceCollection services)
    {
        // ...

        services.AddFluentEmail("from@example.com")
                .AddRazorRenderer()
                .AddPostmarkSender("POSTMARK_API_TEST");

        // ...
    }
    ```

4. Start using the injected service

    ```csharp
    internal class FluentMailer
    {
        private readonly IFluentEmailFactory emailFactory;

        public FluentMailer(IFluentEmailFactory emailFactory)
        {
            this.emailFactory = emailFactory;
        }

        public async Task SendExample(string to)
        {
            var mail = emailFactory.Create();
            mail.To(to); // from is set in service configuration
            mail.Subject("Example from FluentEmail.Postmark");
            // for using templates from file or resx check the FluentEmail docs
            mail.UsingTemplate("This is an example for you, @Model.Email!", new { Email = to });
            await mail.SendAsync().ConfigureAwait(false);
        }
    }
    ```

5. Instead of configuring your ServerToken in code, you should better move it to `appsettings.json`, i.e. like this

    ```json
    // in appsettings.json
    // everything except ServerToken is optional
    "FluentEmailPostmark": {
        "ServerToken": "POSTMARK_API_TEST",
        "TrackOpens": false,
        // None = 0, HtmlAndText = 1, HtmlOnly = 2, TextOnly = 3
        "TrackLinks": "HtmlOnly",
        "Tag": "aspnetcore",
        "Metadata": {
        "demo": "debug"
        }
    }
    ```

    ```csharp
    // in Startup.cs
    public void ConfigureServices(IServiceCollection services)
    {
        // ...

        var postmarkConf = Configuration
            .GetSection("FluentEmailPostmark")
            .Get<PostmarkSenderOptions>();

        services.AddFluentEmail("from@example.com")
                .AddRazorRenderer()
                .AddPostmarkSender(postmarkConf);

        // ...
    }
    ```
