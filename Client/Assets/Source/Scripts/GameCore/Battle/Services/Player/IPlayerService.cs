namespace Source.Scripts.GameCore.Battle.Services.Player
{
    public interface IPlayerService
    {
        Team Team { get; }
        void Initialize();
    }
}