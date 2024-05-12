using Colyseus;
using Cysharp.Threading.Tasks;
using Source.Scripts.Infrastructure.Services;
using Source.Scripts.Multiplayer.Schemas;
using UnityEngine.AddressableAssets;

namespace Source.Scripts.Multiplayer
{
    public class MultiplayerService : IMultiplayerService, IInitializable
    {
        private const string ServerSettings = "ServerSettings";
        private const string RoomName = "game_room";
        
        private ColyseusSettings _serverSettings;
        private ColyseusClient _client;

        public async UniTask Initialize()
        {
            await LoadServerSettings();
            _client = new ColyseusClient(_serverSettings);

            Connect().Forget();
        }

        public async UniTask Connect()
        {
            await _client.JoinOrCreate<GameRoomState>(RoomName);
        }

        private async UniTask LoadServerSettings() => 
            _serverSettings = await Addressables.LoadAssetAsync<ColyseusSettings>(ServerSettings);
    }
}