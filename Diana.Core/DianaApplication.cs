using Diana.Core.Defaults;
using Diana.Core.Host;
using Diana.Core.Host.Settings;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Diana.Core
{
    /// <summary>
    /// DianaApplication is the main entry point for the Diana bot application.
    /// It starts the Discord client, registers commands, and handles logging by default.
    /// Besides, it uses the ISlashCommandListener to listen for slash commands and handle them accordingly.
    /// </summary>
    public class DianaApplication : IHost
    {
        public IServiceProvider Services { get; private set; }
        public IConfigurationRoot Configuration { get; private set; }
        public DiscordSocketClient Client { get; private set; }
        public ILogger<DianaApplication> Logger { get; private set; }
        public IEnumerable<Type> Commands { get; private set; }
        public DianaApplication(IServiceProvider serviceProvider, IConfigurationRoot configuration, IEnumerable<Type> commands)
        {
            Services = serviceProvider;
            Configuration = configuration;
            Client = serviceProvider.GetRequiredService<DiscordSocketClient>();
            Logger = serviceProvider.GetRequiredService<ILogger<DianaApplication>>();
            Commands = commands;
        }
        public void Dispose()
        {
            Client.Dispose();
        }
        private async Task RegisterCommands()
        {
            Dictionary<string, Type> commandMap = new Dictionary<string, Type>();
            foreach (Type commandType in Commands)
            {
                ICommand command = (ICommand) Services.GetRequiredService(commandType);
                SlashCommandBuilder builder = command.Build();
                await Client.CreateGlobalApplicationCommandAsync(builder.Build());
                Logger.LogInformation($"Registered command: {builder.Name}");
                commandMap[builder.Name] = commandType;
            }
            ISlashCommandListener slashCommandListener = Services.GetRequiredService<ISlashCommandListener>();
            slashCommandListener.CommandMap = commandMap;
            slashCommandListener.RegisterSlashCommandListener();
            Logger.LogInformation("Ready to handle slash commands!");
        }
        private Task LogDefault(LogMessage logMessage)
        {
            DianaLogEntry dianaLogEntry = DianaLogEntry.Adapt(logMessage);
            Logger.Log(dianaLogEntry.Level, dianaLogEntry.Message);
            return Task.CompletedTask;
        }
        public async Task StartAsync(CancellationToken cancellationToken = default)
        {
            DiscordSettings? settings = Configuration.GetSection(nameof(DiscordSettings)).Get<DiscordSettings>();
            if (settings == null) throw new ArgumentNullException($"{nameof(DiscordSettings)}");
            Client.Log += LogDefault;
            Client.Ready += RegisterCommands;
            await Client.LoginAsync(TokenType.Bot, settings.Token);
            await Client.StartAsync();
        }
        public async Task StopAsync(CancellationToken cancellationToken = default)
        {
            await Client.StopAsync();
        }
    }
}
