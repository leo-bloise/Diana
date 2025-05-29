using Discord;
using Discord.WebSocket;

namespace Diana.Core.Host
{
    public interface ICommand
    {
        /// <summary>
        /// Builds the slash command for registration with Discord.
        /// </summary>
        SlashCommandBuilder Build();
        /// <summary>
        /// Handles the execution of the slash command.
        /// </summary>
        Task Handle(SocketSlashCommand command);
    }
}
