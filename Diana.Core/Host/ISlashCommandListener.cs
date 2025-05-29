using Discord.WebSocket;

namespace Diana.Core.Host
{
    public interface ISlashCommandListener
    {
        /// <summary>
        /// Map of command names to their respective handlers.
        /// </summary>
        public Dictionary<string, Type> CommandMap { get; set; }
        /// <summary>
        /// Listen for slash commands and handle them accordingly.
        /// </summary>
        Task OnSlashCommandAsync(SocketSlashCommand socketSlashCommand);
        /// <summary>
        /// Registers itself as a listener for slash commands on the Discord client.
        /// </summary>
        void RegisterSlashCommandListener();
    }
}
