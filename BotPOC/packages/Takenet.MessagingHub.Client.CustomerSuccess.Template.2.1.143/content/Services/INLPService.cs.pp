using System.Threading;
using System.Threading.Tasks;
using Lime.Protocol;
using Takenet.Iris.Messaging.Resources.ArtificialIntelligence;

namespace $rootnamespace$.Services
{
    public interface INLPService
    {
        Task<AnalysisResponse> ProcessAsync(string input, Message messageOriginator, CancellationToken cancellationToken);
    }
}