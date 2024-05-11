using Cysharp.Threading.Tasks;

namespace Source.Scripts.Infrastructure.Services.StaticData
{
    public interface IStaticDataLoader
    {
        UniTask Load();
    }
}