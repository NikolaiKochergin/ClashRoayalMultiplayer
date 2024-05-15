namespace Source.Scripts.GameCore.Battle.Services.Enemy
{
    public interface IEnemyService
    {
        IReadOnlyTeam Team { get; }
        void Initialize(IReadOnlyTeam enemyTeam);
    }
}