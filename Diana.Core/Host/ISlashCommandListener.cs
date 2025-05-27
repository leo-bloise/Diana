using Discord.WebSocket;

namespace Diana.Core.Host
{
    public interface ISlashCommandListener
    {
        public Dictionary<string, Type> CommandMap { get; set; }
        Task OnSlashCommandAsync(SocketSlashCommand socketSlashCommand);
        void RegisterSlashCommandListener();
    }
}
