using System;

namespace Source.Scripts.GameCore.UnitLogic
{
    public interface IHealth
    {
        float MaxValue { get; }
        float CurrentValue { get; }
        event Action Changed;
        event Action Died;
    }
}