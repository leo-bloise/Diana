# Diana ⚡

Diana is C# Library for building Discord bots. It is design to be easy to use and flexible, allowing developers to create bots with minimal effort.

## Features

- Based on .NET Core and `Discord.Net`
- Native IHost support
	- It allows you to create a bot using the same patterns as ASP.NET Core applications.
	- It allows you to host a discord bot and other services in the same process.
	- Dependency injection support
- Automatic command registration
	- It allows you to register slash commands automatically from the assembly

### Creating commands

Creating commands is easy, you just need to create a class that implements `ICommand` interface.
```csharp
public class PingCommand : ICommand
{
    public SlashCommandBuilder Build()
    {
        return new SlashCommandBuilder()
            .WithName("ping")
            .WithDescription("Replies with Pong!");
    }
    public async Task Handle(SocketSlashCommand command)
    {
        await command.RespondAsync("Pong!");
    }
}
```
The `Build` method is used to build the command and the `Handle` method is used to handle the command when it is executed. In this example, the command is called `ping` and it replies with `Pong!` when executed.
The command is registered automatically when the bot is started, so you don't need to register it manually.

### DianaApplication and DianaApplicationBuilder

`DianaApplicationBuilder` class is used to build the `DianaApplication` instance. It is similar to `IHostBuilder` in ASP.NET Core. You can use it to manually configure your `DianaApplication` or you can use the default configuration by calling `DianaApplicationBuilder.Create` static method.
Basically, the default `Create` method will create a `DianaApplicationBuilder` instance with the default configuration, which includes:

- Logging configuration
- `DiscordSettings` holding the discord token and other settings
- Registering the default `ClassLoader` for loading commands from the Entry Assembly.
- Registering the default `SlashCommandListener` for listening and handling slash commands.
- Calls `LoadCommands` method to load commands from the Entry Assembly using ClassLoader.

If you want to see a full of example of the use of this framework, you can check the [DianaSystem.Discord](https://github.com/leo-bloise/DianaSystem.Discord)