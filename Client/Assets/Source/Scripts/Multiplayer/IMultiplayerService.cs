using Cysharp.Threading.Tasks;

namespace Source.Scripts.Multiplayer
{
    public interface IMultiplayerService
    {
        UniTask Connect();
        UniTask CancelConnect();
    }
}