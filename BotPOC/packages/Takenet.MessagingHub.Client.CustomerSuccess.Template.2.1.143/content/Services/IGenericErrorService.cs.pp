using Lime.Protocol;
using System.Threading;
using System.Threading.Tasks;

namespace $rootnamespace$.Services
{
    public interface IGenericErrorService
    {
        Task SendGenericErrorAsync(Node from, CancellationToken cancellationToken);
    }
}

