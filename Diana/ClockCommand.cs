using Diana.Core.Host;
using Discord;
using Discord.WebSocket;

namespace Diana
{
    public class ClockCommand : ICommand
    {
        public SlashCommandBuilder Build()
        {
            return new SlashCommandBuilder()
                .WithName("clock")
                .WithDescription("Get the current time with bot's timezone");   
        }
        async Task ICommand.Handle(SocketSlashCommand command)
        {
            var now = DateTime.Now;
            await command.RespondAsync($"Now is {now}");
        }
    }
}
