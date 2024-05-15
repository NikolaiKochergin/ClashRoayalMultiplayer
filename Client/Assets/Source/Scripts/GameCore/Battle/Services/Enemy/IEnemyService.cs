namespace Source.Scripts.GameCore.Battle.Services.Enemy
{
    public interface IEnemyService
    {
        Team Team { get; }
        void Initialize();
    }
}