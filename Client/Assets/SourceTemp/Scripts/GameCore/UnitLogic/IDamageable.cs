namespace Source.Scripts.GameCore.UnitLogic
{
    public interface IDamageable : ITarget
    {
        IHealth Health { get; }
        
        void ApplyDamage(float value);
    }
}