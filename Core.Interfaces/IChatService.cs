using System.Threading;
using Core.DataModels;
using System.Threading.Tasks;

namespace Core.Interfaces;

public interface IChatService
{
    public Task ConnectAsync(CancellationToken cancellationToken);

    public void Disconnect();

    public Task SendMessageAsync(Message message);

}
