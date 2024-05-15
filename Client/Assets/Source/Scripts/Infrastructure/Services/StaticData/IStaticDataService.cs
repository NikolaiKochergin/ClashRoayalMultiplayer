using Source.Scripts.GameCore.Deck.StaticData;
using Source.Scripts.StaticData;

namespace Source.Scripts.Infrastructure.Services.StaticData
{
    public interface IStaticDataService
    {
        public CardInfo ForCard(string id);
        int ForBattleDeckCapacity();
        URL ForURL();
        string ForBattleScene();
        int ForHandCapacity();
    }
}