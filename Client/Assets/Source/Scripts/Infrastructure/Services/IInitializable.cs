using Cysharp.Threading.Tasks;

namespace Source.Scripts.Infrastructure.Services
{
    public interface IInitializable
    {
        public UniTask Initialize();
    }
}