using Lime.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Takenet.MessagingHub.Client.InfoEntertainmentExtensions.Navigation;

namespace BotPOC.Services
{
    public interface IMpaService
    {
        Task<NavigationResult> SendToMPA(List<Message> messageList, CancellationToken cancellationToken, Dictionary<string, string> navigationParameters = null);
        Task<NavigationResult> SendToMPA(Message message, CancellationToken cancellationToken, Dictionary<string, string> navigationParameters = null);
        Task<NavigationResult> SendToMPA(string message, Node from, CancellationToken cancellationToken, Dictionary<string, string> navigationParameters = null);
        Task<bool> IsInMPASessionAsync(Node from, CancellationToken cancellationToken);
    }
}
