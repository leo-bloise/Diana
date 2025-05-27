using Discord;
using Discord.WebSocket;

namespace Diana.Core.Host
{
    public interface ICommand
    {
        SlashCommandBuilder Build();
        Task Handle(SocketSlashCommand command);
    }
}
