using Diana.Core.Host;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;

namespace Diana
{
    public class PingCommand : ICommand
    {
        private ILogger<PingCommand> _logger;
        public PingCommand(ILogger<PingCommand> logger)
        {
            _logger = logger;
        }
        public async Task Handle(SocketSlashCommand socketSlashCommand)
        {
            _logger.LogInformation($"Ping command executed by {socketSlashCommand.User.Username}");   
            await socketSlashCommand.RespondAsync("Pong!");
        }
        public SlashCommandBuilder Build()
        {
            return new SlashCommandBuilder()
                .WithName("ping")
                .WithDescription("Respond with pong!");
        }
    }
}
