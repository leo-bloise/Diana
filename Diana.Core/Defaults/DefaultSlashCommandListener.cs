using Diana.Core.Host;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace Diana.Core.Defaults
{
    public class DefaultSlashCommandListener : ISlashCommandListener
    {
        private DiscordSocketClient _discordSocketClient;
        private IServiceProvider _serviceProvider;
        public Dictionary<string, Type>? CommandMap { get; set; }
        public DefaultSlashCommandListener(DiscordSocketClient discordSocketClient, IServiceProvider serviceProvider)
        {
            _discordSocketClient = discordSocketClient ?? throw new ArgumentNullException(nameof(discordSocketClient));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }
        public async Task OnSlashCommandAsync(SocketSlashCommand socketSlashCommand)
        {
            using IServiceScope scope = _serviceProvider.CreateScope();
            string commandName = socketSlashCommand.CommandName;
            if (CommandMap == null || !CommandMap.TryGetValue(commandName, out Type? commandType))
            {
                await socketSlashCommand.RespondAsync($"Command '{commandName}' not found.", ephemeral: true);
                return;
            }
            ICommand command = (ICommand) scope.ServiceProvider.GetRequiredService(commandType);
            command.Handle(socketSlashCommand);
        }
        public void RegisterSlashCommandListener()
        {
            _discordSocketClient.SlashCommandExecuted += OnSlashCommandAsync;
        }
    }
}
